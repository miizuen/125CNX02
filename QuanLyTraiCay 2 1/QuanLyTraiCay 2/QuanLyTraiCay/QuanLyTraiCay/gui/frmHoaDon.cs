using QuanLyTraiCay.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTraiCay.gui
{
    public partial class frmHoaDon : Form
    {
        int targetX = 200;
        int targetY = 300;
        int targetZ = 400;

        private HoaDon hoaDon; 
        public frmHoaDon()
        {
            InitializeComponent();
            hoaDon = new HoaDon();
            dtgHoaDon.DataSource = hoaDon.DtChiTiet;

            // Gán sự kiện
            btnThem.Click += btnThem_Click;
            btnXoa.Click += btnXoa_Click;
            btnThanhToan.Click += btnThanhToan_Click;
            btnThoat.Click += btnThoat_Click;
            txtSoLuong.TextChanged += txtSoLuong_TextChanged;
            txtMaHang.KeyPress += txtMaHang_KeyPress;
            dtgHoaDon.SelectionChanged += dtgHoaDon_SelectionChanged;
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            btnThem.Left = -btnThem.Width;
            btnXoa.Left = -btnXoa.Width;// ẩn bên trái
            btnLuu.Left = -btnLuu.Width;
            timer1.Start();
            txtSoHoaDon.Text = hoaDon.SoHoaDonHienTai.ToString();
            txtSoHoaDon.ReadOnly = true;
            dateTimePicker1.Value = DateTime.Now;
            txtMaHang.Focus();
        }

        private void txtMaHang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                string maHang = txtMaHang.Text.Trim();
                if (string.IsNullOrEmpty(maHang)) return;

                if (hoaDon.LayThongTinHang(maHang, out string tenHang, out long donGia, out int tonKho, out string donVi))
                {
                    txtDonGia.Text = donGia.ToString("N0");
                    lblTenHang.Text = tenHang;
                    lblTonKho.Text = $"{tonKho} {donVi}";
                    txtSoLuong.Focus();
                    txtSoLuong.SelectAll();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy mã hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    XoaFormChiTiet();
                }
            }
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSoLuong.Text) && !string.IsNullOrEmpty(txtDonGia.Text))
            {
                if (int.TryParse(txtSoLuong.Text, out int sl) && long.TryParse(txtDonGia.Text.Replace(",", ""), out long dg))
                {
                    txtThanhTien.Text = (sl * dg).ToString("N0");
                }
                else
                {
                    txtThanhTien.Text = "0";
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maHang = txtMaHang.Text.Trim();
            if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (hoaDon.ThemHangVaoChiTiet(maHang, soLuong, out string tb))
            {
                txtTongCong.Text = hoaDon.TinhTongTien().ToString("N0");
                XoaFormChiTiet();
            }
            else
            {
                MessageBox.Show(tb, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtgHoaDon.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Xóa dòng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dtgHoaDon.SelectedRows)
                    {
                        if (!row.IsNewRow)
                        {
                            hoaDon.XoaDong(row.Index);
                        }
                    }
                    txtTongCong.Text = hoaDon.TinhTongTien().ToString("N0");
                }
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNhanVien.Text))
            {
                MessageBox.Show("Nhập mã nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Xác nhận thanh toán?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (hoaDon.ThanhToan(txtMaNhanVien.Text.Trim(), dateTimePicker1.Value, out string tb))
                {
                    MessageBox.Show(tb, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSoHoaDon.Text = hoaDon.SoHoaDonHienTai.ToString();
                    txtTongCong.Text = "0";
                    XoaFormChiTiet();
                    txtMaNhanVien.Clear();
                    dateTimePicker1.Value = DateTime.Now;
                }
                else
                {
                    MessageBox.Show(tb, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (hoaDon.DtChiTiet.Rows.Count > 0)
            {
                if (MessageBox.Show("Hóa đơn chưa thanh toán. Thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    this.Close();
            }
            else
            {
                this.Close();
            }
        }

        private void XoaFormChiTiet()
        {
            txtMaHang.Clear();
            txtDonGia.Clear();
            txtSoLuong.Clear();
            txtThanhTien.Clear();
            lblTenHang.Text = "";
            lblTonKho.Text = "0";
            txtMaHang.Focus();
        }

        private void dtgHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            // Có thể thêm: chọn dòng → hiện lên form để sửa
        }

        private void dtgHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnThem.Left += 2; // tốc độ trượt
            btnXoa.Left += 5;
            btnLuu.Left += 7;
            if (btnLuu.Left >= targetZ)
            {
                btnLuu.Left = targetZ;
                timer1.Stop();
            }
            if (btnXoa.Left >= targetY)
            {
                btnXoa.Left = targetY;
                timer1.Stop();
            }
            if (btnThem.Left >= targetX) { 
                btnThem.Left = targetX;
                timer1.Stop();
            }
        }
    }
}
