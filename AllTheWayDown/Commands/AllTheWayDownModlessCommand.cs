using System;
using Rhino;
using Rhino.Commands;
using Rhino.UI;

namespace SampleCsEto.Commands
{
  public class AllTheWayDownModelessCommand : Command
  {
    /// <summary>
    /// Form accessor
    /// </summary>
    private Views.AllTheWayDown Form
    {
      get;
      set;
    }

    public override string EnglishName
    {
      get { return "AllTheWayDown"; }
    }

    protected override Result RunCommand(RhinoDoc doc, RunMode mode)
    {
      if (null == Form)
      {
        Form = new Views.AllTheWayDown { Owner = RhinoEtoApp.MainWindow };
        Form.Closed += OnFormClosed;
        Form.Show();
      }
      return Result.Success;
    }

    /// <summary>
    /// FormClosed EventHandler
    /// </summary>
    private void OnFormClosed(object sender, EventArgs e)
    {
      Form.Dispose();
      Form = null;
    }
  }
}
