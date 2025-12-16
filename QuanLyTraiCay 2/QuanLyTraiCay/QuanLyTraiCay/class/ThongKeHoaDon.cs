using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTraiCay.Class
{
    class ThongKeHoaDon
    {
        private FileXml fxml = new FileXml();
        private DataTable dtHoaDon;
        private DataTable dtChiTiet;

        // Đọc dữ liệu từ XML
        public void LoadData()
        {
            dtHoaDon = fxml.HienThi("HoaDon.xml");
            dtChiTiet = fxml.HienThi("ChiTietHoaDon.xml");
        }

        // Trả về danh sách hóa đơn
        public DataTable GetHoaDon()
        {
            if (dtHoaDon == null)
                LoadData();
            return dtHoaDon;
        }

        // Lấy chi tiết theo số hóa đơn
        public DataTable GetChiTietBySoHoaDon(int soHoaDon)
        {
            if (dtChiTiet == null)
                LoadData();

            var rows = dtChiTiet.AsEnumerable()
                .Where(r => Convert.ToInt32(r["SoHoaDon"]) == soHoaDon);

            if (rows.Any())
                return rows.CopyToDataTable();
            else
                return null;
        }

    }
}
