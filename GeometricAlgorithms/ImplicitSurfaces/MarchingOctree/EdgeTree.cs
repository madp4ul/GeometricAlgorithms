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
        readonly EdgeTreeNode Root;
        readonly SurfaceResult SurfaceResult;

        public EdgeTree(Octree octree)
        {
            if (octree.Configuration.MinimizeContainers)
            {
                throw new ArgumentException();
            }

            SurfaceResult = new SurfaceResult();

            Root = CreateNode(null, new OctreeOffset(0, 0, 0), octree.Root);

            //Do with bfs so that all nodes of higher level are created when creating a node of lower level
            Queue<EdgeTreeNode> nodeQueue = new Queue<EdgeTreeNode>();
            nodeQueue.Enqueue(Root);

            while (nodeQueue.Count > 0)
            {
                EdgeTreeNode currentNode = nodeQueue.Dequeue();

                AddChildren(currentNode, nodeQueue);
            }
        }

        private EdgeTreeNode CreateNode(EdgeTreeBranch parent, OctreeOffset parentOffset, ATreeNode octreeNode)
        {
            if (octreeNode is OctreeBranch branch)
            {
                return new EdgeTreeBranch(parent, parentOffset, branch);
            }
            else if (octreeNode is TreeLeaf leaf)
            {
                return new EdgeTreeLeaf(parent, parentOffset, leaf, SurfaceResult);
            }
            else
            {
                throw new ArgumentException("parameter octreeNode is not an octree node");
            }
        }

        /// <summary>
        /// adds the parents children on the octree to the parent in the edge tree if there are any children.
        /// It also put the children into the node queue
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="queue"></param>
        private void AddChildren(EdgeTreeNode parent, Queue<EdgeTreeNode> queue)
        {
            if (parent is EdgeTreeBranch branch)
            {
                OctreeBranch octreeBranch = branch.OctreeNode as OctreeBranch;

                for (int x = 0; x < 2; x++)
                {
                    for (int y = 0; y < 2; y++)
                    {
                        for (int z = 0; z < 2; z++)
                        {
                            var octreeChild = octreeBranch[x, y, z];

                            var childEdgeNode = CreateNode(branch, new OctreeOffset(x, y, z), octreeChild);

                            branch.Children[x, y, z] = childEdgeNode;
                            queue.Enqueue(childEdgeNode);
                        }
                    }
                }
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
