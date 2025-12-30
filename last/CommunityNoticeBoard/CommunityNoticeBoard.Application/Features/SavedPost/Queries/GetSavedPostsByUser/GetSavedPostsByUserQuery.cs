using CommunityNoticeBoard.Application.Dtos;
using CommunityNoticeBoard.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.SavedPost.Queries.GetSavedPostsByUser
{
    public class GetSavedPostsByUserQuery
        : IRequest<List<SavedPostDto>>
    {
        public int UserId { get; set; }
        public string? Title { get; set; }   
        public string? Category { get; set; }   
        public string? CommunityName { get; set; }
    }
}
