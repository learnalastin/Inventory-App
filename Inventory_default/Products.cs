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
    public partial class Products : Form
    {

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-2RD2LT8;Initial Catalog=default_db;Integrated Security=True");

        public Products()
        {
            InitializeComponent();
        }

        private void Products_Load(object sender, EventArgs e)
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
            cmd.CommandText = " select * from Products";
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

            int f,k,i,b;
       
            bool isString = Boolean.TryParse(textBox1.Text, out bool q);
            bool isString1 = Boolean.TryParse(textBox3.Text, out bool l);
            bool isNumeric = Int32.TryParse(textBox0.Text, out i);
            bool isNumeric1 = Int32.TryParse(textBox2.Text, out f);
            bool isNumeric2= Int32.TryParse(textBox4.Text, out k);
            bool isNumeric3 = Int32.TryParse(textBox5.Text, out b);


            if (isNumeric && isNumeric1 && isNumeric2 && isNumeric3 && isString && isString1 && true)
            {


                int check = 0;

                cmd.CommandText = " select * from Products where ProductID=" +
                    "'" + textBox0.Text + "'";
                cmd.ExecuteNonQuery();

                DataTable dt1 = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                check = Convert.ToInt32(dt1.Rows.Count.ToString());

                if (check == 0)
                {

         
                    int Now = Convert.ToInt32(textBox2.Text) - Convert.ToInt32(textBox4.Text);
                    textBox5.Text = Convert.ToString(Now);
                    cmd.CommandText = "insert into Products(ProductID,ProductName, ProductManufacturer, InventoryReceived,InventoryShipped,InventoryOnHand)" +
                        " values('" + textBox0.Text + "','" + textBox1.Text + "','" + textBox2.Text + "'," + textBox3.Text + "," + textBox4.Text + "," + textBox5.Text + ")";


                    cmd.ExecuteNonQuery();
                    Display();

                    MessageBox.Show("record inserted successfully");

                }
                else
                {
                    MessageBox.Show("Product already exists");
                }

            }
            else
            {
                MessageBox.Show("Incorrect types inserted");
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
                cmd.CommandText = "delete from Products where ProductID=" + id + "";

                cmd.ExecuteNonQuery();
                Display();
                MessageBox.Show("Product removed");
            }
        }
    }
}
