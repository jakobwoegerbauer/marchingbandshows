using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Data
{
    public class Element
    {
        public string Name { get; set; }

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
                    max = Math.Max(max, SubElements.Max(s => s.StartTime + s.Transformation.Duration));
                }
                return max;
            }
        }

        public Formation StartFormation { get; set; }
        public GroupAction[] GroupActions { get; set; }
        public SubElement[] SubElements { get; set; }
    }
}
