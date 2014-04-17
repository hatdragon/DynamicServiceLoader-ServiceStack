using System;
using Funq;
using ServiceStack;
using ServiceStack.ServiceInterface;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;
using ServiceStack.WebHost.Endpoints.Formats;

namespace DynamicServiceLoader.BaseClasses
{
  public class MyAppHostBase : AppHostBase
  {
    public MyAppHostBase()
      : base("My App", typeof(MyServiceBase).Assembly)
    {
      JsConfig.EmitCamelCaseNames = true;
      JsConfig.DateHandler = JsonDateHandler.ISO8601;

      //remove the unwanted plugins, your list may vary
      Plugins.RemoveAll(x => x is AuthFeature);
      Plugins.RemoveAll(x => x is SessionFeature);
      Plugins.RemoveAll(x => x is CsvFormat);
      Plugins.RemoveAll(x => x is RequestInfoFeature);

      //set up the initial configuration
      SetConfig(
        new EndpointHostConfig
        {
          GlobalResponseHeaders =
          {
            {"Access-Control-Allow-Origin", "*"},
            {"Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS"},
            {"Access-Control-Allow-Headers", "Content-Type"},
            {"X-Powered-On", Environment.MachineName},
          },
          DefaultContentType = "application/json"
        }
      );
    }

    public override void Configure(Container container)
    {
      ServiceManager.AutoRegisterServices(this);
    }
  }
}
