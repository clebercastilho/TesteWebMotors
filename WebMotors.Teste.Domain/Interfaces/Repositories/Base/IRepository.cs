using WebMotors.Test.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebMotors.Test.Domain.Interfaces.Repositories.Base
{
    public interface IRepository<T> where T : Entidade<T>
    {
        void Incluir(T entidade);
        void Alterar(T entidade);
        T Buscar(int id);
        IQueryable<T> Listar();
        void Excluir(int id);
        void Excluir(T entidade);
    }
}
