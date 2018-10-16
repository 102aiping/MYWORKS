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
    public partial class Form3 : Form
    {
        SqlConnectionStringBuilder scsb;

        public Form3()
        {// product table
            InitializeComponent();
          
        }

        private void btn新增_Click(object sender, EventArgs e)
        {
            DialogResult R;
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();
            if ((tb產品名稱.Text.Length > 0) && (tb產品價格.Text.Length > 0))
            {
                R = MessageBox.Show("您確認要新增產品資料?", "確認新增", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (R == DialogResult.Yes)
                {
                    string strSQL = "insert into product2 values(@NewName,@Newprice,@Newqty,@Newmakedate,@Newnote)";
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                   // cmd.Parameters.AddWithValue("@NewID", tb產品ID.Text);
                    cmd.Parameters.AddWithValue("@NewName", tb產品名稱.Text);
                    cmd.Parameters.AddWithValue("@Newprice", tb產品價格.Text);
                    cmd.Parameters.AddWithValue("@Newqty", tb產品庫存.Text);
                    cmd.Parameters.AddWithValue("@Newmakedate", (DateTime)dtp製造日期.Value);
                    cmd.Parameters.AddWithValue("@Newnote", tb備註.Text);
                    int rows = cmd.ExecuteNonQuery();
                  //  MessageBox.Show(string.Format("資料新增完成,共新增{0}筆產品資料", rows));
                }
                else
                {
                   // MessageBox.Show("已取消新增");
                }
            }
            else
            {
                MessageBox.Show("您提供的產品資訊不完整");

            }
            updateListBox();
        }
        public void updateListBox()
        {//將異動後最新的資料 同步更新到updateListBox中的方法
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();
            //lb產品查詢.Items.Clear();
            string strSQL = "select*from product2";
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(strSQL, con);
            sda.Fill(ds, "newNameList");
            for (int i = 0; i < ds.Tables["newNameList"].Rows.Count; i++)
            {
                lb產品查詢.Items.Add(string.Format("{0}", ds.Tables["newNameList"].Rows[i][1]));
            }
            con.Close();
        }
        private void btn儲存_Click(object sender, EventArgs e)
        {
           
        }

        private void btn刪除_Click(object sender, EventArgs e)
        {
            int intID = 0;
            Int32.TryParse(tb產品ID.Text, out intID);
            DialogResult R;
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();
            if ((tb產品名稱.Text.Length > 0) && (tb產品價格.Text.Length > 0))
            {
                R = MessageBox.Show("您確認要刪除資料?", "確認刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (R == DialogResult.Yes)
                {
                    string strSQL = "delete from product2 where p_id=@SearchID";
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.Parameters.AddWithValue("@SearchID", intID);
                    /*int rows =*/ cmd.ExecuteNonQuery();
                    MessageBox.Show(string.Format("已刪除"));
                    //將欄位清空
                    tb產品ID.Text = "";
                    tb產品名稱.Text = "";
                    tb產品價格.Text = "";
                    tb產品庫存.Text = "";
                    dtp製造日期.Value = DateTime.Now;
                    tb備註.Text = "";
                }
                else
                {
                    MessageBox.Show("已取消刪除");
                }
            }
            else
            {
                MessageBox.Show("您尚未選擇欲刪除的產品");
            }
            updateListBox();
        }

        private void btn修改_Click(object sender, EventArgs e)
        {
            int intID = 0;
            Int32.TryParse(tb產品ID.Text, out intID);
            DialogResult R;
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();
            if ((tb產品名稱.Text.Length > 1) && (tb產品價格.Text.Length > 0))
            {
               R = MessageBox.Show("您確認要修改資料?", "確認修改", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (R == DialogResult.Yes)
                {
                    string strSQL = "update product2 set p_name=@NewName,p_price=@Newprice,p_qty=@Newqty,p_makedate=@Newmakedate,p_note=@Newnote where p_id=@SearchID";
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.Parameters.AddWithValue("@SearchID", intID);
                    cmd.Parameters.AddWithValue("@NewName", tb產品名稱.Text);
                    cmd.Parameters.AddWithValue("@Newprice", tb產品價格.Text);
                    cmd.Parameters.AddWithValue("@Newqty", tb產品庫存.Text);
                    cmd.Parameters.AddWithValue("@Newmakedate", (DateTime)dtp製造日期.Value);
                    cmd.Parameters.AddWithValue("@Newnote", tb備註.Text);
                    int rows = cmd.ExecuteNonQuery();
                  //  MessageBox.Show(string.Format("已修改{0}筆客戶資料", rows));
                    //將欄位清空
                    tb產品ID.Text = "";
                    tb產品名稱.Text = "";
                    tb產品價格.Text = "";
                    tb產品庫存.Text = "";
                    dtp製造日期.Value = DateTime.Now;
                    tb備註.Text = "";
                }
                else
                {
                    MessageBox.Show("已取消修改");
                }
            }
            else
            {
                MessageBox.Show("您尚未選擇欲修改的產品");
            }
            updateListBox();


        }

        private void btn回復_Click(object sender, EventArgs e)
        {//回復鈕的功能, 紀錄目前所在位置, 回復編輯資料
                      
            
        }

        private void 蝦仁餛飩產品BindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
          //  this.Validate();
          //  this.蝦仁餛飩產品BindingSource.EndEdit();
          //  this.tableAdapterManager.UpdateAll(this.蝦仁餛飩new_DataSet);
          

        }

        private void btn查詢_Click(object sender, EventArgs e)
        {

            //lb產品查詢.Items.Clear();
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();
            string strSQL = "select*from product2";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();
            string strMsg = "";
            int i = 0;
            lb產品查詢.Items.Clear();
            while (reader.Read() == true)
            {
                i += 1;
                strMsg += string.Format("{0}, {1}, {2}, {3}\n", reader["p_id"], reader["p_name"], reader["p_price"], reader["p_qty"]);
                lb產品查詢.Items.Add(reader["p_name"]);
            }
            strMsg += "資料筆數:" + i.ToString();
            reader.Close();
            con.Close();


        }

        private void Form3_Load_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();
        }

        private void lb產品查詢_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(CDictionary.scsb);
            con.Open();
            string strSearchName = lb產品查詢.SelectedItem.ToString();
            if (strSearchName.Length > 0)
            {
                string strSQL = "select * from product2 where p_name like @SearchName";
                SqlCommand cmd = new SqlCommand(strSQL, con);
               cmd.Parameters.AddWithValue("@SearchName", "%" + strSearchName + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                           
                if (reader.Read() == true)
                {
                    tb產品ID.Text = string.Format("{0}", reader["p_id"]);
                    tb產品名稱.Text = string.Format("{0}", reader["p_name"]);
                    tb產品價格.Text = string.Format("{0}", reader["p_price"]);
                    tb產品庫存.Text = string.Format("{0}", reader["p_qty"]);
                    dtp製造日期.Value = (DateTime)reader["p_makedate"];
                    tb備註.Text = string.Format("{0}", reader["p_note"]);
                }
                
                else
                {
                    MessageBox.Show("查無此產品");
                    tb產品ID.Text = "";
                    tb產品名稱.Text = "";
                    tb產品價格.Text = "";
                    tb產品庫存.Text = "";
                    dtp製造日期.Value = DateTime.Now;
                    tb備註.Text = "";
                }
            }
            else
            {
                MessageBox.Show("查無此產品");
            }
        }
    }
}
