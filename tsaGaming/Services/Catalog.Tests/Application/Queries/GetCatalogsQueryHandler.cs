using Catalog.API.Application.Queries;
using Catalog.Domain.Entites;
using Catalog.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Tests.Application.Queries
{
    [TestFixture]
    public class GetCatalogsQueryHandler_GetAll
    {
        private IList<Catalog.Domain.Entites.Catalog> result;
        private GetCatalogsQueryHandler _getCatalogsQueryHandler;

        [SetUp]
        public void Setup()
        {
            result = new List<Catalog.Domain.Entites.Catalog>
            {
                new Catalog.Domain.Entites.Catalog
                {
                    DisplayName = "DisplayName",
                    ImageUrl = "ImageUrl",
                    Name = "Catalog",
                    Lessons = new List<Lesson>()
                    {
                        new Lesson
                        {
                            Name = "Lesson",
                            Games = new List<Game>()
                            {
                                new Game()
                                {
                                    Name = "Game"
                                }
                            }
                        }
                    }
                }
            };

            var logger = Mock.Of<ILogger<GetCatalogsQueryHandler>>();

            var _catalogRepository = new Mock<ICatalogRepository>();
            _catalogRepository.Setup(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync(result)
                    .Verifiable("Notification was not sent.");

            _getCatalogsQueryHandler = new GetCatalogsQueryHandler(_catalogRepository.Object, logger);
        }

        [Test]
        public void GetCatalogsQueryHandler_GetAll_Return_Value()
        {
            var result = _getCatalogsQueryHandler.Handle(new GetCatalogsQuery(), default(CancellationToken));

            Assert.IsNotNull(result.Result);
        }
    }
}
