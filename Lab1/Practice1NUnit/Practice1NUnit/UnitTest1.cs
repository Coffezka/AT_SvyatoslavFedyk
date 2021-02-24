using NUnit.Framework;
using System;
using Microsoft.VisualBasic.FileIO;


namespace Practice1NUnit
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestFixture]
        public class DataDriven {
            [OneTimeSetUp]
            public void Init()
            {
                Console.WriteLine("Hello World");
            }

            [OneTimeTearDown]
            public void Cleanup()
            {
                var timeUtc = DateTime.UtcNow;
                TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
                Console.WriteLine("Test Ended -  {0}",
                   timeUtc);
            }


            [Test, TestCaseSource(nameof(TestData))]
            public void Test
            (double Number, double Exp)
            {
                var timeUtc = DateTime.UtcNow;
                TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
                TestContext.WriteLine("Test Started -  {0}",
                   timeUtc);

                double Result = App.Program.Formula(Number);
                Assert.AreEqual(Exp, Result);

                Console.Out.WriteLine("Cruel World");
            }
            private static System.Collections.Generic.IEnumerable<TestCaseData> TestData()
            {

                yield return new TestCaseData(1, 1);
                yield return new TestCaseData(5, 4.23606797749979);

            }

        }

        [TestFixture]
        public class DataDrivenRandom {
            [OneTimeSetUp]
            public void Init()
            {
                Console.WriteLine("Hello World");
            }

            [OneTimeTearDown]
            public void Cleanup()
            {
                var timeUtc = DateTime.UtcNow;
                TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
                Console.WriteLine("Test Ended -  {0}",
                   timeUtc);
            }

            [Test, TestCaseSource(nameof(TestData)), Timeout(1000)]
            public void Test
            (double Number)
            {
                var timeUtc = DateTime.UtcNow;
                TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
                TestContext.WriteLine("Test Started -  {0}",
                   timeUtc);

                double Result = App.Program.Formula(Number);
                Assert.Greater(Result, 0);

                Console.Out.WriteLine("Cruel World");
            }
            private static System.Collections.Generic.IEnumerable<TestCaseData> TestData()
            {
                var rand = new Random(73247823);
                var n = 10;

                for (int i = 0; i < n + 1; i++)
                {
                    yield return new TestCaseData(rand.Next(50, 100));
                }

            }
        }

        [TestFixture]
        public class DataDrivenCSV
        {
            [OneTimeSetUp]
            public void Init()
            {
                Console.WriteLine("Hello World");
            }

            [OneTimeTearDown]
            public void Cleanup()
            {
                var timeUtc = DateTime.UtcNow;
                TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
                Console.WriteLine("Test Ended -  {0}",
                   timeUtc);
            }

            [Test, TestCaseSource(nameof(TestData)), Timeout(1000)]
            public void Test
            (double Number, double Ex) {

                var timeUtc = DateTime.UtcNow;
                TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
                TestContext.WriteLine("Test Started -  {0}",
                   timeUtc);

                double Result = App.Program.Formula(Number);
                Assert.AreEqual(Ex, Result);

                TestContext.WriteLine("Cruel World");
            }

            private static System.Collections.Generic.IEnumerable<TestCaseData> TestData()
            {
                var path = @"C:\Users\Svyatoslav\Desktop\AT\Data.csv";
                using (TextFieldParser csvParser = new TextFieldParser(path))
                {
                    csvParser.CommentTokens = new string[] { "#" };
                    csvParser.SetDelimiters(new string[] { "," });
                    csvParser.HasFieldsEnclosedInQuotes = true;

                    while (!csvParser.EndOfData)
                    {
                        string[] fields = csvParser.ReadFields();
                        double Number = Convert.ToDouble(fields[0].Replace('.', ','));
                        double Exp = Convert.ToDouble(fields[1].Replace('.', ','));
                        yield return new TestCaseData(Number, Exp);
                    }
                }
            }
        }

        [TestFixture]
        public class TestReloadAndTimeOut
        {
            [OneTimeSetUp]
            public void Init()
            {
                Console.WriteLine("Hello World");
            }

            [OneTimeTearDown]
            public void Cleanup()
            {
                var timeUtc = DateTime.UtcNow;
                TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
                Console.WriteLine("Test Ended -  {0}",
                   timeUtc);
            }

            [TestCase(1,1), Timeout(1), Retry(1)]
            public void Test
            (double Number, double Exp)
            {
                var timeUtc = DateTime.UtcNow;
                TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
                TestContext.WriteLine("Test Started -  {0}",
                   timeUtc);

                double Result = App.Program.Formula(Number);
                Assert.AreEqual(Exp, Result);

                TestContext.WriteLine("Cruel World");
            }
        }

        [TestFixture]
        public class FailedTest
        {
            [OneTimeSetUp]
            public void Init()
            {
                Console.WriteLine("Hello World");
            }

            [OneTimeTearDown]
            public void Cleanup()
            {
                var timeUtc = DateTime.UtcNow;
                TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
                Console.WriteLine("Test Ended -  {0}",
                   timeUtc);
            }

            [TestCase(-1, 1)]
            public void Test
            (double Number, double Exp)
            {
                var timeUtc = DateTime.UtcNow;
                TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
                TestContext.WriteLine("Test Started -  {0}",
                   timeUtc);

                Assert.Throws<Exception>(() => App.Program.Formula(Number));

                TestContext.WriteLine("Cruel World");
            }
        }


    }
}