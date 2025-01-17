﻿using Microsoft.EntityFrameworkCore;
using MvcTemplate.Data.Core;
using System;

namespace MvcTemplate.Tests
{
    public class TestingContext : Context
    {
        protected DbSet<TestModel> TestModel { get; set; }

        private String DatabaseName { get; }

        public TestingContext()
        {
            DatabaseName = Guid.NewGuid().ToString();
        }
        public TestingContext(TestingContext context)
        {
            DatabaseName = context.DatabaseName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseInMemoryDatabase(DatabaseName);
            builder.UseLazyLoadingProxies();
        }
    }
}
