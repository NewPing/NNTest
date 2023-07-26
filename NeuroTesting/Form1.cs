using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Tensorflow.Keras;

namespace NeuroTesting
{
    public partial class Form1 : Form
    {
        private readonly float a = 1.01f;
        private List<Player> players = new List<Player>();

        public Form1()
        {
            InitializeComponent();
            initPlayers(30);
        }

        private void initPlayers(int numberOfPlayers)
        {
            var rand = new Random();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                for (int j = 0; j < 20 && j < numberOfPlayers; j++, i++)
                {
                    players.Add(new Player(this, rand, 10 + (j * 50)));
                }
            }
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            for (int i = 0; i < players.Count; i++)
            {
                var rand = new Random();
                players[i].isHooking = rand.Next(100) > 50;
                doPlayerMove(players[i]);
            }
        }

        private void doPlayerMove(Player _player)
        {
            if (_player.isHooking)
            {
                _player.v += a;
            }
            else
            {
                _player.v += -a;
            }

            _player.pnlPlayer.Location = new System.Drawing.Point(_player.pnlPlayer.Location.X, _player.pnlPlayer.Location.Y + (int)_player.v);

            if (checkCollision(_player))
            {
                _player.pnlPlayer.Location = new System.Drawing.Point(_player.pnlPlayer.Location.X, getValidY(_player));
                _player.v = 0;
                return;
            }
        }

        private bool checkCollision(Player _player)
        {
            if (_player.v > 0)
            {
                if (_player.pnlPlayer.Location.Y + _player.pnlPlayer.Size.Height > pnlBottom.Location.Y)
                {
                    return true;
                }
            }
            else
            {
                if (_player.pnlPlayer.Location.Y < pnlTop.Location.Y + pnlTop.Size.Height)
                {
                    return true;
                }
            }
            return false;
        }

        private int getValidY(Player _player)
        {
            if (_player.pnlPlayer.Location.Y + _player.pnlPlayer.Size.Height > pnlBottom.Location.Y)
            {
                return pnlBottom.Location.Y - _player.pnlPlayer.Size.Height;
            }
            else
            {
                return pnlTop.Location.Y + pnlTop.Size.Height;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].isHooking = false;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].isHooking = true;
            }
        }
    }

    class Player
    {
        public Panel pnlPlayer = new Panel();
        public bool isHooking = false;
        public float v = 0;

        public Player(Form form, Random rand, int posX)
        {
            pnlPlayer.BackColor = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
            pnlPlayer.Location = new Point(posX, 203);
            pnlPlayer.Name = "pnlPlayer";
            pnlPlayer.Size = new Size(45, 45);
            pnlPlayer.TabIndex = 0;
            form.Controls.Add(pnlPlayer);
        }
    }
}