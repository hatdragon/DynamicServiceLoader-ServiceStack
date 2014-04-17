using DynamicServiceLoader.BaseClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DynamicServiceLoader.Tests
{
  [TestClass]
  public class ServiceManagerTests
  {
    [TestMethod]
    public void CanExecuteServiceManagerAutoRegisterTask()
    {
      var theAppHost = new MyAppHostBase();


      ServiceManager.AutoRegisterServices(theAppHost);

      Assert.IsNotNull(theAppHost.Plugins);
      Assert.IsTrue(theAppHost.Plugins.Select(x => x.GetType() == typeof(MyServicePluginBase)).Any());
    }
  }
}
