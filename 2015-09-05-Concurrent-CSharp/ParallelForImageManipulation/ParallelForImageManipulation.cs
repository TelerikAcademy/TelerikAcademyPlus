using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelForImageManipulation
{
    class ParallelForImageManipulation
    {
        public enum RadialEffectDirection
        {
            IncreaseTowardsCenter,
            IncreaseTowardsBorder
        }

        public static Color Blend(Color target, Color modifier, double modifierWeight)
        {
            return Color.FromArgb(
                //Note: For a detailed explanation on the usage of sqrt and pow, see https://www.youtube.com/watch?v=LKnqECcg6Gw. 
                //Also, the sqrt and pow should actually be 1.8 or 2.4 depending on the platform, but that's out of the scope of the demo. 
                //I've left the variant without the sqrt and pow, so you can uncomment them and compare (try with contrasting colors, e.g. red and green)
                //For the rest of the formula: http://stackoverflow.com/questions/3722307/is-there-an-easy-way-to-blend-two-system-drawing-color-values
                (int)Math.Sqrt(Math.Pow(target.R, 2) * (1 - modifierWeight) + Math.Pow(modifier.R, 2) * modifierWeight),
                (int)Math.Sqrt(Math.Pow(target.G, 2) * (1 - modifierWeight) + Math.Pow(modifier.G, 2) * modifierWeight),
                (int)Math.Sqrt(Math.Pow(target.B, 2) * (1 - modifierWeight) + Math.Pow(modifier.B, 2) * modifierWeight)
                //(int)(target.R * (1 - modifierWeight) + modifier.R * modifierWeight),
                //(int)(target.G * (1 - modifierWeight) + modifier.G * modifierWeight),
                //(int)(target.B * (1 - modifierWeight) + modifier.B * modifierWeight)
                );
        }

        public static Color GetRadiallyBlurred(int row, int col, Color sourceColor, 
            int centerRow, int centerCol, Color blurColor, 
            double maxRadius, RadialEffectDirection direction)
        {
            int xDist = centerRow - row,
                yDist = centerCol - col;
            double dist = Math.Sqrt(xDist * xDist + yDist * yDist);

            double effectWeight;
            if(direction == RadialEffectDirection.IncreaseTowardsBorder)
            {
                effectWeight = Math.Min(dist, maxRadius) / maxRadius;
            } else
            {
                effectWeight = 1 - (Math.Min(dist, maxRadius) / maxRadius);
            }

            return Blend(sourceColor, blurColor, effectWeight);
        }

        public static Color[,] GetColorMap(Bitmap bitmap)
        {
            Color[,] colorMap = new Color[bitmap.Height, bitmap.Width];

            for (int row = 0; row < bitmap.Height; row++)
            {
                for (int col = 0; col < bitmap.Width; col++)
                {
                    colorMap[row, col] = bitmap.GetPixel(col, row);
                }
            }

            return colorMap;
        }

        public static void UpdateBitmap(Bitmap targetBitmap, Color[,] colorMap)
        {
            for (int row = 0; row < targetBitmap.Height; row++)
            {
                for (int col = 0; col < targetBitmap.Width; col++)
                {
                    targetBitmap.SetPixel(col, row, colorMap[row, col]);
                }
            }
        }

        public static void ManipulateImages(string sourceDirectoryPath, string targetDirectoryPath)
        {
            var filenamesInDirectory = System.IO.Directory.GetFiles(sourceDirectoryPath);

            foreach(var filename in filenamesInDirectory)
            {
                Bitmap bitmap = new Bitmap(filename);

                //Note: Bitmap does not allow multi-thread access, so we store our wokring vars separately and will flush them back to the bitmap once we're done
                Color[,] colorMap = GetColorMap(bitmap);
                int bitmapWidth = bitmap.Width;
                int bitmapHeight = bitmap.Height;
                DateTime before = DateTime.Now;
                //for (int rowIndex = 0; rowIndex < bitmapHeight; rowIndex++)
                Parallel.For(0, bitmapHeight, (rowIndex) =>
                {
                    for(int colIndex = 0; colIndex < bitmapWidth; colIndex++)
                    {
                        colorMap[rowIndex, colIndex] = GetRadiallyBlurred(
                            rowIndex, colIndex, colorMap[rowIndex, colIndex], 
                            bitmapHeight / 2, bitmapWidth / 2,  Color.Red,
                            Math.Max(bitmapWidth, bitmapHeight) / 2, RadialEffectDirection.IncreaseTowardsBorder);
                    }
                });
                Console.WriteLine(DateTime.Now - before);
                Console.WriteLine(filename.Replace(sourceDirectoryPath, targetDirectoryPath));
                UpdateBitmap(bitmap, colorMap);
                bitmap.Save(filename.Replace(sourceDirectoryPath, targetDirectoryPath));
            }
        }

        static void Main(string[] args)
        {
            ManipulateImages("sourceImages", "targetImages");
        }
    }
}
