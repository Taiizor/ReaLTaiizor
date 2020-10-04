#region Imports

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Extension.Metro
{
    #region MethodsExtension

    internal class Methods
    {
        public void DrawImageFromBase64(Graphics graphics, string base64Image, Rectangle rect)
        {
            Image im;
            using (var ms = new System.IO.MemoryStream(Convert.FromBase64String(base64Image)))
            {
                im = Image.FromStream(ms);
                ms.Close();
            }
            graphics.DrawImage(im, rect);
        }

        public void DrawImageWithColor(Graphics G, Rectangle r, Image image, Color c)
        {
            var ptsArray = new[]
            {
                new[] {Convert.ToSingle(c.R / 255.0), 0f, 0f, 0f, 0f},
                new[] {0f, Convert.ToSingle(c.G / 255.0), 0f, 0f, 0f},
                new[] {0f, 0f, Convert.ToSingle(c.B / 255.0), 0f, 0f},
                new[] {0f, 0f, 0f, Convert.ToSingle(c.A / 255.0), 0f},
                new[]
                {
                    Convert.ToSingle( c.R/255.0),
                    Convert.ToSingle( c.G/255.0),
                    Convert.ToSingle( c.B/255.0), 0f,
                    Convert.ToSingle( c.A/255.0)
                }
            };
            var imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(new ColorMatrix(ptsArray), ColorMatrixFlag.Default, ColorAdjustType.Default);
            G.DrawImage(image, r, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
            image.Dispose();
        }

        public void DrawImageWithColor(Graphics G, Rectangle r, string image, Color c)
        {
            var im = ImageFromBase64(image);
            var ptsArray = new[]
            {
                new[] {Convert.ToSingle(c.R / 255.0), 0f, 0f, 0f, 0f},
                new[] {0f, Convert.ToSingle(c.G / 255.0), 0f, 0f, 0f},
                new[] {0f, 0f, Convert.ToSingle(c.B / 255.0), 0f, 0f},
                new[] {0f, 0f, 0f, Convert.ToSingle(c.A / 255.0), 0f},
                new[]
                {
                    Convert.ToSingle( c.R/255.0),
                    Convert.ToSingle( c.G/255.0),
                    Convert.ToSingle( c.B/255.0), 0f,
                    Convert.ToSingle( c.A/255.0)
                }
            };
            var imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(new ColorMatrix(ptsArray), ColorMatrixFlag.Default, ColorAdjustType.Default);
            G.DrawImage(im, r, 0, 0, im.Width, im.Height, GraphicsUnit.Pixel, imageAttributes);
        }

        public StringFormat SetPosition(StringAlignment horizontal = StringAlignment.Center, StringAlignment vertical = StringAlignment.Center)
        {
            return new StringFormat
            {
                Alignment = horizontal,
                LineAlignment = vertical
            };
        }

        public float[][] ColorToMatrix(float alpha, Color c)
        {
            return new[]
            {
                new [] {Convert.ToSingle(c.R / 255),0,0,0,0},
                new [] {0,Convert.ToSingle(c.G / 255),0,0,0},
                new [] {0,0,Convert.ToSingle(c.B / 255),0,0},
                new [] {0,0,0,Convert.ToSingle(c.A / 255),0},
                new []
                {
                    Convert.ToSingle(c.R / 255),
                    Convert.ToSingle(c.G / 255),
                    Convert.ToSingle(c.B / 255),
                    alpha,
                    Convert.ToSingle(c.A / 255)
                }
            };
        }

        public void DrawImageWithTransparency(Graphics G, float alpha, Image image, Rectangle rect)
        {
            var colorMatrix = new ColorMatrix { Matrix33 = alpha };
            var imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);
            G.DrawImage(image, new Rectangle(rect.X, rect.Y, image.Width, image.Height), rect.X, rect.Y, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
            imageAttributes.Dispose();
        }

        public Image ImageFromBase64(string base64Image)
        {
            using (var ms = new System.IO.MemoryStream(Convert.FromBase64String(base64Image)))
                return Image.FromStream(ms);
        }

        public GraphicsPath RoundRec(Rectangle r, int curve, bool topLeft = true, bool topRight = true, bool bottomLeft = true, bool bottomRight = true)
        {
            var createRoundPath = new GraphicsPath(FillMode.Winding);
            if (topLeft)
                createRoundPath.AddArc(r.X, r.Y, curve, curve, 180f, 90f);
            else
                createRoundPath.AddLine(r.X, r.Y, r.X, r.Y);
            if (topRight)
                createRoundPath.AddArc(r.Right - curve, r.Y, curve, curve, 270f, 90f);
            else
                createRoundPath.AddLine(r.Right - r.Width, r.Y, r.Width, r.Y);
            if (bottomRight)
                createRoundPath.AddArc(r.Right - curve, r.Bottom - curve, curve, curve, 0f, 90f);
            else
                createRoundPath.AddLine(r.Right, r.Bottom, r.Right, r.Bottom);
            if (bottomLeft)
                createRoundPath.AddArc(r.X, r.Bottom - curve, curve, curve, 90f, 90f);
            else
                createRoundPath.AddLine(r.X, r.Bottom, r.X, r.Bottom);
            createRoundPath.CloseFigure();
            return createRoundPath;
        }

        public GraphicsPath RoundRec(int x, int y, int width, int height, int curve, bool topLeft = true, bool topRight = true, bool bottomLeft = true, bool bottomRight = true)
        {
            var r = new Rectangle(x, y, width, height);
            var createRoundPath = new GraphicsPath(FillMode.Winding);
            if (topLeft)
                createRoundPath.AddArc(r.X, r.Y, curve, curve, 180f, 90f);
            else
                createRoundPath.AddLine(r.X, r.Y, r.X, r.Y);
            if (topRight)
                createRoundPath.AddArc(r.Right - curve, r.Y, curve, curve, 270f, 90f);
            else
                createRoundPath.AddLine(r.Right - r.Width, r.Y, r.Width, r.Y);
            if (bottomRight)
                createRoundPath.AddArc(r.Right - curve, r.Bottom - curve, curve, curve, 0f, 90f);
            else
                createRoundPath.AddLine(r.Right, r.Bottom, r.Right, r.Bottom);
            if (bottomLeft)
                createRoundPath.AddArc(r.X, r.Bottom - curve, curve, curve, 90f, 90f);
            else
                createRoundPath.AddLine(r.X, r.Bottom, r.X, r.Bottom);
            createRoundPath.CloseFigure();
            return createRoundPath;
        }
    }

    #endregion
}