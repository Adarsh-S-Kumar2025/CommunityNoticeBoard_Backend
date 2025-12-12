using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Domain.Entities
{
    public class Community
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;

        public ICollection<UserCommunity> Members { get; private set; } = new List<UserCommunity>();
        public ICollection<Post> Posts { get; private set; } = new List<Post>();

        private Community() { }

        public Community(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }

}