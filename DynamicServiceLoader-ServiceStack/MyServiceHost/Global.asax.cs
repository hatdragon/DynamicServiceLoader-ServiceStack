using DynamicServiceLoader;
using System;
using DynamicServiceLoader.BaseClasses;

namespace MyServiceHost
{
  public class Global : System.Web.HttpApplication
  {
    protected void Application_Start(object sender, EventArgs e)
    {
      //register all of our available services
      new MyAppHostBase().Init();
    }

  }
}