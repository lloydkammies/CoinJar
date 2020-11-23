using CoinBase.Entities;
using CoinBase.Models;
using CoinJar.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoinBase.Managers
{
    public class CoinJar : ICoinJar
    {
        private readonly CoinJarContext _context;
        public CoinJar(CoinJarContext context)
        {
            _context = context;
        }
        public void AddCoin(ICoin coin)
        {
            _context.CoinJars.Add(new CoinJarEntity() { Name = "Jar", Amount = coin.Amount, Volume = coin.Volume });
            _context.SaveChanges();
        }

        public decimal GetTotalAmount()
        {
            var result = _context.CoinJars.Select(g => g.Amount).Sum();
            return result;
        }

        public void Reset()
        {
            var results = _context.CoinJars.Select(x => x);
            _context.CoinJars.RemoveRange(results);

            _context.SaveChanges();
        }

        public decimal OuncesLeft()
        {
            var result = _context.CoinJars.Select(g => g.Volume).ToList();
            return (result == null || result.Count == 0) ? 42 : 42 - result.Sum();
        }
    }
}

