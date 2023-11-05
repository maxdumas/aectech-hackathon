using Rhino;
using Rhino.Commands;
using Rhino.Input.Custom;
using Rhino.UI;

namespace SampleCsChromium
{
  [System.Runtime.InteropServices.Guid("a599ea16-fe94-470f-b951-2d26fac5160e")]
  public class SampleCsChromiumCommand : Command
  {
    /// <summary>
    /// Constructor
    /// </summary>
    public SampleCsChromiumCommand()
    {
      // Rhino only creates one instance of each command class defined in a
      // plug-in, so it is safe to store a refence in a static property.
      Instance = this;
    }

    /// <summary>
    /// Gets the only instance of the this command.
    /// </summary>
    public static SampleCsChromiumCommand Instance
    {
      get;
      private set;
    }

    /// <summary>
    /// The command name as it appears on the Rhino command line.
    /// </summary>
    public override string EnglishName
    {
      get { return "SampleCsChromium"; }
    }

    /// <summary>
    /// Call to run the command.
    /// </summary>
    protected override Result RunCommand(RhinoDoc doc, RunMode mode)
    {
      var panel_id = SampleCsChromiumPanelControl.PanelId;
      var visible = Panels.IsPanelVisible(panel_id);

      string prompt = (visible)
        ? "Chromium panel is visible. New value"
        : "Chromium Manager panel is hidden. New value";

      var go = new GetOption();
      go.SetCommandPrompt(prompt);

      var hide_index = go.AddOption("Hide");
      var show_index = go.AddOption("Show");
      var toggle_index = go.AddOption("Toggle");

      go.Get();
      if (go.CommandResult() != Result.Success)
        return go.CommandResult();

      var option = go.Option();
      if (null == option)
        return Result.Failure;

      var index = option.Index;

      if (index == hide_index)
      {
        if (visible)
          Panels.ClosePanel(panel_id);
      }
      else if (index == show_index)
      {
        if (!visible)
          Panels.OpenPanel(panel_id);
      }
      else if (index == toggle_index)
      {
        if (visible)
          Panels.ClosePanel(panel_id);
        else
          Panels.OpenPanel(panel_id);
      }

      return Result.Success;
    }
  }
}
