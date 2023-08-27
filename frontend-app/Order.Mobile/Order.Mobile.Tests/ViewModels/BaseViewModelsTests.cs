using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Mobile.Tests.ViewModels
{
    using NUnit.Framework;
    using Moq;
    using System.ComponentModel;

    [TestFixture]
    public class BaseViewModelTests
    {
        private BaseViewModel viewModel;

        [SetUp]
        public void SetUp()
        {
            viewModel = new BaseViewModel();
        }

        [Test]
        public void IsBusy_ShouldRaisePropertyChangedEvent()
        {
            // Arrange
            var propertyChangedRaised = false;
            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(BaseViewModel.IsBusy))
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            viewModel.IsBusy = true;

            // Assert
            Assert.IsTrue(propertyChangedRaised);
        }

        [Test]
        public void Title_ShouldRaisePropertyChangedEvent()
        {
            // Arrange
            var propertyChangedRaised = false;
            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(BaseViewModel.Title))
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            viewModel.Title = "Test Title";

            // Assert
            Assert.IsTrue(propertyChangedRaised);
        }
    }

}
