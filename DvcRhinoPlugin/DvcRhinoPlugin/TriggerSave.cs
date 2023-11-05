using System;
using Rhino;
using Rhino.Commands;

namespace SpitTextFile
{
    public class TriggerSave 
    {
        public  Rhino.PlugIns.LoadReturnCode Trigger(ref string errorMessage)
        {
            Rhino.RhinoDoc.EndSaveDocument += SaveDocument;
            return Rhino.PlugIns.LoadReturnCode.Success;
        }


        private void SaveDocument(object sender, DocumentSaveEventArgs e)
        {
            SpitTextFile.SpitFile.WriteToFile();
        }
    }
}