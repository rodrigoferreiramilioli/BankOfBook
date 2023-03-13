using ABCBrasil.VCF.Tofi.Purchase.UseCases.Mapping;
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

        public BooksController(
            IBookRepository bookRepository, 
            IActionDescriptorCollectionProvider provider) : base(provider)
        {
            _bookRepository = bookRepository;
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
            var book = await _bookRepository.GetByIdAsync(id);
            return this.CreateResponse(book?.ToResponseModel()!);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BookCreateResquestModel), 200)]
        public async Task PostAsync([FromBody] BookCreateResquestModel bookCreateResquestModel)
        {
            await _bookRepository.CreateAsync(bookCreateResquestModel.ToModel()!);
            this.CreateResponse(HttpStatusCode.OK);
        }

        //Get Livros
        //Create Livros
        //Path and put livros
        //Delete livros
        // Vender livro
        // alugar livro
        // devolver livro   
    }
}