namespace QuanLyTraiCay.gui
{
    partial class ThongKeHoaDon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtgHoaDon = new System.Windows.Forms.DataGridView();
            this.dtgChiTietHoaDon = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgHoaDon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgChiTietHoaDon)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgHoaDon
            // 
            this.dtgHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgHoaDon.Location = new System.Drawing.Point(34, 75);
            this.dtgHoaDon.Name = "dtgHoaDon";
            this.dtgHoaDon.RowHeadersWidth = 62;
            this.dtgHoaDon.RowTemplate.Height = 28;
            this.dtgHoaDon.Size = new System.Drawing.Size(1071, 263);
            this.dtgHoaDon.TabIndex = 0;
            // 
            // dtgChiTietHoaDon
            // 
            this.dtgChiTietHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgChiTietHoaDon.Location = new System.Drawing.Point(34, 370);
            this.dtgChiTietHoaDon.Name = "dtgChiTietHoaDon";
            this.dtgChiTietHoaDon.RowHeadersWidth = 62;
            this.dtgChiTietHoaDon.RowTemplate.Height = 28;
            this.dtgChiTietHoaDon.Size = new System.Drawing.Size(1071, 263);
            this.dtgChiTietHoaDon.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(389, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(346, 36);
            this.label1.TabIndex = 2;
            this.label1.Text = "THỐNG KÊ HÓA ĐƠN";
            // 
            // ThongKeHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1142, 656);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtgChiTietHoaDon);
            this.Controls.Add(this.dtgHoaDon);
            this.Name = "ThongKeHoaDon";
            this.Text = "ThongKeHoaDon";
            this.Load += new System.EventHandler(this.ThongKeHoaDon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgHoaDon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgChiTietHoaDon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgHoaDon;
        private System.Windows.Forms.DataGridView dtgChiTietHoaDon;
        private System.Windows.Forms.Label label1;
    }
}