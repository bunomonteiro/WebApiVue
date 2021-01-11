using System;
using System.Collections.Generic;

using AspCoreVuejs.Api.Model.Entities;

namespace AspCoreVuejs.Api.Model.Factories
{
    public class FooFactory
    {
        public static List<Foo> CreateList(int total = 10)
        {
            var list = new List<Foo>();

            var rand = new Random();

            for(var i = 1; i <= total; i++)
            {
                list.Add(new Foo(i, Guid.NewGuid().ToString(), rand.Next(0, 2) == 1));
            }

            return list;
        }
    }
}
