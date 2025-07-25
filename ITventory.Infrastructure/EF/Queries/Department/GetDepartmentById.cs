﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Shared.Abstractions.Queries;

namespace ITventory.Infrastructure.EF.Queries.Department
{
    public class GetDepartmentById: IQuery<DepartmentDTO>
    {
        public Guid Id { get; set; }
    }
}
