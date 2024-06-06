using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finshark.DTOs.Stock
{
    public class CreateStockRequestDTO
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol can not be over 10 characters")]
        public string Symbol { get; set; } = "";

        [Required]
        [MaxLength(20, ErrorMessage = "Company name can not be over 20 characters")]
        public string CompanyName { get; set; } = "";

        [Required]
        [Range(1, 1000000000)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "Industry can not be over 15 characters")]
        public string Industry { get; set; } = "";

        [Required]
        [Range(1, 5000000000)]
        public long MarketCap { get; set; }
    }
}
