using System;
using Order.Mobile.Services;
using Order.Mobile.Services.Identity;
using Order.Mobile.ViewModels;
using Moq;
using NUnit.Framework;

namespace Order.Mobile.Tests.ViewModels;

public class LoginViewModelTests
{
    private Mock<IIdentityService> _identityServiceMock;
    private Mock<ITokenProvider> _tokenProviderMock;
    private Mock<INavigationService> _navigationServiceMock;
    private Mock<IToastService> _toastServiceMock;
    private LoginViewModel _model;

    [SetUp]
    public void BeforeEachTest()
    {
        _identityServiceMock = new Mock<IIdentityService>();
        _tokenProviderMock = new Mock<ITokenProvider>();
        _navigationServiceMock = new Mock<INavigationService>();
        _toastServiceMock = new Mock<IToastService>();

        _model = new LoginViewModel(_identityServiceMock.Object, _tokenProviderMock.Object, _navigationServiceMock.Object, _toastServiceMock.Object);
    }

    [Test]
    public void LoginCommand_SuccessfulLogin_ShouldSaveAccessTokenAndNavigateToBarPage()
    {
        //Arrange
        ILoginResult successfulLoginResult = CreateSuccessfulLoginResult();
        _identityServiceMock.Setup(service => service.LoginAsync()).ReturnsAsync(successfulLoginResult);

        //Act
        _model.LoginCommand.Execute(null);

        //Assert
        _identityServiceMock.Verify(service => service.LoginAsync(), Times.Once);
        _tokenProviderMock.VerifySet(provider => provider.AuthAccessToken = successfulLoginResult.AccessToken, Times.Once);
        _navigationServiceMock.Verify(service => service.NavigateAsync("BarPage"), Times.Once);
        _toastServiceMock.Verify(service => service.DisplayToastAsync(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public void LoginCommand_FailedLogin_ShouldShowToastMessage()
    {
        //Arrange
        ILoginResult failedLoginResult = CreateFailedLoginResult();
        _identityServiceMock.Setup(service => service.LoginAsync()).ReturnsAsync(failedLoginResult);

        //Act
        _model.LoginCommand.Execute(null);

        //Assert
        _identityServiceMock.Verify(service => service.LoginAsync(), Times.Once);
        _tokenProviderMock.VerifySet(provider => provider.AuthAccessToken = It.IsAny<string>(), Times.Never);
        _navigationServiceMock.Verify(service => service.NavigateAsync(It.IsAny<string>()), Times.Never);
        _toastServiceMock.Verify(service => service.DisplayToastAsync(failedLoginResult.ErrorDescription), Times.Once);
    }

    [Test]
    public void LoginCommand_ShouldBeDisabledWhenOtherLoginIsInProgress()
    {
        //Arrange
        ILoginResult successfulLoginResult = CreateSuccessfulLoginResult();

        _identityServiceMock.Setup(service => service.LoginAsync()).ReturnsAsync(() =>
        {
            Assert.That(_model.IsBusy, Is.True);
            Assert.That(_model.LoginCommand.CanExecute(null), Is.False);
            return successfulLoginResult;
        });
        _model.LoginCommand.Execute(null);

        //Assert
        Assert.That(_model.IsBusy, Is.False);
        Assert.That(_model.LoginCommand.CanExecute(null), Is.True);
    }

    private ILoginResult CreateSuccessfulLoginResult()
    {
        string accessToken = Guid.NewGuid().ToString();
        var successfulLoginResultMock = new Mock<ILoginResult>();
        successfulLoginResultMock.SetupGet(result => result.IsError).Returns(false);
        successfulLoginResultMock.SetupGet(result => result.AccessToken).Returns(accessToken);
        return successfulLoginResultMock.Object;
    }

    private ILoginResult CreateFailedLoginResult()
    {
        string errorDescription = Guid.NewGuid().ToString();
        var failedLoginResultMock = new Mock<ILoginResult>();
        failedLoginResultMock.SetupGet(result => result.IsError).Returns(true);
        failedLoginResultMock.SetupGet(result => result.ErrorDescription).Returns(errorDescription);
        return failedLoginResultMock.Object;
    }
}