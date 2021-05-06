using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

using dataBase;
using jsonSerializer;
using jsonDeserialize;

namespace HouseHQ_server
{
    public partial class DB_view : Form
    {
        public SQLiteConnection con;
        public httpServer Http;

        public DB_view(httpServer http)
        {
            InitializeComponent();

            con = http.con;
            Http = http;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form2(Http).Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Http.db.getDB(con);
            dataGridView1.DataSource = Http.db.getDB(con);
        }
    }
}
