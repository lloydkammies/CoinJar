using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinBase.Models
{
    public interface ICoin
    {
        decimal Amount { get; set; }
        decimal Volume { get; set; }
    }


}
