using Finan.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Finan.BLL
{
    public class ContasBLL
    {
        public ContasBLL()
        {
        }

        /// <summary>
        /// Adicionar objeto conta
        /// </summary>
        /// <param name="item"></param>
        public virtual void Insert(conta item)
        {
            try
            {
                Contexto bd = new Contexto();
                bd.Contas.Add(item);//adiciona o objeto ao contexto
                bd.SaveChanges();   //salva as mudanças do contexto no banco de dados
            }
            catch (Exception erro)
            {
                throw new Exception("Falha ao gravar:" + erro.Message);
            }

        }

        /// <summary>
        /// Alterar objeto conta
        /// </summary>
        /// <param name="item"></param>
        public virtual void Update(conta item)
        {
            try
            {
                Contexto bd = new Contexto();
                bd.Contas.Attach(item);//altera o objeto ao contexto
                bd.SaveChanges();      //salva as mudanças do contexto no banco de dados
            }
            
            catch (Exception erro)
            {
                throw new Exception("Falha ao gravar:" + erro.Message);
            }
        }

        /// <summary>
        /// Excluir objeto conta
        /// </summary>
        /// <param name="item"></param>
        public virtual void Delete(conta item)
        {
            try
            {
                Contexto bd = new Contexto();
                int mvtos = bd.Movimentacoes.Count(a => a.id_conta == item.id_conta);
                if (mvtos > 0)
                    throw new Exception("Esta conta contém movimentações.");
                bd.Contas.Remove(item);
                bd.SaveChanges();
            }
            catch (Exception erro)
            {
                throw new Exception("Falha ao excluir:" + erro.Message);
            }
        }

        /// <summary>
        /// Exclui objeto conta por id
        /// </summary>
        /// <param name="id">conta.id_conta</param>
        public virtual void Delete(int id)
        {
            try
            {
                Contexto bd = new Contexto();
                var item = bd.Contas.Find(id);
                int mvtos = bd.Movimentacoes.Count(a => a.id_conta == item.id_conta);
                if (mvtos > 0)
                    throw new Exception("Esta conta contém movimentações.");
                bd.Contas.Remove(item);
                bd.SaveChanges();
            }
            catch (Exception erro)
            {
                throw new Exception("Falha ao excluir:" + erro.Message);
            }
        }

        /// <summary>
        /// Seleciona um objeto conta
        /// </summary>
        /// <param name="id_conta"></param>
        /// <returns>Objeto conta</returns>
        public conta Get(int id_conta)
        {
            Contexto bd = new Contexto();
            return bd.Contas.Find(id_conta);
        }

        /// <summary>
        /// Seleciona coleção de objetos conta
        /// </summary>
        /// <param name="predicate">Filtros com Lambda</param>
        /// <returns>Lista de objeto conta</returns>
        public ICollection<conta> GetWhere(Func<conta, bool> predicate)
        {
            Contexto bd = new Contexto();
            return bd.Contas.Where(predicate).ToList();
        }


        /// <summary>
        /// Seleciona todos os objeto conta
        /// </summary>
        /// <returns>Lista com todos objetos conta</returns>
        public ICollection<conta> GetAll()
        {
            try
            {
                Contexto bd = new Contexto();
                return (bd.Contas.ToList());
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
