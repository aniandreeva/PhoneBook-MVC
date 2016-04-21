using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhoneBook.Repositories
{
    public class UnitOfWork : IDisposable
    {
        public AppContext Context { get; set; }

        private DbContextTransaction trans = null;

        public UnitOfWork()
        {
            Context = new AppContext();
            this.trans = Context.Database.BeginTransaction();
        }

        public void Commit()
        {
            if (this.trans != null)
            {
                this.trans.Commit();
                this.trans = null;
            }
        }

        public void RollBack()
        {
            if (this.trans != null)
            {
                this.trans.Rollback();
                this.trans = null;
            }
        }

        public void Dispose()
        {
            Commit();
            Context.Dispose();
        }
    }
}