using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Thi.DTO
{
    public class DanhSachKyThi : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string _maKyThi;
        string _tenKyThi;
        string _maDeThi;
        string _maMonHoc;
        string _maKhoi;
        string _tenKhoi;
        string _tenMonhoc;
        int _thoiGianLamBai;
        DateTime _thoiGianBatDau;

        public string MaKyThi
        {
            get => _maKyThi;
            set
            {
                _maKyThi = value;
                OnPropertyChanged("MaKyThi");
            }
        }

        public string TenKyThi
        {
            get => _tenKyThi;
            set
            {
                _tenKyThi = value;
                OnPropertyChanged("MaKyThi");
            }
        }


        public string MaDeThi
        {
            get => _maDeThi;
            set
            {
                _maDeThi = value;
                OnPropertyChanged("MaDeThi");
            }
        }

        public string MaMonHoc
        {
            get => _maMonHoc;
            set
            {
                _maMonHoc = value;
                OnPropertyChanged("MaMonHoc");
            }
        }

        public string MaKhoi
        {
            get => _maKhoi;
            set
            {
                _maKhoi = value;
                OnPropertyChanged("MaKhoi");
            }
        }

        public string TenKhoi
        {
            get => _tenKhoi;
            set
            {
                _tenKhoi = value;
                OnPropertyChanged("TenKhoi");
            }
        }

        public string TenMonHoc
        {
            get => _tenMonhoc;
            set
            {
                _tenMonhoc = value;
                OnPropertyChanged("TenMonHoc");
            }
        }


        public int ThoiGianLamBai
        {
            get => _thoiGianLamBai;
            set
            {
                _thoiGianLamBai = value;
                OnPropertyChanged("ThoiGianLamBai");
            }
        }

        public DateTime ThoiGianBatDau
        {
            get => _thoiGianBatDau;
            set
            {
                _thoiGianBatDau = value;
                OnPropertyChanged("ThoiGianBatDau");
            }
        }

        void OnPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

    }
}
