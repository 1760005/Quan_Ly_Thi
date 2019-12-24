using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Thi.DTO
{
    public class ReportTongKetKyThi : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string _taiKhoan { get; set; }
        string _hoTen { get; set; }
        string _ngaySinh { get; set; }
        string _tenLop { get; set; }
        string _monHoc { get; set; }
        float _diem { get; set; }

        public string TaiKhoan
        {
            get => _taiKhoan;
            set
            {
                _taiKhoan = value;
                OnPropertyChanged("TaiKhoan");
            }
        }

        public string HoTen
        {
            get => _hoTen;
            set
            {
                _hoTen = value;
                OnPropertyChanged("HoTen");
            }
        }


        public string TenLop
        {
            get => _tenLop;
            set
            {
                _tenLop = value;
                OnPropertyChanged("TenLop");
            }
        }

        public string NgaySinh
        {
            get => _ngaySinh;
            set
            {
                _ngaySinh = value;
                OnPropertyChanged("NgaySinh");
            }
        }

        public string MonHoc
        {
            get => _monHoc;
            set
            {
                _monHoc = value;
                OnPropertyChanged("MonHoc");
            }
        }

        public float Diem
        {
            get => _diem;
            set
            {
                _diem = value;
                OnPropertyChanged("Diem");
            }
        }

        void OnPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
    }
}
