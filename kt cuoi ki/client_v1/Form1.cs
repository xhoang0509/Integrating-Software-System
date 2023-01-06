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
namespace client_v1
{
    public partial class Form1 : Form
    {
        DanhMuc[] DS_dm;
        public Form1()
        {
            InitializeComponent();
        }

        public void LoadDataGridView()
        {
            string link = "http://localhost:90/hocrestful/api/sanpham";
            HttpWebRequest request = WebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(SanPham[]));
            object data = js.ReadObject(response.GetResponseStream());
            SanPham[] arr = data as SanPham[];
            dg_sanpham.DataSource = arr;
        }

        public void LoadComboBox()
        {
            string link = "http://localhost:90/hocrestful/api/danhmuc";
            HttpWebRequest request = WebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(DanhMuc[]));
            object data = js.ReadObject(response.GetResponseStream());
            DS_dm = data as DanhMuc[];
            cb_danhmuc.DataSource = DS_dm;
            cb_danhmuc.ValueMember = "MaDanhMuc";
            cb_danhmuc.DisplayMember = "TenDanhMuc";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
            LoadComboBox();
        }

        private void btn_tim_Click(object sender, EventArgs e)
        {
            string madm = DS_dm[cb_danhmuc.SelectedIndex].MaDanhMuc.ToString();
            string link = "http://localhost:90/hocrestful/api/sanpham?madm=" + madm;
            HttpWebRequest request = WebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(SanPham[]));
            object data = js.ReadObject(response.GetResponseStream());
            SanPham[] arr = data as SanPham[];
            dg_sanpham.DataSource = arr;
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            string postString = string.Format("?ma={0}&ten={1}&gia={2}&madm={3}", txt_masp.Text,
                txt_tensp.Text, txt_dongia.Text, cb_danhmuc.SelectedValue);
            string link = "http://localhost:90/hocrestful/api/sanpham/" + postString;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            byte[] byteArray = Encoding.UTF8.GetBytes(postString);
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(bool));
            object data = js.ReadObject(request.GetResponse().GetResponseStream());
            bool kq = (bool)data;
            if(kq)
            {
                LoadDataGridView();
                MessageBox.Show("Thêm sản phẩm thành công");
            } else
            {
                MessageBox.Show("Thêm sản phẩm thất bại");
            }
        }

        private void dg_sanpham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int d = e.RowIndex;
            txt_masp.Text = dg_sanpham.Rows[d].Cells[0].Value.ToString();
            txt_tensp.Text = dg_sanpham.Rows[d].Cells[1].Value.ToString();
            txt_dongia.Text = dg_sanpham.Rows[d].Cells[2].Value.ToString();
            cb_danhmuc.Text = dg_sanpham.Rows[d].Cells[3].Value.ToString();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            string putString = string.Format("?ma={0}&ten={1}&gia={2}&madm={3}",
                txt_masp.Text, txt_tensp.Text, txt_dongia.Text, cb_danhmuc.SelectedValue);
            string link = "http://localhost:90/hocrestful/api/sanpham/" + putString;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            request.Method = "PUT";
            request.ContentType = "application/json:charset=UTF-8";
            byte[] byteArray = Encoding.UTF8.GetBytes(putString);
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(bool));
            object data = js.ReadObject(request.GetResponse().GetResponseStream());
            bool kq = (bool)data;
            if(kq)
            {
                MessageBox.Show("Sửa sản phẩm thành công");
                LoadDataGridView();
            } else
            {
                MessageBox.Show("Sửa sản phẩm thất bại");
            }

        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (dg_sanpham.SelectedRows.Count == 0)
            {
                return;                
            }
            else
            {
                DataGridViewRow row = dg_sanpham.SelectedRows[0];
                string masp = row.Cells[0].Value.ToString();
                string deleteString = string.Format("?ma={0}", masp);
                string link = "http://localhost:90/hocrestful/api/sanpham/" + deleteString;
                HttpWebRequest request = HttpWebRequest.CreateHttp(link);
                request.Method = "DELETE";
                request.ContentType = "application/json;charset=UTF-8";
                byte[] byteArray = Encoding.UTF8.GetBytes(deleteString);
                request.ContentLength = byteArray.Length;
                Stream dataSteam = request.GetRequestStream();
                dataSteam.Write(byteArray, 0, byteArray.Length);
                dataSteam.Close();
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(bool));
                object data = js.ReadObject(request.GetResponse().GetResponseStream());
                bool kq = (bool)data;
                if (kq)
                {
                    LoadDataGridView();
                    MessageBox.Show("Xoa thanh cong");
                }
            }
        }
    }
}
