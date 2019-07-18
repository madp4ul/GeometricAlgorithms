using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.PointRendering
{
    public struct VertexPositionIndex : IVertexType
    {
        public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(
            new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
            new VertexElement(12, VertexElementFormat.Byte4, VertexElementUsage.TextureCoordinate, 0));

        public Vector3 Position;
        public Corner Corner;

        public VertexPositionIndex(Vector3 position, Corner corner)
        {
            Position = position;
            Corner = corner;
        }

        VertexDeclaration IVertexType.VertexDeclaration => VertexDeclaration;
        public override string ToString()
        {
            return $"Pos: {Position} Corner: {Corner.ToString()}";
        }
    }

    public enum Corner : int
    {
        TopLeft = 0,
        TopRight = 1,
        BottomRight = 2,
        BottomLeft = 3
    }
}
