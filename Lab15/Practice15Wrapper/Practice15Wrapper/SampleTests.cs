using Atata;
using NUnit.Framework;
using System;
using System.Threading;

namespace Practice15Wrapper
{
    public class SampleTests : UITestFixture
    {


        [Test]
        public void SampleTest()
        {
            string good = "JYU4191CN";
            char[] ch = { '₴', ' ' };

            Go.To<Page1>()
                .Search.Set(good)
                .SearchButton.Click();


            string priceFirst = Go.To<Page1>().Price.Get().Trim(ch);

            Go.To<Page1>()
                .Good.Click();

            string priceSecond = Go.To<Page2>().Price.Get().Trim(ch);

            Go.ToUrl(String.Format("https://rozetka.com.ua/search/?text={0}",
                         good));
           

            string priceThird = Go.To<Page1>().Price.Get().Trim(ch);

            Assert.AreEqual(priceFirst, priceSecond, "The results doesn't contains the specified text.");
            Assert.AreEqual(priceFirst, priceThird, "The results doesn't contains the specified text.");

        }


    }
}
