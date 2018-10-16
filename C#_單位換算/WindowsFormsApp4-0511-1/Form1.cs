using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4_0511_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn公分轉英吋_Click(object sender, EventArgs e)
        {
            if (tb公分.Text != "")
            {
                try
                {
                    float myCM = 0.0f;
                    float myInch = 0.0f;
                    float myFeet = 0.0f;
                    float youInch = 0.0f;
                    //float myfeet=0.0f;作業
                    myCM = Convert.ToSingle(tb公分.Text);
                    myInch = myCM * 0.3937f;
                    myFeet = Convert.ToInt32(myInch) / 12;
                    tb英吋.Text = Convert.ToString(myInch);
                    tb呎.Text = Convert.ToString(myFeet);

                    youInch = myInch % 12;
                    //作業 1英呎(Feet)=12英吋
                    //tb英吋.Text = string.Format("{0:F2}", myInch);              
                    tb吋.Text = string.Format("{0:F0}",youInch);


                }//作業 1英呎(Feet)=12英吋,

                catch (Exception error)
                {
                    //MessageBox.Show(Convert.ToString(error));
                    MessageBox.Show("輸入格式有誤");
                }

            }
            else
            {
                MessageBox.Show("請輸入公分數值");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn坪數轉平方公尺_Click(object sender, EventArgs e)
        {
            if (tb坪數.Text != "")
            {
                try
                {
                    double myPing = 0.0;
                    double mySquareMeter = 0.0;
                    myPing = Convert.ToDouble(tb坪數.Text);
                    mySquareMeter = myPing * 3.3058;
                    tb平方公尺.Text = Convert.ToString(mySquareMeter);
                }
                catch (Exception error)
                {
                    MessageBox.Show("輸入格式有誤");
                }
            }
            else
            {
                MessageBox.Show("請輸入坪數數值");
            }
          
        }

        private void btn公斤轉磅_Click(object sender, EventArgs e)
        {
            if(tb公斤.Text!="")
            {
                double myKg = 0.0;
                double myPound = 0.0;
                myKg = Convert.ToDouble(tb公斤.Text);
                myPound = myKg * 2.2;
                tb磅.Text = Convert.ToString(myPound);                
            }else
            {
                MessageBox.Show("請輸入公斤數值");
            }
        }
    }
}
