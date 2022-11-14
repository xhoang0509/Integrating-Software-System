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

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Tạo đối tượng sử dụng taiaf liệu XML
        XmlDocument doc = new XmlDocument();
        // Đường dẫn tới file XML
        string tentep = @"D:\Haui\ki 7 2022-2023\tich hop he thong phan mem\thuc hanh\WinFormsApp1\dsnhanvien.xml";
        // Xác đinh chỉ số dòng dược chọn trên DataGrid
        int d;

        private void HienThi()
        {
            dg_NhanVien.Rows.Clear();
            doc.Load(tentep);
            // Tạo đối tượng DS chứa danh sách các nhút nhanvien
            XmlNodeList DS = doc.SelectNodes("/ds/nhanvien");
            int sd = 0;
            dg_NhanVien.ColumnCount = 5;
            dg_NhanVien.Rows.Add();

            // Duyệt từng nút nhân viên
            foreach (XmlNode nhan_vien in DS)
            {
                // Truy xuất thuộc tích @manv
                XmlNode ma_nv = nhan_vien.SelectSingleNode("@manv");

                // lấy giá trị của thuộc tính @manv đưa lên cột thứ 1 của dòng trên DataGrid
                dg_NhanVien.Rows[sd].Cells[0].Value = ma_nv.InnerText.ToString();

                // truy xuất nút hoten
                XmlNode ho_ten = nhan_vien.SelectSingleNode("hoten");
                dg_NhanVien.Rows[sd].Cells[1].Value = ho_ten.InnerText.ToString();

                XmlNode gioi_tinh = nhan_vien.SelectSingleNode("gioitinh");
                dg_NhanVien.Rows[sd].Cells[2].Value = gioi_tinh.InnerText.ToString();

                XmlNode trinh_do = nhan_vien.SelectSingleNode("trinhdo");
                dg_NhanVien.Rows[sd].Cells[3].Value =  trinh_do.InnerText.ToString();             

                XmlNode dia_chi = nhan_vien.SelectSingleNode("diachi");
                dg_NhanVien.Rows[sd].Cells[4].Value = dia_chi.InnerText.ToString();

                dg_NhanVien.Rows.Add();
                sd++;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HienThi();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            // Đọc tài liệu xml
            doc.Load(tentep);
            // Truy xuat nut goc cua tai lieu XML
            XmlElement goc = doc.DocumentElement;
            // Tao mot nut nhanvien
            XmlNode nhan_vien = doc.CreateElement("nhanvien");

            // Tao mot thuoc tinh manv
            XmlAttribute ma_nv = doc.CreateAttribute("manv");
            // xac dinh gia tri cua thuoc tinh mv
            ma_nv.InnerText = txt_manv.Text;
            // bo sung thuoc tinh manv vao nut nhanvien
            nhan_vien.Attributes.Append(ma_nv);

            // tao mot nut hoten
            XmlNode ho_ten = doc.CreateElement("hoten");
            ho_ten.InnerText = txt_hoten.Text;
            nhan_vien.AppendChild(ho_ten);

            // tao mot nut diachi
            XmlNode dia_chi = doc.CreateElement("diachi");
            dia_chi.InnerText = txt_diachi.Text;
            nhan_vien.AppendChild(dia_chi);

            goc.AppendChild(nhan_vien);
            doc.Save(tentep);
            HienThi();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            try
            {
                doc.Load(tentep);
                XmlElement goc = doc.DocumentElement;

                // xac dinh nut can xoa
                XmlNode nhan_vien_xoa = goc.SelectSingleNode("/ds/nhanvien[@manv='" + txt_manv.Text + "']");
                goc.RemoveChild(nhan_vien_xoa);
                doc.Save(tentep);
                HienThi();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Khong co ma nv nay, hay thu lai!");
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            doc.Load(tentep);
            XmlElement goc = doc.DocumentElement;

             // xac dinh nut nhanvien can sua
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

            // thay nut nhanvien cu bang nut nhanvien moi
            goc.ReplaceChild(nhan_vien_moi, nhan_vien_cu);
            doc.Save(tentep);
            HienThi();
        }

        private void dg_NhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            d = e.RowIndex;
            txt_manv.Text = dg_NhanVien.Rows[d].Cells[0].Value.ToString();
            txt_hoten.Text = dg_NhanVien.Rows[d].Cells[1].Value.ToString();
            txt_diachi.Text = dg_NhanVien.Rows[d].Cells[2].Value.ToString();
        }

        private void btn_Preview_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", tentep);
        }
    }
}
