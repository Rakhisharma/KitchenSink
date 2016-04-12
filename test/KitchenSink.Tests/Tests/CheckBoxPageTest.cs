﻿using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using KitchenSink.Tests;

namespace KitchenSink.Test {

    [TestFixture("firefox")]
    [TestFixture("chrome")]
    [TestFixture("edge")]
    //[TestFixture("internet explorer")]
    public class CheckboxPageTest : BaseTest
    {
        private string canDrive = "You can drive";
        private string cantDrive = "You can't drive";

        public CheckboxPageTest(string browser) : base(browser) {}

        [Test]
        public void CheckboxPage_PageLoads() {
            driver.Navigate().GoToUrl(baseURL);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(_driver => _driver.FindElement(By.LinkText("Checkbox")));
            var passLink = driver.FindElement(By.LinkText("Checkbox"));
            var action = new OpenQA.Selenium.Interactions.Actions(driver);
            action.Click(passLink).Build().Perform();
            wait.Until(_driver => _driver.FindElement(By.XPath("(//input)[1]")));
            IWebElement element = driver.FindElement(By.XPath("(//input)[1]"));
            Assert.AreEqual(element.GetAttribute("type"), "checkbox");
        }

        [Test]
        public void CheckboxPage_CheckboxUnchecked()
        {
            driver.Navigate().GoToUrl(baseURL + "/Checkbox");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(_driver => _driver.FindElement(By.XPath("(//input)[1]")));
            IWebElement element = driver.FindElement(By.XPath("(//input)[1]"));
            Assert.AreEqual(element.GetAttribute("type"), "checkbox");
            var action = new OpenQA.Selenium.Interactions.Actions(driver);
            action.Click(element).Build().Perform();
            wait.Until((x) => {
                return !canDrive.Equals(driver.FindElement(By.CssSelector("div.kitchensink-layout__column-right > starcounter-include > div")).Text);
            });
            var uncheckedText = driver.FindElement(By.CssSelector("div.kitchensink-layout__column-right > starcounter-include > div")).Text;
            Assert.AreEqual(cantDrive, uncheckedText);
        }

        [Test]
        public void CheckboxPage_CheckboxUncheckedAndCheckedAgain()
        {
            driver.Navigate().GoToUrl(baseURL + "/Checkbox");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(_driver => _driver.FindElement(By.XPath("(//input)[1]")));
            IWebElement element = driver.FindElement(By.XPath("(//input)[1]"));
            Assert.AreEqual(element.GetAttribute("type"), "checkbox");
            var action = new OpenQA.Selenium.Interactions.Actions(driver);
            action.Click(element).Build().Perform();
            wait.Until((x) => {
                return !canDrive.Equals(driver.FindElement(By.CssSelector("div.kitchensink-layout__column-right > starcounter-include > div")).Text);
            });
            var uncheckedText = driver.FindElement(By.CssSelector("div.kitchensink-layout__column-right > starcounter-include > div")).Text;
            Assert.AreEqual(cantDrive, uncheckedText);
            action.Click(element).Build().Perform();
            wait.Until((x) => {
                return !cantDrive.Equals(driver.FindElement(By.CssSelector("div.kitchensink-layout__column-right > starcounter-include > div")).Text);
            });
            var checkedText = driver.FindElement(By.CssSelector("div.kitchensink-layout__column-right > starcounter-include > div")).Text;
            Assert.AreEqual(canDrive, checkedText);
        }
    }
}
