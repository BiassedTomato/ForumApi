using System.Threading.Tasks;

namespace Shmelev_Backend_Task3
{
    public class AttachmentService : IAttachmentService
    {
        ForumContext _context;

        public AttachmentService(ForumContext ctx)
        {
            _context = ctx;
        }

        public async Task CreateAttachment(Attachment attachment, bool save = true)
        {
            await _context.Attachments.AddAsync(attachment);

            if (save)
                await _context.SaveChangesAsync();
        }
    }
}
