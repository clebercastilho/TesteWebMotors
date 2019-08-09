using WebMotors.Test.Domain.Entities;
using WebMotors.Test.Domain.Interfaces.Repositories.Base;
using WebMotors.Test.Domain.Interfaces.UoW;
using WebMotors.Test.Infra.Data.Context;
using WebMotors.Test.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Amil.Atendimentos.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        readonly WebMotorsContext _context;
        private IRepository<Anuncio> _anuncioRepository;

        public UnitOfWork(WebMotorsContext context)
        {
            _context = context;
        }

        public IRepository<Anuncio> Anuncio
        {
            get
            {
                if (_anuncioRepository == null)
                    _anuncioRepository = new AnuncioRepository(_context);

                return _anuncioRepository;
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Disposer(bool isDispose)
        {
            if (!disposed && isDispose)
            {
                _context.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Disposer(true);
            GC.SuppressFinalize(this);
        }
    }
}
