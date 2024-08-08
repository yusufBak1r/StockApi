using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace api.Repository
{
    public class CommentRespository : ICommentRepository
    {
      private  readonly ApplicationDbContext _context;
        public CommentRespository(ApplicationDbContext context)
        {
            _context = context;
            
        }

      

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
          await _context.AddAsync(commentModel);
          await _context.SaveChangesAsync();
          return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null){
              return null;

            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;

        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        { 
         return await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
           var existingComment = await _context.Comments.FindAsync(id);
           if(existingComment==null){
            return null;

           }
                existingComment.Title = commentModel.Title;
                existingComment.Content = commentModel.Content;
                await  _context.SaveChangesAsync();   

                return existingComment;
        }

    
    }
}