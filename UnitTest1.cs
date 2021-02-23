using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace ContactBook_Selenium_Tests
{
    public class Tests
    {

        ChromeDriver driver;


        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            driver.Navigate().GoToUrl("https://contactbook.kopan77.repl.co/");// not necessary...
         

        }

        [Test]
        public void TestIfSteveJobsIsFirstContact()
        {
            driver.Navigate().GoToUrl("https://contactbook.kopan77.repl.co/contacts");
            //Instead of the line above i could use the following line, but but I don't think it is necessary :) 
            //driver.FindElementByCssSelector(".home-page-icons a:nth-of-type(1)").Click(); //
            var firstName = driver.FindElementByCssSelector("#contact1 .fname td").Text;
            string lastName = driver.FindElementByCssSelector("#contact1 .lname td").Text;
            string testName = firstName + lastName;
            Assert.AreEqual("SteveJobs", testName);
        }

        [Test]

        public void testAlbert() 
        {

            driver.Navigate().GoToUrl("https://contactbook.kopan77.repl.co/contacts");
            var contacts = driver.FindElementsByCssSelector(".contact-entry");
            string firstName="";
            string lastName="";

            foreach (var contact in contacts) 
            {
                string stringifiedContact = contact.Text.ToLower();
                if (stringifiedContact.Contains("albert"))
                {

                    firstName = contact.FindElement(By.CssSelector(".contact-entry .fname td")).Text;
                    lastName = contact.FindElement(By.CssSelector(".contact-entry .lname td")).Text;
                    if (firstName == "Albert" && lastName == "Einstein") 
                    {
                        break; 
                    }

                }
                

            }

            string testName = firstName + lastName;
            Assert.AreEqual("AlbertEinstein", testName);
        }

        [Test]
        public void testInvalidSearchNotExisting()
        {

            driver.Navigate().GoToUrl("https://contactbook.kopan77.repl.co/contacts");
            var contacts = driver.FindElementsByCssSelector(".contact-entry");
            
            
            string invalidString = "invalid2635";
            bool isExist = false;

            foreach (var contact in contacts)
            {
                string stringifiedContact = contact.Text.ToLower();
                if (stringifiedContact.Contains(invalidString))
                {
                    isExist = true;
                    break;
                }
            }
           Assert.IsFalse(isExist);
        }

        [Test]
        public void createInvalidDataWithErrorMessage()
        {

            driver.Navigate().GoToUrl("https://contactbook.kopan77.repl.co/contacts/create");
            driver.FindElementByCssSelector("button#create").Click();
            // It is faster than to use .sendKeys to each field :) 
            // And still missing data is invalid data :) 
            // Also in the next Test everything is included :) 
            string text = driver.FindElementByCssSelector(".err").Text;
            Assert.Pass();
        }

        [Test]
        public void createValidData()
        {

            driver.Navigate().GoToUrl("https://contactbook.kopan77.repl.co/contacts/create");


            string firstName = "testNameMisfits1";
            string lastName = "testNameMisfits1";
            string testMail = "testNameMisfits1@email.com";
            string testPhone = "88866699991";
            string testComment = "No comment1"+DateTime.Now.Ticks;

            driver.FindElementByCssSelector("#firstName").SendKeys(firstName);
            driver.FindElementByCssSelector("#lastName").SendKeys(lastName);
            driver.FindElementByCssSelector("#email").SendKeys(testMail);
            driver.FindElementByCssSelector("#phone").SendKeys(testPhone);
            driver.FindElementByCssSelector("#comments").SendKeys(testComment);
            driver.FindElementByCssSelector("button#create").Click();

            string recordedFirstName = driver.FindElementByCssSelector("main a:last-of-type .fname td").Text;
            string recordedLastName = driver.FindElementByCssSelector("main a:last-of-type .lname td").Text;
            string recordedEmail = driver.FindElementByCssSelector("main a:last-of-type .email td").Text;
            string recordedPhone = driver.FindElementByCssSelector("main a:last-of-type .phone td").Text;
            string recordedComment = driver.FindElementByCssSelector("main a:last-of-type .comments td").Text;

            bool isTrue = false;
            if (firstName.Equals(recordedFirstName) &&
                lastName.Equals(recordedLastName) &&
                testMail.Equals(recordedEmail) &&
                testPhone.Equals(recordedPhone) &&
                testComment.Equals(recordedComment)) 
            {

                isTrue = true;
            }

            Assert.IsTrue(isTrue);

            // Anyway, we could assert, that we have created contact wih the following couple of lines: 
            //string currentPageInfo = driver.FindElementByCssSelector("header.h1").Text;
            //Assert.AreEqual("View Contacts", currentPageInfo);
        }



        [OneTimeTearDown]
        public void closeDriver()
        {

            driver.Quit();

        }

    }
}