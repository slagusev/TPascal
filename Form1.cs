using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.Text.RegularExpressions;
using System.IO;


/* 
 * wroten by : 
 * mohamed aziz knani : medazizknani[at]gmail.com
 * code from FastColoredTextbox
*/
namespace TPascal
{
    public partial class Form1 : Form
    {
        // got this list of reserved words from http://wiki.freepascal.org/Reserved_words
        string[] reswords = {"absolute", "and", "array", "asm", "begin", "case", "const", "constructor", "destructor", "div", "do", "downto", "else", "end", "file", "for", "function", "goto", "if", "implementation", "in", "inherited", "inline", "interface", "label", "mod", "nil", "not", "object", "of", "operator", "or", "packed", "procedure", "program", "record", "reintroduce", "repeat", "self", "set", "shl", "shr", "string", "then", "to", "type", "unit", "until", "uses", "var", "while", "with", "xor"  };
        TextStyle brownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Regular);

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.WindowState = FormWindowState.Maximized;
            
        }



        public static string Path1;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
                
                SaveFileDialog SFD = new SaveFileDialog();
                SFD.Filter = "Pascal source (*.pas)| *.pas";
                SFD.FileName = "";
                if (SFD.ShowDialog() == DialogResult.OK)
                {

                    fastColoredTextBox1.SaveToFile(SFD.FileName, Encoding.UTF8);
                    Path1 = SFD.FileName;
                }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coded By mohamed aziz knani");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (Path1 == null)
            {
                Random rnd = new Random();
                int rndfilename = rnd.Next(1, 1000);
                Path1 = Path.GetTempPath() + rndfilename + ".pas";
                fastColoredTextBox1.SaveToFile(Path1, Encoding.UTF8);
            }
            string strCmdText = "/C fpc " + Path1;
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            string execut = Path1.Remove(Path1.Length - 3) + "exe";
            string run = execut;
            // zzz sleep for 2 seconds because compiling process take some time
            Thread.Sleep(2000);
            try
            {
                System.Diagnostics.Process.Start(run);
                toolStripStatusLabel1.Text = "compile OK !";
            }
            catch
            {
                toolStripStatusLabel1.Text = "there was an ERROR during compiling :(";
            }
        }



        private void fastColoredTextBox1_Load(object sender, EventArgs e)
        {

        }

        private void fastColoredTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            //clear previous highlighting
            e.ChangedRange.ClearStyle(brownStyle);
            //highlight tags
            e.ChangedRange.SetStyle(brownStyle, "<[^>]+>");
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (Path1 == null || Path1.Contains(Path.GetTempPath()) == true)
            {
                toolStripButton1.PerformClick();
            }
            else
            {
                fastColoredTextBox1.SaveToFile(Path1, Encoding.UTF8);
            }
        }


        }
    
}
