﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using MvcTemplate.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvcTemplate.Data.Logging
{
    public interface IAuditLogger : IDisposable
    {
        void Log(IEnumerable<EntityEntry<BaseModel>> entries);
        void Save();
        Task SaveAsync();
    }
}
