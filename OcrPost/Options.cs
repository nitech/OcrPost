using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace OcrPost
{
    class Options
    {
        [Option('s', "source", Required = true, HelpText = "Source folder containing source .txt-files")]
        public string SourceFolder { get; set; }

        [Option('d', "destination", Required = true, HelpText = "Destination folder for regex-ed text files")]
        public string DestinationFolder { get; set; }

        [Option('c', "config", Required = true, HelpText = "Config file that contains regexes")]
        public string Config { get; set; }

        [Option('i', "simulate", Required = false, HelpText = "Just simulate. No writing files")]
        public bool Simulate { get; set; }
    }
}
