#region Imports

using ReaLTaiizor.Controls;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Design;
using ReaLTaiizor.Design.Poison;

#endregion

namespace ReaLTaiizor.Child.Poison
{
    #region PoisonTabPageCollectionChild

    [ToolboxItem(false)]
    [Editor(typeof(PoisonTabControlDesigner), typeof(UITypeEditor))]
    public class PoisonTabPageCollection : TabControl.TabPageCollection
    {
        public PoisonTabPageCollection(PoisonTabControl owner) : base(owner)
        { }
    }

    #endregion
}