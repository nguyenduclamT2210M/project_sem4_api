using System.ComponentModel.DataAnnotations;

namespace project_sem4_api.Models
{
    public class DishModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string name { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string description { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        public decimal price { get; set; }
        [Required(ErrorMessage = "Image is required.")]
        public IFormFile image { get; set; }
        [Required(ErrorMessage = "Discount is required.")]
        public int discount { get; set; }
        [Required(ErrorMessage = "Status Id is required.")]
        public int statusId { get; set; }
        [Required(ErrorMessage = "Category Id is required.")]
        public int categoryId { get; set; }
    }
}
