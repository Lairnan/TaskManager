using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace TestProject.Test.E2E;

public class Tests
{
    private readonly string _desktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    private WindowsDriver<WindowsElement> _driver = null!;
    
    [OneTimeSetUp]
    public void Setup()
    {
        var options = new AppiumOptions();
        options.AddAdditionalCapability("app", @$"{_desktopDirectory}\TaskManager\TaskManager\bin\Debug\net7.0-windows\TaskManager.exe");
        options.AddAdditionalCapability("appWorkingDir", @$"{_desktopDirectory}\TaskManager\TaskManager\bin\Debug\net7.0-windows");
        
        _driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/"),
            options);
        
        Thread.Sleep(1000);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _driver.Close();
    }
    
    [Test]
    public void ClearTagsButtonIsEnabledTest()
    {
        Thread.Sleep(500);

        _driver.FindElementByAccessibilityId("AddTagToFilterButton").Click();
        
        _driver.FindElementByAccessibilityId("RemoveTagFromFilterButton").Click();

        _driver.FindElementByAccessibilityId("AddTagToFilterButton").Click();
        var clearTag = _driver.FindElementByAccessibilityId("ClearTagsButton");
        Assert.That(clearTag.Enabled, Is.EqualTo(true));
        clearTag.Click();
    }

    // TODO: Не определяется количество элементов в ItemsControl.
    [Ignore("Always empty")]
    [Test]
    public void HasCollectionInFilterTagsTest()
    {
        Thread.Sleep(500);

        _driver.FindElementByAccessibilityId("AddTagToFilterButton").Click();

        var filter = _driver.FindElementsByAccessibilityId("FilterTagsItemsControl");
        Assert.That(filter, Is.Not.Empty);
        _driver.FindElementByAccessibilityId("ClearTagsButton").Click();
    }

    // TODO: Не выполняется правильно команда Enter, после ввода элемента.
    [Ignore("Enter not working")]
    [Test]
    public void ComboBoxEditableAddedTagsToFilterTest()
    {
        Thread.Sleep(500);

        var tagsCombobox = _driver.FindElementByAccessibilityId("TagsCollectionComboBox");
        tagsCombobox.SendKeys("New Tag");
        tagsCombobox.Submit();

        var filter = _driver.FindElementsByAccessibilityId("FilterTagsItemsControl");
        Assert.That(filter, Is.Not.Empty);
        _driver.FindElementByAccessibilityId("ClearTagsButton").Click();
    }

    // TODO: При открытии combobox, не получает элементы внутри combobox.
    [Ignore("Enter not working")]
    [Test]
    public void ComboBoxClickableElementTest()
    {
        Thread.Sleep(500);
        
        var tagsCombobox = _driver.FindElementByAccessibilityId("TagsCollectionComboBox");
        tagsCombobox.Click();
        Thread.Sleep(150);
        var tags = tagsCombobox.FindElementsById("ComboBoxItem");
        tags.First().Click();

        _driver.FindElementByAccessibilityId("AddFilterFromComboBoxButton").Click();
        
        var filter = _driver.FindElementsByAccessibilityId("FilterTagsItemsControl");
        Assert.That(filter, Is.Not.Empty);
        _driver.FindElementByAccessibilityId("ClearTagsButton").Click();
    }
}