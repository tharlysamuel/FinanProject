using Finan.BLL;
using Finan.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finan.WEB.Controllers
{
    public class RelatorioController : Controller
    {
        // GET: Relatorio
        public ActionResult Index()
        {
            //Criando as viewbag para carregar os dropdowns de filtro
            var ContaBLL = new ContasBLL();
            var contas = ContaBLL.GetAll().Select(a => new { descricao = a.Cliente.nome + " - " + a.Banco.nome + " - " + a.descricao, id_conta = a.id_conta }).OrderBy(a => a.descricao).ToList();
            ViewBag.contas = new SelectList(contas, "id_conta", "descricao");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string tipo_rel, int id_conta = 0, string data_inicial = "", string data_final = "")
        {

            //Criando as viewbag para carregar os dropdowns de filtro

            var ContaBLL = new ContasBLL();
            var contas = ContaBLL.GetAll().Select(a => new { descricao = a.Cliente.nome + " - " + a.Banco.nome + " - " + a.descricao, id_conta = a.id_conta }).OrderBy(a => a.descricao).ToList();
            ViewBag.contas = new SelectList(contas, "id_conta", "descricao");
            try
            {
                //se for relatorio de movimentações então conta é obrigatório
                if (id_conta == 0 && tipo_rel == "movto_contas")
                    throw new Exception("Conta obrigatória.");


                var ContasBLL = new ContasBLL();
                var listContas = ContasBLL.GetAll();

                //se for relatorio resumo de contas
                if (tipo_rel.Equals("banco_contas"))
                    return View("RelResumidoConta", listContas);
                else if (tipo_rel.Equals("banco_clientes"))
                    return View("RelResumidoCliente", listContas);
                else
                {
                    //se for relatorio de movimentações
                    DateTime dataInicial;
                    if (!DateTime.TryParse(data_inicial, out dataInicial))
                        dataInicial = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

                    DateTime dataFinal;
                    if (!DateTime.TryParse(data_final, out dataFinal))
                        dataFinal = DateTime.Today;

                    var MovtosBLL = new MovtosBLL();
                    var listMovtos = MovtosBLL.GetWhere(id_conta: id_conta, data_inicial: dataInicial, data_final: dataFinal);
                    return View("RelAnaliticoMovto", listMovtos);
                }

            }
            catch (Exception e)
            {
                ViewBag.error =  e.Message;
                return View();
            }

        }
    }
}