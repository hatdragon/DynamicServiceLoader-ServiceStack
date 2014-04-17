using DynamicServiceLoader.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DynamicServiceLoader
{
  public class DynamicServiceLoader : IDynamicPluginLoader
  {

    static DynamicServiceLoader()
    {
      _assemblySearchPatterns = new List<string>() {"TestService*.*"};
    }

    private readonly CompositionContainer _container;
    private static readonly List<string> _assemblySearchPatterns;
    
    [ImportMany(typeof(IMyServicePlugin))]
    private IEnumerable<IMyServicePlugin> _loadedServices;

    public int AvailableNumberOfPlugins
    {
      get { return (_loadedServices != null ? _loadedServices.Count() : 0); }
    }

    public IEnumerable<IMyServicePlugin> PluginsFound
    {
      get { return _loadedServices; }
      set { _loadedServices = value; }
    }


    public void Dispose()
    {
      if (_container != null)
      {
        _container.Dispose();
      }
    }

    private string _assemblyPath;
    private string AssemblyPath
    {
      get
      {
        if (string.IsNullOrEmpty(_assemblyPath))
        {
          var pathUri = new Uri(Path.GetDirectoryName(this.GetType().Assembly.CodeBase));
          _assemblyPath = pathUri.LocalPath;
        }

        return _assemblyPath;
      }
    }


    public DynamicServiceLoader(params Assembly[] additionalAssemblies)
    {
      var catalog = new AggregateCatalog();

      foreach (var searchPattern in _assemblySearchPatterns)
      {
        catalog.Catalogs.Add(new DirectoryCatalog(AssemblyPath, searchPattern));
      }

      if (additionalAssemblies != null)
      {
        foreach (var assembly in additionalAssemblies)
        {
          catalog.Catalogs.Add(new AssemblyCatalog(assembly));
        }
      }

      _container = new CompositionContainer(catalog);
      _container.ComposeParts(this);


    }

  }
}
