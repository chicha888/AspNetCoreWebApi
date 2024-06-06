using Finshark.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Finshark.Mappers;
using Finshark.DTOs.Stock;
using Microsoft.EntityFrameworkCore;
using Finshark.Interfaces;
using Finshark.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Finshark.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        private readonly AppDBContext _context;
        public StockController(AppDBContext context, IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var stocks = await _stockRepository.GetAllAsync(query);
            var stocksDTO = stocks.Select(s => s.ToStockDTO()).ToList();
            return Ok(stocksDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStockRequestDTO stockRequestDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = stockRequestDTO.ToStockFromCreateDTO();
            await _stockRepository.CreateAsync(stockModel);
            return Ok(stockModel);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateStockRequestDTO updateStock)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = await _stockRepository.UpdateAsync(id, updateStock);

            if (stockModel == null) { return NotFound(); }

            return Ok(stockModel.ToStockDTO());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id) 
        {
            var stockModel = await _stockRepository.DeleteAsync(id);

            if (stockModel == null) { return NotFound(); }

            return NoContent();
        }
    }
}
