
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ASC.Test.TestUtilities;
using Automobile_Service_Center_Applications.Web.Configuration;
using ASC.Web.Controllers;
using ASC.Utilities.DataAccess;

namespace ASC.Test
{
    public class HomeControllerTests
    {

        private readonly Mock<IOptions<ApplicationSettings>> optionsMock;
        private readonly Mock<HttpContext> mockHttpContext;
        public  HomeControllerTests()
        {
            optionsMock = new Mock<IOptions<ApplicationSettings>>();


            mockHttpContext = new Mock<HttpContext>();

            mockHttpContext.Setup(p => p.Session).Returns(new FakeSession());


            optionsMock.Setup(ap => ap.Value).Returns(new ApplicationSettings
            {
                ApplicationTitle = "ASC"
            }) ;
        }

        [Fact]
        public void HomeController_Index_Sesion_Test()
        {
            var controller = new HomeController(optionsMock.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;
            controller.Index();

            Assert.NotNull(controller.HttpContext.Session.GetSession<ApplicationSettings>("Test"));
        }
        [Fact]
        public void HomeController_Index_View_Test()
        {
            var controller = new HomeController(optionsMock.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            Assert.IsType(typeof(ViewResult), controller.Index());
        }
        [Fact]
        public void HomeController_Index_NoModel_Test()
        {
            var controller = new HomeController(optionsMock.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            Assert.Null((controller.Index() as ViewResult).Model);

        }
        [Fact]
        public void HomeController_Index_Validation_Test()
        {
            var controller = new HomeController(optionsMock.Object);


            Assert.Equal(0, (controller.Index() as ViewResult).ViewData.ModelState.ErrorCount);
        }

    }
}
