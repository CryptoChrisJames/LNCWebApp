using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LNCLibrary.Models
{
    public enum Category
    {
        Shirts,
        Accesories,

    }
    public enum Gender
    {
        Men,
        Women,
        Both

    } 
    
    public class Product
    {
        public int ID { get; set; }
        [Display(Name ="Name")]
        public string ProductName { get; set; }
        public int Price { get; set; }
        [Display(Name = "Gender")]
        public Gender GenderOption { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }
        [Display(Name ="Created")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Description")]
        public string ProductDescription { get; set; }
        public ICollection<Size> AvailableSizes { get; set; }
        [Display(Name = "Profile Picture")]
        public string ProfilePicture { get; set; }
    }
}
