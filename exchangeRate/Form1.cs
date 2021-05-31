using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//conectSQL

using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;

namespace exchangeRate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        String connectionData = @"Data Source=.;Initial Catalog=exchange;Integrated Security=true";

        private void Form1_Load(object sender, EventArgs e)
        {
            DisplayDB();
            //Application.StartupPath
        }

        private void DisplayDB()
        {
            String query = "select * from exchangeTab";
            using (SqlConnection connection = new SqlConnection(connectionData))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "exchangeTab");
                dataGridView1.DataSource = ds.Tables[0];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double jj = Convert.ToDouble(textBox2.Text) * Convert.ToDouble(textBox4.Text);
            textBox5.Text = Convert.ToString(jj);
            string query = "Insert into exchangeTab Values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "',GETDATE())";
            using (SqlConnection connection = new SqlConnection(connectionData))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();

            }
            DisplayDB();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            DisplayDBS(textBox6.Text.Trim(), textBox7.Text.Trim(), textBox8.Text.Trim());
        }

        private void DisplayDBS(string searchText0, string searchText1, string searchText2)
        {
            String query = string.Format("select * from exchangeTab where day(Сана) like '{0}%' and month(Сана) like '{1}%' and year(Сана) like '{2}%'", searchText0, searchText1, searchText2);
            using (SqlConnection connection = new SqlConnection(connectionData))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "exchangeTab");
                dataGridView1.DataSource = ds.Tables[0];
            }
        }
    }
}
