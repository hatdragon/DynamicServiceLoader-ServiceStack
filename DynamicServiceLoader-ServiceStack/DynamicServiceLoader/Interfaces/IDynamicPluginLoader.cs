using System;
using System.Collections.Generic;

namespace DynamicServiceLoader.Interfaces
{
  public interface IDynamicPluginLoader : IDisposable
  {
    int AvailableNumberOfPlugins { get; }
    IEnumerable<IMyServicePlugin> PluginsFound { get; set; }
  }


}
