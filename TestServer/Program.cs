using GenHTTP.Api.Content.IO;
using GenHTTP.Api.Infrastructure;
using GenHTTP.Engine;
using GenHTTP.Modules.IO.Zipfile;
using GenHTTP.Modules.Practices;
using GenHTTP.Modules.StaticWebsites;
using GenHTTP.Modules.StaticWebsites.Provider;
using System.IO.Compression;

//create zipfile of Test Site
if (File.Exists("testsite.zip"))
{
    File.Delete("testsite.zip");
}
ZipFile.CreateFromDirectory("TestSite", "testsite.zip");

//load resource tree from zip
IBuilder<IResourceTree> content = ResourceTree.FromZip("testsite.zip");

StaticWebsiteBuilder? app = StaticWebsite.From(content);

IServerHost host = Host.Create()
    .Console()
    .Defaults()
    .Handler(app);
Console.WriteLine("Running on port 8080");
Console.WriteLine("Ctrl-C to terminate.");
host.Run();
