using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
 
namespace MP_Task1
{
    static class Methods
    {

        // Searches the NodeList for a Node containing a particular value.
        
        public static Nodes<T> FindByValue<T>(this ICollection<Nodes<T>> collection, T value)
            where T : IEquatable<T>
        {
            return collection.FirstOrDefault(n => n.Data.Equals(value));
        }
 
        //Colors a graph using the Recursive-Largest-First algorithm.
        public static IList<GraphColor<T>> Color<T>(this Graph<T> graph) where T : IEquatable<T>
        {
            IList<GraphColor<T>> nodeSet = new List<GraphColor<T>>();

            int colorNumber = 1; //number of used colors

            int numberOfColoredNodes = 0;

            while (numberOfColoredNodes < graph.Count)
            {
                var max = -1;
                var index = -1;

                for (int i = 0; i < graph.Count; i++)
                {
                    if (!Colored(graph.Nodes[i], nodeSet))
                    {
                        var d = SaturatedDegree(graph.Nodes[i], nodeSet);
                        if (d > max)
                        {
                            max = d;
                            index = i;
                        }

                        else if (d == max)
                        {
                            if (Degree(graph.Nodes[i]) > Degree(graph.Nodes[index]))
                            {
                                index = i;
                            }
                        }
                    }
                }

                AssignColor(graph.Nodes[index], nodeSet, ref colorNumber);
                numberOfColoredNodes++;

            }


            return nodeSet;
        }

      
        // Assign a color to an uncolored node.
        private static void AssignColor<T>(Nodes<T> graphNode, IList<GraphColor<T>> nodeSet, ref int colorNumber) where T : IEquatable<T>
        {
            var colors = nodeSet.Where(n => graphNode.Neighbors.Contains(n.Vertex)).GroupBy(n => n.Color).Select(g => g.Key).ToList();

            if (colors.Count == colorNumber) //all colors are used
            {
                colorNumber++;
                nodeSet.Add(new GraphColor<T> { Vertex = graphNode, Color = colorNumber });
            }
            else //there is an unused color
            {
                var usedColors = Enumerable.Range(1, colorNumber);
                var colorNum = usedColors.Where(c => !colors.Contains(c)).OrderByDescending(c => nodeSet.Count(n => n.Color == c)).First();
                nodeSet.Add(new GraphColor<T> { Vertex = graphNode, Color = colorNum });
            }
        }

        // Finds the number of adjacent differently colored nodes.
       
        private static int SaturatedDegree<T>(Nodes<T> graphNode, IEnumerable<GraphColor<T>> nodeSet) where T : IEquatable<T>
        {
            return nodeSet.Where(n => graphNode.Neighbors.Contains(n.Vertex)).GroupBy(n => n.Color).Count();
        }

        // Determine wether this node has been colored.

        private static bool Colored<T>(Nodes<T> graphNode, IEnumerable<GraphColor<T>> nodeSet) where T : IEquatable<T>
        {
            return nodeSet.Any(n => n.Vertex.Data.Equals(graphNode.Data));
        }


        // Finds the number of neighbors for a specific node.

        private static int Degree<T>(Nodes<T> vertex) where T : IEquatable<T>
        {
            return vertex.Neighbors.Count;
        }

        
    }
}
