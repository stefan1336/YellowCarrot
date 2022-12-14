using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowCarrotStefanJohansson.Data;
using YellowCarrotStefanJohansson.Models;

namespace YellowCarrotStefanJohansson.Services
{
    internal class TagRepository
    {
        private readonly AppDbContext _context;

        public TagRepository(AppDbContext context)
        {
            _context = context;
        }

        public Tag? GetTag(int id)
        {
            return _context.Tags.FirstOrDefault(t => t.TagId == id);
        }
    }
}
