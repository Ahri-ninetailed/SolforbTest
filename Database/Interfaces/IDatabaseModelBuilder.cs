using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Interfaces
{
    public interface IDatabaseModelBuilder<T>
    {
        public T Build();
    }
}
