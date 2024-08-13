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
    public partial class Stats : Form
    {
        string reg;
        OracleDataAdapter da;
        DataSet ds;
        DataTable dt;
        DataRow dr;
        int i = 0;
        string name;
        public Stats(string regno)
        {
            InitializeComponent();
            detailsLB.ScrollAlwaysVisible = true;
            reg = regno;
            reglabel.Text = reg.ToString();
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
                name = namelabel.Text;
                comm.CommandText = "select * from caretaker where hostel_id is not NULL";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "caretaker");
                dt = ds.Tables["caretaker"];
                int n = dt.Rows.Count;
                if (n == 0)
                {
                    detailsLB.Items.Add("\tNo details available...");
                }
                for (int j = 0; j < n; j++)
                {
                    dr = dt.Rows[j];
                    string ct_reg = dr["ct_id"].ToString();
                    string ct_name = dr["ct_name"].ToString();
                    string hostel = dr["hostel_id"].ToString();
                    string shift = dr["shift_timings"].ToString();
                    detailsLB.Items.Add("\t" + ct_reg + "  \t " + ct_name + "\t   " + hostel+"\t   "+shift);
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

        private void roomchangeicon_Click(object sender, EventArgs e)
        {
            ChangeAdmin frm = new ChangeAdmin(reg);
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

        private void Stats_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void searchbutton_Click(object sender, EventArgs e)
        {
                while(detailsLB.Items.Count!=0){
                    detailsLB.Items.RemoveAt(detailsLB.Items.Count - 1);
                }
                string ConStr = "DATA SOURCE=DESKTOP-FE4CR37:1521/XE;USER ID=SYSTEM;Password=rampage";
                OracleConnection conn = new OracleConnection(ConStr);
                try
                {
                    conn.Open();
                    OracleCommand comm = new OracleCommand("", conn);
                    if (searchTB.Text.Length != 0)
                    {
                        comm.CommandText = "select * from caretaker where ct_id='" + searchTB.Text + "'";
                        comm.CommandType = CommandType.Text;
                        ds = new DataSet();
                        da = new OracleDataAdapter(comm.CommandText, conn);
                        da.Fill(ds, "caretaker");
                        dt = ds.Tables["caretaker"];
                        int n = dt.Rows.Count;
                        if (n == 0)
                        {
                            detailsLB.Items.Add("\tNo details available...");
                        }
                        for (int j = 0; j < n; j++)
                        {
                            dr = dt.Rows[j];
                            string ct_reg = dr["ct_id"].ToString();
                            string ct_name = dr["ct_name"].ToString();
                            string hostel = dr["hostel_id"].ToString();
                            string shift = dr["shift_timings"].ToString();
                            detailsLB.Items.Add("\t" + ct_reg + "  \t " + ct_name + "\t   " + hostel + "\t   " + shift);
                        }
                        conn.Close();
                    }
                    else if (searchTB.Text.Length == 0 && hostelCB.SelectedIndex >= 0)
                    {
                        comm.CommandText = "select * from caretaker where hostel_id = '" + hostelCB.SelectedItem.ToString() + "'";
                        comm.CommandType = CommandType.Text;
                        ds = new DataSet();
                        da = new OracleDataAdapter(comm.CommandText, conn);
                        da.Fill(ds, "caretaker");
                        dt = ds.Tables["caretaker"];
                        int n = dt.Rows.Count;
                        if (n == 0)
                        {
                            detailsLB.Items.Add("\tNo details available...");
                        }
                        for (int j = 0; j < n; j++)
                        {
                            dr = dt.Rows[j];
                            string ct_reg = dr["ct_id"].ToString();
                            string ct_name = dr["ct_name"].ToString();
                            string hostel = dr["hostel_id"].ToString();
                            string shift = dr["shift_timings"].ToString();
                            detailsLB.Items.Add("\t" + ct_reg + "  \t " + ct_name + "\t   " + hostel + "\t   " + shift);
                        }
                        conn.Close();
                    }
                    else
                    {
                        comm.CommandText = "select * from caretaker";
                        comm.CommandType = CommandType.Text;
                        ds = new DataSet();
                        da = new OracleDataAdapter(comm.CommandText, conn);
                        da.Fill(ds, "caretaker");
                        dt = ds.Tables["caretaker"];
                        int n = dt.Rows.Count;
                        if (n == 0)
                        {
                            detailsLB.Items.Add("\tNo details available...");
                        }
                        for (int j = 0; j < n; j++)
                        {
                            dr = dt.Rows[j];
                            string ct_reg = dr["ct_id"].ToString();
                            string ct_name = dr["ct_name"].ToString();
                            string hostel = dr["hostel_id"].ToString();
                            string shift = dr["shift_timings"].ToString();
                            detailsLB.Items.Add("\t" + ct_reg + "  \t " + ct_name + "\t   " + hostel + "\t   " + shift);
                        }
                        conn.Close();
                    }
                }
                catch (Exception e1)
                {
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
    }
}
