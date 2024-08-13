using System;
using System.Collections;
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
    public partial class Booking : Form
    {
        OracleDataAdapter da;
        DataSet ds;
        DataTable dt;
        DataRow dr;
        Dictionary<string, double> d20 = new Dictionary<string, double>();
        Dictionary<string, double> d21 = new Dictionary<string, double>();
        Dictionary<string, int> hostel_mess = new Dictionary<string, int>();
        int hostid=0;
        int i = 0;
        string phone;
        long reg;
        string block;
        public Booking(long regno)
        {
            
            InitializeComponent();
            hostel_mess.Add("10", 1);
            hostel_mess.Add("11", 2);
            hostel_mess.Add("12", 3);
            hostel_mess.Add("20", 4);
            hostel_mess.Add("21", 5);
            reg = regno;
            reglabel.Text = reg.ToString();
            string ConStr = "DATA SOURCE=DESKTOP-FE4CR37:1521/XE;USER ID=SYSTEM;Password=rampage";
            OracleConnection conn = new OracleConnection(ConStr);
            try
            {
                conn.Open();
                OracleCommand comm = new OracleCommand("", conn);
                comm.CommandText = "select * from student where registration_number = '" + reg.ToString()+"'";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "student");
                dt = ds.Tables["student"];
                dr = dt.Rows[i];
                namelabel.Text = dr["name"].ToString();
                cgpalabel.Text = dr["cgpa"].ToString();
                genderlabel.Text = dr["gender"].ToString();
                block = dr["hostel_id"].ToString();
                int.TryParse(dr["hostel_id"].ToString(),out hostid);
                comm.CommandText = "select * from usertype where reg_no = '" + reg.ToString() + "'";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "usertype");
                dt = ds.Tables["usertype"];
                dr = dt.Rows[i];
                phone = dr["phone"].ToString();
                if (genderlabel.Text == "MALE")
                {
                    MblockCB.Visible = true;
                    bookbuttonM.Visible = true;
                    malecriteria.Visible = true;
                }
                else 
                {
                    FblockCB.Visible = true;
                    bookuttonF.Visible = true;
                    femalecriteria.Visible = true;
                }
                conn.Close();

            }
            catch (Exception e1)
            {
            }
        }

        private void Booking_Load(object sender, EventArgs e)
        {

        }

        private void cgpalabel_Click(object sender, EventArgs e)
        {

        }

        private void bookbutton_Click(object sender, EventArgs e)
        {
            if (hostid != 0) {
                DialogResult dr2 = MessageBox.Show("Already booked a room!\n\nRedirecting to profile page","Error",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                if (dr2 == DialogResult.OK)
                {
                    Profile frm = new Profile(reg);
                    this.Hide();
                    frm.ShowDialog();
                    this.Close();
                }
                return;
            }
            if (MblockCB.SelectedIndex <= -1)
            {
                invalidblock.Visible = true;
            }
            else
            {
                invalidblock.Visible = false;
            }
            if (roomtypeCB.SelectedIndex <= -1)
            {
                invalidroom.Visible = true;
            }
            else
            {
                invalidroom.Visible = false;
            }
            if (!invalidblock.Visible && !invalidroom.Visible)
            {
                string ConStr = "DATA SOURCE=DESKTOP-FE4CR37:1521/XE;USER ID=SYSTEM;Password=rampage";
                OracleConnection conn = new OracleConnection(ConStr);
                try
                {
                    if (genderlabel.Text == "MALE")
                    {
                        conn.Open();
                        OracleCommand comm = new OracleCommand("", conn);
                        comm.CommandText = "select * from hostel_rooms where cgpa_needed <= '" + cgpalabel.Text + "' and room_id='" + roomtypeCB.SelectedItem.ToString() + "' and hostel_id='" + MblockCB.SelectedItem.ToString() + "'";
                        comm.CommandType = CommandType.Text;
                        ds = new DataSet();
                        da = new OracleDataAdapter(comm.CommandText, conn);
                        da.Fill(ds, "hostel_rooms");
                        dt = ds.Tables["hostel_rooms"];
                        int rcount = dt.Rows.Count;
                        if (rcount == 0)
                        {
                            MessageBox.Show("Not eligible for this room!\n\nCheck criteria before booking", "Booking Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            DialogResult dr0 = MessageBox.Show("Are you sure you want to book this room?", "Booking confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dr0 == DialogResult.Yes)
                            {
                                DialogResult dr1 = MessageBox.Show("You have successfully booked a room", "Booking confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (dr1 == DialogResult.OK)
                                {
                                    //UPDATE BLOCK TABLE
                                    OracleCommand cmd = new OracleCommand("", conn);
                                    OracleTransaction txn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                                    try
                                    {
                                        comm.CommandText = "select min(sno),min(room_number) from b" + MblockCB.SelectedItem.ToString() + " where room_id='" + roomtypeCB.SelectedItem.ToString() + "' and reg_no='NULL' group by room_id";
                                        comm.CommandType = CommandType.Text;
                                        ds = new DataSet();
                                        da = new OracleDataAdapter(comm.CommandText, conn);
                                        da.Fill(ds, "b" + MblockCB.SelectedItem.ToString());
                                        dt = ds.Tables["b" + MblockCB.SelectedItem.ToString()];
                                        dr = dt.Rows[0];
                                        string sno = dr["min(sno)"].ToString();
                                        string roomno = dr["min(room_number)"].ToString();
                                        cmd.CommandText = "update b" + MblockCB.SelectedItem.ToString() + " set reg_no='" + reglabel.Text + "', name='" + namelabel.Text + "',room_id='" + roomtypeCB.SelectedItem.ToString() + "',sno='" + sno + "',contact='" + phone + "',room_number='" + roomno + "' where sno='" + sno + "'";
                                        cmd.CommandType = CommandType.Text;
                                        cmd.ExecuteNonQuery();
                                        int mess_id = hostel_mess[MblockCB.SelectedItem.ToString()];
                                        cmd.CommandText = "update student set hostel_id='" + MblockCB.SelectedItem.ToString() + "',mess_id='" + mess_id + "' where registration_number='" + reg.ToString() + "'";
                                        cmd.CommandType = CommandType.Text;
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
                                            EditDetails frm1 = new EditDetails(reg);
                                            this.Hide();
                                            frm1.ShowDialog();
                                            this.Close();
                                        }
                                    }
                                }
                            }
                        }
                        conn.Close();
                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.ToString(), "Not Found");
                }
            }
        }

        private void bookuttonF_Click(object sender, EventArgs e)
        {
            if (hostid != 0)
            {
                DialogResult dr2 = MessageBox.Show("Already booked a room!\n\nRedirecting to profile page", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                if (dr2 == DialogResult.OK)
                {
                    Profile frm = new Profile(reg);
                    this.Hide();
                    frm.ShowDialog();
                    this.Close();
                }
                return;
            }
            d20.Add("01AC", 8.5);
            d20.Add("01NC", 7);
            d20.Add("02AC", 7);
            d20.Add("02NC", 6.5);
            d21.Add("01AC", 8);
            d21.Add("01NC", 8);
            d21.Add("02AC", 7.5);
            d21.Add("02NC", 7);
            if (FblockCB.SelectedIndex <= -1)
            {
                invalidblock.Visible = true;
            }
            else
            {
                invalidblock.Visible = false;
            }
            if (roomtypeCB.SelectedIndex <= -1)
            {
                invalidroom.Visible = true;
            }
            else
            {
                invalidroom.Visible = false;
            }
            if (!invalidblock.Visible && !invalidroom.Visible)
            {
                string ConStr = "DATA SOURCE=DESKTOP-FE4CR37:1521/XE;USER ID=SYSTEM;Password=rampage";
                OracleConnection conn = new OracleConnection(ConStr);
                if (genderlabel.Text == "FEMALE")
                {
                    conn.Open();
                    OracleCommand comm = new OracleCommand("", conn);
                    int blockno;
                    int.TryParse(FblockCB.SelectedItem.ToString(), out blockno);
                    float cg;
                    float.TryParse(cgpalabel.Text, out cg);
                    comm.CommandText = "select * from hostel_rooms where cgpa_needed<='" + cgpalabel.Text + "' and room_id='" + roomtypeCB.SelectedItem.ToString() + "'";
                    comm.CommandType = CommandType.Text;
                    ds = new DataSet();
                    da = new OracleDataAdapter(comm.CommandText, conn);
                    da.Fill(ds, "hostel_rooms");
                    dt = ds.Tables["hostel_rooms"];
                    int rcount = dt.Rows.Count;
                    if (blockno == 20)
                    {
                        if (d20[roomtypeCB.SelectedItem.ToString()] <= cg)
                        {
                            DialogResult dr0 = MessageBox.Show("Are you sure you want to book this room?", "Booking confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dr0 == DialogResult.Yes)
                            {
                                DialogResult dr1 = MessageBox.Show("You have successfully booked a room", "Booking confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (dr1 == DialogResult.OK)
                                {
                                    OracleCommand cmd = new OracleCommand("", conn);
                                    OracleTransaction txn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                                    try
                                    {
                                        comm.CommandText = "select min(sno),min(room_number) from b" + FblockCB.SelectedItem.ToString() + " where room_id='" + roomtypeCB.SelectedItem.ToString() + "' and reg_no='NULL' group by room_id";
                                        comm.CommandType = CommandType.Text;
                                        ds = new DataSet();
                                        da = new OracleDataAdapter(comm.CommandText, conn);
                                        da.Fill(ds, "b" + FblockCB.SelectedItem.ToString());
                                        dt = ds.Tables["b" + FblockCB.SelectedItem.ToString()];
                                        dr = dt.Rows[0];
                                        string sno = dr["min(sno)"].ToString();
                                        string roomno = dr["min(room_number)"].ToString();
                                        cmd.CommandText = "update b" + FblockCB.SelectedItem.ToString() + " set reg_no='" + reglabel.Text + "', name='" + namelabel.Text + "',room_id='" + roomtypeCB.SelectedItem.ToString() + "',sno='" + sno + "',contact='" + phone + "',room_number='" + roomno + "' where sno='" + sno + "'";
                                        cmd.CommandType = CommandType.Text;
                                        cmd.ExecuteNonQuery();
                                        int mess_id = hostel_mess[FblockCB.SelectedItem.ToString()];
                                        cmd.CommandText = "update student set hostel_id='" + FblockCB.SelectedItem.ToString() + "',mess_id='" + mess_id + "' where registration_number='" + reg.ToString() + "'";
                                        cmd.CommandType = CommandType.Text;
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
                                            EditDetails frm1 = new EditDetails(reg);
                                            this.Hide();
                                            frm1.ShowDialog();
                                            this.Close();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            DialogResult dr = MessageBox.Show("Not eligible for this room!\n\nCheck criteria before booking", "Booking Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (dr == DialogResult.OK)
                            {
                                Booking frm = new Booking(reg);
                                this.Hide();
                                frm.ShowDialog();
                                this.Close();
                            }
                        }
                    }
                    else if (blockno == 21)
                    {
                        if (d21[roomtypeCB.SelectedItem.ToString()] <= cg)
                        {
                            DialogResult dr0 = MessageBox.Show("Are you sure you want to book this room?", "Booking confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dr0 == DialogResult.Yes)
                            {
                                DialogResult dr1 = MessageBox.Show("You have successfully booked a room", "Booking confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (dr1 == DialogResult.OK)
                                {
                                    OracleCommand cmd = new OracleCommand("", conn);
                                    OracleTransaction txn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                                    try
                                    {
                                        comm.CommandText = "select min(sno),min(room_number) from b" + FblockCB.SelectedItem.ToString() + " where room_id='" + roomtypeCB.SelectedItem.ToString() + "' and reg_no='NULL' group by room_id";
                                        comm.CommandType = CommandType.Text;
                                        ds = new DataSet();
                                        da = new OracleDataAdapter(comm.CommandText, conn);
                                        da.Fill(ds, "b" + FblockCB.SelectedItem.ToString());
                                        dt = ds.Tables["b" + FblockCB.SelectedItem.ToString()];
                                        dr = dt.Rows[0];
                                        string sno = dr["min(sno)"].ToString();
                                        string roomno = dr["min(room_number)"].ToString();
                                        cmd.CommandText = "update b" + FblockCB.SelectedItem.ToString() + " set reg_no='" + reglabel.Text + "', name='" + namelabel.Text + "',room_id='" + roomtypeCB.SelectedItem.ToString() + "',sno='" + sno + "',contact='" + phone + "',room_number='" + roomno + "' where sno='" + sno + "'";
                                        cmd.CommandType = CommandType.Text;
                                        cmd.ExecuteNonQuery();
                                        int mess_id = hostel_mess[FblockCB.SelectedItem.ToString()];
                                        cmd.CommandText = "update student set hostel_id='" + FblockCB.SelectedItem.ToString() + "',mess_id='" + mess_id + "' where registration_number='" + reg.ToString() + "'";
                                        cmd.CommandType = CommandType.Text;
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
                                            EditDetails frm1 = new EditDetails(reg);
                                            this.Hide();
                                            frm1.ShowDialog();
                                            this.Close();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            DialogResult dr = MessageBox.Show("Not eligible for this room!", "Booking Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (dr == DialogResult.OK)
                            {
                                Booking frm = new Booking(reg);
                                this.Hide();
                                frm.ShowDialog();
                                this.Close();
                            }
                        }
                    }
                    conn.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void messchangeicon_Click(object sender, EventArgs e)
        {
            if (block.Length != 0)
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

        private void issuesicon_Click(object sender, EventArgs e)
        {
            if (block.Length != 0)
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

        private void roomchangeicon_Click(object sender, EventArgs e)
        {
            if (block.Length != 0)
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
    }
}

// For mess name, refer mess-id and hostel-id mapping