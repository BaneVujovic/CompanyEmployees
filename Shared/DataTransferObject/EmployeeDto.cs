﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public record EmployeeDto(Guid Id, string Name, int Age, string Position);
    public record EmployeeForCreationDto(string Name, int Age, string Position);
}