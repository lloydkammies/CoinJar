using CoinBase.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoinJar.Entities
{
    public class CoinJarContext :DbContext
    {
        public CoinJarContext(): base()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<CoinJarContext>());
        }

        public DbSet<CoinJarEntity> CoinJars { get; set; }
    }
}