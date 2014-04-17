using DynamicServiceLoader.BaseClasses;
using DynamicServiceLoader.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;
using TestService1;

namespace DynamicServiceLoader.Tests
{
  [TestClass]
  public class Service1Tests
  {
    [TestMethod]
    public void CanCreatePluginDefinition()
    {
      var service1PluginDefinition = new TestService1.PluginDefinition();
      Assert.IsInstanceOfType(service1PluginDefinition, typeof(IMyServicePlugin));
      Assert.IsInstanceOfType(service1PluginDefinition, typeof(MyServicePluginBase));

    }

    [TestMethod]
    public void CanCreatePluginDefinitionAndRegisterPlugin()
    {
      var mockAppHost = new Mock<IAppHost>();
      mockAppHost.SetupAllProperties();
      mockAppHost.SetupGet(x => x.Routes).Returns(new ServiceRoutes());


      var service1PluginDefinition = new TestService1.PluginDefinition();
      Assert.IsInstanceOfType(service1PluginDefinition, typeof(IMyServicePlugin));
      Assert.IsInstanceOfType(service1PluginDefinition, typeof(MyServicePluginBase));

      service1PluginDefinition.Register(mockAppHost.Object);

      Assert.IsNotNull(mockAppHost.Object.Routes);
      Assert.IsTrue(((ServiceStack.ServiceHost.ServiceRoutes)(mockAppHost.Object.Routes)).RestPaths.Count > 0);
    }

    [TestMethod]
    public void CanCreateService()
    {
      var service1PluginDefinition = new TestService1.Service1();
      Assert.IsInstanceOfType(service1PluginDefinition, typeof(IService));
      Assert.IsInstanceOfType(service1PluginDefinition, typeof(MyServiceBase));
    }

    [TestMethod]
    public void CanExecuteGetFromService_Greeting()
    {
      const string GREETING_STRING = "Hello, Thanks for your message: ";

      var service2PluginDefinition = new TestService1.Service1();

      var theRequest = new Service1Request() { Text = "I'm Bob." };

      var result = service2PluginDefinition.Get(theRequest);

      Assert.IsTrue(result.Greeting.Equals(string.Format("{0}{1}", GREETING_STRING, theRequest.Text)));
    }
  }
}
