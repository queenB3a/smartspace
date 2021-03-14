using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview
{
    public class EntityRepository
    {
        private IRepository<Entity> _repository;

        public EntityRepository(IRepository<Entity> repository)
        {
            _repository = repository;
        }

        public Entity GetById(int id)
        {
            if (id == 0)
                throw new ArgumentException($"The value passed in {id} is invalid", "id");
            try
            {
                return _repository.FindById(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ICollection<Entity> GetAll()
        {
            try
            {
               return _repository.All().ToList();
            }
            catch (Exception)
            {

                throw;
            } 
        }

        public void Remove(int id)
        {
            if (id == 0)
                throw new ArgumentException($"The value passed in {id} is invalid", "id");
            try
            {
                _repository.Delete(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AddOrUpdate(Entity entity)
        {
            if (entity == null)
                throw new ArgumentException("The value passed in is invalid", "entity");
            try
            {
                _repository.Save(entity);
            }
            catch (Exception)
            {

                throw;
            } 
        }
    }
}
