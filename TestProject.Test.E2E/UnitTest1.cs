using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace TestProject.Test.E2E;

public class Tests
{
    private readonly string _desktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var options = new AppiumOptions();
        options.AddAdditionalCapability("app", @$"{_desktopDirectory}\TaskManager\TaskManager\bin\Debug\net7.0-windows\TaskManager.exe");
        options.AddAdditionalCapability("appWorkingDir", @$"{_desktopDirectory}\TaskManager\TaskManager\bin\Debug\net7.0-windows");
        
        var driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/"),
            options);

        try
        {
            driver.FindElementByAccessibilityId("AddTagToFilterButton").Click();
            driver.FindElementByAccessibilityId("RemoveTagFromFilterButton").Click();

            driver.FindElementByAccessibilityId("AddTagToFilterButton").Click();
            var clearTag = driver.FindElementByAccessibilityId("ClearTagsButton");
            Assert.That(clearTag.Enabled, Is.EqualTo(true));
            clearTag.Click();
            Assert.That(clearTag.Enabled, Is.EqualTo(false));
        }
        finally
        {
            driver.Close();
        }
    }
}