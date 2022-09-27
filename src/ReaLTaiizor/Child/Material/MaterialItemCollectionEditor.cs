#region Imports

using System;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Child.Material
{
    #region MaterialItemCollectionEditor

    public class MaterialItemCollectionEditor : CollectionEditor
    {
        public MaterialItemCollectionEditor() : base(typeof(MaterialItemCollection))
        {

        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(MaterialListBoxItem);
        }

        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] {
            typeof(MaterialListBoxItem)
         };
        }
    }

    #endregion
}