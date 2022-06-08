using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shmelev_Backend_Task3
{
    public interface IBoardService
    {
        Task CreateBoard(Board board);
        Task DeleteBoard(int id);

        Task EditBoard(int id, BoardEditModel model);

        Task<Board> GetBoard(int id);

        Task<List<Board>> GetAllBoards();

        Task RemoveBoard(int boardId);

        Task<List<BoardViewDTO>> GetAllBoardsDTO();

    }
}
