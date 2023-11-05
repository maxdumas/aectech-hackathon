using Eto.Drawing;
using Eto.Forms;
using Rhino.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Grasshopper.Kernel;
using Rhino.PlugIns;
using System.Threading.Tasks;

namespace SampleCsEto.Views
{
  /// <summary>
  /// Warning! Modelers forms are not currently supported on Mac, you should use
  /// Panels for cross platform mode-less UI.
  /// </summary>
  class AllTheWayDown : Form
  {
    public List<string> filepaths = new List<string>();
    public List<TextBox> boxes = new List<TextBox>();

    public AllTheWayDown()
    {
      Maximizable = false;
      Minimizable = false;
      Padding = new Padding(5);
      Resizable = false;
      ShowInTaskbar = false;
      Title = GetType().Name;
      WindowStyle = WindowStyle.Default;



      var hello_button = new Button { Text = "Hello" };
      hello_button.Click += (sender, e) => OnHelloButton();

      var close_button = new Button { Text = "RUN" };
      close_button.Click += (sender, e) => RunFiles();

      var hello_layout = new TableLayout
      {
        Padding = new Padding(5, 10, 5, 5),
        Spacing = new Size(5, 5),
        Rows = { new TableRow(null, hello_button, null) }
      };

      var close_layout = new TableLayout
      {
        Padding = new Padding(5, 10, 5, 5),
        Spacing = new Size(5, 5),
        Rows = { new TableRow(null, close_button, null) }
      };

      var tableLayout = new TableLayout
      {
        Padding = new Padding(5),
        Spacing = new Size(5, 5),
        Rows = {
          
            //new TableRow(hello_layout),
            new TableRow(close_layout)
          }
      };

      LoadFiles();

      foreach (TextBox textBox in boxes)
      {
        tableLayout.Rows.Add(new TableRow(textBox));
      }
      tableLayout.Rows.Add(null);

      Content = tableLayout;
    }

    protected override void OnLoadComplete(EventArgs e)
    {
      base.OnLoadComplete(e);
      this.RestorePosition();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      this.SavePosition();
      base.OnClosing(e);
    }

    protected void OnHelloButton()
    {
      MessageBox.Show(this, "Hello Rhino!", Title, MessageBoxButtons.OK);
    }

    protected void LoadFiles()
    {
      filepaths = new List<string>()
      {
        @"C:\Users\krahimzadeh\Documents\LocalDocs\AEC_Tech\00_REFERENCE\REF_FLOORS\REF_FLOORS.gh",
        //@"C:\Users\krahimzadeh\Documents\LocalDocs\AEC_Tech\01_ENVELOPE\ENV_STG01_MASSING\ENV_STG01_Massing.gh",
        @"C:\Users\krahimzadeh\Documents\LocalDocs\AEC_Tech\01_ENVELOPE\ENV_STG02_LEVELS\ENV_STG02_LEVELS.gh",
        @"C:\Users\krahimzadeh\Documents\LocalDocs\AEC_Tech\01_ENVELOPE\ENV_STG03_JOINTS\ENV_WF_STG03_JOINTS.gh",
        @"C:\Users\krahimzadeh\Documents\LocalDocs\AEC_Tech\01_ENVELOPE\ENV_STG04_OUTLINES\ENV_WF_STG04_Outlines.gh",
        @"C:\Users\krahimzadeh\Documents\LocalDocs\AEC_Tech\01_ENVELOPE\ENV_STG05_SRFS\ENV_STG05_SRFS.gh",
        @"C:\Users\krahimzadeh\Documents\LocalDocs\AEC_Tech\03_SHADING\01_ANALYSIS\SHD_STG01_Analysis\SHD_STG01_Analysis.gh",
        @"C:\Users\krahimzadeh\Documents\LocalDocs\AEC_Tech\03_SHADING\02_PANELS\SHD_PAN_STG01_Setout\SHD_PAN_STG01_Setout.gh"
      };

      foreach (string filepath in filepaths)
      {
        boxes.Add(new TextBox() {Text = filepath});
      }
    }


    internal async void RunFiles()
    {

      var io = new GH_DocumentIO();

      foreach (TextBox box in boxes)
      {
        string filepath = box.Text;

        io.Open(filepath);
        var doc = io.Document;

        if (doc == null) continue;
        var color = box.BackgroundColor;

        Application.Instance.Invoke(() =>
          {
            doc.Enabled = true;
            doc.ExpireSolution();
            box.BackgroundColor = Color.FromArgb(0, 30,120);
          }
        );

        try
        {
          //await Task.Run(() => doc.NewSolution(true, GH_SolutionMode.Silent));

          doc.NewSolution(true, GH_SolutionMode.Silent);
        }
        finally
        {
          Application.Instance.Invoke(() =>
          {
            box.BackgroundColor = Color.FromArgb(25, 100,10);
          });

          
        }

      }
    }
  }
}
