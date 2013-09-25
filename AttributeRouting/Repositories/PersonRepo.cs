using System.Collections.Generic;
using System.Linq;
using AttributeRouting.Models;

namespace AttributeRouting.Repositories
{
    public class PersonRepo : Repository<Person, int>
    {
        private IList<Person> persons;

        public PersonRepo()
        {
            if (persons == null)
            {
                persons = new List<Person>();
            }
        }
        public override ICollection<Person> Entities
        {
            get
            {
                return persons;
            }
            set
            {
                persons = value.ToList();
            }
        }

    }
}
