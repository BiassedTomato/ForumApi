using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Shmelev_Backend_Task3
{
    public class ThreadsController : Controller
    {
        ForumContext _dbContext;

        IThreadService _threadService;

        IUserService _userService;

        IMapper _mapper;

        public ThreadsController(IMapper mapper, ForumContext ctx, IThreadService srv, IUserService usr)
        {
            _dbContext = ctx;
            _threadService = srv;
            _userService = usr;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int threadId)
        {
            try
            {
                var thread = await _threadService.GetThread(threadId);

                if (thread == null)
                {
                    HttpContext.Response.StatusCode = 404;

                    return View();
                }

                var model = new ThreadViewModel()
                {
                    Posts = thread.Posts.ToList(),
                    ThreadId = threadId
                };

                return View(model);
            }
            catch
            {
                return View("Error", new ErrorViewModel() { Message = "Something unexpected happened. Working on fixing this. It's not you, it's me." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int threadId)
        {
            try
            {

                var thread = await _threadService.GetThread(threadId);

                if (await HasNoRights(threadId))
                {
                    return View("Error", new ErrorViewModel() { Message = "You cannot access this page." });
                }

                if (thread == null)
                {
                    return View("Error", new ErrorViewModel() { Message = "No such thread found. Try another one, perhaps?" });

                }

                var model = new ThreadDeleteModel()
                {
                    ThreadId = threadId,
                    Name = thread.Name
                };

                return View("Delete", model);
            }
            catch
            {
                return View("Error", new ErrorViewModel() { Message = "Something unexpected happened. Working on fixing this. It's not you, it's me." });
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Delete(ThreadDeleteModel model)
        {
            try
            {
                if (await HasNoRights(model.ThreadId))
                {
                    return View("Error", new ErrorViewModel() { Message = "You cannot access this page." });
                }

                var thread = await _threadService.GetThread(model.ThreadId);

                await _threadService.RemoveThread(model.ThreadId);
                return RedirectToAction("DisplayBoard", "Boards", new { boardId = thread.BoardId });
            }
            catch
            {
                return View("Error", new ErrorViewModel() { Message = "Something unexpected happened. Working on fixing this. It's not you, it's me." });
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create(int boardId)
        {
            ThreadCreateModel model = new ThreadCreateModel();

            model.BoardId = boardId;

            return View("Create", model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ThreadCreateModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = await _userService.GetUserId(HttpContext.User);

                    var thread = _mapper.Map<Thread>(model);

                    thread.Created = DateTime.Now;
                    thread.AuthorId = userId;

                    await _threadService.CreateThread(thread);

                    return RedirectToAction("DisplayBoard", "Boards", new { boardId = model.BoardId });
                }

                return View(model);
            }
            catch
            {
                return View("Error", new ErrorViewModel() { Message = "Something unexpected happened. Working on fixing this. It's not you, it's me." });
            }
        }

        async Task<bool> HasNoRights(int threadId)
        {
            var thread = await _threadService.GetThread(threadId);
            var userId = await _userService.GetUserId(HttpContext.User);

            if (thread == null)
                return false;

            return !HttpContext.User.IsInRole("Admin") &&
                !_userService.HasModerator(thread.BoardId, userId) &&
                !await _threadService.IsAuthor(userId, threadId);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int threadId)
        {
            try
            {
                var thread = await _threadService.GetThread(threadId);

                var userId = await _userService.GetUserId(HttpContext.User);

                bool no = await HasNoRights(threadId);

                if (no)
                {
                    return View("Error", new ErrorViewModel() { Message = "You cannot access this page." });
                }


                var id = await _userService.GetUserId(HttpContext.User);

                if (thread.AuthorId != id)
                {
                    ErrorViewModel m = new ErrorViewModel()
                    {
                        Message = "Only the OP can edit this thread."
                    };

                    return View("Error", m);
                }

                var model = _mapper.Map<ThreadEditModel>(thread);
                model.ThreadId = threadId;

                return View("Edit", model);
            }
            catch
            {
                return View("Error", new ErrorViewModel() { Message = "Something unexpected happened. Working on fixing this. It's not you, it's me." });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(ThreadEditModel model)
        {
            try
            {

                var id = await _userService.GetUserId(HttpContext.User);

                bool no = await HasNoRights(model.ThreadId);

                if (no)
                {
                    return View("Error", new ErrorViewModel() { Message = "You cannot access this page." });
                }


                if (ModelState.IsValid)
                {
                    await _threadService.EditThread(model.ThreadId, model);
                    return RedirectToAction("DisplayBoard", "Boards", new { boardId = model.BoardId });
                }

                return View("Edit", model);

            }
            catch
            {
                return View("Error", new ErrorViewModel() { Message = "Something unexpected happened. Working on fixing this. It's not you, it's me." });
            }

        }
    }
}
