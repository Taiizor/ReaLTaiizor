#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
	#region ForeverLabel

	public class ForeverLabel : System.Windows.Forms.Label
	{
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			Invalidate();
		}

		public ForeverLabel()
		{
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			Font = new Font("Segoe UI", 8);
			ForeColor = Color.LightGray;
			BackColor = Color.Transparent;
			Text = Text;
		}
	}

	#endregion
}