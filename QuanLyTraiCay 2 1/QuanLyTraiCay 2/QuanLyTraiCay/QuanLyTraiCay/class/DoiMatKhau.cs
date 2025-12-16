using QuanLyTraiCay.gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuanLyTraiCay.Class
{
    class DoiMatKhau
    {
        DangNhap dn = new DangNhap();

        public bool KiemTraMK(string matKhauCu)
        {
            // Trả về đúng kết quả từ hàm kiểm tra đăng nhập
            return dn.kiemtraTTDN("TaiKhoan.xml", frmMain.tenDNMain, matKhauCu);
        }

        public void Doi(string matKhauMoi)
        {
            dn.DoiMatKhau(frmMain.tenDNMain, matKhauMoi);
        }
    }
}
