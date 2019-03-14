using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Data
{
    /// <summary>
    /// A marching band show
    /// </summary>
    public class Show
    {
        /// <summary>
        /// The main element.
        /// </summary>
        public Element Element { get; set; }

        public Show(Element element)
        {
            Element = element;
        }

        /// <summary>
        /// Converts the show to a JSON string validating against the schema 
        /// specified in the file show.schema.json in the root directory of this solution.
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Creates a Show instance from a JSON string
        /// </summary>
        /// <param name="json">valid JSON string (show.schema.json)</param>
        /// <param name="formationTypes">All available formation types</param>
        /// <returns></returns>
        public static Show FromJSON(string json, List<Formation> formationTypes)
        {
            Show s = JsonConvert.DeserializeObject<Show>(json);
            SetFormations(s.Element, formationTypes);
            return s;
        }

        /// <summary>
        /// Sets 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="formationGenerators"></param>
        private static void SetFormations(Element t, List<Formation> formationGenerators)
        {
            var formation = formationGenerators.Single(f => f.FormationTypeIdentifier == t.StartFormation.FormationTypeIdentifier);
            t.StartFormation = formation.FromData(t.StartFormation.Data);
            
            if(t.SubElements != null)
            {
                foreach (var sub in t.SubElements)
                {
                    SetFormations(sub.Element, formationGenerators);
                }
            }
        }
    }
}
