using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Shmelev_Backend_Task3
{
    public class PostService:IPostService
    {
        ForumContext _context;

        IUserService _userService;
        IMapper _mapper;

        public PostService(IMapper mapper, IUserService userService, ForumContext ctx)
        {
            _mapper = mapper;
            _context = ctx;
            _userService = userService;
        }

        public async Task CreatePost(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task RemovePost(int postId)
        {
            var post = await _context.Posts.FindAsync(postId);

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task EditPost(int postId,PostEditModel model)
        {
            var entity = await _context.FindAsync<Post>(postId);

            _mapper.Map(model, entity);

            entity.Modified = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task<Post> GetPost(int postId)
        {
            return await _context.Posts.Include(x=>x.Thread).ThenInclude(x=>x.Board).FirstOrDefaultAsync(x => x.Id == postId);
        }

        public async Task<bool> IsAuthor(string userId, int postId)
        {
            return (await _userService.GetPostAuthor(postId)) == userId;
        }
    }
}
