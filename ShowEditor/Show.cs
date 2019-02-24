using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Data
{
    public class Show
    {
        public Element Transformation { get; set; }

        public Show(Element transformation)
        {
            Transformation = transformation;
        }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Show FromJSON(string json, Dictionary<string, Func<FormationData, Formation>> formationGenerators)
        {
            Show s = JsonConvert.DeserializeObject<Show>(json);
            SetFormations(s.Transformation, formationGenerators);
            return s;
        }

        private static void SetFormations(Element t, Dictionary<string, Func<FormationData, Formation>> formationGenerators)
        {
            if(formationGenerators.TryGetValue(t.StartFormation.FormationTypeIdentifier, out Func<FormationData, Formation> generator))
            {
                t.StartFormation = generator.Invoke(t.StartFormation.Data);
            }
            if(t.SubElements != null)
            {
                foreach (var sub in t.SubElements)
                {
                    SetFormations(sub.Transformation, formationGenerators);
                }
            }
        }
    }
}
