using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shmelev_Backend_Task3
{
    public class BoardService : IBoardService
    {
        ForumContext _context;
        IMapper _mapper;

        public BoardService(IMapper mapper, ForumContext ctx)
        {
            _mapper = mapper;
            _context = ctx;
        }

        public async Task<List<Board>> GetAllBoards()
        {
            return await _context.Boards.ToListAsync();
        }

        public async Task<List<BoardViewDTO>> GetAllBoardsDTO()
        {
            var boards=await _context.Boards.ToListAsync();

            List<BoardViewDTO> views = new List<BoardViewDTO>();

            foreach(var board in boards)
            {
                var dto = _mapper.Map<BoardViewDTO>(board);
                views.Add(dto);
            }

            return views;
        }


        public async Task CreateBoard(Board board)
        {
            await _context.Boards.AddAsync(board);


            await _context.SaveChangesAsync();
        }

        public async Task DeleteBoard(int boardId)
        {
            Board board = new Board()
            {
                Id= boardId,
            };

            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();
        }

        public async Task EditBoard(int boardId, BoardEditModel model)
        {
            var entity = await _context.FindAsync<Board>(boardId);

            if (entity == null)
            {
                throw new Exception($"No entity with a primary key {boardId}");
            }
            _mapper.Map(model, entity);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Returns the thread with a specified ID, threads included.
        /// </summary>
        /// <param name="boardId"></param>
        /// <returns></returns>
        public async Task<Board> GetBoard(int boardId)
        {
            return await _context.Boards.Include(x => x.Threads).FirstOrDefaultAsync(x => x.Id == boardId);
        }

        public async Task RemoveBoard(int boardId)
        {
            var board = await _context.Boards.FindAsync(boardId);

            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();
        }
    }
}
