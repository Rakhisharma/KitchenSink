using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace KitchenSink.Tests.Ui
{
    public class MarkdownPage : BasePage
    {
        public MarkdownPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.TagName, Using = "juicy-markdown")]
        public IWebElement JuicyMarkdown { get; set; }

        public string GetHeaderText()
        {
            return GetJuicyMarkdown().Text;
        }

        private IWebElement GetJuicyMarkdown()
        {
            return GetElementByQuerySelector(By.TagName("juicy-markdown"), "h1");
        }
    }
}
