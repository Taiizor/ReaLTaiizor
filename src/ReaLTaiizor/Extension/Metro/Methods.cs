#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

#endregion

namespace ReaLTaiizor.Extension.Metro
{
    #region MethodsExtension

    internal class Methods
    {
        public static void DrawImageFromBase64(Graphics graphics, string base64Image, Rectangle rect)
        {
            Image im;
            using (System.IO.MemoryStream ms = new(Convert.FromBase64String(base64Image)))
            {
                im = Image.FromStream(ms);
                ms.Close();
            }
            graphics.DrawImage(im, rect);
        }

        public static void DrawImageWithColor(Graphics G, Rectangle r, Image image, Color c)
        {
            float[][] ptsArray = new[]
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
            ImageAttributes imageAttributes = new();
            imageAttributes.SetColorMatrix(new ColorMatrix(ptsArray), ColorMatrixFlag.Default, ColorAdjustType.Default);
            G.DrawImage(image, r, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
            image.Dispose();
        }

        public static void DrawImageWithColor(Graphics G, Rectangle r, string image, Color c)
        {
            Image im = ImageFromBase64(image);
            float[][] ptsArray = new[]
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
            ImageAttributes imageAttributes = new();
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

        public static float[][] ColorToMatrix(float alpha, Color c)
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
            ColorMatrix colorMatrix = new() { Matrix33 = alpha };
            ImageAttributes imageAttributes = new();
            imageAttributes.SetColorMatrix(colorMatrix);
            G.DrawImage(image, new Rectangle(rect.X, rect.Y, image.Width, image.Height), rect.X, rect.Y, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
            imageAttributes.Dispose();
        }

        public static Image ImageFromBase64(string base64Image)
        {
            using System.IO.MemoryStream ms = new(Convert.FromBase64String(base64Image));
            return Image.FromStream(ms);
        }

        public static GraphicsPath RoundRec(Rectangle r, int curve, bool topLeft = true, bool topRight = true, bool bottomLeft = true, bool bottomRight = true)
        {
            GraphicsPath createRoundPath = new(FillMode.Winding);
            if (topLeft)
            {
                createRoundPath.AddArc(r.X, r.Y, curve, curve, 180f, 90f);
            }
            else
            {
                createRoundPath.AddLine(r.X, r.Y, r.X, r.Y);
            }

            if (topRight)
            {
                createRoundPath.AddArc(r.Right - curve, r.Y, curve, curve, 270f, 90f);
            }
            else
            {
                createRoundPath.AddLine(r.Right - r.Width, r.Y, r.Width, r.Y);
            }

            if (bottomRight)
            {
                createRoundPath.AddArc(r.Right - curve, r.Bottom - curve, curve, curve, 0f, 90f);
            }
            else
            {
                createRoundPath.AddLine(r.Right, r.Bottom, r.Right, r.Bottom);
            }

            if (bottomLeft)
            {
                createRoundPath.AddArc(r.X, r.Bottom - curve, curve, curve, 90f, 90f);
            }
            else
            {
                createRoundPath.AddLine(r.X, r.Bottom, r.X, r.Bottom);
            }

            createRoundPath.CloseFigure();
            return createRoundPath;
        }

        public static GraphicsPath RoundRec(int x, int y, int width, int height, int curve, bool topLeft = true, bool topRight = true, bool bottomLeft = true, bool bottomRight = true)
        {
            Rectangle r = new(x, y, width, height);
            GraphicsPath createRoundPath = new(FillMode.Winding);
            if (topLeft)
            {
                createRoundPath.AddArc(r.X, r.Y, curve, curve, 180f, 90f);
            }
            else
            {
                createRoundPath.AddLine(r.X, r.Y, r.X, r.Y);
            }

            if (topRight)
            {
                createRoundPath.AddArc(r.Right - curve, r.Y, curve, curve, 270f, 90f);
            }
            else
            {
                createRoundPath.AddLine(r.Right - r.Width, r.Y, r.Width, r.Y);
            }

            if (bottomRight)
            {
                createRoundPath.AddArc(r.Right - curve, r.Bottom - curve, curve, curve, 0f, 90f);
            }
            else
            {
                createRoundPath.AddLine(r.Right, r.Bottom, r.Right, r.Bottom);
            }

            if (bottomLeft)
            {
                createRoundPath.AddArc(r.X, r.Bottom - curve, curve, curve, 90f, 90f);
            }
            else
            {
                createRoundPath.AddLine(r.X, r.Bottom, r.X, r.Bottom);
            }

            createRoundPath.CloseFigure();
            return createRoundPath;
        }
    }

    #endregion
}