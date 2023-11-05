using System;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Reflection;
using System.IO;

namespace SampleCsChromium
{
    [System.Runtime.InteropServices.Guid("A687BDD9-F74C-4BB2-88E0-E2AEC95A9FCE")]
    public partial class SampleCsChromiumPanelControl : UserControl
    {
        private ChromiumWebBrowser m_browser;

        /// <summary>
        /// Returns the ID of this panel.
        /// </summary>
        public static Guid PanelId
        {
            get
            {
                return typeof(SampleCsChromiumPanelControl).GUID;
            }
        }

        public SampleCsChromiumPanelControl()
        {
            InitializeComponent();
            InitializeBrowser();
            SampleCsChromiumPlugIn.Instance.UserControl = this;
            this.Disposed += new EventHandler(OnDisposed);
            Rhino.RhinoApp.Closing += RhinoApp_Closing;

        }

        private void InitializeBrowser()
        {
            Cef.EnableHighDPISupport();

            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string assemblyPath = Path.GetDirectoryName(assemblyLocation);
            string pathSubprocess = Path.Combine(assemblyPath, "CefSharp.BrowserSubprocess.exe");

            var settings = new CefSettings();
            settings.BrowserSubprocessPath = pathSubprocess;

            //the following setting is added to support Rhino 5.
            // With versions of CefSharp > 47.0.3, the content is rendered on 1/4 of the control area.
            // This setting fixes this, but at the consequence of disabling WebGL. 
            // If you need WebGL in CefSharp and are targeting Rhino 5, use CefSharp 47.0.3 without this setting.
            settings.DisableGpuAcceleration();

            Cef.Initialize(settings);

            m_browser = new ChromiumWebBrowser("http://www.rhino3d.com/");
            Controls.Add(m_browser);
            m_browser.Dock = DockStyle.Fill;
            m_browser.Enabled = true;
            m_browser.Show();
        }

        /// <summary>
        /// Occurs when the component is disposed by a call to the
        /// System.ComponentModel.Component.Dispose() method.
        /// </summary>
        private void OnDisposed(object sender, EventArgs e)
        {
            m_browser.Dispose();
            Cef.Shutdown();
            SampleCsChromiumPlugIn.Instance.UserControl = null;
        }

        /// <summary>
        /// Disposes of the browser when Rhino closes.
        /// </summary>
        private void RhinoApp_Closing(object sender, EventArgs e)
        {
            m_browser.Dispose();
            Cef.Shutdown();
            SampleCsChromiumPlugIn.Instance.UserControl = null;
        }
    }
}
