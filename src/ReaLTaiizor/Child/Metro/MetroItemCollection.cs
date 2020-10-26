#region Imports

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#endregion

namespace ReaLTaiizor.Child.Metro
{
    #region MetroItemCollectionChild

    public class MetroItemCollection : Collection<object>
    {
        public event EventHandler ItemUpdated;

        public delegate void EventHandler(object sender, EventArgs e);

        public void AddRange(IEnumerable<object> items)
        {
            foreach (object item in items)
            {
                Add(item);
            }
        }

        protected new void Add(object item)
        {
            base.Add(item);
            ItemUpdated?.Invoke(this, null);
        }

        protected override void InsertItem(int index, object item)
        {
            base.InsertItem(index, item);
            ItemUpdated?.Invoke(this, null);
        }

        protected override void RemoveItem(int value)
        {
            base.RemoveItem(value);
            ItemUpdated?.Invoke(this, null);
        }

        protected new void Clear()
        {
            base.Clear();
            ItemUpdated?.Invoke(this, null);
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            ItemUpdated?.Invoke(this, null);
        }
    }

    #endregion
}