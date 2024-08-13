namespace HostelManagement
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.mailabel = new System.Windows.Forms.Label();
            this.mailTB = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.regTB = new System.Windows.Forms.RichTextBox();
            this.reqreg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.reqmail = new System.Windows.Forms.Label();
            this.submitButton = new System.Windows.Forms.Button();
            this.passTB = new System.Windows.Forms.TextBox();
            this.reqpass = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.reqretype = new System.Windows.Forms.Label();
            this.retypepassTB = new System.Windows.Forms.TextBox();
            this.invalidreg = new System.Windows.Forms.Label();
            this.invalidmail = new System.Windows.Forms.Label();
            this.invaliduser = new System.Windows.Forms.Label();
            this.match = new System.Windows.Forms.Label();
            this.invalidretype = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(246, 87);
            this.pictureBox6.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(206, 120);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 19;
            this.pictureBox6.TabStop = false;
            // 
            // mailabel
            // 
            this.mailabel.AutoSize = true;
            this.mailabel.BackColor = System.Drawing.Color.Transparent;
            this.mailabel.Font = new System.Drawing.Font("Bookman Old Style", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mailabel.Location = new System.Drawing.Point(380, 291);
            this.mailabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mailabel.Name = "mailabel";
            this.mailabel.Size = new System.Drawing.Size(194, 21);
            this.mailabel.TabIndex = 53;
            this.mailabel.Text = "Retype New Password";
            // 
            // mailTB
            // 
            this.mailTB.Font = new System.Drawing.Font("Bookman Old Style", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mailTB.Location = new System.Drawing.Point(384, 236);
            this.mailTB.Margin = new System.Windows.Forms.Padding(4);
            this.mailTB.Multiline = false;
            this.mailTB.Name = "mailTB";
            this.mailTB.Size = new System.Drawing.Size(212, 29);
            this.mailTB.TabIndex = 49;
            this.mailTB.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(380, 212);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 21);
            this.label3.TabIndex = 47;
            this.label3.Text = "E-mail Address";
            // 
            // regTB
            // 
            this.regTB.Font = new System.Drawing.Font("Bookman Old Style", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.regTB.Location = new System.Drawing.Point(111, 236);
            this.regTB.Margin = new System.Windows.Forms.Padding(4);
            this.regTB.Multiline = false;
            this.regTB.Name = "regTB";
            this.regTB.Size = new System.Drawing.Size(217, 29);
            this.regTB.TabIndex = 46;
            this.regTB.Text = "";
            // 
            // reqreg
            // 
            this.reqreg.AutoSize = true;
            this.reqreg.BackColor = System.Drawing.Color.Transparent;
            this.reqreg.Font = new System.Drawing.Font("Bookman Old Style", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reqreg.ForeColor = System.Drawing.Color.Red;
            this.reqreg.Location = new System.Drawing.Point(243, 268);
            this.reqreg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.reqreg.Name = "reqreg";
            this.reqreg.Size = new System.Drawing.Size(93, 16);
            this.reqreg.TabIndex = 45;
            this.reqreg.Text = "*required field";
            this.reqreg.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(107, 211);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 21);
            this.label1.TabIndex = 44;
            this.label1.Text = "Registration Number";
            // 
            // reqmail
            // 
            this.reqmail.AutoSize = true;
            this.reqmail.BackColor = System.Drawing.Color.Transparent;
            this.reqmail.Font = new System.Drawing.Font("Bookman Old Style", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reqmail.ForeColor = System.Drawing.Color.Red;
            this.reqmail.Location = new System.Drawing.Point(494, 271);
            this.reqmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.reqmail.Name = "reqmail";
            this.reqmail.Size = new System.Drawing.Size(93, 16);
            this.reqmail.TabIndex = 56;
            this.reqmail.Text = "*required field";
            this.reqmail.Visible = false;
            // 
            // submitButton
            // 
            this.submitButton.BackColor = System.Drawing.SystemColors.Info;
            this.submitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.submitButton.Font = new System.Drawing.Font("Bookman Old Style", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitButton.Location = new System.Drawing.Point(307, 383);
            this.submitButton.Margin = new System.Windows.Forms.Padding(4);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(103, 30);
            this.submitButton.TabIndex = 57;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = false;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // passTB
            // 
            this.passTB.AcceptsReturn = true;
            this.passTB.Font = new System.Drawing.Font("Bookman Old Style", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passTB.Location = new System.Drawing.Point(117, 315);
            this.passTB.Multiline = true;
            this.passTB.Name = "passTB";
            this.passTB.Size = new System.Drawing.Size(220, 29);
            this.passTB.TabIndex = 61;
            // 
            // reqpass
            // 
            this.reqpass.AutoSize = true;
            this.reqpass.BackColor = System.Drawing.Color.Transparent;
            this.reqpass.Font = new System.Drawing.Font("Bookman Old Style", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reqpass.ForeColor = System.Drawing.Color.Red;
            this.reqpass.Location = new System.Drawing.Point(243, 347);
            this.reqpass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.reqpass.Name = "reqpass";
            this.reqpass.Size = new System.Drawing.Size(93, 16);
            this.reqpass.TabIndex = 62;
            this.reqpass.Text = "*required field";
            this.reqpass.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Bookman Old Style", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(113, 291);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 21);
            this.label5.TabIndex = 63;
            this.label5.Text = "New Password";
            // 
            // reqretype
            // 
            this.reqretype.AutoSize = true;
            this.reqretype.BackColor = System.Drawing.Color.Transparent;
            this.reqretype.Font = new System.Drawing.Font("Bookman Old Style", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reqretype.ForeColor = System.Drawing.Color.Red;
            this.reqretype.Location = new System.Drawing.Point(494, 347);
            this.reqretype.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.reqretype.Name = "reqretype";
            this.reqretype.Size = new System.Drawing.Size(93, 16);
            this.reqretype.TabIndex = 65;
            this.reqretype.Text = "*required field";
            this.reqretype.Visible = false;
            this.reqretype.Click += new System.EventHandler(this.reqretype_Click);
            // 
            // retypepassTB
            // 
            this.retypepassTB.AcceptsReturn = true;
            this.retypepassTB.Font = new System.Drawing.Font("Bookman Old Style", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.retypepassTB.Location = new System.Drawing.Point(384, 315);
            this.retypepassTB.Multiline = true;
            this.retypepassTB.Name = "retypepassTB";
            this.retypepassTB.Size = new System.Drawing.Size(212, 29);
            this.retypepassTB.TabIndex = 64;
            // 
            // invalidreg
            // 
            this.invalidreg.AutoSize = true;
            this.invalidreg.BackColor = System.Drawing.Color.Transparent;
            this.invalidreg.Font = new System.Drawing.Font("Bookman Old Style", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invalidreg.ForeColor = System.Drawing.Color.Red;
            this.invalidreg.Location = new System.Drawing.Point(244, 269);
            this.invalidreg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.invalidreg.Name = "invalidreg";
            this.invalidreg.Size = new System.Drawing.Size(96, 16);
            this.invalidreg.TabIndex = 66;
            this.invalidreg.Text = "*invalid format";
            this.invalidreg.Visible = false;
            // 
            // invalidmail
            // 
            this.invalidmail.AutoSize = true;
            this.invalidmail.BackColor = System.Drawing.Color.Transparent;
            this.invalidmail.Font = new System.Drawing.Font("Bookman Old Style", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invalidmail.ForeColor = System.Drawing.Color.Red;
            this.invalidmail.Location = new System.Drawing.Point(494, 270);
            this.invalidmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.invalidmail.Name = "invalidmail";
            this.invalidmail.Size = new System.Drawing.Size(96, 16);
            this.invalidmail.TabIndex = 67;
            this.invalidmail.Text = "*invalid format";
            this.invalidmail.Visible = false;
            // 
            // invaliduser
            // 
            this.invaliduser.AutoSize = true;
            this.invaliduser.BackColor = System.Drawing.Color.Transparent;
            this.invaliduser.Font = new System.Drawing.Font("Bookman Old Style", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invaliduser.ForeColor = System.Drawing.Color.Red;
            this.invaliduser.Location = new System.Drawing.Point(243, 270);
            this.invaliduser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.invaliduser.Name = "invaliduser";
            this.invaliduser.Size = new System.Drawing.Size(84, 16);
            this.invaliduser.TabIndex = 68;
            this.invaliduser.Text = "*invalid user";
            this.invaliduser.Visible = false;
            // 
            // match
            // 
            this.match.AutoSize = true;
            this.match.BackColor = System.Drawing.Color.Transparent;
            this.match.Font = new System.Drawing.Font("Bookman Old Style", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.match.ForeColor = System.Drawing.Color.Red;
            this.match.Location = new System.Drawing.Point(188, 271);
            this.match.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.match.Name = "match";
            this.match.Size = new System.Drawing.Size(330, 16);
            this.match.TabIndex = 69;
            this.match.Text = "*email address and registration number do not match";
            this.match.Visible = false;
            // 
            // invalidretype
            // 
            this.invalidretype.AutoSize = true;
            this.invalidretype.BackColor = System.Drawing.Color.Transparent;
            this.invalidretype.Font = new System.Drawing.Font("Bookman Old Style", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invalidretype.ForeColor = System.Drawing.Color.Red;
            this.invalidretype.Location = new System.Drawing.Point(425, 347);
            this.invalidretype.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.invalidretype.Name = "invalidretype";
            this.invalidretype.Size = new System.Drawing.Size(162, 16);
            this.invalidretype.TabIndex = 70;
            this.invalidretype.Text = "*passwords do not match\'";
            this.invalidretype.Visible = false;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::HostelManagement.Properties.Resources.forgotpassword1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(695, 497);
            this.Controls.Add(this.invalidretype);
            this.Controls.Add(this.match);
            this.Controls.Add(this.invaliduser);
            this.Controls.Add(this.invalidmail);
            this.Controls.Add(this.invalidreg);
            this.Controls.Add(this.reqretype);
            this.Controls.Add(this.retypepassTB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.reqpass);
            this.Controls.Add(this.passTB);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.reqmail);
            this.Controls.Add(this.mailabel);
            this.Controls.Add(this.mailTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.regTB);
            this.Controls.Add(this.reqreg);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox6);
            this.DoubleBuffered = true;
            this.Name = "Form3";
            this.Text = "Change Password";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Label mailabel;
        private System.Windows.Forms.RichTextBox mailTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox regTB;
        private System.Windows.Forms.Label reqreg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label reqmail;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.TextBox passTB;
        private System.Windows.Forms.Label reqpass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label reqretype;
        private System.Windows.Forms.TextBox retypepassTB;
        private System.Windows.Forms.Label invalidreg;
        private System.Windows.Forms.Label invalidmail;
        private System.Windows.Forms.Label invaliduser;
        private System.Windows.Forms.Label match;
        private System.Windows.Forms.Label invalidretype;
    }
}