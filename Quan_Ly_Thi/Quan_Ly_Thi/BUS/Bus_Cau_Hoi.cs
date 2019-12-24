using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Linq;
using Quan_Ly_Thi.DAO;
using Quan_Ly_Thi.DTO;
using System.ComponentModel;
using Microsoft.Office.Interop.Excel;
using System.Threading;

public class Bus_Cau_Hoi
{
    //public static Table<Cau_Hoi> getQuestion()
    // {
    // QLTTNDataContext db = new QLTTNDataContext();
    // return db.GetTable<Cau_Hoi>();
    // }
    public static List<Cau_Hoi> getListQuestion()
    {
        return DAO_Cau_Hoi.getListQuestion().ToList();
    }
    public static void insertQuestion(Cau_Hoi ch)
    {
        DAO_Cau_Hoi.AddNewQuestion(ch);    
    }
    public static void deleteQuestionByID(string mach)
    {
        DAO_Cau_Hoi.deleteQuestion(mach);
    }

    static public List<KHOI> layTatCaKhoi()
    {
        using (QLTTNDataContext qlttn = new QLTTNDataContext())
        {
            return qlttn.KHOIs.ToList<KHOI>();
        }
    }
    static public List<MONHOC> layTatCaMonHoc()
    {
        using (QLTTNDataContext qlttn = new QLTTNDataContext())
        {
            return qlttn.MONHOCs.ToList<MONHOC>();
        }
    }
    static public List<CAPDOCAUHOI> layTatCaCapDo()
    {
        using (QLTTNDataContext qlttn = new QLTTNDataContext())
        {
            return qlttn.CAPDOCAUHOIs.ToList<CAPDOCAUHOI>();
        }
    }
    static public List<LOAICAUHOI> layTatCaLoaiCauHoi()
    {
        using (QLTTNDataContext qlttn = new QLTTNDataContext())
        {
            return qlttn.LOAICAUHOIs.ToList<LOAICAUHOI>();
        }
    }
    static public string tuDongTangKhoaCauHoi()
    {
        int ma = 1;
        
        if (DAO_Cau_Hoi.getAllListQuestion().Count > 0)
        {
            string Temp = DAO_Cau_Hoi.getAllListQuestion().Last().ma_CH.Substring(2);
            ma = int.Parse(Temp) + 1;
        }
        string maCH = "CH" + ma.ToString("D6") + "  ";
        return maCH;

    }

    public static void ImportQuestion(string Path, DataGridView gridView)
    {
        Microsoft.Office.Interop.Excel.Application xlApp;
        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        Microsoft.Office.Interop.Excel.Range xlRange;

        if (Path != string.Empty)
        {
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(Path);
            xlWorkSheet = xlWorkBook.Worksheets["Sheet1"];
            xlRange = xlWorkSheet.UsedRange;

            gridView.ColumnCount = xlRange.Columns.Count;
            int n = 0;
            for (int i = 1; i < gridView.ColumnCount; i++)
            {
                gridView.Columns[n].Name = xlRange.Cells[1, i].Text;
                n++;
            }

            for (int i = 2; i <= xlRange.Rows.Count; i++)
            {
                gridView.Rows.Add(xlRange.Cells[i, 1].Text, xlRange.Cells[i, 2].Text, xlRange.Cells[i, 3].Text, xlRange.Cells[i, 4].Text, xlRange.Cells[i, 5].Text, xlRange.Cells[i, 6].Text, xlRange.Cells[i, 7].Text, xlRange.Cells[i, 8].Text, xlRange.Cells[i, 9].Text, xlRange.Cells[i, 10].Text, xlRange.Cells[i, 11].Text, xlRange.Cells[i, 12].Text);

                DAO_Cau_Hoi.InsertQuestionWithExcel(xlRange, i);
            }

            xlWorkBook.Close();
            xlApp.Quit();
        }
    }

    public static void btnExport_Question_Click(DataParameter _inputParameter, BackgroundWorker bgWorker_Export, DataGridView dtgv_ListQuestion, ProgressBar pgBar)
    {
        if (bgWorker_Export.IsBusy)
        {
            return;
        }
        using (SaveFileDialog s_File = new SaveFileDialog() { Filter = "Excel Workbook|*.xls" })
        {
            if (s_File.ShowDialog() == DialogResult.OK)
            {
                _inputParameter.FileName = s_File.FileName;
                List<CAUHOI> listQuestion = new List<CAUHOI>();
                for (int i = 0; i < dtgv_ListQuestion.Rows.Count - 1; i++)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row = dtgv_ListQuestion.Rows[i];
                    CAUHOI user = new CAUHOI()
                    {
                        MaCauHoi = row.Cells[0].Value.ToString(),
                        MaKhoi = row.Cells[1].Value.ToString(),
                        MaMonHoc = row.Cells[2].Value.ToString(),
                        MaLoaiCauHoi = row.Cells[3].Value.ToString(),
                        MaCapDo = row.Cells[4].Value.ToString(),
                        CauHoi1 = row.Cells[5].Value.ToString(),
                        CauA = row.Cells[6].Value.ToString(),
                        CauB = row.Cells[7].Value.ToString(),
                        CauC = row.Cells[8].Value.ToString(),
                        CauD = row.Cells[9].Value.ToString(),
                        DapAn = row.Cells[10].Value.ToString(),
                        GoiY = row.Cells[11].Value.ToString()
                    };
                    listQuestion.Add(user);
                }

                _inputParameter.listQuestion = listQuestion;

                pgBar.Minimum = 0;
                pgBar.Value = 0;
                bgWorker_Export.RunWorkerAsync(_inputParameter);
            }
        }
    }

    public static void bgWorker_Export_Question_DoWork(object sender, DoWorkEventArgs e, BackgroundWorker bgWorker_Export)
    {
        List<CAUHOI> listQuestion = ((DataParameter)e.Argument).listQuestion;
        string FileName = ((DataParameter)e.Argument).FileName;

        Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel.Workbook xlWorkBook = Excel.Workbooks.Add(XlSheetType.xlWorksheet);
        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = (Worksheet)Excel.ActiveSheet;
        Excel.Visible = false;
        int Index = 1;
        int Process = listQuestion.Count;

        xlWorkSheet.Cells[1, 1] = "Ma_CH";
        xlWorkSheet.Cells[1, 2] = "Ma_Khoi";
        xlWorkSheet.Cells[1, 3] = "Ma_Mon";
        xlWorkSheet.Cells[1, 4] = "Ma_LoaiCH";
        xlWorkSheet.Cells[1, 5] = "Ma_CD";
        xlWorkSheet.Cells[1, 6] = "Noi_Dung";
        xlWorkSheet.Cells[1, 7] = "CauA";
        xlWorkSheet.Cells[1, 8] = "CauB";
        xlWorkSheet.Cells[1, 9] = "CauC";
        xlWorkSheet.Cells[1, 10] = "CauD";
        xlWorkSheet.Cells[1, 11] = "Dap_An";
        xlWorkSheet.Cells[1, 12] = "GoiY";



        foreach (CAUHOI nd in listQuestion)
        {
            if (!bgWorker_Export.CancellationPending)
            {
                bgWorker_Export.ReportProgress(Index++ * 100 / Process);
                xlWorkSheet.Cells[Index, 1] = nd.MaCauHoi;
                xlWorkSheet.Cells[Index, 2] = nd.MaKhoi;
                xlWorkSheet.Cells[Index, 3] = nd.MaMonHoc;
                xlWorkSheet.Cells[Index, 4] = nd.MaLoaiCauHoi;
                xlWorkSheet.Cells[Index, 5] = nd.MaCapDo;
                xlWorkSheet.Cells[Index, 6] = nd.CauHoi1;
                xlWorkSheet.Cells[Index, 7] = nd.CauA;
                xlWorkSheet.Cells[Index, 8] = nd.CauB;
                xlWorkSheet.Cells[Index, 9] = nd.CauC;
                xlWorkSheet.Cells[Index, 10] = nd.CauD;
                xlWorkSheet.Cells[Index, 11] = nd.DapAn;
                xlWorkSheet.Cells[Index, 12] = nd.GoiY;
            }
        }

        xlWorkSheet.SaveAs(FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
        xlWorkBook.Close();
        Excel.Quit();
    }

    public static void bgWorker_Export_Question_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e, System.Windows.Forms.Label lbStatus)
    {
        if (e.Error == null)
        {
            Thread.Sleep(100);
            lbStatus.Text = "Your Data Has Been Sucessfully Exported!";
        }
    }

    public static void bgWorker_Export_Question_ProgressChanged(object sender, ProgressChangedEventArgs e, ProgressBar pgBar, System.Windows.Forms.Label lbStatus)
    {
        pgBar.Value = e.ProgressPercentage;
        lbStatus.Text = string.Format("Process...{0}%", e.ProgressPercentage);
        pgBar.Update();
    }
}

