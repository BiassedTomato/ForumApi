using AutoMapper;

namespace Shmelev_Backend_Task3
{
    public class ForumMapperProfile:Profile
    {
        public ForumMapperProfile()
        {
            CreateMap<Board,BoardCreateModel>().ReverseMap();
            CreateMap<Board,BoardEditModel>().ReverseMap();

            CreateMap<Thread, ThreadCreateModel>().ReverseMap();
            CreateMap<Thread, ThreadEditModel>().ReverseMap();

            CreateMap<Post, PostCreateModel>().ReverseMap();
            CreateMap<Post, PostEditModel>().ReverseMap();


            CreateMap<Board, BoardViewDTO>().ReverseMap();
            CreateMap<Board, BoardCreateEditDTO>().ReverseMap();
            CreateMap<BoardEditModel, BoardCreateEditDTO>().ReverseMap();

            CreateMap<Thread, ThreadViewDTO>().ReverseMap();
            CreateMap<Thread, ThreadCreateEditDTO>().ReverseMap();
            CreateMap<ThreadEditModel, ThreadCreateEditDTO>().ReverseMap();

            CreateMap<Post, PostViewDTO>().ReverseMap();
            CreateMap<Post, PostCreateEditDTO>().ReverseMap();
            CreateMap<PostEditModel, PostCreateEditDTO>().ReverseMap();

        }
    }
}
