using DynamicServiceLoader.BaseClasses;
using DynamicServiceLoader.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;
using TestService2;

namespace DynamicServiceLoader.Tests
{
  [TestClass]
  public class Service2Tests
  {
    [TestMethod]
    public void CanCreatePluginDefinition()
    {
      var service2PluginDefinition = new TestService2.PluginDefinition();
      Assert.IsInstanceOfType(service2PluginDefinition, typeof(IMyServicePlugin));
      Assert.IsInstanceOfType(service2PluginDefinition, typeof(MyServicePluginBase));

    }

    [TestMethod]
    public void CanCreatePluginDefinitionAndRegisterPlugin()
    {
      var mockAppHost = new Mock<IAppHost>();
      mockAppHost.SetupAllProperties();
      mockAppHost.SetupGet(x => x.Routes).Returns(new ServiceRoutes());


      var service2PluginDefinition = new TestService2.PluginDefinition();
      Assert.IsInstanceOfType(service2PluginDefinition, typeof(IMyServicePlugin));
      Assert.IsInstanceOfType(service2PluginDefinition, typeof(MyServicePluginBase));

      service2PluginDefinition.Register(mockAppHost.Object);

      Assert.IsNotNull(mockAppHost.Object.Routes);
      Assert.IsTrue(((ServiceStack.ServiceHost.ServiceRoutes)(mockAppHost.Object.Routes)).RestPaths.Count > 0);
    }


    [TestMethod]
    public void CanCreateService()
    {
      var service2PluginDefinition = new TestService2.Service2();
      Assert.IsInstanceOfType(service2PluginDefinition, typeof(IService));
      Assert.IsInstanceOfType(service2PluginDefinition, typeof(MyServiceBase));
    }


    [TestMethod]
    public void CanExecuteGetFromService_Add()
    {
      var service2PluginDefinition = new TestService2.Service2();

      var theRequest = new Service2Request() { Operation = "add", Num1 = 2, Num2 = 67 };

      var result = service2PluginDefinition.Get(theRequest);

      Assert.IsTrue(result.OperationResult.Equals(69));
    }

    [TestMethod]
    public void CanExecuteGetFromService_Subtract()
    {
      var service2PluginDefinition = new TestService2.Service2();

      var theRequest = new Service2Request() { Operation = "subtract", Num1 = 2, Num2 = 67 };

      var result = service2PluginDefinition.Get(theRequest);

      Assert.IsTrue(result.OperationResult.Equals(-65));
    }

    [TestMethod]
    public void CanExecuteGetFromService_Multiply()
    {
      var service2PluginDefinition = new TestService2.Service2();

      var theRequest = new Service2Request() { Operation = "multiply", Num1 = 2, Num2 = 67 };

      var result = service2PluginDefinition.Get(theRequest);

      Assert.IsTrue(result.OperationResult.Equals(134));
    }

    [TestMethod]
    public void CanExecuteGetFromService_Divide()
    {
      var service2PluginDefinition = new TestService2.Service2();

      var theRequest = new Service2Request() { Operation = "divide", Num1 = 9, Num2 = 3 };

      var result = service2PluginDefinition.Get(theRequest);

      Assert.IsTrue(result.OperationResult.Equals(3));
    }

    [TestMethod]
    public void CanExecuteGetFromService_Exception()
    {
      var service2PluginDefinition = new TestService2.Service2();

      var theRequest = new Service2Request() { Operation = "exception_case", Num1 = 9, Num2 = 3 };

      var result = service2PluginDefinition.Get(theRequest);

      Assert.IsTrue(result.ResponseStatus.ErrorCode.Equals("-1"));
      Assert.IsTrue(result.OperationResult.Equals(0));
    }


  }
}
