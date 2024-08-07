using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{
    [Route("api/comment")]
    [ApiController]

    public class CommentController:ControllerBase
    {
        private readonly ICommentRepository  _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo,IStockRepository stokRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stokRepo;
        }

        [HttpGet]
        public async Task<IActionResult>GettAll(){
            var comments = await _commentRepo.GetAllAsync();
            var commentDto = comments.Select(s=>s.ToCommentDto());
            return Ok(commentDto);

        }

          [HttpGet]
          [Route("{id}")]
          public async Task<IActionResult>GetById([FromRoute] int id) {

            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null){
                return NotFound();

            }
            return Ok(comment.ToCommentDto());
          }
            

          [HttpPost]
          [Route("{stockId}")]
          public async Task<IActionResult>Create([FromRoute] int stockId,CreateCommentDto commentDto) 
          {
            if (!await _stockRepo.StokExisting(stockId)) {
                return BadRequest("stock does not existing");

            }
                  var commentModel = commentDto.ToCommentFromCreate(stockId);
                  await _commentRepo.CreateAsync(commentModel);
                  
                  return CreatedAtAction(nameof(GetById),new{id = commentModel },commentModel.ToCommentDto());


          }
    
    }
}