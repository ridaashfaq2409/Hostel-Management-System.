using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace HostelManagement
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            passTB.PasswordChar = '*';
            retypepassTB.PasswordChar = '*';
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }

        public bool Validate(string yourString)
        {
            return yourString.All(ch => char.IsLetterOrDigit(ch)) || (yourString.Any(ch => char.IsWhiteSpace(ch)));
        }

        public bool HaveNumeric(string value)
        {
            return value.Any(char.IsNumber);
        }

        public bool HaveWhitespace(string value) {
            return value.Any(char.IsWhiteSpace);
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (regTB.Text == string.Empty)
            {
                reqreg.Visible = true;
            }
            else
            {
                reqreg.Visible = false;
            }
            if(userTB.Text == string.Empty)
            {
                requser.Visible = true;
            }
            else
            {
                requser.Visible= false;
            }
            if(contactTB.Text == string.Empty)
            {
                reqcontact.Visible = true;
            }
            else
            {
                reqcontact.Visible= false;
            }
            if(mailTB.Text == string.Empty)
            {
                reqmail.Visible = true;
            }
            else
            {
                reqmail.Visible= false;
            }
            if(passTB.Text == string.Empty)
            {
                reqpass.Visible = true;
            }
            else
            {
                reqpass.Visible = false;
            }
            if(retypepassTB.Text == string.Empty)
            {
                reqretype.Visible = true;
            }
            else
            {
                reqretype.Visible = false;
            }
            if(captcha.Checked == false)
            {
                reqcaptcha.Visible = true;
            }
            else
            {
                reqcaptcha.Visible= false;
            }
            // Ensures registration number to have digits starting from the digit 0,1,2
            if (!reqreg.Visible && !IsNumeric(regTB.Text) && regTB.Text[0]!='2' && regTB.Text[0]!='1' && regTB.Text[0]!='0')
            {
                invalidreg.Visible = true;
            }
            else
            {
                if ((regTB.Text[0] == '2' && regTB.Text.Length != 8) || (regTB.Text[0] == '1' && regTB.Text.Length != 3) || (regTB.Text[0] == '0' && regTB.Text.Length != 5))
                {
                    invalidreg.Visible = true;
                }
                else
                {
                    invalidreg.Visible = false;
                }
            }
            // Ensures domain-check in email feild
            if (!reqmail.Visible && (!mailTB.Text.Contains("@gmail.com") && !mailTB.Text.Contains("@yahoo.com") && !mailTB.Text.Contains("@outlook.com") && !mailTB.Text.Contains("@hotmail.com") && !mailTB.Text.Contains("@icloud.com")))
            {
                invalidmail.Visible = true;
            }
            else
            {
                invalidmail.Visible = false;
            }
            if (HaveNumeric(userTB.Text) || !Validate(userTB.Text))
            {
                invalidname.Visible = true;
            }
            else
            {
                invalidname.Visible = false;
            }
            if (passTB.Text != retypepassTB.Text && !reqpass.Visible && !reqretype.Visible)
            {
                invalidretype.Visible = true;
            }
            else
            {
                invalidretype.Visible = false;
            }
            if (contactTB.Text.Length != 10 && !reqcontact.Visible)
            {
                invalidno.Visible = true;
            }
            else
            {
                invalidno.Visible = false;
            }
            if (!reqpass.Visible && HaveWhitespace(passTB.Text))
            {
                invalidpass.Visible = true;
            }
            else 
            {
                invalidpass.Visible = false;
            }
            if(!reqcaptcha.Visible && !reqcontact.Visible && !reqmail.Visible && !reqpass.Visible && !reqreg.Visible && !reqretype.Visible && !requser.Visible && !invalidreg.Visible && !invalidmail.Visible && !invalidname.Visible && !invalidretype.Visible && !invalidno.Visible && !invalidpass.Visible)
            {
                string ConStr = "DATA SOURCE=DESKTOP-FE4CR37:1521/XE;USER ID=SYSTEM;Password=rampage";
                OracleConnection con = new OracleConnection(ConStr);
                try
                {
                    con.Open();
                    OracleCommand cmd1 = new OracleCommand("", con);
                    OracleCommand cmd2 = new OracleCommand("", con);
                    OracleTransaction txn = con.BeginTransaction(IsolationLevel.ReadCommitted);
                    try
                    {
                        if (regTB.Text[0] == '2') {
                            cmd1.CommandText = "insert into usertype values ('"+regTB.Text+"','"+mailTB.Text+"','"+passTB.Text+"','"+contactTB.Text+"')";
                            cmd1.CommandType = CommandType.Text;
                            cmd1.ExecuteNonQuery();
                            txn.Commit();
                            cmd2.CommandText = "insert into student(registration_number,name) values('"+regTB.Text+"','"+userTB.Text+"')";
                            cmd2.CommandType = CommandType.Text;
                            cmd2.ExecuteNonQuery();
                            //txn.Commit();
                            DialogResult dr = MessageBox.Show("Student Registered Successfully!", "Success", MessageBoxButtons.OK,MessageBoxIcon.Information);
                            if (dr == DialogResult.OK)
                            {
                                LOGIN frm = new LOGIN();
                                this.Hide();
                                frm.ShowDialog();
                                this.Close();
                            }
                        }
                        else if (regTB.Text[0] == '1')
                        {
                            cmd1.CommandText = "insert into usertype values ('" + regTB.Text + "','" + mailTB.Text + "','" + passTB.Text + "','" + contactTB.Text + "')";
                            cmd1.CommandType = CommandType.Text;
                            cmd1.ExecuteNonQuery();
                            txn.Commit();
                            cmd2.CommandText = "insert into caretaker(ct_id,ct_name) values('" + regTB.Text + "','" + userTB.Text + "')";
                            cmd2.CommandType = CommandType.Text;
                            cmd2.ExecuteNonQuery();
                            //txn.Commit();
                            DialogResult dr = MessageBox.Show("Caretaker Registered Successfully!", "Success", MessageBoxButtons.OK,MessageBoxIcon.Information);
                            if (dr == DialogResult.OK)
                            {
                                LOGIN frm = new LOGIN();
                                this.Hide();
                                frm.ShowDialog();
                                this.Close();
                            }
                        }
                        else if (regTB.Text[0] == '0')
                        {
                            cmd1.CommandText = "insert into usertype values ('" + regTB.Text + "','" + mailTB.Text + "','" + passTB.Text + "','" + contactTB.Text + "')";
                            cmd1.CommandType = CommandType.Text;
                            cmd1.ExecuteNonQuery();
                            txn.Commit();
                            cmd2.CommandText = "insert into administrator(admin_id,name) values('" + regTB.Text + "','" + userTB.Text + "')";
                            cmd2.CommandType = CommandType.Text;
                            cmd2.ExecuteNonQuery();
                            //txn.Commit();
                            DialogResult dr = MessageBox.Show("Administrator Registered Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (dr == DialogResult.OK)
                            {
                                LOGIN frm = new LOGIN();
                                this.Hide();
                                frm.ShowDialog();
                                this.Close();
                            }
                        }
                    }
                    catch (Exception e1)
                    {
                        txn.Rollback();
                        DialogResult dr = MessageBox.Show("Already Registered!\n\nRedirecting to Login Page", "Fail", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        if (dr == DialogResult.OK)
                        {
                            LOGIN frm = new LOGIN();
                            this.Hide();
                            frm.ShowDialog();
                            this.Close();
                        }
                    }
                }
                catch (Exception e1)
                {
                    DialogResult dr = MessageBox.Show(e1.ToString(), "Fail", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    if (dr == DialogResult.OK)
                    {
                        Form2 frm = new Form2();
                        this.Hide();
                        frm.ShowDialog();
                        this.Close();
                    }
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LOGIN frm = new LOGIN();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }
    }
}
