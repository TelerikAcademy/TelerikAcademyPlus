using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelForImageNegative
{
    class Program
    {
        static void MakeImagesNegative(string sourceDirectoryPath, string targetDirectoryPath)
        {
            string[] filenames = System.IO.Directory.GetFiles(sourceDirectoryPath);

            foreach (var filename in filenames)
            {
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(filename);
                Parallel.For(0, bitmap.Height, (bitmapRowIndex) =>
                {
                    lock (bitmap)
                    {
                        for (int bitmapColIndex = 0; bitmapColIndex < bitmap.Width; bitmapColIndex++)
                        {

                            var pixel = bitmap.GetPixel(bitmapColIndex, bitmapRowIndex);
                            var negativePixel = System.Drawing.Color.FromArgb(
                                byte.MaxValue - pixel.A,
                                byte.MaxValue - pixel.R,
                                byte.MaxValue - pixel.G,
                                byte.MaxValue - pixel.B);

                            bitmap.SetPixel(bitmapColIndex, bitmapRowIndex, negativePixel);
                        }
                    }
                });

                bitmap.Save(filename.Replace(sourceDirectoryPath, targetDirectoryPath));
            }
        }

        static void Main(string[] args)
        {
            MakeImagesNegative("sourceImages", "resultImages");
        }
    }
}
