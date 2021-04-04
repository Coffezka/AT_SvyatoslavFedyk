using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppiumExamle.Screens
{
    class LoginScreen
    {
        private readonly AndroidDriver<AndroidElement> driver;
        public LoginScreen(AndroidDriver<AndroidElement> screenDriver)
        {
            driver = screenDriver;
        }
        protected AndroidElement UsernameField => driver.FindElementById("com.example.automationqa:id/username");
        protected AndroidElement PasswordField => driver.FindElementById("com.example.automationqa:id/password");
        protected AndroidElement SignInButton => driver.FindElementById("com.example.automationqa:id/login");
        protected AndroidElement ValidatorUsername => driver.FindElementById("com.example.automationqa:id/ValidatorUsername");
        protected AndroidElement ValidatorPassword => driver.FindElementById("com.example.automationqa:id/ValidatorPassword");

        public LoginScreen ClickUsernameField()
        {
            UsernameField.Click();
            return this;
        }

        public LoginScreen ClickPasswordField()
        {
            PasswordField.Click();
            return this;
        }

        public LoginScreen ClickSignInButton()
        {
            SignInButton.Click();
            return this;
        }

        public LoginScreen WriteUsername(string text)
        {
            UsernameField.SendKeys(text);
            return this;
        }

        public LoginScreen WritePassword(string text)
        {
            PasswordField.SendKeys(text);
            return this;
        }

        public bool StatusButton() {
            return "true" == SignInButton.GetAttribute("enabled").ToLower();
        }

        public bool ValidateUsername()
        {
            try
            {

                return !("not a valid username" == ValidatorUsername.GetAttribute("text").ToLower());
            }
            catch
            {
                return true;
            }
        }

        public bool ValidatePassword()
        {
            try
            {
                // Do not initialize this variable here.
                return !("password must be >5 characters" == ValidatorPassword.GetAttribute("text").ToLower());
            }
            catch{
                return true;
            }
        }

    }
}
