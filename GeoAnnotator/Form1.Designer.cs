namespace GeoAnnotator;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.PictureBox pictureBox;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 650);
        this.Text = "GeoAnnotator";

        // Add PictureBox control
        this.pictureBox = new System.Windows.Forms.PictureBox();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
        this.pictureBox.Location = new System.Drawing.Point(32, 52); // Set the location as needed
        this.pictureBox.Name = "pictureBox";
        this.pictureBox.Size = new System.Drawing.Size(600, 400); // Set the size as needed
        this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom; // Set the size mode as needed
        this.pictureBox.TabIndex = 0;
        this.pictureBox.TabStop = true;
        this.Controls.Add(this.pictureBox);
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
    }

    #endregion
}
