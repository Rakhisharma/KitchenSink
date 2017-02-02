﻿using KitchenSink.Tests.Ui;
using KitchenSink.Tests.Ui.SectionCustom;
using NUnit.Framework;
using System.IO;
using KitchenSink.Tests.Utilities;

namespace KitchenSink.Tests.Test.SectionCustom
{
    [TestFixture(Config.Browser.Chrome, "Krystian Matti", "Running File Upload Page Test on Chrome")]
    [TestFixture(Config.Browser.Edge, "Krystian Matti", "Running File Upload Page Test on Edge")]
    [TestFixture(Config.Browser.Firefox, "Krystian Matti", "Running File Upload Page Test on Firefox")]
    class FileUploadPageTest : BaseTest
    {
        private FileUploadPage _fileUploadPage;
        private MainPage _mainPage;

        public FileUploadPageTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _fileUploadPage = _mainPage.GoToFileUploadPage();
        }

        [Test]
        public void FileUploadPage_UploadAFile()
        {
            WaitUntil(x => _fileUploadPage.FileInput.Enabled);

            string tempFilePath = Path.GetTempFileName();
            using (StreamWriter outputFile = new StreamWriter(tempFilePath, false))
            {
                outputFile.WriteLine("Test123");
            }

            _fileUploadPage.UploadAFile(tempFilePath);
            WaitUntil(x => _fileUploadPage.GetUploadedFilesCount() > 0);

            Assert.AreEqual("Do not forget to delete files from your temporary folder!", _fileUploadPage.InfoLabel.Text);
        }

        [Test]
        public void FileUploadPage_UploadAndDeleteAFile()
        {
            WaitUntil(x => _fileUploadPage.FileInput.Enabled);

            string tempFilePath = Path.GetTempFileName();
            using (StreamWriter outputFile = new StreamWriter(tempFilePath, false))
            {
                outputFile.WriteLine("Test123");
            }

            _fileUploadPage.UploadAFile(tempFilePath);
            WaitUntil(x => _fileUploadPage.GetUploadedFilesCount() > 0);

            Assert.AreEqual("Do not forget to delete files from your temporary folder!", _fileUploadPage.InfoLabel.Text);

            _fileUploadPage.DeleteAllFiles();
            WaitUntil(x => _fileUploadPage.GetUploadedFilesCount() == 0);

            Assert.IsTrue(!_fileUploadPage.InfoLabel.Displayed);
        }
    }
}
