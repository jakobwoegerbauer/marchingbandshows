using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Data
{
    public class GroupAction
    {
        public int Delay { get; set; }
        public int Duration { get; set; }
        public double Priority { get; set; }
        public int[] Positions { get; set; }
        public string ActionType { get; set; }
        public int[] Dependencies { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}
