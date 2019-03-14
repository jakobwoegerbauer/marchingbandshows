using ShowEditor.Data;
using ShowEditor.Simulator.Templates;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowEditor.WinFormsPlayer.ViewModels
{
    class ElementNode : TreeNode
    {
        private Element _element;

        public Element Element
        {
            get
            {
                return _element;
            }
            set
            {
                _element = value;
                Text = _element.Name;
            }
        }

        public int StartTime { get; }
        public int GlobalStartTime { get; }
        private readonly int[] reversePositionMapping;

        public ElementNode(Element element, int[] reversePositionMapping, int startTime = 0, int globalStartTime = 0)
        {
            Element = element;
            StartTime = startTime;
            GlobalStartTime = globalStartTime;

            this.reversePositionMapping = new int[reversePositionMapping.Length];
            Array.Copy(reversePositionMapping, this.reversePositionMapping, reversePositionMapping.Length);

            Nodes.Clear();
            if (Element.SubElements != null)
            {
                foreach (var s in Element.SubElements.OrderBy(s => s.StartTime))
                {
                    int[] mapping = new int[s.Element.StartFormation.Size];
                    for (int i = 0; i < s.Element.StartFormation.Size; i++)
                    {
                        mapping[i] = -1;
                    }

                    int[] subMapping = s.PositionMapping ?? Combination.Range(0, element.StartFormation.Size - 1);
                    for (int i = 0; i < subMapping.Length; i++)
                    {
                        if (subMapping[i] != -1)
                        {
                            mapping[subMapping[i]] = i;
                        }
                    }

                    Nodes.Add(new ElementNode(s.Element, mapping, s.StartTime, GlobalStartTime + s.StartTime));
                }
            }
        }

        public void UpdateVisibility(int localTime)
        {
            if (localTime >= 0 && localTime < Element.Duration)
            {
                NodeFont = new Font(SystemFonts.DefaultFont, FontStyle.Italic);
                ForeColor = Color.Green;
                EnsureVisible();
                Expand();
            }
            else
            {
                NodeFont = new Font(SystemFonts.DefaultFont, FontStyle.Regular);
                ForeColor = Color.Black;
                Collapse();
            }
            foreach (ElementNode n in Nodes)
            {
                n.UpdateVisibility(localTime - n.StartTime);
            }
        }

        public int GetGlobalPositionIndex(int position)
        {
            return reversePositionMapping[position];
        }
    }
}
