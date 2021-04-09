using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Numerics;

namespace CentralEngine.CentralEngine.Components
{
    class ImageRenderer : Component
    {
        public Image Image;
        public string ImagePath;
        public Vector2 ImageScale;

        public ImageRenderer(string ImagePath, Vector2 ImageScale) : base("image_renderer")
        {
            this.ImagePath = ImagePath;
            this.ImageScale = ImageScale;

            Bitmap temp = new Bitmap(this.ImagePath);
            this.Image = new Bitmap(temp, (int)ImageScale.X, (int)ImageScale.Y);
        }

    }
}
