using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapEditor.View
{
    public partial class MapSizeForm : Form
    {
        private static Keys[] acceptableKeys = { Keys.Back, Keys.Escape, Keys.Delete, Keys.Home, Keys.End };
        private bool okPressed;
        private Microsoft.Xna.Framework.Point mapSize;

        public MapSizeForm()
        {
            InitializeComponent();
            button1.Enabled = false;
        }

        private void MapSizeForm_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if(e.CloseReason == CloseReason.WindowsShutDown || okPressed)
                return;

            // Confirm user wants to close
            switch(MessageBox.Show(this, "Are you sure?", "Exit Without Setting Map Size", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                case DialogResult.Yes:
                    break;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonStatus();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonStatus();
        }
        private void UpdateButtonStatus()
        {
            if(textBox1.Text.Length < 1 || textBox2.Text.Length < 1)
                button1.Enabled = false;
            else
                button1.Enabled = true;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            ValidateKeyEvent(textBox1, e);
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            ValidateKeyEvent(textBox2, e);
        }
        private void ValidateKeyEvent(TextBox textBox, KeyEventArgs e)
        {
            KeyEventArgs keyEvent = e as KeyEventArgs;

            if(textBox.Text.Length > 3 && !acceptableKeys.Contains(keyEvent.KeyCode))
                keyEvent.SuppressKeyPress = true;

            if(!Char.IsDigit((char)keyEvent.KeyValue) && !acceptableKeys.Contains(keyEvent.KeyCode))
            {
                keyEvent.SuppressKeyPress = true;
            }
            if(keyEvent.KeyCode == Keys.Enter && button1.Enabled)
            {
                button1_Click(textBox, e);
            }
            if(keyEvent.KeyCode == Keys.Escape)
            {
                Close();
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length > 0 && textBox1.Text.Length > 0)
            {
                okPressed = true;
                mapSize = new Microsoft.Xna.Framework.Point(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        public Microsoft.Xna.Framework.Point MapSize
        {
            get { return mapSize; }
        }
    }
}
