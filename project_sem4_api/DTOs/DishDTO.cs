﻿namespace project_sem4_api.DTOs
{
    public class DishDTO
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int statusId { get; set; }
        public string image {  get; set; }
        public int discount { get; set; }
        public int categoryId { get; set; }
    }
}
