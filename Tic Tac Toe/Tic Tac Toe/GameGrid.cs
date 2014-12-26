using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Tic_Tac_Toe
{
    class GameGrid:Panel
    {

        //private Color brushColor; later for custom grid paint

        Bitmap CurrentTurn;
        PictureBox[] Zones = new PictureBox[9];
        Boolean[] IsZoneTaken = new Boolean[9];
        Label result;

        //constructor
        public GameGrid(Label l_result)
        {
            this.Paint += new PaintEventHandler(DrawGrid);

            result = l_result;
            BackColor = SystemColors.Control;
            this.Size = new Size(500, 500);
            SetZones();

        }


        private void DrawGrid(object sender, PaintEventArgs e)
        {
            var p = sender as Panel;
            var g = e.Graphics;

            //Create a brush
            SolidBrush brush = new SolidBrush(Color.Black);

            //Draw the game grid
            g.FillRectangle(brush, 175, 25, 10, 450);
            g.FillRectangle(brush, 325, 25, 10, 450);
            g.FillRectangle(brush, 25, 175, 450, 10);
            g.FillRectangle(brush, 25, 325, 450, 10);

        }



        private void SetZones()
        {
            
            //Set First Turn to be X
            CurrentTurn = Tic_Tac_Toe.Properties.Resources.X;

            //Create and set each Zone's size, name declare their event handlers
            for (int i = 0; i < 9;i++ )
            {
                Zones[i] = new PictureBox();
                Zones[i].Size = new Size(125, 125);
                Zones[i].Name = "z" + i;
                
                IsZoneTaken[i] = new Boolean();
                IsZoneTaken[i] = false;

                Zones[i].MouseEnter += new EventHandler(ZoneMouseEnter);
                Zones[i].MouseLeave += new EventHandler(ZoneMouseLeave);
                Zones[i].MouseClick += new MouseEventHandler(ZoneMouseClick);
            }

           

            //Set Zone location individually
            Zones[0].Location = new Point(35, 35);
            Zones[1].Location = new Point(195, 35);
            Zones[2].Location = new Point(345, 35);
            Zones[3].Location = new Point(35, 195);
            Zones[4].Location = new Point(195, 195);
            Zones[5].Location = new Point(345, 195);
            Zones[6].Location = new Point(35, 345);
            Zones[7].Location = new Point(195, 345);
            Zones[8].Location = new Point(340, 340);
           
            //Add zones to game panel
            foreach (PictureBox p in Zones)
            {
                this.Controls.Add(p);
            }
        }


        private void ZoneMouseEnter(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;

            //Checks if there's already a symbol in that specific zone, if there isnt, draws the current player's symbol
            if (IsZoneTaken[Convert.ToInt32(p.Name.Substring(1,1))])
            {
            }
            else
            {
                p.Image = CurrentTurn;
            }
            
        }

        private void ZoneMouseLeave(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;

            //If there was not a symbol inside the zone on mouse leave remove the image
            if (IsZoneTaken[Convert.ToInt32(p.Name.Substring(1, 1))])
            {
            }
            else
            {
                p.Image = null;
            }

        }

        private void ZoneMouseClick(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;

            /*Checks if there's already a symbol in that specific zone, if there isnt, draws the current
             player's symbol permanently, sets the zone to be taken and switches the player */
            if (!IsZoneTaken[Convert.ToInt32(p.Name.Substring(1, 1))])
            {
                p.Image = CurrentTurn;
                
                //check if current turn was X or Y and sets the tag accordingly
                if (CurrentTurn.GetPixel(62, 62) == Tic_Tac_Toe.Properties.Resources.X.GetPixel(62, 62))
                {
                    p.Tag = "X";
                }
                else
                {
                    p.Tag = "Y";
                }
                
                //Check for victory on each successful symbol add and play sound
                PlayClickSound();
                CheckForVictory();
                IsZoneTaken[Convert.ToInt32(p.Name.Substring(1, 1))] = true;
                SwitchTurn();
            }
            else
            {
            }
            

        }

        

        //Switches X and O
        private void SwitchTurn()
        {
            if (CurrentTurn.GetPixel(62,62) == Tic_Tac_Toe.Properties.Resources.X.GetPixel(62,62))
            {
                  CurrentTurn = new Bitmap(Tic_Tac_Toe.Properties.Resources.O);
            }
            else
            {
                CurrentTurn = Tic_Tac_Toe.Properties.Resources.X;
            }
        }


        private void CheckForVictory()
        {

            //tie check - it does nothing for now, TODO later
            if (IsZoneTaken[0] == true && IsZoneTaken[1] == true && IsZoneTaken[2] == true && IsZoneTaken[3] == true && IsZoneTaken[4] == true && IsZoneTaken[5] == true && IsZoneTaken[6] == true && IsZoneTaken[7] == true && IsZoneTaken[8] == true)
            {
                result.Visible = true; // doesn't work
                result.Text = "It's a Tie! Click Game->Restart to go again."; //doesn't work
            }


            //linear check
            if (Zones[0].Tag == Zones[1].Tag && Zones[1].Tag != null)
            {
                if (Zones[1].Tag == Zones[2].Tag)
                {
                    EndGame();
                }
            }

            if (Zones[3].Tag == Zones[4].Tag && Zones[4].Tag != null)
            {
                if (Zones[4].Tag == Zones[5].Tag)
                {
                    EndGame();
                }
            }

            if (Zones[6].Tag == Zones[7].Tag && Zones[7].Tag != null)
            {
                if (Zones[7].Tag == Zones[8].Tag)
                {
                    EndGame();
                }
            }


            //vertical check
            if (Zones[0].Tag == Zones[3].Tag && Zones[3].Tag != null)
            {
                if (Zones[3].Tag == Zones[6].Tag)
                {
                    EndGame();
                }
            }

            if (Zones[1].Tag == Zones[4].Tag && Zones[4].Tag != null)
            {
                if (Zones[4].Tag == Zones[7].Tag)
                {
                    EndGame();
                }
            }

            if (Zones[2].Tag == Zones[5].Tag && Zones[5].Tag != null)
            {
                if (Zones[5].Tag == Zones[8].Tag)
                {
                    EndGame();
                }
            }

            

            //diagonal check
            if (Zones[0].Tag == Zones[4].Tag && Zones[4].Tag != null)
            {
                if (Zones[4].Tag == Zones[8].Tag)
                {
                    EndGame();
                }
            }

            if (Zones[6].Tag == Zones[4].Tag && Zones[4].Tag != null)
            {
                if (Zones[4].Tag == Zones[2].Tag)
                {
                    EndGame();
                }
            }

        }

        private void PlayVictorySound()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Tic_Tac_Toe.Properties.Resources.victory);
            player.Play();
        }

        private void PlayClickSound()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Tic_Tac_Toe.Properties.Resources.click);
            player.Play();
        }

        private void EndGame()
        {
            if (CurrentTurn.GetPixel(62, 62) == Tic_Tac_Toe.Properties.Resources.X.GetPixel(62, 62))
            {
                result.Text = "X Wins! Click Game->Restart to go again.";
            }
            else
            {
                result.Text = "O Wins! Click Game->Restart to go again.";
            }
            result.Visible = true;
            PlayVictorySound();
        }
        
    }
}
