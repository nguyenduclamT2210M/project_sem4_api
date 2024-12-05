namespace project_sem4_api.DTOs
{
    public class TimeEmployeeDTO
    { 
        public int id {  get; set; }
        public string name { get; set; }
        public TimeOnly timeStart { get; set; }
        public TimeOnly timeEnd { get; set; }
    }
}
