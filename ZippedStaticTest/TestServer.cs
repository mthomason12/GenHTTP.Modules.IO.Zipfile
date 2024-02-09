using GenHTTP.Api.Content.IO;
using GenHTTP.Api.Infrastructure;
using GenHTTP.Engine;
using GenHTTP.Modules.IO.Zipfile;
using GenHTTP.Modules.Practices;
using GenHTTP.Modules.StaticWebsites;
using GenHTTP.Modules.StaticWebsites.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZippedStaticTest
{
    public class TestServer
    {
        TestServer() 
        {
            IBuilder<IResourceTree> content = ResourceTree.FromZip("test.zip");

            StaticWebsiteBuilder? app = StaticWebsite.From(content);

            Host.Create()
                .Console()
                .Defaults()
                .Handler(app)
                .Run();
        }
    }
}
