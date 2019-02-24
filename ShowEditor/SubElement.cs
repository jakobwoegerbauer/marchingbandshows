using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Data
{
    public class SubElement
    {
        public int StartTime { get; set; }
        public Element Transformation { get; set; }

        /// <summary>
        /// Maps the position of the parent element to the subelement.
        /// Empty mappings are marked with -1.
        /// </summary>
        public int[] PositionMapping { get; set; }
    }
}
