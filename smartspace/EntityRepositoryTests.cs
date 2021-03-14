using System.Linq;
using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace Interview
{
    [TestFixture]
    public class EntityRepositoryTests
    {
        private Mock<IRepository<Entity>> _mockRepository;
        private List<Entity> entities;
        private EntityRepository entityRepositoryUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IRepository<Entity>>();

            entities = new List<Entity>
            {
                new Entity { Name = "Kings of Leon", Id = 1 },
                new Entity { Name = "One Republic", Id = 2 },
                new Entity { Name = "Destinys Child", Id = 3 },

            };
           

            entityRepositoryUnderTest = new EntityRepository(_mockRepository.Object);

        }

        [Test]
        public void GetAll_WhenAllIsOK_ShouldReturnListOfEntities()
        {
            _mockRepository.Setup(x => x.All()).Returns(entities);
            var result = entityRepositoryUnderTest.GetAll();
            Assert.IsInstanceOf<IEnumerable<Entity>>(result);
            Assert.True(result.Any());
        }

        [Test]
        public void GetAll_WWhenRepoReturnsNoResults_ShouldReturnEmptyList()
        {
            _mockRepository.Setup(x => x.All()).Returns(new List<Entity>());
            var result = entityRepositoryUnderTest.GetAll();
            Assert.IsInstanceOf<IEnumerable<Entity>>(result);
            Assert.False(result.Any());
        }

        [Test]

        public void GetAll_WhenRepoThrowsException_ShouldThrowExcpetion()
        {
            _mockRepository.Setup(x => x.All()).Throws(new Exception());
            Assert.Throws<Exception>(() => entityRepositoryUnderTest.GetAll());

        }

        [Test]
        public void GetById_WhenIdIsInRepo_ShouldReturnEntity()
        {
            var id = 1;
            var entity = new Entity { Id = id, Name = "Kings of Leon" };
            _mockRepository.Setup(x => x.FindById(id)).Returns(entity);

            var result = entityRepositoryUnderTest.GetById(id);

            Assert.AreEqual(result.Id, id);
        }

        [Test]
        public void GetById_WhenIdIsNotInRepo_ShouldReturnNull()
        {
            var id = 4;
            _mockRepository.Setup(x => x.FindById(id)).Returns((Entity)null);

            var result = entityRepositoryUnderTest.GetById(id);

            Assert.IsNull(result);
        }

        [Test]
        public void GetById_WhenAnExcpetionHasBeenThrown_ShouldThrowException()
        {
            _mockRepository.Setup(x => x.FindById(1)).Throws(new Exception());
            Assert.Throws<Exception>(() => entityRepositoryUnderTest.GetById(1));

        }

        [Test]
        public void GetById_WhenIdPassedIsZero_ShouldThrowArgumentExcpetion()
        {
            
            Assert.Throws<ArgumentException>(() => entityRepositoryUnderTest.GetById(0));

        }

        [Test]
        public void Remove_WhenAsuccesfulRemoveOccurs_ShouldRemoveItemFromList()
        {
            var id = 1;

            _mockRepository.Setup(x => x.Delete(id));

            entityRepositoryUnderTest.Remove(id);

            _mockRepository.Verify(x => x.Delete(id), Times.Once);

 
        }

        [Test]
        public void Remove_WhenSomethingGoesWrongWhilstDeleting_ShouldThrowException()
        {
            var id = 1;
            _mockRepository.Setup(x => x.Delete(It.IsAny<int>())).Throws(new Exception());

            Assert.Throws<Exception>(() => entityRepositoryUnderTest.Remove(id));


        }

        [Test]
        public void Remove_WhenIdPassedInIsInvalid_ShouldThrowArgumentException()
        {
            
            Assert.Throws<ArgumentException>(() => entityRepositoryUnderTest.Remove(0));


        }

        [Test]
        public void Add_WhenAsuccesfulAddHasOccured_ShouldAddItem()
        {
            var id = 1;
            var entity = new Entity { Id = id, Name = "Kings of Leon" };
            _mockRepository.Setup(x => x.Save(entity));

            entityRepositoryUnderTest.AddOrUpdate(entity);

            _mockRepository.Verify(x => x.Save(entity), Times.Once);


        }

        [Test]
        public void Add_WhenSomethingGoesWrongWhilstAdding_ShouldThrowException()
        {
            var entity = new Entity { Id = 1, Name = "Kings of Leon" };
            _mockRepository.Setup(x => x.Save(It.IsAny<Entity>())).Throws(new Exception());

            Assert.Throws<Exception>(() => entityRepositoryUnderTest.AddOrUpdate(entity));


        }

        [Test]
        public void Add_WhenEntityPassedInIsInvalid_ShouldThrowArgumentException()
        {
       
            Assert.Throws<ArgumentException>(() => entityRepositoryUnderTest.AddOrUpdate((Entity)null));


        }
    }
}