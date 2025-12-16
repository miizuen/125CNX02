using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;

namespace QuanLyTraiCay.Class
{
    class FileXml
    {
        string Conn = @"Data Source=localhost\\SQLEXPRESS; Initial Catalog = QuanLyTraiCay; Integrated Security=true";
        //public DataTable HienThi(string duongDan)
        //{
        //    DataTable dt = new DataTable();
        //    string FilePath = Application.StartupPath + "\\" + duongDan;
        //    if (File.Exists(FilePath))
        //    {
        //        FileStream fsReadXML = new FileStream(FilePath, FileMode.Open);
        //        dt.ReadXml(fsReadXML);
        //        fsReadXML.Close();
        //    }
        //    else
        //    {
        //        MessageBox.Show("File XML '" + duongDan + "' không tồn tại");
        //    }

        //    return dt;
        //}
        public DataTable HienThi(string file)
        {
            DataTable dt = new DataTable();
            string FilePath = Application.StartupPath + "\\" + file;
            if (File.Exists(FilePath))
            {
                FileStream fsReadXML = new FileStream(FilePath, FileMode.Open);
                dt.ReadXml(fsReadXML);
                fsReadXML.Close();
            }
            else
            {
                MessageBox.Show("File XML '" + file + "' không tồn tại");
            }

            return dt;
        }
        public void TaoXML(string bang)
        {
            SqlConnection con = new SqlConnection(Conn);
            con.Open();
            string sql = "Select* from " + bang;
            SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable("'" + bang + "'");
            ad.Fill(dt);
            dt.WriteXml(Application.StartupPath + "\\" + bang + ".xml", XmlWriteMode.WriteSchema);
        }

        public void Them(string duongDan, string noiDung)
        {
            XmlTextReader reader = new XmlTextReader(duongDan);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            XmlNode currNode;
            XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
            docFrag.InnerXml = noiDung;
            currNode = doc.DocumentElement;
            currNode.InsertAfter(docFrag, currNode.LastChild);
            doc.Save(duongDan);
        }
        public void Xoa(string duongDan, string tenFileXML, string xoaTheoTruong, string giaTriTruong)
        {
            string fileName = Application.StartupPath + "\\" + duongDan;
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName); 
            XmlNode nodeCu = doc.SelectSingleNode("NewDataSet/" + "_x0027_" + tenFileXML+ "_x0027_"+ "[./" + xoaTheoTruong + "/text()='" + giaTriTruong + "']");
            doc.DocumentElement.RemoveChild(nodeCu);
            doc.Save(fileName);
        }

        public void Sua(string duongDan, string tenFile, string suaTheoTruong, string giaTriTruong, string noiDung)
        {

            XmlTextReader reader = new XmlTextReader(duongDan);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            XmlNode oldHang;
            XmlElement root = doc.DocumentElement;
            oldHang = doc.SelectSingleNode("NewDataSet/" + "_x0027_" + tenFile + "_x0027_" + "[./" + suaTheoTruong + "/text()='" + giaTriTruong + "']");
          //  oldHang = root.SelectSingleNode("/NewDataSet/" + tenFile + "[" + suaTheoTruong + "='" + giaTriTruong + "']");
            XmlElement newhang = doc.CreateElement("_x0027_" + tenFile + "_x0027_");
            newhang.InnerXml = noiDung;
            root.ReplaceChild(newhang, oldHang);
            doc.Save(duongDan);

        }
        public string LayGiaTri(string duongDan, string truongA, string giaTriA, string truongB)
        {
            string giatriB = "";
            DataTable dt = HienThi(duongDan);
            foreach (DataRow row in dt.Rows)
            {
                if (row[truongA].ToString().Trim() == giaTriA.Trim())
                {
                    giatriB = row[truongB].ToString().Trim();
                    break;
                }
            }
            return giatriB;
        }
        public void DoiMatKhau(string nguoiDung, string matKhau)
        {
            XmlDocument doc1 = new XmlDocument();
            doc1.Load(Application.StartupPath + "\\TaiKhoan.xml");
            XmlNode node1 = doc1.SelectSingleNode("NewDataSet/TaiKhoan[MaNhanVien = '" + nguoiDung + "']");
            if (node1 != null)
            {
                node1.ChildNodes[1].InnerText = matKhau;
                doc1.Save(Application.StartupPath + "\\TaiKhoan.xml");
            }
        }
        public void TimKiemXSLT(string data, string tenFileXML, string tenfileXSLT)
        {

            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load("\\" + tenfileXSLT + ".xslt");
            // Create the XsltArgumentList.
            XsltArgumentList argList = new XsltArgumentList();
            // Calculate the discount date.
            argList.AddParam("Data", "", data);
            // Create an XmlWriter to write the output.             
            XmlWriter writer = XmlWriter.Create("\\" + tenFileXML + ".html");
            // Transform the file.
            xslt.Transform(new XPathDocument("\\" + tenFileXML + ".xml"), argList, writer);
            writer.Close();
            System.Diagnostics.Process.Start("\\" + tenFileXML + ".html");

        }
        public void InsertOrUpDateSQL(string sql)
        {
            SqlConnection con = new SqlConnection(Conn);
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
