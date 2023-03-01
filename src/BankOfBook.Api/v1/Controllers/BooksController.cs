using BankOfBook.Api.v1.Model;
using Microsoft.AspNetCore.Mvc;

namespace BankOfBook.Api.v1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }


        //Get Livros
        //Create Livros
        //Path and put livros
        //Delete livros
        // Vender livro
        // alugar livro
        // devolver livro        

        [HttpGet]
        public IEnumerable<BookGetResponseModel> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new BookGetResponseModel
            {
             
            })
            .ToArray();
        }
    }
}