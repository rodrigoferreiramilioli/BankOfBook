using BankOfBook.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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
        public DateOnly? RentDate { get; set; } // Quando estiver disponível,~será null
        public DateOnly? RentReturnDate { get; set; } // Quando estiver disponível,~será null
        public Audit? Audit { get; set; }
    }
}