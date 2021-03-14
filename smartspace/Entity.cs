using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview
{
    public class Entity : IStoreable
    {
        public string Name { get; set; }
        public IComparable Id { get; set ; }
    }
}
