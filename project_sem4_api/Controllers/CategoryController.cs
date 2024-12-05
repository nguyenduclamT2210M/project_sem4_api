using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using project_sem4_api.Context;
using project_sem4_api.DTOs;
using project_sem4_api.Entities;
using project_sem4_api.Models;
using System.Security.Claims;

namespace project_sem4_api.Controllers
{
    [ApiController]
    [Route("/api/v1/category")]
    public class CategoryController : Controller
    {
        private readonly DataContext _context;
        public CategoryController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<CategoryDTO> categories = _context.Categories
                .Select(m => new CategoryDTO
                {
                   id  = m.id,
                     name= m.name,
                     image= m.image,
                    
                }).ToList();
            return Ok(categories);
        }
        [HttpGet("searchbyid")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories
                .FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return category;
        }
        [HttpPost]
        public IActionResult Create([FromForm] CategoryModel creCategoryModel)
        {
            if (ModelState.IsValid)
            {
                // Đường dẫn tới thư mục chứa ảnh
                var path = "wwwroot/images";

                // Tạo tên tập tin duy nhất bằng GUID và tên tập tin gốc
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(creCategoryModel.image.FileName);

                // Kết hợp đường dẫn với tên tập tin để có đường dẫn đầy đủ
                var upload = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);

                // Kiểm tra và tạo thư mục nếu chưa tồn tại
                var directoryPath = Path.GetDirectoryName(upload);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Kiểm tra model.image không phải là null trước khi sao chép
                if (creCategoryModel.image != null)
                {
                    using (var stream = new FileStream(upload, FileMode.Create))
                    {
                        creCategoryModel.image.CopyTo(stream);
                    }
                }
                else
                {
                    throw new ArgumentNullException(nameof(creCategoryModel.image), "Ảnh không được null.");
                }
                Category newCa = new Category()
                {
                    name = creCategoryModel.name,
                    image = fileName,
                };

                _context.Categories.Add(newCa);
                _context.SaveChanges();
                return Created("Food created.", new CategoryDTO
                {
                    name = newCa.name
                });
            }
            return BadRequest("Create category error.");
        }
        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Edit([FromForm]CategoryModel updateCategoryModel, int id)
        {
            if (ModelState.IsValid)
            {

                Category updateCategory = _context.Categories.Find(id);
                if(updateCategory == null)
                {
                    return NotFound("Category not found.");
                }
                updateCategory.name = updateCategoryModel.name;
                if (updateCategoryModel.image != null) {

                    var path = "wwwroot/images";

                    var oldThumb = Path.Combine(path, updateCategory.image);
                    System.IO.File.Delete(oldThumb);


                    var newFileName = Guid.NewGuid().ToString() + Path.GetFileName(updateCategoryModel.image.FileName);

                    var upload = Path.Combine(Directory.GetCurrentDirectory(), path, newFileName);

                    using (var stream = new FileStream(upload, FileMode.Create))
                    {
                        updateCategoryModel.image.CopyTo(stream);
                    }
                    updateCategory.image = newFileName;
                    _context.SaveChanges();

                    return Ok("Category updated successfully.");
                }
            }
            return BadRequest("Error");
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Category delCategory = _context.Categories.Find(id);
                if(delCategory == null)
                {
                    return NotFound("Category not found.");
                }
                var path = "wwwroot/images";
                var imgPath = Path.Combine(path, delCategory.image);

                if (!System.IO.File.Exists(imgPath))
                {
                    return NotFound("Image file not found.");
                }

                System.IO.File.Delete(imgPath);
                _context.Categories.Remove(delCategory);
                _context.SaveChanges();

                return Ok("Food deleted.");
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
    }
}
