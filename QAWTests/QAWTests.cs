using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using System.Diagnostics;
using OpenQA.Selenium.Support;
using System;
using OpenQA.Selenium.Support.UI;



namespace QAWTests
{
    [TestClass]

    public class QAWValidSubmission
    {
        [TestMethod]

        public void RunTest()
        {
            //here we create a new instance of the Firefox driver
            var driver = new FirefoxDriver();
            // GoTo 'Contact'
            driver.Navigate().GoToUrl("http://www.qaworks.com/contact.aspx");
            driver.Manage().Timeouts().ImplicitlyWait(System.TimeSpan.FromSeconds(5));
            //new WebDriverWait(driver, System.TimeSpan.  3 );
            
            driver.FindElement(By.Id("ctl00_MainContent_NameBox")).SendKeys("Bloggs");
            driver.FindElement(By.Id("ctl00_MainContent_EmailBox")).SendKeys("j.Bloggs@qaworks.com");
            driver.FindElement(By.Id("ctl00_MainContent_MessageBox")).SendKeys("Please contact me I want to find out more");
            
            // GoTo 'Careers'
            driver.Navigate().GoToUrl("http://careers.qaworks.com/");
            driver.Manage().Timeouts().ImplicitlyWait(System.TimeSpan.FromSeconds(5));
            
            IWebElement table = driver.FindElement(By.Id("jobListings"));

            //Get number of rows in table 
            int numOfRow = table.FindElements(By.TagName("tr")).Count;
            List<string> jobLinkUrl = new System.Collections.Generic.List(numOfRow);

            for (int j = 1; j <= (numOfRow - 2); j = (j + 2))
            {

                string first_part_jobShortDesc_xpath = "//*[@id='jobListings']/tbody/tr[";
                string second_part_jobShortDesc_xpath = "]/td/span";

                string first_part_jobLinkTitle_xpath = "//*[@id='jobListings']/tbody/tr[";
                string second_part_jobLinkTitle_xpath = "]/td/a";

                string first_part_jobLinkUrl_xpath = "//*[@id='jobListings']/tbody/tr[";
                string second_part_jobLinkUrl_xpath = "]/td/a";

                IWebElement webJobLinkUrl = driver.FindElement(By.XPath(first_part_jobLinkUrl_xpath + (j + 2) + second_part_jobLinkUrl_xpath));
                jobLinkUrl.Add(webJobLinkUrl.GetAttribute("href"));
                //                   
                string final_jobLinkTitle_xpath = first_part_jobLinkTitle_xpath + (j + 2) + second_part_jobLinkTitle_xpath;
                string final_jobShortDesc_xpath = first_part_jobShortDesc_xpath + (j + 3) + second_part_jobShortDesc_xpath;

                IWebElement webJobLinkTitle = driver.FindElement(By.XPath(final_jobLinkTitle_xpath));
                string jobLinkTitle = webJobLinkTitle.GetAttribute("text");

                IWebElement webJobShortDesc = driver.FindElement(By.XPath(final_jobShortDesc_xpath));
                string jobShortDesc = webJobShortDesc.GetAttribute("innerHTML");
                                

                Debug.WriteLine("Job Title = " + jobLinkTitle);
                Debug.WriteLine("");

                Debug.WriteLine("Short Description of Job = " + jobShortDesc);
                Debug.WriteLine("");

                Debug.WriteLine("Link URL of Job = " + jobLinkUrl);
                Debug.WriteLine("");
            }

            //        // GoTo 'Full Job Description'
            for (int i = 0; i < jobLinkUrl.Count; i++)
            {

                driver.Navigate().GoToUrl(jobLinkUrl[i]);
                driver.Manage().Timeouts().ImplicitlyWait(System.TimeSpan.FromSeconds(5));
                //          
                IWebElement webJobFullDescData = driver.FindElement(By.XPath("//*[@id='jobDetails']/div[2]/table/tbody/tr[1]/td"));
                string jobFullDescData = webJobFullDescData.Text; //.GetAttribute("Text");
                //     
                Debug.WriteLine("Job Full Description Data = " + jobFullDescData);
                Debug.WriteLine("");

            }
            
        }
    }
}
