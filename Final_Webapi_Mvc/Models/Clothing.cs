using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Final_Webapi_Mvc.Models
{
    public class Clothing
    {
        [Required(ErrorMessage ="Please Enter ID")]
        public int CatID { get; set; }

        [Required(ErrorMessage = "Please Enter Vaild Name")]
        public string CatName { get; set; }

        [Required(ErrorMessage = "Please Product SKU")]
        public string SKU { get; set; }

        [Required(ErrorMessage = "Please Enter Vaild Product Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public string Image { get; set; }
        [Required(ErrorMessage = "Enter vaild Category")]
        public int Catinv { get; set; }
    }
}