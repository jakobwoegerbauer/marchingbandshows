using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Data
{
    /// <summary>
    /// Specifies a formation
    /// </summary>
    public class Formation
    {
        /// <summary>
        /// Identifier of the formation type. When parsing the formation the according formation type is used.
        /// The default value is the empty string.
        /// </summary>
        public string FormationTypeIdentifier { get; private set; }

        /// <summary>
        /// Formation data
        /// </summary>
        public FormationData Data { get; set; }

        [JsonIgnore]
        public Position[] Positions => Data.Positions;

        [JsonIgnore]
        public int Size => Data.Positions.Length;

        public virtual Formation FromData(FormationData data)
        {
            throw new InvalidOperationException("FromData should only be called on classes deriving from Formation.");
        }

        public Formation(string formationTypeIdentifier)
        {
            FormationTypeIdentifier = formationTypeIdentifier;
        }
    }
}
