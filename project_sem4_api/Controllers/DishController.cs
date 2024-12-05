using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_sem4_api.Context;
using project_sem4_api.DTOs;
using project_sem4_api.Entities;
using project_sem4_api.Models;

namespace project_sem4_api.Controllers
{
    [ApiController]
    [Route("api/dish")]
    public class DishController : Controller
    {
        private readonly DataContext _context;
        public DishController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dishes = await _context.Dishes
                .Include(m => m.Category)
                .Include(m => m.StatusDish)
                .ToListAsync();

            return Ok(dishes);
        }
        [HttpGet("search/{id}")]
        public async Task<ActionResult<Dish>> GetDishWithDetails(int id)
        {
            var dish = await _context.Dishes
                .Include(m => m.Category)
                .Include(m => m.StatusDish)
                .FirstOrDefaultAsync(d => d.id == id); // Sử dụng FirstOrDefaultAsync với điều kiện

            if (dish == null)
            {
                return NotFound();
            }

            return Ok(dish);
        }
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Create([FromForm] DishModel creDishModel)
        {
            if (ModelState.IsValid)
            {
                // Đường dẫn tới thư mục chứa ảnh
                var path = "wwwroot/images";

                // Tạo tên tập tin duy nhất bằng GUID và tên tập tin gốc
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(creDishModel.image.FileName);

                // Kết hợp đường dẫn với tên tập tin để có đường dẫn đầy đủ
                var upload = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);

                // Kiểm tra và tạo thư mục nếu chưa tồn tại
                var directoryPath = Path.GetDirectoryName(upload);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Kiểm tra model.image không phải là null trước khi sao chép
                if (creDishModel.image != null)
                {
                    using (var stream = new FileStream(upload, FileMode.Create))
                    {
                        creDishModel.image.CopyTo(stream);
                    }
                }
                else
                {
                    throw new ArgumentNullException(nameof(creDishModel.image), "Ảnh không được null.");
                }
                Dish newDish = new Dish()
                {
                    name = creDishModel.name,
                    image = fileName,
                    description = creDishModel.description,
                    discount = creDishModel.discount,
                    categoryId = creDishModel.categoryId,
                    statusId = creDishModel.statusId,
                    price = creDishModel.price,
                };

                _context.Dishes.Add(newDish);
                _context.SaveChanges();
                return Created("Dish created.", new CategoryDTO
                {
                    name = newDish.name
                });
            }
            return BadRequest("Create Dish error.");
        }
        
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Update(int id, [FromForm] DishModel updateDishModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingDish = _context.Dishes.Find(id);
                    if (existingDish == null)
                    {
                        return NotFound("Dish not found.");
                    }

                    // Đường dẫn tới thư mục chứa hình ảnh
                    var path = Path.Combine("wwwroot", "images");

                    // Xử lý nếu có ảnh mới
                    if (updateDishModel.image != null && updateDishModel.image.Length > 0)
                    {
                        // Xóa ảnh cũ nếu tồn tại
                        var oldImagePath = Path.Combine(path, existingDish.image);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                        // Tạo tên file mới và lưu
                        var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(updateDishModel.image.FileName);
                        var newUploadPath = Path.Combine(Directory.GetCurrentDirectory(), path, newFileName);

                        using (var stream = new FileStream(newUploadPath, FileMode.Create))
                        {
                            updateDishModel.image.CopyTo(stream);
                        }

                        existingDish.image = newFileName;
                    }

                    // Cập nhật các thuộc tính khác
                    existingDish.name = updateDishModel.name;
                    existingDish.description = updateDishModel.description;
                    existingDish.price = updateDishModel.price;
                    existingDish.discount = updateDishModel.discount;
                    existingDish.categoryId = updateDishModel.categoryId;
                    existingDish.statusId = updateDishModel.statusId;

                    _context.SaveChanges();
                    return Ok("Dish updated successfully.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            return BadRequest("Invalid input data.");
        }
       
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingDish = _context.Dishes.Find(id);
                if (existingDish == null)
                {
                    return NotFound("Dish not found.");
                }

                // Đường dẫn tới thư mục chứa hình ảnh
                var path = Path.Combine("wwwroot", "images");

                // Xóa ảnh nếu tồn tại
                var imagePath = Path.Combine(path, existingDish.image);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                // Xóa món ăn khỏi cơ sở dữ liệu
                _context.Dishes.Remove(existingDish);
                _context.SaveChanges();

                return Ok("Dish deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
