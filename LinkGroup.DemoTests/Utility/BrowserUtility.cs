using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading;

namespace LinkGroup.DemoTests.ReusableFile
{
    class BrowserUtility
    {
        public IWebDriver _currentWebDriver;
        public static string BrowserDriverPath = ConfigurationManager.AppSettings["BrowserDriverPath"];
        private ReusableMethods reusableMethods = new ReusableMethods();

        public IWebDriver Current
        {

            get
            {

                if (_currentWebDriver != null)
                    return _currentWebDriver;
                BrowserDriverPath = reusableMethods.BasePath + BrowserDriverPath;



                ChromeOptions option = new ChromeOptions();
                ChromeDriverService service = ChromeDriverService.CreateDefaultService(reusableMethods.BasePath + @"\Drivers\ChromeDriver");
                service.SuppressInitialDiagnosticInformation = true;
                option.AddArgument("--disable-extensions");
                option.AddUserProfilePreference("profile.password_manager_enabled", false);
                option.AddUserProfilePreference("credentilas_enabled_service", false);
                option.AddArgument("--start-maximized");
                option.AddArgument("--allow-file-acces-from-file");
                option.AddArgument("--disable-popup-blocking");
                option.AddUserProfilePreference("download.default_directory", reusableMethods.BasePath + @"\Downloads\");
                
                option.AddUserProfilePreference("disable-popup-blocking", "true");
                int threadId = Thread.CurrentThread.ManagedThreadId;
                string downloadDirPath = reusableMethods.BasePath + @"\Downloads" + @"\Downloads" + "_" + threadId + @"\";

                System.IO.Directory.CreateDirectory(downloadDirPath);
                option.AddUserProfilePreference("download.default_directory", downloadDirPath);
                option.AddUserProfilePreference("plugins.always_open_pdf_externally", true);

                option.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
               
                _currentWebDriver = new ChromeDriver(service, option);
                return _currentWebDriver;
            }

        }
        private WebDriverWait _wait;
        public WebDriverWait Wait
        {
            get
            {
                if (_wait == null)
                {
                    this._wait = new WebDriverWait(Current, TimeSpan.FromSeconds(10));
                }
                return _wait;
            }
        }

        protected string BrowserConfig => ConfigurationManager.AppSettings["browser"];

        protected string SeleniumBaseUrl => ConfigurationManager.AppSettings["seleniumBaseUrl"];

        public void Quit()
        {
            _currentWebDriver?.Quit();
        }
    }
}
