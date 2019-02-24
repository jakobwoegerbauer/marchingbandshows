using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Data
{
    /// <summary>
    /// A show element. Can contain multiple Groupactions and Subelements.
    /// </summary>
    public class Element
    {
        /// <summary>
        /// The name of the element
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The duration of the element is the maximum of the durations of all subelements and groupactions
        /// including their starttimes and delays.
        /// 
        /// </summary>
        [JsonIgnore]
        public int Duration
        {
            get
            {
                int max = 0;
                if (GroupActions != null)
                {
                    max = GroupActions.Max(g => g.Delay + g.Duration);
                }
                if(SubElements != null)
                {
                    max = Math.Max(max, SubElements.Max(s => s.StartTime + s.Element.Duration));
                }
                return max;
            }
        }

        /// <summary>
        /// The start formation of the element which is used if it has no parent element.
        /// </summary>
        public Formation StartFormation { get; set; }

        /// <summary>
        /// Groupactions of this element
        /// </summary>
        public GroupAction[] GroupActions { get; set; }

        /// <summary>
        /// Subelements of this element
        /// </summary>
        public SubElement[] SubElements { get; set; }
    }
}
