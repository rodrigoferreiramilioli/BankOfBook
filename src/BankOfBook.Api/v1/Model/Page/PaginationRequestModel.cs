namespace BankOfBook.Api.v1.Model.Page
{
    public class PaginationRequestModel
    {
        public PaginationRequestModel()
        {
            PageSize = 10;
            PageNumber = 0;
        }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
