using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shmelev_Backend_Task3
{
    public class Board
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name has to be between 3 and 100 symbols long", MinimumLength = 3)]
        public string Name { get; set; }

        public ICollection<Thread> Threads { get; set; } = new List<Thread>();

        public ModeratedBoard ModeratedBoard { get; set; }
        public int ModeratedBoardId { get; set; }
    }
}
