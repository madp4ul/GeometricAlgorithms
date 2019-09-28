using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.FileProcessing
{
    class MeshDoublePositionRemover
    {
        public Mesh RemoveDoublePositions(Mesh mesh)
        {
            var doubles = FindDoublePositions(mesh);

            //if no doubles found the mesh is already valid
            if (doubles.Count == 0)
            {
                return mesh;
            }

            var newPositions = RemoveDoubleVectors(mesh.Positions, doubles);
            var newNormals = mesh.HasNormals ? RemoveDoubleVectors(mesh.UnitNormals, doubles) : null;

            //finds element with old index and returns its mapping or default to old index and "maps it to itself"
            int getIndexMapping(int oldIndex) => doubles.FirstOrDefault(d => d.Old == oldIndex)?.New ?? oldIndex;

            var newFaces = mesh.HasFaces
               ? mesh.Faces.Select(f => new Triangle(
                 index0: getIndexMapping(f.Index0),
                 index1: getIndexMapping(f.Index1),
                 index2: getIndexMapping(f.Index2))).ToArray()
               : null;

            return new Mesh(newPositions, newFaces, newNormals);
        }

        private List<IndexMapping> FindDoublePositions(Mesh mesh)
        {
            var doubles = mesh.Positions
                .Select((p, i) => new { position = p, index = i })//select index
                .GroupBy(a => a.position)
                .Where(a => a.Count() > 1)//filter groups to only leave those with doubles
                .SelectMany(a =>
                {
                    //map every index in groups to its first index
                    var first = a.First();
                    return a.Skip(1).Select(groupElement => new IndexMapping(groupElement.index, first.index));
                })
                .OrderBy(im => im.Old)
                .ToList();

            return doubles;
        }

        private Vector3[] RemoveDoubleVectors(IReadOnlyList<Vector3> vectors, List<IndexMapping> orderedDoubleMappings)
        {
            Vector3[] newVectors = new Vector3[vectors.Count - orderedDoubleMappings.Count];

            int mappingsIndex = 0;

            for (int i = 0; i < vectors.Count; i++)
            {
                if (orderedDoubleMappings[mappingsIndex].Old == i)
                {
                    mappingsIndex++;
                }
                else
                {
                    newVectors[i - mappingsIndex] = vectors[i];
                }
            }

            return newVectors;
        }

        private class IndexMapping
        {
            public readonly int Old;
            public readonly int New;

            public IndexMapping(int old, int @new)
            {
                Old = old;
                New = @new;
            }

            public override string ToString()
            {
                return $"{Old} -> {New}";
            }
        }
    }
}
