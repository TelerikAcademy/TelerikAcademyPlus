using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionsInParallelForeachHandling
{
    class FileFlipFailedException : Exception
    {
        public FileFlipFailedException(string failedFilepath, Exception reason)
            : base("Failed to process " + failedFilepath + ". See innerException for details", reason)
        {
            this.FailedFilepath = failedFilepath;
        }

        public string FailedFilepath { get; private set; }
    }

    class ExceptionsInParallelLoopsHandling
    {
        private static void FlipImage(string filepath, string targetDirectoryPath)
        {
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(filepath);
            
            bitmap.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

            bitmap.Save(System.IO.Path.Combine(targetDirectoryPath, System.IO.Path.GetFileName(filepath)));
        }

        public static void FlipImages(List<String> filepathsToFlip, string targetDirectoryPath)
        {
            ConcurrentQueue<FileFlipFailedException> exceptions = new ConcurrentQueue<FileFlipFailedException>();
            Parallel.ForEach(filepathsToFlip,
                (filepath) =>
                {
                    try
                    {
                        FlipImage(filepath, targetDirectoryPath);
                    }
                    //Note: catching all exceptions like this is not a good practice in production code. A better approach is to handle specific types of 
                    //exceptions (i.e. file not found, GDI exception, etc.) and handle them separately. However, for the purposes of this demo this should 
                    //be enough, to avoid more complicated code
                    catch (Exception e)
                    {
                        //Instead of failing all (remaining) flips just because one of them failed, just "log" the exception so higher-level code can 
                        //decide what to do with failed files (e.g. reprocess them)
                        exceptions.Enqueue(new FileFlipFailedException(filepath, e));
                    }
                });

            if (!exceptions.IsEmpty)
            {
                throw new AggregateException("One or more files failed processing", exceptions);
            }
        }

        private static void FlipImagesWithOptionalRetry(List<string> imageFilepaths, string targetDirectoryPath)
        {
            while (imageFilepaths.Count > 0)
            {
                try
                {
                    FlipImages(imageFilepaths, targetDirectoryPath);
                    imageFilepaths.Clear();
                }
                catch (AggregateException e)
                {
                    imageFilepaths.Clear();
                    List<string> failedFilepaths = new List<string>();
                    foreach (var exception in e.InnerExceptions)
                    {
                        FileFlipFailedException flipFailed = exception as FileFlipFailedException;
                        if (flipFailed != null)
                        {
                            failedFilepaths.Add(flipFailed.FailedFilepath);
                        }
                    }

                    Console.WriteLine("The following files could not be flipped due to errors:");
                    Console.WriteLine(String.Join(System.Environment.NewLine, failedFilepaths));

                    Console.Write("Would you like to reprocess them? (y/n): ");
                    if (Console.ReadLine()[0] == 'y')
                    {
                        imageFilepaths.AddRange(failedFilepaths);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a directory with images to flip:");
            var imagesDirectory = Console.ReadLine();

            var imageFilepaths = System.IO.Directory.GetFiles(imagesDirectory).ToList();

            Console.WriteLine("Found the following files:");
            Console.WriteLine(String.Join(System.Environment.NewLine, imageFilepaths));
            Console.Write("Would you like to flip them ? (y/n): ");
            if (Console.ReadLine().ToLower() == "y")
            { 
                FlipImagesWithOptionalRetry(imageFilepaths, "flippedImages");
            }
        }
    }
}
