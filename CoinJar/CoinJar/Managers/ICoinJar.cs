using CoinBase.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinBase.Managers
{
    public interface ICoinJar
    {
        void AddCoin(ICoin coin);
        decimal GetTotalAmount();
        void Reset();

        decimal OuncesLeft();
    }
}

