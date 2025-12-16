using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;

namespace QuanLyTraiCay.Class
{
    class NhaCungCap
    {
        FileXml Fxml = new FileXml();
        public bool kiemtraMaNCC(string MaNCC)
        {
            XmlTextReader reader = new XmlTextReader("NhaCungCap.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            XmlNode node = doc.SelectSingleNode("NewDataSet/NhaCungCap[MaNCC='" + MaNCC + "']");
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
        public void themNCC(string MaNCC, string TenNCC, string DiaChi, string SDT, string Email, string MoTa)
        {
            string noiDung = "<NhaCungCap>" +
                    "<MaNCC>" + MaNCC + "</MaNCC>" +
                    "<TenNCC>" + TenNCC + "</TenNCC>" +
                    "<DiaChi>" + DiaChi + "</DiaChi>" +
                    "<SDT>" + SDT + "</SDT>" +
                    "<Email>" + Email + "</Email>" +
                    "<MoTa>" + MoTa + "</MoTa>" +
                    "</NhaCungCap>";
            Fxml.Them("NhaCungCap.xml", noiDung);
        }
        public void suaNCC(string MaNCC, string TenNCC, string DiaChi, string SDT, string Email, string MoTa)
        {

            string noiDung = "<MaNCC>" + MaNCC + "</MaNCC>" +
                    "<TenNCC>" + TenNCC + "</TenNCC>" +
                    "<DiaChi>" + DiaChi + "</DiaChi>" +
                    "<SDT>" + SDT + "</SDT>" +
                    "<Email>" + Email + "</Email>" +
                    "<MoTa>" + MoTa + "</MoTa>";

            Fxml.Sua("NhaCungCap.xml", "NhaCungCap", "MaNCC", MaNCC, noiDung);

        }
        public void xoaNCC(string MaNCC)
        {
            Fxml.Xoa("NhaCungCap.xml", "NhaCungCap", "MaNCC", MaNCC);
        }
        public DataTable LoadMaNCC()
        {
            DataTable dt = new DataTable();
            dt = Fxml.HienThi("NhaCungCap.xml");
            return dt;
        }
        public DataTable LoadTable()
        {
            DataTable dt = new DataTable();
            dt = Fxml.HienThi("NhaCungCap.xml");
            DataTable dtNCC = new DataTable();;
            dtNCC = LoadMaNCC();
            int soDong = LoadMaNCC().Rows.Count;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < soDong; j++)
                {
                    if (dt.Rows[i]["MaNCC"].ToString().Equals(dtNCC.Rows[j]["MaNCC"].ToString()))
                    {
                        dt.Rows[i]["MaNCC"] = dtNCC.Rows[j]["TenNCC"];
                    }
                }
            }
          
            return dt;
        }
    }
}
