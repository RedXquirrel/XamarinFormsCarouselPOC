using System;
using System.Diagnostics;
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

            app.SwipeRightToLeft();
            app.Screenshot("View 2");
        }

        [Test]
        public void AppLaunches_SwipeLeft_To_ThirdView()
        {

            app.SwipeRightToLeft();
            app.SwipeRightToLeft();
            app.Screenshot("View 3");
        }

        [Test]
        public void AppLaunches_SwipeLeft_To_FourthView()
        {

            app.SwipeRightToLeft();
            app.SwipeRightToLeft();
            app.SwipeRightToLeft();
            app.Screenshot("View 4");
        }

        [Test]
        public void AppLaunches_Type_Text()
        {
            app.Tap(x => x.Class("EntryEditText"));
            app.EnterText(x => x.Class("EntryEditText"), "Anthony Harrison");
            var a = app.Query(e => e.All().Property("Text", "Anthony Harrison")).ToList();
            foreach (var appResult in a)
            {
                Assert.IsTrue(appResult.Text.Equals("Anthony Harrison"));
            }

            app.Screenshot("View 4");
        }

        [Test]
        public void AppLaunches_Type_Text_Clear_Text()
        {
            app.Tap(x => x.Class("EntryEditText"));
            app.EnterText(x => x.Class("EntryEditText"), "Anthony Harrison");
            var a = app.Query(e => e.All().Property("Text", "Anthony Harrison")).ToList();
            foreach (var appResult in a)
            {
                Assert.IsTrue(appResult.Text.Equals("Anthony Harrison"));
            }
            app.ClearText(x => x.Text("Anthony Harrison"));
            app.Screenshot("View 5");
        }

        [Test]
        public void AppLaunches_By_Marked()
        {
            app.Tap(x => x.Marked("InputEntry"));
            app.Screenshot("Tapped on view EntryEditText");
            app.EnterText(x => x.Marked("InputEntry"), "Captain Xamtastic!");
            app.Screenshot("Entered 'Captain Xamtastic!' into view EntryEditText");
            app.PressEnter();
            app.WaitForElement(c => c.Marked("InputLabel").Text("Captain Xamtastic!"));
            app.Tap(x => x.Marked("ClearButton"));

            app.Tap(x => x.Marked("InputEntry"));
            app.EnterText(x => x.Marked("InputEntry"), "Anthony Harrison");

            var inputEntry = app.Query(x => x.Marked("InputEntry")).ToList().FirstOrDefault();

            if (inputEntry != null)
            {
                Assert.IsTrue(inputEntry.Text.Equals("Anthony Harrison"), "Text dos not equal Anthony Harrison");
            }
            else
            {
                Assert.IsTrue(false, "InputEntry is null.");
            }

            app.Tap(x => x.Marked("ClearButton"));

            var inputEntry1 = app.Query(x => x.Marked("InputEntry")).ToList().FirstOrDefault();

            if (inputEntry1 != null)
            {
                Assert.IsTrue(inputEntry1.Text.Equals(string.Empty), "Text dos not equal ''");
            }
            else
            {
                Assert.IsTrue(false, "InputEntry1 is null.");
            }

            app.SwipeRightToLeft(swipeSpeed: 5000);

            var titleLabel = app.Query(x => x.Marked("TitleLabel")).ToList().FirstOrDefault();

            if (titleLabel != null)
            {
                Assert.IsTrue(titleLabel.Text.Equals("Two"),
                    $"TitleLabel.Text expected as 'Two' but is {titleLabel.Text}");
            }
            else
            {
                Assert.IsTrue(false, "TitleLabel is null.");
            }

            app.SwipeRightToLeft(swipeSpeed: 5000);

            var titleLabel1 = app.Query(x => x.Marked("TitleLabel")).ToList().FirstOrDefault();

            if (titleLabel1 != null)
            {
                Assert.IsTrue(titleLabel1.Text.Equals("Three"),
                    $"TitleLabel1.Text expected as 'Three' but is {titleLabel1.Text}");
            }
            else
            {
                Assert.IsTrue(false, "TitleLabel1 is null.");
            }

        }

    }
}

