using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using project_sem4_api.Context;
using project_sem4_api.DTOs;
using project_sem4_api.Entities;
using project_sem4_api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace project_sem4_api.Controllers
{
    [ApiController]
    [Route("/api/v1/auth")]
    public class AuthController : Controller
    {
        private readonly DataContext dbContext;
        private IConfiguration configuration;

        public AuthController(DataContext context, IConfiguration config)
        {
            dbContext = context;
            configuration = config;
        }

        //register
        [HttpPost("register")]
        public IActionResult Register(EmployeeModel regModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (dbContext.Employees.Any(user => user.email == regModel.email))
                    {
                        return Unauthorized("Email is already exist. Please enter another email.");
                    }
                    /*if(dbContext.Roles.Any(role => role.id != regModel.roleId))
                    {
                        return Unauthorized("Role Id is already exist. Please enter another role id.");
                    }*/
                    string salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(regModel.password, salt);

                    Employee newEmployee = new Employee
                    {
                        name = regModel.name,
                        email = regModel.email,
                        phone = regModel.phone,
                        roleId = regModel.roleId,
                        timeEmployeeId = regModel.timeEmployeeId,
                        password = hashedPassword
                    };

                    dbContext.Employees.Add(newEmployee);
                    dbContext.SaveChanges();
                    return Created("", new EmployeeDTO
                    {
                        id = newEmployee.id,
                        name = newEmployee.name,
                        email = newEmployee.email,
                        token = null
                    });
                }
                catch (Exception e)
                {
                    return Unauthorized("Registration error.");
                }
            }
            return Unauthorized("Registration error.");
        }

        //login and receive a token

        [HttpPost("login")]
        public IActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                // Tìm user theo email
                var user = dbContext.Employees
                    .Include(u => u.Role) // Include thông tin Role
                    .SingleOrDefault(u => u.email == loginModel.email);

                if (user != null)
                {
                    // Kiểm tra mật khẩu
                    bool passwordMatch = BCrypt.Net.BCrypt.Verify(loginModel.password, user.password);
                    if (passwordMatch)
                    {
                        // Lấy vai trò từ bảng Role
                        var roleName = user.Role?.name ?? "User"; // Nếu không có vai trò, mặc định là "User"

                        // Tạo payload cho JWT
                        var payload = new[]
                        {
                    new Claim(ClaimTypes.Email, user.email),
                    new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                    new Claim(ClaimTypes.Name, user.name),
                    new Claim(ClaimTypes.Role, roleName), // Thêm vai trò vào token
                    new Claim(JwtRegisteredClaimNames.Aud, configuration["Jwt:Audience"]),
                    new Claim(JwtRegisteredClaimNames.Iss, configuration["Jwt:Issuer"]),
                };

                        // Generate JWT token
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                        var token = new JwtSecurityToken(
                            issuer: configuration["Jwt:Issuer"],
                            audience: configuration["Jwt:Audience"],
                            claims: payload,
                            expires: DateTime.Now.AddMinutes(Convert.ToInt32(configuration["Jwt:LifeTime"])),
                            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                        );

                        // Trả về token và thông tin người dùng
                        return Ok(new EmployeeDTO()
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            id = user.id,
                            name = user.name,
                            email = user.email,
                            role = roleName // Trả role về client
                        });
                    }

                    return Unauthorized("Email or password incorrect");
                }

                return Unauthorized("Email or password incorrect");
            }

            return Unauthorized("Email or password incorrect");
        }

    }
}
 
