using Catalog.API.Application.Queries;
using Catalog.API.Controllers;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Ocelot.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Tests.Controller
{
    [TestFixture]
    public class CatalogController_GetAll
    {
        private CatalogController _catalogController;
        private IList<CatalogDTO> result;

        [SetUp]
        public void SetUp()
        {
            result = new List<CatalogDTO>
            {
                new CatalogDTO
                {
                    DisplayName = "DisplayName",
                    ImageUrl = "ImageUrl",
                    Name = "Catalog",
                    Lessons = new List<LessonDTO>()
                    {
                        new LessonDTO
                        {
                            Name = "Lesson",
                            Games = new List<GameDTO>()
                            {
                                new GameDTO()
                                {
                                    Name = "Game"
                                }
                            }
                        }
                    }
                }
            };

            var logger = Mock.Of<ILogger<CatalogController>>();

            var mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(It.IsAny<GetCatalogsQuery>(), default(CancellationToken)))
                    .ReturnsAsync(result)
                    .Verifiable("Notification was not sent.");

            _catalogController = new CatalogController(logger, mediator.Object);
        }

        [Test]
        public void CatalogController_GetAll_Return_Value()
        {
            var result = _catalogController.Get(new GetCatalogsQuery());

            Assert.IsNotNull(result.Result);
        }
    }
}
