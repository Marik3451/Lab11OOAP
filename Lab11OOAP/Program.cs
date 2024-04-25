using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

class Program
{
    static void Main()
    {
        IWebDriver driver = new ChromeDriver();

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        driver.Navigate().GoToUrl("https://rozetka.com.ua/");

        List<string> productsToSearch = new List<string> { "телефон", "ноутбук", "планшет" };

        foreach (var product in productsToSearch)
        {
            IWebElement searchInput = driver.FindElement(By.Name("search"));
            searchInput.Clear();
            searchInput.SendKeys(product);
            searchInput.SendKeys(Keys.Enter);

            Thread.Sleep(2000);

            IReadOnlyCollection<IWebElement> searchResults = driver.FindElements(By.CssSelector(".goods-tile"));

            Console.WriteLine($"Результати пошуку для товару: {product}");

            int count = 0;
            foreach (var result in searchResults)
            {
                if (count >= 10)
                {
                    break;
                }

                string productName = result.FindElement(By.CssSelector(".goods-tile__title")).Text;
                string productPrice = result.FindElement(By.CssSelector(".goods-tile__price-value")).Text;

                Console.WriteLine($"Назва: {productName}, Ціна: {productPrice}");

                count++;
            }


            Console.WriteLine();
        }

        driver.Quit();
    }
}
