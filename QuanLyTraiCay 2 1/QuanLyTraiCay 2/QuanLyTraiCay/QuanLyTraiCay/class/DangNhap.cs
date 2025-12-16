using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;


namespace QuanLyTraiCay.Class
{
    class DangNhap
    {
        FileXml Fxml = new FileXml();
        public void layMaQuyen()
        {

            XmlTextReader reader = new XmlTextReader("TaiKhoan.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            XmlNode nodeMQ = doc.SelectSingleNode("NewDataSet/TaiKhoan/Quyen");


        }
        public bool kiemtraDangNhap(string MaNhanVien, string MatKhau)
        {
            XmlTextReader reader = new XmlTextReader("TaiKhoan.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            XmlNode node = doc.SelectSingleNode("NewDataSet/TaiKhoan[MaNhanVien='" + MaNhanVien + "']");
            node = doc.SelectSingleNode("NewDataSet/TaiKhoan[MatKhau='" + MatKhau + "']");
            reader.Close();
            bool kq = true;
            if (node != null)
            {
                return kq = true;
            }
            else
            {
                return kq = false;

            }


        }
        public void dangkiTaiKhoan(string MaNhanVien, string MatKhau, int Quyen)
        {
            string noiDung = "<_x0027_TaiKhoan_x0027_>" +
                             "<MaNhanVien>" + MaNhanVien + "</MaNhanVien>" +
                             "<MatKhau>" + MatKhau + "</MatKhau>" +
                             "<Quyen>" + Quyen + "</Quyen>" +
                             "</_x0027_TaiKhoan_x0027_>";

            Fxml.Them("TaiKhoan.xml", noiDung);
        }

        public void xoaTK(string MaNhanVien)
        {
            Fxml.Xoa("TaiKhoan.xml","TaiKhoan","MaNhanVien", MaNhanVien);
            
        }
        public bool kiemtraTTTK(string MaNhanVien)
        {
            XmlTextReader reader = new XmlTextReader("TaiKhoan.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            XmlNode node = doc.SelectSingleNode("NewDataSet/TaiKhoan[MaNhanVien='" + MaNhanVien + "']");
            reader.Close();
            bool kq = true;
            if (node != null)
            {
                return kq = true;
            }
            else
            {
                return kq = false;

            }
        }
        public void DoiMatKhau(string tenDN, string matKhauMoi)
        {
            string filePath = Application.StartupPath + "\\TaiKhoan.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            // Đúng tag _x0027_TaiKhoan_x0027_
            XmlNode node = doc.SelectSingleNode("NewDataSet/_x0027_TaiKhoan_x0027_[MaNhanVien='" + tenDN + "']");
            if (node != null)
            {
                node["MatKhau"].InnerText = matKhauMoi;
                doc.Save(filePath);
            }
        }

        public bool kiemtraTTDN(string duongdan,string MaNhanVien, string MatKhau)
        {
            try
            {
                string filePath = Application.StartupPath + "\\TaiKhoan.xml";
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);

                // Đổi tên tag cho đúng với cấu trúc file thật
                string xpath = "NewDataSet/_x0027_TaiKhoan_x0027_[MaNhanVien='" + MaNhanVien + "']";
                XmlNode node = doc.SelectSingleNode(xpath);

                if (node != null)
                {
                    string mkTrongFile = node["MatKhau"].InnerText.Trim();
                    return mkTrongFile == MatKhau.Trim();
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập: " + ex.Message);
                return false;
            }

        }
    }
}
