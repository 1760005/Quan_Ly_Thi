using Microsoft.Reporting.WinForms;
using Quan_Ly_Thi.BUS;
using Quan_Ly_Thi.DAO;
using Quan_Ly_Thi.DTO;
using Quan_Ly_Thi.GUI.Hoc_Sinh;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Thi.GUI.Giao_Vien
{
    public partial class frmGiao_Vien : Form
    {
        //Đối tượng giáo viên
        public static Giao_Vienn giaoVien = new Giao_Vienn();
        public Hoc_Sinhh hocSinh = new Hoc_Sinhh();

        //Binding
        BindingSource bsDanhSachKyThi = new BindingSource(); //DanhSachKyThi
        BindingSource bsCbbTenKyThi = new BindingSource(); //DanhSachKyThi
        BindingSource bsReportDanhSachThiSinh = new BindingSource(); //ReportDanhSachThiSinh
        BindingSource bsReportTongKetKyThi = new BindingSource(); //ReportTongKetKyThi;

        ReportDataSource rdsDanhSachThiSinh; //ReportDanhSachThiSinh
        ReportDataSource rdsTongKetKyThi; //ReportTongKetKyThi;

       
        public frmGiao_Vien()
        {
            InitializeComponent();
            TabControlTeacher.TabPages.Clear(); 
            tsmiDanhSachKetQua.Click += TsmiDanhSachKetQua_Click; //toolStripMenuItemDanhSachKetQua
            tsmiDanhSachDeThi.Click += TsmiDanhSachDeThi_Click;
            tsmiDanhSachKyThi.Click += TsmiDanhSachKyThi_Click;
            tsmiDanhSachThiSinh.Click += TsmiDanhSachThiSinh_Click;
            tsmiThongTinCaNhan.Click += TsmiThongTinCaNhan_Click;
            tsmiDanhSachHocSinh.Click += TsmiDanhSachHocSinh_Click;
            tsmiReportDanhSachThiSinh.Click += TsmiReportDanhSachThiSinh_Click;
            tsmiReportTongKetKyThi.Click += TsmiReportTongKetKyThi_Click;

        }


        //Xử lý tabpage report tổng kết kỳ thi
        private void TsmiReportTongKetKyThi_Click(object sender, EventArgs e)
        {
            if (TabControlTeacher.TabPages.Contains(tabPageReportTongKetKyThi))
            {
                TabControlTeacher.SelectedTab = tabPageReportTongKetKyThi;
                return;
            }
            else
                TabControlTeacher.TabPages.Add(tabPageReportTongKetKyThi);
            TabControlTeacher.SelectedTab = tabPageReportTongKetKyThi;

            cbbTenKyThiReportTongKetKyThi.DataSource = BUS_Giao_Vien.layTatCaKiThiDaHoanThanh();
            cbbTenKyThiReportTongKetKyThi.DisplayMember = "TenKyThi";
            cbbTenKyThiReportTongKetKyThi.ValueMember = "MaKyThi";

            btnReportTongKetKyThi.Click += BtnReportTongKetKyThi_Click;
            btnDongTabPageReportTongKetKyThi.Click += BtnDongTabPageReportTongKetKyThi_Click;
        }

        private void BtnReportTongKetKyThi_Click(object sender, EventArgs e)
        {
            rpTongKetKyThi.LocalReport.DataSources.Clear();
            if (cbbTenKyThiReportTongKetKyThi.SelectedValue != null)
            {
                KYTHI kithi = new KYTHI();
                kithi = cbbTenKyThiReportTongKetKyThi.SelectedItem as KYTHI;

                rdsTongKetKyThi = new ReportDataSource();
                rpTongKetKyThi.ProcessingMode = ProcessingMode.Local;
                //rpDanhSachThiSinh.LocalReport.ReportPath = "Quan_Ly_Thi.GUI.Giao_Vien.ReportDanhSachThiSinh.rdlc";

                ReportParameterCollection rpcTenKyThi = new ReportParameterCollection();
                rpcTenKyThi.Add(new ReportParameter("ReportParameterTenKyThi", kithi.TenKyThi.ToUpper()));
                rpTongKetKyThi.LocalReport.SetParameters(rpcTenKyThi);

                rdsTongKetKyThi.Name = "dataSetReportTongKetKyThi";
                bsReportTongKetKyThi.DataSource = BUS_Giao_Vien.reportTongKetKyThi(kithi.MaKyThi);
                rdsTongKetKyThi.Value = bsReportTongKetKyThi;
                rpTongKetKyThi.LocalReport.DataSources.Add(rdsTongKetKyThi);
                this.rpTongKetKyThi.RefreshReport();
            }
        }

        private void BtnDongTabPageReportTongKetKyThi_Click(object sender, EventArgs e)
        {
            TabControlTeacher.TabPages.RemoveByKey("tabPageReportTongKetKyThi");
        }


        //Xử lý tabpage report danh sách thí sinh
        private void TsmiReportDanhSachThiSinh_Click(object sender, EventArgs e)
        {
            if (TabControlTeacher.TabPages.Contains(tagPageReportDanhSachThiSinh))
            {
                TabControlTeacher.SelectedTab = tagPageReportDanhSachThiSinh;
                return;
            }
            else
                TabControlTeacher.TabPages.Add(tagPageReportDanhSachThiSinh);
            TabControlTeacher.SelectedTab = tagPageReportDanhSachThiSinh;

            cbbTenKyThiReportDanhSachThiSinh.DataSource = BUS_Giao_Vien.layTatCaKiThi();
            cbbTenKyThiReportDanhSachThiSinh.DisplayMember = "TenKyThi";
            cbbTenKyThiReportDanhSachThiSinh.ValueMember = "MaKyThi";

            
            btnDongTabPageReportDanhSachThiSinh.Click += BtnDongTabPageReportDanhSachThiSinh_Click;
            btnReportDanhSachThiSinh.Click += BtnReportDanhSachThiSinh_Click;
        }

        private void BtnReportDanhSachThiSinh_Click(object sender, EventArgs e)
        {
            rpDanhSachThiSinh.LocalReport.DataSources.Clear();
            if (cbbTenKyThiReportDanhSachThiSinh.SelectedValue != null)
            {
                KYTHI kithi = new KYTHI();
                kithi = cbbTenKyThiReportDanhSachThiSinh.SelectedItem as KYTHI;

                rdsDanhSachThiSinh = new ReportDataSource();                
                rpDanhSachThiSinh.ProcessingMode = ProcessingMode.Local;
                //rpDanhSachThiSinh.LocalReport.ReportPath = "Quan_Ly_Thi.GUI.Giao_Vien.ReportDanhSachThiSinh.rdlc";

                ReportParameterCollection rpcTenKyThi = new ReportParameterCollection();
                rpcTenKyThi.Add(new ReportParameter("ReportParameterTenKyThi", kithi.TenKyThi.ToUpper()));
                rpDanhSachThiSinh.LocalReport.SetParameters(rpcTenKyThi);

                rdsDanhSachThiSinh.Name = "dataSetReportDanhSachThiSinh";               
                bsReportDanhSachThiSinh.DataSource = BUS_Giao_Vien.reportDanhSachThiSinh(kithi.MaKyThi);
                rdsDanhSachThiSinh.Value = bsReportDanhSachThiSinh;
                rpDanhSachThiSinh.LocalReport.DataSources.Add(rdsDanhSachThiSinh);
                this.rpDanhSachThiSinh.RefreshReport();
            }
        }

        private void BtnDongTabPageReportDanhSachThiSinh_Click(object sender, EventArgs e)
        {
            TabControlTeacher.TabPages.RemoveByKey("tagPageReportDanhSachThiSinh");
        }

        //Xử lý tabpage danh sách học sinh
        private void TsmiDanhSachHocSinh_Click(object sender, EventArgs e)
        {
            if (TabControlTeacher.TabPages.Contains(tabPageDanhSachHocSinh))
            {
                TabControlTeacher.SelectedTab = tabPageDanhSachHocSinh;
                return;
            }
            else
                TabControlTeacher.TabPages.Add(tabPageDanhSachHocSinh);

            TabControlTeacher.SelectedTab = tabPageDanhSachHocSinh;

            cbbLopDSHS.DataSource = BUS_Giao_Vien.layTatCaLopHoc();
            cbbLopDSHS.DisplayMember = "TenLop";
            cbbLopDSHS.ValueMember = "MaLop";            

            dgvDanhSachHocSinh.ColumnHeadersDefaultCellStyle.Font = new Font("Tohoma", 10.0F, FontStyle.Bold);
            dgvDanhSachHocSinh.DefaultCellStyle.Font = new Font("Tohoma", 8.25F, FontStyle.Regular);
            dgvDanhSachHocSinh.AutoGenerateColumns = false;
            dgvDanhSachHocSinh.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; //Chiều cao cao tự động đồng loạt theo nội dung
            dgvDanhSachHocSinh.DefaultCellStyle.WrapMode = DataGridViewTriState.True; //Tự động xuống dòng
            dgvDanhSachHocSinh.DataSource = BUS_Giao_Vien.danhSachHocSinh(cbbLopDSHS.SelectedValue.ToString());


            btnLocDSHS.Click += BtnLocDSHS_Click;
            btnDongTabPageDanhSachHocSinh.Click += BtnDongTabPageDanhSachHocSinh_Click;
            btnTruyCapQuyenDSHS.Click += BtnTruyCapQuyenDSHS_Click;

        }

        private void BtnTruyCapQuyenDSHS_Click(object sender, EventArgs e)
        {
            if (BUS_Giao_Vien.danhSachHocSinh(cbbLopDSHS.SelectedValue.ToString()).Count > 0)
            {
                hocSinh = BUS_Tai_Khoan.layThongTinTaiKhoan(dgvDanhSachHocSinh.SelectedRows[0].Cells[0].Value.ToString()) as Hoc_Sinhh;
                frmHoc_Sinh frmHS = new frmHoc_Sinh();
                frmHoc_Sinh.hs = hocSinh;
                frmHS.Show();
            }
        }

        private void BtnDongTabPageDanhSachHocSinh_Click(object sender, EventArgs e)
        {
            TabControlTeacher.TabPages.RemoveByKey("tabPageDanhSachHocSinh");
        }

        private void BtnLocDSHS_Click(object sender, EventArgs e)
        {
            if (rbtnHocSinhDSHS.Checked)
            {
                if (!txtHocSinhDSHS.Text.Equals(""))
                    dgvDanhSachHocSinh.DataSource = BUS_Giao_Vien.danhSachHocSinhTheoThongTinHocSinh(cbbLopDSHS.SelectedValue.ToString(), txtHocSinhDSHS.Text);          
                else
                {
                    MessageBox.Show("Hãy nhập tên hoặc tài khoản của học sinh");
                    return;
                }
            }
            else
                dgvDanhSachHocSinh.DataSource = BUS_Giao_Vien.danhSachHocSinh(cbbLopDSHS.SelectedValue.ToString());
        }


        //Xử lý tab thông tin cá nhân
        private void TsmiThongTinCaNhan_Click(object sender, EventArgs e)
        {
            if (TabControlTeacher.TabPages.Contains(tabPageThongTinCaNhan))
            {
                TabControlTeacher.SelectedTab = tabPageThongTinCaNhan;
                return;
            }
            else
                TabControlTeacher.TabPages.Add(tabPageThongTinCaNhan);

            TabControlTeacher.SelectedTab = tabPageThongTinCaNhan;

            dienThongTinGiaoVienVaoFormThongTinCaNhan();

            btnDongTabpPagesThongTinCaNhan.Click += BtnDongTabpPagesThongTinCaNhan_Click;
            btnCapNhatTTCNGiaoVien.Click += BtnCapNhatTTCNGiaoVien_Click;
            
        }

        private void BtnCapNhatTTCNGiaoVien_Click(object sender, EventArgs e)
        {
            if (kiemTraFormThongTinCaNhanHopLe())
            {
                giaoVien.Ho_Ten = txtHoTenTTCN.Text;
                giaoVien.CMND_TCC = txtCMNDTTCN.Text;
                giaoVien.Ngay_Sinh = dtpNgaySinhTTCN.Value;
                giaoVien.SDT = txtSDTTTCN.Text;
                giaoVien.Email = txtEmailTTCN.Text;

                if (!BUS_Giao_Vien.capNhatNguoiDungVaoDB(giaoVien))
                {
                    MessageBox.Show("Cập nhật thất bại");
                    return;
                }
                else
                    dienThongTinGiaoVienVaoFormThongTinCaNhan();

            }
        }

        void dienThongTinGiaoVienVaoFormThongTinCaNhan()
        {
            if (giaoVien.Tai_Khoan != null)
            {
                //txtPhanQuyenTTCN.Text = BUS_Giao_Vien.layTenPhanQuyenTuongUng(giaoVien.Ma);
                txtTaiKhoanTTCN.Text = giaoVien.Tai_Khoan;
                txtKhoiTTCN.Text = giaoVien.Khoi;
                txtHoTenTTCN.Text = giaoVien.Ho_Ten;
                txtCMNDTTCN.Text = giaoVien.CMND_TCC;
                dtpNgaySinhTTCN.Value = giaoVien.Ngay_Sinh;
                txtSDTTTCN.Text = giaoVien.SDT;
                txtEmailTTCN.Text = giaoVien.Email;
            }
        }

        bool kiemTraFormThongTinCaNhanHopLe()
        {
            //string test;
            if (txtHoTenTTCN.Equals(""))
                MessageBox.Show("Họ tên không được để trống");
            else if (txtCMNDTTCN.Equals(""))
                MessageBox.Show("Chứng minh nhân dân Không được để trống");
            else if (txtSDTTTCN.Equals(""))
                MessageBox.Show("Số điện thoại không được để trống");
            else if (txtEmailTTCN.Equals(""))
                MessageBox.Show("Email không được để trống");
            else
                return true;
            return false;
        }

        private void BtnDongTabpPagesThongTinCaNhan_Click(object sender, EventArgs e)
        {
            TabControlTeacher.TabPages.RemoveByKey("tabPageThongTinCaNhan");
        }

        //Xử lý tabpages danh sách thí sinh

        private void TsmiDanhSachThiSinh_Click(object sender, EventArgs e)
        {
            //Mở tab
            if (TabControlTeacher.TabPages.Contains(tabPageDanhSachThiSinh))
            {
                TabControlTeacher.SelectedTab = tabPageDanhSachThiSinh;
                return;
            }              
            else
                TabControlTeacher.TabPages.Add(tabPageDanhSachThiSinh);
            TabControlTeacher.SelectedTab = tabPageDanhSachThiSinh;

            cbbTenKyThiDSTS.DataSource = BUS_Giao_Vien.layTatCaKiThi();
            cbbTenKyThiDSTS.DisplayMember = "TenKyThi";
            cbbTenKyThiDSTS.ValueMember = "MaKyThi";


            cbbKhoiDSKT.DataSource = BUS_Giao_Vien.layTatCaKhoi();
            cbbKhoiDSKT.DisplayMember = "TenKhoi";
            cbbKhoiDSKT.ValueMember = "MaKhoi";


            cbbMonHocDSKT.DataSource = BUS_Giao_Vien.layTatCaMonHoc();
            cbbMonHocDSKT.DisplayMember = "TenMonHoc";
            cbbMonHocDSKT.ValueMember = "MaMonHoc";

            dgvDanhSachThiSinh.ColumnHeadersDefaultCellStyle.Font = new Font("Tohoma", 10.0F, FontStyle.Bold);
            dgvDanhSachThiSinh.DefaultCellStyle.Font = new Font("Tohoma", 8.25F, FontStyle.Regular);
            dgvDanhSachThiSinh.AutoGenerateColumns = false;
            dgvDanhSachThiSinh.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; //Chiều cao cao tự động đồng loạt theo nội dung
            dgvDanhSachThiSinh.DefaultCellStyle.WrapMode = DataGridViewTriState.True; //Tự động xuống dòng

            btnLocDSTS.Click += BtnLocDSTS_Click;
            btnDongTabPagesDanhDanhSachThiSinh.Click += BtnDongTabPagesDanhDanhSachThiSinh_Click;
            //btnReportDanhSachThiSinh.Click += BtnReportDanhSachThiSinh_Click;

            //Hoat động
            btnReportDanhSachThiSinh.Enabled = false;
            this.rpDanhSachThiSinh.RefreshReport();
        }

        //private void BtnReportDanhSachThiSinh_Click(object sender, EventArgs e)
        //{
        //    if (TabControlTeacher.TabPages.Contains(tagpageReportDanhSachThiSinh))
        //    {
        //        TabControlTeacher.SelectedTab = tagpageReportDanhSachThiSinh;
        //        return;
        //    }              
        //    else
        //        TabControlTeacher.TabPages.Add(tagpageReportDanhSachThiSinh);
        //    TabControlTeacher.SelectedTab = tagpageReportDanhSachThiSinh;

        //    if (cbbTenKyThiDSTS.SelectedValue != null)
        //    {
        //        rdsDanhSachThiSinh = new ReportDataSource();
        //        rpDanhSachThiSinh.LocalReport.DataSources.Clear();
        //        rpDanhSachThiSinh.ProcessingMode = ProcessingMode.Local;
        //        //rpDanhSachThiSinh.LocalReport.ReportPath = "Quan_Ly_Thi.GUI.Giao_Vien.ReportDanhSachThiSinh.rdlc";
        //        rdsDanhSachThiSinh.Name = "dataSetDanhSachThiSinh";
        //        rdsDanhSachThiSinh.Value = bsDanhSachThiSinh;              
        //        rpDanhSachThiSinh.LocalReport.DataSources.Add(rdsDanhSachThiSinh);
        //        this.rpDanhSachThiSinh.RefreshReport();
        //    }           
        //}

        private void BtnDongTabPagesDanhDanhSachThiSinh_Click(object sender, EventArgs e)
        {
            TabControlTeacher.TabPages.RemoveByKey("tabPageDanhSachThiSinh");
        }

        private void BtnLocDSTS_Click(object sender, EventArgs e)
        {
            if (cbbTenKyThiDSTS.SelectedValue != null)
            {              
                dgvDanhSachThiSinh.DataSource = BUS_Giao_Vien.danhSachThiSinh(cbbTenKyThiDSTS.SelectedValue.ToString(), cbbKhoiDSKT.SelectedValue.ToString(), cbbMonHocDSKT.SelectedValue.ToString());
            }
        }

        //
        //Xử lý tabpages danh sách kỳ thi
        //
        private void TsmiDanhSachKyThi_Click(object sender, EventArgs e)
        {
            //Mở tab
            if (TabControlTeacher.TabPages.Contains(tabPagesThongTinKyThi))
            {
                TabControlTeacher.SelectedTab = tabPagesThongTinKyThi;
                return;
            }             
            else
                TabControlTeacher.TabPages.Add(tabPagesThongTinKyThi);

            TabControlTeacher.SelectedTab = tabPagesThongTinKyThi;

            //Load dữ liệu lên loại kỳ thi
            cbbLoaiKyThi.DataSource = BUS_Giao_Vien.layTatCaLoaiKyThi();
            cbbLoaiKyThi.DisplayMember = "TenLoaiKyThi";
            cbbLoaiKyThi.ValueMember = "MaLoaiKyThi";


            //
            bsCbbTenKyThi.DataSource = BUS_Giao_Vien.layTatCaKiThi();
            cbbTenKyThi.DataSource = bsCbbTenKyThi;
            cbbTenKyThi.DisplayMember = "TenKyThi";
            cbbTenKyThi.ValueMember = "MaKyThi";
            txtTenKyThi.DataBindings.Add("Text", bsCbbTenKyThi, "TenKyThi", true, DataSourceUpdateMode.Never);
            cbbLoaiKyThi.DataBindings.Add("SelectedValue", bsCbbTenKyThi, "MaLoaiKyThi", true, DataSourceUpdateMode.Never);
            txtTenKyThiCTKT.DataBindings.Add("Text", bsCbbTenKyThi, "TenKyThi", true, DataSourceUpdateMode.Never);


            cbbKhoiCTKT.DataSource = BUS_Giao_Vien.layTatCaKhoi();
            cbbKhoiCTKT.DisplayMember = "TenKhoi";
            cbbKhoiCTKT.ValueMember = "MaKhoi";


            cbbMonHocCTKT.DataSource = BUS_Giao_Vien.layTatCaMonHoc();
            cbbMonHocCTKT.DisplayMember = "TenMonHoc";
            cbbMonHocCTKT.ValueMember = "MaMonHoc";
           

            cbbMaDeThiCTKT.DataSource = BUS_Giao_Vien.layThongTinDeThiTheoKyThi(cbbTenKyThi.SelectedValue.ToString());
            cbbMaDeThiCTKT.DisplayMember = "MaDeThi";
            cbbMaDeThiCTKT.ValueMember = "MaDeThi";
            

            //Format
            dtpThoiGianThiCTKT.Format = DateTimePickerFormat.Custom;
            dtpThoiGianThiCTKT.CustomFormat = "dd/MM/yyyy HH:mm";

            dgvDanhSachKyThi.ColumnHeadersDefaultCellStyle.Font = new Font("Tohoma", 10.0F, FontStyle.Bold);
            dgvDanhSachKyThi.DefaultCellStyle.Font = new Font("Tohoma", 8.25F, FontStyle.Regular);
            dgvDanhSachKyThi.AutoGenerateColumns = false;
            dgvDanhSachKyThi.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; //Chiều cao cao tự động đồng loạt theo nội dung
            dgvDanhSachKyThi.DefaultCellStyle.WrapMode = DataGridViewTriState.True; //Xuống dòng nếu dữ liệu lớn hơn chiều rộng cho phép
            dgvDanhSachKyThi.Columns[0].Visible = false; //Ẩn mã kỳ thi
            dgvDanhSachKyThi.Columns[1].Visible = false; //Ẩn mã tên kỳ thi
            dgvDanhSachKyThi.Columns[2].Visible = false; //Ẩn mã khối
            dgvDanhSachKyThi.Columns[3].Visible = false; //Ẩn mã môn học
            dgvDanhSachKyThi.DataSource = bsDanhSachKyThi;
            thucHienBSDanhSachKyThi();


            cbbKhoiCTKT.DataBindings.Add(new Binding("SelectedValue", bsDanhSachKyThi, "MaKhoi", true, DataSourceUpdateMode.Never));
            txtThoiGianLamBai.DataBindings.Add(new Binding("Text", bsDanhSachKyThi, "ThoiGianLamBai", true, DataSourceUpdateMode.Never));
            cbbMaDeThiCTKT.DataBindings.Add(new Binding("SelectedValue", bsDanhSachKyThi, "MaDeThi", true, DataSourceUpdateMode.Never));
            cbbMonHocCTKT.DataBindings.Add(new Binding("SelectedValue", bsDanhSachKyThi, "MaMonHoc", true, DataSourceUpdateMode.Never));
            dtpThoiGianThiCTKT.DataBindings.Add(new Binding("Value", bsDanhSachKyThi, "ThoiGianBatDau", true, DataSourceUpdateMode.Never));
           

            dgvChiTietDeThi.ColumnHeadersDefaultCellStyle.Font = new Font("Tohoma", 10.0F, FontStyle.Bold);
            dgvChiTietDeThi.DefaultCellStyle.Font = new Font("Tohoma", 8.25F, FontStyle.Regular);
            dgvChiTietDeThi.AutoGenerateColumns = false;
            dgvChiTietDeThi.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; //Chiều cao cao tự động đồng loạt theo nội dung
            dgvChiTietDeThi.DefaultCellStyle.WrapMode = DataGridViewTriState.True; //Xuống dòng nếu dữ liệu lớn hơn chiều rộng cho phép
            dgvChiTietDeThi.Columns[0].Visible = false;



            //Hoạt động 
            txtThoiGianLamBai.Enabled = false; //Thời gian làm bài luôn luôn không cho thao tác ở form này, chỉ để hiển thị
            txtTenKyThiCTKT.Enabled = false; //Tên kỳ thi ở form chi tiet ky thi luôn không cho thao tác
            BsCbbTenKyThi_CurrentChanged(null, null);

            //
            //Event handle
            //
            btnDongThongTinKyThi.Click += BtnDongThongTinKyThi_Click;
            btnLamSachKyThi.Click += BtnLamSachKyThi_Click;
            btnSuaKyThi.Click += BtnCapNhatKyThi_Click;
            btnXoaKyThi.Click += BtnXoaKyThi_Click;
            btnThemKyThi.Click += BtnThemKyThi_Click;
            btnLamSachChiTietKyThi.Click += BtnLamSachChiTietKyThi_Click;
            btnThemChiTietKyThi.Click += BtnThemChiTietKyThi_Click;
            btnXoaChiTietKyThi.Click += BtnXoaChiTietKyThi_Click;
            btnCapNhatChiTietKyThi.Click += BtnCapNhatChiTietKyThi_Click;
            cbbMonHocCTKT.SelectedIndexChanged += CbbMonHocCTKT_SelectedIndexChanged;
            cbbKhoiCTKT.SelectedIndexChanged += CbbKhoiCTKT_SelectedIndexChanged;
            cbbMaDeThiCTKT.SelectedIndexChanged += CbbMaDeThiCTKT_SelectedIndexChanged;
            bsDanhSachKyThi.CurrentChanged += BsDanhSachKyThi_CurrentChanged;
            bsCbbTenKyThi.CurrentChanged += BsCbbTenKyThi_CurrentChanged;
           
        }

        private void BsCbbTenKyThi_CurrentChanged(object sender, EventArgs e)
        {
            thucHienBSDanhSachKyThi();
            // Khi chọn kỳ thi lúc này thêm kì thi bị vô hiệu hóa, kiểm tra kỳ thi cho phép sửa, xóa
            if (cbbTenKyThi.SelectedValue != null)
            {
                btnThemKyThi.Enabled = false;
                if (BUS_Giao_Vien.kyThiCoTrongKetQuaThi(cbbTenKyThi.SelectedValue.ToString()))
                {
                    btnSuaKyThi.Enabled = false;
                    btnXoaKyThi.Enabled = false;
                    txtTenKyThi.Enabled = false;
                }
                else
                {
                    btnSuaKyThi.Enabled = true;
                    btnXoaKyThi.Enabled = true;
                    txtTenKyThi.Enabled = true;
                }
            }
            if (bsCbbTenKyThi.Count != 0)
                BsDanhSachKyThi_CurrentChanged(null, null);
        }

        private void BsDanhSachKyThi_CurrentChanged(object sender, EventArgs e)
        {
            if (bsDanhSachKyThi.Count != 0)
            {
                btnThemChiTietKyThi.Enabled = false;
                if (BUS_Giao_Vien.chiTietKiThiCoTrongKetQuaThi(dgvDanhSachKyThi.SelectedRows[0].Cells[0].Value.ToString(), dgvDanhSachKyThi.SelectedRows[0].Cells[7].Value.ToString()))
                {
                    btnCapNhatChiTietKyThi.Enabled = false;
                    btnXoaChiTietKyThi.Enabled = false;
                    cbbKhoiCTKT.Enabled = false;
                    cbbMonHocCTKT.Enabled = false;
                    cbbMaDeThiCTKT.Enabled = false;
                    dtpThoiGianThiCTKT.Enabled = false;
                }
                else
                {
                    btnCapNhatChiTietKyThi.Enabled = true;
                    btnXoaChiTietKyThi.Enabled = true;
                    cbbKhoiCTKT.Enabled = true;
                    cbbMonHocCTKT.Enabled = true;
                    cbbMaDeThiCTKT.Enabled = true;
                    dtpThoiGianThiCTKT.Enabled = true;
                }
            }
        }

        private void CbbKhoiCTKT_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvChiTietDeThi.DataSource = null;
            txtThoiGianLamBai.Text = "";
            KYTHI kiThi = new KYTHI();
            kiThi = cbbTenKyThi.SelectedItem as KYTHI;
            if (kiThi != null && cbbKhoiCTKT.SelectedValue != null && cbbMonHocCTKT.SelectedValue != null)
                cbbMaDeThiCTKT.DataSource = BUS_Giao_Vien.layTatCaDeThiTheoMaKyThiVaMonHocVaKhoi(kiThi, cbbKhoiCTKT.SelectedValue.ToString(), cbbMonHocCTKT.SelectedValue.ToString());
        }

        private void CbbMonHocCTKT_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvChiTietDeThi.DataSource = null;
            txtThoiGianLamBai.Text = "";
            KYTHI kiThi = new KYTHI();
            kiThi = cbbTenKyThi.SelectedItem as KYTHI;
            if (kiThi != null && cbbKhoiCTKT.SelectedValue != null && cbbMonHocCTKT.SelectedValue != null)
                cbbMaDeThiCTKT.DataSource = BUS_Giao_Vien.layTatCaDeThiTheoMaKyThiVaMonHocVaKhoi(kiThi, cbbKhoiCTKT.SelectedValue.ToString(), cbbMonHocCTKT.SelectedValue.ToString());
            
        }

        private void CbbMaDeThiCTKT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbMaDeThiCTKT.SelectedValue != null)
            {
                DETHI deThi = new DETHI();
                deThi = BUS_Giao_Vien.layThongTinDeThi(cbbMaDeThiCTKT.SelectedValue.ToString());
                txtThoiGianLamBai.Text = deThi.ThoiGianLamBai.ToString();

                dgvChiTietDeThi.DataSource = BUS_Giao_Vien.layTatCaCauHoiCuaDe(cbbMaDeThiCTKT.SelectedValue.ToString());
            }         
        }

        //private void CbbTenKyThi_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    thucHienBSDanhSachKyThi();

        //    // Khi chọn kỳ thi lúc này thêm kì thi bị vô hiệu hóa, kiểm tra kỳ thi cho phép sửa, xóa
        //    if (cbbTenKyThi.SelectedValue != null)
        //    {
        //        btnThemKyThi.Enabled = false;
        //        if (BUS_Giao_Vien.kyThiCoTrongKetQuaThi(cbbTenKyThi.SelectedValue.ToString()))
        //        {
        //            btnSuaKyThi.Enabled = false;
        //            btnXoaKyThi.Enabled = false;
        //        }
        //        else
        //        {
        //            btnSuaKyThi.Enabled = true;
        //            btnXoaKyThi.Enabled = true;
        //        }
        //    }
        //}

        private void BtnDongThongTinKyThi_Click(object sender, EventArgs e)
        {
            TabControlTeacher.TabPages.RemoveByKey("tabPagesThongTinKyThi");

        }

        private void BtnLamSachChiTietKyThi_Click(object sender, EventArgs e)
        {
            dgvChiTietDeThi.DataSource = null;
            cbbMonHocCTKT.SelectedIndex = 0;
            cbbKhoiCTKT.SelectedIndex = 0;
            if (cbbMaDeThiCTKT.Items.Count != 0)
                cbbMaDeThiCTKT.SelectedIndex = 0;
            txtThoiGianLamBai.Text = "";

            //Khi click làm sạch chi tiết kỳ thi thì vô hiệu hóa sửa, xóa, được phép thêm
            btnThemChiTietKyThi.Enabled = true;
            btnCapNhatChiTietKyThi.Enabled = false;
            btnXoaChiTietKyThi.Enabled = false;
            cbbKhoiCTKT.Enabled = true;
            cbbMonHocCTKT.Enabled = true;
            cbbMaDeThiCTKT.Enabled = true;
            dtpThoiGianThiCTKT.Enabled = true;
        }

        private void BtnCapNhatChiTietKyThi_Click(object sender, EventArgs e)
        {
            if (kiemTraHopLeFormChiTietKyThi())
            {
                CHITIETKYTHI chiTietKyThi = new CHITIETKYTHI();
                chiTietKyThi.MaDeThi = cbbMaDeThiCTKT.SelectedValue.ToString();
                chiTietKyThi.MaKyThi = cbbTenKyThi.SelectedValue.ToString();
                chiTietKyThi.ThoiGianBatDau = dtpThoiGianThiCTKT.Value;
                DateTime aDateTime = chiTietKyThi.ThoiGianBatDau.Value;
                chiTietKyThi.ThoiGianKetThuc = aDateTime.AddMinutes(Double.Parse(txtThoiGianLamBai.Text));

                if (!BUS_Giao_Vien.capNhatChiTietKyThiVaoDB(chiTietKyThi))
                {
                    MessageBox.Show("Lỗi...!");
                    return;
                }
                MessageBox.Show("Cập nhật chi tiết kỳ thi thành công");
                
                thucHienBSCbbTenKyThi();

                //Liên kết bên danh sách thí sinh
                if (TabControlTeacher.TabPages.Contains(tabPageDanhSachThiSinh))
                {
                    dgvDanhSachThiSinh.DataSource = BUS_Giao_Vien.danhSachThiSinh(cbbTenKyThiDSTS.SelectedValue.ToString(), cbbKhoiDSKT.SelectedValue.ToString(), cbbMonHocDSKT.SelectedValue.ToString());
                }
            }
        }

        private void BtnXoaChiTietKyThi_Click(object sender, EventArgs e)
        {
            if (kiemTraHopLeFormChiTietKyThi())
            {
                CHITIETKYTHI chiTietKyThi = new CHITIETKYTHI();
                chiTietKyThi.MaDeThi = cbbMaDeThiCTKT.SelectedValue.ToString();
                chiTietKyThi.MaKyThi = cbbTenKyThi.SelectedValue.ToString();
                chiTietKyThi.ThoiGianBatDau = dtpThoiGianThiCTKT.Value;
                DateTime aDateTime = chiTietKyThi.ThoiGianBatDau.Value;
                chiTietKyThi.ThoiGianKetThuc = aDateTime.AddMinutes(Double.Parse(txtThoiGianLamBai.Text));

                if (!BUS_Giao_Vien.xoaChiTietKyThiVaoDB(chiTietKyThi))
                {
                    MessageBox.Show("Lỗi...!");
                    return;
                }
                MessageBox.Show("Xóa chi tiết kỳ thi thành công");
                           
                thucHienBSCbbTenKyThi();

                //Liên kết bên danh sách thí sinh
                if (TabControlTeacher.TabPages.Contains(tabPageDanhSachThiSinh))
                {
                    dgvDanhSachThiSinh.DataSource = BUS_Giao_Vien.danhSachThiSinh(cbbTenKyThiDSTS.SelectedValue.ToString(), cbbKhoiDSKT.SelectedValue.ToString(), cbbMonHocDSKT.SelectedValue.ToString());
                }

            }
        }

        private void BtnThemChiTietKyThi_Click(object sender, EventArgs e)
        {
            if (kiemTraHopLeFormChiTietKyThi())
            {
                CHITIETKYTHI chiTietKyThi = new CHITIETKYTHI();
                chiTietKyThi.MaDeThi = cbbMaDeThiCTKT.SelectedValue.ToString();
                chiTietKyThi.MaKyThi = cbbTenKyThi.SelectedValue.ToString();
                chiTietKyThi.ThoiGianBatDau = dtpThoiGianThiCTKT.Value;
                DateTime aDateTime = chiTietKyThi.ThoiGianBatDau.Value;
                chiTietKyThi.ThoiGianKetThuc = aDateTime.AddMinutes(Double.Parse(txtThoiGianLamBai.Text));

                if (!BUS_Giao_Vien.themChiTietKyThiVaoDB(chiTietKyThi))
                {
                    MessageBox.Show("Thêm chi tiết kỳ thi thất bại");
                    return;
                }
                MessageBox.Show("Thêm chi tiết kỳ thi thành công");
               
                thucHienBSCbbTenKyThi();

                //Liên kết bên danh sách thí sinh
                if (TabControlTeacher.TabPages.Contains(tabPageDanhSachThiSinh))
                {
                    dgvDanhSachThiSinh.DataSource = BUS_Giao_Vien.danhSachThiSinh(cbbTenKyThiDSTS.SelectedValue.ToString(), cbbKhoiDSKT.SelectedValue.ToString(), cbbMonHocDSKT.SelectedValue.ToString());
                }
            }
        }

        private void BtnLamSachKyThi_Click(object sender, EventArgs e)
        {
            txtTenKyThi.Text = "";

            //Khi click làm sạch kỳ thi thì vô hiệu hóa sửa, xóa, được phép thêm
            btnThemKyThi.Enabled = true;
            btnSuaKyThi.Enabled = false;
            btnXoaKyThi.Enabled = false;
            txtTenKyThi.Enabled = true;
        }

        private void BtnCapNhatKyThi_Click(object sender, EventArgs e)
        {
            if (kiemTraHopLeFormThongTinKyThi())
            {
                KYTHI kyThi = new KYTHI();
                kyThi.MaKyThi = cbbTenKyThi.SelectedValue.ToString();
                kyThi.TenKyThi = txtTenKyThi.Text;
                kyThi.MaLoaiKyThi = cbbLoaiKyThi.SelectedValue.ToString();

                if (!BUS_Giao_Vien.capNhatKyThiVaoDB(kyThi))
                {
                    MessageBox.Show("Lỗi...!");
                    return;
                }

                MessageBox.Show("Cập nhật kỳ thi thành công");
                thucHienBSCbbTenKyThi();
            }
        }

        private void BtnXoaKyThi_Click(object sender, EventArgs e)
        {
            KYTHI kyThi = new KYTHI();
            kyThi.MaKyThi = cbbTenKyThi.SelectedValue.ToString();
            kyThi.TenKyThi = txtTenKyThi.Text;
            kyThi.MaLoaiKyThi = cbbLoaiKyThi.SelectedValue.ToString();

            if (!BUS_Giao_Vien.xoaKyThiVaoDB(kyThi))
            {
                MessageBox.Show("Lỗi...!");
                return;
            }

            MessageBox.Show("Xóa kỳ thi thành công");
     
            thucHienBSCbbTenKyThi();
        }

        private void BtnThemKyThi_Click(object sender, EventArgs e)
        {
            if (kiemTraHopLeFormThongTinKyThi())
            {
                KYTHI kyThi = new KYTHI();
                kyThi.MaKyThi = BUS_Giao_Vien.tuDongTangKhoaKyThi();
                kyThi.TenKyThi = txtTenKyThi.Text;
                kyThi.MaLoaiKyThi = cbbLoaiKyThi.SelectedValue.ToString();

                if (!BUS_Giao_Vien.themKyThiVaoDB(kyThi))
                {
                    MessageBox.Show("Thêm kỳ thi thất bại");
                    return;
                }

                MessageBox.Show("Thêm kỳ thi thành công");
            
                thucHienBSCbbTenKyThi();
            }
        }

        bool kiemTraHopLeFormThongTinKyThi()
        {
            if (string.IsNullOrEmpty(txtTenKyThi.Text))
            {
                MessageBox.Show("Tên kỳ thi không được phép trống");
                return false;
            }

            return true;
        }

        bool kiemTraHopLeFormChiTietKyThi()
        {
            int thoiGian;
            if (txtThoiGianLamBai.Text.Equals(""))
                MessageBox.Show("Không được để trống trường thời gian");
            else if (!int.TryParse(txtThoiGianLamBai.Text, out thoiGian))
                MessageBox.Show("Thời gian không đúng định dạng");
            else if (txtTenKyThiCTKT.Text.Equals(""))
                MessageBox.Show("Hãy chọn kỳ thi");
            else if (cbbMaDeThiCTKT.SelectedValue == null)
                MessageBox.Show("Không có đề thi phù hợp");
            else if (cbbKhoiCTKT.SelectedValue == null)
                MessageBox.Show("Hãy chọn khối");
            else if (cbbMonHocCTKT.SelectedValue == null)
                MessageBox.Show("Hãy chọn môn học");
            else
                return true;
            return false;

        }

        void thucHienBSDanhSachKyThi()
        {
            if (BUS_Giao_Vien.danhSachKyThi(cbbTenKyThi.SelectedValue.ToString()).Count == 0)
            {
                bsDanhSachKyThi.DataSource = BUS_Giao_Vien.danhSachKyThi(cbbTenKyThi.SelectedValue.ToString());
                bsDanhSachKyThi.SuspendBinding();
                BtnLamSachChiTietKyThi_Click(null, null);
            }
            else
            {
                bsDanhSachKyThi.ResumeBinding();
                bsDanhSachKyThi.DataSource = BUS_Giao_Vien.danhSachKyThi(cbbTenKyThi.SelectedValue.ToString());              
            }
            cbbMaDeThiCTKT.DataSource = BUS_Giao_Vien.layThongTinDeThiTheoKyThi(cbbTenKyThi.SelectedValue.ToString());

        }

        void thucHienBSCbbTenKyThi()
        {
            if (BUS_Giao_Vien.layTatCaKiThi().Count == 0)
            {
                bsCbbTenKyThi.DataSource = BUS_Giao_Vien.layTatCaKiThi();
                bsCbbTenKyThi.SuspendBinding();
                BtnLamSachChiTietKyThi_Click(null, null);
                BtnLamSachKyThi_Click(null, null);
            }
            else
            {
                bsCbbTenKyThi.ResumeBinding();
                bsCbbTenKyThi.DataSource = BUS_Giao_Vien.layTatCaKiThi();
            }
            thucHienBSDanhSachKyThi();
        }

        //
        //Xử lý tagpages danh sách đề thi
        //
        private void TsmiDanhSachDeThi_Click(object sender, EventArgs e)
        {
            
            //Mở tab
            if (TabControlTeacher.TabPages.Contains(tabPageThongTinDeThi))
            {
                TabControlTeacher.SelectedTab = tabPageThongTinDeThi;
                return;
            }               
            else
                TabControlTeacher.TabPages.Add(tabPageThongTinDeThi);

            TabControlTeacher.SelectedTab = tabPageThongTinDeThi;

            //Cấu hình cho dgv
            dgvCauHoiCuaDe.AutoGenerateColumns = false; //Không được xóa header
            dgvCauHoiCuaDe.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; //Chiều cao cao tự động đồng loạt theo nội dung
            //dgvCauHoiCuaDe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; //Không cho phép điều chỉnh chiểu rộng của cột
            dgvCauHoiCuaDe.DefaultCellStyle.WrapMode = DataGridViewTriState.True; //Xuống dòng nếu dữ liệu lớn hơn chiều rộng cho phép
            dgvCauHoiCuaDe.Columns[0].Visible = false; //Ẩn cột mã câu hỏi đi

            //
            dgvTatCaCauHoi.AutoGenerateColumns = false; //Không được xóa header
            dgvTatCaCauHoi.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; //Chiều cao cao tự động đồng loạt theo nội dung
            //dgvCauHoiCuaDe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; //Không cho phép điều chỉnh chiểu rộng của cột
            dgvTatCaCauHoi.DefaultCellStyle.WrapMode = DataGridViewTriState.True; //Xuống dòng nếu dữ liệu lớn hơn chiều rộng cho phép
            dgvTatCaCauHoi.Columns[0].Visible = false; //Ẩn cột mã câu hỏi đi

            cbbMonHoc.DataSource = BUS_Giao_Vien.layTatCaMonHoc();
            cbbMonHoc.DisplayMember = "TenMonHoc";
            cbbMonHoc.ValueMember = "MaMonHoc";

            cbbKhoi.DataSource = BUS_Giao_Vien.layTatCaKhoi();
            cbbKhoi.DisplayMember = "TenKhoi";
            cbbKhoi.ValueMember = "MaKhoi";

            cbbDeThi.DataSource = BUS_Giao_Vien.layTatCaDeThi();
            cbbDeThi.DisplayMember = "MaDeThi";

            cbbLoaiCauHoi.DataSource = BUS_Giao_Vien.layTatCaLoaiCauHoi();
            cbbLoaiCauHoi.DisplayMember = "TenLoaiCauHoi";
            cbbLoaiCauHoi.ValueMember = "MaLoaiCauHoi";

            //Thêm vào form tạo đề thi mã đề thi mới nhất
            txtDeThi.Text = BUS_Giao_Vien.tuDongTangKhoaDeThi();
            CbbDeThi_SelectedIndexChanged(null, null);

            //Tạo sự kiện
            cbbDeThi.SelectedIndexChanged += CbbDeThi_SelectedIndexChanged;
            cbbLoaiCauHoi.SelectedIndexChanged += CbbLoaiCauHoi_SelectedIndexChanged;
            btnLamSachForm.Click += BtnLamSachForm_Click;
            btnXoaDeThi.Click += BtnXoaDeThi_Click;
            btnCapNhatDeThi.Click += BtnCapNhatDeThi_Click;
            btnThemDeThi.Click += BtnThemDeThi_Click;
            btnDongThongTinDeThi.Click += BtnDongThongTinDeThi_Click;
            btnThemCauHoiVaoDeThi.Click += BtnThemCauHoiVaoDeThi_Click;
            btnXoaCauHoiKhoiDeThi.Click += BtnXoaCauHoiKhoiDeThi_Click;

        }

        private void BtnXoaCauHoiKhoiDeThi_Click(object sender, EventArgs e)
        {
            DETHI deThi = new DETHI();
            deThi = cbbDeThi.SelectedItem as DETHI;
            foreach (DataGridViewRow row in dgvCauHoiCuaDe.SelectedRows)
            {
                //Xóa ở DB
                CHITIETDETHI ctdt = new CHITIETDETHI();
                ctdt.MaCauHoi = row.Cells[0].Value.ToString();
                ctdt.MaDeThi = deThi.MaDeThi;
             
                if (!BUS_Giao_Vien.xoaChiTietDeThiVaoDB(ctdt))
                    MessageBox.Show("Lỗi...!");
            }
            //Thay đổi hiển thị 
            dgvCauHoiCuaDe.DataSource = BUS_Giao_Vien.layTatCaCauHoiCuaDe(deThi.MaDeThi);
        }

        private void BtnThemCauHoiVaoDeThi_Click(object sender, EventArgs e)
        {
            //Thêm vào DB => nếu thất bại thì thông báo trùng
            DETHI deThi = new DETHI();
            deThi = cbbDeThi.SelectedItem as DETHI;
            foreach (DataGridViewRow row in  dgvTatCaCauHoi.SelectedRows)
            {
                CHITIETDETHI ctdt = new CHITIETDETHI();
                ctdt.MaCauHoi = row.Cells[0].Value.ToString();
                ctdt.MaDeThi = deThi.MaDeThi;
              
                if (!BUS_Giao_Vien.themChiTietDeThiVaoDB(ctdt))
                    MessageBox.Show("Câu hỏi đã có trong vào bộ đề");
            }
            //Thêm vào hiển thị 
            dgvCauHoiCuaDe.DataSource = BUS_Giao_Vien.layTatCaCauHoiCuaDe(deThi.MaDeThi);
        }

        //Đóng tabgages danh sách đề thi
        private void BtnDongThongTinDeThi_Click(object sender, EventArgs e)
        {
            TabControlTeacher.TabPages.RemoveByKey("tabPageThongTinDeThi");
        }

        private void CbbLoaiCauHoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clear nội dung trong dgvTatCaCauHoi
            dgvTatCaCauHoi.DataSource = null;

            DETHI deThi = new DETHI();
            deThi = cbbDeThi.SelectedItem as DETHI;

            if (!BUS_Giao_Vien.deThiCoTrongKetQuaThi(deThi.MaDeThi)) //Nếu đề thi cho phép cập nhật
            {
                dgvTatCaCauHoi.DataSource = BUS_Giao_Vien.layTatCaCauHoiTheoLoaiCauHoi(cbbLoaiCauHoi.SelectedValue.ToString(), deThi.MaMonHoc, deThi.MaKhoi);
            }
        }

        private void BtnThemDeThi_Click(object sender, EventArgs e)
        {
            //Kiểm tra thời gian.
            int kiemTraThoiGian = -1;
            if (string.IsNullOrEmpty(txtThoiGian.Text))
            {
                MessageBox.Show("Bạn chưa nhập thời gian của đề thi");
                return;
            }          
            else if (!int.TryParse(txtThoiGian.Text, out kiemTraThoiGian) || kiemTraThoiGian < 0)
            {
                MessageBox.Show("Thời gian đề thi không đúng định dạng");
                return;
            }
            else if (kiemTraThoiGian == 0)
            {
                MessageBox.Show("Thời gian đề thi phải khác 0");
                return;
            }
                

            //Tạo đè thi
            DETHI dt = new DETHI();
            dt.MaDeThi = txtDeThi.Text;
            dt.MaKhoi = cbbKhoi.SelectedValue.ToString();
            dt.MaMonHoc = cbbMonHoc.SelectedValue.ToString();
            dt.ThoiGianLamBai = kiemTraThoiGian;


            //Thêm dữ liệu vào
            if (!BUS_Giao_Vien.themDeThiVaoDB(dt))
            {
                MessageBox.Show("Thêm thất bại");
                return;
            }
        
            MessageBox.Show("Thêm thành công");
            //Đổ dữ liệu ngược lại về cbbDeThi
            cbbDeThi.DataSource = BUS_Giao_Vien.layTatCaDeThi();
            //Hiển thị cbbDeThi là deThi vừa thêm
            cbbDeThi.SelectedIndex = cbbDeThi.Items.Count - 1;

            //Liên kết với với tagpages Thông tin kỳ thi
            
            if (TabControlTeacher.TabPages.Contains(tabPagesThongTinKyThi))
            {
                CbbKhoiCTKT_SelectedIndexChanged(null, null); //Nhằm mục đích tải lại đề thi
            }
        }
     
        private void BtnCapNhatDeThi_Click(object sender, EventArgs e)
        {
            //Chỉ cho phép cập nhật thời gian
            //Kiểm tra thời gian.
            int kiemTraThoiGian = -1;
            if (string.IsNullOrEmpty(txtThoiGian.Text))
            {
                MessageBox.Show("Bạn chưa nhập thời gian của đề thi");
                return;
            }
            else if (!int.TryParse(txtThoiGian.Text, out kiemTraThoiGian) || kiemTraThoiGian < 0)
            {
                MessageBox.Show("Thời gian đề thi không đúng định dạng");
                return;
            }
            else if (kiemTraThoiGian == 0)
            {
                MessageBox.Show("Thời gian đề thi phải khác 0");
                return;
            }

            //Dữ liệu
            DETHI dt = new DETHI();
            dt.MaDeThi = txtDeThi.Text;
            dt.MaKhoi = cbbKhoi.SelectedValue.ToString();
            dt.MaMonHoc = cbbMonHoc.SelectedValue.ToString();
            dt.ThoiGianLamBai = kiemTraThoiGian;

            //Cập nhật dữ liệu
            if (BUS_Giao_Vien.capNhatDeThiVaoDB(dt))
                MessageBox.Show("Cập nhật thành công");
            else
                MessageBox.Show("Cập nhật thất bại");

            //Đổ dữ liệu ngược lại về cbbDeThi
            cbbDeThi.DataSource = BUS_Giao_Vien.layTatCaDeThi();

            //Liên kết với với tagpages Thông tin kỳ thi
            if (TabControlTeacher.TabPages.Contains(tabPagesThongTinKyThi))
            {
                CbbKhoiCTKT_SelectedIndexChanged(null, null); //Nhằm mục đích tải lại đề thi
            }

        }

        private void BtnXoaDeThi_Click(object sender, EventArgs e)
        {
            //Dữ liệu
            DETHI dt = new DETHI();
            dt.MaDeThi = txtDeThi.Text;
            dt.MaKhoi = cbbKhoi.SelectedValue.ToString();
            dt.MaMonHoc = cbbMonHoc.SelectedValue.ToString();
            dt.ThoiGianLamBai = int.Parse(txtThoiGian.Text);

            //Cập nhật dữ liệu
            if (BUS_Giao_Vien.xoaDeThiVaoDB(dt))
                MessageBox.Show("Xóa thành công");
            else
                MessageBox.Show("Xóa thất bại");

            //Đổ dữ liệu ngược lại về cbbDeThi
            cbbDeThi.DataSource = BUS_Giao_Vien.layTatCaDeThi();

            //Liên kết với với tagpages Thông tin kỳ thi
            if (TabControlTeacher.TabPages.Contains(tabPagesThongTinKyThi))
            {
                CbbKhoiCTKT_SelectedIndexChanged(null, null); //Nhằm mục đích tải lại đề thi
            }
        }

        private void BtnLamSachForm_Click(object sender, EventArgs e)
        {
            //Xóa những nội dung
            txtThoiGian.Clear();
            //Mã mới tương ứng với đề
            txtDeThi.Text = BUS_Giao_Vien.tuDongTangKhoaDeThi();

            ////Xóa dữ liệu trong dgv
            dgvTatCaCauHoi.DataSource = null;
            dgvCauHoiCuaDe.DataSource = null;

            //Khi đã làm sạch là cập nhật và xóa không được phép hoạt động
            btnCapNhatDeThi.Enabled = false;
            btnXoaDeThi.Enabled = false;
            btnThemCauHoiVaoDeThi.Enabled = false;
            btnXoaCauHoiKhoiDeThi.Enabled = false;
            btnThemCauHoiVaoDeThi.Enabled = false;
            btnXoaCauHoiKhoiDeThi.Enabled = false;

            //Ngoại trừ ở trên
            btnThemDeThi.Enabled = true;         
            cbbKhoi.Enabled = true;
            cbbMonHoc.Enabled = true;
            txtThoiGian.Enabled = true;
          
        }

        void batTatCaDieuKhienThaoTac()
        {
            btnCapNhatDeThi.Enabled = true;
            btnXoaDeThi.Enabled = true;
            btnThemCauHoiVaoDeThi.Enabled = true;
            btnXoaCauHoiKhoiDeThi.Enabled =true;
            btnThemDeThi.Enabled = true;
            cbbKhoi.Enabled = true;
            cbbMonHoc.Enabled = true;
            txtThoiGian.Enabled = true;
            btnThemCauHoiVaoDeThi.Enabled = true;
            btnXoaCauHoiKhoiDeThi.Enabled = true;
        }

        private void CbbDeThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Xóa dữ liệu ở tất cả câu hỏi
            dgvTatCaCauHoi.DataSource = null;

            //Bật hết tất cả điều khiển thao tác
            batTatCaDieuKhienThaoTac();
            //Khi chọn một đề để thao tác ta sẽ không có thao tác trên
            btnThemDeThi.Enabled = false;
            
            DETHI deThi = new DETHI();
            deThi = cbbDeThi.SelectedItem as DETHI;
            txtDeThi.Text = deThi.MaDeThi;
            cbbMonHoc.SelectedValue = deThi.MaMonHoc;
            cbbKhoi.SelectedValue = deThi.MaKhoi;
            txtThoiGian.Text = deThi.ThoiGianLamBai.ToString();

           
            if (BUS_Giao_Vien.deThiCoTrongKetQuaThi(deThi.MaDeThi)) //Nếu đề thi không được phép cập nhật
            {
                //Thực hiện khóa những control không được phép thao tác
                btnCapNhatDeThi.Enabled = false;        
                cbbKhoi.Enabled = false;
                cbbMonHoc.Enabled = false;
                txtThoiGian.Enabled = false;
                btnThemCauHoiVaoDeThi.Enabled = false;
                btnXoaCauHoiKhoiDeThi.Enabled = false;
                btnXoaDeThi.Enabled = false;

                //if (BUS_Giao_Vien.deThiCoTrongChiTietDeThi(deThi.MaDeThi)) //Nếu đề thi không cho phép xóa
                //{
                //    btnXoaDeThi.Enabled = false;
                //}
            }
            else
            {
                CbbLoaiCauHoi_SelectedIndexChanged(null, null);
            }

            dgvCauHoiCuaDe.DataSource = BUS_Giao_Vien.layTatCaCauHoiCuaDe(deThi.MaDeThi);

            //Kiểm tra có cho phép cập nhật khối và môn
            //if (BUS_Giao_Vien.deThiCoTrongChiTietDeThi(txtDeThi.Text))
            //{ 
            //    cbbKhoi.Enabled = false;
            //    cbbMonHoc.Enabled =false;
            //}

        }


        //
        //Xử lý tagpages danh sách kết quả
        //
        private void TsmiDanhSachKetQua_Click(object sender, EventArgs e)
        {
            //Mở tab
            if (TabControlTeacher.TabPages.Contains(tabPageDanhSachKetQua))
            {
                TabControlTeacher.SelectedTab = tabPageDanhSachKetQua;
                return;
            }               
            else
                TabControlTeacher.TabPages.Add(tabPageDanhSachKetQua);


            TabControlTeacher.SelectedTab = tabPageDanhSachKetQua;

            rbtnExam.Checked = true;
            btnFilter.Click += BtnFilter_Click;
            btnCloseExamResult.Click += BtnCloseExamResult_Click;
            dgvListResult.AutoGenerateColumns = false; //Không được xóa header

            cbbExam.DataSource = BUS_Giao_Vien.layTatCaKiThi();
            cbbExam.DisplayMember = "TenKyThi";
            cbbExam.ValueMember = "MaKyThi";

            cbbKindQuestion.DataSource = BUS_Giao_Vien.layTatCaLoaiCauHoi();
            cbbKindQuestion.DisplayMember = "TenLoaiCauHoi";
            cbbKindQuestion.ValueMember = "MaLoaiCauHoi";

        }

        //Tắt tabpages danh sách kết quả
        private void BtnCloseExamResult_Click(object sender, EventArgs e)
        {
            TabControlTeacher.TabPages.RemoveByKey("tabPageDanhSachKetQua");
        }

        //Khi click nút lọc trong danh sách kết quả
        private void BtnFilter_Click(object sender, EventArgs e)
        {

            if (rbtnStudent.Checked)
            {
                if (txtFilterStudent.Text.Equals(""))
                {
                    dgvListResult.DataSource = null;
                }
                else
                {
                    if (BUS_Giao_Vien.layTatCaKetQua("HOCSINH", txtFilterStudent.Text) != null)
                        dgvListResult.DataSource = BUS_Giao_Vien.layTatCaKetQua("HOCSINH", txtFilterStudent.Text);
                    else
                        dgvListResult.Rows.Clear();

                }
               
            }
            else if (rbtnKindQuestion.Checked)
            {
                if (BUS_Giao_Vien.layTatCaKetQua("LOAICAUHOI", cbbKindQuestion.SelectedValue.ToString()) != null)
                    dgvListResult.DataSource = BUS_Giao_Vien.layTatCaKetQua("LOAICAUHOI", cbbKindQuestion.SelectedValue.ToString());
                else
                    dgvListResult.Rows.Clear();
            }
            else
            {
                if (BUS_Giao_Vien.layTatCaKetQua("KYTHI", cbbExam.SelectedValue.ToString()) != null)
                    dgvListResult.DataSource = BUS_Giao_Vien.layTatCaKetQua("KYTHI", cbbExam.SelectedValue.ToString());
                else
                    dgvListResult.Rows.Clear();
            }
        }


        private void frmGiao_Vien_Load(object sender, EventArgs e)
        {

            this.rpDanhSachThiSinh.RefreshReport();
            this.rpDanhSachThiSinh.RefreshReport();
        }
    }
}
