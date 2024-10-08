using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stokc;
using api.Interfaces;
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
        private readonly IStockRepository _stockRepository;
         public StockController(ApplicationDbContext context,IStockRepository stockRepository)
         {
            _context = context;
            _stockRepository = stockRepository;

         }
         
          [HttpGet]
          public async Task<IActionResult>GetAll()
           {
              
             var stocks = await _stockRepository.GetAllAsync();
             var stokDto = stocks.Select(s=> s.ToStockDto()).ToList();
             
             return Ok(stocks);
          }
          [HttpGet("{id:int}")]
             public async Task<IActionResult> GetById([FromRoute] int id)
           {
            if(!ModelState.IsValid) 
             return BadRequest(ModelState);
             var stock = await _stockRepository.GetByIdAsync(id);
             if(stock == null){
                return NotFound();

             }
             return Ok(stock.ToStockDto()); 
          }
        [HttpPost]
        public async Task<IActionResult>  Create([FromBody] CreateStockRequestDto stokDto) {
        
                var stockModel =  stokDto.ToStockFromCreateDto();
                          await _stockRepository.CreateAsync(stockModel);
                 return CreatedAtAction(nameof(GetById),new{id =stockModel.Id},stockModel.ToStockDto());
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult>  Update([FromRoute] int id,[FromBody] UpdateStockRequestDto dto)  {
       

            var stockModel =await _stockRepository.UpdateAsync(id,dto);

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
        [Route("{id:int}")]
        public async Task<IActionResult>  Delete([FromRoute ] int id) {
        
   var StokModel = await _stockRepository.DeleteAsync(id);
              if(StokModel == null) {
                return NotFound();
              }
            
              return NoContent();
        }
    }
}