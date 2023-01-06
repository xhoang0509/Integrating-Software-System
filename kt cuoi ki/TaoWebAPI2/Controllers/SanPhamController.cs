using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaoWebAPI2.Models;

namespace TaoWebAPI2.Controllers
{
    public class SanPhamController : ApiController
    {
        QLBanHang2Entities db = new QLBanHang2Entities();

        [HttpGet]
        public List<SanPham> DanhSach()
        {
            return db.SanPhams.ToList();
        }

        [HttpPost]
        public bool ThemSP(int ma, string ten, string dv, int sl, int gia)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == ma);
            if (sp == null)
            {
                SanPham sp1 = new SanPham();
                sp1.MaSP = ma;
                sp1.TenSP = ten;
                sp1.DVTinh = dv;
                sp1.SoLuong = sl;
                sp1.Gia = gia;
                db.SanPhams.Add(sp1);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpDelete]
        public bool XoaSP(int ma)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == ma);
            if (sp != null)
            {
                db.SanPhams.Remove(sp);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
