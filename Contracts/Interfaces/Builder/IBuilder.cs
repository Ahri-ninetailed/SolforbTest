using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces.Builder
{
    public interface IBuilder<T>
    {
        /// <summary>
        /// Создает экземпляр целевого обьекта <see cref="T"/> из обьекта конструктора
        /// </summary>
        /// <returns></returns>
        public T Build();
    }
}
