using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BankOfBook.Domain.Extensions
{
    public class ErrorResponseModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public List<Error> Errors { get; set; }

        [JsonIgnore]
        public IDictionary Data { get; set; }
    }
}
