using ShowEditor.Data;
using ShowEditor.Simulator;
using ShowEditor.Simulator.Templates;
using ShowEditor.WinFormsPlayer.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowEditor.WinFormsPlayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private ShowSimulator simulator;
        bool first = true;

        private void Form1_Load(object sender, EventArgs e)
        {
            var elements = new BasicElements(1);
            var formation = new RowsFormation(12, 5);

            Show baseProgram = new Show(Combination.Concatenate("Pflichtprogramm",
                elements.MoveForward("Vorwärts", formation, 24),
                elements.Wait("Halt im klingenden Spiel", formation, 8),
                elements.MoveForward("Vorwärts", formation, 16),
                elements.Schwenkung("Schwenkung", formation, toRight: true),
                elements.MoveForward("Vorwärts", formation, 8),
                //TODO add abfallen/aufmarschieren
                elements.GrosseWendeComplete("Große Wende", formation)));


            /*Show show = new Show(Combination.Concatenate("Show",
                Combination.Parallel("",
                    trans.MoveForward("v", formation, 8),
                    trans.BreiteFormation("wide", formation)),
                trans.Schwenkung("Schwenkung", formation, toRight: false),
                trans.Schwenkung("Schwenkung", formation, toRight: true),
                trans.BreiteFormation("wide", formation, sideMarginFactor: 0.5),
                trans.Schwenkung("Schwenkung 2", formation, toRight: true),
                trans.Schwenkung("Schwenkung 2", formation, toRight: true),
                trans.Schwenkung("Schwenkung 2", formation, toRight: true),
                trans.Schwenkung("Schwenkung 2", formation, toRight: true),
                trans.Schwenkung("Schwenkung 2", formation, toRight: true),
                trans.Schwenkung("Schwenkung 2", formation, toRight: true),
                trans.Schwenkung("Schwenkung 2", formation, toRight: true),
                trans.Schwenkung("Schwenkung 2", formation, toRight: true),
                trans.Schwenkung("Schwenkung 2", formation, toRight: true),
                trans.Schwenkung("Schwenkung 3", formation, toRight: true)
                ));*/
            Show show = new Show(elements.GrosseWendeComplete("Große Wende", formation));

            Show rotTest = new Show(Combination.Concatenate("show",
                new Element
                {
                    Name = "sfd",
                    StartFormation = formation,
                    GroupActions = new GroupAction[]
                    {
                        GroupActions.MoveForward(4),
                        GroupActions.Rotate(90, delay: 4, duration: 2),
                        GroupActions.MoveForward(4, delay: 6),
                        GroupActions.Rotate(90, delay: 10, duration: 2),
                        GroupActions.MoveForward(4, delay: 12),
                        GroupActions.Rotate(90, delay: 16, duration: 2),
                        GroupActions.MoveForward(4, delay: 18),
                        GroupActions.Rotate(90, delay: 22, duration: 2),
                    }
                },
                new Element
                {
                    Name = "sfd",
                    StartFormation = formation,
                    GroupActions = new GroupAction[]
                    {
                        GroupActions.MoveForward(4),
                        GroupActions.Rotate(-90, delay: 4, duration: 2),
                        GroupActions.MoveForward(4, delay: 6),
                        GroupActions.Rotate(-90, delay: 10, duration: 2),
                        GroupActions.MoveForward(4, delay: 12),
                        GroupActions.Rotate(-90, delay: 16, duration: 2),
                        GroupActions.MoveForward(4, delay: 18),
                        GroupActions.Rotate(-90, delay: 22, duration: 2),
                    }
                }
            ));
            var x = baseProgram.ToJSON();

            var formationTypes = new List<Formation>
            {
                new BasicFormation(),
                new RowsFormation()
            };

            Show s = Data.Show.FromJSON(x, formationTypes);
            simulator = new ShowSimulator(s);
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            if (first)
            {
                simulator.Initialize();
                graphics = panel.CreateGraphics();
                lblStep.Text = "Step " + simulator.Time;
                var pos = simulator.GetPositions();
                players = new Player[pos.Length];
                for (int i = 0; i < players.Length; i++)
                {
                    players[i] = new Player(i, pos[i]);
                }
                Draw();

                _root = new ElementNode(simulator.Show.Element, Combination.Range(0, players.Length-1), Combination.Range(0, players.Length-1));
                tvElements.Nodes.Add(_root);
                tvElements.Refresh();

                first = false;
                return;
            }
            GoToStep(simulator.Time + 1);
        }

        private void Draw()
        {          
            using (Pen p = new Pen(Color.Black))
            {
                graphics.Clear(Color.White);
            }
            var positions = simulator.GetPositions();
            for (int i = 0; i < positions.Length; i++)
            {
                players[i].Position = positions[i];
                players[i].Draw(graphics);
            }
        }

        private ElementNode _root;
        private Player[] players;
        private Graphics graphics;

        private void tvElements_AfterSelect(object sender, TreeViewEventArgs e)
        {
            OnElementSelected(e.Node as ElementNode);
        }

        private void lvActions_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            OnActionSelected(e.IsSelected ? e.Item as GroupActionItem : null);
        }

        private void OnElementSelected(ElementNode e)
        {
            lvActions.Items.Clear();
            if (e?.Element.GroupActions != null)
            {
                foreach (var action in e.Element.GroupActions.OrderBy(a => a.Delay))
                {
                    lvActions.Items.Add(new GroupActionItem(action, e));
                }
            }
        }

        private void OnActionSelected(GroupActionItem item)
        {
            dgAction.Rows.Clear();
            if (item != null)
            {
                item.ShowParameters(dgAction);
                foreach(var p in players)
                {
                    p.IsSelected = false;
                }

                var pos = item.Action.Positions;
                if(pos == null)
                {
                    pos = Combination.Range(0, item.ElementNode.Element.StartFormation.Size - 1);
                }
                foreach(var p in pos)
                {
                    players[item.GetGlobalPositionIndex(p)].IsSelected = true;
                }
            }
            dgAction.Refresh();
            Draw();
        }

        private void panel_Click(object sender, EventArgs e)
        {
            List<int> selected = new List<int>();
            foreach(var p in players)
            {
                var point = panel.PointToClient(Cursor.Position);
                p.IsSelected = p.IsHovered(point.X, point.Y);
                if (p.IsSelected)
                    selected.Add(p.Index);
            }
            lvActions.Items.Clear();
            lvActions.Items.AddRange(_root.CollectActions(selected).ToArray());
            Draw();
        }

        private void tvElements_DoubleClick(object sender, EventArgs e)
        {
            if (tvElements.SelectedNode != null)
                GoToStep((tvElements.SelectedNode as ElementNode).GlobalStartTime);
        }

        public void GoToStep(int time)
        {
            simulator.GoToStep(time);
            lblStep.Text = "Step " + simulator.Time;
            _root.UpdateVisibility(simulator.Time);
            tvElements.Refresh();
            foreach (GroupActionItem actionItem in lvActions.Items)
            {
                actionItem.UpdateVisibility(simulator.Time);
            }
            Draw();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            GoToStep(simulator.Time - 1);
        }
    }
}
