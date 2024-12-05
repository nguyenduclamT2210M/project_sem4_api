using System.ComponentModel.DataAnnotations;

namespace project_sem4_api.Entities
{
    public class Evaluate
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int rating { get; set; }
    }
}
