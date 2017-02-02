﻿using KitchenSink.Tests.Ui;
using KitchenSink.Tests.Ui.SectionArray;
using KitchenSink.Tests.Utilities;
using NUnit.Framework;

namespace KitchenSink.Tests.Test.SectionArray
{
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    class RadioPageTest : BaseTest
    {
        private RadioPage _radioPage;
        private MainPage _mainPage;

        public RadioPageTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _radioPage = _mainPage.GoToRadioPage();
        }

        [Test]
        public void ButtonPage_RegularButton()
        {
            WaitUntil(x => _radioPage.InfoLabel.Displayed);
            Assert.AreEqual("You like dogs", _radioPage.InfoLabel.Text);

            _radioPage.SelectRadio("cats");
            Assert.AreEqual("You like cats", _radioPage.InfoLabel.Text);

            _radioPage.SelectRadio("rabbit");
            Assert.AreEqual("You like rabbit", _radioPage.InfoLabel.Text);

            _radioPage.SelectRadio("dogs");
            Assert.AreEqual("You like dogs", _radioPage.InfoLabel.Text);
        }
    }
}
