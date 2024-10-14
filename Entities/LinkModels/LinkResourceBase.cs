using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.LinkModels
{
    //za HATEOAS
    //klasa koja predstavlja kolekciju svih nasih linkova
    public class LinkResourceBase
    {
        public LinkResourceBase() { }
        public List<Link> Links { get; set; } = new List<Link>();
    }
}
