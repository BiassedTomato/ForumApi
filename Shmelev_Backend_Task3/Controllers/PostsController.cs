using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shmelev_Backend_Task3
{
    public class PostsController : Controller
    {
        ForumContext _context;
        IPostService _postService;
        IUserService _userService;
        IMapper _mapper;
        IWebHostEnvironment _environment;
        IAttachmentService _attachmentService;

        public PostsController(
            IWebHostEnvironment env, IMapper mapper,
            IUserService userService, IPostService srv, IAttachmentService attachmentService,
            ForumContext ctx)
        {
            _userService = userService;
            _context = ctx;
            _postService = srv;
            _mapper = mapper;
            _environment = env;
            _attachmentService = attachmentService;
        }


        [Authorize]
        [HttpGet]
        public IActionResult Create(int threadId)
        {
            var model = new PostCreateModel()
            {
                ThreadId = threadId
            };

            return View("Create", model);
        }

        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PostCreateModel model, int threadId)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var userId = await _userService.GetUserId(HttpContext.User);

                    var post = _mapper.Map<Post>(model);

                    post.ThreadId = threadId;
                    post.AuthorId = userId;
                    post.Created = DateTime.Now;

                    if (model.Files != null)
                        foreach (var file in model.Files)
                        {
                            var stream = file.OpenReadStream();

                            Random rand = new Random();


                            var path = new StringBuilder()
                                .Append(_environment.WebRootPath)
                                .Append("\\uploads\\").Append("usr_")
                                .Append(DateTime.Now.ToString("dd'.'MM'.'yyyy_HH.mm.ss", CultureInfo.InvariantCulture))
                                .Append("_")
                                .Append(rand.Next())
                                .Append("_")
                                .Append(file.FileName)
                                .ToString();

                            var attachment = new Attachment()
                            {
                                Created = DateTime.Now,
                                FileName = file.FileName,
                                FilePath = path,
                                Post = post

                            };



                            using (FileStream fileStream = new FileStream(path, FileMode.Create))
                            {
                                await stream.CopyToAsync(fileStream);
                            }

                            await _attachmentService.CreateAttachment(attachment, false);

                        }

                    await _postService.CreatePost(post);

                    return RedirectToAction("Index", "Threads", new { threadId = threadId });
                }

                return View("Create", model);
            }
            catch
            {
                return View("Error", new ErrorViewModel() { Message = "Something unexpected happened. Working on fixing this. It's not you, it's me." });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int postId)
        {
            try
            {

                if (await HasNoRights(postId))
                {
                    return View("Error", new ErrorViewModel() { Message = "You cannot access this page." });
                }

                var post = await _postService.GetPost(postId);

                if (post == null)
                {
                    return View("Error", new ErrorViewModel() { Message = "We tried really hard, but couldn't find the post to delete." });
                }

                var model = new PostDeleteModel()
                {
                    PostId = postId,
                    Text = post.Text,
                    ThreadId = post.ThreadId
                };



                return View("Delete", model);
            }
            catch
            {
                return View("Error", new ErrorViewModel() { Message = "Something unexpected happened. Working on fixing this. It's not you, it's me." });
            }
        }

        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(PostDeleteModel model)
        {
            try
            {

                if (await HasNoRights(model.PostId))
                {
                    return View("Error", new ErrorViewModel() { Message = "You cannot access this page." });
                }
                await _postService.RemovePost(model.PostId);

                return RedirectToAction("Index", "Threads", new { threadId = model.ThreadId });
            }
            catch
            {
                return View("Error", new ErrorViewModel() { Message = "Something unexpected happened. Working on fixing this. It's not you, it's me." });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int postId)
        {
            try
            {

                var userId = await _userService.GetUserId(HttpContext.User);

                if (await HasNoRights(postId))
                {
                    return View("Error", new ErrorViewModel() { Message = "You cannot access this page." });
                }

                await HasNoRights(postId);

                var post = await _postService.GetPost(postId);

                if (post == null)
                {
                    return View("Error", new ErrorViewModel() { Message = "We tried really hard, but couldn't find the post to delete." });
                }

                if (post.AuthorId != userId)
                {
                    return View("Error", new ErrorViewModel() { Message = "Only the author can edit this message." });
                }

                var model = new PostEditModel()
                {
                    Text = post.Text,
                    PostId = postId,
                    ThreadId = post.ThreadId
                };

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
        public async Task<IActionResult> Edit(PostEditModel model)
        {
            try
            {

                if (await HasNoRights(model.PostId))
                {
                    return View("Error", new ErrorViewModel() { Message = "You cannot access this page." });
                }

                if (ModelState.IsValid)
                {
                    await _postService.EditPost(model.PostId, model);

                    return RedirectToAction("Index", "Threads", new { threadId = model.ThreadId });

                }

                return View("Edit", model);
            }
            catch
            {
                return View("Error", new ErrorViewModel() { Message = "Something unexpected happened. Working on fixing this. It's not you, it's me." });

            }
        }

        async Task<bool> HasNoRights(int postId)
        {
            var post = await _postService.GetPost(postId);
            var userId = await _userService.GetUserId(HttpContext.User);

            if (post == null)
                return false;

            return !HttpContext.User.IsInRole("Admin") &&
                !_userService.HasModerator(post.Thread.BoardId, userId) &&
                !await _postService.IsAuthor(userId, post.Id);
        }
    }
}
