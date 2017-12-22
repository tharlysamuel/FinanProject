using Finan.BLL;
using Finan.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finan.WEB.Controllers
{
    public class TipoMovtoController : Controller
    {
        // GET: TipoMovto
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartialIndex(string pesquisa = "", string tipo = "")
        {
            var BLL = new Repositorio<tipo_movto>();

            var lista = BLL.GetAll();

            if (!string.IsNullOrEmpty(pesquisa))
                lista = lista.Where(a => a.descricao.Contains(pesquisa)).ToList();

            if (!string.IsNullOrEmpty(tipo))
                lista = lista.Where(a => a.tipo_lancamento == tipo).ToList();

            return PartialView(lista);
        }

        public ActionResult Edit(int id = 0)
        {
            var BLL = new Repositorio<tipo_movto>();
            var obj = BLL.Get(id);

            if (obj == null)
                obj = new tipo_movto();

            return PartialView(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tipo_movto objView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var BLL = new Repositorio<tipo_movto>();

                    var objExiste = BLL.Get(objView.id_tipo_movto);
                    if (objExiste == null)
                    {
                        BLL.Insert(objView);
                    }
                    else
                        BLL.Update(objView);

                    ViewBag.gravado = "true";
                    return PartialView(objView);
                }

                ViewBag.gravado = "false";
                return PartialView(objView);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.gravado = "false";
                return PartialView(objView);
            }

        }


        public JsonResult Delete(int id)
        {
            try
            {
                var BLL = new Repositorio<tipo_movto>();

                BLL.Delete(id);

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", msg = ex.Message });
            }
        }



    }
}