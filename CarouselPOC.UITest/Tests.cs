using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace CarouselPOC.UITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AppLaunches_FirstView()
        {
            app.Screenshot("View 1");
        }

        [Test]
        public void AppLaunches_SwipeLeft_To_SecondView()
        {

            app.SwipeLeft();
            app.Screenshot("View 2");
        }

        [Test]
        public void AppLaunches_SwipeLeft_To_ThirdView()
        {

            app.SwipeLeft();
            app.SwipeLeft();
            app.Screenshot("View 3");
        }

        [Test]
        public void AppLaunches_SwipeLeft_To_FourthView()
        {

            app.SwipeLeft();
            app.SwipeLeft();
            app.SwipeLeft();
            app.Screenshot("View 4");
        }


    }
}

