using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Shmelev_Backend_Task3
{
    public class ThreadService : IThreadService
    {
        ForumContext _context;

        IUserService _userService;
        IMapper _mapper;

        public ThreadService(IMapper mapper, IUserService userService, ForumContext ctx)
        {
            _mapper = mapper;
            _userService = userService;
            _context = ctx;
        }

        public async Task CreateThread(Thread thread)
        {
            await _context.Threads.AddAsync(thread);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveThread(int threadId)
        {
            var thread = await _context.Threads.FindAsync(threadId);

            _context.Threads.Remove(thread);
            await _context.SaveChangesAsync();


        }

        public async Task EditThread(int threadId, ThreadEditModel model)
        {
            var entity = await _context.FindAsync<Thread>(threadId);

            if (entity == null)
            {
                throw new Exception($"No entity with a primary key {model.ThreadId}");
            }

            _mapper.Map(model, entity);


            entity.Modified = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task<Thread> GetThread(int threadId)
        {

            return await _context.Threads.Include(x => x.Posts).ThenInclude(x => x.Attachments).FirstOrDefaultAsync(x => x.Id == threadId);
        }

        public async Task<bool> IsAuthor(string userId, int threadId)
        {
            return (await _userService.GetThreadAuthor(threadId)) == userId;
        }
    }
}
