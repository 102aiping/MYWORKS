using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp_作業3_血壓查詢系統_新_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TB收_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void TB舒_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btn送出資料_Click(object sender, EventArgs e)
        {
            string str訊息="";
            int int收 = 0;
            int int舒 = 0;
            
            if ((TB收.Text!="")&&(TB舒.Text!=""))
            {
                bool if收 = false;
                bool if舒 = false;

                if收 = Int32.TryParse(TB收.Text, out int收);
                if舒 = Int32.TryParse(TB舒.Text, out int舒);

                if (if收 && if舒)
                {
                    int收 = Convert.ToInt32(TB收.Text);
                    int舒 = Convert.ToInt32(TB舒.Text);
                    if ((int收>=int舒)&&((int收 <380) && (int收 > 0))&& ((int舒<250) &&(int舒 >0 )))
                    {
                        if ((int收 > 60) && (int舒 > 30))
                        {
                            if ((int收 > 180) || (int舒 >110))
                            {
                                str訊息 = "您的血壓值範圍為:高血壓第三期,請隨時注意血壓";
                            }
                            else if ((int收 <= 180) || (int舒 <= 110))
                            {
                                str訊息 = "您的血壓值範圍為:高血壓第二期,請經常量測血壓";
                            }
                            else if ((int收 < 160) || (int舒 < 100))
                            {
                                str訊息 = "您的血壓值範圍為:高血壓第一期,請持續追蹤血壓";
                            }
                            else if ((int收 < 140) || (int舒 < 90))
                            {
                                str訊息 = "您的血壓值範圍為:正常但是偏高,請定期追蹤血壓";
                            }
                            else if ((int收 < 130) || (int舒 < 85))
                            {
                                str訊息 = "您的血壓值範圍為:請繼續保持";
                            }
                            else if ((int收 < 120) || (int舒 < 80))
                            {
                                str訊息 = "您的血壓值範圍為:理想血壓";
                            }
                            else
                            {
                                str訊息 = "您的血壓值範圍為:理想血壓";
                            }
                            str訊息 = "您的收縮壓值為:" + int收.ToString() + "\n您的舒張壓為:" + int舒.ToString()+"\n********************************************\n" + str訊息;
                            MessageBox.Show(str訊息);
                            TB收.Text = "";   //自動清空
                            TB舒.Text = "";   //自動清空


                        }
                        else
                        {                           
                            MessageBox.Show(" 您輸入的血壓值偏低,請隨時注意血壓;\n 如輸入有誤請輸入正確血壓值");
                            TB收.Text = "";   //自動清空
                            TB舒.Text = "";   //自動清空
                        }
                    }
                    else
                    {
                       MessageBox.Show("您輸入的血壓值有誤或不合理, 請輸入正確血壓值");
                        TB收.Text = "";   //自動清空
                        TB舒.Text = "";   //自動清空
                    }
                }
                else
                {
                  MessageBox.Show("您輸入的血壓值有誤或不合理, 請輸入正確血壓值(收縮壓需大於舒張壓)");
                    TB收.Text = "";   //自動清空
                    TB舒.Text = "";   //自動清空
                }
            }
            else
            {
                MessageBox.Show("您輸入的血壓值有誤或不合理, 請輸入正確血壓值(不能有空值)");
                TB收.Text = "";   //自動清空
                TB舒.Text = "";   //自動清空
            }

        }
    }
}
