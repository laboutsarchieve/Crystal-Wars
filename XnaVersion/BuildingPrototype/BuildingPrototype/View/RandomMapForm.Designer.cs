namespace MapEditor.View
{
    partial class RandomMapForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ZoomBox = new System.Windows.Forms.TextBox();
            this.PersistenceBox = new System.Windows.Forms.TextBox();
            this.FrequencyBox = new System.Windows.Forms.TextBox();
            this.OctaveBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.XSizeBox = new System.Windows.Forms.TextBox();
            this.YSizeBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.RandomCheckBox = new System.Windows.Forms.CheckBox();
            this.TreeAttributeLabel = new System.Windows.Forms.Label();
            this.ShortMountianLevelLabel = new System.Windows.Forms.Label();
            this.GrassLevelLabel = new System.Windows.Forms.Label();
            this.WaterLevelLabel = new System.Windows.Forms.Label();
            this.WaterLevelBox = new System.Windows.Forms.TextBox();
            this.GrassLevelBox = new System.Windows.Forms.TextBox();
            this.ShortMountianLevelBox = new System.Windows.Forms.TextBox();
            this.TreeLevelBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ZoomBox
            // 
            this.ZoomBox.Location = new System.Drawing.Point(303, 286);
            this.ZoomBox.Name = "ZoomBox";
            this.ZoomBox.Size = new System.Drawing.Size(100, 20);
            this.ZoomBox.TabIndex = 0;
            this.ZoomBox.Text = "10";
            this.ZoomBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ZoomBox_KeyDown);
            // 
            // PersistenceBox
            // 
            this.PersistenceBox.Location = new System.Drawing.Point(303, 239);
            this.PersistenceBox.Name = "PersistenceBox";
            this.PersistenceBox.Size = new System.Drawing.Size(100, 20);
            this.PersistenceBox.TabIndex = 1;
            this.PersistenceBox.Text = "0.5";
            this.PersistenceBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PersistenceBox_KeyDown);
            // 
            // FrequencyBox
            // 
            this.FrequencyBox.Location = new System.Drawing.Point(303, 195);
            this.FrequencyBox.Name = "FrequencyBox";
            this.FrequencyBox.Size = new System.Drawing.Size(100, 20);
            this.FrequencyBox.TabIndex = 2;
            this.FrequencyBox.Text = "2.0";
            this.FrequencyBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrequencyBox_KeyDown);
            // 
            // OctaveBox
            // 
            this.OctaveBox.Location = new System.Drawing.Point(303, 148);
            this.OctaveBox.Name = "OctaveBox";
            this.OctaveBox.Size = new System.Drawing.Size(100, 20);
            this.OctaveBox.TabIndex = 3;
            this.OctaveBox.Text = "6";
            this.OctaveBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OctaveBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(240, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Octaves";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Frequency";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(230, 246);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Persistence";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(253, 289);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Zoom";
            // 
            // XSizeBox
            // 
            this.XSizeBox.Location = new System.Drawing.Point(132, 48);
            this.XSizeBox.Name = "XSizeBox";
            this.XSizeBox.Size = new System.Drawing.Size(100, 20);
            this.XSizeBox.TabIndex = 8;
            this.XSizeBox.Text = "100";
            this.XSizeBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.XSizeBox_KeyDown);
            // 
            // YSizeBox
            // 
            this.YSizeBox.Location = new System.Drawing.Point(263, 48);
            this.YSizeBox.Name = "YSizeBox";
            this.YSizeBox.Size = new System.Drawing.Size(100, 20);
            this.YSizeBox.TabIndex = 9;
            this.YSizeBox.Text = "100";
            this.YSizeBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.YSizeBox_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(57, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Map Size";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(238, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "By";
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(157, 346);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 12;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // RandomCheckBox
            // 
            this.RandomCheckBox.AutoSize = true;
            this.RandomCheckBox.Location = new System.Drawing.Point(174, 101);
            this.RandomCheckBox.Name = "RandomCheckBox";
            this.RandomCheckBox.Size = new System.Drawing.Size(137, 17);
            this.RandomCheckBox.TabIndex = 13;
            this.RandomCheckBox.Text = "Generate Random Map";
            this.RandomCheckBox.UseVisualStyleBackColor = true;
            this.RandomCheckBox.CheckedChanged += new System.EventHandler(this.RandomCheckBox_CheckedChanged);
            // 
            // TreeAttributeLabel
            // 
            this.TreeAttributeLabel.AutoSize = true;
            this.TreeAttributeLabel.Location = new System.Drawing.Point(36, 286);
            this.TreeAttributeLabel.Name = "TreeAttributeLabel";
            this.TreeAttributeLabel.Size = new System.Drawing.Size(71, 13);
            this.TreeAttributeLabel.TabIndex = 21;
            this.TreeAttributeLabel.Text = "Tree Attribute";
            // 
            // ShortMountianLevelLabel
            // 
            this.ShortMountianLevelLabel.AutoSize = true;
            this.ShortMountianLevelLabel.Location = new System.Drawing.Point(0, 246);
            this.ShortMountianLevelLabel.Name = "ShortMountianLevelLabel";
            this.ShortMountianLevelLabel.Size = new System.Drawing.Size(108, 13);
            this.ShortMountianLevelLabel.TabIndex = 20;
            this.ShortMountianLevelLabel.Text = "Short Mountian Level";
            // 
            // GrassLevelLabel
            // 
            this.GrassLevelLabel.AutoSize = true;
            this.GrassLevelLabel.Location = new System.Drawing.Point(51, 198);
            this.GrassLevelLabel.Name = "GrassLevelLabel";
            this.GrassLevelLabel.Size = new System.Drawing.Size(63, 13);
            this.GrassLevelLabel.TabIndex = 19;
            this.GrassLevelLabel.Text = "Grass Level";
            // 
            // WaterLevelLabel
            // 
            this.WaterLevelLabel.AutoSize = true;
            this.WaterLevelLabel.Location = new System.Drawing.Point(43, 155);
            this.WaterLevelLabel.Name = "WaterLevelLabel";
            this.WaterLevelLabel.Size = new System.Drawing.Size(65, 13);
            this.WaterLevelLabel.TabIndex = 18;
            this.WaterLevelLabel.Text = "Water Level";
            // 
            // WaterLevelBox
            // 
            this.WaterLevelBox.Location = new System.Drawing.Point(124, 148);
            this.WaterLevelBox.Name = "WaterLevelBox";
            this.WaterLevelBox.Size = new System.Drawing.Size(100, 20);
            this.WaterLevelBox.TabIndex = 17;
            this.WaterLevelBox.Text = "0.0";
            this.WaterLevelBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WaterLevelBox_KeyDown);
            // 
            // GrassLevelBox
            // 
            this.GrassLevelBox.Location = new System.Drawing.Point(124, 195);
            this.GrassLevelBox.Name = "GrassLevelBox";
            this.GrassLevelBox.Size = new System.Drawing.Size(100, 20);
            this.GrassLevelBox.TabIndex = 16;
            this.GrassLevelBox.Text = "0.3";
            this.GrassLevelBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GrassLevelBox_KeyDown);
            // 
            // ShortMountianLevelBox
            // 
            this.ShortMountianLevelBox.Location = new System.Drawing.Point(124, 239);
            this.ShortMountianLevelBox.Name = "ShortMountianLevelBox";
            this.ShortMountianLevelBox.Size = new System.Drawing.Size(100, 20);
            this.ShortMountianLevelBox.TabIndex = 15;
            this.ShortMountianLevelBox.Text = "0.5";
            this.ShortMountianLevelBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ShortMountianLevelBox_KeyDown);
            // 
            // TreeLevelBox
            // 
            this.TreeLevelBox.Location = new System.Drawing.Point(124, 286);
            this.TreeLevelBox.Name = "TreeLevelBox";
            this.TreeLevelBox.Size = new System.Drawing.Size(100, 20);
            this.TreeLevelBox.TabIndex = 14;
            this.TreeLevelBox.Text = "0.4";
            this.TreeLevelBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeLevelBox_KeyDown);
            // 
            // RandomMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 393);
            this.Controls.Add(this.TreeAttributeLabel);
            this.Controls.Add(this.ShortMountianLevelLabel);
            this.Controls.Add(this.GrassLevelLabel);
            this.Controls.Add(this.WaterLevelLabel);
            this.Controls.Add(this.WaterLevelBox);
            this.Controls.Add(this.GrassLevelBox);
            this.Controls.Add(this.ShortMountianLevelBox);
            this.Controls.Add(this.TreeLevelBox);
            this.Controls.Add(this.RandomCheckBox);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.YSizeBox);
            this.Controls.Add(this.XSizeBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OctaveBox);
            this.Controls.Add(this.FrequencyBox);
            this.Controls.Add(this.PersistenceBox);
            this.Controls.Add(this.ZoomBox);
            this.Name = "RandomMapForm";
            this.Text = "RandomMapForm";
            this.Load += new System.EventHandler(this.RandomMapForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ZoomBox;
        private System.Windows.Forms.TextBox PersistenceBox;
        private System.Windows.Forms.TextBox FrequencyBox;
        private System.Windows.Forms.TextBox OctaveBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox XSizeBox;
        private System.Windows.Forms.TextBox YSizeBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.CheckBox RandomCheckBox;
        private System.Windows.Forms.Label TreeAttributeLabel;
        private System.Windows.Forms.Label ShortMountianLevelLabel;
        private System.Windows.Forms.Label GrassLevelLabel;
        private System.Windows.Forms.Label WaterLevelLabel;
        private System.Windows.Forms.TextBox WaterLevelBox;
        private System.Windows.Forms.TextBox GrassLevelBox;
        private System.Windows.Forms.TextBox ShortMountianLevelBox;
        private System.Windows.Forms.TextBox TreeLevelBox;
    }
}