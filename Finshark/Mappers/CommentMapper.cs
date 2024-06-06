using Finshark.DTOs.Comment;
using Finshark.Models;
using System.Runtime.CompilerServices;

namespace Finshark.Mappers
{
    public static class CommentMapper
    {
        public static CommentDTO ToCommentDTO(this Comment commentModel)
        {
            return new CommentDTO
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn.Date,
                CreatedBy = commentModel.AppUser.UserName,
                StockId = commentModel.StockId,
            };
        }

        public static Comment ToCommentFromCreate(this CreateCommentDTO commentDto, int stockId) 
        {
            return new Comment 
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId
            };
        }

        public static Comment ToCommentFromUpdate(this UpdateCommentRequestDTO updateDto)
        {
            return new Comment
            {
                Title = updateDto.Title,
                Content = updateDto.Content
            };
        }
    }
}
