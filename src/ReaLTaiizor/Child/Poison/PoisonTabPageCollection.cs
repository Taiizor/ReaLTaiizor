#region Imports

using ReaLTaiizor.Controls;
using ReaLTaiizor.Design.Poison;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

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