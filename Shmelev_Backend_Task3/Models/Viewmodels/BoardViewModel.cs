using System.Collections.Generic;

namespace Shmelev_Backend_Task3
{
    public class BoardViewModel
    {
        public List<Thread> Threads { get; set; }=new List<Thread>();
        
        public int BoardId { get; set; }
    }
}
