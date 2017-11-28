using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Entities.Test
{
    public class Util
    {
        internal static DbContextOptions<YabaDBContext> GetInMemoryDatabase()
        {
            return new DbContextOptionsBuilder<YabaDBContext>()
                .UseInMemoryDatabase("in_memory_test_database")
                .Options;
        }

        internal static IYabaDBContext GetNewContext()
        {
            var ctx = new YabaDBContext(GetInMemoryDatabase());
            ctx.Database.EnsureDeleted();
            return ctx;
        }
    }
}
