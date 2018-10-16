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
    public partial class Form4 : Form
    {
        SqlConnectionStringBuilder scsb;

        public Form4()
        {// customer table
            InitializeComponent();                           
            
        }
  
        private void Form4_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            
        }

        private void btn客戶總表_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();
            string strSQL = "select*from customer2";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();
            string strMsg = "";
            int i = 0;
            lbx客戶查詢.Items.Clear();
            while (reader.Read() == true)
            {
                i += 1;
                strMsg += string.Format("{0}, {1}, {2}, {3}\n", reader["customer_id"], reader["c_name"], reader["c_nickname"], reader["c_phone"]);
                lbx客戶查詢.Items.Add(reader["c_name"]);
            }
            strMsg += "資料筆數:" + i.ToString();            
            reader.Close();
            con.Close();

            //MessageBox.Show(strMsg);
        }

        private void lbx客戶查詢_SelectedIndexChanged(object sender, EventArgs e)
        {string strSearchName = lbx客戶查詢.SelectedItem.ToString();
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();            
            
            if (strSearchName.Length > 0)
            {
                string strSQL = "select*from customer2 where c_name like @SearchName";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearchName", "%" + strSearchName + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() == true)
                {
                    tbID.Text = string.Format("{0}", reader["customer_id"]);
                    tb姓名.Text = string.Format("{0}", reader["c_name"]);
                    tb暱稱.Text = string.Format("{0}", reader["c_nickname"]);
                    tb電話.Text = string.Format("{0}", reader["c_phone"]);
                    tb住址.Text = string.Format("{0}", reader["c_address"]);
                    tbEMAIL.Text = string.Format("{0}", reader["c_email"]);
                    dpt生日.Value = (DateTime)reader["c_birth"];
                    tb備註.Text = string.Format("{0}", reader["c_note"]);
                }
                else
                {   
                    tbID.Text = "";
                    tb姓名.Text = "";
                    tb暱稱.Text = "";
                    tb電話.Text = "";
                    tb住址.Text = "";
                    tbEMAIL.Text = "";
                    dpt生日.Value = DateTime.Now;
                    tb備註.Text = "";
                }
                // reader.Close();
                //   con.Close();
            }
            else
            {
                MessageBox.Show("查無此人");
            }
        }
        private void btn新增_Click(object sender, EventArgs e)
        {
            DialogResult R;
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();
            if ((tb姓名.Text.Length > 1) && (tb電話.Text.Length > 1))
            {
                R = MessageBox.Show("您確認要新增客戶資料?", "確認新增", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (R == DialogResult.Yes)
                {
                    string strSQL = "insert into customer2 values(@Newc_name,@Newc_nickname,@Newc_phone,@Newc_address,@Newc_email,@Newc_birth,@Newc_note)";
                    SqlCommand cmd = new SqlCommand(strSQL, con);

                   // cmd.Parameters.AddWithValue("@Newcustomer_id", tbID.Text);
                    cmd.Parameters.AddWithValue("@Newc_name", tb姓名.Text);
                    cmd.Parameters.AddWithValue("@Newc_nickname", tb暱稱.Text);
                    cmd.Parameters.AddWithValue("@Newc_phone", tb電話.Text);
                    cmd.Parameters.AddWithValue("@Newc_address", tb住址.Text);
                    cmd.Parameters.AddWithValue("@Newc_email", tbEMAIL.Text);
                    cmd.Parameters.AddWithValue("@Newc_birth", (DateTime)dpt生日.Value);
                    cmd.Parameters.AddWithValue("@Newc_note", tb備註.Text);

                    int rows = cmd.ExecuteNonQuery();

                   // MessageBox.Show(string.Format("資料新增完成,共新增{0}筆客戶資料", rows));
                   // string strMsg=string.Format("資料新增完成,共新增{0}筆客戶資料", rows);
                    //tb顯示資訊.Items.Add(strMsg);
                }
                else
                {
                    MessageBox.Show("已取消");
                }
            }
            else
            {
                MessageBox.Show("您提供的客戶資料不完整");
            }
            updateListBox();
        }

        private void btn刪除_Click(object sender, EventArgs e)
        {
            int intID = 0;
            Int32.TryParse(tbID.Text, out intID);
            // int intPhone = 0;
            // int intName = 0;//刪除資料: 以電話為依據(因不會重複)
            //Int32.TryParse(tb電話.Text, out intPhone);//將tb電話 轉為int型態
            // Int32.TryParse(tb姓名.Text, out intName);
            DialogResult R;
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();
           // if ((tb姓名.Text.Length > 1) && (tb電話.Text.Length > 8))
                if (intID > 0)
                {
                R = MessageBox.Show("您確認要刪除資料?", "確認刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (R == DialogResult.Yes)
                {
                    string strSQL = "delete from customer2 where customer_id=@SearchID";
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.Parameters.AddWithValue("@SearchID", intID);
                   // cmd.Parameters.AddWithValue("@SearchPhone", intPhone);
                    int rows = cmd.ExecuteNonQuery();
                   // MessageBox.Show(string.Format("已刪除{0}筆客戶資料", rows));
                    //將欄位清空
                    tbID.Text = "";
                    tb姓名.Text = "";
                    tb暱稱.Text = "";
                    tb電話.Text = "";
                    tb住址.Text = "";
                    tbEMAIL.Text = "";
                    dpt生日.Value = DateTime.Now;
                    tb備註.Text = "";
                }
                else
                {
                    MessageBox.Show("已取消刪除");
                }

            }
            else
            {
                MessageBox.Show("您尚未選擇欲刪除的客戶");
            }
            updateListBox();

        }

        private void btn修改_Click(object sender, EventArgs e)
        {
            int intID = 0;
            Int32.TryParse(tbID.Text, out intID);
            int intPhone = 0;
            int intName = 0;
            Int32.TryParse(tb電話.Text, out intPhone);
            Int32.TryParse(tb姓名.Text, out intName);
            DialogResult R;
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();
            
            if ((tb姓名.Text.Length > 1) && (tb電話.Text.Length > 1))            
            {
                R = MessageBox.Show("您確認要修改資料?", "確認修改", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (R == DialogResult.Yes)
                {
                    string strSQL = "update customer2 set c_name=@Newc_name, c_nickname=@Newc_nickname, c_phone=@Newc_phone, c_address=@Newc_address, c_email=@Newc_email, c_birth=@Newc_birth, c_note=@Newc_note where customer_id=@SearchID";
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.Parameters.AddWithValue("@SearchID", intID);
                    cmd.Parameters.AddWithValue("@Newc_name", tb姓名.Text);
                    cmd.Parameters.AddWithValue("@Newc_nickname", tb暱稱.Text);
                    cmd.Parameters.AddWithValue("@Newc_phone", tb電話.Text);
                    cmd.Parameters.AddWithValue("@Newc_address", tb住址.Text);
                    cmd.Parameters.AddWithValue("@Newc_email", tbEMAIL.Text);
                    cmd.Parameters.AddWithValue("@Newc_birth", (DateTime)dpt生日.Value);
                    cmd.Parameters.AddWithValue("@Newc_note", tb備註.Text);

                    int rows = cmd.ExecuteNonQuery();
                  //  MessageBox.Show(string.Format("您修改了{0}筆客戶資料", rows));
                }
                else
                {  MessageBox.Show("已取消");                }
            }
            else
            {  MessageBox.Show("您尚未選擇欲修改的項目");}
            updateListBox();
        }

        public void updateListBox()
        {//將異動後最新的資料 同步更新到updateListBox中的方法
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();
            lbx客戶查詢.Items.Clear();
            string strSQL = "select*from customer2";
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(strSQL, con);
            sda.Fill(ds, "newNameList");
            
            for (int i = 0; i < ds.Tables["newNameList"].Rows.Count; i++)
            {
                lbx客戶查詢.Items.Add(string.Format("{0}", ds.Tables["newNameList"].Rows[i][1]));
            }
            con.Close();
        }
    }
  
}
