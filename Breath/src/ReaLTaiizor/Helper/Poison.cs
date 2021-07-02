#region Imports

using ReaLTaiizor.Controls;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Helper
{
    #region MaterialHelper

    public class PoisonDataGridHelper
    {
        private readonly PoisonScrollBar _scrollbar;

        private readonly DataGridView _grid;

        private int _ignoreScrollbarChange = 0;

        private readonly bool _ishorizontal = false;
        private readonly HScrollBar hScrollbar = null;
        private readonly VScrollBar vScrollbar = null;

        public PoisonDataGridHelper(PoisonScrollBar scrollbar, DataGridView grid) : this(scrollbar, grid, true)
        { }

        public PoisonDataGridHelper(PoisonScrollBar scrollbar, DataGridView grid, bool vertical)
        {
            _scrollbar = scrollbar;
            _scrollbar.UseBarColor = true;
            _grid = grid;
            _ishorizontal = !vertical;

            foreach (object item in _grid.Controls)
            {
                if (item.GetType() == typeof(VScrollBar))
                {
                    vScrollbar = (VScrollBar)item;
                }

                if (item.GetType() == typeof(HScrollBar))
                {
                    hScrollbar = (HScrollBar)item;
                }
            }

            _grid.RowsAdded += new DataGridViewRowsAddedEventHandler(_grid_RowsAdded);
            _grid.UserDeletedRow += new DataGridViewRowEventHandler(_grid_UserDeletedRow);
            _grid.Scroll += new ScrollEventHandler(_grid_Scroll);
            _grid.Resize += new EventHandler(_grid_Resize);
            _scrollbar.Scroll += _scrollbar_Scroll;
            _scrollbar.ScrollbarSize = 21;

            UpdateScrollbar();
        }

        private void _grid_Scroll(object sender, ScrollEventArgs e)
        {
            UpdateScrollbar();
        }

        private void _grid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            UpdateScrollbar();
        }

        private void _grid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            UpdateScrollbar();
        }

        private void _scrollbar_Scroll(object sender, ScrollEventArgs e)
        {
            if (_ignoreScrollbarChange > 0)
            {
                return;
            }

            if (_ishorizontal)
            {
                try
                {
                    hScrollbar.Value = _scrollbar.Value;
                    _grid.HorizontalScrollingOffset = _scrollbar.Value;
                }
                catch { }
            }
            else
            {

                try
                {
                    int firstDisplayedRowIndex = 0;

                    if (_scrollbar.Value >= 0 && _scrollbar.Value < _grid.Rows.Count)
                    {
                        firstDisplayedRowIndex = _scrollbar.Value + (_scrollbar.Value == 1 ? -1 : 1) >= _grid.Rows.Count ? _grid.Rows.Count - 1 : _scrollbar.Value + (_scrollbar.Value == 1 ? -1 : 1);
                    }
                    else
                    {
                        firstDisplayedRowIndex = _scrollbar.Value - 1;
                    }

                    while (!_grid.Rows[firstDisplayedRowIndex].Visible)
                    {
                        if (firstDisplayedRowIndex < 1)
                        {
                            firstDisplayedRowIndex = 0;
                        }
                        else
                        {
                            firstDisplayedRowIndex -= 1;
                        }
                    }

                    _grid.FirstDisplayedScrollingRowIndex = firstDisplayedRowIndex;
                }
                catch (Exception)
                {
                    _grid.FirstDisplayedScrollingRowIndex = 0;
                }

            }

            _grid.Invalidate();
        }

        private void BeginIgnoreScrollbarChangeEvents()
        {
            _ignoreScrollbarChange++;
        }

        private void EndIgnoreScrollbarChangeEvents()
        {
            if (_ignoreScrollbarChange > 0)
            {
                _ignoreScrollbarChange--;
            }
        }

        public void UpdateScrollbar()
        {
            if (_grid == null)
            {
                return;
            }

            try
            {
                BeginIgnoreScrollbarChangeEvents();

                if (_ishorizontal)
                {
                    //int visibleCols = VisibleFlexGridCols();
                    _scrollbar.Maximum = hScrollbar.Maximum;
                    _scrollbar.Minimum = hScrollbar.Minimum;
                    _scrollbar.SmallChange = hScrollbar.SmallChange;
                    _scrollbar.LargeChange = hScrollbar.LargeChange;
                    _scrollbar.Location = new(0, _grid.Height - _scrollbar.ScrollbarSize);
                    _scrollbar.Width = _grid.Width - (vScrollbar.Visible ? _scrollbar.ScrollbarSize : 0);
                    _scrollbar.BringToFront();
                    _scrollbar.Visible = hScrollbar.Visible;
                    _scrollbar.Value = hScrollbar.Value == 0 ? 1 : hScrollbar.Value;
                }
                else
                {
                    int visibleRows = VisibleFlexGridRows();
                    _scrollbar.Maximum = _grid.RowCount;
                    _scrollbar.Minimum = 1;
                    _scrollbar.SmallChange = 1;
                    _scrollbar.LargeChange = Math.Max(1, visibleRows - 1);
                    _scrollbar.Value = _grid.FirstDisplayedScrollingRowIndex;
                    if (_grid.RowCount > 0 && _grid.Rows[_grid.RowCount - 1].Cells[0].Displayed)
                    {
                        _scrollbar.Value = _grid.RowCount;
                    }

                    _scrollbar.Location = new(_grid.Width - _scrollbar.ScrollbarSize, 0);
                    _scrollbar.Height = _grid.Height - (hScrollbar.Visible ? _scrollbar.ScrollbarSize : 0);
                    _scrollbar.BringToFront();
                    _scrollbar.Visible = vScrollbar.Visible;
                }
            }
            finally
            {
                EndIgnoreScrollbarChangeEvents();
            }
        }

        private int VisibleFlexGridRows()
        {
            return _grid.DisplayedRowCount(true);
        }

        private int VisibleFlexGridCols()
        {
            return _grid.DisplayedColumnCount(true);
        }

        public bool VisibleVerticalScroll()
        {
            bool _return = false;

            if (_grid.DisplayedRowCount(true) < _grid.RowCount + (_grid.RowHeadersVisible ? 1 : 0))
            {
                _return = true;
            }

            return _return;
        }

        public bool VisibleHorizontalScroll()
        {
            bool _return = false;

            if (_grid.DisplayedColumnCount(true) < _grid.ColumnCount + (_grid.ColumnHeadersVisible ? 1 : 0))
            {
                _return = true;
            }

            return _return;
        }

        #region Events of interest

        private void _grid_Resize(object sender, EventArgs e)
        {
            UpdateScrollbar();
        }

        private void _grid_AfterDataRefresh(object sender, ListChangedEventArgs e)
        {
            UpdateScrollbar();
        }
        #endregion
    }

    #endregion
}