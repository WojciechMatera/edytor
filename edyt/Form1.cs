using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace edyt
{
    public partial class Form1 : Form
    {
        private bool isCollapsed;
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
            //    button5.Image = Resources.Collapse_Arrow_20px;
                panelDropDown.Height += 10;
                if (panelDropDown.Size == panelDropDown.MaximumSize)
                {
                    timer1.Stop();
                    isCollapsed = false;
                }
            }
            else
            {
            //    button5.Image = Resources.Expand_Arrow_20px;
                panelDropDown.Height -= 10;
                if (panelDropDown.Size == panelDropDown.MinimumSize)
                {
                    timer1.Stop();
                    isCollapsed = true;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        MemoryStream userInput = new MemoryStream();
        private void button11_Click(object sender, EventArgs e)
        {
                // Create an OpenFileDialog to request a file to open.
                 OpenFileDialog openFile1 = new OpenFileDialog();

                 // Initialize the OpenFileDialog to look for RTF files.
                 openFile1.DefaultExt = "*.txt";
                 openFile1.Filter = "TXT Files|*.txt";

            // Determine whether the user selected a file from the OpenFileDialog.
            if (openFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
               openFile1.FileName.Length > 0)
            {
                // Load the contents of the file into the RichTextBox.
              //  richTextBox1.LoadFile(openFile1.FileName);


                // Display the entire contents of the stream,
                // by setting its position to 0, to RichTextBox2.
                userInput.Position = 0;
                
                richTextBox1.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText);
                Find(richTextBox1, "html", Color.Blue);
                Find(richTextBox1, "head", Color.Red);
                Find(richTextBox1, "body", Color.Green);
                Find(richTextBox1, "name", Color.Silver);

            }
        }
        private void AddFontStyle(FontStyle style)
        {
            int start = richTextBox1.SelectionStart;
            int len = richTextBox1.SelectionLength;
            System.Drawing.Font currentFont;
            FontStyle fs;
            for (int i = 0; i < len; ++i)
            {
                richTextBox1.Select(start + i, 1);
                currentFont = richTextBox1.SelectionFont;
                fs = currentFont.Style;
                //add style
                fs = fs | style;
                richTextBox1.SelectionFont = new Font(
                  currentFont.FontFamily,
                  currentFont.Size,
                  fs
                  );
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            // Index
            int index = 0;

            // Loop over each line
            foreach (string line in richTextBox1.Lines)
            {
                // Ignore the non-assembly lines
                if (line.Substring(0, 2) != ";;")
                {
                    // Start position
                    int start = (richTextBox1.GetFirstCharIndexFromLine(index) + line.LastIndexOf(";") + 1);
                 

                    // Length
                    int length = line.Substring(line.LastIndexOf(";"), (line.Length - (line.LastIndexOf(";")))).Length;

                    System.Diagnostics.Debug.WriteLine(start + " " + length);
                  //  int length = richTextBox1.SelectionLength                   
                  // Make the selection
                    richTextBox1.SelectionStart = start;
                    richTextBox1.SelectionLength = length;

                    // Change the colour
                    richTextBox1.SelectionColor = Color.Red;

                }

                // Increase index
                index++;
            }
        }
        public static void Find(RichTextBox rtb, String word, Color color)
        {
            if (word == "")
            {
                return;
            }
            int s_start = rtb.SelectionStart, startIndex = 0, index;
            System.Diagnostics.Debug.WriteLine(s_start); 
            while ((index = rtb.Text.IndexOf(word, startIndex)) != -1)
            {
                rtb.Select(index, word.Length);
                rtb.SelectionColor = color;
                startIndex = index + word.Length;
            }
       /*     rtb.SelectionStart = 0;
            rtb.SelectionLength = rtb.TextLength;
            rtb.SelectionColor = Color.Black;*/
        }
        private void button7_Click(object sender, EventArgs e)
        {
            AddFontStyle(FontStyle.Bold);
        }
    }
}
