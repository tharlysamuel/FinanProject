using Finan.BLL;
using Finan.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finan.WEB.Controllers
{
    public class ContaController : Controller
    {
        // GET: Conta
        public ActionResult Index()
        {
            //Criando as viewbag para carregar os dropdowns de filtros
            var BancoBLL = new Repositorio<banco>();
            var bancos = BancoBLL.GetAll().OrderBy(a => a.nome).ToList();
            ViewBag.bancos = new SelectList(bancos, "id_banco", "nome");

            var ClienteBLL = new Repositorio<cliente>();
            var clientes = ClienteBLL.GetAll().OrderBy(a => a.nome).ToList();
            ViewBag.clientes = new SelectList(clientes, "id_cliente", "nome");

            return View();
        }

        public ActionResult PartialIndex(string pesquisa = "", int id_banco = 0, int id_cliente = 0)
        {
            var BLL = new ContasBLL();

            var lista = BLL.GetAll();

            if (!string.IsNullOrEmpty(pesquisa))
                lista = lista.Where(a => a.descricao.Contains(pesquisa)).ToList();

            if (id_banco > 0)
                lista = lista.Where(a => a.id_banco == id_banco).ToList();

            if (id_cliente > 0)
                lista = lista.Where(a => a.id_cliente == id_cliente).ToList();

            return PartialView(lista);
        }

        public ActionResult Edit(int id = 0)
        {
            var BLL = new ContasBLL();
            var obj = BLL.Get(id);

            if (obj == null)
                obj = new conta();

            //Criando as viewbag para carregar os dropdowns

            var BancoBLL = new Repositorio<banco>();
            var bancos = BancoBLL.GetAll().OrderBy(a => a.nome).ToList();
            ViewBag.bancos = new SelectList(bancos, "id_banco", "nome");

            var ClienteBLL = new Repositorio<cliente>();
            var clientes = ClienteBLL.GetAll().OrderBy(a => a.nome).ToList();
            ViewBag.clientes = new SelectList(clientes, "id_cliente", "nome");

            return PartialView(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(conta objView)
        {
            //Criando as viewbag para carregar os dropdowns

            var BancoBLL = new Repositorio<banco>();
            var bancos = BancoBLL.GetAll().OrderBy(a => a.nome).ToList();
            ViewBag.bancos = new SelectList(bancos, "id_banco", "nome");

            var ClienteBLL = new Repositorio<cliente>();
            var clientes = ClienteBLL.GetAll().OrderBy(a => a.nome).ToList();
            ViewBag.clientes = new SelectList(clientes, "id_cliente", "nome");

            try
            {
                if(objView.id_cliente == 0)
                    ModelState.AddModelError("id_cliente", "Cliente é obrigatório.");

                if (objView.id_banco == 0)
                    ModelState.AddModelError("id_banco", "Banco é obrigatório.");

                if (ModelState.IsValid)
                {
                    var BLL = new ContasBLL();

                    var objExiste = BLL.Get(objView.id_conta);
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
                var BLL = new ContasBLL();

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