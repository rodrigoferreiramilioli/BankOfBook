using BankOfBook.Domain.Enums;

namespace BankOfBook.Api.v1.Model
{
    public class BookCreateResquestModel
    {
        public string? Name { get; set; }
        public decimal PriceSale { get; set; }
        public decimal PriceRent { get; set; }
        public Category Category { get; set; }
        public string? Author { get; set; }
    }
}
