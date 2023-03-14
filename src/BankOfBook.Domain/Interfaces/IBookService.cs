using BankOfBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfBook.Domain.Interfaces
{
    public interface IBookService
    {
        Task CreateAsync(Book book);
    }
}
