using Microsoft.VisualStudio.TestTools.UnitTesting;
using Currency2StringConverter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency2StringConverter.ViewModels.Tests
{
    [TestClass()]
    public class MainViewModelTests
    {
        [TestMethod()]
        public void MainViewModelCreationTest()
        {
            ViewModelBase viewModel = new MainViewModel();
            Assert.IsNotNull(viewModel);
        }

        [TestMethod()]
        public void MainViewModelConvertionResultChangedTest()
        {
            MainViewModel viewModel = new MainViewModel();
            string propertyName = string.Empty;
            viewModel.PropertyChanged+=(sender, args) => propertyName = args.PropertyName;
            viewModel.ConversionResult = "test";
            Assert.AreEqual(nameof(viewModel.ConversionResult), propertyName);
        }
    }
}