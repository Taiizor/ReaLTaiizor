#region Imports

using System.Diagnostics;
using System.Windows.Forms;
using ReaLTaiizor.Child.Poison;

#endregion

namespace ReaLTaiizor.Properties
{
    #region PoisonMessageBoxProperties

    public class PoisonMessageBoxProperties
    {
        public PoisonMessageBoxProperties(PoisonMessageBoxControl owner)
        {
            _owner = owner;
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

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private PoisonMessageBoxControl _owner = null;

        public PoisonMessageBoxControl Owner
        {
            get
            {
                return _owner;
            }
        }

        public string Title
        {
            get; set;
        }
    }

    #endregion
}