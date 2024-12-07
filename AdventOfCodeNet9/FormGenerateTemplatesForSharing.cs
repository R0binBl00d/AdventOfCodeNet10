using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Exception = System.Exception;

namespace AdventOfCodeNet9
{
  public partial class FormGenerateTemplatesForSharing : Form
  {
    public FormGenerateTemplatesForSharing()
    {
      InitializeComponent();

      toolStripStatusLabel1.Text = "";
    }

    private void btnGenerate_Click(object sender, EventArgs e)
    {
      try
      {
        var input = richTextBox1.Text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var executable = new FileInfo(Assembly.GetExecutingAssembly().Location);
        var di = new DirectoryInfo(Path.Combine(executable.DirectoryName, "Temp"));
        if (!di.Exists) di.Create();

        foreach (var s in input)
        {
          GenerateFoldersAndFiles(s, di);
        }
        var res = MessageBox.Show($"Done generating Files\n\nCheck: {di.FullName}\n\nDo you want me to open the Folder?\n\n" +
                        $"Yes -> Try to open the Folder\nNo -> Copy Path to Clipboard\nCancel -> do neither", "Success", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
        switch (res)
        {
          case DialogResult.Yes:
            Process.Start("explorer.exe", di.FullName);
            break;
          case DialogResult.No:
            Clipboard.Clear();
            Clipboard.SetText(di.FullName);
            break;
          default:
            break;
        }
        this.Close();
      }
      catch (Exception exception)
      {
        string msg = $"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}: {exception.Message}";
        MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void GenerateFoldersAndFiles(string SubfolderYear, DirectoryInfo di)
    {
      var sfy = new DirectoryInfo(Path.Combine(di.Name, SubfolderYear));
      if (!sfy.Exists) sfy.Create();

      for (int day = 1; day <= 25; day++)
      {
        var daypath = new DirectoryInfo(Path.Combine(sfy.FullName, $"Day_{day:00}"));
        if (!daypath.Exists) daypath.Create();
        GenerateFiles(SubfolderYear, day, daypath);
      }
    }

    private void GenerateFiles(string year, int day, DirectoryInfo daypath)
    {
      using (StreamWriter srInput = new StreamWriter(Path.Combine(daypath.FullName, "Input.txt"),
               Encoding.ASCII, new FileStreamOptions() { Mode = FileMode.Create, Access = FileAccess.Write }))
      {
        srInput.Write("\n");
      }
      using (StreamWriter srInput = new StreamWriter(Path.Combine(daypath.FullName, "Test-Example-Input.txt"),
               Encoding.ASCII, new FileStreamOptions()
               {
                 Mode = FileMode.Create,
                 Access = FileAccess.Write
               }))
      {
        srInput.Write("\n");
      }

      for (int i = 1; i <= 2; i++)
      {
        using (StreamWriter srInput = new StreamWriter(Path.Combine(daypath.FullName, $"Part_{i}.cs"),
                 Encoding.ASCII, new FileStreamOptions() { Mode = FileMode.Create, Access = FileAccess.Write }))
        {
          string text = """
namespace AdventOfCodeNet9._##YEAR##.Day_##01##
{
  internal class Part_##PART## : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/##YEAR##/day/##1##
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    public override string Execute()
    {
      string result = "";
      int totalCount = 0;
      foreach (var line in Lines)
      {
        totalCount++;
      }
      result = totalCount.ToString();
      return result;
    }
  }
}
""";
          text = text.Replace("##YEAR##", year);
          text = text.Replace("##PART##", i.ToString());
          text = text.Replace("##1##", day.ToString());
          text = text.Replace("##01##", day.ToString("00"));

          srInput.Write(text);
        }
      }
    }
  }
}
