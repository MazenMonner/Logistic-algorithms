namespace LFTFsolver
{
    partial class Form1
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
            if (disposing && (components != null))
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
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboSheet = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.calcBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.FCtxtbx = new System.Windows.Forms.TextBox();
            this.VCtxtbx = new System.Windows.Forms.TextBox();
            this.HCtxtbx = new System.Windows.Forms.TextBox();
            this.ctxtBx = new System.Windows.Forms.TextBox();
            this.currentLbl = new System.Windows.Forms.Label();
            this.CompRevBtn = new System.Windows.Forms.Button();
            this.VoptTxtBx = new System.Windows.Forms.Label();
            this.tcTxt = new System.Windows.Forms.Label();
            this.tvcTxt = new System.Windows.Forms.Label();
            this.tfcTxt = new System.Windows.Forms.Label();
            this.thcTxt = new System.Windows.Forms.Label();
            this.cthcTxt = new System.Windows.Forms.Label();
            this.ctvcTxt = new System.Windows.Forms.Label();
            this.ctfcTxt = new System.Windows.Forms.Label();
            this.ctcTxt = new System.Windows.Forms.Label();
            this.coTxt = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(657, 33);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(44, 23);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(373, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "file name :";
            // 
            // cboSheet
            // 
            this.cboSheet.FormattingEnabled = true;
            this.cboSheet.Location = new System.Drawing.Point(444, 62);
            this.cboSheet.Name = "cboSheet";
            this.cboSheet.Size = new System.Drawing.Size(121, 24);
            this.cboSheet.TabIndex = 2;
            this.cboSheet.SelectedIndexChanged += new System.EventHandler(this.cbosheet_selected);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(328, 506);
            this.dataGridView1.TabIndex = 3;
            // 
            // txtFilename
            // 
            this.txtFilename.Location = new System.Drawing.Point(444, 34);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.ReadOnly = true;
            this.txtFilename.Size = new System.Drawing.Size(207, 22);
            this.txtFilename.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(373, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Sheet :";
            // 
            // calcBtn
            // 
            this.calcBtn.Location = new System.Drawing.Point(376, 249);
            this.calcBtn.Name = "calcBtn";
            this.calcBtn.Size = new System.Drawing.Size(144, 35);
            this.calcBtn.TabIndex = 6;
            this.calcBtn.Text = "Calculate Results";
            this.calcBtn.UseVisualStyleBackColor = true;
            this.calcBtn.Click += new System.EventHandler(this.calcBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(373, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Fixed Cost";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(373, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Variable Cost";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(373, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Hiring Cost";
            // 
            // FCtxtbx
            // 
            this.FCtxtbx.Location = new System.Drawing.Point(502, 116);
            this.FCtxtbx.Name = "FCtxtbx";
            this.FCtxtbx.Size = new System.Drawing.Size(100, 22);
            this.FCtxtbx.TabIndex = 13;
            this.FCtxtbx.Text = "350";
            // 
            // VCtxtbx
            // 
            this.VCtxtbx.Location = new System.Drawing.Point(502, 153);
            this.VCtxtbx.Name = "VCtxtbx";
            this.VCtxtbx.Size = new System.Drawing.Size(100, 22);
            this.VCtxtbx.TabIndex = 14;
            this.VCtxtbx.Text = "150";
            // 
            // HCtxtbx
            // 
            this.HCtxtbx.Location = new System.Drawing.Point(502, 191);
            this.HCtxtbx.Name = "HCtxtbx";
            this.HCtxtbx.Size = new System.Drawing.Size(100, 22);
            this.HCtxtbx.TabIndex = 15;
            this.HCtxtbx.Text = "800";
            // 
            // ctxtBx
            // 
            this.ctxtBx.Location = new System.Drawing.Point(792, 317);
            this.ctxtBx.Name = "ctxtBx";
            this.ctxtBx.Size = new System.Drawing.Size(65, 22);
            this.ctxtBx.TabIndex = 17;
            this.ctxtBx.Visible = false;
            // 
            // currentLbl
            // 
            this.currentLbl.AutoSize = true;
            this.currentLbl.Location = new System.Drawing.Point(678, 320);
            this.currentLbl.Name = "currentLbl";
            this.currentLbl.Size = new System.Drawing.Size(108, 17);
            this.currentLbl.TabIndex = 16;
            this.currentLbl.Text = "current vehicles";
            this.currentLbl.Visible = false;
            // 
            // CompRevBtn
            // 
            this.CompRevBtn.Location = new System.Drawing.Point(536, 249);
            this.CompRevBtn.Name = "CompRevBtn";
            this.CompRevBtn.Size = new System.Drawing.Size(162, 35);
            this.CompRevBtn.TabIndex = 18;
            this.CompRevBtn.Text = "Compare with existing";
            this.CompRevBtn.UseVisualStyleBackColor = true;
            this.CompRevBtn.Click += new System.EventHandler(this.CompRevBtn_Click);
            // 
            // VoptTxtBx
            // 
            this.VoptTxtBx.AutoSize = true;
            this.VoptTxtBx.Location = new System.Drawing.Point(380, 320);
            this.VoptTxtBx.Name = "VoptTxtBx";
            this.VoptTxtBx.Size = new System.Drawing.Size(179, 17);
            this.VoptTxtBx.TabIndex = 19;
            this.VoptTxtBx.Text = "Optimal number of vehicles";
            this.VoptTxtBx.Visible = false;
            // 
            // tcTxt
            // 
            this.tcTxt.AutoSize = true;
            this.tcTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcTxt.Location = new System.Drawing.Point(372, 355);
            this.tcTxt.Name = "tcTxt";
            this.tcTxt.Size = new System.Drawing.Size(199, 20);
            this.tcTxt.TabIndex = 20;
            this.tcTxt.Text = "Total Transportation Cost";
            this.tcTxt.Visible = false;
            // 
            // tvcTxt
            // 
            this.tvcTxt.AutoSize = true;
            this.tvcTxt.Location = new System.Drawing.Point(419, 416);
            this.tvcTxt.Name = "tvcTxt";
            this.tvcTxt.Size = new System.Drawing.Size(128, 17);
            this.tvcTxt.TabIndex = 22;
            this.tvcTxt.Text = "Total Variable Cost";
            this.tvcTxt.Visible = false;
            // 
            // tfcTxt
            // 
            this.tfcTxt.AutoSize = true;
            this.tfcTxt.Location = new System.Drawing.Point(419, 389);
            this.tfcTxt.Name = "tfcTxt";
            this.tfcTxt.Size = new System.Drawing.Size(109, 17);
            this.tfcTxt.TabIndex = 21;
            this.tfcTxt.Text = "Total Fixed Cost";
            this.tfcTxt.Visible = false;
            // 
            // thcTxt
            // 
            this.thcTxt.AutoSize = true;
            this.thcTxt.Location = new System.Drawing.Point(419, 444);
            this.thcTxt.Name = "thcTxt";
            this.thcTxt.Size = new System.Drawing.Size(113, 17);
            this.thcTxt.TabIndex = 23;
            this.thcTxt.Text = "Total Hiring Cost";
            this.thcTxt.Visible = false;
            // 
            // cthcTxt
            // 
            this.cthcTxt.AutoSize = true;
            this.cthcTxt.Location = new System.Drawing.Point(739, 444);
            this.cthcTxt.Name = "cthcTxt";
            this.cthcTxt.Size = new System.Drawing.Size(113, 17);
            this.cthcTxt.TabIndex = 27;
            this.cthcTxt.Text = "Total Hiring Cost";
            this.cthcTxt.Visible = false;
            // 
            // ctvcTxt
            // 
            this.ctvcTxt.AutoSize = true;
            this.ctvcTxt.Location = new System.Drawing.Point(739, 416);
            this.ctvcTxt.Name = "ctvcTxt";
            this.ctvcTxt.Size = new System.Drawing.Size(128, 17);
            this.ctvcTxt.TabIndex = 26;
            this.ctvcTxt.Text = "Total Variable Cost";
            this.ctvcTxt.Visible = false;
            // 
            // ctfcTxt
            // 
            this.ctfcTxt.AutoSize = true;
            this.ctfcTxt.Location = new System.Drawing.Point(739, 389);
            this.ctfcTxt.Name = "ctfcTxt";
            this.ctfcTxt.Size = new System.Drawing.Size(109, 17);
            this.ctfcTxt.TabIndex = 25;
            this.ctfcTxt.Text = "Total Fixed Cost";
            this.ctfcTxt.Visible = false;
            // 
            // ctcTxt
            // 
            this.ctcTxt.AutoSize = true;
            this.ctcTxt.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ctcTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctcTxt.Location = new System.Drawing.Point(739, 355);
            this.ctcTxt.Name = "ctcTxt";
            this.ctcTxt.Size = new System.Drawing.Size(199, 20);
            this.ctcTxt.TabIndex = 24;
            this.ctcTxt.Text = "Total Transportation Cost";
            this.ctcTxt.Visible = false;
            // 
            // coTxt
            // 
            this.coTxt.AutoSize = true;
            this.coTxt.Location = new System.Drawing.Point(380, 501);
            this.coTxt.Name = "coTxt";
            this.coTxt.Size = new System.Drawing.Size(97, 17);
            this.coTxt.TabIndex = 28;
            this.coTxt.Text = "comparestion:";
            this.coTxt.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 530);
            this.Controls.Add(this.coTxt);
            this.Controls.Add(this.cthcTxt);
            this.Controls.Add(this.ctvcTxt);
            this.Controls.Add(this.ctfcTxt);
            this.Controls.Add(this.ctcTxt);
            this.Controls.Add(this.thcTxt);
            this.Controls.Add(this.tvcTxt);
            this.Controls.Add(this.tfcTxt);
            this.Controls.Add(this.tcTxt);
            this.Controls.Add(this.VoptTxtBx);
            this.Controls.Add(this.CompRevBtn);
            this.Controls.Add(this.ctxtBx);
            this.Controls.Add(this.currentLbl);
            this.Controls.Add(this.HCtxtbx);
            this.Controls.Add(this.VCtxtbx);
            this.Controls.Add(this.FCtxtbx);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.calcBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cboSheet);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBrowse);
            this.Name = "Form1";
            this.Text = "LFTF solver";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboSheet;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button calcBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox FCtxtbx;
        private System.Windows.Forms.TextBox VCtxtbx;
        private System.Windows.Forms.TextBox HCtxtbx;
        private System.Windows.Forms.TextBox ctxtBx;
        private System.Windows.Forms.Label currentLbl;
        private System.Windows.Forms.Button CompRevBtn;
        private System.Windows.Forms.Label VoptTxtBx;
        private System.Windows.Forms.Label tcTxt;
        private System.Windows.Forms.Label tvcTxt;
        private System.Windows.Forms.Label tfcTxt;
        private System.Windows.Forms.Label thcTxt;
        private System.Windows.Forms.Label cthcTxt;
        private System.Windows.Forms.Label ctvcTxt;
        private System.Windows.Forms.Label ctfcTxt;
        private System.Windows.Forms.Label ctcTxt;
        private System.Windows.Forms.Label coTxt;
    }
}

