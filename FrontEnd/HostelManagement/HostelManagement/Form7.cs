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
    public partial class Change : Form
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
        string password;
        string room_type;
        public Change(long rego)
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
                int t = dt.Rows.Count;
                gender = dr["gender"].ToString();
                cg = dr["cgpa"].ToString();
                branch = dr["branch"].ToString();
                sem = dr["semester"].ToString();
                namelabel.Text = dr["name"].ToString();
                hostel = dr["hostel_id"].ToString();
                reglabel.Text = reg.ToString();
                blocklabel.Text = hostel;
                comm.CommandText = "select * from usertype where reg_no = '" + reg.ToString() + "'";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "usertype");
                dt = ds.Tables["usertype"];
                dr = dt.Rows[i];
                password = dr["password"].ToString();
                comm.CommandText = "select * from b"+blocklabel.Text+" where reg_no = '" + reg.ToString() + "'";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "b" + blocklabel.Text);
                dt = ds.Tables["b" + blocklabel.Text];
                dr = dt.Rows[i];
                room_type = dr["room_id"].ToString();
                typelabel.Text = room_type;
                comm.CommandText = "select * from hostel where hostel_id like '"+blocklabel.Text[0]+"%'";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "hostel");
                dt = ds.Tables["hostel"];
                int n = dt.Rows.Count;
                for (i = 0; i < n; i++)
                {
                    dr = dt.Rows[i];
                    hostelCB.Items.Add(dr["hostel_id"].ToString());
                }
                i = 0;
                comm.CommandText = "select room_id from hostel_rooms group by room_id";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "hostel_rooms");
                dt = ds.Tables["hostel_rooms"];
                int n1 = dt.Rows.Count;
                for (i = 0; i < n1; i++)
                {
                    dr = dt.Rows[i];
                    roomCB.Items.Add(dr["room_id"].ToString());
                }
                i = 0;
                conn.Close();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void namelabel_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void messlabel_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void blocklabel_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void reglabel_Click(object sender, EventArgs e)
        {

        }

        private void apply_Click(object sender, EventArgs e)
        {
            
            if (hostelCB.SelectedIndex <= -1)
            {
                invalidhostel.Visible = true;
            }
            else
            {
                invalidhostel.Visible = false;
            }
            if (roomCB.SelectedIndex <= -1)
            {
                invalidroom.Visible = true;
            }
            else
            {
                invalidroom.Visible = false;
            }
            if (reasonTB.Text == string.Empty)
            {
                invalidreason.Visible = true;
            }
            else
            {
                invalidreason.Visible = false;
            }
            if (!invalidreason.Visible && !invalidhostel.Visible)
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
            else
            {
                invalidpass.Visible = false;
            }
            if(!invalidroom.Visible && !invalidhostel.Visible){
                if (roomCB.SelectedItem.ToString() == room_type && hostelCB.SelectedItem.ToString() == blocklabel.Text)
                {
                    MessageBox.Show("Cannot book the same type of room again!", "Not a valid option", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            
            if (!invalidpass.Visible && !invalidhostel.Visible && !invalidreason.Visible && passTB.Visible && !invalidroom.Visible)
            {
                DialogResult dr2 = MessageBox.Show("Applied for hostel change successfully!", "Request Submitted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (dr2 == DialogResult.OK)
                {
                    string ConStr = "DATA SOURCE=DESKTOP-FE4CR37:1521/XE;USER ID=SYSTEM;Password=rampage";
                    OracleConnection conn = new OracleConnection(ConStr);
                    conn.Open();
                    OracleCommand comm = new OracleCommand("", conn);
                    OracleTransaction txn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                    try
                    {
                        comm.CommandText = "insert into room_change values('"+reglabel.Text+"','"+blocklabel.Text+"','"+typelabel.Text+"','"+hostelCB.SelectedItem.ToString()+"','"+roomCB.SelectedItem.ToString()+"','"+reasonTB.Text+"')";
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
                            Profile frm1 = new Profile(reg);
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

        private void messchangeicon_Click(object sender, EventArgs e)
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
                    comm.CommandText = "select * from mess_change where reg_no='" + reglabel.Text + "'";
                    comm.CommandType = CommandType.Text;
                    ds = new DataSet();
                    da = new OracleDataAdapter(comm.CommandText, conn);
                    da.Fill(ds, "mess_change");
                    dt = ds.Tables["mess_change"];
                    int n = dt.Rows.Count;
                    if (n == 0)
                    {
                        Mess frm = new Mess(reg);
                        this.Hide();
                        frm.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Mess change application under approval process\n\nPlease contact your administrator for updates!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                DialogResult dr = MessageBox.Show("Mess not alloted yet\n\nBook a room first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
    }
}
