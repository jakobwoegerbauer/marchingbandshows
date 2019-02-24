using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor
{
    public class MovementDependency
    {
        public int Position { get; set; }

        /// <summary>
        /// Set this to true if you need the dependencies' position of this step
        /// to force topological order.
        /// </summary>
        public bool Concurrent{ get; set; }
    }
}
