using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GeoAnnotator
{
    public partial class Form1 : Form
    {
        private string[] imageFiles = new string[0];
        private int currentIndex = -1;

        private readonly Button previousButton;
        private readonly Button nextButton;

        private readonly Button chooseButton; // Add a "Choose" button

        public Form1()
        {
            InitializeComponent();

            // Create "Previous" Button
            previousButton = new Button();
            previousButton.Text = "Previous";
            previousButton.Location = new Point(10, 30); // Set the button's location
            previousButton.Click += PreviousButton_Click; // Attach event handler
            Controls.Add(previousButton);

            // Create "Next" Button
            nextButton = new Button();
            nextButton.Text = "Next";
            nextButton.Location = new Point(100, 30); // Set the button's location
            nextButton.Click += NextButton_Click; // Attach event handler
            Controls.Add(nextButton);

            // Create "Choose" Button
            chooseButton = new Button();
            chooseButton.Text = "Choose";
            chooseButton.Location = new Point(200, 30); // Set the button's location
            chooseButton.Click += ChooseButton_Click; // Attach event handler
            Controls.Add(chooseButton);

            // Create a MenuStrip
            MenuStrip menuStrip = new MenuStrip();
            this.MainMenuStrip = menuStrip;
            Controls.Add(menuStrip);

            // Create a "File" menu
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");
            menuStrip.Items.Add(fileMenu);

            // Create an "Open" item under the "File" menu
            ToolStripMenuItem openMenuItem = new ToolStripMenuItem("Open");
            fileMenu.DropDownItems.Add(openMenuItem);

            // Subscribe to the "Click" event of the "Open" item
            openMenuItem.Click += OpenMenuItem_Click;

            // Create an "Exit" item under the "File" menu
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("Exit");
            fileMenu.DropDownItems.Add(exitMenuItem);

            // Subscribe to the "Click" event of the "Exit" item
            exitMenuItem.Click += ExitMenuItem_Click;

            // Create an instance of your custom form (FreeSelectTool)
            FreeSelectTool freeSelectTool = new FreeSelectTool();
            // Optionally, you can show your custom form
            freeSelectTool.Show();
        }

        // Event handler for the "Exit" menu item
        private void ExitMenuItem_Click(object? sender, EventArgs e)
        {
            // Exit the application when "Exit" is clicked
            Application.Exit();
        }

        // Event handler for the "Open" menu item
        private void OpenMenuItem_Click(object? sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                    imageFiles = Directory.GetFiles(folderDialog.SelectedPath)
                                        .Where(file => imageExtensions.Contains(Path.GetExtension(file).ToLower()))
                                        .ToArray();

                    if (imageFiles.Length > 0)
                    {
                        currentIndex = 0;
                        ShowCurrentImage();
                    }
                    else
                    {
                        MessageBox.Show("No supported image files found in the selected folder.");
                    }
                }
            }
        }

        private void PreviousButton_Click(object? sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                ShowCurrentImage();
            }
            else
            {
                MessageBox.Show("You are at the first image.");
            }
        }

        private void NextButton_Click(object? sender, EventArgs e)
        {
            if (currentIndex < imageFiles.Length - 1)
            {
                currentIndex++;
                ShowCurrentImage();
            }
            else
            {
                MessageBox.Show("You are at the last image.");
            }
        }

        private void ShowCurrentImage()
        {
            if (currentIndex >= 0 && currentIndex < imageFiles.Length)
            {
                pictureBox.Image = new System.Drawing.Bitmap(imageFiles[currentIndex]);
            }
        }

        private void ChooseButton_Click(object? sender, EventArgs e)
        {
            // Check if there is a current image to open
            if (currentIndex >= 0 && currentIndex < imageFiles.Length)
            {
                // Create and show a new instance of ImageForm with the current image
                using (var imageForm = new ImageForm(imageFiles[currentIndex]))
                {
                    imageForm.ShowDialog();
                }
            }
        }
    }
}
