using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using real.Models;
using WebApplication6.Models;

namespace WebApplication6.Data
{
    public class WebApplication6Context : DbContext
    {
        public WebApplication6Context (DbContextOptions<WebApplication6Context> options)
            : base(options)
        {
        }

        public DbSet<real.Models.User>? User { get; set; }

        public DbSet<real.Models.Message>? Message { get; set; }

        public DbSet<real.Models.Contact>? Contact { get; set; }

        public DbSet<WebApplication6.Models.invite>? invite { get; set; }
    }
}
