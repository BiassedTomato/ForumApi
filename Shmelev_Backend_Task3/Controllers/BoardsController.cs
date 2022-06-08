using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Shmelev_Backend_Task3
{
    public class BoardsController : Controller
    {
        ForumContext _context;

        IBoardService _boardService;

        IUserService _userService;

        IMapper _mapper;

        public BoardsController(IMapper mapper, IUserService userService, ForumContext ctx, IBoardService srv)
        {
            _context = ctx;
            _boardService = srv;
            _userService = userService;
            _mapper = mapper;

        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new IndexViewModel();


            model.Boards = _boardService.GetAllBoards().Result;

            return View(model);
        }

        public async Task<IActionResult> DisplayBoard(int boardId)
        {

            var board = await _boardService.GetBoard(boardId);

            if (boardId == 0)
            {
                return View("Error", new ErrorViewModel() { Message = "404 - not found" });

            }

            if (board == null)
            {
                return View("Error", new ErrorViewModel() { Message = "Couldn't find a board. Make sure the index is correct" });
            }

            BoardViewModel model = new BoardViewModel() { Threads = board.Threads.ToList(), BoardId = boardId };

            return View("BoardDisplay", model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int boardId)
        {
            try
            {

                if (!HttpContext.User.IsInRole("Admin") &&
                    !_userService.HasModerator(boardId, await _userService.GetUserId(HttpContext.User)))
                {
                    return View("Error", new ErrorViewModel() { Message = "You cannot edit this board" });
                }

                if (boardId == 0)
                    return View("Error", new ErrorViewModel() { Message = "404 - not found" });

                var board = await _boardService.GetBoard(boardId);

                BoardEditModel model = _mapper.Map<BoardEditModel>(board);

                return View("Edit", model);
            }
            catch
            {
                return View("Error", new ErrorViewModel() { Message = "Something unexpected happened. Working on fixing this. It's not you, it's me." });

            }
        }

        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(BoardEditModel model)
        {
                if (!HttpContext.User.IsInRole("Admin") &&
                    !_userService.HasModerator(model.Id, await _userService.GetUserId(HttpContext.User)))
                {
                    return View("Error", new ErrorViewModel() { Message = "You cannot edit this board." });
                }

                if (ModelState.IsValid)
                {
                    await _boardService.EditBoard(model.Id, model);


                    return RedirectToAction("Index");
                }


                return View("Edit", model);
           
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(BoardCreateModel model)
        {
            try
            {
                if (!HttpContext.User.IsInRole("Admin"))
                {
                    // Concealing the fact that there is creation board page so we don't accidentally spill any info useful for hackers
                    return View("Error", new ErrorViewModel() { Message = "404 - not found" });

                }

                if (ModelState.IsValid)
                {

                    var board = new Board();

                    board = _mapper.Map<Board>(model);

                    await _boardService.CreateBoard(board);


                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch
            {
                return View("Error", new ErrorViewModel() { Message = "Something unexpected happened. Working on fixing this. It's not you, it's me." });
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int boardId)
        {
            try
            {
                var board = await _boardService.GetBoard(boardId);

                if (board == null)
                {
                    return View("Error", new ErrorViewModel() { Message = "No such thread found. Try another one, perhaps?" });

                }

                var model = new BoardDeleteModel()
                {
                    BoardId = boardId,
                    Name = board.Name
                };

                return View("Delete", model);
            }
            catch
            {
                return View("Error", new ErrorViewModel() { Message = "Something unexpected happened. Working on fixing this. It's not you, it's me." });
            }
        }

        [Authorize(Roles = "Admin")]

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(BoardDeleteModel model)
        {
            try
            {
                var board = await _boardService.GetBoard(model.BoardId);

                await _boardService.RemoveBoard(model.BoardId);
                return RedirectToAction("Index", "Boards");
            }
            catch
            {
                return View("Error", new ErrorViewModel() { Message = "Something unexpected happened. Working on fixing this. It's not you, it's me." });
            }
        }

    }
}
