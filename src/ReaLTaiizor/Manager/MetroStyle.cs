#region Imports

using System;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Design;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Design.Metro;
using System.Collections.Generic;
using System.Windows.Forms.Design;
using ReaLTaiizor.Interface.Metro;

#endregion

namespace ReaLTaiizor.Manager
{
    #region MetroStyleManager

    [DefaultProperty("Style")]
    [Designer(typeof(MetroStyleManagerDesigner))]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroStyleManager), "Style.bmp")]
    public class MetroStyleManager : Component
    {
        #region Constructor

        public MetroStyleManager(Form ownerForm)
        {
            MetroForm = ownerForm;
        }

        public MetroStyleManager()
        {
            _style = Style.Light;
            if (_customTheme == null)
            {
                var themePath = Properties.Settings.Default.ThemeFile;
                if (File.Exists(themePath))
                {
                    FileInfo FI = new FileInfo(themePath);
                    if (FI.Length > 0)
                        _customTheme = themePath;
                    else
                        _customTheme = ThemeFilePath(Properties.Resources.Metro_Theme);
                }
                else
                    _customTheme = ThemeFilePath(Properties.Resources.Metro_Theme);
            }
            EvaluateDicts();
        }

        #endregion

        #region Methods

        private void UpdateForm()
        {
            switch (MetroForm)
            {
                case null:
                    return;
                case iForm form when CustomTheme != null:
                    form.Style = Style;
                    form.ThemeAuthor = ThemeAuthor;
                    form.ThemeName = ThemeName;
                    form.MetroStyleManager = this;
                    break;
            }

            if (MetroForm.Controls.Count > 0)
                UpdateControls(MetroForm.Controls);

            MetroForm.Invalidate();
        }

        private void UpdateControls(Control.ControlCollection controls)
        {
            if (controls == null) throw new ArgumentNullException(nameof(controls));
            foreach (Control ctrl in controls)
            {
                var control = ctrl as iControl;
                if (control != null && CustomTheme != null)
                {
                    control.Style = Style;
                    control.ThemeAuthor = ThemeAuthor;
                    control.ThemeName = ThemeName;
                    control.MetroStyleManager = this;
                }
                if (control is TabControl tabControl)
                {
                    foreach (TabPage c in tabControl.TabPages)
                    {
                        if (c is iControl)
                        {
                            control.Style = Style;
                            control.MetroStyleManager = this;
                            control.ThemeAuthor = ThemeAuthor;
                            control.ThemeName = ThemeName;
                        }
                        UpdateControls(c.Controls);
                    }
                }

                foreach (Control child in ctrl.Controls)
                {
                    if (!(child is iControl)) continue;
                    ((iControl)child).Style = Style;
                    ((iControl)child).MetroStyleManager = this;
                    ((iControl)child).ThemeAuthor = ThemeAuthor;
                    ((iControl)child).ThemeName = ThemeName;
                }
            }
        }

        private void ControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control is iControl control && CustomTheme != null)
            {
                control.Style = Style;
                control.ThemeAuthor = ThemeAuthor;
                control.ThemeName = ThemeName;
                control.MetroStyleManager = this;
            }
            else
                UpdateForm();
        }

        #endregion

        #region Internal Vars

        private Style _style;
        private Form _metroForm;
        private string _customTheme;

        #endregion Internal Vars

        #region Properties

        [Category("Metro"), Description("Gets or sets the The Author name associated with the theme.")]
        public string ThemeAuthor { get; set; }

        [Category("Metro"), Description("Gets or sets the The Theme name associated with the theme.")]
        public string ThemeName { get; set; }

        [Category("Metro"), Description("Gets or sets the form (MetroForm) to Apply themes for.")]
        public Form MetroForm
        {
            get => _metroForm;
            set
            {
                if (_metroForm != null) return;
                _metroForm = value;
                _metroForm.ControlAdded += ControlAdded;
                UpdateForm();
            }
        }

        [Category("Metro"), Description("Gets or sets the style.")]
        public Style Style
        {
            get => _style;
            set
            {
                _style = value;
                switch (value)
                {
                    case Style.Light:
                        ThemeAuthor = "Taiizor";
                        ThemeName = "MetroLite";
                        break;
                    case Style.Dark:
                        ThemeAuthor = "Taiizor";
                        ThemeName = "MetroDark";
                        break;
                    case Style.Custom:
                        if (!string.IsNullOrEmpty(_customTheme))
                        {
                            Properties.Settings.Default.ThemeFile = _customTheme;
                            Properties.Settings.Default.Save();
                            ControlProperties(_customTheme);
                        }
                        else
                            Style = Style.Light;
                        break;
                }
                UpdateForm();
            }
        }

        [Editor(typeof(FileNamesEditor), typeof(UITypeEditor)), Category("Metro"), Description("Gets or sets the custom theme file.")]
        public string CustomTheme
        {
            get => _customTheme;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Properties.Settings.Default.ThemeFile = value;
                    Properties.Settings.Default.Save();
                    ControlProperties(value);
                    Style = Style.Custom;
                }
                else
                    Style = Style.Light;
                _customTheme = value;
            }
        }

        #endregion Properties

        #region Open Theme

        public void OpenTheme()
        {
            Style = Style.Custom;
            using (var ofd = new OpenFileDialog { Filter = "Xml File (*.xml)|*.xml" })
            {
                if (ofd.ShowDialog() != DialogResult.OK)
                    return;
                CustomTheme = ofd.FileName;
            }
        }

        public void SetTheme(string path)
        {
            Style = Style.Custom;
            CustomTheme = path;
        }

        private string ThemeFilePath(string str)
        {
            var path = $"{Environment.GetFolderPath(Environment.SpecialFolder.Templates) + @"\ThemeFile.xml"}";
            File.WriteAllText(path, str);
            return path;
        }

        #endregion Open Theme

        #region Dictionaries

        #region Declartions

        public Dictionary<string, object> ButtonDictionary;

        public Dictionary<string, object> DefaultButtonDictionary;

        public Dictionary<string, object> LabelDictionary;

        public Dictionary<string, object> LinkLabelDictionary;

        public Dictionary<string, object> TextBoxDictionary;

        public Dictionary<string, object> RichTextBoxDictionary;

        public Dictionary<string, object> ComboBoxDictionary;

        public Dictionary<string, object> FormDictionary;

        public Dictionary<string, object> BadgeDictionary;

        public Dictionary<string, object> DividerDictionary;

        public Dictionary<string, object> CheckBoxDictionary;

        public Dictionary<string, object> RadioButtonDictionary;

        public Dictionary<string, object> SwitchBoxDictionary;

        public Dictionary<string, object> ToolTipDictionary;

        public Dictionary<string, object> NumericDictionary;

        public Dictionary<string, object> EllipseDictionary;

        public Dictionary<string, object> TileDictionary;

        public Dictionary<string, object> ProgressDictionary;

        public Dictionary<string, object> ControlBoxDictionary;

        public Dictionary<string, object> TabControlDictionary;

        public Dictionary<string, object> ScrollBarDictionary;

        public Dictionary<string, object> PanelDictionary;

        public Dictionary<string, object> TrackBarDictionary;

        public Dictionary<string, object> ContextMenuDictionary;

        public Dictionary<string, object> ListBoxDictionary;

        #endregion

        #region Methods 

        private void Clear()
        {
            ButtonDictionary.Clear();
            DefaultButtonDictionary.Clear();
            FormDictionary.Clear();
            LabelDictionary.Clear();
            TextBoxDictionary.Clear();
            LabelDictionary.Clear();
            LinkLabelDictionary.Clear();
            BadgeDictionary.Clear();
            DividerDictionary.Clear();
            CheckBoxDictionary.Clear();
            RadioButtonDictionary.Clear();
            SwitchBoxDictionary.Clear();
            ToolTipDictionary.Clear();
            RichTextBoxDictionary.Clear();
            ComboBoxDictionary.Clear();
            NumericDictionary.Clear();
            EllipseDictionary.Clear();
            TileDictionary.Clear();
            ProgressDictionary.Clear();
            ControlBoxDictionary.Clear();
            TabControlDictionary.Clear();
            ScrollBarDictionary.Clear();
            PanelDictionary.Clear();
            TrackBarDictionary.Clear();
            ContextMenuDictionary.Clear();
            ListBoxDictionary.Clear();
        }

        #endregion

        #region Evaluate

        private void EvaluateDicts()
        {
            ButtonDictionary = new Dictionary<string, object>();
            DefaultButtonDictionary = new Dictionary<string, object>();
            LabelDictionary = new Dictionary<string, object>();
            LinkLabelDictionary = new Dictionary<string, object>();
            TextBoxDictionary = new Dictionary<string, object>();
            RichTextBoxDictionary = new Dictionary<string, object>();
            FormDictionary = new Dictionary<string, object>();
            BadgeDictionary = new Dictionary<string, object>();
            DividerDictionary = new Dictionary<string, object>();
            CheckBoxDictionary = new Dictionary<string, object>();
            RadioButtonDictionary = new Dictionary<string, object>();
            SwitchBoxDictionary = new Dictionary<string, object>();
            ToolTipDictionary = new Dictionary<string, object>();
            ComboBoxDictionary = new Dictionary<string, object>();
            NumericDictionary = new Dictionary<string, object>();
            EllipseDictionary = new Dictionary<string, object>();
            TileDictionary = new Dictionary<string, object>();
            ProgressDictionary = new Dictionary<string, object>();
            ControlBoxDictionary = new Dictionary<string, object>();
            TabControlDictionary = new Dictionary<string, object>();
            ScrollBarDictionary = new Dictionary<string, object>();
            PanelDictionary = new Dictionary<string, object>();
            TrackBarDictionary = new Dictionary<string, object>();
            ContextMenuDictionary = new Dictionary<string, object>();
            ListBoxDictionary = new Dictionary<string, object>();
        }

        #endregion

        #endregion

        #region Reader

        private void ControlProperties(string path)
        {
            // We clear every dictionary for avoid the "the key is already exist in dictionary" exception.

            Clear();

            // Here we refill the dictionaries with information we get in custom theme.

            FormDictionary = GetValues(path, "Form");

            ButtonDictionary = GetValues(path, "Button");

            DefaultButtonDictionary = GetValues(path, "DefaultButton");

            LabelDictionary = GetValues(path, "Label");

            LinkLabelDictionary = GetValues(path, "LinkLabel");

            BadgeDictionary = GetValues(path, "Badge");

            DividerDictionary = GetValues(path, "Divider");

            CheckBoxDictionary = GetValues(path, "CheckBox");

            RadioButtonDictionary = GetValues(path, "RadioButton");

            SwitchBoxDictionary = GetValues(path, "SwitchBox");

            ToolTipDictionary = GetValues(path, "ToolTip");

            TextBoxDictionary = GetValues(path, "TextBox");

            RichTextBoxDictionary = GetValues(path, "RichTextBox");

            ComboBoxDictionary = GetValues(path, "ComboBox");

            NumericDictionary = GetValues(path, "Numeric");

            EllipseDictionary = GetValues(path, "Ellipse");

            TileDictionary = GetValues(path, "Tile");

            ProgressDictionary = GetValues(path, "Progress");

            ControlBoxDictionary = GetValues(path, "ControlBox");

            TabControlDictionary = GetValues(path, "TabControl");

            ScrollBarDictionary = GetValues(path, "ScrollBar");

            PanelDictionary = GetValues(path, "Panel");

            TrackBarDictionary = GetValues(path, "TrackBar");

            ContextMenuDictionary = GetValues(path, "ContextMenu");

            ListBoxDictionary = GetValues(path, "ListBox");

            ThemeDetailsReader(path);

            UpdateForm();
        }

        private void ThemeDetailsReader(string path)
        {
            foreach (var item in GetValues(path, "Theme"))
            {
                if (item.Key == "Name")
                    ThemeName = item.Value.ToString();
                else if (item.Key == "Author")
                    ThemeAuthor = item.Value.ToString();
            }
        }

        private Dictionary<string, object> GetValues(string path, string nodename)
        {
            try
            {
                var dict = new Dictionary<string, object>();
                var doc = new XmlDocument();
                if (File.Exists(path))
                    doc.Load(path);
                if (doc.DocumentElement == null) { return null; }
                var xmlNode = doc.SelectSingleNode($"/MetroTheme/{nodename}");
                if (xmlNode == null) return dict;
                foreach (XmlNode node in xmlNode.ChildNodes)
                    dict.Add(node.Name, node.InnerText);

                return dict;
            }
            catch
            {
                return null;
            }
        }


        #endregion Reader

        #region UITypeEditor

        public class FileNamesEditor : UITypeEditor
        {
            private OpenFileDialog _ofd;

            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return UITypeEditorEditStyle.Modal;
            }

            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                if (context == null || provider == null) return base.EditValue(context, provider, value);
                var editorService =
                    (IWindowsFormsEditorService)
                    provider.GetService(typeof(IWindowsFormsEditorService));
                if (editorService == null) return base.EditValue(context, provider, value);
                _ofd = new OpenFileDialog
                {
                    Filter = "Xml File (*.xml)|*.xml",
                };
                return _ofd.ShowDialog() == DialogResult.OK ? _ofd.FileName : base.EditValue(context, provider, value);
            }
        }

        #endregion
    }

    #endregion
}