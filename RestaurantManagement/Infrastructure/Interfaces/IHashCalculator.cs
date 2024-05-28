using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Interfaces
{
    public interface IHashCalculator
    {
        string Calculate(string rawText);
    }
}
