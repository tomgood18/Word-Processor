using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Assignment2
{
    public partial class Form2 : Form
    {
        public string userLabel { get; set; }
        public string userType;
        public bool isEdited;
        AboutBox1 aboutBox = new AboutBox1();
        List<User> list;

        public Form2(List<User> userList)
        {
            InitializeComponent();
            this.Text = "New Document";
            list = userList;
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            userToolStripLabel.Text = "User Name: " + userLabel;
            // Sets GUI constraints based on user type
            if (userType.Equals("View")) {
                textBox.ReadOnly = true;
                boldToolStripButton.Enabled = false;
                italicsToolStripButton.Enabled = false;
                underlineToolStripButton.Enabled = false;
                saveAsToolStripButton.Enabled = false;
                saveToolStripButton.Enabled = false;
                cutToolStripButton1.Enabled = false;
                copyToolStripButton1.Enabled = false;
                pasteToolStripButton1.Enabled = false;
                cutToolStripMenuItem1.Enabled = false;
                copyToolStripMenuItem1.Enabled = false;
                pasteToolStripMenuItem1.Enabled = false;
                fontSizeComboBox.Enabled = false;
            }
            else
            {
                textBox.ReadOnly = false;
            }
            // Sets font sizes for font selection
            setSize();
            textBox.Tag = textBox.Text;
        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox.SelectionFont != null)
            {
                boldToolStripButton.Checked = textBox.SelectionFont.Bold;
                italicsToolStripButton.Checked = textBox.SelectionFont.Italic;
                underlineToolStripButton.Checked = textBox.SelectionFont.Underline;
            }
            
        }
        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 firstForm = new Form1(list);
            this.Hide();
            firstForm.ShowDialog();
            this.Close();
        }

        private void BoldToolStripButton_Click(object sender, EventArgs e)
        {
            if (textBox.SelectionFont == null)
            {
                return;
            }
            FontStyle style = textBox.SelectionFont.Style;
            if (textBox.SelectionFont.Bold)
            {
                style &= ~FontStyle.Bold;
            }
            else
            {
                style |= FontStyle.Bold;

            }
            textBox.SelectionFont = new Font(textBox.SelectionFont, style);
        }
        private void ItalicsToolStripButton_Click(object sender, EventArgs e)
        {
            if (textBox.SelectionFont == null)
            {
                return;
            }
            FontStyle style = textBox.SelectionFont.Style;
            if (textBox.SelectionFont.Italic)
            {
                style &= ~FontStyle.Italic;
            }
            else
            {
                style |= FontStyle.Italic;
            }
            textBox.SelectionFont = new Font(textBox.SelectionFont, style);
        }

        private void UnderlineToolStripButton_Click(object sender, EventArgs e)
        {
            if (textBox.SelectionFont == null)
            {
                return;
            }
            FontStyle style = textBox.SelectionFont.Style;
            if (textBox.SelectionFont.Underline)
            {
                style &= ~FontStyle.Underline;
            }
            else
            {
                style |= FontStyle.Underline;
            }
            textBox.SelectionFont = new Font(textBox.SelectionFont, style);
        }

        private void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            open();
        }

        private void SaveAsToolStripButton_Click(object sender, EventArgs e)
        {
            saveAs();
        }
        private void saveAs()
        {
            SaveFileDialog saveAsDialog = new SaveFileDialog();
            saveAsDialog.Title = "Save As";
            saveAsDialog.Filter = "Rich Text Files (*.rtf) | *.rtf | All Files(*.*) | *.*";
            DialogResult dr = saveAsDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string filename = saveAsDialog.FileName;
                using (File.Create(filename));
                textBox.SaveFile(filename, RichTextBoxStreamType.RichText);
                this.Text = filename;

                textBox.Tag = textBox.Text;
            }
        }
        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            save();
        }
        private void save()
        {
            string filename = this.Text;
            if (filename != "New Document")
            {
                using (File.Create(filename));
                textBox.SaveFile(filename, RichTextBoxStreamType.RichText);
                textBox.Tag = textBox.Text;
            }
            else
            // If file hasn't been saved to a location before, open 'Save As' box.
            {
                saveAs();
            }
            textBox.Tag = textBox.Text;
        }

        private void open()
        {
            if (!textBox.Text.Equals(textBox.Tag))
            {
                getDialogResultOpen();
            }
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Open a text file";
            openDialog.Filter = "Text (*.txt)|*.txt|All (*.*)|*.*";
            DialogResult dr = openDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string filename = openDialog.FileName;
                try {
                    textBox.LoadFile(filename, RichTextBoxStreamType.RichText);
                } catch (ArgumentException f) {
                    textBox.LoadFile(filename, RichTextBoxStreamType.PlainText);
                }
                this.Text = filename;
                textBox.Tag = textBox.Text;
            }
        }
        private void CutToolStripButton1_Click(object sender, EventArgs e)
        {
            textBox.Cut();
        }

        private void PasteToolStripButton1_Click(object sender, EventArgs e)
        {
            textBox.Paste();
        }

        private void CopyToolStripButton1_Click(object sender, EventArgs e)
        {
            textBox.Copy();
        }

        private void CutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textBox.Cut();
        }

        private void CopyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textBox.Copy();
        }

        private void PasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textBox.Paste();
        }

        private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            aboutBox.Show();
        }

        private void OpenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            open();
        }

        private void SaveAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveAs();
        }

        private void SaveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            save();
        }

        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            newDoc();
        }

        private void NewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            newDoc();
        }

        private void newDoc()
        {
            string filename = this.Text;
            if (filename == "New Document")
            {
                if (textBox.Text != "")
                {
                    getDialogResultNew();
                }
            }
            else
            {
                //Checks if text format is different than saved file.
                string fileContents = File.ReadAllText(filename);
                string rtfContents = textBox.Rtf + "\0";
                if (fileContents.Equals(rtfContents)){
                    isEdited = false;
                }
                else
                {
                    isEdited = true;
                }
                if (textBox.Text.Equals(textBox.Tag) && isEdited == false)
                {
                    textBox.Text = "";
                    this.Text = "New Document";
                    textBox.Tag = textBox.Text;
                }
                else
                {
                    getDialogResultNew();
                }
            }
        }

        public void getDialogResultNew()
        {
            DialogResult result = MessageBox.Show("Do you want to save changes to " + this.Text + "?", "Alert", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                save();
                textBox.Text = "";
                this.Text = "New Document";
                textBox.Tag = textBox.Text;
            }
            else if (result == DialogResult.No)
            {
                textBox.Text = "";
                this.Text = "New Document";
                textBox.Tag = textBox.Text;
            }
        }

        public void getDialogResultOpen()
        {
            DialogResult result = MessageBox.Show("Do you want to save changes to " + this.Text + "?", "Alert", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                save();
            }
        }

        private void setSize()
        {
            fontSizeComboBox.SelectedItem = Font.Size.ToString();
            this.textBox.Font = new Font("Arial", 12);
            {
                // Sets font size
                this.fontSizeComboBox.Items.Add(8);
                this.fontSizeComboBox.Items.Add(9);
                this.fontSizeComboBox.Items.Add(10);
                this.fontSizeComboBox.Items.Add(11);
                this.fontSizeComboBox.Items.Add(12);
                this.fontSizeComboBox.Items.Add(14);
                this.fontSizeComboBox.Items.Add(16);
                this.fontSizeComboBox.Items.Add(18);
                this.fontSizeComboBox.Items.Add(20);
                this.fontSizeComboBox.Items.Add(22);
                this.fontSizeComboBox.Items.Add(24);
                this.fontSizeComboBox.Items.Add(26);
                this.fontSizeComboBox.Items.Add(28);
                this.fontSizeComboBox.Items.Add(36);
                this.fontSizeComboBox.Items.Add(48);
                this.fontSizeComboBox.Items.Add(72);
                this.fontSizeComboBox.SelectedItem = 12;
                this.fontSizeComboBox.SelectedIndexChanged += new EventHandler(FontSizeComboBox_SelectedIndexChanged);
            }
        }
        private void FontSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            {
                float fontSize;
                FontStyle styling = textBox.Font.Style;
                try
                {
                    if (float.TryParse(fontSizeComboBox.SelectedItem.ToString(), out fontSize))             // Iterates through all font selection possibilities
                    {
                        if (boldToolStripButton.Checked)
                        {
                            styling = FontStyle.Bold;
                        } 
                        else if (italicsToolStripButton.Checked)
                        {
                            styling = FontStyle.Italic;
                        }
                        else if (underlineToolStripButton.Checked)
                        {
                            styling = FontStyle.Underline;
                        }
                        if (boldToolStripButton.Checked && italicsToolStripButton.Checked)
                        {
                            styling = FontStyle.Bold | FontStyle.Italic;
                        }
                        else if (boldToolStripButton.Checked && underlineToolStripButton.Checked)
                        {
                            styling = FontStyle.Bold | FontStyle.Underline;
                        }
                        else if (italicsToolStripButton.Checked && underlineToolStripButton.Checked)
                        {
                            styling = FontStyle.Italic | FontStyle.Underline;
                        }
                        if (boldToolStripButton.Checked && italicsToolStripButton.Checked && underlineToolStripButton.Checked)
                        {
                            styling = FontStyle.Bold | FontStyle.Italic | FontStyle.Underline;
                        }
                        this.textBox.SelectionFont = new Font(textBox.Font.Name, fontSize, styling, GraphicsUnit.Point);
                    }
                } catch (NullReferenceException f)
                {
                    
                }
            }
        }
        private void HelpToolStripButton_Click_1(object sender, EventArgs e)
        {
            aboutBox.Show();
        }
    }
}






