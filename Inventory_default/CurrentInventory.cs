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
    public partial class CurrentInventory : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-2RD2LT8;Initial Catalog=default_db;Integrated Security=True");

        public CurrentInventory()
        {
            InitializeComponent();
        }

        private void CurrentInventory_Load(object sender, EventArgs e)
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
            cmd.CommandText = " select ProductID,ProductName,ProductManufacturer,InventoryOnHand from Products";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = " select OrderID,ProductID,NumberShipped from Orders";
            cmd1.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(dt1);
            dataGridView2.DataSource = dt1;

            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = " select Purchases.NumberRecieved, Suppliers.SupplierCity," +
                "Suppliers.SupplierName, Products.ProductName from Purchases, " +
                "Suppliers, Products where Purchases.SupplierID = Suppliers.SupplierID " +
                "and Purchases.ProductID = Products.ProductID";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);
            dataGridView3.DataSource = dt2;
            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
