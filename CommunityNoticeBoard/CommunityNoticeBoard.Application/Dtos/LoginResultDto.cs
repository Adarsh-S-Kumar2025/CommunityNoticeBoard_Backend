using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Dtos
{
    public record LoginResultDto(
        int Id,
        string Email,
        string Token
     );

}
