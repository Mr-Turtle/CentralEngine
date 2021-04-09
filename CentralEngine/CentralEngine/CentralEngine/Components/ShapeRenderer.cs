using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace CentralEngine.CentralEngine.Components
{
    public enum Shape
    {
        Rectangle
    }

    class ShapeRenderer : Component
    {
        public Shape ShapeType;
        public Color Color;

        public ShapeRenderer(Shape ShapeType, Color Color) : base("shape_renderer")
        {
            this.ShapeType = ShapeType;
            this.Color = Color;
        }

    }
}
