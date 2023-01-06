using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using kt_v1.Models;

namespace kt_v1.Controllers
{
    public class SanPhamController : ApiController
    {
        CSDLTestEntities db = new CSDLTestEntities();
        // lay du lieu
        [HttpGet]
        public List<SanPham> LaySP()
        {
            return db.SanPhams.ToList();
        }

        [HttpGet]
        public List<SanPham> TimSPTheoDanhMuc(int madm)
        {
            return db.SanPhams.Where(x => x.MaDanhMuc == madm).ToList();
        }

        [HttpGet]
        public SanPham TimSP(int ma)
        {
            return db.SanPhams.SingleOrDefault(x => x.Ma == ma);
        }

        // them du lieu
        [HttpPost]
        public bool ThemMoi(int ma, string ten, int gia, int madm)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.Ma == ma);
            if(sp == null)
            {
                SanPham sp1 = new SanPham();
                sp1.Ma = ma;
                sp1.Ten = ten;
                sp1.DonGia = gia;
                sp1.MaDanhMuc = madm;
                db.SanPhams.Add(sp1);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        // sua du lieu
        [HttpPut]
        public bool CapNhat(int ma, string ten, int gia, int madm)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.Ma == ma);
            if(sp != null)
            {
                sp.Ma = ma;
                sp.Ten = ten;
                sp.DonGia = gia;
                sp.MaDanhMuc = madm;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        // xoa du lieu
        [HttpDelete]
        public bool Xoa(int ma)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.Ma == ma);
            if(sp != null)
            {
                db.SanPhams.Remove(sp);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
