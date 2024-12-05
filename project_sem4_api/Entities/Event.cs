using System.ComponentModel.DataAnnotations;

namespace project_sem4_api.Entities
{
    public class Event
    {
      
        public int id { get; set; }
        public string name { get; set; }
         public Decimal discount { get; set; }
        public DateTime dayStart {  get; set; }
        public DateTime dayEnd { get; set; }
    }
}
