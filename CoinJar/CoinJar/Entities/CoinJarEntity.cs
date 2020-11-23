using System;
using System.ComponentModel.DataAnnotations;

namespace CoinBase.Entities
{
    public class CoinJarEntity
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal Volume { get; set; }

    }


}
