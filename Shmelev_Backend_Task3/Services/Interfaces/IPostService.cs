using System.Threading.Tasks;

namespace Shmelev_Backend_Task3
{
    public interface IPostService
    {
        Task CreatePost(Post post);

        Task RemovePost(int id);

        Task EditPost(int id, PostEditModel model);

        Task<Post> GetPost(int id);

        Task<bool> IsAuthor(string userId, int postId);
    }
}
