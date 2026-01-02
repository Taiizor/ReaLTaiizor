#region Imports

using ReaLTaiizor.Child.Poison;
using System.Diagnostics;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Properties
{
    #region PoisonMessageBoxProperties

    public class PoisonMessageBoxProperties
    {
        public PoisonMessageBoxProperties(PoisonMessageBoxControl owner)
        {
            Owner = owner;
        }

        public MessageBoxButtons Buttons
        {
            get; set;
        }

        public MessageBoxDefaultButton DefaultButton
        {
            get; set;
        }

        public MessageBoxIcon Icon
        {
            get; set;
        }

        public string Message
        {
            get; set;
        }

        [field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PoisonMessageBoxControl Owner { get; } = null;

        public string Title
        {
            get; set;
        }
    }

    #endregion
}