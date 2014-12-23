using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {
        GameGrid g;

        public Form1()
        {
            InitializeComponent();
        }

        private void playerGameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = new GameGrid(label_result);
            g.Location = new Point(10, 35);
            this.Controls.Add(g);
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.Dispose();
            label_result.Visible = false;
            g = new GameGrid(label_result);
            g.Location = new Point(10, 35);
            this.Controls.Add(g);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
