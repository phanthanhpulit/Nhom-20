﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop;

namespace MobileDesktop
{
    public partial class FromThongKeSP : Form
    {

        ServiceDonHang.ServiceDonHangClient svdh = new ServiceDonHang.ServiceDonHangClient();
        ServiceDonHang.Thongke svtk = new ServiceDonHang.Thongke();
        ServiceDonHang.CT_DonHang ctdh = new ServiceDonHang.CT_DonHang();

        public FromThongKeSP()
        {
            InitializeComponent();
            loadNowTime();
            //            loadChangeTime();
        }
        private void loadNowTime()
        {
            DateTime dt = DateTime.Now;
            string txt = dt.ToString();
            txtNgay1.Text = "01/" + dt.Month.ToString() + "/" + dt.Year.ToString();
            txtNgay2.Text = dt.ToShortDateString();
            //dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString();
        }

        private void loadChangeTime()
        {
            DateTime _ngay1 = DateTime.Parse(txtNgay1.Text);
            String Ngay1 = _ngay1.ToShortDateString();
            // _ngay1.Day + "/" + _ngay1.Month + "/" + _ngay1.Year;
            //_ngay1.Year + "-" + _ngay1.Month + "-" + _ngay1.Day;
            DateTime _ngay2 = DateTime.Parse(txtNgay2.Text);
            String Ngay2 = _ngay2.ToShortDateString();
            //_ngay2.Year + "/" + _ngay2.Month + "/" + _ngay2.Day;

            //            dataGridView1.DataSource = svtk.BaocaoNgay(Ngay1, Ngay2);
            dataGridView1.DataSource = svdh.BaocaoNgay(Ngay1, Ngay2);
            //            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //
            TongTien();
        }

        private void txtNgay1_TextChanged(object sender, EventArgs e)
        {
            if (txtNgay2.Text != "")
            {
                loadChangeTime();
            }
        }

        private void txtNgay2_TextChanged(object sender, EventArgs e)
        {
            if (txtNgay1.Text != "")
            {
                loadChangeTime();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            txtNgay1.Text = dateTimePicker1.Value.Day.ToString() + "/" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Year.ToString();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            txtNgay2.Text = dateTimePicker2.Value.Day.ToString() + "/" + dateTimePicker2.Value.Month.ToString() + "/" + dateTimePicker2.Value.Year.ToString();
        }

        public void TongTien()
        {
            try
            {
                int sum = 0;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    sum = sum + int.Parse(dataGridView1.Rows[i].Cells["ThanhTien"].Value.ToString());
                }
                lbTongtien.Text = sum.ToString();
            }
            catch
            {
                lbTongtien.Text = "";
            }
        }

        //---Báo cáo-----------------------------------------------------------------------------------------------------------------
        public void Export(DataTable dt, String sheetName, String title)
        {
            //Tạo các đối tượng Excel

            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbooks oBooks;

            Microsoft.Office.Interop.Excel.Sheets oSheets;

            Microsoft.Office.Interop.Excel.Workbook oBook;

            Microsoft.Office.Interop.Excel.Worksheet oSheet;

            //Tạo mới một Excel WorkBook 

            oExcel.Visible = true;

            oExcel.DisplayAlerts = false;

            oExcel.Application.SheetsInNewWorkbook = 1;

            oBooks = oExcel.Workbooks;

            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));

            oSheets = oBook.Worksheets;

            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);

            oSheet.Name = sheetName;

            //Tiêu đề tên công ty
            Microsoft.Office.Interop.Excel.Range CTY = oSheet.get_Range("A1", "B1");
            CTY.MergeCells = true;
            CTY.Value2 = "CÔNG TY CỔ PHẦN SHOPMOBILE";
            CTY.ColumnWidth = 20;
            CTY.Font.Name = "Tahoma";
            CTY.Font.Size = "10";
            CTY.Font.Bold = true;

            //Tiêu đề địa chỉ công ty
            Microsoft.Office.Interop.Excel.Range add = oSheet.get_Range("A2", "B2");
            add.MergeCells = true;
            add.Value2 = "273 An Dương Vương Q5 TP. Hồ Chí Minh";
            add.ColumnWidth = 18;
            add.Font.Name = "Tahoma";
            add.Font.Size = "8";

            //Tiêu đề mã phiếu báo cáo
            Microsoft.Office.Interop.Excel.Range maphieu = oSheet.get_Range("F1", "F1");
            maphieu.MergeCells = true;
            maphieu.Value2 = "Mẫu số B 09-BH";
            maphieu.Font.Bold = true;
            maphieu.ColumnWidth = 18;
            maphieu.Font.Name = "Tahoma";
            maphieu.Font.Size = "8";

            //Tiêu đề banhanh báo cáo
            Microsoft.Office.Interop.Excel.Range banhanh = oSheet.get_Range("F2", "F4");
            banhanh.MergeCells = true;
            banhanh.Value2 = "Ban hành theo QĐ số 07/2003/QĐ - BTC ngày 17/01/2013 của Bộ Tài Chính";
            banhanh.WrapText = true;
            banhanh.ColumnWidth = 18;
            banhanh.Font.Name = "Tahoma";
            banhanh.Font.Size = "8";
            //banhanh.Width = "32";

            // Tạo phần đầu nếu muốn

            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("B5", "E5");

            head.MergeCells = true;

            head.Value2 = title;

            head.Font.Bold = true;

            head.Font.Name = "Tahoma";

            head.Font.Size = "16";

            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo phần đầu nếu muốn

            Microsoft.Office.Interop.Excel.Range TG = oSheet.get_Range("C6", "E6");

            TG.MergeCells = true;

            //TG.Value2 = thoiGian;

            TG.Font.Bold = true;

            TG.Font.Name = "Tahoma";

            TG.Font.Size = "10";

            TG.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo tiêu đề cột 

            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("B8", "B8");

            cl1.Value2 = "Đơn giá";

            cl1.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("C8", "C8");

            cl2.Value2 = "Mã sản phẩm";

            cl2.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("D8", "D8");

            cl3.Value2 = "Số lượng";

            cl3.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("E8", "E8");

            cl4.Value2 = "Tên sản phẩm";

            cl4.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("F8", "F8");

            cl5.Value2 = "Thành tiền";

            cl5.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("B8", "F8");

            rowHead.Font.Bold = true;

            // Kẻ viền

            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            // Thiết lập màu nền

            rowHead.Interior.ColorIndex = 15;

            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo mẳng đối tượng để lưu dữ toàn bồ dữ liệu trong DataTable,

            // vì dữ liệu được được gán vào các Cell trong Excel phải thông qua object thuần.

            object[,] arr = new object[dt.Rows.Count, dt.Columns.Count];

            //Chuyển dữ liệu từ DataTable vào mảng đối tượng

            for (int r = 0; r < dt.Rows.Count; r++)
            {

                DataRow dr = dt.Rows[r];

                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    arr[r, c] = dr[c];
                }
            }

            //TongTien------------------------------------------------------------
            //arr[dt.Rows.Count, dt.Columns.Count] = "Tổng tiền:"; //lb_TongTien.Text
            //arr[dt.Rows.Count, dt.Columns.Count - 1] = "=SUM(F9:F" + (dt.Rows.Count + 9) + ")"; // rowEnd - 1

            //Thiết lập vùng điền dữ liệu

            int rowStart = 9;

            int columnStart = 2;

            int rowEnd = rowStart + dt.Rows.Count - 1;

            int columnEnd = dt.Columns.Count + 1;

            //định dạng dấu "," ở cột Đơn giá, Thành tiền và Tổng tiền
            Microsoft.Office.Interop.Excel.Range DonGia = oSheet.get_Range("B8", "B" + rowEnd);
            Microsoft.Office.Interop.Excel.Range ThanhTien = oSheet.get_Range("F8", "F" + rowEnd);
            DonGia.NumberFormat = "#,###";
            ThanhTien.NumberFormat = "#,###";

            // Ô bắt đầu điền dữ liệu

            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];

            // Ô kết thúc điền dữ liệu

            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];

            // Lấy về vùng điền dữ liệu

            Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);

            //Điền dữ liệu vào vùng đã thiết lập

            range.Value2 = arr;

            // Kẻ viền

            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            // Căn giữa cột STT

            Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnStart];

            Microsoft.Office.Interop.Excel.Range c4 = oSheet.get_Range(c1, c3);

            oSheet.get_Range(c3, c4).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Ngày lập báo cáo
            Microsoft.Office.Interop.Excel.Range c5 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd + 2, columnEnd];
            Microsoft.Office.Interop.Excel.Range c6 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd + 2, columnEnd+1];
            Microsoft.Office.Interop.Excel.Range ngaylap = oSheet.get_Range(c5, c6);
            ngaylap.MergeCells = true;
            ngaylap.Value2 = "TP. Hồ Chí Minh ngày ..., tháng ..., năm ...";
            ngaylap.WrapText = true;
            ngaylap.ColumnWidth = 18;
            ngaylap.Font.Name = "Tahoma";
            ngaylap.Font.Size = "8";
            ngaylap.Font.Italic = true;

            //Giám đốc
            Microsoft.Office.Interop.Excel.Range c7 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd + 3, columnEnd];
            Microsoft.Office.Interop.Excel.Range c8 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd + 3, columnEnd + 1];
            Microsoft.Office.Interop.Excel.Range giamdoc = oSheet.get_Range(c7, c8);
            giamdoc.MergeCells = true;
            giamdoc.Value2 = "Giám đốc";
            giamdoc.WrapText = true;
            giamdoc.Font.Bold = true;
            giamdoc.ColumnWidth = 18;
            giamdoc.Font.Name = "Tahoma";
            giamdoc.Font.Size = "8";
            oSheet.get_Range(c7, c8).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Giám đốc ký
            Microsoft.Office.Interop.Excel.Range c9 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd + 4, columnEnd];
            Microsoft.Office.Interop.Excel.Range c10 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd + 4, columnEnd + 1];
            Microsoft.Office.Interop.Excel.Range giamdocky = oSheet.get_Range(c9, c10);
            giamdocky.MergeCells = true;
            giamdocky.Value2 = "(Chữ ký, họ tên, đóng dấu)";
            giamdocky.WrapText = true;
            giamdocky.ColumnWidth = 18;
            giamdocky.Font.Name = "Tahoma";
            giamdocky.Font.Size = "8";
            oSheet.get_Range(c9, c10).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Người lập
            Microsoft.Office.Interop.Excel.Range c11 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd + 3, columnStart];
            Microsoft.Office.Interop.Excel.Range c12 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd + 3, columnStart];
            Microsoft.Office.Interop.Excel.Range nguoilap = oSheet.get_Range(c11, c12);
            nguoilap.MergeCells = true;
            nguoilap.Value2 = "Người lập biểu";
            nguoilap.WrapText = true;
            nguoilap.Font.Bold = true;
            nguoilap.ColumnWidth = 20;
            nguoilap.Font.Name = "Tahoma";
            nguoilap.Font.Size = "8";
            oSheet.get_Range(c11, c12).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Người lập ký
            Microsoft.Office.Interop.Excel.Range c13 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd + 4, columnStart];
            Microsoft.Office.Interop.Excel.Range c14 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd + 4, columnStart];
            Microsoft.Office.Interop.Excel.Range nguoilapky = oSheet.get_Range(c13, c14);
            nguoilapky.MergeCells = true;
            nguoilapky.Value2 = "(Chữ ký, họ tên)";
            nguoilapky.WrapText = true;
            nguoilapky.ColumnWidth = 20;
            nguoilapky.Font.Name = "Tahoma";
            nguoilapky.Font.Size = "8";
            oSheet.get_Range(c13, c14).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Người lập
            Microsoft.Office.Interop.Excel.Range c15 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd + 3, columnStart + 2];
            Microsoft.Office.Interop.Excel.Range c16 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd + 3, columnStart + 2];
            Microsoft.Office.Interop.Excel.Range truongphong = oSheet.get_Range(c15, c16);
            truongphong.MergeCells = true;
            truongphong.Value2 = "Trưởng phòng KH - TC";
            truongphong.WrapText = true;
            truongphong.Font.Bold = true;
            truongphong.ColumnWidth = 20;
            truongphong.Font.Name = "Tahoma";
            truongphong.Font.Size = "8";
            oSheet.get_Range(c15, c16).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Người lập ký
            Microsoft.Office.Interop.Excel.Range c17 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd + 4, columnStart + 2];
            Microsoft.Office.Interop.Excel.Range c18 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd + 4, columnStart + 2];
            Microsoft.Office.Interop.Excel.Range truongphongky = oSheet.get_Range(c17, c18);
            truongphongky.MergeCells = true;
            truongphongky.Value2 = "(Chữ ký, họ tên)";
            truongphongky.WrapText = true;
            truongphongky.ColumnWidth = 20;
            truongphongky.Font.Name = "Tahoma";
            truongphongky.Font.Size = "8";
            oSheet.get_Range(c17, c18).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

        }
        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            var dt = new DataTable();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Visible)
                {
                    dt.Columns.Add();
                }
            }

            object[] cellValues = new object[dgv.Columns.Count];
            foreach (DataGridViewRow row in dgv.Rows)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    cellValues[i] = row.Cells[i].Value;
                }
                dt.Rows.Add(cellValues);
            }

            return dt;

        }
        private void btnXuatbaocao_Click(object sender, EventArgs e)
        {
            try
            {
                if (DateTime.Parse(txtNgay1.Text) <= DateTime.Parse(txtNgay2.Text))
                {
                    FromThongKeSP excel = new FromThongKeSP();
                    DataTable dt = GetDataTableFromDGV(dataGridView1);
                    excel.Export(dt, "Bao cao", "BÁO CÁO BÁN HÀNG: " + txtNgay1.Text + " - " + txtNgay2.Text);
                }
                else
                    MessageBox.Show(txtNgay1.Text + " - " + txtNgay2.Text + " không hợp lệ!");
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng thao tác lại!");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            FormMenuThongKe frm = new FormMenuThongKe();
            this.Visible = false;
            frm.Visible = true;
        }
    }
}
