using DynamicServiceLoader.BaseClasses;
using ServiceStack.WebHost.Endpoints;

namespace TestService2
{
  public class PluginDefinition : MyServicePluginBase
  {
    public PluginDefinition()
    {
      Name = "Service2";
      Description = "This is Test Service 1";
    }

    public string Name { get; set; }
    public string Description { get; set; }

    public override void Register(IAppHost appHost)
    {
      appHost.Routes.Add<Service2Request>("/service2/{Operation}/{Num1}/{Num2}", "GET");
      appHost.Routes.Add<Service2Request>("/service2", "POST");
      appHost.RegisterService(typeof(Service2), "/service2");
    }
  }

}
