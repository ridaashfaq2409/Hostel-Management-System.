using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;

namespace HostelManagement
{
    public partial class CIssue : Form
    {
        OracleDataAdapter da;
        DataSet ds;
        DataTable dt;
        DataRow dr;
        int i = 0;
        string name;
        string reg;
        public CIssue(string regno)
        {
            InitializeComponent();
            reg = regno;
            reglabel.Text = reg.ToString();
            issueLB.ScrollAlwaysVisible = true;
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
                comm.CommandText = "select * from caretaker where ct_id = '" + reg.ToString() + "'";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "caretaker");
                dt = ds.Tables["caretaker"];
                dr = dt.Rows[i];
                namelabel.Text = dr["ct_name"].ToString();
                comm.CommandText = "select * from complaints where status='PENDING'";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "complaints");
                dt = ds.Tables["complaints"];
                int n = dt.Rows.Count;
                if (n == 0)
                {
                    issueLB.Items.Add("No new mess change applications...");
                }
                for (int j = 0; j < n; j++)
                {
                    dr = dt.Rows[j];
                    string sreg = dr["reg_no"].ToString();
                    string issueid = dr["issue_id"].ToString();
                    string itype = dr["issue_type"].ToString();
                    string prob = dr["problem"].ToString();
                    issueLB.Items.Add("       " + sreg + "  \t " + issueid + "\t   " + itype +"\t"+prob);
                }
                conn.Close();

            }
            catch (Exception e1)
            {
            }
        }

        private void C2_Load(object sender, EventArgs e)
        {

        }

        private void issuelabel_Click(object sender, EventArgs e)
        {

        }

        private void issuesicon_Click(object sender, EventArgs e)
        {

        }

        private void profileicon_Click(object sender, EventArgs e)
        {
            ProfileCaretaker frm = new ProfileCaretaker(reg);
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }

        private void detailsicon_Click(object sender, EventArgs e)
        {
            CDetails frm = new CDetails(reg);
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

        private void apply_Click(object sender, EventArgs e)
        {
            if (issueLB.SelectedIndex <= -1)
            {
                approve.Visible = true;
            }
            else
            {
                approve.Visible = false;
            }
            if (!approve.Visible)
            {
                MessageBox.Show("Issue Resolved!", "Successfully Resolved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string ConStr = "DATA SOURCE=DESKTOP-FE4CR37:1521/XE;USER ID=SYSTEM;Password=rampage";
                OracleConnection conn = new OracleConnection(ConStr);
                conn.Open();
                OracleCommand comm = new OracleCommand("", conn);
                OracleTransaction txn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    string id = issueLB.SelectedItem.ToString().Substring(19, 8);
                    string regno = issueLB.SelectedItem.ToString().Substring(7, 8);
                    comm.CommandText = "update complaints set status='RESOLVED' where issue_id='" + id + "'";
                    comm.CommandType = CommandType.Text;
                    comm.ExecuteNonQuery();
                    comm.CommandText = "select max(mail_code) from mail";
                    comm.CommandType = CommandType.Text;
                    ds = new DataSet();
                    da = new OracleDataAdapter(comm.CommandText, conn);
                    da.Fill(ds, "mail");
                    dt = ds.Tables["mail"];
                    dr = dt.Rows[0];
                    string mail_id = dr["max(mail_code)"].ToString();
                    long mail;
                    long.TryParse(mail_id, out mail);
                    mail++;
                    string application = "Query Resolution";
                    comm.CommandText = "insert into mail values('" + regno + "','" + mail.ToString() + "','" + application + "','RESOLVED')";
                    comm.CommandType = CommandType.Text;
                    comm.ExecuteNonQuery();
                    txn.Commit();
                    issueLB.Items.RemoveAt(issueLB.SelectedIndex);
                }
                catch (Exception e1)
                {
                    txn.Rollback();
                    MessageBox.Show(e1.ToString(), "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
