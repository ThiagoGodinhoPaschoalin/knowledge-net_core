using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Helper.BaseContext.Constants
{
    public static class BaseConstants
    {
        internal static string GetConnectionString
        {
            get
            {
                try
                {
                    string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
                    string result = $"{string.Join("\\", projectPath.Split("\\").SkipLast(2))}\\Helper.BaseContext\\";

                    IConfigurationRoot configuration = new ConfigurationBuilder()
                        .SetBasePath(result)
                        .AddJsonFile("appsettings.Development.json")
                        .Build();

                    return configuration.GetConnectionString("DefaultConnection");
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}