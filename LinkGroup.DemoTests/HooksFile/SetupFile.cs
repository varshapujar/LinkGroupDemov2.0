using BoDi;
using LinkGroup.DemoTests.Helpers;
using LinkGroup.DemoTests.ReusableFile;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace LinkGroup.DemoTests.HooksFile
{
    class SetupFile
    {
        
        public static string _status = "Pass";
        public static string _tcDescription = "";
        public static string _statusMessage = "Test case failed since actual and expected results do not match";
        public static IWebDriver driver = null;
        public static string basePath = "";

       [Binding]
        public class Setup
        {
            
            public static string BrowserdriverPath = ConfigurationManager.AppSettings["BrowserdriverPath"];
            public static IWebDriver driver = null;
            private static ReusableMethods reusableMethods = new ReusableMethods();
            private readonly IObjectContainer container;
            public Setup(IObjectContainer container)
            {
                this.container = container;
            }

            [BeforeScenario]
            public void BeforeScenario()
            {
                
                reusableMethods.LaunchBrowser();

                var _tcDescription = ScenarioContext.Current.ScenarioInfo.Title;
                string name = _tcDescription.ToString();
                _status = "Pass";
            }

            [AfterScenario]
            public void AfterScenario()
            {
            
               reusableMethods.Closedriver();

            
            }
        }
    }
}