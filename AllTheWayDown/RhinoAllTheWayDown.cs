using System.Collections.Generic;
using Rhino;
using Rhino.UI;

namespace SampleCsEto
{
  public class RhinoAllTheWayDown : Rhino.PlugIns.PlugIn
  {
    public RhinoAllTheWayDown()
    {
      Instance = this;
    }

    public static RhinoAllTheWayDown Instance
    {
      get;
      private set;
    }

    protected override void DocumentPropertiesDialogPages(RhinoDoc doc, List<OptionsDialogPage> pages)
    {
      var page = new Views.SampleCsEtoOptionsPage();
      pages.Add(page);
    }

    protected override void OptionsDialogPages(List<OptionsDialogPage> pages)
    {
      var page = new Views.SampleCsEtoOptionsPage();
      pages.Add(page);
    }

    protected override void ObjectPropertiesPages(ObjectPropertiesPageCollection collection)
    {
      var page = new Views.SampleCsEtoPropertiesPage();
      collection.Add(page);
    }
  }
}