using System;
using System.Diagnostics;
using Xunit;
using OpenQA.Selenium.Chrome;

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

                Debugger.Break();

                var searchResultsDiv = chromeDriver.FindElementByCssSelector("div[data-async-rclass=\"search\"]");

                Debugger.Break();

                var images = searchResultsDiv.FindElements(OpenQA.Selenium.By.CssSelector("img"));

                System.Net.WebClient webClient = new System.Net.WebClient();

                int index = 0;

                var baseDir = AppContext.BaseDirectory;

                foreach(var image in images)
                {
                    var src = image.GetAttribute("src");

                    if (src.StartsWith(@"https://"))
                    {
                        while (System.IO.File.Exists($@"{baseDir}\images\{index}.jpg"))
                        {
                            index += 1;
                        }
                        // this is link to image
                        webClient.DownloadFile(src, $@"{baseDir}\images\{index}.jpg");
                    }
                    else if(src.StartsWith(@"data:image/")){
                        // this is base 64 encoded image

                    }

                }

                Debugger.Break();
            }


        }
    }
}
