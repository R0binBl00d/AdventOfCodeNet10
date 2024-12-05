namespace AdventOfCodeNet9
{
  partial class FormGenerateTemplatesForSharing
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      richTextBox1 = new RichTextBox();
      btnGenerate = new Button();
      statusStrip1 = new StatusStrip();
      toolStripStatusLabel1 = new ToolStripStatusLabel();
      statusStrip1.SuspendLayout();
      SuspendLayout();
      // 
      // richTextBox1
      // 
      richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      richTextBox1.Location = new Point(12, 12);
      richTextBox1.Name = "richTextBox1";
      richTextBox1.Size = new Size(776, 369);
      richTextBox1.TabIndex = 0;
      richTextBox1.Text = "2015\n2016\n2017\n2018\n2019\n2020\n2021\n2022\n2023\n2024";
      // 
      // btnGenerate
      // 
      btnGenerate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnGenerate.Location = new Point(12, 387);
      btnGenerate.Name = "btnGenerate";
      btnGenerate.Size = new Size(776, 72);
      btnGenerate.TabIndex = 1;
      btnGenerate.Text = "Generate Templates in Subfolder";
      btnGenerate.UseVisualStyleBackColor = true;
      btnGenerate.Click += btnGenerate_Click;
      // 
      // statusStrip1
      // 
      statusStrip1.ImageScalingSize = new Size(32, 32);
      statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
      statusStrip1.Location = new Point(0, 472);
      statusStrip1.Name = "statusStrip1";
      statusStrip1.Size = new Size(800, 42);
      statusStrip1.TabIndex = 2;
      statusStrip1.Text = "statusStrip1";
      // 
      // toolStripStatusLabel1
      // 
      toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      toolStripStatusLabel1.Size = new Size(237, 32);
      toolStripStatusLabel1.Text = "toolStripStatusLabel1";
      // 
      // FormGenerateTemplatesForSharing
      // 
      AutoScaleDimensions = new SizeF(13F, 32F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(800, 514);
      Controls.Add(statusStrip1);
      Controls.Add(btnGenerate);
      Controls.Add(richTextBox1);
      Name = "FormGenerateTemplatesForSharing";
      Text = "FormGenerateTemplatesForSharing";
      statusStrip1.ResumeLayout(false);
      statusStrip1.PerformLayout();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private RichTextBox richTextBox1;
    private Button btnGenerate;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel toolStripStatusLabel1;
  }
}