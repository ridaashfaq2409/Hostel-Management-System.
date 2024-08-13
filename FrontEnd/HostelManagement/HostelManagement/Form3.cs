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
    public partial class Form3 : Form
    {
        string reg,email;
        OracleDataAdapter da;
        DataSet ds;
        DataTable dt;
        public Form3()
        {
            InitializeComponent();
            this.passTB.PasswordChar = '*';
            this.retypepassTB.PasswordChar = '*';
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        public bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (mailTB.Text == string.Empty)
            {
                reqmail.Visible = true;
            }
            else
            {
                reqmail.Visible = false;
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
            if (regTB.Text == string.Empty)
            {
                reqreg.Visible = true;
            }
            else
            {
                reqreg.Visible = false;
            }
            if(!reqmail.Visible && (!mailTB.Text.Contains("@") ||  !mailTB.Text.Contains(".com"))) 
            { 
                invalidmail.Visible = true;
            }
            else
            {
                invalidmail.Visible = false;
            }
            if(!reqreg.Visible && (!IsNumeric(regTB.Text) || regTB.Text.Length > 9))
            {
                invalidreg.Visible = true;
            }
            else
            {
                invalidreg.Visible = false;
            }
            if(retypepassTB.Text != passTB.Text && !reqretype.Visible){
                invalidretype.Visible = true;
            }
            else{
                invalidretype.Visible = false;
            }
            if (!reqmail.Visible && !reqpass.Visible && !reqretype.Visible && !reqreg.Visible && !invalidmail.Visible && !invalidreg.Visible && !invalidretype.Visible)
            {
                string ConStr = "DATA SOURCE=DESKTOP-FE4CR37:1521/XE;USER ID=SYSTEM;Password=rampage";
                OracleConnection conn = new OracleConnection(ConStr);
                reg = regTB.Text;
                try
                {     
                    conn.Open();
                    OracleCommand comm = new OracleCommand("", conn);
                    comm.CommandText = "select email from usertype where reg_no='" + reg +"'";
                    comm.CommandType = CommandType.Text;
                    ds = new DataSet();
                    da = new OracleDataAdapter(comm.CommandText, conn);
                    da.Fill(ds, "usertype");
                    dt = ds.Tables["usertype"];
                    DataRow dr;
                    int t = dt.Rows.Count;
                    if (t == 0)
                    {
                        invaliduser.Visible = true;
                        conn.Close();
                        return;
                    }
                    else
                    {
                        invaliduser.Visible = false;
                        dr = dt.Rows[0];
                        email = dr["email"].ToString();
                        if (email != mailTB.Text)
                        {
                            match.Visible = true;
                            conn.Close();
                            return;
                        }
                        match.Visible = false;
                        OracleTransaction txn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                        try
                        {
                            comm.CommandText = "update usertype set password='" + passTB.Text + "' where reg_no='" + reg.ToString() +"'";
                            comm.CommandType = CommandType.Text;
                            comm.ExecuteNonQuery();
                            txn.Commit();
                            DialogResult dr1 = MessageBox.Show("Password Reset Successfully!\n\nRedirecting to Login", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (dr1 == DialogResult.OK)
                            {
                                LOGIN frm = new LOGIN();
                                this.Hide();
                                frm.ShowDialog();
                                this.Close();
                            }
                        }
                        catch (Exception e1) {
                            txn.Rollback();
                            MessageBox.Show(e1.ToString(), "Fail", MessageBoxButtons.OK);
                        }
                    }
                    conn.Close();
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.ToString(), "Fail", MessageBoxButtons.OK);
                }
            }
        }

        private void reqretype_Click(object sender, EventArgs e)
        {

        }
    }
}
