using System.ComponentModel.Composition;
using ServiceStack.ServiceHost;

namespace DynamicServiceLoader.BaseClasses
{
  [InheritedExport(typeof(MyServiceBase))]
  public class MyServiceBase : IService
  {
  }

}
