
using System.Web.Http;
using AttributeRouting.Models;
using AttributeRouting.Repositories;
using HyperApi.Controllers;

namespace AttributeRouting.Controllers
{
    [RoutePrefix("person/")]
    public class PersonController : HyperController<Person, PersonRepo, int>
    {
        internal static PersonRepo Persons = new PersonRepo();

        public PersonController() : base(Persons) {}

    }
}