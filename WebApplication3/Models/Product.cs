using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Błąd")]
        [Display(Name = "Cena label")]
        public decimal Price { get; set; }

        [Display(Name = "Kategoria")]
        public string Category { get; set; }
    }
}
