using Commands;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SyncProgrammingProblemsImageFlipper
{
    public class ImageFlipperViewModel
    {
        private ICommand browseToFlipCommand;
        private ObservableCollection<ImageSource> images;

        public ICommand BrowseToFlip
        {
            get
            {
                if (this.browseToFlipCommand == null)
                {
                    browseToFlipCommand = new RelayCommand(BroweToFlip);
                }

                return browseToFlipCommand;
            }
        }

        public ICollection<ImageSource> Images
        {
            get
            {
                if (images == null)
                {
                    images = new ObservableCollection<ImageSource>();
                }

                return images;
            }
        }

        private void BroweToFlip(object context)
        {
            Console.WriteLine(context);
            var dialog = new OpenFileDialog();
            dialog.Multiselect = true;

            dialog.FileOk += FilesToFlipChosen;

            dialog.ShowDialog();
        }

        private void FilesToFlipChosen(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            var openFileDialog = sender as OpenFileDialog;

            foreach (var filename in openFileDialog.FileNames)
            {
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(filename);
                bitmap.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
                bitmap.Save(filename);

                ImageSource source = new BitmapImage(new Uri(filename));

                this.Images.Add(source);
            }
        }
    }
}
