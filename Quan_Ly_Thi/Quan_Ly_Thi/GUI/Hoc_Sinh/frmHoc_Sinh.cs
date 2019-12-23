using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quan_Ly_Thi.GUI.He_Thong;
using Quan_Ly_Thi.BUS;
using Quan_Ly_Thi.DAO;
using Quan_Ly_Thi.DTO;
using Quan_Ly_Thi.GUI.Hoc_Sinh.Report_Viewer;

namespace Quan_Ly_Thi.GUI.Hoc_Sinh
{
    public partial class frmHoc_Sinh : Form
    {
        public static Hoc_Sinhh hs;
        
        public frmHoc_Sinh()
        {
            InitializeComponent();
        }

        private void frmHoc_Sinh_Load(object sender, EventArgs e)
        {
            controlStudent.TabPages.Clear();
            hs = BUS_Tai_Khoan.layThongTinTaiKhoan(hs.Tai_Khoan) as Hoc_Sinhh;
        }

        //thoát
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //xem lịch thi
        private void btnTest_day_Click(object sender, EventArgs e)
        {
            controlStudent.TabPages.Clear();
            controlStudent.TabPages.Add(Tab_test_day);
            controlStudent.SelectedTab = Tab_test_day;


            dt_test_day.DataSource = BUS_Hoc_Sinh.Lay_lich_Thi(BUS_Hoc_Sinh.ID_Lop(hs.Lop));
        }

        //Xem Thông Tin Cá Nhân
        private void btnInformation_Click(object sender, EventArgs e)
        {
            frmThong_Tin thong_Tin = new frmThong_Tin();
            thong_Tin.hs_new = hs;
            thong_Tin.ShowDialog();
            hs = BUS_Tai_Khoan.layThongTinTaiKhoan(hs.Tai_Khoan) as Hoc_Sinhh;
        }

        //đi Thi
        De_thi De;
        int Thoi_gian_lam_bai;
        public static string _ma_de_thi_;
        public static string _ma_ky_thi_;
        int  _vi_tri_Cau_hoi_;

        private void btnStart_Click(object sender, EventArgs e)
        {

            controlStudent.TabPages.Clear();
            controlStudent.TabPages.Add(TabTest);
            controlStudent.SelectedTab = TabTest;

            lbName.Text = hs.Ho_Ten;
            lbClass.Text = hs.Lop;
            pbBirth_day.Value = hs.Ngay_Sinh;

            //Set all
            _ma_de_thi_ = null;
            _ma_ky_thi_ = null;
            De = null;

            _vi_tri_Cau_hoi_ = 0;
            Dem_Lan_Bam = 0;

            Thoi_gian_lam_bai = 0;
            lb_minute.Text = "00";
            lb_second.Text = ":00";
            lbSTT_Question.Text = "00/00";

            btnStart_Pause.Enabled = true;
            btnNext.Enabled = true;

            btnStart_Pause.BackgroundImage = Play_pause.Images[1];
            btnStart_Pause.BackgroundImageLayout = ImageLayout.Stretch;

            rbtn_A.Text = "Câu A";
            rbtn_B.Text = "Câu B";
            rbtn_C.Text = "Câu C";
            rbtn_D.Text = "Câu D";

            rbtn_A.Checked = false;
            rbtn_B.Checked = false;
            rbtn_C.Checked = false;
            rbtn_D.Checked = false;

            
            lb_Question.Text = "Câu Hỏi";
        }

        //Bắt Đầu

        //reset cau tra loi
        private void Reset_Cau_Tra_Loi()
        {
            rbtn_A.Checked = false;
            rbtn_B.Checked = false;
            rbtn_C.Checked = false;
            rbtn_D.Checked = false;
        }

        public List<CauTraLoi> DS_Cau_Tra_Loi ;
        int Dem_Lan_Bam;
        DateTime Start_time;
        DateTime End_time;
        
        private void btnStart_Pause_Click(object sender, EventArgs e)
        {
            if (_ma_de_thi_ == null || _ma_ky_thi_ == null)
            {
                frmChon_Bai_Thi.lop = hs.Lop;
                frmChon_Bai_Thi._tai_khoan_ = hs.Tai_Khoan;
                frmChon_Bai_Thi chon_Bai_Thi = new frmChon_Bai_Thi();
                chon_Bai_Thi.ShowDialog();
            }
            //có Đề  Thi
             if (_ma_de_thi_ != null && Dem_Lan_Bam == 0)
            {
                //Load De
                De = BUS_De_Thi.Lay_De_Thi_(_ma_de_thi_);
                Thoi_gian_lam_bai = BUS_De_Thi.Thoi_Gian_Thi(_ma_de_thi_);
                DS_Cau_Tra_Loi = new List<CauTraLoi>();
                lbSTT_Question.Text = "Câu " + (_vi_tri_Cau_hoi_ + 1).ToString() + " / " + De.De.Count.ToString();
                Dem_Lan_Bam = 1;

                //set up Thoi Gian
                lb_minute.Text =( Thoi_gian_lam_bai-1).ToString();
                lb_second.Text = "59";
                Start_time = DateTime.Now;
                Thoi_Gian.Start();
            }
             
        }

        //Tới
        private void btnNext_Click(object sender, EventArgs e)
        {
            btnSubmit.Enabled = true;
            Luu_Cau_Tra_Loi();
            Reset_Cau_Tra_Loi();
            _vi_tri_Cau_hoi_++;

            lbSTT_Question.Text = "Câu " + (_vi_tri_Cau_hoi_ + 1).ToString() + " / " + De.De.Count.ToString();
            Dap_an_Da_Chon(_vi_tri_Cau_hoi_);

            int so_cau = De.De.Count;
            if (_vi_tri_Cau_hoi_ > 0 && _vi_tri_Cau_hoi_ < so_cau)
            {
                btnPrev.Enabled = true;
            }
            if (_vi_tri_Cau_hoi_ == so_cau - 1)
            {
                _vi_tri_Cau_hoi_ = so_cau - 1;
                btnNext.Enabled = false;
            }

        }

        //Lui
        private void btnPrev_Click(object sender, EventArgs e)
        {
            Luu_Cau_Tra_Loi();
            Reset_Cau_Tra_Loi();
            _vi_tri_Cau_hoi_--;

            if (De.De.Count > 0)
            {
                lbSTT_Question.Text = "Câu " + (_vi_tri_Cau_hoi_ + 1).ToString() + " / " + De.De.Count.ToString();
            }
               
            Dap_an_Da_Chon(_vi_tri_Cau_hoi_);
            int so_cau = De.De.Count;
            if (_vi_tri_Cau_hoi_ > 0)
            {
                btnNext.Enabled = true;
            }
            if (_vi_tri_Cau_hoi_ == 0)
            {
                _vi_tri_Cau_hoi_ = 0;
                btnPrev.Enabled = false;
            }

        }

        //Xử Lý Câu Hỏi
        private void lbSTT_Question_TextChanged(object sender, EventArgs e)
        {
            if (De != null)
            {
                int a = _vi_tri_Cau_hoi_;
                if (De.De[_vi_tri_Cau_hoi_].noi_dung.Length > 50)
                {
                    De.De[_vi_tri_Cau_hoi_].noi_dung = De.De[_vi_tri_Cau_hoi_].noi_dung.Replace("\n", ""); ;
                    De.De[_vi_tri_Cau_hoi_].noi_dung = De.De[_vi_tri_Cau_hoi_].noi_dung.Insert(De.De[_vi_tri_Cau_hoi_].noi_dung.Length / 2, "\n");
                }
                lb_Question.Text = De.De[_vi_tri_Cau_hoi_].noi_dung;
                rbtn_A.Text = De.De[_vi_tri_Cau_hoi_].Cau_A;
                rbtn_B.Text = De.De[_vi_tri_Cau_hoi_].Cau_B;
                rbtn_C.Text = De.De[_vi_tri_Cau_hoi_].Cau_C;
                rbtn_D.Text = De.De[_vi_tri_Cau_hoi_].Cau_D;
            }

        }

        //Nộp Bài
        private void Luu_Cau_Tra_Loi()
        {
            End_time = DateTime.Now;
            CauTraLoi ctl = new CauTraLoi();

            if (rbtn_A.Checked)
            {
                ctl.new_Cau_Tra_Loi(_vi_tri_Cau_hoi_, rbtn_A.Text, De.De[_vi_tri_Cau_hoi_].Dap_An);

            }
            else if (rbtn_B.Checked)
            {
                ctl.new_Cau_Tra_Loi(_vi_tri_Cau_hoi_, rbtn_B.Text, De.De[_vi_tri_Cau_hoi_].Dap_An);
            }
            else if (rbtn_C.Checked)
            {
                ctl.new_Cau_Tra_Loi(_vi_tri_Cau_hoi_, rbtn_C.Text, De.De[_vi_tri_Cau_hoi_].Dap_An);
            }
            else if (rbtn_D.Checked)
            {
                ctl.new_Cau_Tra_Loi(_vi_tri_Cau_hoi_, rbtn_D.Text, De.De[_vi_tri_Cau_hoi_].Dap_An);
            }
            else
            {
                ctl.new_Cau_Tra_Loi(_vi_tri_Cau_hoi_, "", De.De[_vi_tri_Cau_hoi_].Dap_An);
            }

            for (int i = 0; i < DS_Cau_Tra_Loi.Count; i++)
            {
                
                if (DS_Cau_Tra_Loi[i].index == _vi_tri_Cau_hoi_)
                {
                    DS_Cau_Tra_Loi[i] = ctl;
                    return;
                }
            }
            

            DS_Cau_Tra_Loi.Add(ctl);
        }

        // Tải Lại Cau trả Loi
        private void Dap_an_Da_Chon(int vi_tri_cau_tra_loi)
        {
            for (int i = 0; i < DS_Cau_Tra_Loi.Count; i++)
            {
                if (_vi_tri_Cau_hoi_ == DS_Cau_Tra_Loi[i].index)
                {
                    if (DS_Cau_Tra_Loi[i].cau_tra_loi == rbtn_A.Text)
                    {
                        rbtn_A.Checked = true;
                        break;
                    }
                    else if (DS_Cau_Tra_Loi[i].cau_tra_loi == rbtn_B.Text)
                    {
                        rbtn_B.Checked = true;
                        break;
                    }
                    else if (DS_Cau_Tra_Loi[i].cau_tra_loi == rbtn_C.Text)
                    {
                        rbtn_C.Checked = true;
                        break;
                    }
                    else if (DS_Cau_Tra_Loi[i].cau_tra_loi == rbtn_D.Text)
                    {
                        rbtn_D.Checked = true;
                        break;
                    }
                }
            }

        }

        //thoát Thi
        private void btnExit_test_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn Muốn Hủy Thi","Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                controlStudent.TabPages.Clear();
            }
            
        }

        //Nộp Bài
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Thoi_Gian.Stop();
            Luu_Cau_Tra_Loi();
            double So_cau_Dung = 0;
            double  Tong_So_cau = De.De.Count;
            double Diem;

            for (int i = 0; i < DS_Cau_Tra_Loi.Count; i++)
                {
                    if (DS_Cau_Tra_Loi[i].cau_tra_loi.Replace(" ", "") == DS_Cau_Tra_Loi[i].Dap_An.Replace(" ",""))
                    {
                        So_cau_Dung++;
                    }
                }
            Diem = (So_cau_Dung / Tong_So_cau) * 10;
            Diem = Math.Round(Diem, 2);

            for (int i = 0; i < De.De.Count; i++)
            {
                if (i > DS_Cau_Tra_Loi.Count - 1)
                {
                    CauTraLoi cauTraLoi = new CauTraLoi();
                    cauTraLoi.new_Cau_Tra_Loi(i, "", De.De[i].Dap_An);
                    DS_Cau_Tra_Loi.Add(cauTraLoi);
                }
            }
            frmDap_An.DS_Cau_Tra_Loi = DS_Cau_Tra_Loi;
            KETQUATHI kqua = new KETQUATHI();
            kqua.MaKyThi = _ma_ky_thi_;
            kqua.MaDeThi = _ma_de_thi_;
            kqua.TaiKhoan = hs.Tai_Khoan;
            kqua.Diem = Diem;
            kqua.ThoiGianBatDau = Start_time;
            kqua.ThoiGianKetThuc = End_time;

            if (BUS_Hoc_Sinh.Luu_Ket_Qua(kqua) == true)
            {
                MessageBox.Show("Đã Lưu ^_^", "Thông Báo",MessageBoxButtons.OK ,MessageBoxIcon.Information);
                frmKetQua.Diem = Diem;
                frmKetQua frmKetQua1 = new frmKetQua();
                frmKetQua1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Đã Có Kết Quả Đề Này ^_^", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            btnSubmit.Enabled = false;
            btnNext.Enabled = false;
            btnPrev.Enabled = false;
            btnStart_Pause.Enabled = false;
        }

        //thời Gian Thi
        private void Thoi_Gian_Tick(object sender, EventArgs e)
        {
            int a = int.Parse(lb_minute.Text);
            int b = int.Parse(lb_second.Text);

            b--;
            if (a == 0 && b == 0)
            {
                Thoi_Gian.Stop();
                MessageBox.Show("Hết giờ !!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnSubmit_Click(sender,e);
                a = Thoi_gian_lam_bai;
                b = 0;
                lb_second.Text = b.ToString();
                lb_minute.Text = a.ToString();

            }
            if (b == 0)
            {
                a--;
                b = 59;
            }

            lb_second.Text = b.ToString();
            lb_minute.Text = a.ToString();
        }





        //funtion
        // reset câu trả lời
        void reset_Lua_Chon()
        {
            rbtnTrial_A.Checked = false;
            rbtnTrial_B.Checked = false;
            rbtnTrial_C.Checked = false;
            rbtnTrial_D.Checked = false;
        }

        // Thi Thử 
        private void btnTrial_test_Click(object sender, EventArgs e)
        {
            thoi_gian1.Stop();

            controlStudent.TabPages.Clear();
            controlStudent.TabPages.Add(TabTrial_test);

            //khởi tạo 

            lbTrial_Name.Text = hs.Ho_Ten;
            lbTrial_Class.Text = hs.Lop;
            dpTrial_birth_day.Value = hs.Ngay_Sinh;

            //Set all
            _ma_de_thi_ = null;
            _ma_ky_thi_ = null;

            De = null;
            _vi_tri_Cau_hoi_ = 0;
            Dem_Lan_Bam = 0;

            Thoi_gian_lam_bai = 0;
            lb_minute.Text = "00";
            lb_second.Text = ":00";

            lbTrial_STT_question.Text = "00/00";
            btnTrial_start_pause.Enabled = true;
            btnTrial_Next.Enabled = true;

            rbtnTrial_A.Text = "Câu A";
            rbtnTrial_B.Text = "Câu B";
            rbtnTrial_C.Text = "Câu C";
            rbtnTrial_D.Text = "Câu D";

            reset_Lua_Chon();

            lbTrial_Question.Text = "Câu Hỏi";

           
        }

        //bắt đầu thi thử
        private void btnTrial_start_pause_Click(object sender, EventArgs e)
        {
            if (_ma_de_thi_ == null || _ma_ky_thi_ == null)
            {
                frmChon_Bai_Thi.lop = hs.Lop;
                frmChon_Bai_Thi._tai_khoan_ =null;
                frmChon_Bai_Thi chon_Bai_Thi = new frmChon_Bai_Thi();
                chon_Bai_Thi.Text = "Chọn Bài Thi Thử";
                chon_Bai_Thi.ShowDialog();
            }
            //có Đề  Thi
            if (_ma_de_thi_ != null && Dem_Lan_Bam == 0)
            {
                //Load De
                De = BUS_De_Thi.Lay_De_Thi_(_ma_de_thi_);
                Thoi_gian_lam_bai = BUS_De_Thi.Thoi_Gian_Thi(_ma_de_thi_);
                DS_Cau_Tra_Loi = new List<CauTraLoi>();
                if (De.De.Count > 0)
                {
                    
                    lbTrial_STT_question.Text = "Câu " + (_vi_tri_Cau_hoi_ + 1).ToString() + " / " + De.De.Count.ToString();
                }
               
                Dem_Lan_Bam = 1;

                //set up Thoi Gian
                lbTrial_minute.Text = (Thoi_gian_lam_bai - 1).ToString();
                lbTrial_second.Text = "59";
                Start_time = DateTime.Now;
                btnSuggest.Enabled = true;
                thoi_gian1.Start();
            }
        }

        //thoát thi thử
        private void btnTrial_exit_Click(object sender, EventArgs e)
        {
            controlStudent.TabPages.Clear();
        }

        //cập nhật câu hỏi
        private void lbTrial_STT_question_TextChanged(object sender, EventArgs e)
        {
            if (De !=null)
            {
                if (De.De[_vi_tri_Cau_hoi_].noi_dung.Length > 50)
                {
                    De.De[_vi_tri_Cau_hoi_].noi_dung = De.De[_vi_tri_Cau_hoi_].noi_dung.Replace("\n", ""); ;
                    De.De[_vi_tri_Cau_hoi_].noi_dung = De.De[_vi_tri_Cau_hoi_].noi_dung.Insert(De.De[_vi_tri_Cau_hoi_].noi_dung.Length / 2, "\n");
                }
                lbTrial_Question.Text = De.De[_vi_tri_Cau_hoi_].noi_dung;
                rbtnTrial_A.Text = De.De[_vi_tri_Cau_hoi_].Cau_A;
                rbtnTrial_B.Text = De.De[_vi_tri_Cau_hoi_].Cau_B;
                rbtnTrial_C.Text = De.De[_vi_tri_Cau_hoi_].Cau_C;
                rbtnTrial_D.Text = De.De[_vi_tri_Cau_hoi_].Cau_D;
            }


        }

        //Tới
        private void btnTrial_Next_Click(object sender, EventArgs e)
        {
            btnTrial_submit.Enabled = true;
            Lua_Chon_TT();
            reset_Lua_Chon();
            _vi_tri_Cau_hoi_++;

            lbTrial_STT_question.Text = "Câu " + (_vi_tri_Cau_hoi_ + 1).ToString() + " / " + De.De.Count.ToString();
            Lua_Chon(_vi_tri_Cau_hoi_);

            int so_cau = De.De.Count;
            if (_vi_tri_Cau_hoi_ > 0 && _vi_tri_Cau_hoi_ < so_cau)
            {
                btnTrial_Prev.Enabled = true;
            }
            if (_vi_tri_Cau_hoi_ == so_cau - 1)
            {
                _vi_tri_Cau_hoi_ = so_cau - 1;
                btnTrial_Next.Enabled = false;
            }
        }

        //Lui
        private void btnTrial_Prev_Click(object sender, EventArgs e)
        {
            Lua_Chon_TT();
            reset_Lua_Chon();
            _vi_tri_Cau_hoi_--;
            if (De.De.Count > 0)
            {
                lbTrial_STT_question.Text = "Câu " + (_vi_tri_Cau_hoi_ + 1).ToString() + " / " + De.De.Count.ToString();
            }

            Lua_Chon(_vi_tri_Cau_hoi_);
            int so_cau = De.De.Count;
            if (_vi_tri_Cau_hoi_ > 0)
            {
                btnTrial_Next.Enabled = true;
            }
            if (_vi_tri_Cau_hoi_ == 0)
            {
                _vi_tri_Cau_hoi_ = 0;
                btnTrial_Prev.Enabled = false;
            }
        }

        //nộp bài thi thử
        private void Lua_Chon_TT()
        {
            End_time = DateTime.Now;
            CauTraLoi ctl = new CauTraLoi();

            if (rbtnTrial_A.Checked)
            {
                ctl.new_Cau_Tra_Loi(_vi_tri_Cau_hoi_, rbtnTrial_A.Text, De.De[_vi_tri_Cau_hoi_].Dap_An);

            }
            else if (rbtnTrial_B.Checked)
            {
                ctl.new_Cau_Tra_Loi(_vi_tri_Cau_hoi_, rbtnTrial_B.Text, De.De[_vi_tri_Cau_hoi_].Dap_An);
            }
            else if (rbtnTrial_C.Checked)
            {
                ctl.new_Cau_Tra_Loi(_vi_tri_Cau_hoi_, rbtnTrial_C.Text, De.De[_vi_tri_Cau_hoi_].Dap_An);
            }
            else if (rbtnTrial_D.Checked)
            {
                ctl.new_Cau_Tra_Loi(_vi_tri_Cau_hoi_, rbtnTrial_D.Text, De.De[_vi_tri_Cau_hoi_].Dap_An);
            }
            else
            {
                ctl.new_Cau_Tra_Loi(_vi_tri_Cau_hoi_, "", De.De[_vi_tri_Cau_hoi_].Dap_An);
            }

            for (int i = 0; i < DS_Cau_Tra_Loi.Count; i++)
            {

                if (DS_Cau_Tra_Loi[i].index == _vi_tri_Cau_hoi_)
                {
                    DS_Cau_Tra_Loi[i] = ctl;
                    return;
                }
            }

            DS_Cau_Tra_Loi.Add(ctl);
        }

        // Tải Lại Cau trả Loi
        private void Lua_Chon(int vi_tri_cau_tra_loi)
        {
            for (int i = 0; i < DS_Cau_Tra_Loi.Count; i++)
            {
                if (_vi_tri_Cau_hoi_ == DS_Cau_Tra_Loi[i].index)
                {
                    if (DS_Cau_Tra_Loi[i].cau_tra_loi == rbtnTrial_A.Text)
                    {
                        rbtnTrial_A.Checked = true;
                        break;
                    }
                    else if (DS_Cau_Tra_Loi[i].cau_tra_loi == rbtnTrial_B.Text)
                    {
                        rbtnTrial_B.Checked = true;
                        break;
                    }
                    else if (DS_Cau_Tra_Loi[i].cau_tra_loi == rbtnTrial_C.Text)
                    {
                        rbtnTrial_C.Checked = true;
                        break;
                    }
                    else if (DS_Cau_Tra_Loi[i].cau_tra_loi == rbtnTrial_D.Text)
                    {
                        rbtnTrial_D.Checked = true;
                        break;
                    }
                }
            }

        }

        //Gợi Ý Đáp Án
        private void btnSuggest_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đáp án : " + De.De[_vi_tri_Cau_hoi_].Dap_An, "Gợi Ý");
        }

        //Nộp bài
        private void btnTrial_submit_Click(object sender, EventArgs e)
        {
            thoi_gian1.Stop();
            Lua_Chon_TT();
            double So_cau_Dung = 0;
            double Tong_So_cau = De.De.Count;
            double Diem;

            for (int i = 0; i < DS_Cau_Tra_Loi.Count; i++)
            {
                if (DS_Cau_Tra_Loi[i].cau_tra_loi.Replace(" ", "") == DS_Cau_Tra_Loi[i].Dap_An.Replace(" ",""))
                {
                    So_cau_Dung++;
                }
            }
            Diem = (So_cau_Dung / Tong_So_cau) * 10;
            Diem = Math.Round(Diem, 2);


            for (int i = 0; i < De.De.Count; i++)
            {
                if (i > DS_Cau_Tra_Loi.Count-1 )
                {
                    CauTraLoi cauTraLoi = new CauTraLoi();
                    cauTraLoi.new_Cau_Tra_Loi(i, "", De.De[i].Dap_An);
                    DS_Cau_Tra_Loi.Add(cauTraLoi);
                }
            }
            KETQUATHI kqua = new KETQUATHI();
            kqua.MaKyThi = _ma_ky_thi_;
            kqua.MaDeThi = _ma_de_thi_;
            kqua.TaiKhoan = hs.Tai_Khoan;
            kqua.Diem = Diem;
            kqua.ThoiGianBatDau = Start_time;
            kqua.ThoiGianKetThuc = End_time;
            frmDap_An.DS_Cau_Tra_Loi = DS_Cau_Tra_Loi;
            if (BUS_Hoc_Sinh.Luu_Ket_Qua(kqua) != true)
            {
                BUS_Hoc_Sinh.Cap_Nhat_Ket_Qua(kqua, Diem);
            }
               MessageBox.Show("Đã Lưu ^_^", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            frmKetQua.Diem = Diem;
            frmKetQua frmKetQua1 = new frmKetQua();
            frmKetQua1.ShowDialog();

            btnTrial_submit.Enabled = false;
            btnTrial_Prev.Enabled = false;
            btnTrial_Next.Enabled = false;
            btnTrial_start_pause.Enabled = false;
        }

        //thời gian thi thử
        private void thoi_gian1_Tick(object sender, EventArgs e)
        {
            int a = int.Parse(lbTrial_minute.Text);
            int b = int.Parse(lbTrial_second.Text);

            b--;
            if (a == 0 && b == 0)
            {
                thoi_gian1.Stop();
                MessageBox.Show("Hết giờ !!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnTrial_submit_Click(sender, e);
                a = Thoi_gian_lam_bai;
                b = 0;
                lbTrial_second.Text = b.ToString();
                lbTrial_minute.Text = a.ToString();

            }
            if (b == 0)
            {
                a--;
                b = 59;
            }

            lbTrial_second.Text = b.ToString();
            lbTrial_minute.Text = a.ToString();
        }

        //Đăng Xuất 
        private void btnLog_out_Click(object sender, EventArgs e)
        {
            frmDang_Nhap frm = new frmDang_Nhap();
            frm.Show();
            this.Close();
        }

        //đổi mật khẩu
        private void btnChange_Password_Click(object sender, EventArgs e)
        {
            frmDoi_Mat_Khau.Tai_Khoan = hs.Tai_Khoan;
            frmDoi_Mat_Khau doi_Mat_Khau = new frmDoi_Mat_Khau();
            doi_Mat_Khau.ShowDialog();
        }

        //Ôn Tập
        private void btnDocument_Click(object sender, EventArgs e)
        {
            frmChon_De_On De = new frmChon_De_On();
            De.ShowDialog();
        }

        // kết quả
        private void btnResult_Click(object sender, EventArgs e)
        {
            controlStudent.TabPages.Clear();
            controlStudent.TabPages.Add(TabResult);
          
            using (var QLTTN = new QLTTNDataContext())
            {
                var querry = from q in QLTTN.KETQUATHIs
                             where q.TaiKhoan == hs.Tai_Khoan
                             orderby q.MaKyThi
                             select new
                             {
                                 q.CHITIETKYTHI.MaKyThi,
                                 q.CHITIETKYTHI.KYTHI.TenKyThi,
                                 q.CHITIETKYTHI.KYTHI.LOAIKYTHI.TenLoaiKyThi,
                                 q.Diem
                             };
                if (querry.Count() == 0)
                {
                    return;
                }
                double s = 0;
                int c = 0;
                int index = 0;
                var lst = querry.ToList();
                string t = lst[0].MaKyThi;
                dt_result.RowCount = lst.Count;

                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i].MaKyThi == t )
                    {
                        s += lst[i].Diem.Value;
                        c++;
                    }
                    else
                    {


                        dt_result.Rows[index].Cells[0].Value = lst[i - 1].MaKyThi;
                        dt_result.Rows[index].Cells[1].Value = lst[i - 1].TenKyThi;
                        dt_result.Rows[index].Cells[2].Value = lst[i - 1].TenLoaiKyThi;
                        dt_result.Rows[index].Cells[3].Value =( s / c);

                        t = lst[i].MaKyThi;
                        s = lst[i].Diem.Value;
                        c = 1;

                        index++;

                    }

                    if (i == lst.Count - 1)
                    {
                        dt_result.Rows[index].Cells[0].Value = lst[i].MaKyThi;
                        dt_result.Rows[index].Cells[1].Value = lst[i].TenKyThi;
                        dt_result.Rows[index].Cells[2].Value = lst[i].TenLoaiKyThi;
                        dt_result.Rows[index].Cells[3].Value = (s / c);

                    }
                }

                for (int i = index+1; i < dt_result.Rows.Count; i++)
                {
                    dt_result.Rows.RemoveAt(i);
                }
            }
        }

        // chọn kết quả in
        private void dt_result_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dt_result.CurrentCell.RowIndex;

            PhieuDiem pd = new PhieuDiem();
            PhieuDiem._ma_ky_thi_ = dt_result.Rows[index].Cells[0].Value.ToString();
            PhieuDiem.TaiKhoan_ = hs.Tai_Khoan;

            pd.ShowDialog();
        }
    }
}
