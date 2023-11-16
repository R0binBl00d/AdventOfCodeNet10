namespace AdventOfCodeNet8
{
  partial class MainForm
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

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
      tabControl1 = new TabControl();
      tabPage1 = new TabPage();
      statusStrip1 = new StatusStrip();
      toolStripStatusLabel1 = new ToolStripStatusLabel();
      tabControl1.SuspendLayout();
      statusStrip1.SuspendLayout();
      SuspendLayout();
      // 
      // tabControl1
      // 
      tabControl1.Controls.Add(tabPage1);
      tabControl1.Dock = DockStyle.Fill;
      tabControl1.Location = new Point(0, 0);
      tabControl1.Name = "tabControl1";
      tabControl1.SelectedIndex = 0;
      tabControl1.Size = new Size(947, 711);
      tabControl1.TabIndex = 0;
      // 
      // tabPage1
      // 
      tabPage1.Location = new Point(8, 46);
      tabPage1.Name = "tabPage1";
      tabPage1.Padding = new Padding(3);
      tabPage1.Size = new Size(931, 657);
      tabPage1.TabIndex = 0;
      tabPage1.Text = "filled on Form Shown";
      tabPage1.UseVisualStyleBackColor = true;
      // 
      // statusStrip1
      // 
      statusStrip1.ImageScalingSize = new Size(32, 32);
      statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
      statusStrip1.Location = new Point(0, 669);
      statusStrip1.Name = "statusStrip1";
      statusStrip1.Size = new Size(947, 42);
      statusStrip1.TabIndex = 1;
      statusStrip1.Text = "statusStrip1";
      // 
      // toolStripStatusLabel1
      // 
      toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      toolStripStatusLabel1.Size = new Size(237, 32);
      toolStripStatusLabel1.Text = "toolStripStatusLabel1";
      // 
      // MainForm
      // 
      AutoScaleDimensions = new SizeF(13F, 32F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(947, 711);
      Controls.Add(statusStrip1);
      Controls.Add(tabControl1);
      Name = "MainForm";
      Text = "Advent of Code";
      Load += MainForm_Load;
      Shown += MainForm_Shown;
      tabControl1.ResumeLayout(false);
      statusStrip1.ResumeLayout(false);
      statusStrip1.PerformLayout();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private TabControl tabControl1;
    private TabPage tabPage1;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel toolStripStatusLabel1;
  }
}
