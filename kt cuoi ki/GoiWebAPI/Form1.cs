using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;

namespace GoiWebAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void LoadDataGridView()
        {
            string link = "http://localhost:90/hocrestful2/api/sanpham";
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(SanPham[]));
            object data = js.ReadObject(response.GetResponseStream());
            SanPham[] arr = data as SanPham[];
            dg_sanpham.DataSource = arr;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void dg_sanpham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int d = e.RowIndex;
            txt_masp.Text = dg_sanpham.Rows[d].Cells[0].Value.ToString();
            txt_ten.Text = dg_sanpham.Rows[d].Cells[1].Value.ToString();
            txt_donvi.Text = dg_sanpham.Rows[d].Cells[2].Value.ToString();
            txt_sl.Text = dg_sanpham.Rows[d].Cells[3].Value.ToString();
            txt_gia.Text = dg_sanpham.Rows[d].Cells[4].Value.ToString();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            string postString = string.Format("?masp={0}&ten={1}&dv={2}&sl={3}&gia={4}",
                txt_masp.Text,txt_ten.Text, txt_donvi.Text, txt_sl.Text, txt_gia.Text);
            string link = "http://localhost:90/hocrestful2/api/sanpham" + postString;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            byte[] byteArr = Encoding.UTF8.GetBytes(postString);
            request.ContentLength = byteArr.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArr, 0, byteArr.Length);
            dataStream.Close();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(bool));
            object data = js.ReadObject(request.GetResponse().GetResponseStream());
            bool kq = (bool)data;
            if(kq)
            {
                LoadDataGridView();
                MessageBox.Show("Them thanh cong");
            } else
            {
                MessageBox.Show("btn_them that bai");
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (dg_sanpham.Rows.Count == 0) return;
            DataGridViewRow row = dg_sanpham.SelectedRows[0];
            string masp = row.Cells[0].Value.ToString();
            string postString = string.Format("?masp={0}", masp);
            string link = "http://localhost:90/hocrestful2/api/sanpham" + postString;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            request.Method = "DELETE";
            request.ContentType = "application/json;charset=UTF-8";
            byte[] byteArr = Encoding.UTF8.GetBytes(postString);
            request.ContentLength = byteArr.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArr, 0, byteArr.Length);
            dataStream.Close();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(bool));
            object data = js.ReadObject(request.GetResponse().GetResponseStream());
            bool kq = (bool)data;
            if(kq)
            {
                LoadDataGridView();
                MessageBox.Show("Xoa thanh cong");
            } 
        }
    }
}
