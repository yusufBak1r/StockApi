using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stokc;
using api.Model;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>>GetAllAsync();
        Task<Stock?>GetByIdAsync(int id);
        Task<Stock>CreateAsync(Stock stockModel);
        Task<Stock?>UpdateAsync(int id,UpdateStockRequestDto stockDto);
        Task<Stock?>DeleteAsync(int id);
        Task<bool>StokExisting(int id);
    }
}