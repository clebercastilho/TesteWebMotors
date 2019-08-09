using WebMotors.Test.Domain.Entities;
using WebMotors.Test.Domain.Interfaces.Repositories.Base;
using System.Threading.Tasks;

namespace WebMotors.Test.Domain.Interfaces.UoW
{
    public interface IUnitOfWork
    {
        void Commit();

        IRepository<Anuncio> Anuncio { get; }
    }
}
