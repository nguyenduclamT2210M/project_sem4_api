﻿using System.ComponentModel.DataAnnotations;

namespace project_sem4_api.Entities
{
    public class Category
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
    }
}
