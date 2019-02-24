using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Data
{
    public class BasicFormation : Formation
    {
        public BasicFormation() : base("BasicFormation")
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
