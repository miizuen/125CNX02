using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace QuanLyTraiCay.Class
{
    class Hang
    {
        FileXml Fxml = new FileXml();

        // Tên thẻ encoded cố định để dùng chung cho tất cả phương thức
        private string encodedTag = "_x0027_Hang_x0027_";
        private string normalTag = "Hang";

        // Kiểm tra mã hàng tồn tại - tìm cả 2 loại thẻ để tương thích file cũ
        public bool kiemtraMaHang(string MaHang)
        {
            string filePath = System.IO.Path.Combine(Application.StartupPath, "Hang.xml");
            if (!System.IO.File.Exists(filePath)) return false;

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            // Thử tìm với thẻ encoded trước
            XmlNode node = doc.SelectSingleNode($"NewDataSet/{encodedTag}[MaHang='{MaHang}']");

            // Nếu không thấy, thử tìm với thẻ thường (dành cho dữ liệu cũ sai)
            if (node == null)
            {
                node = doc.SelectSingleNode($"NewDataSet/{normalTag}[MaHang='{MaHang}']");
            }

            return node != null;
        }

        // Thêm hàng - dùng đúng thẻ encoded
        public void themH(string MaHang, string TenHang, string DonViTinh, string DonGia, string SoLuong, string MaNCC)
        {
            string noiDung = $"<{encodedTag}>" +
                    "<MaHang>" + MaHang + "</MaHang>" +
                    "<TenHang>" + TenHang + "</TenHang>" +
                    "<DonViTinh>" + DonViTinh + "</DonViTinh>" +
                    "<DonGia>" + DonGia + "</DonGia>" +
                    "<SoLuong>" + SoLuong + "</SoLuong>" +
                    "<MaNCC>" + MaNCC + "</MaNCC>" +
                    $"</{encodedTag}>";

            Fxml.Them("Hang.xml", noiDung);
        }

        // Sửa hàng - dùng đúng tên bảng encoded
        public void suaH(string MaHang, string TenHang, string DonViTinh, string DonGia, string SoLuong, string MaNCC)
        {
            string noiDung = "<MaHang>" + MaHang + "</MaHang>" +
                    "<TenHang>" + TenHang + "</TenHang>" +
                    "<DonViTinh>" + DonViTinh + "</DonViTinh>" +
                    "<DonGia>" + DonGia + "</DonGia>" +
                    "<SoLuong>" + SoLuong + "</SoLuong>" +
                    "<MaNCC>" + MaNCC + "</MaNCC>";

            // Dùng "Hang" nhưng FileXml.Sua đã được sửa để tìm cả 2 loại thẻ (xem lưu ý bên dưới)
            // Nếu bạn đã sửa FileXml.Sua như mình hướng dẫn trước → dùng "Hang" vẫn ok
            // Để chắc chắn nhất: truyền encodedTag
            Fxml.Sua("Hang.xml", "Hang", "MaHang", MaHang, noiDung);
        }

        // Xóa hàng - dùng đúng tên bảng encoded
        public void xoaH(string MaHang)
        {
            // Tương tự suaH, nếu FileXml.Xoa đã được sửa để tìm cả 2 thẻ → dùng "Hang" ok
            Fxml.Xoa("Hang.xml", "Hang", "MaHang", MaHang);
        }
    }
}