using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuanLyTraiCay.Class
{
    class Main
    {
        FileXml Fxml = new FileXml();
        public string HoTen(string MaNhanVien)
        {
            return Fxml.LayGiaTri("NhanVien.xml", "MaNhanVien", MaNhanVien, "TenNhanVien"); ;
        }
    }
}
