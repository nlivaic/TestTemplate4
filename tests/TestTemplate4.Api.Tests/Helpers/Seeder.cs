﻿using System.Collections.Generic;
using TestTemplate4.Core.Entities;
using TestTemplate4.Data;

namespace TestTemplate4.Api.Tests.Helpers
{
    public static class Seeder
    {
        public static void Seed(this TestTemplate4DbContext ctx)
        {
            ctx.Foos.AddRange(
                new List<Foo>
                {
                    new ("Text 1"),
                    new ("Text 2"),
                    new ("Text 3")
                });
            ctx.SaveChanges();
        }
    }
}
