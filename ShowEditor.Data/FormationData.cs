using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Data
{
    /// <summary>
    /// Contains the positions of a formation
    /// </summary>
    public class FormationData
    {
        /// <summary>
        /// Name of the formation
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// All positions in the formation
        /// </summary>
        public Position[] Positions { get; set; }

        /// <summary>
        /// Additional parameters for specific formations like Rows, Columns, Depth and SideMargin for RowsFormations
        /// </summary>
        public Dictionary<string, object> Parameters { get; set; }

        public FormationData(string formationTypeIdentifier)
        {
            Positions = new Position[0];
            Parameters = new Dictionary<string, object>();
        }
    }
}
