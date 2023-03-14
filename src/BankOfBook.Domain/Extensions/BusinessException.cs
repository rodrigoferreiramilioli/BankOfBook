using System.Collections;

namespace BankOfBook.Domain.Extensions
{
    public class BusinessException : Exception
    {
        public IDictionary InternalData { get; internal set; }

        public BusinessException(string message, string code, string description) : base(message)
        {
            Data.Add(code, description);
        }

        public BusinessException(string message, List<Error> errors) : base(message)
        {
            foreach (var error in errors)
                Data.Add(error.Code, error.Description);
        }
        public void SetInternalData(IDictionary internalData)
        {
            InternalData = internalData;
        }
    }
}