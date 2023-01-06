using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using kt_v1.Models;

namespace kt_v1.Controllers
{
    public class DanhMucController : ApiController
    {
        CSDLTestEntities db = new CSDLTestEntities();

        // lay du lieu
        [HttpGet]
        public List<DanhMuc> LayDM()
        {
            return db.DanhMucs.ToList();
        }
    }
}
