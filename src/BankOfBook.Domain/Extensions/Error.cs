namespace BankOfBook.Domain.Extensions
{
    public class Error
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public Error()
        {
        }

        public Error(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}