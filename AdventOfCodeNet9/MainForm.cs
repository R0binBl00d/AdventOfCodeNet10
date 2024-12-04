using System.Diagnostics;

namespace AdventOfCodeNet9
{
  public partial class MainForm : Form
  {
    #region not interesting

    public MainForm()
    {
      tabPages = new List<TabPage>();
      InitializeComponent();
    }

    private List<TabPage> tabPages;

    private void MainForm_Load(object sender, EventArgs e)
    {
      // Create all Buttons !!
      this.tabControl1.SuspendLayout();
      this.SuspendLayout();

      this.tabControl1.Controls.Clear();

      int x, y;

      int x_left = 7;
      int x_offset = 160;
      int x_gap = 190;
      int y_bottom = 550;
      int y_offset = 60;

      for (int year = 2015; year <= 2024; year++)
      {
        // new TabPage
        TabPage tp = new System.Windows.Forms.TabPage();

        tp.SuspendLayout();

        tp.Location = new System.Drawing.Point(4, 22);
        tp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        tp.Name = "tabPage" + year.ToString();
        tp.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
        tp.Size = new System.Drawing.Size(432, 415);
        tp.TabIndex = 0;
        tp.Text = year.ToString();
        tp.UseVisualStyleBackColor = true;

        tabPages.Add(tp);
        x = x_left;
        y = y_bottom;

        for (int day = 1; day <= 25; day++)
        {
          for (int part = 1; part <= 2; part++)
          {
            // Add new Button
            Button btn = new Button();

            btn.Location = new Point(x, y);

            btn.Name = String.Format("button{0}_{1}_{2}", year, day, part);
            btn.Size = new System.Drawing.Size(150, 50);
            btn.TabIndex = 0;
            btn.Tag = String.Format("{0} {1} {2}", year, day, part);
            btn.Text = String.Format("Day {0:00} {1}", day, part);
            btn.UseVisualStyleBackColor = true;
            btn.Click += new System.EventHandler(this.button_Click!);

            tp.Controls.Add(btn);

            if (day <= 20) // next 2 each other
            {
              if (part == 1)
              {
                // shift right
                x = x + x_offset;
              }
              else // part == 2
              {
                // shift left and up
                x = x - x_offset;
                y = y - y_offset;
              }

              if ((day == 10 || day == 20) && part == 2)
              {
                x = x + x_offset + x_gap;
                y = y_bottom;
              }
            }
            else // on Top of each other
            {
              y = y - y_offset;
            }
          }
        }
        // Add TagPage to TabControl
        this.tabControl1.Controls.Add(tp);
      }

      foreach (var page in tabPages)
      {
        page.ResumeLayout(false);
      }

      this.tabControl1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private void MainForm_Shown(object sender, EventArgs e)
    {
      toolStripStatusLabel1.Text = "";
      //tabControl1.SelectTab(0); // 2015
      //tabControl1.SelectTab(1); // 2016
      //tabControl1.SelectTab(2); // 2017
      //tabControl1.SelectTab(3); // 2018
      //tabControl1.SelectTab(4); // 2019
      //tabControl1.SelectTab(5); // 2020
      //tabControl1.SelectTab(6); // 2021
      //tabControl1.SelectTab(7); // 2022
      //tabControl1.SelectTab(8); // 2023
      tabControl1.SelectTab(9); // 2024

      // last tab
      //this.tabControl1.SelectedTab = tabPages[tabPages.Count - 1];
    }

    #endregion not interesting

    private void button_Click(object sender, EventArgs e)
    {
      Button btn = (Button)sender;
      var chunks = btn.Tag!.ToString()!.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

      //  "2015 1 1"  // Year Day Part
      //  "2015 25 2" // Year Day Part
      int day = Int32.Parse(chunks[1]);
      Type type = Type.GetType(String.Format("AdventOfCodeNet9._{0}.Day_{1:00}.Part_{2}", chunks[0], day, chunks[2]))!;
#pragma warning disable CS8604 // Possible null reference argument.
      RunDay(Activator.CreateInstance(type) as Days, String.Format(@".\{0}\Day_{1:00}\Input.txt", chunks[0], day));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    private void RunDay(Days day, string input)
    {
      day.Init(input);
      Stopwatch sw = new Stopwatch();
      sw.Start();
      String result = day.Execute();
      sw.Stop();

      if (!String.IsNullOrEmpty(result))
      {
        Clipboard.Clear();
        Clipboard.SetText(result);
      }

      toolStripStatusLabel1.Text = String.Format("{0}\t Solution:\t \"{1}\"\t Calculation Time: '{2}'",
        DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss"), result, sw.Elapsed);

      MessageBox.Show(result, sw.Elapsed.ToString());
    }

    private void tsslConvertInstructions_Click(object sender, EventArgs e)
    {
      var fci = new FormConvertInstructions();
      fci.ShowDialog();
    }
  }
}
