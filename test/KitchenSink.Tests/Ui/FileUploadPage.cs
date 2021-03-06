﻿using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace KitchenSink.Tests.Ui
{
    public class FileUploadPage : BasePage
    {
        public FileUploadPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = ".alert-warning")]
        public IWebElement InfoLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-uploaded-files-list")]
        public IList<IWebElement> UploadedFilesList { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-delete-button")]
        public IList<IWebElement> DeleteButtons { get; set; }

        public void UploadAFile(string filePath)
        {
            GetFileElement().SendKeys(filePath);
        }

        public int GetUploadedFilesCount()
        {
            return UploadedFilesList.Count;
        }

        public bool CheckFileInputVisible()
        {
            return GetFileElement().Enabled;
        }

        public void DeleteAllFiles()
        {
            foreach (var deleteButton in DeleteButtons)
            {
                ClickOn(deleteButton);
            }
        }

        private IWebElement GetFileElement()
        {
            return GetShadowElementByQuerySelector(By.XPath("//starcounter-upload"), "#fileElement");
        }

    }
}
