using System.ComponentModel.DataAnnotations;

namespace Shmelev_Backend_Task3
{
    public class ThreadCreateModel
    {
        [StringLength(100,MinimumLength =2)]
        public string Name { get; set; }

        [StringLength(999,MinimumLength =2)]
        public string Description { get; set; }

        public int BoardId { get; set; }
    }
}
