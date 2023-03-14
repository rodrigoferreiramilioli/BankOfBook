using BankOfBook.Domain.Enums;

namespace BankOfBook.Api.v1.Model
{
    public class BookGetResponseModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public decimal PriceSale { get; set; }
        public decimal PriceRent { get; set; }
        public Category Category { get; set; }
        public string? Author { get; set; }
        public Status StatusLocalization { get; set; }
        public DateTime? RentDate { get; set; }
        public DateTime? RentReturnDate { get; set; }
        public DateTime? SoldReturnDate { get; set; }
    }
}
