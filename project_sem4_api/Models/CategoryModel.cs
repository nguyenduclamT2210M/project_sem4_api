using System.ComponentModel.DataAnnotations;

namespace project_sem4_api.Models
{
    public class CategoryModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string name { get; set; }
        [Required(ErrorMessage = "Image is required.")]
        public IFormFile image { get; set; }
    }
}
