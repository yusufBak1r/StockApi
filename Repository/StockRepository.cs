using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
       
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<List<Stock>> GetStocks()
        {
           return _context.Stocks.ToListAsync();
        }
   }
}