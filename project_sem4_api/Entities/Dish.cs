using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_sem4_api.Entities
{
    public class Dish
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public string image {  get; set; }
        public int discount { get; set; }
        public int categoryId { get; set; }
        [ForeignKey("categoryId")]
        public Category Category { get; set; }
        public int statusId { get; set; }
        [ForeignKey("statusId")]
        public StatusDish StatusDish{ get; set;}
    }
}
