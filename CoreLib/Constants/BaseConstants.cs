using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace CoreLib.Constants
{
    public static class BaseConstants
    {
        public static string GetConnectionString
        {
            get
            {
                try
                {
                    string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
                    string result = $"{string.Join("\\", projectPath.Split("\\").SkipLast(2))}\\CoreLib\\";

                    IConfigurationRoot configuration = new ConfigurationBuilder()
                        .SetBasePath(result)
                        .AddJsonFile("appsettings.json")
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