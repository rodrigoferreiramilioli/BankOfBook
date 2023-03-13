using BankOfBook.Domain.Enums;

namespace BankOfBook.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal PriceSale { get; set; }
        public decimal PriceRent { get; set; }
        public Category Category { get; set; }
        public string? Author { get; set; }
        public Status StatusLocalization { get; set; }
        public DateTime? RentDate { get; set; } // Quando estiver disponível,~será null
        public DateTime? RentReturnDate { get; set; } // Quando estiver disponível,~será null
        public Audit? Audit { get; set; }
    }
}