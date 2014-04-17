using DynamicServiceLoader.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DynamicServiceLoader.Tests
{
  [TestClass]
  public class ServiceLoaderTests
  {
    [TestMethod]
    public void CanCreateDynamicServiceLoader()
    {
      var loader = new DynamicServiceLoader(null);
      Assert.IsInstanceOfType(loader, typeof(IDynamicPluginLoader));
      Assert.IsInstanceOfType(loader, typeof(DynamicServiceLoader));
    }

    [TestMethod]
    public void DynamicServiceLoaderHasPluginsLoaded()
    {
      var loader = new DynamicServiceLoader(null);
      Assert.IsTrue(loader.AvailableNumberOfPlugins > 0);
      Assert.IsTrue(loader.PluginsFound.Any());
    }


    [TestMethod]
    public void DynamicServiceLoaderSetPluginsLoadedToNull()
    {
      var loader = new DynamicServiceLoader(null);
      loader.PluginsFound = null;

      Assert.IsTrue(loader.AvailableNumberOfPlugins.Equals(0));
      Assert.IsTrue(loader.PluginsFound == null);
    }
  }
}
