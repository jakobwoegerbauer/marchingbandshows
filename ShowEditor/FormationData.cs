using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Data
{
    public class FormationData
    {
        public string FormationTypeIdentifier { get; set; }

        public string Name { get; set; }

        public Position[] Positions { get; set; }

        public Dictionary<string, object> Parameters { get; set; }

        public FormationData(string formationTypeIdentifier)
        {
            FormationTypeIdentifier = formationTypeIdentifier; 
            Positions = new Position[0];
            Parameters = new Dictionary<string, object>();
        }
    }
}
