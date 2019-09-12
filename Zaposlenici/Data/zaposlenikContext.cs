using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Zaposlenici.Models
{
    public class zaposlenikContext : DbContext
    {
        public zaposlenikContext (DbContextOptions<zaposlenikContext> options)
            : base(options)
        {
        }

        public DbSet<Zaposlenici.Models.zaposlenik> zaposlenik { get; set; }
    }
}
