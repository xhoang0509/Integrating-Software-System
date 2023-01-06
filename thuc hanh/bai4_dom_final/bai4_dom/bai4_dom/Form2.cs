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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        XmlDocument doc = new XmlDocument();
        string tentep = @"D:\Haui\ki 7 2022-2023\tich hop he thong phan mem\thuc hanh\bai4_dom_final\bai4_dom\nhanvien.xml";
        int d;

        private void HienThi()
        {
            dg_nhanvien.Rows.Clear();
            doc.Load(tentep);

            XmlNodeList DS = doc.SelectNodes("/ds/nhanvien");

            int d = 0;
            dg_nhanvien.Rows.Add();
            foreach (XmlNode nhan_vien in DS)
            {
                XmlNode ma_nv = nhan_vien.SelectSingleNode("@manv");
                dg_nhanvien.Rows[d].Cells[0].Value = ma_nv.InnerText.ToString();

                XmlNode ho_ten = nhan_vien.SelectSingleNode("hoten");
                XmlNode ho = ho_ten.SelectSingleNode("ho");
                dg_nhanvien.Rows[d].Cells[1].Value = ho.InnerText.ToString();

                XmlNode ten = ho_ten.SelectSingleNode("ten");
                dg_nhanvien.Rows[d].Cells[2].Value = ten.InnerText.ToString();

                XmlNode dia_chi = nhan_vien.SelectSingleNode("diachi");
                dg_nhanvien.Rows[d].Cells[3].Value = dia_chi.InnerText.ToString();

                dg_nhanvien.Rows.Add();
                d++;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            HienThi();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            doc.Load(tentep);
            XmlNodeList DS = doc.SelectNodes("/ds/nhanvien");
            bool isHadNhanVien = false;

            foreach (XmlNode nhan_vien_1 in DS)
            {
                XmlNode ma_nv_1 = nhan_vien_1.SelectSingleNode("@manv");
                if (ma_nv_1.InnerText.ToString().Equals(txt_manv.Text.ToString()))
                {
                    isHadNhanVien = true;
                }
            }

            if (isHadNhanVien)
            {
                MessageBox.Show("Mã nhân viên này đã tồn tại, hãy thử lại!");
            }
            else
            {
                XmlElement goc = doc.DocumentElement;

                XmlNode nhan_vien = doc.CreateElement("nhanvien");
                XmlAttribute ma_nv = doc.CreateAttribute("manv");
                ma_nv.InnerText = txt_manv.Text;
                nhan_vien.Attributes.Append(ma_nv);

                XmlNode ho_ten = doc.CreateElement("hoten");
                XmlNode ho = doc.CreateElement("ho");
                ho.InnerText = txt_ho.Text;
                ho_ten.AppendChild(ho);
                XmlNode ten = doc.CreateElement("ten");
                ten.InnerText = txt_ten.Text;
                ho_ten.AppendChild(ten);
                nhan_vien.AppendChild(ho_ten);

                XmlNode dia_chi = doc.CreateElement("diachi");
                dia_chi.InnerText = txt_diachi.Text;
                nhan_vien.AppendChild(dia_chi);

                goc.AppendChild(nhan_vien);
                doc.Save(tentep);
                HienThi();
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {            
            var isConfirm = MessageBox.Show("Hành động này không thể phục hồi. Bạn có chắc chắn muốn xóa không?", "Bạn có chắc chắn muốn xóa không?", MessageBoxButtons.YesNoCancel);
            if (isConfirm == DialogResult.Yes)
            {
                doc.Load(tentep);
                XmlElement goc = doc.DocumentElement;
                XmlNode nhan_vien_xoa = goc.SelectSingleNode("/ds/nhanvien[@manv='" + txt_manv.Text + "']");
                goc.RemoveChild(nhan_vien_xoa);
                doc.Save(tentep);
                HienThi();
            }
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
            XmlNode ho = doc.CreateElement("ho");
            ho.InnerText = txt_ho.Text;
            ho_ten.AppendChild(ho);
            XmlNode ten = doc.CreateElement("ten");
            ten.InnerText = txt_ten.Text;
            ho_ten.AppendChild(ten);
            nhan_vien_moi.AppendChild(ho_ten);

            XmlNode dia_chi = doc.CreateElement("diachi");
            dia_chi.InnerText = txt_diachi.Text;
            nhan_vien_moi.AppendChild(dia_chi);

            goc.ReplaceChild(nhan_vien_moi, nhan_vien_cu);
            doc.Save(tentep);
            HienThi();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btn_Tim_Click(object sender, EventArgs e)
        {
            doc.Load(tentep);
            XmlElement goc = doc.DocumentElement;
            XmlNodeList DS = goc.SelectNodes("/ds/nhanvien");
            XmlNode DS_tim = doc.CreateElement("ds");
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

                int d = 0;
                dg_nhanvien.Rows.Add();
                foreach (XmlNode nhan_vien in DS_tim)
                {
                    XmlNode ma_nv = nhan_vien.SelectSingleNode("@manv");
                    dg_nhanvien.Rows[d].Cells[0].Value = ma_nv.InnerText.ToString();

                    XmlNode ho_ten = nhan_vien.SelectSingleNode("hoten");
                    XmlNode ho = ho_ten.SelectSingleNode("ho");
                    dg_nhanvien.Rows[d].Cells[1].Value = ho.InnerText.ToString();

                    XmlNode ten = ho_ten.SelectSingleNode("ten");
                    dg_nhanvien.Rows[d].Cells[2].Value = ten.InnerText.ToString();

                    XmlNode dia_chi = nhan_vien.SelectSingleNode("diachi");
                    dg_nhanvien.Rows[d].Cells[3].Value = dia_chi.InnerText.ToString();

                    dg_nhanvien.Rows.Add();
                    d++;
                }
            }

        }

        private void dg_nhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            d = e.RowIndex;
            txt_manv.Text = dg_nhanvien.Rows[d].Cells[0].Value.ToString();
            txt_ho.Text = dg_nhanvien.Rows[d].Cells[1].Value.ToString();
            txt_ten.Text = dg_nhanvien.Rows[d].Cells[2].Value.ToString();
            txt_diachi.Text = dg_nhanvien.Rows[d].Cells[3].Value.ToString();
        }
    }
}
