namespace TurbolinksBenchmark.Results
{
    using System;
    using System.Diagnostics;

    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.Internal;

    class Program
    {
        static void Main(string[] args)
        {
            var times = args.Length > 0 ? int.Parse(args[0]) : 50;

            var disabled = Run(times, false);
            var enabled = Run(times, true);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Results for " + times + " clicks:");
            Console.WriteLine("====================================");
            Console.WriteLine();
            Console.WriteLine("Without Turbolinks: " + disabled);
            Console.WriteLine("   With Turbolinks: " + enabled);
            Console.WriteLine("        Difference: " + (disabled - enabled));
            Console.Read();
        }

        static TimeSpan Run(int times, bool enable)
        {
            var url = "http://localhost:52374/Home/Index";

            if (!enable)
            {
                url += "?enableTurbolinks=false";
            }

            Action<IFindsByLinkText> click = c => c.FindElementByLinkText("next").Click();

            using (var browser = new InternetExplorerDriver(

                new InternetExplorerOptions
                {
                    IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                    IgnoreZoomLevel = true,
                    EnsureCleanSession = true
                }))
            {

                browser.Navigate().GoToUrl(url);

                // Warm up
                click(browser);

                var watch = new Stopwatch();

                watch.Start();

                for (var i = 0; i < times; i++)
                {
                    click(browser);
                }

                watch.Stop();

                return watch.Elapsed;
            }
        }
    }
}