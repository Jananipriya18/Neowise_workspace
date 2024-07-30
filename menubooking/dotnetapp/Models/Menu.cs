using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Menu
    {
        [Key]
        public int menuId { get; set; }

        [Required(ErrorMessage = "Chef name is required")]
        public string chefName { get; set; }

        public string menuName { get; set; }

        [Required(ErrorMessage = "Description are required")]
        public string description { get; set; }

        [Required(ErrorMessage = "Price are required")]
        public string price { get; set; }

        public string availability { get; set; }

    }
}
