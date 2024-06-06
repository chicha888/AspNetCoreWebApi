using System.ComponentModel.DataAnnotations;

namespace Finshark.DTOs.Comment
{
    public class UpdateCommentRequestDTO
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Title can not be over 280 characters")]
        public string Title { get; set; } = "";

        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Content can not be over 280 characters")]
        public string Content { get; set; }
    }
}
