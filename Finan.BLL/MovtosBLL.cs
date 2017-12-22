using Finan.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Finan.BLL
{
    public class MovtosBLL
    {
        public MovtosBLL()
        {
        }

        /// <summary>
        /// Adicionar objeto conta_movto
        /// </summary>
        /// <param name="item"></param>
        public virtual void Insert(conta_movto item)
        {
            try
            {
                Contexto bd = new Contexto();
                bd.Movimentacoes.Add(item);
                bd.SaveChanges();
            }
            catch (Exception erro)
            {
                throw new Exception("Falha ao gravar:" + erro.Message);
            }

        }

        /// <summary>
        /// Alterar objeto conta_movto
        /// </summary>
        /// <param name="item"></param>
        public virtual void Update(conta_movto item)
        {
            try
            {
                Contexto bd = new Contexto();
                bd.Movimentacoes.Attach(item);
                bd.SaveChanges();
            }
            
            catch (Exception erro)
            {
                throw new Exception("Falha ao gravar:" + erro.Message);
            }
        }

        /// <summary>
        /// Excluir objeto conta_movto
        /// </summary>
        /// <param name="item"></param>
        public virtual void Delete(conta_movto item)
        {
            try
            {
                Contexto bd = new Contexto();
                int mvtos = bd.Movimentacoes.Count(a => a.id_movto == item.id_movto);
                if (mvtos > 0)
                    throw new Exception("Esta conta_movto contém movimentações.");
                bd.Movimentacoes.Remove(item);
                bd.SaveChanges();
            }
            catch (Exception erro)
            {
                throw new Exception("Falha ao excluir:" + erro.Message);
            }
        }


        /// <summary>
        /// Exclui objeto conta_movto por id
        /// </summary>
        /// <param name="id">conta_movto.id_movto</param>
        public virtual void Delete(int id)
        {
            try
            {
                Contexto bd = new Contexto();
                var item = bd.Movimentacoes.Find(id);
                bd.Movimentacoes.Remove(item);
                bd.SaveChanges();
            }
            catch (Exception erro)
            {
                throw new Exception("Falha ao excluir:" + erro.Message);
            }
        }

        /// <summary>
        /// Seleciona um objeto conta_movto
        /// </summary>
        /// <param name="id_movto"></param>
        /// <returns>Objeto conta_movto</returns>
        public conta_movto Get(int id_movto)
        {
            Contexto bd = new Contexto();
            return bd.Movimentacoes.Find(id_movto);
        }

        /// <summary>
        /// Seleciona coleção de objetos conta_movto
        /// </summary>
        /// <param name="predicate">Filtros com Lambda</param>
        /// <returns>Lista de objeto conta_movto</returns>
        public ICollection<conta_movto> GetWhere(Func<conta_movto, bool> predicate)
        {
            Contexto bd = new Contexto();
            return bd.Movimentacoes.Where(predicate).ToList();
        }

        /// <summary>
        /// Seleciona coleção de objetos conta_mvto com vários paramêtros
        /// </summary>
        /// <param name="id_conta"></param>
        /// <param name="id_tipo_movto"></param>
        /// <param name="id_banco"></param>
        /// <param name="id_cliente"></param>
        /// <param name="data_inicial"></param>
        /// <param name="data_final"></param>
        /// <returns>Lista de objeto conta_mvto</returns>
        public List<conta_movto> GetWhere(int? id_conta = 0, int id_tipo_movto = 0, int id_banco = 0, int id_cliente =0,  DateTime? data_inicial = null, DateTime? data_final = null)
        {
            Contexto bd = new Contexto();
            var list = bd.Movimentacoes.Include("Conta").AsEnumerable();

            if (id_conta > 0)
                list = list.Where(a => a.id_conta == id_conta);

            if (id_tipo_movto > 0)
                list = list.Where(a => a.id_tipo_movto == id_tipo_movto);

            if (id_cliente > 0)
                list = list.Where(a => a.Conta.id_cliente == id_cliente);

            if (id_banco > 0)
                list = list.Where(a => a.Conta.id_banco == id_banco);

            if (data_inicial != null)
                list = list.Where(a => a.data.Date >= data_inicial);

            if (data_final != null)
                list = list.Where(a => a.data.Date <= data_final);


            list = list.OrderByDescending(a => a.data).ThenByDescending(a => a.id_movto);

            return list.ToList();
        }

        /// <summary>
        /// Seleciona todos os objeto conta_movto
        /// </summary>
        /// <returns>Lista com todos objetos conta_movto</returns>
        public ICollection<conta_movto> GetAll()
        {
            try
            {
                Contexto bd = new Contexto();
                return (bd.Movimentacoes.ToList());
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }

        public void Dispose()
        {

        }


    }



}
