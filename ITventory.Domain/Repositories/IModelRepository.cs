﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Domain.Repositories
{
    public interface IModelRepository
    {
        Task<Model> GetAsync(Guid modelId);
    }
}
