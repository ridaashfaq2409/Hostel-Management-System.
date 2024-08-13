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
    public partial class Issues : Form
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

        public Issues(long rego)
        {
            InitializeComponent();
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
                cg = dr["cgpa"].ToString();
                sem = dr["semester"].ToString();
                gender = dr["gender"].ToString();
                branch = dr["branch"].ToString();
                hostel = dr["hostel_id"].ToString();
                blocklabel.Text = "Block "+hostel;
                reglabel.Text = reg.ToString();
                namelabel.Text = dr["name"].ToString();
                string mess = dr["mess_id"].ToString();
                comm.CommandText = "select * from mess where mess_id = '" + mess + "'";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "mess");
                dt = ds.Tables["mess"];
                dr = dt.Rows[i];
                messlabel.Text = dr["mess_name"].ToString();
                conn.Close();
            }
            catch (Exception e1)
            {
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form8_Load(object sender, EventArgs e)
        {

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
            else if(hostel.Length!=0){
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
                    comm.CommandText = "select * from mess_change where reg_no='" + reg.ToString() + "'";
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
        
        private void roomchangeicon_Click(object sender, EventArgs e)
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
                    comm.CommandText = "select * from room_change where reg_no='" + reg.ToString() + "'";
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

        private void hostelproblem_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void post_Click(object sender, EventArgs e)
        {
            if (typeCB.SelectedIndex <= -1)
            {
                invalidtype.Visible = true;
            }
            else
            {
                invalidtype.Visible = false;
            }
            if (problemCB.SelectedIndex <= -1)
            {
                invalidproblem.Visible = true;
            }
            else
            {
                invalidproblem.Visible = false;
            }
            if (descTB.Text.Length == 0)
            {
                invalidesc.Visible = true;
            }
            else
            {
                invalidesc.Visible = false;
            }
            if(!invalidesc.Visible && !invalidproblem.Visible && !invalidtype.Visible){
                DialogResult dr2 = MessageBox.Show("Thank you for your feedback!", "Complaint Posted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (dr2 == DialogResult.OK)
                {
                    string ConStr = "DATA SOURCE=DESKTOP-FE4CR37:1521/XE;USER ID=SYSTEM;Password=rampage";
                    OracleConnection conn = new OracleConnection(ConStr);
                    conn.Open();
                    OracleCommand comm = new OracleCommand("", conn);
                    OracleTransaction txn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                    try
                    {
                        comm.CommandText = "select max(issue_id) from complaints";
                        comm.CommandType = CommandType.Text;
                        ds = new DataSet();
                        da = new OracleDataAdapter(comm.CommandText, conn);
                        da.Fill(ds, "complaints");
                        dt = ds.Tables["complaints"];
                        dr = dt.Rows[0];
                        string issue_id = dr["max(issue_id)"].ToString();
                        int id;
                        int.TryParse(issue_id,out id);
                        id++;
                        comm.CommandText = "insert into complaints values('" + reglabel.Text + "','"+id+"','" + typeCB.SelectedItem.ToString() + "','" + problemCB.SelectedItem.ToString() + "','PENDING')";
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
    }
}