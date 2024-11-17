using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }



        bool PlayMode = false;
        int Conter = 0;


        public char[,] Array3X3 = new char[3, 3];

      
      

        private bool CheckRow(char PlayType, int RowIndex, int columnIndex)
        {
            int num = 1;

            for (int i = columnIndex+1; i < Array3X3.GetLength(1); i++)
            {
                if (Array3X3[RowIndex, i]!= PlayType)
                    break;
                num++;
            }


            for (int i = columnIndex - 1; i >= 0; i--)
            {
                if (Array3X3[RowIndex, i] != PlayType)
                    break;
                num++;
            }

            return (num >= 3);
        }



        private bool CheckColumn(char PlayType, int RowIndex, int columnIndex)
        {

            int num = 1;

            for (int i = RowIndex+1; i < Array3X3.GetLength(0); i++)
            {
                if (Array3X3[i, columnIndex]!= PlayType)
                    break;
                num++;
            }


            for (int i = RowIndex-1; i >= 0; i--)
            {
                if (Array3X3[i, columnIndex] != PlayType)
                    break;
                num++;
            }


            return (num >= 3);
        }



        private bool CheckPolar(char PlayType, int RowIndex, int columnIndex)
        {
            int num = 1;

            
            for (int i = RowIndex + 1, j = columnIndex + 1; i < Array3X3.GetLength(0) && j < Array3X3.GetLength(1); i++, j++)
            {
                if (Array3X3[i, j] != PlayType)
                    break;
                num++;
            }

        
            for (int i = RowIndex - 1, j = columnIndex - 1; i >= 0 && j >= 0; i--, j--)
            {
                if (Array3X3[i, j] != PlayType)
                    break;
                num++;
            }

            return (num >= 3);
        }


        private bool CheckReversePolar(char PlayType, int RowIndex, int columnIndex)
        {
            int num = 1;

            
            for (int i = RowIndex + 1, j = columnIndex - 1; i < Array3X3.GetLength(0) && j >= 0; i++, j--)
            {
                if (Array3X3[i, j] != PlayType)
                    break;
                num++;
            }

           
            for (int i = RowIndex - 1, j = columnIndex + 1; i >= 0 && j < Array3X3.GetLength(1); i--, j++)
            {
                if (Array3X3[i, j] != PlayType)
                    break;
                num++;
            }

            return (num >= 3);
        }



        private bool IsEmpty()
        {
            Conter++;
            return (Conter == Array3X3.Length);
        }


        private bool IsWinner(char PlayType, int RowIndex, int columnIndex)
        {
            return (CheckColumn(PlayType, RowIndex, columnIndex) || CheckRow(PlayType, RowIndex, columnIndex) 
                || CheckPolar(PlayType, RowIndex, columnIndex) || CheckReversePolar(PlayType, RowIndex, columnIndex));
        }


        private void GameEnd(char PlayType, int RowIndex, int columnIndex)
        {
            if (IsWinner(PlayType, RowIndex, columnIndex))
            {
                if(PlayType=='O')
                {
                    label4.Text = "Player 1";
                    MessageBox.Show("Player 1 wins!"); 
                }
                else
                {
                    label4.Text = "Player 2";
                    MessageBox.Show("Player 2 wins!");
                }
                
                this.Close();
            }
            else if (IsEmpty())
            {
                MessageBox.Show("It's a draw!");
                this.Close();
            }
        }


        private int GetRowIndex(Button butt)
        {
            string str = Convert.ToString(butt.Tag);
            if (str.Length > 0 && char.IsDigit(str[0]))
            {
                return str[0] - '0';
            }
            else
            {
                throw new FormatException("Tag does not contain a valid first digit.");
            }
        }


        private int GetColumnIndex(Button butt)
        {
            string str = Convert.ToString(butt.Tag);
            if (str.Length > 1 && char.IsDigit(str[1]))
            {
                return str[1] - '0';
            }
            else
            {
                throw new FormatException("Tag does not contain a valid second digit.");
            }
        }



        private System.Drawing.Image GetImage()
        {

            if (PlayMode)
            {
                PlayMode = false;
                return Properties.Resources.X_com;
            }
            else
            {
                PlayMode = true;
                return Properties.Resources.O_com;
            }
        }

        private char GetPlayType()
        {
            if (PlayMode)
             return 'X'; 

            else 
             return 'O'; 
        }
      
        private void  UpdatePlayTypeInArray(int row,int col,char PlayType)
        {
            Array3X3[row, col] = PlayType;
        }


        private void  UpdateTrun(char PlayType)
        {
          if(PlayType == 'X')
           {
                label4.Text = "Player 1";
           }

          else
           {
                label4.Text = "Player 2";
           }

        }


        private void play(Button butt)
        {
            char PlayType = GetPlayType();
            UpdateTrun(PlayType);
            int row = GetRowIndex(butt);
            int col = GetColumnIndex(butt);
            UpdatePlayTypeInArray(row ,col ,PlayType);
            butt.BackgroundImage = GetImage();
            butt.Enabled = false;
            GameEnd(PlayType, row, col);

        }


      

        private void Form2_Load(object sender, EventArgs e)
        { 
            
        }


        private void button_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                play(button);
            }
        }

    }
}
