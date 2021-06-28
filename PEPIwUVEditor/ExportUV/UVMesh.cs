﻿using PEPlugin.Pmx;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace IwUVEditor.ExportUV
{
    class UVMesh
    {
        public IEnumerable<UVEdge> Mesh { get; }
        public Point MinBound { get; }
        public Point MaxBound { get; }

        public UVMesh(IList<IPXVertex> vertices, IList<IPXFace> faces)
        {
            Mesh = faces.AsParallel().SelectMany(UVEdge.FromFace);
            (MinBound, MaxBound) = CalcUVRange(Mesh);
        }

        private (Point Min, Point Max) CalcUVRange(IEnumerable<UVEdge> mesh)
        {
            var uvBound = mesh.Aggregate((Min: new PointF(float.MaxValue, float.MaxValue), Max: new PointF(float.MinValue, float.MinValue)), (acm, elm) => (
                    new PointF(Math.Min(elm.UV.Min(p => p.X), acm.Min.X), Math.Min(elm.UV.Min(p => p.Y), acm.Min.Y)),
                    new PointF(Math.Max(elm.UV.Max(p => p.X), acm.Max.X), Math.Max(elm.UV.Max(p => p.Y), acm.Max.Y))
                ));

            int Round(float value) => (int)(value < 0 ? Math.Floor(value) : Math.Ceiling(value));
            var uvRange = (new Point(Round(uvBound.Min.X), Round(uvBound.Min.Y)), new Point(Round(uvBound.Max.X), Round(uvBound.Max.Y)));
            return uvRange;
        }
    }
}
