﻿using KitchenSink.Tests.Ui;
using KitchenSink.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace KitchenSink.Tests.Test
{
    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    [Ignore("For test purposes")]

    internal class UrlPageTest : BaseTest
    {
        private UrlPage _urlPage;
        private MainPage _mainPage;

        public UrlPageTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _urlPage = _mainPage.GoToUrlPage();
        }

        private string GetIframeCurrentURL()
        {
            IJavaScriptExecutor jsExecuter = (IJavaScriptExecutor)Driver;
            return (string)jsExecuter.ExecuteScript("return document.querySelector('#link-target').contentWindow.location.href");
        }

        [TearDown]
        public void TeadDown()
        {
            _urlPage = _mainPage.GoToUrlPage();
        }

        [Test]
        public void UrlPage_ClickSimpleLink()
        {
            IJavaScriptExecutor jsExecuter = (IJavaScriptExecutor)Driver;
            WaitUntil(x => _urlPage.SimpleMorphableLink.Displayed);

            // leave a foot print in the window object
            jsExecuter.ExecuteScript("window.footprintExists = true");

            // control test 
            Assert.AreEqual($"{Config.KitchenSinkUrl}/Url", Driver.Url);
            Assert.AreEqual(true, jsExecuter.ExecuteScript("return window.footprintExists"));

            _urlPage.ClickSimpleMorphableLink();

            System.Threading.Thread.Sleep(2000);

            Assert.AreEqual(Driver.Url, Config.KitchenSinkUrl.ToString());

            // if the foot print still exists, we can infer that the page was actually morphed, not fully loaded
            Assert.AreEqual(true, jsExecuter.ExecuteScript("return window.footprintExists"));

        }
        
        [Test]
        public void UrlPage_ClickBlankTargettedLink()
        {
            WaitUntil(x => _urlPage.BlankTargettedLink.Displayed);

            //control test 
            Assert.AreEqual(Driver.WindowHandles.Count, 1);

            _urlPage.ClickBlankTargettedLink();

            System.Threading.Thread.Sleep(500);

            Assert.AreEqual(Driver.WindowHandles.Count, 2);

            //close pop up
            Driver.SwitchTo().Window(Driver.WindowHandles[1]);
            Driver.Close();

            // use the original page
            Driver.SwitchTo().Window(Driver.WindowHandles[0]);
        }

        [Test]
        public void UrlPage_ClickIframeTargettedLink()
        {
            WaitUntil(x => _urlPage.IframeTargettedLink != null && _urlPage.IframeTargettedLink.Displayed);

            //control test 
            Assert.AreEqual(GetIframeCurrentURL(), "about:blank");

            _urlPage.ClickIframeTargettedLink();

            System.Threading.Thread.Sleep(500);

            Assert.AreEqual(GetIframeCurrentURL(), Config.KitchenSinkUrl.ToString());
        }
    }
}
