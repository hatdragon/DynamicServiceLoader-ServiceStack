using DynamicServiceLoader.BaseClasses;
using ServiceStack.WebHost.Endpoints;

namespace TestService1
{
  public class PluginDefinition : MyServicePluginBase
  {
    public PluginDefinition()
    {
      Name = "Service1";
      Description = "This is Test Service 1";
    }

    public string Name { get; set; }
    public string Description { get; set; }

    public override void Register(IAppHost appHost)
    {
      appHost.Routes.Add<Service1Request>("/service1/{Text}", "GET");
      appHost.Routes.Add<Service1Request>("/service1", "POST");
      appHost.RegisterService(typeof(Service1), "/service1");
    }
  }
}
