using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttributeRouting.Models;
using AttributeRouting.Repositories;

namespace AttributeRouting.Controllers
{
    [RoutePrefix("person/")]
    public class PersonController : HyperController<Person, PersonRepo, int>
    {
        internal static PersonRepo Persons = new PersonRepo();

        public PersonController() : base(Persons) {}

    }
}