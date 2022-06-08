using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shmelev_Backend_Task3.Controllers
{
    public class APIController : Controller
    {
        IBoardService boardService;
        IThreadService threadService;
        IPostService postService;
        IMapper mapper;
        ForumContext context;

        public APIController(IMapper mapper, IBoardService board, IThreadService thread, IPostService post, ForumContext ctx)
        {
            boardService = board;
            threadService = thread;
            postService = post;
            context = ctx;
            this.mapper = mapper;
            
        }

        [Route("sections")]
        [HttpGet]
        public async Task<IActionResult> AllBoards()
        {
            try
            {
                return Ok(await boardService.GetAllBoardsDTO());

            }
            catch
            {
                return StatusCode(500);
            }
        }

        [Route("sections")]
        [HttpPost]
        public async Task<IActionResult> CreateBoard(BoardCreateEditDTO model)
        {
            try
            {
                if (model.Name == null)
                {
                    return BadRequest( new ResponseModel("No name specified"));
                }

                if(model.Description == null)
                {
                    return BadRequest(new ResponseModel("No description specified"));

                }

                var board = mapper.Map<Board>(model);
                
                await boardService.CreateBoard(board); ;
            }
            catch
            {
                return StatusCode(500,new ResponseModel("Internal server error"));

            }



            return Ok(new ResponseModel("OK"));
        }

        [Route("sections/{id}")]
        [HttpPut]
        public async Task<IActionResult> EditBoard(int id, BoardCreateEditDTO model)
        {
            try
            {
                if (model.Name == null)
                {
                    return BadRequest(new ResponseModel("No name specified"));
                }

                if (model.Description == null)
                {
                    return BadRequest(new ResponseModel("No description specified"));

                }

                if (await boardService.GetBoard(id) == null)
                    return NotFound();

                var board = mapper.Map<BoardEditModel>(model);

                board.Id = id;


                await boardService.EditBoard(id, board);

                return Ok(new ResponseModel("OK"));
            }
            catch
            {
                return StatusCode(500, new ResponseModel("Internal server error"));
            }
        }



        [Route("sections/{id}/topics")]
        [HttpGet]
        public async Task<IActionResult> GetThreads(int id)
        {
            var board = await boardService.GetBoard(id);

            if (board == null)
                return NotFound();

            var threads=new List<ThreadViewDTO>();

            foreach(var thread in board.Threads)
            {
                var dto = mapper.Map<ThreadViewDTO>(thread);

                threads.Add(dto);
            }



            return Ok(threads);
        }

        [Route("sections/{id}/topics")]
        [HttpPost]
        public async Task<IActionResult> CreateThread(int id, ThreadCreateEditDTO model)
        {
            try
            {
                var board = await boardService.GetBoard(id);

                if (board == null)
                    return NotFound();

                if (model.Name == null)
                {
                    return BadRequest(new ResponseModel("No name specified"));
                }

                if (model.Description == null)
                {
                    return BadRequest(new ResponseModel("No description specified"));

                }

                var thread = mapper.Map<Thread>(model);

                await threadService.CreateThread(thread); ;
            }
            catch
            {
                return StatusCode(500, new ResponseModel("Internal server error"));

            }



            return Ok(new ResponseModel("OK"));
        }

        [Route("topics/{id}")]
        [HttpPut]
        public async Task<IActionResult> EditThread(int id, BoardCreateEditDTO model)
        {
            try
            {
                if (model.Name == null)
                {
                    return BadRequest(new ResponseModel("No name specified"));
                }

                if (model.Description == null)
                {
                    return BadRequest(new ResponseModel("No description specified"));

                }

                if (await threadService.GetThread(id) == null)
                    return NotFound();

                var thread = mapper.Map<ThreadEditModel>(model);


                await threadService.EditThread(id, thread); ;
            }
            catch
            {
                return StatusCode(500, new ResponseModel("Internal server error"));
            }

            return Ok(new ResponseModel("OK"));
        }

        [Route("topics/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteThread(int id)
        {
            try
            {
                var thread = threadService.GetThread(id);

                if (thread == null)
                    return NotFound();

                await threadService.RemoveThread(id);

                return Ok();
            }

            catch
            {
                return StatusCode(500);
            }
        }




        [Route("topics/{id}/messages")]
        [HttpGet]
        public async Task<IActionResult> GetPosts(int id)
        {
            var thread = await threadService.GetThread(id);

            if (thread == null)
                return NotFound();

            var posts = new List<PostViewDTO>();

            foreach (var post in thread.Posts)
            {
                var dto = mapper.Map<PostViewDTO>(post);

                posts.Add(dto);
            }
            return Ok(posts);
        }

        [Route("topics/{id}/messages")]
        [HttpPost]
        public async Task<IActionResult> CreatePost(int id, PostCreateEditDTO model)
        {
            try
            {
                var thread = await threadService.GetThread(id);

                if (thread == null)
                    return NotFound();

                if (model.Text == null)
                {
                    return BadRequest(new ResponseModel("No name specified"));
                }

                var post = mapper.Map<Post>(model);

                await postService.CreatePost(post); ;
            }
            catch
            {
                return StatusCode(500, new ResponseModel("Internal server error"));

            }



            return Ok(new ResponseModel("OK"));
        }

        [Route("messages/{id}")]
        [HttpPut]
        public async Task<IActionResult> EditPost(int id, PostCreateEditDTO model)
        {
            try
            {
                if (model.Text == null)
                {
                    return BadRequest(new ResponseModel("No name specified"));
                }


                if (await postService.GetPost(id) == null)
                    return NotFound();

                var post = mapper.Map<PostEditModel>(model);


                await postService.EditPost(id, post); ;
            }
            catch
            {
                return StatusCode(500, new ResponseModel("Internal server error"));
            }

            return Ok(new ResponseModel("OK"));
        }

        [Route("topics/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                var post = threadService.GetThread(id);

                if (post == null)
                    return NotFound();

                await postService.RemovePost(id);

                return Ok();
            }

            catch
            {
                return StatusCode(500);
            }
        }
    }

    public class ResponseModel
    {

        public ResponseModel(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
