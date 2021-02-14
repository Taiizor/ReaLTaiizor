#region Imports

using ReaLTaiizor.Controls;
using System;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Child.Poison
{
    #region PoisonTabPageCollectionEditorChild

    internal class PoisonTabPageCollectionEditor : CollectionEditor
    {
        protected override CollectionForm CreateCollectionForm()
        {
            CollectionForm baseForm = base.CreateCollectionForm();
            baseForm.Text = "PoisonTabPage Collection Editor";
            return baseForm;
        }

        public PoisonTabPageCollectionEditor(Type type) : base(type)
        { }

        protected override Type CreateCollectionItemType()
        {
            return typeof(PoisonTabPage);
        }

        protected override Type[] CreateNewItemTypes()
        {
            return new[]
            {
                typeof(PoisonTabPage)
            };
        }
    }

    #endregion
}