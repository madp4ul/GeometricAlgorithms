using GeometricAlgorithms.Domain;
using GeometricAlgorithms.MeshQuerying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class EdgeTree
    {
        EdgeTreeNode Root;

        public List<Vector3> Vertices = new List<Vector3>();
        public List<Triangle> Faces = new List<Triangle>();

        public EdgeTree(Octree octree)
        {
            if (octree.Configuration.MinimizeContainers)
            {
                throw new ArgumentException();
            }

            Root = new EdgeTreeNode(null, new Point(0, 0, 0), octree.Root);

            //Do with bfs so that all nodes of higher level are created when creating a node of lower level
            Queue<EdgeTreeNode> nodeQueue = new Queue<EdgeTreeNode>();
            nodeQueue.Enqueue(Root);

            while (nodeQueue.Count > 0)
            {
                EdgeTreeNode currentNode = nodeQueue.Dequeue();

                //if cound not add children we are in a leaf and want to load or create edges
                if (!TryAddChildren(currentNode, nodeQueue))
                {

                }

                //TODO 
                //1. try load or create edges
                //2. add children
                //3. repeat with next
            }
        }

        private bool TryAddChildren(EdgeTreeNode parent, Queue<EdgeTreeNode> queue)
        {
            if (parent.OctreeNode is OctreeBranch branch)
            {
                for (int x = 0; x < 2; x++)
                {
                    for (int y = 0; y < 2; y++)
                    {
                        for (int z = 0; z < 2; z++)
                        {
                            var octreeChild = branch[x, y, z];

                            var childEdgeNode = new EdgeTreeNode(parent, new Point(x, y, z), octreeChild);

                            parent.Children[x, y, z] = childEdgeNode;
                            queue.Enqueue(childEdgeNode);
                        }
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        //private bool TryLoadEdge(EdgeTreeNode node, EdgeIndex edge)
        //{
        //    //Nicht vergessen: kanten gibt es nur in leafs

        //    //Wenn inside edge
        //    //-> prüfe nur geschwisternodes
        //    //wenn outsideedge am rand des parent
        //    //-> suche nach position in aktuellem level und kindern von allen parents. Wenn längere kante passt, wähle passenden abschnitt daraus

        //    //wenn outsideedge in mitte der außenfläche des parent
        //    //->suche in kanten auf aktuellem level nach passender position, aber nicht nur in geschwistern, sondern in allen!
        //}
    }
}
