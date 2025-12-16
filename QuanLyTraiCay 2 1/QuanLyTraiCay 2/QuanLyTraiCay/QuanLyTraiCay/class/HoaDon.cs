using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyTraiCay.Class
{
    class HoaDon
    {
        private FileXml Fxml;
        private int soHoaDonHienTai;
        private DataTable dtChiTiet;
        private DataTable dtHoaDon;
        private string duongDanHoaDon = Path.Combine(Application.StartupPath, "HoaDon.xml");
        private string duongDanChiTiet = Path.Combine(Application.StartupPath, "ChiTietHoaDon.xml");
        private string duongDanHang = Path.Combine(Application.StartupPath, "Hang.xml");

        public int SoHoaDonHienTai => soHoaDonHienTai;
        public DataTable DtChiTiet => dtChiTiet;

        public HoaDon()
        {
            Fxml = new FileXml();
            dtChiTiet = new DataTable();
            KhoiTaoChiTietHoaDon();
            TaiSoHoaDonMoi();
        }

        #region Khởi tạo bảng chi tiết
        private void KhoiTaoChiTietHoaDon()
        {
            dtChiTiet.Columns.Add("MaHang", typeof(string));
            dtChiTiet.Columns.Add("TenHang", typeof(string));
            dtChiTiet.Columns.Add("DonGia", typeof(long));
            dtChiTiet.Columns.Add("SoLuong", typeof(int));
            dtChiTiet.Columns.Add("ThanhTien", typeof(long));
        }
        #endregion

        #region Tải số hóa đơn mới
        public void TaiSoHoaDonMoi()
        {
            try
            {
                dtHoaDon = Fxml.HienThi("HoaDon.xml");
                if (dtHoaDon != null && dtHoaDon.Rows.Count > 0)
                {
                    soHoaDonHienTai = dtHoaDon.AsEnumerable()
                        .Max(r => Convert.ToInt32(r["SoHoaDon"])) + 1;
                }
                else
                {
                    soHoaDonHienTai = 1;
                }
            }
            catch
            {
                soHoaDonHienTai = 1;
            }
        }
        #endregion

        #region Thêm hàng vào chi tiết
        public bool ThemHangVaoChiTiet(string maHang, int soLuong, out string thongBao)
        {
            thongBao = "";
            if (string.IsNullOrEmpty(maHang))
            {
                thongBao = "Vui lòng nhập mã hàng!";
                return false;
            }

            if (soLuong <= 0)
            {
                thongBao = "Số lượng không hợp lệ!";
                return false;
            }

            // Kiểm tra tồn kho
            string tonKhoStr = Fxml.LayGiaTri("Hang.xml", "MaHang", maHang, "SoLuong");
            if (!int.TryParse(tonKhoStr, out int tonKho))
            {
                thongBao = "Không lấy được tồn kho!";
                return false;
            }

            if (soLuong > tonKho)
            {
                string donVi = Fxml.LayGiaTri("Hang.xml", "MaHang", maHang, "DonViTinh");
                thongBao = $"Không đủ hàng! Chỉ còn {tonKho} {donVi}";
                return false;
            }

            // Kiểm tra trùng
            foreach (DataRow row in dtChiTiet.Rows)
            {
                if (row["MaHang"].ToString() == maHang)
                {
                    thongBao = "Hàng đã có trong hóa đơn. Vui lòng sửa số lượng trên bảng.";
                    return false;
                }
            }

            // Lấy thông tin hàng
            string tenHang = Fxml.LayGiaTri("Hang.xml", "MaHang", maHang, "TenHang");
            string donGiaStr = Fxml.LayGiaTri("Hang.xml", "MaHang", maHang, "DonGia");
            if (!long.TryParse(donGiaStr, out long donGia))
            {
                thongBao = "Lỗi giá bán!";
                return false;
            }

            // Thêm dòng
            DataRow dr = dtChiTiet.NewRow();
            dr["MaHang"] = maHang;
            dr["TenHang"] = tenHang;
            dr["DonGia"] = donGia;
            dr["SoLuong"] = soLuong;
            dr["ThanhTien"] = soLuong * donGia;
            dtChiTiet.Rows.Add(dr);

            return true;
        }
        #endregion

        #region Xóa dòng
        public void XoaDong(int index)
        {
            if (index >= 0 && index < dtChiTiet.Rows.Count)
            {
                dtChiTiet.Rows.RemoveAt(index);
            }
        }
        #endregion

        #region Tính tổng tiền
        public long TinhTongTien()
        {
            long tong = 0;
            foreach (DataRow row in dtChiTiet.Rows)
            {
                tong += Convert.ToInt64(row["ThanhTien"]);
            }
            return tong;
        }
        #endregion

        #region Thanh toán
        public bool ThanhToan(string maNhanVien, DateTime ngayLap, out string thongBao)
        {
            thongBao = "";
            if (dtChiTiet.Rows.Count == 0)
            {
                thongBao = "Chưa có hàng hóa nào!";
                return false;
            }

            if (string.IsNullOrEmpty(maNhanVien))
            {
                thongBao = "Vui lòng nhập mã nhân viên!";
                return false;
            }

            try
            {
                string ngayLapStr = ngayLap.ToString("yyyy-MM-dd hh:mm:ss tt");
                long tongTien = TinhTongTien();

                // 1. Lưu hóa đơn
                string noiDungHD = $"<HoaDon>" +
                                   $"<SoHoaDon>{soHoaDonHienTai}</SoHoaDon>" +
                                   $"<NgayLap>{ngayLapStr}</NgayLap>" +
                                   $"<MaNhanVien>{maNhanVien}</MaNhanVien>" +
                                   $"<TongTien>{tongTien}</TongTien>" +
                                   $"</HoaDon>";
                Fxml.Them(duongDanHoaDon, noiDungHD);

                // 2. Lưu chi tiết
                int id = LayIdChiTietMoiNhat() + 1;
                foreach (DataRow row in dtChiTiet.Rows)
                {
                    string chiTiet = $"<ChiTietHoaDon>" +
                                     $"<Id>{id}</Id>" +
                                     $"<SoHoaDon>{soHoaDonHienTai}</SoHoaDon>" +
                                     $"<MaHang>{row["MaHang"]}</MaHang>" +
                                     $"<DonGia>{row["DonGia"]}</DonGia>" +
                                     $"<SoLuong>{row["SoLuong"]}</SoLuong>" +
                                     $"</ChiTietHoaDon>";
                    Fxml.Them(duongDanChiTiet, chiTiet);
                    id++;
                }

                // 3. Cập nhật tồn kho
                foreach (DataRow row in dtChiTiet.Rows)
                {
                    string maHang = row["MaHang"].ToString();
                    int slBan = Convert.ToInt32(row["SoLuong"]);
                    int slTon = int.Parse(Fxml.LayGiaTri("Hang.xml", "MaHang", maHang, "SoLuong"));
                    int slMoi = slTon - slBan;

                    string noiDungCapNhat = $"<MaHang>{maHang}</MaHang>" +
                                            $"<TenHang>{row["TenHang"]}</TenHang>" +
                                            $"<DonViTinh>{Fxml.LayGiaTri("Hang.xml", "MaHang", maHang, "DonViTinh")}</DonViTinh>" +
                                            $"<DonGia>{row["DonGia"]}</DonGia>" +
                                            $"<SoLuong>{slMoi}</SoLuong>" +
                                            $"<MaNCC>{Fxml.LayGiaTri("Hang.xml", "MaHang", maHang, "MaNCC")}</MaNCC>";
                    Fxml.Sua(duongDanHang, "Hang", "MaHang", maHang, noiDungCapNhat);
                }

                // Tạo hóa đơn mới
                TaoHoaDonMoi();
                thongBao = $"Thanh toán thành công! Hóa đơn số: {soHoaDonHienTai}\nTổng tiền: {tongTien:N0} VNĐ";
                return true;
            }
            catch (Exception ex)
            {
                thongBao = "Lỗi thanh toán: " + ex.Message;
                return false;
            }
        }
        #endregion

        #region Tạo hóa đơn mới
        public void TaoHoaDonMoi()
        {
            soHoaDonHienTai++;
            dtChiTiet.Clear();
        }
        #endregion

        #region Lấy ID chi tiết mới nhất
        private int LayIdChiTietMoiNhat()
        {
            try
            {
                DataTable dt = Fxml.HienThi("ChiTietHoaDon.xml");
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.AsEnumerable().Max(r => Convert.ToInt32(r["Id"]));
                }
            }
            catch { }
            return 0;
        }
        #endregion

        #region Lấy thông tin hàng
        public bool LayThongTinHang(string maHang, out string tenHang, out long donGia, out int tonKho, out string donViTinh)
        {
            tenHang = Fxml.LayGiaTri("Hang.xml", "MaHang", maHang, "TenHang");
            string dgStr = Fxml.LayGiaTri("Hang.xml", "MaHang", maHang, "DonGia");
            string tonStr = Fxml.LayGiaTri("Hang.xml", "MaHang", maHang, "SoLuong");
            donViTinh = Fxml.LayGiaTri("Hang.xml", "MaHang", maHang, "DonViTinh");

            donGia = long.TryParse(dgStr, out long dg) ? dg : 0;
            tonKho = int.TryParse(tonStr, out int tk) ? tk : 0;

            return !string.IsNullOrEmpty(tenHang) && donGia > 0;
        }
        #endregion

    }
}
