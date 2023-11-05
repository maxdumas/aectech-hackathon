using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    internal static class GH_Utilies
    {

        public static GH_Document LoadGrasshopperDoc(string filepath)
        {
            var io = new GH_DocumentIO();

            io.Open(filepath);


            var doc = io.Document;
            if (doc == null)
            {
            }

            return doc;
        }
    }
}
