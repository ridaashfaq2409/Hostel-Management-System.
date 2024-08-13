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
    public partial class CDetails : Form
    {
        OracleDataAdapter da;
        DataSet ds;
        DataTable dt;
        DataRow dr;
        int i = 0;
        string name;
        string reg;

        public CDetails(string regno)
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
                comm.CommandText = "select * from caretaker where ct_id = '" + reg.ToString() + "'";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "caretaker");
                dt = ds.Tables["caretaker"];
                dr = dt.Rows[i];
                namelabel.Text = dr["ct_name"].ToString();
                name = namelabel.Text;
                comm.CommandText = "select * from student where hostel_id is not NULL and name<>'NULL'";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "student");
                dt = ds.Tables["student"];
                int n = dt.Rows.Count;
                if (n == 0)
                {
                    detailsLB.Items.Add("\tNo details available...");
                }
                for (int j = 0; j < n; j++)
                {
                    dr = dt.Rows[j];
                    string ct_reg = dr["registration_number"].ToString();
                    string ct_name = dr["name"].ToString();
                    string hostel = dr["hostel_id"].ToString();
                    string branch = dr["branch"].ToString();
                    detailsLB.Items.Add(ct_reg + "  \t " + ct_name + "\t   " + hostel + "\t   " + branch);
                }
                conn.Close();

            }
            catch (Exception e1)
            {
            }
        }

        private void C3_Load(object sender, EventArgs e)
        {

        }

        private void issuesicon_Click(object sender, EventArgs e)
        {
            CIssue frm = new CIssue(reg);
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }

        private void profileicon_Click(object sender, EventArgs e)
        {
            ProfileCaretaker frm = new ProfileCaretaker(reg);
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

        private void searchbutton_Click(object sender, EventArgs e)
        {
                while (detailsLB.Items.Count != 0)
                {
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
                        comm.CommandText = "select * from student where registration_number='" + searchTB.Text + "'";
                        comm.CommandType = CommandType.Text;
                        ds = new DataSet();
                        da = new OracleDataAdapter(comm.CommandText, conn);
                        da.Fill(ds, "student");
                        dt = ds.Tables["student"];
                        int n = dt.Rows.Count;
                        if (n == 0)
                        {
                            detailsLB.Items.Add("\tNo details available...");
                        }
                        for (int j = 0; j < n; j++)
                        {
                            dr = dt.Rows[j];
                            string sreg = dr["registration_number"].ToString();
                            string name = dr["name"].ToString();
                            string hostel = dr["hostel_id"].ToString();
                            string branch = dr["branch"].ToString();
                            detailsLB.Items.Add(sreg + "  \t " + name + "\t   " + hostel + "\t   " + branch);
                        }
                        
                        conn.Close();
                    }
                    else if (searchTB.Text.Length == 0 && hostelCB.SelectedIndex >= 0)
                    {
                        comm.CommandText = "select * from student where hostel_id = '" + hostelCB.SelectedItem.ToString() + "'";
                        comm.CommandType = CommandType.Text;
                        ds = new DataSet();
                        da = new OracleDataAdapter(comm.CommandText, conn);
                        da.Fill(ds, "student");
                        dt = ds.Tables["student"];
                        int n = dt.Rows.Count;
                        if (n == 0)
                        {
                            detailsLB.Items.Add("\tNo details available...");
                        }
                        for (int j = 0; j < n; j++)
                        {
                            dr = dt.Rows[j];
                            string sreg = dr["registration_number"].ToString();
                            string name = dr["name"].ToString();
                            string hostel = dr["hostel_id"].ToString();
                            string branch = dr["branch"].ToString();
                            detailsLB.Items.Add(sreg + "  \t " + name + "\t   " + hostel + "\t   " + branch);
                        }
                        conn.Close();
                    }
                    else if (searchTB.Text.Length != 0 && hostelCB.SelectedIndex <= -1)
                    {
                        comm.CommandText = "select * from student where registration_number='" + searchTB.Text + "'";
                        comm.CommandType = CommandType.Text;
                        ds = new DataSet();
                        da = new OracleDataAdapter(comm.CommandText, conn);
                        da.Fill(ds, "student");
                        dt = ds.Tables["student"];
                        int n = dt.Rows.Count;
                        if (n == 0)
                        {
                            detailsLB.Items.Add("\tNo details available...");
                        }
                        for (int j = 0; j < n; j++)
                        {
                            dr = dt.Rows[j];
                            string sreg = dr["registration_number"].ToString();
                            string name = dr["name"].ToString();
                            string hostel = dr["hostel_id"].ToString();
                            string branch = dr["branch"].ToString();
                            detailsLB.Items.Add(sreg + "  \t " + name + "\t   " + hostel + "\t   " + branch);
                        }

                        conn.Close();
                    }
                    else
                    {
                        comm.CommandText = "select * from student";
                        comm.CommandType = CommandType.Text;
                        ds = new DataSet();
                        da = new OracleDataAdapter(comm.CommandText, conn);
                        da.Fill(ds, "student");
                        dt = ds.Tables["student"];
                        int n = dt.Rows.Count;
                        if (n == 0)
                        {
                            detailsLB.Items.Add("\tNo details available...");
                        }
                        for (int j = 0; j < n; j++)
                        {
                            dr = dt.Rows[j];
                            string sreg = dr["registration_number"].ToString();
                            string name = dr["name"].ToString();
                            string hostel = dr["hostel_id"].ToString();
                            string branch = dr["branch"].ToString();
                            detailsLB.Items.Add(sreg + "  \t " + name + "\t   " + hostel + "\t   " + branch);
                        }
                        conn.Close();
                    }

                }
                catch (Exception e1)
                {
                }
            }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
