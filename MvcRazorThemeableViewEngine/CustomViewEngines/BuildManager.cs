using System;
using System.Web.Compilation;

namespace MvcRazorThemeableViewEngine.CustomViewEngines
{
    public interface IBuildManager
    {
        bool FileExists(string virtualPath);

        Type GetCompiledType(string virtualPath);
    }

    public class BuildManagerWrapper : IBuildManager
    {
        public bool FileExists(string virtualPath)
        {
            return BuildManager.GetObjectFactory(virtualPath, false) != null;
        }

        public Type GetCompiledType(string virtualPath)
        {
            return BuildManager.GetCompiledType(virtualPath);
        }
    }
}