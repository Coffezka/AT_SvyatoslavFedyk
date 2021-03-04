using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Practice4Selenium
{
    public partial class SearchPageObject
    {
        private readonly IWebDriver driver;
        private readonly string url = @"https://rozetka.com.ua/";

        public IWebElement SearchBox => driver.FindElement(By.XPath("//input[@name='search']"));
        public IWebElement GoButton => driver.FindElement(By.XPath("//button[contains(., 'Знайти')]"));
        public ReadOnlyCollection<IWebElement> Prices => driver.FindElements(By.XPath("//span[contains(@class, 'goods-tile__price')]"));

        public SearchPageObject(IWebDriver browser)
        {
            driver = browser;
        }

        public SearchPageObject Navigate()
        {
            driver.Navigate().GoToUrl(url);
            return this;
        }

        public SearchPageObject SearchField(string textToType)
        {
            SearchBox.Clear();
            SearchBox.SendKeys(textToType);
            GoButton.Click();
            return this;
        }

        public SearchPageObject ClickSearchButton()
        {
            GoButton.Click();
            return this;
        }

        public String PriceText(int elem)
        {
            return Prices[elem-1].Text.Trim('₴');
        }

    }

    public partial class GoodPageObject
    {
        private readonly IWebDriver driver;

        public IWebElement Price => driver.FindElement(By.XPath("//p[contains(@class, 'product-prices__big')]"));
        public ReadOnlyCollection<IWebElement> Goods => driver.FindElements(By.XPath("//span[contains(@class, 'goods-tile__title')]"));
        public GoodPageObject(IWebDriver browser)
        {
            driver = browser;
        }

        public GoodPageObject ClickGood(int elem)
        {
            IWebElement Good = Goods[elem-1];
            Good.Click();
            return this;
        }

        public String PriceText()
        {
            return Price.Text.Trim('₴');
        }


    }

    public class SeleniumTests{
        IWebDriver driver;
        private SearchPageObject searchPage;
        private GoodPageObject goodPage;

        [OneTimeSetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver("C:\\Users\\Svyatoslav\\Desktop\\AT");
            searchPage = new SearchPageObject(driver);
            goodPage = new GoodPageObject(driver);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }

        [Test,Repeat(20)]
        public void rozetkaTest()
        {
            int elem = 1;

            //----
            searchPage
                .Navigate()
                .SearchField("JYU4191CN")
                .ClickSearchButton();

            string priceFirst = searchPage.PriceText(elem);
            //--

            //----
            goodPage
                .ClickGood(elem);

            string priceSecond = goodPage.PriceText();
            //--

            //----
            driver
                .Navigate()
                .Back();

            string priceThird = searchPage.PriceText(elem);

            TestContext.WriteLine(priceFirst);
            TestContext.WriteLine(priceSecond);
            TestContext.WriteLine(priceThird);
            Assert.AreEqual(priceFirst, priceSecond, "The results doesn't contains the specified text.");
            Assert.AreEqual(priceFirst, priceThird, "The results doesn't contains the specified text.");
            //--


            //--
            //driver.FindElement(By.XPath("//input[@name='search']")).SendKeys("JYU4191CN"); ;
            //driver.FindElement(By.XPath("//button[contains(., 'Знайти')]")).Click();
            //String priceFirst = driver.FindElement(By.XPath("//span[contains(@class, 'goods-tile__price')]")).Text;
            //TestContext.WriteLine(priceFirst);
            //--

            //--
            //driver.FindElement(By.XPath("//span[contains(@class, 'goods-tile__title')]")).Click();
            //String priceSecond = driver.FindElement(By.XPath("//p[contains(@class, 'product-prices__big')]")).Text;
            //TestContext.WriteLine(priceSecond);
            //--

            //Assert.AreEqual(priceFirst, priceSecond.Trim('₴'), "The results DIV doesn't contains the specified text.");

            //driver.Navigate().Back();

            //--
            //String priceThird = driver.FindElement(By.XPath("//span[contains(@class, 'goods-tile__price')]")).Text;
            //TestContext.WriteLine(priceThird);
            //-

            //Assert.AreEqual(priceFirst, priceThird, "The results doesn't contains the specified text.");
        }

        [OneTimeTearDown]
        public void closeBrowser()
        {
            driver.Close();
        }
    }
}