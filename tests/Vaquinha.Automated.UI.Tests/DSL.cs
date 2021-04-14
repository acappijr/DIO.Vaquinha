using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using Vaquinha.AutomatedUITests;

namespace Vaquinha.Automated.UI.Tests
{
    public class DSL
    {
        private DriverFactory _driverFactory;
        private string _homeUrl;

        public DSL(string homeUrl)
        {
            _driverFactory = new DriverFactory();
            _homeUrl = homeUrl;
        }

        public DSL NavegarHome()
        {
            _driverFactory.NavigateToUrl(_homeUrl);

            return this;
        }

        public void Dispose()
        {
            _driverFactory.Close();
        }

        public IWebElement ObterElementoPorClasse(string classe)
        {
            var elemento = _driverFactory.GetWebDriver().FindElement(By.ClassName(classe));
            return elemento;
        }

        public IWebElement ObterElementoPorId(string id)
        {
            var elemento = _driverFactory.GetWebDriver().FindElement(By.Id(id));
            return elemento;
        }


        public string Url()
        {
            return _driverFactory.GetWebDriver().Url;
        }

        public void ClicarBotaoPorClasse(string classe)
        {
            var botao = ObterElementoPorClasse(classe);
            botao.Click();
        }

        public void EscreverPorId(string id, string texto)
        {
            var elemento = ObterElementoPorId(id);
            elemento.SendKeys(texto);
        }

        public bool PaginaContemTexto(string texto)
        {
            return _driverFactory.GetWebDriver().PageSource.Contains(texto);
        }
    }
}
