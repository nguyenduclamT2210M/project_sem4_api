namespace project_sem4_api.DTOs
{
    public class OrderDetailDTO
    {
        public int id { get; set; }
        public int dishId { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public string? note {   get; set; }
    }
}
