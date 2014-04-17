using ServiceStack.WebHost.Endpoints;
using System.Reflection;

namespace DynamicServiceLoader
{
  public static class ServiceManager
  {
    public static void AutoRegisterServices(AppHostBase appHost)
    {
      AutoRegisterServices(appHost, null);
    }

    public static void AutoRegisterServices(AppHostBase appHost, params Assembly[] additionalAssemblies)
    {
      using (var serviceLoader = new DynamicServiceLoader(additionalAssemblies))
      {
        foreach (var servicePlugin in serviceLoader.PluginsFound)
        {
          Assembly asm = servicePlugin.GetType().Assembly;
          appHost.AddPluginsFromAssembly(asm);
        }
      }
    }

  }
}
