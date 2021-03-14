using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview
{
    /// <summary>
    /// one would argue testing this class
    /// </summary>
    [TestFixture]
    public class Tests
    {
        private List<Entity> _entities;
        private Entity _entity;
        private int _count;
        private Repository<Entity> repository;

        [SetUp]
        public void Setup()
        {
            _entity = new Entity { Name = "Destinys Child", Id = 3 };
            _entities = new List<Entity>
            {
                new Entity { Name = "Kings of Leon", Id = 1 },
                new Entity { Name = "One Republic", Id = 2 },
                _entity

            };
            _count = _entities.Count;
            repository = new Repository<Entity>(_entities);
        }

        [Test]
        public void Save_WhenANewEntityGetsAddedd_ShouldAddToList()
        {
            var entity = new Entity { Name = "BlackEyedPeas", Id = 4 };
            repository.Save(entity);

            Assert.Contains(entity, _entities);
            Assert.True(_entities.Count == _count + 1);
        }

        [Test]
        public void Save_WhenAnEntityGetsUpdate_ShouldUpdateListAndCountRemainTheSame()
        {
            _entity.Name = "SWV";
            repository.Save(_entity);

            Assert.True( _entities.Any(e => e.Name == _entity.Name));
            Assert.True(_entities.Count == _count);
        }

        [Test]
        public void Delete_WhenRecordIsNotInList_CountShouldStayTheSame()
        {
            repository.Delete(4);

            Assert.IsTrue(_entities.Count == _count);
        }

        [Test]
        public void Delete_WhenRecordIsInList_CountShouldBeTheLessThan1()
        {
            repository.Delete(_entity.Id);

            Assert.IsTrue(_entities.Count == _count-1);
        }

        [Test]
        public void FIndById_WhenRecordIsNotInContext_ShouldReturnNull()
        {
            var result = repository.FindById(4);

            Assert.IsNull(result);
        }
        
        [Test]
        public void FIndById_WhenRecordIsInContext_ShouldReturnRecord()
        {
            var result = repository.FindById(_entity.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(_entity, result);
        }

        [Test]
        public void All_WHenThereAreNoRecordsInContext_ShouldReturnEmpty()
        {
            repository = new Repository<Entity>(new List<Entity>());
            var result = repository.All();

            Assert.IsEmpty(result);
            
        }

        [Test]
        public void All_WHenThereAreRecordsInContext_ShouldReturnList()
        {
            var result = repository.All();

            Assert.AreEqual(result.Count(), _entities.Count);

        }
    }
}
