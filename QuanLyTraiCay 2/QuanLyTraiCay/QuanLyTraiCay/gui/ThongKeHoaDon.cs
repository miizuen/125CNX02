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
    public partial class ThongKeHoaDon : Form
    {

        private Class.ThongKeHoaDon thongKeHD;

        public ThongKeHoaDon()
        {
            InitializeComponent();
            thongKeHD = new Class.ThongKeHoaDon();
        }

        private void ThongKeHoaDon_Load(object sender, EventArgs e)
        {
            try
            {
                dtgHoaDon.DataSource = thongKeHD.GetHoaDon();
                dtgHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dtgHoaDon.SelectionChanged += dtgHoaDon_SelectionChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void dtgHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            if (dtgHoaDon.CurrentRow == null)
                return;

            try
            {
                int soHD = Convert.ToInt32(dtgHoaDon.CurrentRow.Cells["SoHoaDon"].Value);
                DataTable dtChiTiet = thongKeHD.GetChiTietBySoHoaDon(soHD);

                if (dtChiTiet != null)
                {
                    dtgChiTietHoaDon.DataSource = dtChiTiet;
                    dtgChiTietHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    dtgChiTietHoaDon.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị chi tiết: " + ex.Message);
            }
        }
    }
}
