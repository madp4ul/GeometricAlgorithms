using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Triangulation
{
    class PositionTriangle : Triangle
    {
        public readonly Vector3 Position0;
        public readonly Vector3 Position1;
        public readonly Vector3 Position2;

        public PositionTriangle(PositionIndex index0, PositionIndex index1, PositionIndex index2)
            : base(index0.Index, index1.Index, index2.Index)
        {
            Position0 = index0.Position;
            Position1 = index1.Position;
            Position2 = index2.Position;
        }

        public float GetDistance(Vector3 point)
        {
            #region dont look

            Vector3 diff = point - Position0;
            Vector3 edge0 = Position1 - Position0;
            Vector3 edge1 = Position2 - Position0;
            float a00 = Vector3.Dot(edge0, edge0);
            float a01 = Vector3.Dot(edge0, edge1);
            float a11 = Vector3.Dot(edge1, edge1);
            float b0 = -Vector3.Dot(diff, edge0);
            float b1 = -Vector3.Dot(diff, edge1);
            float det = a00 * a11 - a01 * a01;
            float t0 = a01 * b1 - a11 * b0;
            float t1 = a01 * b0 - a00 * b1;

            if (t0 + t1 <= det)
            {
                if (t0 < 0)
                {
                    if (t1 < 0)  // region 4
                    {
                        if (b0 < 0)
                        {
                            t1 = 0;
                            if (-b0 >= a00)  // V1
                            {
                                t0 = 1;
                            }
                            else  // E01
                            {
                                t0 = -b0 / a00;
                            }
                        }
                        else
                        {
                            t0 = 0;
                            if (b1 >= 0)  // V0
                            {
                                t1 = 0;
                            }
                            else if (-b1 >= a11)  // V2
                            {
                                t1 = 1;
                            }
                            else  // E20
                            {
                                t1 = -b1 / a11;
                            }
                        }
                    }
                    else  // region 3
                    {
                        t0 = 0;
                        if (b1 >= 0)  // V0
                        {
                            t1 = 0;
                        }
                        else if (-b1 >= a11)  // V2
                        {
                            t1 = 1;
                        }
                        else  // E20
                        {
                            t1 = -b1 / a11;
                        }
                    }
                }
                else if (t1 < 0)  // region 5
                {
                    t1 = 0;
                    if (b0 >= 0)  // V0
                    {
                        t0 = 0;
                    }
                    else if (-b0 >= a00)  // V1
                    {
                        t0 = 1;
                    }
                    else  // E01
                    {
                        t0 = -b0 / a00;
                    }
                }
                else  // region 0, interior
                {
                    float invDet = 1 / det;
                    t0 *= invDet;
                    t1 *= invDet;
                }
            }
            else
            {
                float tmp0, tmp1, numer, denom;

                if (t0 < 0)  // region 2
                {
                    tmp0 = a01 + b0;
                    tmp1 = a11 + b1;
                    if (tmp1 > tmp0)
                    {
                        numer = tmp1 - tmp0;
                        denom = a00 - ((float)2) * a01 + a11;
                        if (numer >= denom)  // V1
                        {
                            t0 = 1;
                            t1 = 0;
                        }
                        else  // E12
                        {
                            t0 = numer / denom;
                            t1 = 1 - t0;
                        }
                    }
                    else
                    {
                        t0 = 0;
                        if (tmp1 <= 0)  // V2
                        {
                            t1 = 1;
                        }
                        else if (b1 >= 0)  // V0
                        {
                            t1 = 0;
                        }
                        else  // E20
                        {
                            t1 = -b1 / a11;
                        }
                    }
                }
                else if (t1 < 0)  // region 6
                {
                    tmp0 = a01 + b1;
                    tmp1 = a00 + b0;
                    if (tmp1 > tmp0)
                    {
                        numer = tmp1 - tmp0;
                        denom = a00 - ((float)2) * a01 + a11;
                        if (numer >= denom)  // V2
                        {
                            t1 = 1;
                            t0 = 0;
                        }
                        else  // E12
                        {
                            t1 = numer / denom;
                            t0 = 1 - t1;
                        }
                    }
                    else
                    {
                        t1 = 0;
                        if (tmp1 <= 0)  // V1
                        {
                            t0 = 1;
                        }
                        else if (b0 >= 0)  // V0
                        {
                            t0 = 0;
                        }
                        else  // E01
                        {
                            t0 = -b0 / a00;
                        }
                    }
                }
                else  // region 1
                {
                    numer = a11 + b1 - a01 - b0;
                    if (numer <= 0)  // V2
                    {
                        t0 = 0;
                        t1 = 1;
                    }
                    else
                    {
                        denom = a00 - ((float)2) * a01 + a11;
                        if (numer >= denom)  // V1
                        {
                            t0 = 1;
                            t1 = 0;
                        }
                        else  // 12
                        {
                            t0 = numer / denom;
                            t1 = 1 - t0;
                        }
                    }
                }
            }

            #endregion

            Vector3 closestPointOnTriangle = Position0 + t0 * edge0 + t1 * edge1;

            Vector3 shortestDiff = point - closestPointOnTriangle;
            return shortestDiff.Length;
        }
    }
}
