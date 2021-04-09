using System;
using System.Runtime.Remoting.Contexts;
using AppiumExamle.Screens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace AppiumExamle
{
    [TestClass]
    public class UnitTest1
    {
        private static Uri testServerAddress = new Uri("http://localhost:4723/wd/hub");
        private static TimeSpan INIT_TIMEOUT_SEC = TimeSpan.FromSeconds(180);
        
        private LoginScreen loginScreen;
        AndroidDriver<AndroidElement> driver;
        AppiumOptions capabilities;
        
        [TestInitialize]
        public void Init()
        {
            capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.AutomationName, AutomationName.AndroidUIAutomator2);
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Android Emulator");
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, @"C:\Users\Svyatoslav\Desktop\AT\AutomationApp.apk");
            driver = new AndroidDriver<AndroidElement>(testServerAddress, capabilities, INIT_TIMEOUT_SEC);
            loginScreen = new LoginScreen(driver);
        }
        
        [TestMethod]
        public void ValidData()
        {
            loginScreen.WriteUsername(Environment.GetEnvironmentVariable("usernameValid"))
                    .WritePassword(Environment.GetEnvironmentVariable("passwordValid"));

            Assert.AreEqual(true, loginScreen.ValidateUsername());
            Assert.AreEqual(true, loginScreen.ValidatePassword());

            loginScreen.ClickSignInButton();
        }
        [TestMethod]
        public void NotValidEmail()
        {
            loginScreen.WriteUsername(Environment.GetEnvironmentVariable("usernameInValid"))
                    .WritePassword(Environment.GetEnvironmentVariable("passwordValid"));
            Assert.AreEqual(false, loginScreen.ValidateUsername());
        }
        [TestMethod]
        public void NotValidPassword()
        {
            loginScreen.WriteUsername(Environment.GetEnvironmentVariable("usernameValid"))
                    .WritePassword(Environment.GetEnvironmentVariable("passwordInValid"));
            Assert.AreEqual(false,loginScreen.ValidatePassword());
        }
        [TestMethod]
        public void DisabledButton()
        {
            Assert.AreEqual(false , loginScreen.StatusButton());
        }
    }
}
