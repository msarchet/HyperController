using AttributeRouting.Models;
using AttributeRouting.Repositories;

namespace AttributeRouting.Controllers
{
    public class PersonController : HyperController<Person, PersonRepo, int>
    {
        internal static PersonRepo Persons = new PersonRepo();

        public PersonController() : base(Persons) {}
    }
}