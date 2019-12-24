using Quan_Ly_Thi.DTO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Linq;
using System;

namespace Quan_Ly_Thi.DAO
{
    public class DAO_Cau_Hoi
    {
        public static List<Cau_Hoi> getAllListQuestion()
        {
            List<Cau_Hoi> listQuestion = new List<Cau_Hoi>();
            using (var QLTTN = new QLTTNDataContext())
            {
                
                var query = QLTTN.CAUHOIs;
                foreach (var i in query)
                {
                    Cau_Hoi ch = new Cau_Hoi()
                    {
                        Cau_A = i.CauA,
                        Cau_B = i.CauB,
                        Cau_C = i.CauC,
                        Cau_D = i.CauD,
                        Dap_An = i.DapAn,
                        goi_Y = i.GoiY,
                        noi_dung = i.CauHoi1,
                        ma_CH = i.MaCauHoi,
                        Ten_CD = i.MaCapDo,
                        Ten_Khoi = i.MaKhoi,
                        Ten_LoaiCH = i.MaLoaiCauHoi,
                        Ten_Mon = i.MaMonHoc
                    };
                    listQuestion.Add(ch);
                }
            }
            return listQuestion;
        }
        public static List<Cau_Hoi> getListQuestion()
        {
            List<Cau_Hoi> data = new List<Cau_Hoi>();
            using (QLTTNDataContext db = new QLTTNDataContext())
            {

                var query = (from ch in db.CAUHOIs
                             join kh in db.KHOIs on ch.MaKhoi equals kh.MaKhoi
                             join mh in db.MONHOCs on ch.MaMonHoc equals mh.MaMonHoc
                             join cd in db.CAPDOCAUHOIs on ch.MaCapDo equals cd.MaCapDo
                             join lch in db.LOAICAUHOIs on ch.MaLoaiCauHoi equals lch.MaLoaiCauHoi
                             where ch.MaKhoi == kh.MaKhoi
                                && ch.MaMonHoc == mh.MaMonHoc
                                && ch.MaLoaiCauHoi == lch.MaLoaiCauHoi
                                && ch.MaCapDo == cd.MaCapDo
                             select new
                             {
                                 MA_CH = ch.MaCauHoi,
                                 KHOI_HOC = kh.TenKhoi,
                                 MON_HOC = mh.TenMonHoc,
                                 LOAI_CH = lch.TenLoaiCauHoi,
                                 CAP_DO = cd.TenCapDo,
                                 NOI_DUNG = ch.CauHoi1,
                                 CAU_A = ch.CauA,
                                 CAU_B = ch.CauB,
                                 CAU_C = ch.CauC,
                                 CAU_D = ch.CauD,
                                 DAP_AN = ch.DapAn,
                                 GOI_Y = ch.GoiY,
                             });

                foreach (var item in query)
                {
                    Cau_Hoi c = new Cau_Hoi();
                    c.ma_CH = item.MA_CH;
                    c.Ten_Khoi = item.KHOI_HOC;
                    c.Ten_Mon = item.MON_HOC;
                    c.Ten_LoaiCH = item.LOAI_CH;
                    c.Ten_CD = item.CAP_DO;
                    c.noi_dung = item.NOI_DUNG;
                    c.Cau_A = item.CAU_A;
                    c.Cau_B = item.CAU_B;
                    c.Cau_C = item.CAU_C;
                    c.Cau_D = item.CAU_D;
                    c.Dap_An = item.DAP_AN;
                    c.goi_Y = item.GOI_Y;
                    data.Add(c);
                }
            }
            return data;
        }
        public static void AddNewQuestion(Cau_Hoi ch1)
        {
            using (QLTTNDataContext db = new QLTTNDataContext())
            {
                var query = (from ch in db.CAUHOIs
                             join kh in db.KHOIs on ch.MaKhoi equals kh.MaKhoi
                             join mh in db.MONHOCs on ch.MaMonHoc equals mh.MaMonHoc
                             join cd in db.CAPDOCAUHOIs on ch.MaCapDo equals cd.MaCapDo
                             join lch in db.LOAICAUHOIs on ch.MaLoaiCauHoi equals lch.MaLoaiCauHoi
                             
                             where ch.MaCauHoi == ch1.ma_CH
                             select new
                             {
                                 ch
                             }).SingleOrDefault();
                if (query == null)
                {
                    try
                    {
                        CAUHOI c = new CAUHOI();
                        c.MaCauHoi = ch1.ma_CH;
                        c.MaKhoi = ch1.Ten_Khoi;
                        c.MaMonHoc = ch1.Ten_Mon;
                        c.MaLoaiCauHoi = ch1.Ten_LoaiCH;
                        c.MaCapDo = ch1.Ten_CD;
                        c.CauHoi1 = ch1.noi_dung;
                        c.CauA = ch1.Cau_A;
                        c.CauB = ch1.Cau_B;
                        c.CauC = ch1.Cau_C;
                        c.CauD = ch1.Cau_D;
                        c.DapAn = ch1.Dap_An;
                        c.GoiY = ch1.goi_Y;
                        db.CAUHOIs.InsertOnSubmit(c);
                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    try
                    {
                        query.ch.MaCauHoi = ch1.ma_CH;
                        query.ch.MaKhoi = ch1.Ten_Khoi;
                        query.ch.MaMonHoc = ch1.Ten_Mon;
                        query.ch.MaLoaiCauHoi = ch1.Ten_LoaiCH;
                        query.ch.MaCapDo = ch1.Ten_CD;
                        query.ch.CauHoi1 = ch1.noi_dung;
                        query.ch.CauA = ch1.Cau_A;
                        query.ch.CauB = ch1.Cau_B;
                        query.ch.CauC = ch1.Cau_C;
                        query.ch.CauD = ch1.Cau_D;
                        query.ch.DapAn = ch1.Dap_An;
                        query.ch.GoiY = ch1.goi_Y;

                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }


            }
        }
        public static void deleteQuestion(string mach)
        {
            using (QLTTNDataContext db = new QLTTNDataContext())
            {
                CAUHOI query = (from ch in db.CAUHOIs
                             join kh in db.KHOIs on ch.MaKhoi equals kh.MaKhoi
                             join mh in db.MONHOCs on ch.MaMonHoc equals mh.MaMonHoc
                             join cd in db.CAPDOCAUHOIs on ch.MaCapDo equals cd.MaCapDo
                             join lch in db.LOAICAUHOIs on ch.MaLoaiCauHoi equals lch.MaLoaiCauHoi
                             where ch.MaCauHoi == mach
                                && ch.MaMonHoc == mh.MaMonHoc
                                && ch.MaLoaiCauHoi == lch.MaLoaiCauHoi
                                && ch.MaCapDo == cd.MaCapDo
                             select ch).SingleOrDefault();

                var ctdt = db.CHITIETDETHIs.Where(ct => ct.MaCauHoi.Equals(mach)).Select(ct => ct);

                foreach (var i in ctdt)
                {
                    CHITIETDETHI ch = i;
                    db.CHITIETDETHIs.DeleteOnSubmit(i);
                }
                try
                {
                    
                    db.CAUHOIs.DeleteOnSubmit(query);                    
                    db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static void InsertQuestionWithExcel(Microsoft.Office.Interop.Excel.Range xlRange, int row)
        {
            using (QLTTNDataContext dataContext = new QLTTNDataContext())
            {
                int column = 1;
                CAUHOI newUser = new CAUHOI()
                {
                    MaCauHoi = xlRange.Cells[row, column].Text,
                    MaKhoi = xlRange.Cells[row, column + 1].Text,
                    MaMonHoc = xlRange.Cells[row, column + 2].Text,
                    MaLoaiCauHoi = xlRange.Cells[row, column + 3].Text,
                    MaCapDo = xlRange.Cells[row, column + 4].Text,
                    CauHoi1 = xlRange.Cells[row, column + 5].Text,
                    CauA = xlRange.Cells[row, column + 6].Text,
                    CauB = xlRange.Cells[row, column + 7].Text,
                    CauC = xlRange.Cells[row, column + 8].Text,
                    CauD = xlRange.Cells[row, column + 9].Text,
                    DapAn = xlRange.Cells[row, column + 10].Text,
                    GoiY = xlRange.Cells[row, column + 11].Text
                };

                dataContext.CAUHOIs.InsertOnSubmit(newUser);
                dataContext.SubmitChanges();
            }
        }
    }
}


           
        
    


