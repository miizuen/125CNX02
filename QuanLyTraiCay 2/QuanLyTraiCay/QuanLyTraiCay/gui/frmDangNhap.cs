using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLyTraiCay.Class;

namespace QuanLyTraiCay.gui
{
    public partial class frmDangNhap : Form
    {
        FileXml Fxml = new FileXml();
        DangNhap dn = new DangNhap();
        int targetX = 180; 
        int targetY = 200;
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtTenDN.Text.Equals("") || txtMK.Text.Equals(""))
            {
                MessageBox.Show("Không được bỏ trống các trường!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtTenDN.Focus();
            }
            else
            {

                if (dn.kiemtraTTDN("TaiKhoan.xml",txtTenDN.Text, txtMK.Text) == true)
                {
                    MessageBox.Show("Đăng nhập thành công");
                    dn.layMaQuyen();
                    frmMain.tenDNMain = txtTenDN.Text;
                    frmMain frm = (frmMain)Application.OpenForms["frmMain"];
                    if (frm != null)
                    {
                        frm.QuyenDangNhap(true);
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenDN.Text = "";
                    txtMK.Text = "";
                    txtTenDN.Focus();
                }
            }
        }
        public string HoTen(string MaNhanVien)
        {
            return Fxml.LayGiaTri("NhanVien.xml", "MaNhanVien", MaNhanVien, "TenNhanVien"); ;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnDangNhap.Left += 2; // tốc độ trượt
            btnThoat.Left += 5;
            if (btnThoat.Left >= targetY)
            {
                btnThoat.Left = targetY;
                timerMove.Stop();
            }
            if (btnDangNhap.Left >= targetX)
            {
                btnDangNhap.Left = targetX;
                timerMove.Stop();
            }
            
        }
        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            btnDangNhap.Left = -btnDangNhap.Width;
            btnThoat.Left = -btnThoat.Width;// ẩn bên trái
            timerMove.Start();
        }
    }
}
