using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Inventory_default
{
    public partial class Users : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-2RD2LT8;Initial Catalog=default_db;Integrated Security=True");

        public Users()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, EventArgs e)
        {
            int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " select * from Login where LoginUsername='" + textBox2.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());
            if (i == 0)
            {





                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "insert into Login values(" + textBox2.Text + "," + textBox3.Text + ")";
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("User created");


                    textBox2.Text = ""; textBox3.Text = "";
                    Display();
   
            }
            else
            {
                MessageBox.Show("Username already exists");
            }
        }

        private void Users_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            Display();
        }

        public void Display()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " select * from Login";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            if (id == 0)
            {
                MessageBox.Show("Empty");

            }
            else
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Login where LoginID=" + id + "";

                cmd.ExecuteNonQuery();
                MessageBox.Show("User deleted");
                Display();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
