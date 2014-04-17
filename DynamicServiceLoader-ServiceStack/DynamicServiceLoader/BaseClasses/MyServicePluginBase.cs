using System.ComponentModel.Composition;
using DynamicServiceLoader.Interfaces;
using ServiceStack.WebHost.Endpoints;

namespace DynamicServiceLoader.BaseClasses
{
  [InheritedExport(typeof(IMyServicePlugin))]
  public abstract class MyServicePluginBase : IMyServicePlugin
  {
    public abstract void Register(IAppHost appHost);

    public string Name { get; set; }
    public string Description { get; set; }
  }
}
