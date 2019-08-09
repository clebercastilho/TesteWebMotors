using WebMotors.Test.Domain.Entities.Base;
using WebMotors.Test.Domain.Interfaces.Repositories.Base;
using WebMotors.Test.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq;

namespace WebMotors.Test.Infra.Data.Repositories.Base
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : Entidade<T>
    {
        readonly protected WebMotorsContext _ctx;
        readonly DbSet<T> _dbEntidade;

        protected RepositoryBase(WebMotorsContext ctx)
        {
            _ctx = ctx;
            _dbEntidade = _ctx.Set<T>();
        }

        public void AdicionarTodos(System.Collections.Generic.IEnumerable<T> list)
        {
            _dbEntidade.AddRange(list);
        }


        public void Incluir(T entidade)
        {
            _dbEntidade.Add(entidade);
        }

        public void Alterar(T entidade)
        {
            _dbEntidade.Update(entidade);
        }

        public void AlterarTodos(System.Collections.Generic.IEnumerable<T> list)
        {
            _dbEntidade.UpdateRange(list);
        }

        public T Buscar(int id)
        {
            return _dbEntidade.Find(id);
        }

        public void Excluir(int id)
        {
            Excluir(Buscar(id));
        }

        public void Excluir(T entidade)
        {
            _dbEntidade.Remove(entidade);
        }

        public IQueryable<T> Listar()
        {
            return _dbEntidade;
        }
    }
}
