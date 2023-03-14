using BankOfBook.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BankOfBook.Api.v1.Model
{
    public class BookCreateResquestModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public decimal PriceSale { get; set; }
        [Required]
        public decimal PriceRent { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public string? Author { get; set; }
    }
}
