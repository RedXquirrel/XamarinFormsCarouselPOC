using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace CarouselPOC.UITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .EnableLocalScreenshots()
                    .ApiKey("a7671b43b03e0bd2a1ccb7b80aec2fae")
                    .StartApp();
            }

            return ConfigureApp
                .Android
                .EnableLocalScreenshots()
                .ApiKey("a7671b43b03e0bd2a1ccb7b80aec2fae")
                .StartApp();

            //return ConfigureApp
            //    .iOS
            //    .StartApp();
        }
    }
}

