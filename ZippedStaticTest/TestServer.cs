using GenHTTP.Engine;
using GenHTTP.Modules.IO;
using GenHTTP.Modules.Practices;
using GenHTTP.Modules.StaticWebsites;
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
            var content = ResourceTree.FromDirectory("");

            var app = StaticWebsite.From(content);

            Host.Create()
                .Console()
                .Defaults()
                .Handler(app)
                .Run();
        }
    }
}
