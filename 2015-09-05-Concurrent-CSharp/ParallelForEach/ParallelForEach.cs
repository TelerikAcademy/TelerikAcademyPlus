using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelForEach
{
    class ParallelForEach
    {
        public static void FlipImages(string sourceDirectoryPath, string targetDirectoryPath)
        {
            var filesInDirectory = System.IO.Directory.GetFiles(sourceDirectoryPath);

            Parallel.ForEach(filesInDirectory,
                (currentFile) =>
                {
                    string filename = System.IO.Path.GetFileName(currentFile);
                    // Warning: The Console will slow down our loop
                    Console.WriteLine("Processing {0} on thread {1}", filename,
                                        Thread.CurrentThread.ManagedThreadId);

                    System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(currentFile);

                    bitmap.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
                    bitmap.Save(System.IO.Path.Combine(targetDirectoryPath, filename));
                });
        }

        static void Main(string[] args)
        {
            FlipImages("imagesToFlip", "flippedImages");
        }
    }
}
