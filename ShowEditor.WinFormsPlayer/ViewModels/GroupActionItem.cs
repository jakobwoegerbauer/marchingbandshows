using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShowEditor.Data;

namespace ShowEditor.WinFormsPlayer.ViewModels
{
    class GroupActionItem : ListViewItem
    {
        public GroupAction Action { get;}
        public ElementNode ElementNode { get; }

        public GroupActionItem(GroupAction action, ElementNode elementNode)
        {
            Action = action;
            ElementNode = elementNode;
            Text = $"{Action.ActionType} ({ElementNode.GlobalStartTime + Action.Delay} - {ElementNode.GlobalStartTime + Action.Delay + Action.Duration})";
        }

        public void ShowParameters(DataGridView dgv)
        {
            CreateRow("ActionType", Action.ActionType, dgv, true);
            CreateRow("Delay", Action.Delay, dgv, true);
            CreateRow("Duration", Action.Duration, dgv, true);
            CreateRow("Priority", Action.Priority, dgv, true);
            if(Action.Parameters != null)
            {
                foreach (var kv in Action.Parameters)
                {
                    CreateRow(kv.Key, kv.Value, dgv);
                }
            }
        }

        private DataGridViewRow CreateRow(string paramName, object value, DataGridView dgv, bool fixedName = false)
        {
            var rowIndex = dgv.Rows.AddCopy(0);
            var row = dgv.Rows[rowIndex];
            row.Cells[0].Value = paramName;
            if (fixedName)
            {
                row.Cells[0].ReadOnly = fixedName;
                row.Cells[0].Style.BackColor = Color.LightGray;
            }
            else
            {
                row.Cells[0].Style.BackColor = Color.White;
            }
            
            row.Cells[1].Value = value;
            return row;
        }

        public void UpdateVisibility(int globalTime)
        {
            if(globalTime >= ElementNode.GlobalStartTime + Action.Delay  && globalTime < ElementNode.GlobalStartTime + Action.Delay + Action.Duration)
            {
                ForeColor = Color.Green;
            }
            else
            {
                ForeColor = Color.Black;
            }
        }

        public int GetGlobalPositionIndex(int position)
        {
            return ElementNode.GetGlobalPositionIndex(position);
        }
    }
}
