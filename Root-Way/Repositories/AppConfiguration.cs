using System.IO;
using Microsoft.Extensions.Configuration;

namespace Root_Way.Repositories;

public static class AppConfiguration
{

    private static IConfigurationRoot _configuration;
    
    public static string DirProject()
    {
        string DirDebug = System.IO.Directory.GetCurrentDirectory();
        string DirProject = DirDebug;

        for (int counter_slash = 0; counter_slash < 4; counter_slash++)
        {
            DirProject = DirProject.Substring(0, DirProject.LastIndexOf(@"/"));
        }

        return DirProject;
    }

    static AppConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile(DirProject() + "/Root-Way/appsettings.json", optional: false, reloadOnChange: true);

        _configuration = builder.Build();
    }

    public static string GetValue(string key)
    {
        return _configuration[key];
    }
    
    
}