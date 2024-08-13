using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Oracle.ManagedDataAccess.Client;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace HostelManagement
{
    public partial class LOGIN : Form
    {
        OracleDataAdapter da;
        DataSet ds;
        DataTable dt;
        DataRow dr;
        int i = 0;
        string reg = string.Empty;
        string pass = string.Empty;
        public LOGIN()
        {
            InitializeComponent();
            passTB.PasswordChar = '*';  
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void loginButton_Click(object sender, EventArgs e)
        {
            if (regTB.Text == string.Empty)
            {
                reqreg.Visible = true;
            }
            else
            {
                reqreg.Visible = false;
            }
            if (passTB.Text == string.Empty)
            {
                reqpass.Visible = true;
            }
            else
            {
                reqpass.Visible = false;
            }
            if (captcha.Checked == false)
            {
                reqcaptcha.Visible = true;
            }
            else
            {
                reqcaptcha.Visible = false; 
            }
            if(!reqcaptcha.Visible && !reqpass.Visible && !reqreg.Visible){
                string ConStr = "DATA SOURCE=DESKTOP-FE4CR37:1521/XE;USER ID=SYSTEM;Password=rampage";
                OracleConnection conn = new OracleConnection(ConStr);
                reg = regTB.Text;
                try
                {
                    conn.Open();
                    OracleCommand comm = new OracleCommand("", conn);
                    comm.CommandText = "select password from usertype where reg_no='"+reg+"'";
                    comm.CommandType = CommandType.Text;
                    ds = new DataSet();
                    da = new OracleDataAdapter(comm.CommandText, conn);
                    da.Fill(ds, "usertype");
                    dt = ds.Tables["usertype"];
                    int t = dt.Rows.Count;
                    if (t == 0)
                    {
                        invaliduser.Visible = true;
                        conn.Close();
                        return;
                    }
                    else {
                        invaliduser.Visible = false;
                        dr = dt.Rows[0];
                        pass = dr["password"].ToString();
                        if (pass != passTB.Text) {
                            match.Visible = true;
                            conn.Close();
                            return;
                        }
                        match.Visible = false;
                        if (regTB.Text[0] == '2')
                        {
                            long x = 0;
                            long.TryParse(regTB.Text, out x);
                            Profile frm = new Profile(x);
                            this.Hide();
                            frm.ShowDialog();
                            this.Close();
                        }
                        else if (regTB.Text[0] == '0')
                        {
                            ProfileAdmin frm = new ProfileAdmin(regTB.Text);
                            this.Hide();
                            frm.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            ProfileCaretaker frm = new ProfileCaretaker(regTB.Text);
                            this.Hide();
                            frm.ShowDialog();
                            this.Close();
                        }
                    }
                    
                    conn.Close();

                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.ToString(),"Fail",MessageBoxButtons.OK);
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 frm = new Form2();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 fr = new Form3();
            this.Hide();
            fr.ShowDialog();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
