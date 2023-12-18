using Lab_3_Mini_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab_3_Mini_API.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Persons> Persons { get; set; }
        public DbSet<Interests> Interests { get; set; }
        

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
             
        }

        

    }
}
