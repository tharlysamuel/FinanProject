using Finan.BLL;
using Finan.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finan.WEB.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult PartialIndex(string pesquisa = "", string cpf = "")
        {
            var BLL = new Repositorio<cliente>();

            var lista = BLL.GetAll();

            if (!string.IsNullOrEmpty(pesquisa))
                lista = lista.Where(a => a.nome.Contains(pesquisa)).ToList();

            if (!string.IsNullOrEmpty(cpf))
                lista = lista.Where(a => a.cpf == cpf).ToList();

            return PartialView(lista);
        }

        public ActionResult Edit(int id = 0)
        {
            var BLL = new Repositorio<cliente>();
            var obj = BLL.Get(id);

            if (obj == null)
                obj = new cliente();

            return PartialView(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(cliente objView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var BLL = new Repositorio<cliente>();

                    var objExiste = BLL.Get(objView.id_cliente);
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
                var BLL = new Repositorio<cliente>();

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