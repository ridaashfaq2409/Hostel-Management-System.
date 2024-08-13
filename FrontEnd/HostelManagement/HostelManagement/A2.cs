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
    public partial class ChangeAdmin : Form
    {
        OracleDataAdapter da;
        DataSet ds;
        DataTable dt;
        DataRow dr;
        int i = 0;
        string reg;
        string newid=string.Empty, oldid=string.Empty;
        string newtype = string.Empty;
        string oldtype=string.Empty;
        public ChangeAdmin(string regno)
        {
            InitializeComponent();
            reg = regno;
            reglabel.Text = reg.ToString();
            requestLB.ScrollAlwaysVisible = true;
            string ConStr = "DATA SOURCE=DESKTOP-FE4CR37:1521/XE;USER ID=SYSTEM;Password=rampage";
            OracleConnection conn = new OracleConnection(ConStr);
            try
            {
                conn.Open();
                OracleCommand comm = new OracleCommand("", conn);
                comm.CommandText = "select * from administrator where admin_id = '" + reg.ToString() + "'";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "administrator");
                dt = ds.Tables["administrator"];
                dr = dt.Rows[i];
                namelabel.Text = dr["name"].ToString();
                comm.CommandText = "select * from room_change";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "room_change");
                dt = ds.Tables["room_change"];
                int n = dt.Rows.Count;
                if (n == 0)
                {
                    requestLB.Items.Add("No new room change applications...");
                }
                for (int j = 0; j < n; j++)
                {
                    dr = dt.Rows[j];
                    string student_reg = dr["reg_no"].ToString();
                    string oldid = dr["old_hostel_id"].ToString();
                    string oldtype = dr["old_hostel_type"].ToString();
                    string newid = dr["new_hostel_id"].ToString();
                    string newtype = dr["new_hostel_type"].ToString();
                    string reason = dr["reason"].ToString();
                    requestLB.Items.Add(student_reg + "\t" + oldid + "-" + oldtype + "  \t  " + newid + "-" + newtype+"\t\t"+reason);
                }
                conn.Close();

            }
            catch (Exception e1)
            {
            }
        }

        private void profileicon_Click(object sender, EventArgs e)
        {
            ProfileAdmin frm = new ProfileAdmin(reg);
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }

        private void messchangeicon_Click(object sender, EventArgs e)
        {
            MessAdmin frm = new MessAdmin(reg);
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }

        private void ChangeAdmin_Load(object sender, EventArgs e)
        {

        }

        private void detailsicon_Click(object sender, EventArgs e)
        {
            Stats frm = new Stats(reg);
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }

        private void requestLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void apply_Click(object sender, EventArgs e)
        {
            if (requestLB.SelectedIndex <= -1)
            {
                approve.Visible = true;
            }
            else
            {
                approve.Visible = false;
            }
            if (!approve.Visible)
            {
                MessageBox.Show("Approved hostel change request!", "Successfully Approved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string ConStr = "DATA SOURCE=DESKTOP-FE4CR37:1521/XE;USER ID=SYSTEM;Password=rampage";
                OracleConnection conn = new OracleConnection(ConStr);
                conn.Open();
                OracleCommand comm = new OracleCommand("", conn);
                OracleTransaction txn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    string regno = requestLB.SelectedItem.ToString().Substring(0, 8);
                    comm.CommandText = "select * from room_change where reg_no = '" + regno + "'";
                    comm.CommandType = CommandType.Text;
                    ds = new DataSet();
                    da = new OracleDataAdapter(comm.CommandText, conn);
                    da.Fill(ds, "room_change");
                    dt = ds.Tables["room_change"];
                    dr = dt.Rows[0];
                    string old_id = dr["old_hostel_id"].ToString();
                    string new_id = dr["new_hostel_id"].ToString();
                    string oldtype = dr["old_hostel_type"].ToString();
                    string newtype = dr["new_hostel_type"].ToString();
                    comm.CommandText = "select * from b"+old_id+" where reg_no = '" + regno + "'";
                    comm.CommandType = CommandType.Text;
                    ds = new DataSet();
                    da = new OracleDataAdapter(comm.CommandText, conn);
                    da.Fill(ds, "b"+old_id);
                    dt = ds.Tables["b"+old_id];
                    dr = dt.Rows[0];
                    string sno_old = dr["sno"].ToString();
                    string contact = dr["contact"].ToString();
                    string name = dr["name"].ToString();
                    comm.CommandText = "select min(sno),min(room_number) from b" + new_id + " where room_id='" + newtype + "' and reg_no='NULL' group by room_id";
                    comm.CommandType = CommandType.Text;
                    ds = new DataSet();
                    da = new OracleDataAdapter(comm.CommandText, conn);
                    da.Fill(ds, "b" + requestLB.SelectedItem.ToString());
                    dt = ds.Tables["b" + requestLB.SelectedItem.ToString()];
                    dr = dt.Rows[0];
                    string sno_new = dr["min(sno)"].ToString();
                    string roomno = dr["min(room_number)"].ToString();
                    comm.CommandText = "update b" + new_id + " set reg_no='"+regno+"', name='"+name+"',contact='"+contact+"' where sno='" + sno_new + "'";
                    comm.CommandType = CommandType.Text;
                    comm.ExecuteNonQuery();
                    comm.CommandText = "update student set hostel_id='"+new_id+"' where registration_number='"+regno+"'";
                    comm.CommandType = CommandType.Text;
                    comm.ExecuteNonQuery();
                    comm.CommandText = "delete from room_change where reg_no='" + regno + "'";
                    comm.CommandType = CommandType.Text;
                    comm.ExecuteNonQuery();
                    comm.CommandText = "update b" + old_id + " set reg_no='NULL', name='NULL',contact='NULL' where sno='" + sno_old + "'";
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
                    long.TryParse(mail_id,out mail);
                    mail++;
                    string application = "Room Change Application";
                    comm.CommandText = "insert into mail values('"+regno+"','"+mail.ToString()+"','"+application+"','APPROVED')";
                    comm.CommandType = CommandType.Text;
                    comm.ExecuteNonQuery();
                    txn.Commit();
                    requestLB.Items.RemoveAt(requestLB.SelectedIndex);
                }
                catch (Exception e1)
                {
                    txn.Rollback();
                    MessageBox.Show(e1.ToString(), "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (requestLB.SelectedIndex <= -1)
            {
                reject.Visible = true;
            }
            else
            {
                reject.Visible = false;
            }
            if (!reject.Visible)
            {
                MessageBox.Show("Rejected hostel change request!", "Successfully Rejected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string ConStr = "DATA SOURCE=DESKTOP-FE4CR37:1521/XE;USER ID=SYSTEM;Password=rampage";
                OracleConnection conn = new OracleConnection(ConStr);
                conn.Open();
                OracleCommand comm = new OracleCommand("", conn);
                OracleTransaction txn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    string regno = requestLB.SelectedItem.ToString().Substring(0, 8);
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
                    string application = "Room Change Application";
                    comm.CommandText = "insert into mail values('" + regno + "','" + mail.ToString() + "','" + application + "','REJECTED')";
                    comm.CommandType = CommandType.Text;
                    comm.ExecuteNonQuery();
                    comm.CommandText = "delete from room_change where reg_no='" + regno + "'";
                    comm.CommandType = CommandType.Text;
                    comm.ExecuteNonQuery();
                    txn.Commit();
                    requestLB.Items.RemoveAt(requestLB.SelectedIndex);
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
