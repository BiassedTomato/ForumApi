using System.Threading.Tasks;

namespace Shmelev_Backend_Task3
{
    public interface IAttachmentService
    {
        Task CreateAttachment(Attachment attachment, bool save = true);
    }
}
