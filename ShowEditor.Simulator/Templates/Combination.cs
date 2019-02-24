using ShowEditor.Data;
using ShowEditor.Simulator.ActionExecutors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShowEditor.Simulator.Templates
{
    public class Combination
    {
        public static Element Create(string name, Formation startFormation, params GroupAction[] actions)
        {
            return new Element
            {
                Name = name,
                StartFormation = startFormation,
                GroupActions = actions
            };
        }
                
        public static Element Concatenate(string name, params Element[] transformations)
        {
            return Concatenate(name, null, transformations);
        }

        public static Element Concatenate(string name, int[] positions = null, params Element[] transformations)
        {
            if (transformations == null || transformations.Length == 0)
            {
                throw new ArgumentException("transformations can't be null or empty");
            }

            List<SubElement> subs = new List<SubElement>();
            int time = 0;
            foreach (Element t in transformations)
            {
                subs.Add(new SubElement
                {
                    StartTime = time,
                    Element = t
                });
                time += t.Duration;
            }

            return new Element
            {
                Name = name,
                StartFormation = transformations[0].StartFormation,
                SubElements = subs.ToArray()
            };
        }

        public static Element Parallel(string name, params Element[] transformations)
        {
            return Parallel(name, Range(0,transformations[0].StartFormation.Size-1), transformations);
        }

        public static Element Parallel(string name, int[] mapping = null, params Element[] transformations)
        {
            if (transformations == null || transformations.Length == 0)
            {
                throw new ArgumentException("transformations can't be null or empty");
            }

            return new Element
            {
                Name = name,
                StartFormation = transformations[0].StartFormation,
                SubElements = transformations.Select(t => new SubElement
                {
                    StartTime = 0,
                    Element = t
                }).ToArray()
            };
        }

        public static int[] Range(int from, int to)
        {
            int[] pos = new int[to-from+1];
            for(int i = from; i <= to; i++)
            {
                pos[i - from] = i;
            }
            return pos;
        }
    }
}
