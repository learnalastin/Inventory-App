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
    public partial class OutgoingOrders : Form
    {

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-2RD2LT8;Initial Catalog=default_db;Integrated Security=True");

        public OutgoingOrders()
        {
            InitializeComponent();
        }

        private void OutgoingOrders_Load(object sender, EventArgs e)
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
            cmd.CommandText = " select * from Orders";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //insert
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            //int i, j;
          //  int check1 = 0;
           

            bool isString = char.TryParse(textBox1.Text,out char a);
            bool isNumeric = Int32.TryParse(textBox2.Text, out int j);
            bool isNumeric1 = Int32.TryParse(textBox3.Text, out int i);



            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "select ProductID from Products where Products.ProductID = "+textBox2.Text+" ";
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            cmd2.ExecuteNonQuery();
            da2.Fill(dt2);

            if (dt2.Rows.Count != 0)
            {

                // doesnt complete correct check -- rework required
                if (isString == isNumeric && isNumeric1 == true)
                {

                    cmd.CommandText = "insert into Orders(Title, ProductID, NumberShipped) values('" + textBox1.Text + "'," + textBox2.Text + "," + textBox3.Text + ")";
                    cmd.ExecuteNonQuery();
                    Display();

                    MessageBox.Show("record inserted successfully");
                }
                else
                {
                    MessageBox.Show("Incorrect type");
                }


            }
            else
            {
                MessageBox.Show("ProductID doesn't exist");

            }





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
                cmd.CommandText = "delete from Orders where OrderID=" + id + "";

                cmd.ExecuteNonQuery();
                Display();
                MessageBox.Show("Purchase removed");
            }
        }
    }
}
