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
    public partial class ProfileCaretaker : Form
    {
        OracleDataAdapter da;
        DataSet ds;
        DataTable dt;
        DataRow dr;
        int i = 0;
        string name;
        string reg;
        public ProfileCaretaker(string regno)
        {
            InitializeComponent();
            reg = regno;
            reglabel.Text = reg.ToString();
            string ConStr = "DATA SOURCE=DESKTOP-FE4CR37:1521/XE;USER ID=SYSTEM;Password=rampage";
            OracleConnection conn = new OracleConnection(ConStr);
            try
            {
                conn.Open();
                OracleCommand comm = new OracleCommand("", conn);
                comm.CommandText = "select * from usertype where reg_no = '" + reg.ToString() + "'";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "usertype");
                dt = ds.Tables["usertype"];
                dr = dt.Rows[i];
                phonelabel.Text = dr["phone"].ToString();
                emailabel.Text = dr["email"].ToString();
                comm.CommandText = "select * from caretaker where ct_id = '" + reg.ToString() + "'";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "caretaker");
                dt = ds.Tables["caretaker"];
                dr = dt.Rows[i];
                namelabel.Text = dr["ct_name"].ToString();
                hostelabel.Text = dr["hostel_id"].ToString();
                shiftlabel.Text = dr["shift_timings"].ToString();
                conn.Close();

            }
            catch (Exception e1)
            {
            }
        }

        private void C1_Load(object sender, EventArgs e)
        {


        }

        private void detailsicon_Click(object sender, EventArgs e)
        {
            CDetails frm = new CDetails(reg);
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }

        private void issuesicon_Click(object sender, EventArgs e)
        {
            CIssue frm = new CIssue(reg);
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to logout?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dr == DialogResult.OK)
            {
                LOGIN frm = new LOGIN();
                this.Hide();
                frm.ShowDialog();
                this.Close();
            }
        }
    }
}
