using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview
{
    public class Repository<T> : IRepository<T> where T : IStoreable
    {
        private readonly IList<T> Entities;     

        public Repository(IList<T> entities)
        {
            Entities = entities;
            
        }


        public IEnumerable<T> All()
        {
            return Entities;
        }

        public void Delete(IComparable id)
        {
            var entity = FindById(id);
            if (entity != null)
                Entities.Remove(entity);
        }

        public T FindById(IComparable id)
        {
            return Entities.FirstOrDefault(x => x.Id.Equals(id));
        }

        public void Save(T item)
        {
            //lets make sure we are not adding duplicates. Update instead when entity exists
            var entity = FindById(item.Id);
            if(entity == null)
                Entities.Add(item);
            else
            {
                entity = item;
                //ideal world, we save our entities eg. context
            }
        }
    }
}
