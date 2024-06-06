﻿using Finshark.DTOs.Comment;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finshark.DTOs.Stock
{
    public class StockDTO
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = "";
        public string CompanyName { get; set; } = "";
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = "";
        public long MarketCap { get; set; }
        public List<CommentDTO> Comments { get; set; }
    }
}
