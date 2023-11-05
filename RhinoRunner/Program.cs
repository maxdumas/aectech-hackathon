using System;
using System.Runtime.InteropServices;
using Rhino.Runtime.InProcess;
using Rhino.Geometry;

namespace HelloWorld
{
  class Program
  {
    #region Program static constructor
   
    static Program()
    {
      RhinoInside.Resolver.Initialize();
    }
    #endregion

    [System.STAThread]
    
    static void Main(string[] args)
    {
      try
      {
        using (new RhinoCore(args))
        {
          MeshABrep();
          Console.WriteLine("press any key to exit");
          Console.ReadKey();

          try
          {
              GH_Utilies.LoadGrasshopperDoc(
                            @"C:\Users\krahimzadeh\Documents\LocalDocs\AEC_Tech\00_REFERENCE\REF_FLOORS\REF_FLOORS.gh");
                    }
          finally
          {

          }
          //Grasshopper.Instances

        }
      }
      catch (Exception ex)
      {
        Console.Error.WriteLine(ex.Message);
      }
    }

    static void MeshABrep()
    {
      var sphere = new Sphere(Point3d.Origin, 12);
      var brep = sphere.ToBrep();
      var mp = new MeshingParameters(0.5);
      var mesh = Mesh.CreateFromBrep(brep, mp);
      Console.WriteLine($"Mesh with {mesh[0].Vertices.Count} vertices created");
    }
  }
}
