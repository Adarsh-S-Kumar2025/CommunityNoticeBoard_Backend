using CommunityNoticeBoard.Application.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Comment.Commands.CreateComment
{
    public class CreateCommentCommandHandler
        : IRequestHandler<CreateCommentCommand, int>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.Comment> _commentRepo;

        public CreateCommentCommandHandler(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.Comment> commentRepo)
        {
            _commentRepo = commentRepo;
        }

        public async Task<int> Handle(CreateCommentCommand request,CancellationToken cancellationToken)
        {
            var comment = new CommunityNoticeBoard.Domain.Entities.Comment(
                request.PostId,
                request.UserId,
                request.Content
            );

            await _commentRepo.AddAsync(comment, cancellationToken);
            await _commentRepo.SaveChangesAsync(cancellationToken);

            return comment.Id;
        }
    }
}