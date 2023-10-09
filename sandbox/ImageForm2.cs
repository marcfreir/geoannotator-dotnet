using System;
using System.Drawing;
using System.Windows.Forms;

namespace GeoAnnotator
{
    public partial class ImageForm : Form
    {
        private readonly PictureBox pictureBox;
        private readonly string imagePath;

        public ImageForm(string imagePath)
        {
            InitializeComponent();
            this.imagePath = imagePath;

            // Set the WindowState to Maximized for full-screen mode
            this.WindowState = FormWindowState.Maximized;

            // Create PictureBox control
            pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Controls.Add(pictureBox);

            // Load the image
            if (File.Exists(imagePath))
            {
                pictureBox.Image = new Bitmap(imagePath);
            }
            else
            {
                MessageBox.Show("Image file not found.");
            }

            // Create "Close" Button
            var closeButton = new Button();
            closeButton.Text = "Close";
            closeButton.Dock = DockStyle.Bottom;
            closeButton.Click += CloseButton_Click;
            Controls.Add(closeButton);

            // Load and display the image
            LoadImage();
        }

        private void LoadImage()
        {
            try
            {
                var image = Image.FromFile(imagePath);
                pictureBox.Image = image;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void CloseButton_Click(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
