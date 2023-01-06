using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace bai4_dom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        XmlDocument doc = new XmlDocument();
        string tentep = @"D:\Haui\ki 7 2022-2023\tich hop he thong phan mem\thuc hanh\bai4_dom_final\bai4_dom\dsnhanvien.xml";
        int d;

        private void HienThi()
        {
            dg_nhanvien.Rows.Clear();
            doc.Load(tentep);
            XmlNodeList DS = doc.SelectNodes("/ds/nhanvien");
            int sd = 0;
            dg_nhanvien.ColumnCount = 3;
            dg_nhanvien.Rows.Add();

            foreach (XmlNode nhan_vien in DS)
            {
                XmlNode ma_nv = nhan_vien.SelectSingleNode("@manv");
                dg_nhanvien.Rows[sd].Cells[0].Value = ma_nv.InnerText.ToString();
                XmlNode ho_ten = nhan_vien.SelectSingleNode("hoten");
                dg_nhanvien.Rows[sd].Cells[1].Value = ho_ten.InnerText.ToString();
                XmlNode dia_chi = nhan_vien.SelectSingleNode("diachi");
                dg_nhanvien.Rows[sd].Cells[2].Value = dia_chi.InnerText.ToString();

                dg_nhanvien.Rows.Add();
                sd++;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HienThi();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            doc.Load(tentep);
            XmlElement goc = doc.DocumentElement;

            XmlNode nhan_vien = doc.CreateElement("nhanvien");
            XmlAttribute ma_nv = doc.CreateAttribute("manv");
            ma_nv.InnerText = txt_manv.Text;
            nhan_vien.Attributes.Append(ma_nv);

            XmlNode ho_ten = doc.CreateElement("hoten");
            ho_ten.InnerText = txt_hoten.Text;
            nhan_vien.AppendChild(ho_ten);

            XmlNode dia_chi = doc.CreateElement("diachi");
            dia_chi.InnerText = txt_diachi.Text;
            nhan_vien.AppendChild(dia_chi);

            goc.AppendChild(nhan_vien);
            doc.Save(tentep);
            HienThi();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            doc.Load(tentep);
            XmlElement goc = doc.DocumentElement;

            XmlNode nhan_vien_xoa = goc.SelectSingleNode("/ds/nhanvien[@manv='" + txt_manv.Text + "']");
            goc.RemoveChild(nhan_vien_xoa);
            doc.Save(tentep);
            HienThi();
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            doc.Load(tentep);
            XmlElement goc = doc.DocumentElement;
            XmlNode nhan_vien_cu = goc.SelectSingleNode("/ds/nhanvien[@manv='" + txt_manv.Text + "']");
            XmlNode nhan_vien_moi = doc.CreateElement("nhanvien");

            XmlAttribute ma_nv = doc.CreateAttribute("manv");
            ma_nv.InnerText = txt_manv.Text;
            nhan_vien_moi.Attributes.Append(ma_nv);

            XmlNode ho_ten = doc.CreateElement("hoten");
            ho_ten.InnerText = txt_hoten.Text;
            nhan_vien_moi.AppendChild(ho_ten);

            XmlNode dia_chi = doc.CreateElement("diachi");
            dia_chi.InnerText = txt_diachi.Text;
            nhan_vien_moi.AppendChild(dia_chi);

            goc.ReplaceChild(nhan_vien_moi, nhan_vien_cu);
            doc.Save(tentep);
            HienThi();
        }

        private void dg_nhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            d = e.RowIndex;
            txt_manv.Text = dg_nhanvien.Rows[d].Cells[0].Value.ToString();
            txt_hoten.Text = dg_nhanvien.Rows[d].Cells[1].Value.ToString();
            txt_diachi.Text = dg_nhanvien.Rows[d].Cells[1].Value.ToString();
        }

        private void btn_Preview_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", tentep);
        }

        private void btn_Tim_Click(object sender, EventArgs e)
        {
            doc.Load(tentep);
            XmlNodeList DS = doc.SelectNodes("/ds/nhanvien");
            XmlElement DS_tim = doc.CreateElement("ds");
            bool isFound = false;


            foreach (XmlNode nhan_vien in DS)
            {
                XmlNode ma_nv = nhan_vien.SelectSingleNode("@manv");
                if (ma_nv.InnerText.ToString().Equals(txt_manv.Text.ToString()))
                {
                    isFound = true;
                    DS_tim.AppendChild(nhan_vien);
                }
            }

            if (!isFound)
            {
                MessageBox.Show("Không tìm thấy nhân viên có mã này!");
            }
            else
            {
                dg_nhanvien.Rows.Clear();
                doc.Load(tentep);
                int sd = 0;
                dg_nhanvien.ColumnCount = 3;
                dg_nhanvien.Rows.Add();

                foreach (XmlNode nhan_vien in DS_tim)
                {
                    XmlNode ma_nv = nhan_vien.SelectSingleNode("@manv");
                    dg_nhanvien.Rows[sd].Cells[0].Value = ma_nv.InnerText.ToString();
                    XmlNode ho_ten = nhan_vien.SelectSingleNode("hoten");
                    dg_nhanvien.Rows[sd].Cells[1].Value = ho_ten.InnerText.ToString();
                    XmlNode dia_chi = nhan_vien.SelectSingleNode("diachi");
                    dg_nhanvien.Rows[sd].Cells[2].Value = dia_chi.InnerText.ToString();

                    dg_nhanvien.Rows.Add();
                    sd++;
                }
            }
        }
    }
}
