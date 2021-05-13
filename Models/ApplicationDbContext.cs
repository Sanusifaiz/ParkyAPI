using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)  //constructor (ctor)
        {

        }

        // to add any model to the database "DbContext" u need to make an entry
        public DbSet<NationalPark> NationalPark { get; set; }
        public DbSet<Trail> Trails { get; set; }

    }
}
