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
    public partial class EditDetails : Form
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
        public EditDetails(long rego)
        {
            InitializeComponent();
            reg = rego;
            reglabel.Text = reg.ToString();
            try
            {
                string ConStr = "DATA SOURCE=DESKTOP-FE4CR37:1521/XE;USER ID=SYSTEM;Password=rampage";
                OracleConnection conn = new OracleConnection(ConStr);
                conn.Open();
                OracleCommand comm = new OracleCommand("", conn);
                comm.CommandText = "select * from usertype where reg_no = '" + reg.ToString()+"'";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "usertype");
                dt = ds.Tables["usertype"];
                dr = dt.Rows[i];
                phoneTB.Text = dr["phone"].ToString();
                mailTB.Text = dr["email"].ToString();
                comm.CommandText = "select * from student where registration_number = '" + reg.ToString()+"'";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "student");
                dt = ds.Tables["student"];
                dr = dt.Rows[i];
                namelabel.Text = dr["name"].ToString();
                cgpaTB.Text = dr["cgpa"].ToString();
                branchTB.Text = dr["branch"].ToString();
                semTB.Text = dr["semester"].ToString();
                cg = dr["cgpa"].ToString();
                sem = dr["semester"].ToString();
                gender = dr["gender"].ToString();
                branch = dr["branch"].ToString();
                hostel = dr["hostel_id"].ToString();
                conn.Close();
            }
            catch (Exception e1)
            {
            }
        }

        private void gender_Load(object sender, EventArgs e)
        {

        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            int sem = 0;
            int.TryParse(semTB.Text, out sem);
            string ConStr = "DATA SOURCE=DESKTOP-FE4CR37:1521/XE;USER ID=SYSTEM;Password=rampage";
            OracleConnection con = new OracleConnection(ConStr);
            if (!mailTB.Text.Contains("@gmail.com") && !mailTB.Text.Contains("@yahoo.com") && !mailTB.Text.Contains("@outlook.com") && !mailTB.Text.Contains("@hotmail.com") && !mailTB.Text.Contains("@icloud.com"))
            {
                invalidmail.Visible = true;
            }
            else {
                invalidmail.Visible = false;
            }
            if (phoneTB.Text.Length != 10)
            {
                invalidphone.Visible = true;
            }
            else
            {
                invalidphone.Visible = false;
            }
            if (branchTB.Text == string.Empty)
            {
                invalidbranch.Visible = true;
            }
            else {
                invalidbranch.Visible = false;
            }
            if (semTB.Text == string.Empty || sem>8 || sem<1)
            {
                invalidsem.Visible = true;
            }
            else {
                invalidsem.Visible = false;
            }
            if (genderCB.SelectedIndex <= -1)
            {
                invalidgender.Visible = true;
            }
            else
            {
                invalidgender.Visible = false;
            }
            double cgp;
            double.TryParse(cgpaTB.Text,out cgp);
            if (cgpaTB.Text == string.Empty && cgp < 0 && cgp > 10)
            {
                invalidcg.Visible = true;
            }
            else
            {
                invalidcg.Visible = false;
            }
            if (!invalidcg.Visible && !invalidmail.Visible && !invalidphone.Visible && !invalidbranch.Visible && !invalidsem.Visible && !invalidgender.Visible) {
                try
                {
                    con.Open();
                    OracleCommand cmd = new OracleCommand("", con);
                    OracleTransaction txn = con.BeginTransaction(IsolationLevel.ReadCommitted);
                    try
                    {
                        cmd.CommandText = "update usertype set phone='" + phoneTB.Text + "', email='" + mailTB.Text + "' where reg_no='" + reg.ToString()+"'";
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        //txn.Commit();
                        float cg = 0;
                        float.TryParse(cgpaTB.Text,out cg);
                        cmd.CommandText = "update student set  cgpa=" + cg + ", branch='" + branchTB.Text + "',gender='"+ genderCB.SelectedItem.ToString()+"' where registration_number='" + reg.ToString()+"'";
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "update student set semester=" + sem + " where registration_number='" + reg.ToString()+"'";
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "fresherCG";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("sem",OracleDbType.Int32).Value = sem;
                        cmd.ExecuteNonQuery();
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
                            EditDetails frm = new EditDetails(reg);
                            this.Hide();
                            frm.ShowDialog();
                            this.Close();
                        }
                    }
                }
                catch (Exception e1)
                {
                    DialogResult dr = MessageBox.Show(e1.ToString(), "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (dr == DialogResult.OK)
                    {
                        EditDetails frm = new EditDetails(reg);
                        this.Hide();
                        frm.ShowDialog();
                        this.Close();
                    }
                }
            }
        }

        private void forgotpassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 fr = new Form3();
            this.Hide();
            fr.ShowDialog();
            this.Close();
        }

        private void mailicon_Click(object sender, EventArgs e)
        {
            Mail frm = new Mail(reg);
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }

        private void roombookicon_Click_1(object sender, EventArgs e)
        {
            if (cg != string.Empty && gender != string.Empty && branch != string.Empty && gender != string.Empty)
            {
                Booking frm = new Booking(reg);
                this.Hide();
                frm.ShowDialog();
                this.Close();
            }
            else
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
        }

        private void messchangeicon_Click(object sender, EventArgs e)
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

        private void profileicon_Click(object sender, EventArgs e)
        {
            Profile frm = new Profile(reg);
            this.Hide();
            frm.ShowDialog();
            this.Close();
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

        private void forgotpassword_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
