#region Imports

using ReaLTaiizor.Enum.Poison;
using System;
using System.Collections;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Util
{
    #region PoisonUtil

    #region PoisonDefaults

    internal static class PoisonDefaults
    {
        public const ColorStyle Style = ColorStyle.Blue;
        public const ThemeStyle Theme = ThemeStyle.Light;

        public static class PropertyCategory
        {
            public const string Appearance = "Poison Appearance";
            public const string Behaviour = "Poison Behaviour";
        }
    }

    #endregion

    #region HiddenTabClass
    public class HiddenTabs
    {
        public HiddenTabs(int id, string page)
        {
            Index = id;
            Tabpage = page;
        }

        public int Index { get; }

        public string Tabpage { get; }
    }
    #endregion HiddenTabClass

    #region ListViewColumnSorter

    //namespace System.Runtime.CompilerServices
    //{
    //    public class ExtensionAttribute : Attribute { }
    //}

    //public static class ControlExtensions
    //{
    //    public static void DoubleBuffering(this Control control, bool enable)
    //    {
    //        var method = typeof(Control).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
    //        method.Invoke(control, new object[] { ControlStyles.OptimizedDoubleBuffer, enable });
    //    }
    //}

    public class ListViewColumnSorter : IComparer
    {
        public int ColumnToSort;

        public SortOrder OrderOfSort;

        private readonly CaseInsensitiveComparer ObjectCompare;

        public SortModifiersType _SortModifier { set; get; } = SortModifiersType.SortByText;

        public ListViewColumnSorter()
        {
            // Initialize the column to '0'
            ColumnToSort = 0;

            // Initialize the CaseInsensitiveComparer object
            ObjectCompare = new CaseInsensitiveComparer();
        }

        public int Compare(object x, object y)
        {
            ListViewItem listviewX, listviewY;

            // Cast the objects to be compared to ListViewItem objects
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;


            int compareResult;
            if (DateTime.TryParse(listviewX.SubItems[ColumnToSort].Text, out DateTime dateX) && DateTime.TryParse(listviewY.SubItems[ColumnToSort].Text, out DateTime dateY))
            {
                compareResult = ObjectCompare.Compare(dateX, dateY);
            }
            else
            {
                compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);
            }

            // Calculate correct return value based on object comparison
            if (OrderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return -compareResult;
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }

        public int SortColumn
        {
            set => ColumnToSort = value;
            get => ColumnToSort;
        }

        public SortOrder Order
        {
            set => OrderOfSort = value;
            get => OrderOfSort;
        }
    }

    #endregion

    #endregion
}