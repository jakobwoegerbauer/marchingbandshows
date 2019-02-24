using ShowEditor.Data;
using ShowEditor.Simulator;
using ShowEditor.Simulator.Templates;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            //graphics = ActiveForm.CreateGraphics();

            var trans = new BasicTransformations(1);

            var formation = new RowsFormation(5, 5);
            Show show = new Show(Combination.Concatenate("Show",
                //trans.MoveForward("v", formation, 8),
                trans.Schwenkung("Schwenkung", formation, toRight: false),
                trans.Schwenkung("Schwenkung", formation, toRight: true),
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
                ));

            /*show = new Show(new Element
            {
                SubElements = new SubElement[]
                {
                    new SubElement
                    {
                        StartTime = 1,
                        Transformation = trans.Schwenkung("",formation, toRight: false),
                    },
                    new SubElement
                    {
                        StartTime = 9,
                        Transformation = trans.Schwenkung("",formation, toRight: true),
                    }
                },
                StartFormation = formation
            });*/

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
            var x = show.ToJSON();

            var generators = new Dictionary<string, Func<FormationData, Formation>>
            {
                { new BasicFormation().FormationTypeIdentifier, new BasicFormation().FromData },
                { new RowsFormation().FormationTypeIdentifier, new RowsFormation().FromData }
            };

            Show s = ShowEditor.Data.Show.FromJSON(x, generators);

            simulator = new ShowSimulator(show);
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            simulator.ExecuteStep();
            lblStep.Text = "Step " + simulator.Time;
            Draw();
            //panel.Refresh();
        }

        private void Draw()
        {
            int maxX = 500;
            int maxY = 500;
            var graphics = panel.CreateGraphics();
            float rad = 3f;
            float scale = 10;
            float mx = 200;
            float my = maxY / 2+100;

            using (Pen p = new Pen(Color.Black))
            {
                graphics.Clear(Color.White);
                graphics.DrawLine(p, 0, my, maxX, my);
                graphics.DrawLine(p, mx, 0, mx, maxY);

                var positions = simulator.GetPositions();
                for (int i = 0; i < positions.Length; i++)
                {
                    float cx = scale * (float)positions[i].X + mx;
                    float cy = scale * (float)positions[i].Y + my;
                    graphics.DrawEllipse(p, cx - rad, cy - rad, 2 * rad, 2 * rad);

                    float fx = (float)Math.Round(Math.Cos(PositionHelper.ToRadians(positions[i].Rotation)) * scale, 1);
                    float fy = (float)Math.Round(Math.Sin(PositionHelper.ToRadians(positions[i].Rotation)) * scale, 1);
                    graphics.DrawLine(p, cx, cy, cx + fx, cy + fy);
                }
            }
        }

        private void panel_Click(object sender, EventArgs e)
        {

        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            //Draw();
        }
    }
}
