using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
namespace api.Data
{
    public class ApplicationDbContext:DbContext
    {
          public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options)
           :base(options)
        {

        }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        internal async Task<Comment?> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}