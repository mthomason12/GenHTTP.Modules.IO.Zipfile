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
IBuilder<IResourceTree> content = ResourceTree.FromZip("test.zip");

StaticWebsiteBuilder? app = StaticWebsite.From(content);

Host.Create()
    .Console()
    .Defaults()
    .Handler(app)
    .Run();

Console.WriteLine("Press any key to terminate");
Console.ReadKey();
