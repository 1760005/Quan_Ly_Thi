using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Thi.BUS;
using Quan_Ly_Thi.DAO;
using Quan_Ly_Thi.GUI;
using Quan_Ly_Thi.DTO;
using System.Data.Linq;
using System.Windows.Forms;

namespace Quan_Ly_Thi.GUI.Giao_Vien
{
    public partial class frmGiao_Vien : Form
    {
        public frmGiao_Vien()
        {
            InitializeComponent();
            btnList_question.Click += BtnList_question_Click;
        }

        private void BtnList_question_Click(object sender, EventArgs e)
        {
            if (TabControlTeacher.TabPages.Contains(TabList_question))
            {
                TabControlTeacher.SelectedTab = TabList_question;
                return;
            }
            else
                TabControlTeacher.TabPages.Add(TabList_question);

            cboxGrade.DataSource = Bus_Cau_Hoi.layTatCaKhoi();
            cboxGrade.DisplayMember = "TenKhoi";
            cboxGrade.ValueMember = "MaKhoi";

            cboxSubject.DataSource = Bus_Cau_Hoi.layTatCaMonHoc();
            cboxSubject.DisplayMember = "TenMonHoc";
            cboxSubject.ValueMember = "MaMonHoc";

            cboxLevelQuestion.DataSource = Bus_Cau_Hoi.layTatCaCapDo();
            cboxLevelQuestion.DisplayMember = "TenCapDo";
            cboxLevelQuestion.ValueMember = "MaCapDo";

            cboxTypeQuestion.DataSource = Bus_Cau_Hoi.layTatCaLoaiCauHoi();
            cboxTypeQuestion.DisplayMember = "TenLoaiCauHoi";
            cboxTypeQuestion.ValueMember = "MaLoaiCauHoi";

            txtIdQuestion.Text = Bus_Cau_Hoi.tuDongTangKhoaCauHoi();
        }

        private void btnAdd_question_Click(object sender, EventArgs e)
        {           
            Cau_Hoi cauhoi = new Cau_Hoi();

            cauhoi.ma_CH = Bus_Cau_Hoi.tuDongTangKhoaCauHoi();
            cauhoi.Cau_A = txtA.Text;
            cauhoi.Cau_B = txtB.Text;
            cauhoi.Cau_C = txtC.Text;
            cauhoi.Cau_D = txtD.Text;
            cauhoi.noi_dung = txtContentQuestion.Text;
            cauhoi.Ten_Khoi = cboxGrade.SelectedValue.ToString();
            cauhoi.Dap_An = txtAnswerQuestion.Text;
            cauhoi.goi_Y = txtSuggestionQuestion.Text;
            cauhoi.Ten_Mon = cboxSubject.SelectedValue.ToString();
            cauhoi.Ten_CD = cboxLevelQuestion.SelectedValue.ToString();
            cauhoi.Ten_LoaiCH = cboxTypeQuestion.SelectedValue.ToString();
            cauhoi.noi_dung = txtContentQuestion.Text.ToString();

           
            Bus_Cau_Hoi.insertQuestion(cauhoi);
           

            if (Bus_Cau_Hoi.getListQuestion().Count == 0)
            {
                dtgv_ListQuestion.DataSource = null;
            }
            else
            {
                dtgv_ListQuestion.DataSource = Bus_Cau_Hoi.getListQuestion();
            }
        }

        private void btnListQuestion_Click(object sender, EventArgs e)
        {
            List<Cau_Hoi> cauhoi = Bus_Cau_Hoi.getListQuestion();
            dtgv_ListQuestion.DataSource = cauhoi;
        }

        private void btnDelete_question_Click(object sender, EventArgs e)
        {           
            try
            {
                Bus_Cau_Hoi.deleteQuestionByID(Temp.ma_CH);
                Bus_Cau_Hoi.getListQuestion();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
        }

        private void frmGiao_Vien_Load(object sender, EventArgs e)
        {
            TabControlTeacher.TabPages.Clear();
        }

        Cau_Hoi Temp = new Cau_Hoi();
        private void dtgv_ListQuestion_SelectionChanged(object sender, EventArgs e)
        {
            if (dtgv_ListQuestion.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dtgv_ListQuestion.SelectedRows[0];

                Temp.ma_CH = row.Cells[0].Value.ToString();
            }
            

        }

        private void btnImport_question_Click(object sender, EventArgs e)
        {
            TabControlTeacher.TabPages.Add(TabList_question);
            OpenFileDialog file = new OpenFileDialog();
            string Path = null;
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Path = file.FileName;
                Bus_Cau_Hoi.ImportQuestion(Path, dtgv_ListQuestion);
            }
        }

        static DataParameter _inputParameter = new DataParameter();
        private void btnExport_question_Click(object sender, EventArgs e)
        {
            if (TabControlTeacher.SelectedIndex == -1)
            {
                return;
            }
            if (TabControlTeacher.TabPages[TabControlTeacher.SelectedIndex] == TabList_question)
            {
                Bus_Cau_Hoi.btnExport_Question_Click(_inputParameter, bgWorker, dtgv_ListQuestion, pgBar);
            }

            else
            {
                return;
            }
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Bus_Cau_Hoi.bgWorker_Export_Question_DoWork(sender, e, bgWorker);
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Bus_Cau_Hoi.bgWorker_Export_Question_ProgressChanged(sender, e, pgBar, lbStatus);
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Bus_Cau_Hoi.bgWorker_Export_Question_RunWorkerCompleted(sender, e, lbStatus);
        }
    }
}
