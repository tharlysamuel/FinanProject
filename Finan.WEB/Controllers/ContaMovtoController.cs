using Finan.BLL;
using Finan.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finan.WEB.Controllers
{
    public class ContaMovtoController : Controller
    {
        // GET: ContaMovto
        public ActionResult Index()
        {
            //Criando as viewbag para carregar os dropdowns de filtros

            var ContaBLL = new ContasBLL();
            var contas = ContaBLL.GetAll().Select(a => new { descricao = a.Cliente.nome + " - " + a.Banco.nome + " - " + a.descricao, id_conta = a.id_conta }).OrderBy(a => a.descricao).ToList();
            ViewBag.contas = new SelectList(contas, "id_conta", "descricao");

            var TipoBLL = new Repositorio<tipo_movto>();
            var tipos = TipoBLL.GetAll();
            ViewBag.tipos = new SelectList(tipos, "id_tipo_movto", "descricao");

            var BancoBLL = new Repositorio<banco>();
            var bancos = BancoBLL.GetAll();
            ViewBag.bancos = new SelectList(bancos, "id_banco", "nome");

            var ClienteBLL = new Repositorio<cliente>();
            var clientes = ClienteBLL.GetAll();
            ViewBag.clientes = new SelectList(clientes, "id_cliente", "nome");

            return View();
        }

        public ActionResult PartialIndex(int id_conta = 0, int id_tipo = 0, int id_banco =0, int id_cliente =0, string data_inicial = "", string data_final = "")
        {
            DateTime dataInicial;
            if(!DateTime.TryParse(data_inicial,out dataInicial))
                dataInicial = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            DateTime dataFinal;
            if(!DateTime.TryParse(data_final, out dataFinal))
                dataFinal = DateTime.Today;


            var BLL = new MovtosBLL();
            var list = BLL.GetWhere(id_conta: id_conta, id_tipo_movto: id_tipo, id_banco:id_banco, id_cliente:id_cliente, data_inicial: dataInicial, data_final:dataFinal);

            return PartialView(list);
        }

        public ActionResult Edit(int id = 0)
        {
            var BLL = new MovtosBLL();
            var obj = BLL.Get(id);

            if (obj == null)
                obj = new conta_movto();

            //Criando as viewbag para carregar os dropdowns

            var ContaBLL = new Repositorio<conta>();
            var contas = ContaBLL.GetAll().Select(a => new { descricao = a.Cliente.nome + " - " + a.Banco.nome + " - " + a.descricao, id_conta = a.id_conta }).OrderBy(a => a.descricao).ToList();
            ViewBag.contas = new SelectList(contas, "id_conta", "descricao");

            var TipoBLL = new Repositorio<tipo_movto>();
            var tipos = TipoBLL.GetAll().OrderBy(a => a.descricao).ToList();
            ViewBag.tipos = new SelectList(tipos, "id_tipo_movto", "descricao");

            return PartialView(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(conta_movto objView)
        {
            //Criando as viewbag para carregar os dropdowns
            var ContaBLL = new Repositorio<conta>();
            var contas = ContaBLL.GetAll().Select(a => new { descricao = a.Cliente.nome + " - " + a.Banco.nome + " - " + a.descricao, id_conta = a.id_conta }).OrderBy(a => a.descricao).ToList();
            ViewBag.contas = new SelectList(contas, "id_conta", "descricao");

            var TipoBLL = new Repositorio<tipo_movto>();
            var tipos = TipoBLL.GetAll().OrderBy(a => a.descricao).ToList();
            ViewBag.tipos = new SelectList(tipos, "id_tipo_movto", "descricao");

            try
            {
                if (objView.valor <= 0)
                    ModelState.AddModelError("valor", "Valor não pode ser menor ou igual a 0.");

                if (objView.id_tipo_movto <= 0)
                    ModelState.AddModelError("id_tipo_movto", "Tipo é obrigatório.");

                if (ModelState.IsValid)
                {
                    var tipo_movto = TipoBLL.Get(objView.id_tipo_movto);
                    objView.valor = tipo_movto.tipo_lancamento == "D" ? -1 * objView.valor : objView.valor;

                    var BLL = new MovtosBLL();
                    var objExiste = BLL.Get(objView.id_movto);
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
                var BLL = new MovtosBLL();

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