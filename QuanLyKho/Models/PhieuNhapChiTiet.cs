using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLyKho.Models.ModelDB;

namespace QuanLyKho.Models
{
    public class PhieuNhapChiTiet
    {
        public Phieu_Nhap PN { get; set; }
        public List<Phieu_Nhap_Hang_Hoa> ListPNHH { get; set; }
        
        public PhieuNhapChiTiet()
        {
            PN = new Phieu_Nhap();
            ListPNHH = new List<Phieu_Nhap_Hang_Hoa>();
        }
    }
}