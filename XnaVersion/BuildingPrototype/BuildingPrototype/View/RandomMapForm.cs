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
    public partial class RandomMapForm : Form
    {
        private static Keys[] acceptableKeys = { Keys.Back, Keys.Escape, Keys.Delete, Keys.Home, Keys.End };

        private Microsoft.Xna.Framework.Point mapSize;
        private float waterLevel;
        private float grassLevel;        
        private float shortMountianLevel;
        private float treeLevel;
        private int octaves;        
        private float frequency;
        private float persistence;
        private float zoom;
        private bool okPressed;        
        
        public RandomMapForm()
        {
            okPressed = false;            
            InitializeComponent();
            UpdateRandomOptions( );
        }        
        private void RandomMapForm_Load(object sender, EventArgs e)
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
        private void UpdateRandomOptions()
        {
            FrequencyBox.Enabled = RandomCheckBox.Checked;
            OctaveBox.Enabled = RandomCheckBox.Checked;
            PersistenceBox.Enabled = RandomCheckBox.Checked;
            ZoomBox.Enabled = RandomCheckBox.Checked;
            WaterLevelBox.Enabled = RandomCheckBox.Checked;
            GrassLevelBox.Enabled = RandomCheckBox.Checked;
            ShortMountianLevelBox.Enabled = RandomCheckBox.Checked;
            TreeLevelBox.Enabled = RandomCheckBox.Checked;
        }
        private void RandomCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateRandomOptions();

            UpdateButtonStatus();
        }
        private void XSizeBox_KeyDown(object sender, KeyEventArgs e)
        {
            ValidateIntegerKeyEvent(XSizeBox, e);
            UpdateButtonStatus();
        }
        private void YSizeBox_KeyDown(object sender, KeyEventArgs e)
        {
            ValidateIntegerKeyEvent(YSizeBox, e);
            UpdateButtonStatus();
        }
        private void WaterLevelBox_KeyDown(object sender, KeyEventArgs e)
        {
            ValidateFloatKeyEvent(WaterLevelBox, e);
            UpdateButtonStatus();
        }
        private void GrassLevelBox_KeyDown(object sender, KeyEventArgs e)
        {
            ValidateFloatKeyEvent(GrassLevelBox, e);
            UpdateButtonStatus();
        }

        private void ShortMountianLevelBox_KeyDown(object sender, KeyEventArgs e)
        {
            ValidateFloatKeyEvent(ShortMountianLevelBox, e);
            UpdateButtonStatus();
        }

        private void TreeLevelBox_KeyDown(object sender, KeyEventArgs e)
        {
            ValidateFloatKeyEvent(TreeLevelBox, e);
            UpdateButtonStatus();
        }
        private void OctaveBox_KeyDown(object sender, KeyEventArgs e)
        {
            ValidateFloatKeyEvent(OctaveBox, e);
            UpdateButtonStatus();
        }
        private void FrequencyBox_KeyDown(object sender, KeyEventArgs e)
        {
            ValidateFloatKeyEvent(FrequencyBox, e);
            UpdateButtonStatus();
        }
        private void PersistenceBox_KeyDown(object sender, KeyEventArgs e)
        {
            ValidateFloatKeyEvent(PersistenceBox, e);
            UpdateButtonStatus();
        }
        private void ZoomBox_KeyDown(object sender, KeyEventArgs e)
        {
            ValidateFloatKeyEvent(ZoomBox, e);
            UpdateButtonStatus();
        }
        private void UpdateButtonStatus()
        {
            OkButton.Enabled = true;

            if(XSizeBox.Text.Length < 1 || YSizeBox.Text.Length < 1)
                OkButton.Enabled = false;

            if(RandomCheckBox.Checked)
            {
                if(OctaveBox.Text.Length < 1 || 
                   FrequencyBox.Text.Length < 1 ||
                   PersistenceBox.Text.Length < 1 ||
                   ZoomBox.Text.Length < 1)
                    OkButton.Enabled = false;
            }
        }
        private void ValidateIntegerKeyEvent(TextBox textBox, KeyEventArgs e)
        {
            KeyEventArgs keyEvent = e as KeyEventArgs;

            if(textBox.Text.Length > 3 && !acceptableKeys.Contains(keyEvent.KeyCode))
                keyEvent.SuppressKeyPress = true;

            if(!Char.IsDigit((char)keyEvent.KeyValue) && !acceptableKeys.Contains(keyEvent.KeyCode))
            {
                keyEvent.SuppressKeyPress = true;
            }
            if(keyEvent.KeyCode == Keys.Enter && OkButton.Enabled)
            {
                OkButton_Click(textBox, e);
            }
            if(keyEvent.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
        private void ValidateFloatKeyEvent(TextBox textBox, KeyEventArgs keyEvent)
        {
            if(textBox.Text.Length > 3 && !acceptableKeys.Contains(keyEvent.KeyCode))
                keyEvent.SuppressKeyPress = true;

            if(!Char.IsDigit((char)keyEvent.KeyValue) &&
                keyEvent.KeyCode != Keys.OemPeriod &&
                keyEvent.KeyCode != Keys.OemMinus &&
                !acceptableKeys.Contains(keyEvent.KeyCode))            
                keyEvent.SuppressKeyPress = true;
            
            if( keyEvent.KeyCode == Keys.OemPeriod && textBox.Text.Contains('.') )
                keyEvent.SuppressKeyPress = true;
            if( keyEvent.KeyCode == Keys.OemMinus && textBox.Text.Length > 0 )
                keyEvent.SuppressKeyPress = true;           

            if(keyEvent.KeyCode == Keys.Enter && OkButton.Enabled)
            {
                OkButton_Click(textBox, keyEvent);
            }
            if(keyEvent.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
        private void OkButton_Click(object sender, EventArgs e)
        {
            okPressed = true;
            mapSize = new Microsoft.Xna.Framework.Point(int.Parse(XSizeBox.Text), int.Parse(YSizeBox.Text));

            waterLevel = float.Parse(WaterLevelBox.Text);
            grassLevel = float.Parse(GrassLevelBox.Text);
            shortMountianLevel = float.Parse(ShortMountianLevelBox.Text);
            treeLevel = float.Parse(TreeLevelBox.Text);

            octaves = int.Parse(OctaveBox.Text);
            frequency = float.Parse(FrequencyBox.Text);
            persistence = float.Parse(PersistenceBox.Text);
            zoom = float.Parse(ZoomBox.Text);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
        public bool RandomMap
        {
            get { return RandomCheckBox.Checked; }
        }
        public Microsoft.Xna.Framework.Point MapSize
        {
            get { return mapSize; }            
        }
        public float GrassLevel
        {
            get { return grassLevel; }
            set { grassLevel = value; }
        }
        public float WaterLevel
        {
            get { return waterLevel; }
            set { waterLevel = value; }
        }
        public float ShortMountianLevel
        {
            get { return shortMountianLevel; }
            set { shortMountianLevel = value; }
        }
        public float TreeLevel
        {
            get { return treeLevel; }
            set { treeLevel = value; }
        }
        public float Frequency
        {
            get { return frequency; }            
        }
        public int Octaves
        {
            get { return octaves; }            
        }
        public float Persistence
        {
            get { return persistence; }            
        }
        public float Zoom
        {
            get { return zoom; }            
        }   
    }
}
