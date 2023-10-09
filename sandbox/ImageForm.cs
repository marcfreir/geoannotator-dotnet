using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GeoAnnotator
{
    public partial class ImageForm : Form
    {
        private string? imagePath; // Store the path of the current image

        public ImageForm()
        {
            InitializeComponent();
        }

        // Event handler for the "Choose" button
        private void ChooseButton_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif|All Files|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = openFileDialog.FileName;
                    ShowSelectedImage();
                }
            }
        }

        // Method to display the selected image
        private void ShowSelectedImage()
        {
            if (File.Exists(imagePath))
            {
                pictureBox.Image = new Bitmap(imagePath);
            }
            else
            {
                MessageBox.Show("The selected image file does not exist.");
            }
        }
    }
}
