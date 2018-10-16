using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 餛飩訂購
{
    public partial class 訂購系統登入頁面 : Form
    {
        public 訂購系統登入頁面()
        {
            InitializeComponent();
        }

        private void 訂購系統登入頁面_Load(object sender, EventArgs e)
        {
          
        }

        private void btn商品訂購_Click(object sender, EventArgs e)
        {
            btn加 form = new btn加();
            
            form.ShowDialog();//FORM2完成關閉之後才能再點
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form = new Form4();
          
            form.ShowDialog();
        }
    }
}
