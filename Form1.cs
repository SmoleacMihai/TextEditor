using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1();
            newForm.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutWindow form = new AboutWindow();
            form.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            saveAsToolStripMenuItem_Click(sender, e);
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FontDialog dialog = new FontDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SelectionFont = dialog.Font;
                }
            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            richTextBox1.SelectionColor = colorDialog1.Color;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            String s = richTextBox1.SelectedText;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.SelectedText = s;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            String s = richTextBox1.SelectedText;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Italic);
            richTextBox1.SelectedText = s;
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            String s = richTextBox1.SelectedText;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Underline);
            richTextBox1.SelectedText = s;
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindWindow newFind = new FindWindow();
            newFind.Show();
        }


        private void textToReplace_Click(object sender, EventArgs e)
        {
            ReplaceAll(richTextBox1, findText.Text, replaceText.Text);
        }

        private void findText_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(findText.Text);
            MatchCollection matches = regex.Matches(richTextBox1.Text);
            richTextBox1.SelectAll();
            richTextBox1.SelectionBackColor = richTextBox1.BackColor;
            foreach (Match match in matches)
            {
                richTextBox1.Select(match.Index, match.Length);
                richTextBox1.SelectionBackColor = Color.Yellow;
            }
        }

        public void ReplaceAll(RichTextBox myRtb, string word, string replacement)
        {
            int i = 0;
            int n = 0;
            int a = replacement.Length - word.Length;
            foreach (Match m in Regex.Matches(myRtb.Text, word))
            {
                myRtb.Select(m.Index + i, word.Length);
                i += a;
                myRtb.SelectedText = replacement;
                n++;
            }
            MessageBox.Show("Replaced " + n + " matches!");
        }
    }
}
