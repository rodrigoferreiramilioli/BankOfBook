using BankOfBook.Api.Mappers;
using BankOfBook.Api.v1.Extensions;
using BankOfBook.Api.v1.Model;
using BankOfBook.Api.v1.Model.Page;
using BankOfBook.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using ControllerBase = BankOfBook.Api.v1.Extensions.ControllerBase;

namespace BankOfBook.Api.v1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookService _bookService;

        public BooksController(
            IBookRepository bookRepository,
            IBookService bookService,
            IActionDescriptorCollectionProvider provider) : base(provider)
        {
            _bookRepository = bookRepository;
            _bookService = bookService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<BookGetResponseModel>), 200)]
        public async Task<List<BookGetResponseModel>> GetAsync([FromRoute] PaginationRequestModel pagination)
        {
            var (book, totalResults) = await _bookRepository.GetAsync(pagination.ToModel()!);
            return this.CreateResponse(book?.ToResponseModel()!, totalResults);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(List<BookGetResponseModel>), 200)]
        public async Task<BookGetResponseModel> GetAsync([FromRoute] Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id.ToString());
            return this.CreateResponse(book?.ToResponseModel()!);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BookCreateResquestModel), 200)]
        public async Task PostAsync([FromBody] BookCreateResquestModel bookCreateResquestModel)
        {
            await _bookService.CreateAsync(bookCreateResquestModel.ToModel()!);
            this.CreateResponse(HttpStatusCode.OK);
        }

        //Create Livros
        //Path and put livros
        //Delete livros
        // Vender livro
        // alugar livro
        // devolver livro   
    }
}