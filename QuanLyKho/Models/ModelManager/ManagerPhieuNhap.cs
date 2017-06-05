using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLyKho.Models.ViewModel;
using QuanLyKho.Models.ModelDB;

namespace QuanLyKho.Models.ModelEntities
{
    public class ManagerPhieuNhap
    {
       public bool ThemPhieuNhapHH(int ma_phieu,Phieu_Nhap_Json pn)
        {
            using (QuanLyKhoEntities db = new QuanLyKhoEntities())
            {
                using (var tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        var pnhh = new Phieu_Nhap_Hang_Hoa();
                        pnhh.Phieu_Nhap_Id = ma_phieu;
                        pnhh.Don_gia = pn.Don_gia;
                        pnhh.So_Luong = pn.So_Luong;
                        pnhh.Hang_Hoa_id = pn.Hang_Hoa_id;
                        db.Phieu_Nhap_Hang_Hoa.Add(pnhh);
                        var kho = db.Kho_Chua.Find(pnhh.Hang_Hoa_id);
                        if(kho!=null)
                        {
                            kho.So_Luong += pnhh.So_Luong;
                            db.Entry(kho).State = System.Data.Entity.EntityState.Modified;
                            
                        }
                        else
                        {
                            kho = new Kho_Chua();
                            kho.Hang_Hoa_Id = pnhh.Hang_Hoa_id;
                            kho.So_Luong = pnhh.So_Luong;
                            db.Entry(kho).State = System.Data.Entity.EntityState.Added;
                           
                        }

                        

                      
                        db.SaveChanges();
                        tran.Commit();

                        return true;

                    }catch(Exception)
                    {
                        tran.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}