using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MP_Task1
{
    class Nodes<T>where T : IEquatable<T>
    {
     

           
            public Nodes() : this(default(T)) { }

          
            public Nodes(T data)
            {
                Data = data;
                Neighbors = new Collection<Nodes<T>>();
            }

          
           
            public T Data { get; set; }
         
            public ICollection<Nodes<T>> Neighbors { get; private set; }

           
    }
}
