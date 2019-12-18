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
    public partial class Suppliers : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-2RD2LT8;Initial Catalog=default_db;Integrated Security=True");

        public Suppliers()
        {
            InitializeComponent();
        }

        private void Suppliers_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            fill_dg();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;

            int i;

            bool isNumeric = Int32.TryParse(textBox1.Text, out i);
            bool isString = Boolean.TryParse(textBox2.Text, out bool k);
            bool isString1 = Boolean.TryParse(textBox4.Text, out bool l);
            
      

          
            if (isNumeric == isString == isString1 == true)
            {

                int check = 0;

                cmd1.CommandText = " select * from Suppliers where SupplierID=" +
                    "'" + textBox1.Text + "'";
                cmd1.ExecuteNonQuery();

                DataTable dt1 = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(dt1);
                check = Convert.ToInt32(dt1.Rows.Count.ToString());

                if (check == 0)
                {
                    cmd1.CommandText = "insert into Suppliers values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox4.Text + "')";
                    cmd1.ExecuteNonQuery();

                    textBox1.Text = ""; textBox2.Text = ""; textBox4.Text = "";

                    fill_dg();

                    MessageBox.Show("Supplier added");
                }
                else
                {
                    MessageBox.Show("Supplier already exists ");

                }
            }
            else
            {
                MessageBox.Show("Incorrect input type retry ");
            }
        


        }

        public void fill_dg()
        {

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Suppliers";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
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
                cmd.CommandText = "delete from Suppliers where SupplierID=" + id + "";

                cmd.ExecuteNonQuery();
                MessageBox.Show("Supplier deleted");
                fill_dg();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
