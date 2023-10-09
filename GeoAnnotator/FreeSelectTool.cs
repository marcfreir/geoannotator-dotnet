using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GeoAnnotator
{
    public class FreeSelectTool : Form
    {
        private bool isDrawing = false;
        private List<Point> selectionPoints = new List<Point>();
        private Bitmap canvas;
        private Color fillColor = Color.Blue;

        // Public properties to set the image path and initial canvas image
        public string? ImagePath { get; set; }
        public Bitmap? InitialCanvas { get; set; } = new Bitmap(1, 1); // Set a default initial canvas

        // Constructor that accepts image path and initial canvas
        public FreeSelectTool(string? imagePath, Bitmap? initialCanvas)
        {
            // Set properties
            ImagePath = imagePath;
            InitialCanvas = initialCanvas;

            // Initialize UI components
            InitializeComponent();

            // Check if the InitialCanvas is set, and if so, use it as the canvas
            if (InitialCanvas != null && InitialCanvas.Width > 1 && InitialCanvas.Height > 1)
            {
                canvas = new Bitmap(InitialCanvas);
            }
            else
            {
                canvas = new Bitmap(this.Width, this.Height);
            }
        }

        // UI component initialization method
        private void InitializeComponent()
        {
            this.DoubleBuffered = true;
            this.Size = new Size(800, 600);

            this.MouseDown += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    isDrawing = true;
                    selectionPoints.Clear();
                    selectionPoints.Add(e.Location);
                }
            };

            this.MouseMove += (sender, e) =>
            {
                if (isDrawing)
                {
                    selectionPoints.Add(e.Location);
                    this.Invalidate();
                }
            };

            this.MouseUp += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    isDrawing = false;
                    this.FillSelectedArea();
                    selectionPoints.Clear();
                    this.Invalidate();
                }
            };

            this.Paint += (sender, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.DrawImage(canvas, Point.Empty);

                // Draw the current selection if in drawing mode
                if (isDrawing && selectionPoints.Count > 1)
                {
                    e.Graphics.DrawLines(Pens.Black, selectionPoints.ToArray());
                }
            };
        }

        // Property to get the modified image
        public Bitmap ModifiedImage
        {
            get { return canvas; }
        }

        private void FillSelectedArea()
        {
            using (Graphics g = Graphics.FromImage(canvas))
            {
                if (selectionPoints.Count > 2)
                {
                    g.FillPolygon(new SolidBrush(fillColor), selectionPoints.ToArray());
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw the canvas
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawImage(canvas, Point.Empty);

            // Draw the current selection if in drawing mode
            if (isDrawing && selectionPoints.Count > 1)
            {
                e.Graphics.DrawLines(Pens.Black, selectionPoints.ToArray());
            }
        }
    }
}
