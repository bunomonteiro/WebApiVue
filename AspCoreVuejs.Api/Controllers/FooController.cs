using System.Collections.Generic;
using System.Linq;

using AspCoreVuejs.Api.Model.Entities;
using AspCoreVuejs.Api.Model.Factories;

using Microsoft.AspNetCore.Mvc;

namespace AspCoreVuejs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FooController : ControllerBase
    {
        private static List<Foo> fooList = FooFactory.CreateList();

        // GET: api/Foo>
        [HttpGet]
        public IEnumerable<Foo> Get()
        {
            return fooList;
        }

        // GET api/Foo/5
        [HttpGet("{id}")]
        public Foo Get(int id)
        {
            return fooList.FirstOrDefault(f => f.Id == id);
        }
    }
}
