using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaoWebAPI.Models;

namespace TaoWebAPI.Controllers
{
    public class SanPhamController : ApiController
    {
        QLBanHangEntities db = new QLBanHangEntities();

        [HttpGet]
        public List<SanPham> DanhSach()
        {
            return db.SanPhams.ToList();
        }

        [HttpPost]
        public bool ThemSP(int masp, string ten, string dv, int sl, int gia)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == masp);
            if (sp == null)
            {
                SanPham sp1 = new SanPham();
                sp1.MaSP = masp;
                sp1.TenSP = ten;
                sp1.DVTinh = dv;
                sp1.SoLuong = sl;
                sp1.Gia = gia;
                db.SanPhams.Add(sp1);
                db.SaveChanges();
                return true;
            } else
            {
                return false;
            }
        }

        [HttpDelete]
        public bool XoaSP(int masp)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == masp);
            if(sp == null)
            {
                return false;
            } else
            {
                db.SanPhams.Remove(sp);
                db.SaveChanges();
                return true;
            }
        }
    }
}
