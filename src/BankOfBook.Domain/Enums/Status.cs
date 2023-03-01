using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfBook.Domain.Enums
{
    //neste momento ele está disponível ou alugado?
    public enum Status
    {
        Rented,
        Available,
        UnAvailable
    }
}
