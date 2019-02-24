using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Data
{
    /// <summary>
    /// Simplest Formation without any additional information and functionality
    /// </summary>
    public class BasicFormation : Formation
    {
        public BasicFormation() : base("")
        {

        }

        public BasicFormation(FormationData data) : this()
        {
            Data = data;
        }

        public override Formation FromData(FormationData data)
        {
            return new BasicFormation(data);
        }
    }
}
