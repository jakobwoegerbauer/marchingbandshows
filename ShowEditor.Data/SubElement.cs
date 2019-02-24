using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Data
{
    /// <summary>
    /// Subelement of an show element
    /// </summary>
    public class SubElement
    {
        /// <summary>
        /// Starttime of the subelement. A starttime of 0 does not delay the actions in the element.
        /// </summary>
        public int StartTime { get; set; }

        /// <summary>
        /// The element
        /// </summary>
        public Element Element { get; set; }

        /// <summary>
        /// Maps the position of the parent element to the subelement.
        /// Empty mappings are marked with -1.
        /// </summary>
        public int[] PositionMapping { get; set; }
    }
}
