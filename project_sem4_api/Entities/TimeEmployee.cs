using System.ComponentModel.DataAnnotations;

namespace project_sem4_api.Entities
{
    public class TimeEmployee
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string timeStart { get; set; }
        public string timeEnd { get; set; }
    }
}
