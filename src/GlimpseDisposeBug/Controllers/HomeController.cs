using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GlimpseDisposeBug.Controllers
{
    public class HomeController : Controller 
    {
        //
        // GET: /Home/

        private IDisposable _disposableVM;

        public ActionResult Index()
        {
            var model = new Models.DisposableIndexModel();
            _disposableVM = model;

            return View(model);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (null != _disposableVM)
                {
                    _disposableVM.Dispose();
                    _disposableVM = null;
                }
            }
            base.Dispose(disposing);
        }

        
    }
}
