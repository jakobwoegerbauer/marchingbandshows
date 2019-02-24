using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Data
{
    public class Formation
    {
        public string FormationTypeIdentifier { get; private set; }

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
