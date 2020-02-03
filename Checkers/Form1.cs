using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BoardGames.Generic;
using BoardGames.Checkers;
using System.Diagnostics;

namespace BoardGames
{
    public partial class FormMain : Form
    {
        CheckersBoard board;
        Queue<string> moves;

        public FormMain()
        {
            InitializeComponent();
            board = new CheckersBoard();
            tb_Main.Font = new Font(FontFamily.GenericMonospace, tb_Main.Font.Size+8);
            //PrintBoard(board);
            DrawBoard();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(TbEnd.Text) && !string.IsNullOrWhiteSpace(TbStart.Text))
            {
                if (int.TryParse(TbStart.Text, out int start) && int.TryParse(TbEnd.Text, out int end))
                {
                    board.MovePiece(start, end);
                }
            }
            PrintBoard(board);
        }

        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            if(ofdMain.ShowDialog() == DialogResult.OK)
            {
                moves = Engine.ParseMovesFile(ofdMain.FileName);
                BtnNextMove.Text = moves.Peek();
            }
        }

        void PrintBoard(CheckersBoard board)
        {
            tb_Main.Clear();
            tb_Main.Text += string.Format("{0,4}{1,2}{2,2}{3,2}{4,2}{5,2}{6,2}{7,2}", " XY0", 1, 2, 3, 4, 5, 6, 7) + Environment.NewLine;
            for (int row = 0; row < board.Grid.GetLength(0); row++)
            {
                tb_Main.Text += string.Format("{0,2}", row);
                for (int col = 0; col < board.Grid.GetLength(1); col++)
                {
                    CheckersTile tile = board.Grid[row, col];
                    if (tile.Piece == null)
                        tb_Main.Text += string.Format("{0,2}", "X");
                    else
                        if(tile.Piece.IsKing)
                            tb_Main.Text += (tile.Piece.Team ? string.Format("{0,2}", "w") : string.Format("{0,2}", "b"));
                        else
                            tb_Main.Text += (tile.Piece.Team ? string.Format("{0,2}", "W") : string.Format("{0,2}", "B"));
                }
                tb_Main.Text += Environment.NewLine;
            }
        }

        void DrawBoard()
        {
            const int tileSize = 40;
            var black = Color.Black;
            var white = Color.White;
                for (var i = 0; i < board.Grid.GetLength(0); i++)
                {
                    for (var j = 0; j < board.Grid.GetLength(1); j++)
                    {
                        // create new Panel control which will be one 
                        // chess board tile
                        var newPanel = new Panel
                        {
                            Size = new Size(tileSize, tileSize),
                            Location = new Point(tileSize * i, (tileSize * j) + 100)
                        };

                        // add to Form's Controls so that they show up
                        Controls.Add(newPanel);

                        newPanel.BackColor = board.Grid[i, j].Type ? white : black;
                    }
                }
        }

        private void BtnNextMove_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();

            if(moves == null)
            {
                moves = Engine.ParseMovesFile(@"C:\Users\Dave\Desktop\Temp\checkersMoves.txt");
                BtnNextMove.Text = moves.Peek();
            }
            var moveText = moves.Peek();
            if (moveText.Contains("-"))
            {
                var split = moveText.Split('-');
                var move = new Tuple<int, int>(int.Parse(split[0]), int.Parse(split[1]));
                sw.Start();
                var validMoves = board.GetValidMoves();
                sw.Stop();
                Console.WriteLine("Time caculating " + validMoves.Count + " moves: " + sw.ElapsedTicks + "ms");
                if (validMoves.Contains(move)) {
                    if (board.MovePiece(move.Item1, move.Item2))
                    {
                        PrintBoard(board);
                        moves.Dequeue();
                        BtnNextMove.Text = moves.Peek();
                    }
                }
            }

        }

        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics g = e.Graphics)
            {
                const int tileSize = 40;
                var size = new Size(tileSize, tileSize);
                
                var tileBlack = new Pen(Color.DarkGray);
                var tileWhite = new Pen(Color.White);
                var pieceBlack = new Pen(Color.Black);
                var pieceWhite = new Pen(Color.Red);

                for (var i = 0; i < board.Grid.GetLength(0); i++)
                {
                    for (var j = 0; j < board.Grid.GetLength(1); j++)
                    {
                        Point location = new Point(tileSize * i, (tileSize * j) + 100);
                        Rectangle rect = new Rectangle(location, size);
                        var tile = board.Grid[i, j];
                        var color = tile.Type ? tileWhite : tileBlack;
                        var piece = tile.Piece;

                        g.DrawEllipse(color, rect);
                        if (piece == null)
                            continue;
                        size = new Size(tileSize/2, tileSize/2);
                        color = piece.Team ? pieceWhite : pieceBlack;
                        rect = new Rectangle(location, size);
                        g.DrawEllipse(color, rect);
                        
                    }
                }
            }
        }
    }
}
