#region Imports

using System.ComponentModel;
using static ReaLTaiizor.Util.CyberLibrary;

#endregion

namespace ReaLTaiizor.Child.Cyber
{
    #region CyberControllerChild

    public partial class CyberController : Component
    {
        #region Property Region

        [Category("Cyber")]
        [Description("Enable/Disable global RGB mode for all Cyber controls")]
        public bool Status
        {
            get;
            set
            {
                field = value;
                DrawEngine.TimerGlobalRGB(field);
            }
        } = false;

        [Category("Cyber")]
        [Description("RGB Timer Update Interval")]
        public int TimerInterval
        {
            get => DrawEngine.GlobalRGB.Interval;
            set => DrawEngine.GlobalRGB.Interval = value;
        }

        #endregion

        #region Constructor Region

        public CyberController(IContainer Container)
        {
            Container.Add(this);
        }

        #endregion
    }

    #endregion
}