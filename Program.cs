using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Xml.Serialization;
using CommandLine;
using concourse_rss_resource.ConcourseModel;
using concourse_rss_resource.RssModel20;

namespace concourse_rss_resource
{
    public class Options
    {
        [Option('c', "check", Required = false, HelpText = "Run an update check")]
        public bool Check { get; set; }

        [Option('i', "in", Required = false, HelpText = "Fetch the resource")]
        public bool In { get; set; }

        [Option('t', "out", Required = false, HelpText = "No-op output of the resource")]
        public bool Out { get; set; }

        [Option('o', "outDir", Required = false, HelpText = "The directory to output the resource to")]
        public string OutDir { get; set; }
    }

    internal static class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(options =>
                {
                    if (options.Check)
                    {
                        RunUpdateCheck();
                    }
                    else if (options.In)
                    {
                        FetchResource(options);
                    }
                    else if (options.Out)
                    {
                        var resourceInputModel = ReadResourceInputModelFromStdIn();
                        RssResource.OutputMetadata(resourceInputModel, null);
                    }
                });
        }

        private static void RunUpdateCheck()
        {
            var resourceInputModel = ReadResourceInputModelFromStdIn();
            new RssResource().IsUpdatedFromRemote(resourceInputModel);
        }

        private static void FetchResource(Options options)
        {
            var resourceInputModel = ReadResourceInputModelFromStdIn();
            new RssResource().FetchResourceFromRemove(resourceInputModel, options.OutDir);
        }

        private static ResourceInputModel ReadResourceInputModelFromStdIn()
        {
            var stdIn = "";
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                stdIn += line;
            }

            return JsonSerializer.Deserialize<ResourceInputModel>(stdIn);
        }
    }

    public class RssResource
    {
        public void IsUpdatedFromRemote(ResourceInputModel resourceInputModel)
        {
            using var client = new WebClient();
            var modelString = client.DownloadString(resourceInputModel.Source.Uri);

            var model = BuildFromString(modelString);

            IsUpdated(resourceInputModel, model);
        }

        private static void IsUpdated(ResourceInputModel resourceInputModel, RssModel20.RssModel20 model)
        {
            var output = new LinkedList<RssVersion>();
            for (var i = resourceInputModel.Version.Index; i <= model.Channel.Items.Count; i++)
            {
                var rssVersion = new RssVersion {Index = i};
                output.AddLast(rssVersion);
            }

            var jsonOutput = JsonSerializer.Serialize(output);
            Console.Write(jsonOutput);
            Console.Out.Flush();
        }

        public void FetchResourceFromRemove(ResourceInputModel resourceInputModel, string outDir)
        {
            using var client = new WebClient();
            var modelString = client.DownloadString(resourceInputModel.Source.Uri);

            var model = BuildFromString(modelString);

            FetchResource(resourceInputModel, outDir, model);
        }

        private void FetchResource(ResourceInputModel resourceInputModel, string outDir, RssModel20.RssModel20 model)
        {
            var index = model.Channel.Items.Count - resourceInputModel.Version.Index;

            var item = model.Channel.Items[index];

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var serializer = new XmlSerializer(typeof(RssItem20));

            var fileStream = new FileStream(Path.Join(outDir, "item.xml"), FileMode.Create);
            serializer.Serialize(fileStream, item, ns);

            OutputMetadata(resourceInputModel, item);
        }

        internal static void OutputMetadata(ResourceInputModel resourceInputModel, RssItem20 item)
        {
            var meta = item == null
                ? new List<OutputMetadata>()
                : new List<OutputMetadata> {new OutputMetadata {Title = item.Title}};

            var output = new FetchResourceOutputModel
            {
                Version = resourceInputModel.Version,
                Metadata = meta
            };

            Console.Write(JsonSerializer.Serialize(output));
            Console.Out.Flush();
        }

        private RssModel20.RssModel20 BuildFromString(string input)
        {
            var serializer = new XmlSerializer(typeof(RssModel20.RssModel20));

            using var reader = new StringReader(input);
            var model = (RssModel20.RssModel20) serializer.Deserialize(reader);

            return model;
        }
    }
}