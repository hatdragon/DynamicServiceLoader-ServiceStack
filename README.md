DynamicServiceLoader-ServiceStack
=================================

Used to add new services into your stack with minimal configuration needed on your website.

Using 
<a href="http://msdn.microsoft.com/en-us/library/dd460648(v=vs.110).aspx">Managed Extensibility Framework (MEF)</a>,
I created a plugin system for use with the <a href="https://github.com/ServiceStackV3/ServiceStackV3">ServiceStack</a>    model.

Basically, we'll be creating an assebmly per service.  This assembly, properly decorated, will be able to drop into the bin directory of your service and picked up, ready to go.

So, assuming that you already have ServiceStack up and running, you may have to make a few modifications to get into the same type of format, but nothing should be too difficult to follow.


So, to get started, I've got a couple of base classes: 

```
  public class MyAppHostBase : AppHostBase
  {
    public MyAppHostBase() : base("My App", typeof(MyServiceBase).Assembly)
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
```

```
  [InheritedExport(typeof(MyServiceBase))]
  public class MyServiceBase : IService
  {
  }
```


```
  [InheritedExport(typeof(IMyServicePlugin))]
  public abstract class MyServicePluginBase : IMyServicePlugin
  {
    public abstract void Register(IAppHost appHost);

    public string Name { get; set; }
    public string Description { get; set; }
  }
```  
  
  
Using these classes for the basis of our services, we are able to find all of these new items via our DynamicServiceLoader via the MEF exposed attributes.

Our DynamicLoaderService is going to load up all the assemblies matching our assemblySearchPattern, in the case of this example code, anything matching `"TestService*.*"`

Once it has the list of assemblies matching this pattern, it will attempt to find any items in the assemblies that match our `IMyServicePlugin` interface.   For every matched plugin, we'll later loop through them and register them with ServiceStack's AppHost for our solution.  This is done in our `ServiceManager` class.


```
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
```


That's pretty much it.  Take a look at the TestService classes for examples of how to make this work. 

