﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Interfaces.Builder;

namespace Database.Interfaces
{
    public interface IDatabaseModelBuilder<T> : IBuilder<T>
    {
    }
}
