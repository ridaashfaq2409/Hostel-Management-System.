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
    public partial class Mess : Form
    {
        OracleDataAdapter da;
        DataSet ds;
        DataTable dt;
        DataRow dr;
        int i = 0;
        long reg;
        string gender = string.Empty;
        string cg = string.Empty;
        string branch = string.Empty;
        string sem = string.Empty;
        string hostel;
        int oldmess, newmess;
        string password;

        public Mess(long rego)
        {
            InitializeComponent();
            passTB.PasswordChar = '*';
            reg = rego;
            try
            {
                string ConStr = "DATA SOURCE=DESKTOP-FE4CR37:1521/XE;USER ID=SYSTEM;Password=rampage";
                OracleConnection conn = new OracleConnection(ConStr);
                conn.Open();
                OracleCommand comm = new OracleCommand("", conn);
                comm.CommandText = "select * from student where registration_number = '" + reg.ToString()+"'";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "student");
                dt = ds.Tables["student"];
                dr = dt.Rows[i];
                reglabel.Text = reg.ToString();
                namelabel.Text = dr["name"].ToString();
                gender = dr["gender"].ToString();
                cg = dr["cgpa"].ToString();
                branch = dr["branch"].ToString();
                sem = dr["semester"].ToString();
                hostel = dr["hostel_id"].ToString();
                blocklabel.Text = dr["hostel_id"].ToString();
                string mess = dr["mess_id"].ToString();
                comm.CommandText = "select * from mess where mess_id = '" + mess + "'";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "mess");
                dt = ds.Tables["mess"];
                dr = dt.Rows[i];
                messlabel.Text = dr["mess_name"].ToString();
                comm.CommandText = "select * from usertype where reg_no = '" + reg.ToString() + "'";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "usertype");
                dt = ds.Tables["usertype"];
                dr = dt.Rows[i];
                password = dr["password"].ToString();
                comm.CommandText = "select * from mess where mess_id <> '" + mess + "' and mess_id<>-999 ";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "mess");
                dt = ds.Tables["mess"];
                int n = dt.Rows.Count;
                for (i = 0; i < n; i++ )
                {
                    dr = dt.Rows[i];
                    messCB.Items.Add(dr["mess_name"].ToString());
                }
                i = 0;
                conn.Close();
            }
            catch (Exception e1)
            {
            }
        }

        private void Mess_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (messCB.SelectedIndex<=-1)
            {
                invalidmess.Visible = true;
            }
            else
            {
                invalidmess.Visible = false;
            }
            if (reasonTB.Text == string.Empty)
            {
                invalidreason.Visible = true;
            }
            else
            {
                invalidreason.Visible = false;
            }
            if (!invalidreason.Visible && !invalidmess.Visible)
            {

                if (!passTB.Visible)
                {
                    passenter.Visible = true;
                    passTB.Visible = true;
                    return;
                }
            }
            if (passTB.Text != password && passTB.Visible)
            {
                invalidpass.Visible = true;
            }
            else{
                invalidpass.Visible = false;
            }
            if(!invalidpass.Visible && !invalidmess.Visible && !invalidreason.Visible && passTB.Visible)
            {
                DialogResult dr2 = MessageBox.Show("Applied for mess change successfully!", "Request Submitted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (dr2 == DialogResult.OK)
                {
                    string ConStr = "DATA SOURCE=DESKTOP-FE4CR37:1521/XE;USER ID=SYSTEM;Password=rampage";
                    OracleConnection conn = new OracleConnection(ConStr);
                    conn.Open();
                    OracleCommand comm = new OracleCommand("", conn);
                    OracleTransaction txn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                    try
                    {
                        comm.CommandText = "insert into mess_change values('" + reglabel.Text + "','" + messlabel.Text + "','" + messCB.SelectedItem.ToString() + "')";
                        comm.CommandType = CommandType.Text;
                        comm.ExecuteNonQuery();
                        txn.Commit();
                        Profile frm = new Profile(reg);
                        this.Hide();
                        frm.ShowDialog();
                        this.Close();
                    }
                    catch (Exception e1)
                    {
                        txn.Rollback();
                        DialogResult dr = MessageBox.Show(e1.ToString(), "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (dr == DialogResult.OK)
                        {
                            EditDetails frm1 = new EditDetails(reg);
                            this.Hide();
                            frm1.ShowDialog();
                            this.Close();
                        }
                    }
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

        private void mailicon_Click(object sender, EventArgs e)
        {
            Mail frm = new Mail(reg);
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }

        private void roombookicon_Click(object sender, EventArgs e)
        {
            if (cg == string.Empty || sem == string.Empty || branch == string.Empty || gender == string.Empty)
            {
                DialogResult dr = MessageBox.Show("Details not updated yet!\n\nUpdate details first to proceed for room booking", "Edit Details First", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (dr == DialogResult.OK)
                {
                    EditDetails frm = new EditDetails(reg);
                    this.Hide();
                    frm.ShowDialog();
                    this.Close();
                }
            }
            else if (hostel.Length != 0)
            {
                MessageBox.Show("Already booked a room!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {
                Booking frm = new Booking(reg);
                this.Hide();
                frm.ShowDialog();
                this.Close();
            }
        }

        private void roomchangeicon_Click_1(object sender, EventArgs e)
        {
            if (hostel.Length != 0)
            {
                string ConStr = "DATA SOURCE=DESKTOP-FE4CR37:1521/XE;USER ID=SYSTEM;Password=rampage";
                OracleConnection conn = new OracleConnection(ConStr);
                conn.Open();
                OracleCommand comm = new OracleCommand("", conn);
                OracleTransaction txn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    comm.CommandText = "select * from room_change where reg_no='" + reglabel.Text + "'";
                    comm.CommandType = CommandType.Text;
                    ds = new DataSet();
                    da = new OracleDataAdapter(comm.CommandText, conn);
                    da.Fill(ds, "room_change");
                    dt = ds.Tables["room_change"];
                    int n = dt.Rows.Count;
                    if (n == 0)
                    {
                        Change frm = new Change(reg);
                        this.Hide();
                        frm.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Room change application under approval process\n\nPlease contact your administrator for updates!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                catch (Exception e1)
                {
                    txn.Rollback();
                    DialogResult dr = MessageBox.Show(e1.ToString(), "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (dr == DialogResult.OK)
                    {
                        EditDetails frm1 = new EditDetails(reg);
                        this.Hide();
                        frm1.ShowDialog();
                        this.Close();
                    }
                }
            }
            else
            {
                DialogResult dr = MessageBox.Show("Room not alloted yet\n\nBook a room first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                if (dr == DialogResult.OK)
                {
                    Booking frm1 = new Booking(reg);
                    this.Hide();
                    frm1.ShowDialog();
                    this.Close();
                }
            }
        }

        private void profileicon_Click(object sender, EventArgs e)
        {
            Profile frm = new Profile(reg);
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }

        private void issuesicon_Click(object sender, EventArgs e)
        {
            if (hostel.Length != 0)
            {
                Issues frm = new Issues(reg);
                this.Hide();
                frm.ShowDialog();
                this.Close();
            }
            else
            {
                DialogResult dr = MessageBox.Show("Room not alloted yet\n\nBook a room first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                if (dr == DialogResult.OK)
                {
                    Booking frm1 = new Booking(reg);
                    this.Hide();
                    frm1.ShowDialog();
                    this.Close();
                }
            }
        }
    }
}
