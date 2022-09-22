using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Core.Interfaces
{
    public interface IValidator<T>
    {
        public void ValidteEntity(T entity);
    }
}
