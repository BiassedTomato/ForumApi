using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Shmelev_Backend_Task3
{
    public class AdministrationController : Controller
    {
        IAdministrationService _admService;
        IBoardService _boardService;
        IUserService _userService;
        ForumContext _forumContext;
        public AdministrationController(ForumContext ctx, IBoardService bord, IAdministrationService srv, IUserService userService)
        {
            _forumContext = ctx;
            _admService = srv;
            _boardService = bord;
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {

            try
            {

                var model = new RolesEditModel()
                {
                    Users = _userService.GetUsersWithRole("Moderator"),
                    Boards = await _boardService.GetAllBoards()
                };


                return View(model);
            }
            catch
            {
                return View("Error", new ErrorViewModel() { Message = "Something unexpected happened. Working on fixing this. It's not you, it's me." });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AssignModerator(RolesEditModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    await _admService.AssignModerator(model.SelectedUserId, model.SelectedBoardId);

                    return RedirectToAction("Index");

                }

                return View(model);
            }
            catch
            {
                return View("Error", new ErrorViewModel() { Message = "Something unexpected happened. Working on fixing this. It's not you, it's me." });
            }
        }
    }
}
