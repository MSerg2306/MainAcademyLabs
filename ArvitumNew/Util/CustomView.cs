using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ArvitumNew.Util
{
    public class CustomView : IView
    {
        public CustomView(string viewPath)
        {
            Path = viewPath;
        }
        public string Path { get; set; }
        public async Task RenderAsync(ViewContext context)
        {
            string content = String.Empty;
            using (FileStream viewStream = new FileStream(Path, FileMode.Open))
            {
                using (StreamReader viewReader = new StreamReader(viewStream))
                {
                    content = viewReader.ReadToEnd();
                }
            }
            await context.Writer.WriteAsync(content);
        }
    }
}
