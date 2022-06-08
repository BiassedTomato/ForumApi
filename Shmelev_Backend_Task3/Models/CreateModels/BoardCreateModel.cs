using System.ComponentModel.DataAnnotations;

namespace Shmelev_Backend_Task3
{
    public class BoardCreateModel
    {
        [StringLength(100,MinimumLength =1)]
        public string Name { get; set; }
    }
}
