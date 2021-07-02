#region Imports

using System;
using System.Drawing;

#endregion

namespace ReaLTaiizor.Child.Crown
{
    #region CrownListItemChild

    public class CrownListItem
    {
        #region Event Region

        public event EventHandler TextChanged;

        #endregion

        #region Field Region

        private string _text;

        #endregion

        #region Property Region

        public string Text
        {
            get => _text;
            set
            {
                _text = value;

                TextChanged?.Invoke(this, new EventArgs());
            }
        }

        public Rectangle Area { get; set; }

        //public Color TextColor { get; set; }

        public FontStyle FontStyle { get; set; }

        public Bitmap Icon { get; set; }

        public object Tag { get; set; }

        #endregion

        #region Constructor Region

        public CrownListItem()
        {
            //TextColor = ThemeProvider.Theme.Colors.LightText;
            FontStyle = FontStyle.Regular;
        }

        public CrownListItem(string text) : this()
        {
            Text = text;
        }

        #endregion
    }

    #endregion
}