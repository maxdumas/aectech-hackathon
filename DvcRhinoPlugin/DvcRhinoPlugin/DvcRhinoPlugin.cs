﻿using Rhino;
using Rhino.PlugIns;
using SpitTextFile;
using System;

namespace DvcRhinoPlugin
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class DvcRhinoPlugin : Rhino.PlugIns.PlugIn
    {
        public DvcRhinoPlugin()
        {
            Instance = this;

        }

        ///<summary>Gets the only instance of the DvcRhinoPlugin plug-in.</summary>
        public static DvcRhinoPlugin Instance { get; private set; }

        protected override LoadReturnCode OnLoad(ref string errorMessage) {
            var triggerSave = new TriggerSave();
            triggerSave.Trigger(ref errorMessage);
            return base.OnLoad(ref errorMessage); }

        // You can override methods here to change the plug-in behavior on
        // loading and shut down, add options pages to the Rhino _Option command
        // and maintain plug-in wide options in a document.
    }
}