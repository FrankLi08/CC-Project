using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheDream.DAL.DAL;
using TheDream.Model.Models;

namespace TheDream.DAL.Repository
{
    public interface ICustoemrUserRepo : IRepository<CustomerUser>
    {
        CustomerUser GetStudentByLastName(string lastName);
    }

    public class CustomerUserRepo : GenericRepository<CustomerUser>, ICustoemrUserRepo, IDisposable
    {
        private bool _disposed = false;
        public CustomerUserRepo(Cloudy_ChefEntitiesContext context) : base(context)
        {

        }



        public CustomerUser GetStudentByLastName(string lastName)
        {
            var user = _context.CustomerUser.FirstOrDefault(s => s.LastName == lastName);
            return user;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }
    }
}
