using System;
using System.Collections.Generic;
using System.IO;

namespace OcrPost
{
    public static class Replaces
    {
        public static List<Config> GetReplaceConfigs(string config)
        {
            var configs = new List<Config>() {};

            // get file
            var lines = File.ReadLines(config);

            // iterate lines, create config objects
            string find = null;
            string replace = null;
            foreach (var line in lines)
            {
                if (line.StartsWith("find:", StringComparison.OrdinalIgnoreCase))
                {
                    find = line.Substring(5);
                }

                if (line.StartsWith("replace:", StringComparison.OrdinalIgnoreCase))
                {
                    replace = line.Substring(8);
                }

                if (replace != null)
                {
                    configs.Add(new Config()
                    {
                        Find = find,
                        Replace = replace
                    });

                    find = null;
                    replace = null; 
                }
            }

            return configs; 
        }

    }

    public class Config
    {
        public string Find { get; set; }
        public string Replace { get; set; }
    }

    public class Replace
    {
        public string Find { get; set; }
        public string ReplaceWith { get; set; }
    }
}