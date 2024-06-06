using Finshark.Interfaces;
using Finshark.Models;
using Finshark.Data;
using Microsoft.EntityFrameworkCore;
using Finshark.DTOs.Stock;
using Finshark.Helpers;

namespace Finshark.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDBContext _context;
        public StockRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock StockModel)
        {
            await _context.Stocks.AddAsync(StockModel);
            await _context.SaveChangesAsync();
            return StockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null) 
            {
                return null;
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject querry)
        {
            var stocks = _context.Stocks.Include(c => c.Comments).ThenInclude(a => a.AppUser).AsQueryable();

            if (!string.IsNullOrWhiteSpace(querry.CompanyName))
                stocks = stocks.Where(s => s.CompanyName.Contains(querry.CompanyName));

            if (!string.IsNullOrWhiteSpace(querry.Symbol))
                stocks = stocks.Where(s => s.Symbol.Contains(querry.Symbol));

            if(!string.IsNullOrWhiteSpace(querry.SortBy))
            {
                if(querry.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                    stocks = querry.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
            }

            var skipNumber = (querry.PageNumber - 1) * querry.PageSize;

            return await stocks.Skip(skipNumber).Take(querry.PageSize).ToListAsync();
        }

        public Task<Stock?> GetByIdAsync(int id)
        {
            return _context.Stocks.Include(c => c.Comments).FirstAsync(x => x.Id == id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(x => x.Symbol == symbol);
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO stockDto)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();
            return existingStock;
        }
    }
}
