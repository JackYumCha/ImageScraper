using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using OpenQA.Selenium;
using Xunit;
using OpenQA.Selenium.Chrome;
using System.Net;
using System.Threading.Tasks;

namespace ImageScraper
{
    public class UnitTest1
    {
        [Fact(DisplayName = "Open Chrome and Search Image Manually")]
        public void OpenChromeAndSearchImage()
        {

            using (ChromeDriver chromeDriver = new ChromeDriver())
            {


                chromeDriver.Url = "https://image.google.com";

                chromeDriver.Navigate();

                Debugger.Break(); // you need to type "dog" in the search box

                // make the page show as many dog images as it allows

                var searchResultsDiv = chromeDriver.FindElementByCssSelector("div[data-async-rclass=\"search\"]");


                var images = searchResultsDiv.FindElements(By.CssSelector("img"));

                WebClient webClient = new WebClient();

                int index = 0;

                var baseDir = AppContext.BaseDirectory;

                //var srcs = images
                //    .Select(img => img.GetAttribute("src"))
                //    .Where(src => src != null)
                //    .ToList();

                //Parallel.ForEach(srcs, new ParallelOptions()
                //{
                //    MaxDegreeOfParallelism = 10
                //},
                //(src) =>
                //{
                //    if (src.StartsWith(@"https://"))
                //    {
                //        while (File.Exists($@"{baseDir}\images\{index}.jpg"))
                //        {
                //            index += 1;
                //        }
                //        // this is link to image
                //        webClient.DownloadFile(src, $@"{baseDir}\images\{index}.jpg");
                //    }
                //    else if (src.StartsWith(@"data:image/"))
                //    {
                //        // this is base 64 encoded image

                //    }
                //});

                foreach (var image in images)
                {
                    var src = image.GetAttribute("src");
                    if (src == null)
                        continue;
                    if (src.StartsWith(@"https://"))
                    {
                        while (File.Exists($@"{baseDir}\images\{index}.jpg"))
                        {
                            index += 1;
                        }
                        // this is link to image
                        webClient.DownloadFile(src, $@"{baseDir}\images\{index}.jpg");
                    }
                    else if (src.StartsWith(@"data:image/"))
                    {
                        // this is base 64 encoded image

                    }

                }

                Debugger.Break();
            }


        }
    }
}
