using LinkGroup.DemoTests.Helpers;
using LinkGroup.DemoTests.HooksFile;
using Microsoft.VisualBasic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace LinkGroup.DemoTests.ReusableFile
{
   public class ReusableMethods
    {
        TestData inputDataFile = TestData.getInstance();

        public static IWebDriver driver = null;
        public static string BrowserdriverPath = ConfigurationManager.AppSettings["BrowserdriverPath"];
      

        public static WebDriverWait wait;
     

        public void LaunchBrowser()
        {
            BrowserUtility _webDriver = new BrowserUtility();
            driver = _webDriver.Current;
        }

        public string BasePath
        {
            get
            {
                var basePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                return basePath;
            }
        }
        public string GetElementText(IWebElement element)
        {
            try { return element.Text; }
            catch (Exception e)
            {
                LogFile.LogInformation(e.ToString());
                CaptureScreenshot();
                SetupFile._status = "Fail";
                SetupFile._statusMessage = e.ToString();
                throw new Exception(e.ToString());
            }

        }
        public void Closedriver()
        {
           driver.Quit();
        }
        public void launchUrl(string url)
        {
            
           driver.Navigate().GoToUrl(url);
        }
        public void ScrollToTopOfPage()
        {
            try
            {
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                executor.ExecuteScript("scroll(0, -250);");
            }
            catch (Exception e)
            {
                CaptureScreenshot();
                SetupFile._status = "Fail";
                SetupFile._statusMessage = e.ToString();
                throw new Exception(e.ToString());
            }
        }
        public void CaptureScreenshot()
        {
            try
            { 
            ScrollToTopOfPage();
            string basePath = BasePath;
            string path = basePath + @"\Screenshots\output";
           
            var filename = string.Empty + DateTime.Now.ToString("ddmmyyyyHHmmss");
            filename = path + @"\" + filename;
          
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(@filename, ScreenshotImageFormat.Jpeg);
               
                Console.WriteLine(@"Screenshot: {0}", new Uri(filename));
            }
            catch (Exception e)
            {
            e.ToString();
            }
        }
        public bool IsElementDisplayed(IWebElement ele)
        {
            bool actres;
            try
            {

                actres = ele.Displayed;
            }
            catch (Exception)
            {
                return false;
            }
            return actres;
        }
        public string GetUrl
        {
            get 
            {
                return driver.Url; 
            }
        }
        public void WaitForPageToLoadComplete(int min)
        {
            
            try
            {
                IWait<IWebDriver> wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMinutes(min));
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                executor.ExecuteScript("return document.readyState").Equals("complete");
            }
            catch (Exception)
            {
                try
                {
                    IWait<IWebDriver> wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMinutes(min));
                    IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                    executor.ExecuteScript("return document.readyState").Equals("complete");
                }
                catch (Exception)
                {
                    try
                    {
                        IWait<IWebDriver> wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMinutes(min));
                        IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                        executor.ExecuteScript("return document.readyState").Equals("complete");
                    }
                    catch (Exception)
                    {
                        try
                        {
                            IWait<IWebDriver> wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMinutes(min));
                            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                            executor.ExecuteScript("return document.readyState").Equals("complete");
                        }
                        catch (Exception e)
                        {
                            LogFile.LogInformation(e.ToString());
                            CaptureScreenshot();
                            SetupFile._status = "Fail";
                            SetupFile._statusMessage = e.ToString();
                            throw new Exception(e.ToString());
                        }
                    }
                }
            }
        }
        public void ExplicitWait(int sec)
        {
            IWait<IWebDriver> wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(sec));
        }

        public string GetScreenShotPath()
        {
            return ConfigurationManager.AppSettings["Browser"];
        }

        public string GetEnvironment()
        {
            return ConfigurationManager.AppSettings["Environment"];
        }
        public void ClickElement(IWebElement ele)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            Thread.Sleep(1000);
            executor.ExecuteScript("arguments[0].scrollIntoView(true);", new object[] { ele });
            ele.Click();
        }
        public void MouseHover(IWebElement ele)
        {
            try
            {
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                Thread.Sleep(1000);
                executor.ExecuteScript("arguments[0].scrollIntoView(true);", ele);
                var action = new Actions(driver);
                action.MoveToElement(ele).Perform();
            }
            catch (Exception e)
            {
                CaptureScreenshot();
                LogFile.LogErrorInformation(e.ToString());
                SetupFile._status = "Fail";
                SetupFile._statusMessage = e.ToString();
                throw new Exception(e.ToString());
            }
        }
    }
}
