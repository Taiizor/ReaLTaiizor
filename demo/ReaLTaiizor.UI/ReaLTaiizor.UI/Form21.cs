using ReaLTaiizor.Docking.Crown;
using ReaLTaiizor.Forms;
using ReaLTaiizor.Native;
using ReaLTaiizor.UI.Forms.Dialogs;
using ReaLTaiizor.UI.Forms.Docking;
using ReaLTaiizor.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.CrownHelper;

namespace ReaLTaiizor.UI
{
    public partial class Form21 : CrownForm
    {
        #region Field Region

        private readonly List<CrownDockContent> _toolWindows = new();

        private readonly DockProject _dockProject;
        private readonly DockProperties _dockProperties;
        private readonly DockConsole _dockConsole;
        private readonly DockLayers _dockLayers;
        private readonly DockHistory _dockHistory;

        private readonly string ConfigFile = "CrownDockPanel.config";

        #endregion

        #region Constructor Region

        public Form21()
        {
            InitializeComponent();

            // Add the control scroll message filter to re-route all mousewheel events
            // to the control the user is currently hovering over with their cursor.
            Application.AddMessageFilter(new ControlScrollFilter());

            // Add the dock content drag message filter to handle moving dock content around.
            Application.AddMessageFilter(DockPanel.DockContentDragFilter);

            // Add the dock panel message filter to filter through for dock panel splitter
            // input before letting events pass through to the rest of the application.
            Application.AddMessageFilter(DockPanel.DockResizeFilter);

            // Hook in all the UI events manually for clarity.
            HookEvents();

            // Build the tool windows and add them to the dock panel
            _dockProject = new DockProject();
            _dockProperties = new DockProperties();
            _dockConsole = new DockConsole();
            _dockLayers = new DockLayers();
            _dockHistory = new DockHistory();

            // Add the tool windows to a list
            _toolWindows.Add(_dockProject);
            _toolWindows.Add(_dockProperties);
            _toolWindows.Add(_dockConsole);
            _toolWindows.Add(_dockLayers);
            _toolWindows.Add(_dockHistory);

            // Deserialize if a previous state is stored
            if (File.Exists(ConfigFile))
            {
                DeserializeDockPanel(ConfigFile);
            }
            else
            {
                // Add the tool window list contents to the dock panel
                foreach (CrownDockContent toolWindow in _toolWindows)
                {
                    DockPanel.AddContent(toolWindow);
                }

                // Add the history panel to the layer panel group
                DockPanel.AddContent(_dockHistory, _dockLayers.DockGroup);
            }

            // Check window menu items which are contained in the dock panel
            BuildWindowMenu();

            // Add dummy documents to the main document area of the dock panel
            DockPanel.AddContent(new DockDocument("Document 1", Properties.Resources.document_16xLG));
            DockPanel.AddContent(new DockDocument("Document 2", Properties.Resources.document_16xLG));
            DockPanel.AddContent(new DockDocument("Document 3", Properties.Resources.document_16xLG));
        }

        #endregion

        #region Method Region

        private void HookEvents()
        {
            FormClosing += Form21_FormClosing;

            DockPanel.ContentAdded += DockPanel_ContentAdded;
            DockPanel.ContentRemoved += DockPanel_ContentRemoved;

            mnuNewFile.Click += NewFile_Click;
            mnuClose.Click += Close_Click;

            btnNewFile.Click += NewFile_Click;

            mnuDialog.Click += Dialog_Click;

            mnuProject.Click += Project_Click;
            mnuProperties.Click += Properties_Click;
            mnuConsole.Click += Console_Click;
            mnuLayers.Click += Layers_Click;
            mnuHistory.Click += History_Click;
        }

        private void ToggleToolWindow(CrownToolWindow toolWindow)
        {
            if (toolWindow.DockPanel == null)
            {
                DockPanel.AddContent(toolWindow);
            }
            else
            {
                DockPanel.RemoveContent(toolWindow);
            }
        }

        private void BuildWindowMenu()
        {
            mnuProject.Checked = DockPanel.ContainsContent(_dockProject);
            mnuProperties.Checked = DockPanel.ContainsContent(_dockProperties);
            mnuConsole.Checked = DockPanel.ContainsContent(_dockConsole);
            mnuLayers.Checked = DockPanel.Contains(_dockLayers);
            mnuHistory.Checked = DockPanel.Contains(_dockHistory);
        }

        #endregion

        #region Event Handler Region

        private void Form21_FormClosing(object sender, FormClosingEventArgs e)
        {
            SerializeDockPanel(ConfigFile);
        }

        private void DockPanel_ContentAdded(object sender, DockContentEventArgs e)
        {
            if (_toolWindows.Contains(e.Content))
            {
                BuildWindowMenu();
            }
        }

        private void DockPanel_ContentRemoved(object sender, DockContentEventArgs e)
        {
            if (_toolWindows.Contains(e.Content))
            {
                BuildWindowMenu();
            }
        }

        private void NewFile_Click(object sender, EventArgs e)
        {
            DockDocument newFile = new("New document", Properties.Resources.document_16xLG);
            DockPanel.AddContent(newFile);
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Dialog_Click(object sender, EventArgs e)
        {
            DialogControls test = new();
            test.ShowDialog();
        }

        private void Project_Click(object sender, EventArgs e)
        {
            ToggleToolWindow(_dockProject);
        }

        private void Properties_Click(object sender, EventArgs e)
        {
            ToggleToolWindow(_dockProperties);
        }

        private void Console_Click(object sender, EventArgs e)
        {
            ToggleToolWindow(_dockConsole);
        }

        private void Layers_Click(object sender, EventArgs e)
        {
            ToggleToolWindow(_dockLayers);
        }

        private void History_Click(object sender, EventArgs e)
        {
            ToggleToolWindow(_dockHistory);
        }

        private void DarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemeProvider.Theme = new DarkTheme();
            BackColor = ThemeProvider.Theme.Colors.GreyBackground;

            foreach (ToolStripMenuItem Control in mnuMain.Items)
            {
                Control.BackColor = ThemeProvider.Theme.Colors.GreyBackground;
            }

            foreach (ToolStripLabel Control in stripMain.Items)
            {
                Control.ForeColor = ThemeProvider.Theme.Colors.LightText;
            }

            foreach (Control Control in DockPanel.ActiveDocument.Controls)
            {
                if (Control.Name == "txtDocument")
                {
                    Control.ForeColor = ThemeProvider.Theme.Colors.LightText;
                    Control.BackColor = Color.FromArgb(43, 43, 43);
                }
            }

            Invalidate();
            Refresh();
        }

        private void LightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemeProvider.Theme = new LightTheme();
            BackColor = ThemeProvider.Theme.Colors.GreyBackground;

            foreach (ToolStripMenuItem Control in mnuMain.Items)
            {
                Control.BackColor = ThemeProvider.Theme.Colors.GreyBackground;
            }

            foreach (ToolStripLabel Control in stripMain.Items)
            {
                Control.ForeColor = ThemeProvider.Theme.Colors.LightText;
            }

            foreach (Control Control in DockPanel.ActiveDocument.Controls)
            {
                if (Control.Name == "txtDocument")
                {
                    Control.ForeColor = ThemeProvider.Theme.Colors.LightText;
                    Control.BackColor = ThemeProvider.Theme.Colors.LightBackground;
                    break;
                }
            }

            Invalidate();
            Refresh();
        }

        #endregion

        #region Serialization Region

        private void SerializeDockPanel(string path)
        {
            DockPanelState state = DockPanel.GetDockPanelState();
            SerializerHelper.Serialize(state, path);
        }

        private void DeserializeDockPanel(string path)
        {
            DockPanelState state = SerializerHelper.Deserialize<DockPanelState>(path);
            DockPanel.RestoreDockPanelState(state, GetContentBySerializationKey);
        }

        private CrownDockContent GetContentBySerializationKey(string key)
        {
            foreach (CrownDockContent window in _toolWindows)
            {
                if (window.SerializationKey == key)
                {
                    return window;
                }
            }

            return null;
        }

        #endregion
    }
}