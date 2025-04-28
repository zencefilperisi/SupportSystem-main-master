using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportRequestManagement.Application.Features.SupportRequestComment.Dtos
{
    public class SupportRequestCommentDto
    {
        public int Id { get; set; }
        public int SupportRequestId { get; set; }
        public int UserId { get; set; } // ID of the user who made the comment
        public string Username { get; set; } // Username of the user (denormalized for easier display)
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsAdminComment { get; set; }
    }
}
