using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stokc;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{


  [Route("api/stock")]
  [ApiController]
    public class StockController:ControllerBase
    {
        private readonly ApplicationDbContext _context ;
         public StockController(ApplicationDbContext context)
         {
            _context = context;

         }
         
          [HttpGet]
          public async Task<IActionResult>GetAll()
           {
             var stocks = await _context.Stocks.ToListAsync();
           var stokDto = stocks .Select(s=> s.ToStockDto());
             
             return Ok(stocks);
          }
          [HttpGet("{id}")]
             public async Task<IActionResult> GetById([FromRoute] int id)
           {
             var stock = await _context.Stocks.FindAsync(id);
             if(stock == null){
                return NotFound();

             }
             return Ok(stock.ToStockDto()); 
          }
        [HttpPost]
        public async Task<IActionResult>  Create([FromBody] CreateStockRequestDto stokDto) {
                var stockModel =  stokDto.ToStockFromCreateDto();
              await _context.Stocks.AddAsync(stockModel);
             await _context.SaveChangesAsync();
                 return CreatedAtAction(nameof(GetById),new{id =stockModel.Id},stockModel.ToStockDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult>  Update([FromRoute ] int id,[FromBody] UpdateStockRequestDto dto)  {

            var stockModel =await _context.Stocks.FirstOrDefaultAsync(x=> x.Id == id);

             if(stockModel == null){
               return NotFound(); 
             }
             stockModel.Symbol = dto.Symbol;
             stockModel.CompanyName = dto.CompanyName;
             stockModel.Purchase = dto.Purchase;
             stockModel.LastDiv = dto.LastDiv;
             stockModel.Industry = dto.Industry;
             stockModel.MarketCap = dto.MarketCap;
            
           await  _context.SaveChangesAsync();
              return Ok(stockModel.ToStockDto());

        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult>  Delete([FromRoute ] int id) {
              var StokModel = await _context.Stocks.FirstOrDefaultAsync(x=>x.Id == id  );
              if(StokModel == null) {
                return NotFound();
              }
              _context.Stocks.Remove(StokModel);
            await  _context.SaveChangesAsync();
              return NoContent();
        }
    }
}