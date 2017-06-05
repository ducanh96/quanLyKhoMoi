using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKho.Models
{
    public class ListPhieuNhapChiTiet
    {
        public List<PhieuNhapChiTiet> ListPNCT { get; set; }
        public ListPhieuNhapChiTiet()
        {
            ListPNCT = new List<PhieuNhapChiTiet>();
        }
    }
}