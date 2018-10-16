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

namespace 餛飩訂購
{
    public partial class btn加 : Form
    {
        SqlConnectionStringBuilder scsb;
        int intPrice = 0;//單價
        string strSumPrice;//訂購原價總價
        int intAmount = 0;//盒數
        int 單一品項總價 = 0;
        int intID = 0;
        int strgo = 0;
        int int折扣 = 10;
        double 折數 = 0.0;
        double 折扣後總價 = 0.0;
        double 總價 = 0.0;

        public btn加()
        {
            InitializeComponent();

        }

        private void cb訂購人_SelectedIndexChanged(object sender, EventArgs e)
        {//tb寄送地址
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();
            string strSQL = "select c_address from customer2 where c_name='" + cb訂購人.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(strSQL, con);
            DataSet ds = new DataSet();
            sda.Fill(ds, "address");

            for (int i = 0; i < ds.Tables["address"].Rows.Count; i++)
            {
                tb寄送地址.Text = string.Format("{0}", ds.Tables["address"].Rows[i][0]);
            }

            //tb電話            
            string strSQL2 = "select c_phone from customer2 where c_name='" + cb訂購人.Text + "'";
            SqlDataAdapter sda2 = new SqlDataAdapter(strSQL2, con);
            DataSet ds2 = new DataSet();
            sda2.Fill(ds2, "phone");
            for (int j = 0; j < ds2.Tables["phone"].Rows.Count; j++)
            {
                tb電話.Text = string.Format("{0}", ds2.Tables["phone"].Rows[j][0]);
            }

            con.Close();
        }

        private void btn確認_Click(object sender, EventArgs e)
        {
            string str訊息 = "";
            System.Collections.ArrayList myArrayList = new System.Collections.ArrayList();
            List<string> myList = new List<string>();//暫存的暫存
            myList.Add(cb訂購人.Text);
            myList.Add(cb選購商品.Text);
            myList.Add(tb訂購數.Text);
            myList.Add(Convert.ToInt32(intPrice * intAmount).ToString());//訂購產品量x單價的價格
            myList.Add(tb訂單id.Text);
            myArrayList.Add(myList);
          //  lb確認訂單明細.Items.Clear();
            if (cb訂購人.Text != "" && tb訂購數.Text != "0")
            {              
                lb確認訂單明細.Items.Add( "\n訂購商品:" + myList[1] + "\n訂購數:" + myList[2] + ",小計:NT$" + myList[3]);//使用List將資訊暫存並導入lbox訂購產品中
            }
            else
            {
                lb確認訂單明細.Items.Add("您必須輸入訂購人,訂購數量, 及折扣");             
            }
            //第二頁的訂單明細
            DialogResult R;
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();
            string strSQL = "insert into orders(p_name,c_name,o_amount,o_price,o_finalprice,o_address,o_phone,o_discount) values(@p_name,@c_name,@o_amount,@o_price,@o_finalprice,@o_address,@o_phone,@o_discount)";
            SqlCommand cmd = new SqlCommand(strSQL, con);
           // cmd.Parameters.AddWithValue("@o_id", tb訂單id.Text);
            cmd.Parameters.AddWithValue("@p_name", cb選購商品.Text);
            cmd.Parameters.AddWithValue("@c_name", cb訂購人.Text);
            cmd.Parameters.AddWithValue("@o_amount", tb訂購數.Text);
            cmd.Parameters.AddWithValue("@o_price", tb售價.Text);
            cmd.Parameters.AddWithValue("@o_finalprice", tb折扣後價格.Text);
            cmd.Parameters.AddWithValue("@o_address", tb寄送地址.Text);
            cmd.Parameters.AddWithValue("@o_phone", tb電話.Text);
            cmd.Parameters.AddWithValue("@o_discount", tb折扣.Text);
            int rows = cmd.ExecuteNonQuery();

           // MessageBox.Show(string.Format("新增{0}筆訂單成功,", rows));
           // MessageBox.Show("訂購完成");
            con.Close();
            
            /*
              if (cb訂購人.Text.Length > 0)
              {
                  if ((cb選購商品.Text.Length > 0) && (tb訂購數.Text != "0"))
                  {
                      R = MessageBox.Show("您確認要新增此訂單?", "確認新增", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                      if (R == DialogResult.Yes)
                      {
                          DataSet ds1 = new DataSet();
                          SqlDataAdapter sda1 = new SqlDataAdapter("select customer_id from customer where c_name='" + cb訂購人.Text + "'", con);
                          sda1.Fill(ds1, "customerID");
                          string customerID = string.Format("{0}", ds1.Tables["customerID"].Rows[0][0]);

                          DataSet ds2 = new DataSet();
                          SqlDataAdapter sda2 = new SqlDataAdapter("select p_id from product where p_name='" + cb選購商品.Text + "'", con);
                          sda2.Fill(ds2, "productID");
                          string productID = string.Format("{0}", ds2.Tables["productID"].Rows[0][0]);

                         // string strDate = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                          string strSQL = "insert into orders(o_id,p_name,c_name,o_amount,o_price,o_finalprice,o_address,o_phone,o_gostatus,o_discount) values(@o_id,@p_name,@c_name,@o_amount,@o_price,@o_finalprice,@o_address,@o_phone,@o_gostatus,@o_discount)";
                          SqlCommand cmd = new SqlCommand(strSQL, con);
                          cmd.Parameters.AddWithValue("@o_id",intID);
                          cmd.Parameters.AddWithValue("@p_name", cb選購商品.Text);
                          cmd.Parameters.AddWithValue("@c_name", cb訂購人.Text);
                          cmd.Parameters.AddWithValue("@o_amount", tb訂購數.Text);
                          cmd.Parameters.AddWithValue("@o_price", tb售價.Text);
                          cmd.Parameters.AddWithValue("@o_finalprice", 單一品項總價);
                          cmd.Parameters.AddWithValue("@o_address", tb寄送地址.Text);
                          cmd.Parameters.AddWithValue("@o_phone", tb電話.Text);
                          cmd.Parameters.AddWithValue("@o_gostatus", strgo);
                          cmd.Parameters.AddWithValue("@o_discount", 折數);
                        //  cmd.Parameters.AddWithValue("@o_date", strDate);

                          int rows = cmd.ExecuteNonQuery();

                          MessageBox.Show(string.Format("新增{0}筆訂單成功,", rows));
                          MessageBox.Show("訂購完成");
                      }
                      else
                      {
                          MessageBox.Show("已取消新增");
                      }
                  }
                  else
                  {
                      MessageBox.Show("產品、數量及取貨方式皆為必填欄位");
                  }
              }
              else
              {
                  MessageBox.Show("您尚未選擇客戶");
              }*/
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();
            dtp訂單日期.Value = DateTime.Now;
           //把產品叫出來        
            string strSQL = "select p_name from product2";
            SqlDataAdapter sda = new SqlDataAdapter(strSQL, con);
            DataSet ds = new DataSet();
            sda.Fill(ds, "productName");
            for (int i = 0; i < ds.Tables["productName"].Rows.Count; i++)
            {
                cb選購商品.Items.Add(ds.Tables["productName"].Rows[i][0]);
            }
          //  m1();

            //把客戶叫出來
            string strSQL2 = "select c_name from customer2";
            SqlDataAdapter sda2 = new SqlDataAdapter(strSQL2, con);
            DataSet ds2 = new DataSet();
            sda2.Fill(ds2, "customerName");
            for (int i = 0; i < ds2.Tables["customerName"].Rows.Count; i++)
            {
                cb訂購人.Items.Add(ds2.Tables["customerName"].Rows[i][0]);
            }
          //  m2();
        }

        private void orderdetailsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            //this.Validate();
            //  this.orderdetailsBindingSource.EndEdit();
            //  this.tableAdapterManager.UpdateAll(this.蝦仁餛飩new_DataSet);
        }

        private void tb折扣_TextChanged(object sender, EventArgs e)
        {
            
            if (tb折扣.Text != "")
            {
                bool ifNum = System.Double.TryParse(tb折扣.Text, out 折數);
                if (ifNum == true)
                {
                    //合理折數
                    if ((折數 >= 0) && (折數 <= 10))
                    {
                      // tb折扣.Text = "10";
                    }
                    else
                    {
                        MessageBox.Show("折數輸入錯誤0.0 - 10.0"); tb折扣.Text = ""; 折數 = 10.0;
                    }
                }
                else
                {
                    MessageBox.Show("折數輸入錯誤0.0-10.0");
                    tb折扣.Text = "10";
                    折數 = 10.0;
                }
            }
            else { 折數 = 10.0; }

            計算總價();

        }

        private void cb選購商品_SelectedIndexChanged(object sender, EventArgs e)
        {

            tb訂購數.Text = intAmount.ToString();
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();
            //tb售價顯示出來
            string strSQL = "select p_price from product2 where p_name='" + cb選購商品.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(strSQL, con);
            DataSet ds1 = new DataSet();
            sda.Fill(ds1, "unitPrice");

            for (int i = 0; i < ds1.Tables["unitPrice"].Rows.Count; i++)
            {
                tb售價.Text = string.Format("{0}", ds1.Tables["unitPrice"].Rows[i][0]);
            }
            Int32.TryParse(tb售價.Text, out intPrice);
            // tb原價總計.Text = $"{intPrice * intAmount}";
            con.Close();
        }

        private void m1()
        {/*
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();
            string strSQL = "select * from product";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();
            string strMsg = "";
            int i = 0;
            while (reader.Read() == true)
            {
                strMsg += string.Format("{0}, {1}, {2}\n", reader["p_id"], reader["p_name"], reader["p_price"]);
                cb選購商品.Items.Add(reader["p_name"]);
            }
            strMsg += "資料筆數:" + i.ToString();
            reader.Close();
            con.Close();
            */
        }
        private void m2()
        {/*
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();
            string strSQL = "select * from customer";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();
            string strMsg = "";
            int i = 0;
            while (reader.Read() == true)
            {
                strMsg += string.Format("{0}, {1}\n", reader["customer_id"], reader["c_name"]);
                cb訂購人.Items.Add(reader["c_name"]);
            }
            strMsg += "資料筆數:" + i.ToString();

            reader.Close();
            con.Close();
            */
        }

        private void num訂購數量_ValueChanged(object sender, EventArgs e)
        {//這個拿掉了
            // intAmount = Convert.ToInt32(num訂購數量.Value);
            // this.num訂購數量.Value.ToString();
        }

        private void tb訂購數_TextChanged(object sender, EventArgs e)
        {
            if (tb訂購數.Text != "")
            {
                bool ifNum = System.Int32.TryParse(tb訂購數.Text, out intAmount);//用Try.Parse檢查只能輸入整數 才轉換成功

                if ((ifNum) && (intAmount >= 0))//輸入的轉換整數成功 且 大於等於0
                {
                    btn減.Enabled = true;
                }
                else
                {
                  //  MessageBox.Show("請輸入正確數字", "輸入格式錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    intAmount = 0;
                    tb訂購數.Text = "0";
                }
            }
            else
            {
                intAmount = 0;
            }
            tb原價總計.Text = $"{intPrice * intAmount}";



        }

        private void btn減_Click(object sender, EventArgs e)
        {
            intAmount -= 1;
            if (intAmount < 0)
            {
                intAmount = 0;
                btn減.Enabled = false;
            }
            tb訂購數.Text = intAmount.ToString();
        }

        private void btn加_Click(object sender, EventArgs e)
        {
            intAmount += 1;
            btn減.Enabled = true;
            tb訂購數.Text = intAmount.ToString();
        }

        private void chk1_SelectedIndexChanged(object sender, EventArgs e)
        {//這個已經拿掉
            CheckBox checkbox = new CheckBox();
            if (checkbox.Checked == true)
            {
                MessageBox.Show("66");
            }
        }

        void 計算總價()
        {
            tb原價總計.Text = $"{intPrice * intAmount}";
            //tb原價總計.Text = string.Format("{0:C}", 單一品項總價);
            tb折扣後價格.Text = $"{intPrice * intAmount * 折數 / 10.0}";
            //string.Format("{0:C}", 折扣後總價);
            折扣後總價 = (double)單一品項總價 * 折數 / 10.0;
        }

        private void btn清除_Click(object sender, EventArgs e)
        {
            //目前訂購產品的清除  
            lb確認訂單明細.Items.Clear();//清除訂單介面
            //myArrayList.Clear();//取消暫存檔內的金額
            tb折扣.Text = "";
            tb折扣後價格.Text = "";
            cb訂購人.Text = "";
            cb選購商品.Text = "";
            tb售價.Text = "";
            tb原價總計.Text = "";
            tb訂購數.Text = "";
            dtp訂單日期.Value = DateTime.Now;
            dtp取貨日期.Value = DateTime.Now.AddDays(1);
            tb電話.Text = "";
            tb寄送地址.Text = "";           
        }

        private void btn訂單明細查詢_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();
            
                string strSQL查詢 = "select *from orders";
                SqlDataAdapter sda = new SqlDataAdapter(strSQL查詢, con);
                DataSet ds = new DataSet();
                sda.Fill(ds, "Orders");
                orderdetailsDataGridView.DataSource = ds.Tables["Orders"];
                con.Close();
        }

        private void btn訂單刪除明細_Click(object sender, EventArgs e)
        {
            int intID = 0;
            Int32.TryParse(tb訂單id.Text, out intID);
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();
            string strSQL = "delete from orders where o_id =@SearchID";
            SqlCommand cmd = new SqlCommand(strSQL, con);           
            cmd.Parameters.AddWithValue("@SearchID", intID);
            cmd.ExecuteNonQuery();
           
            con.Close();
            
        }

        private void tb訂單id_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();
            string strSQL查詢 = "select *from orders";
            SqlDataAdapter sda = new SqlDataAdapter(strSQL查詢, con);
            DataSet ds = new DataSet();
            sda.Fill(ds, "Orders");
            orderdetailsDataGridView.DataSource = ds.Tables["Orders"];
            con.Close();

        }
    }
    
}


