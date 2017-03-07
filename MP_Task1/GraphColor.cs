using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_Task1
{
    class GraphColor<T> where T : IEquatable<T>
    { 
        // Gets or sets the Vertex that is being colored.

        public Nodes<T> Vertex { get; set; }

        // Gets or sets the color of the graph node.

        public int Color { get; set; }

        
    }
}
