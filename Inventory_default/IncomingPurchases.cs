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
    public partial class IncomingPurchases : Form
    {

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-2RD2LT8;Initial Catalog=default_db;Integrated Security=True");

        public IncomingPurchases()
        {
            InitializeComponent();
        }

        private void IncomingPurchases_Load(object sender, EventArgs e)
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
            cmd.CommandText = " select * from Purchases";
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
                cmd.CommandText = "delete from Purchases where PurchasesID=" + id + "";

                cmd.ExecuteNonQuery();

                Display();
                MessageBox.Show("Purchase removed");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //insert
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            int i, j, k;
   

            bool isNumeric = Int32.TryParse(textBox1.Text, out i);
            bool isNumeric1 = Int32.TryParse(textBox1.Text, out j);
            bool isNumeric2 = Int32.TryParse(textBox1.Text, out k);



            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "select ProductID from Products where Products.ProductID = " + textBox2.Text + " ";
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            cmd2.ExecuteNonQuery();
            da2.Fill(dt2);

            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select SupplierID from Suppliers where Suppliers.SupplierID = " + textBox1.Text + " ";
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            cmd1.ExecuteNonQuery();
            da1.Fill(dt1);




            if (dt2.Rows.Count !=0 && dt1.Rows.Count !=0 )
            {
                if (isNumeric && isNumeric1 && isNumeric2 == true)
                {
                    cmd.CommandText = "insert into Purchases(SupplierID, ProductID, NumberRecieved) values(" + textBox1.Text + "," + textBox2.Text + "," + textBox3.Text + ")";

                    cmd.ExecuteNonQuery();
                    Display();
                    MessageBox.Show("Purchase Inserted");

                }
                else
                {
                    MessageBox.Show("Type incorrect");
                }
            }
            else
            {
                MessageBox.Show("Input incorrect");
            }
        }
    }
}
