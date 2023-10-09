using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GeoAnnotator
{
    public partial class ImageForm : Form
    {
        private readonly PictureBox pictureBox;
        private readonly string imagePath;
        private Bitmap canvas;

        public ImageForm(string imagePath)
        {
            InitializeComponent();
            this.imagePath = imagePath;
            // Initialize the canvas field with a new Bitmap instance
            canvas = new Bitmap(1, 1); // You can set the initial size according to your requirements

            // Set the WindowState to Maximized for full-screen mode
            this.WindowState = FormWindowState.Maximized;

            // Create PictureBox control
            pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseUp += PictureBox_MouseUp;
            Controls.Add(pictureBox);

            // Load and display the image
            LoadImage();
        }

        private void LoadImage()
        {
            try
            {
                // Load the image from file
                canvas = new Bitmap(imagePath);
                pictureBox.Image = canvas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        // Drawing logic
        private bool isDrawing = false;
        private Point lastPoint;

        private void PictureBox_MouseDown(object? sender, MouseEventArgs e)
        {
            isDrawing = true;
            lastPoint = e.Location;
        }

        private void PictureBox_MouseMove(object? sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                using (Graphics g = Graphics.FromImage(canvas))
                {
                    g.DrawLine(Pens.Black, lastPoint, e.Location);
                    lastPoint = e.Location;
                    pictureBox.Invalidate();
                }
            }
        }

        private void PictureBox_MouseUp(object? sender, MouseEventArgs e)
        {
            isDrawing = false;
        }
    }
}
