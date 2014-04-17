
using ServiceStack.WebHost.Endpoints;

namespace DynamicServiceLoader.Interfaces
{
  public interface IMyServicePlugin : IPlugin
  {
    string Name { get; set; }
    string Description { get; set; }
  }
}
