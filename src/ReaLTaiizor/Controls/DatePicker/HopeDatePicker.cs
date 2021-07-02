#region Imports

using ReaLTaiizor.Colors;
using ReaLTaiizor.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region HopeDatePicker

    public partial class HopeDatePicker : Control
    {
        #region General

        private RectangleF TopDateRect;
        private RectangleF WeekRect;

        private List<List<Util.HopeBase.DateRectHopeBase>> DateRectangles;

        private RectangleF PreviousMonthRect;
        private RectangleF NextMonthRect;
        private RectangleF PreviousYearRect;
        private RectangleF NextYearRect;

        private DateTime CurrentDate;
        public DateTime Date { get => CurrentDate.Date; set { CurrentDate = value; Invalidate(); } }

        private Color _SelectedTextColor = Color.White;
        public Color SelectedTextColor
        {
            get => _SelectedTextColor;
            set => _SelectedTextColor = value;
        }

        private Color _SelectedBackColor = HopeColors.PrimaryColor;
        public Color SelectedBackColor
        {
            get => _SelectedBackColor;
            set => _SelectedBackColor = value;
        }

        private Color _ValueTextColor = HopeColors.DarkPrimary;
        public Color ValueTextColor
        {
            get => _ValueTextColor;
            set => _ValueTextColor = value;
        }

        private Color _HoverColor = HopeColors.ThreeLevelBorder;
        public Color HoverColor
        {
            get => _HoverColor;
            set => _HoverColor = value;
        }

        private Color _DayTextColorA = HopeColors.MainText;
        public Color DayTextColorA
        {
            get => _DayTextColorA;
            set => _DayTextColorA = value;
        }

        private Color _DayTextColorB = HopeColors.SecondaryText;
        public Color DayTextColorB
        {
            get => _DayTextColorB;
            set => _DayTextColorB = value;
        }

        private Color _HeadLineColor = HopeColors.TwoLevelBorder;
        public Color HeadLineColor
        {
            get => _HeadLineColor;
            set => _HeadLineColor = value;
        }

        private Color _DaysTextColor = HopeColors.RegularText;
        public Color DaysTextColor
        {
            get => _DaysTextColor;
            set => _DaysTextColor = value;
        }

        private Color _BorderColor = HopeColors.OneLevelBorder;
        public Color BorderColor
        {
            get => _BorderColor;
            set => _BorderColor = value;
        }

        private Color _HeaderTextColor = HopeColors.MainText;
        public Color HeaderTextColor
        {
            get => _HeaderTextColor;
            set => _HeaderTextColor = value;
        }

        private Color _PYHoverColor = HopeColors.PrimaryColor;
        public Color PYHoverColor
        {
            get => _PYHoverColor;
            set => _PYHoverColor = value;
        }

        private Color _PYColor = HopeColors.PlaceholderText;
        public Color PYColor
        {
            get => _PYColor;
            set => _PYColor = value;
        }

        private Color _NYHoverColor = HopeColors.PrimaryColor;
        public Color NYHoverColor
        {
            get => _NYHoverColor;
            set => _NYHoverColor = value;
        }

        private Color _NYColor = HopeColors.PlaceholderText;
        public Color NYColor
        {
            get => _NYColor;
            set => _NYColor = value;
        }

        private Color _PMHoverColor = HopeColors.PrimaryColor;
        public Color PMHoverColor
        {
            get => _PMHoverColor;
            set => _PMHoverColor = value;
        }

        private Color _PMColor = HopeColors.PlaceholderText;
        public Color PMColor
        {
            get => _PMColor;
            set => _PMColor = value;
        }

        private Color _NMHoverColor = HopeColors.PrimaryColor;
        public Color NMHoverColor
        {
            get => _NMHoverColor;
            set => _NMHoverColor = value;
        }

        private Color _NMColor = HopeColors.PlaceholderText;
        public Color NMColor
        {
            get => _NMColor;
            set => _NMColor = value;
        }

        private string _HeaderFormat = "{0} Y - {1} M"; //"{0} Y - {1,2} M"
        public string HeaderFormat
        {
            get => _HeaderFormat;
            set
            {
                try
                {
                    if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) || value.Length < 9)
                    {
                        _HeaderFormat = "{0} Y - {1} M";
                    }
                    else
                    {
                        string.Format(value, CurrentDate.Year, CurrentDate.Month);
                        _HeaderFormat = value;
                    }
                }
                catch
                {
                    _HeaderFormat = "{0} Y - {1} M";
                }
            }
        }

        private string _DayNames = "MTWTFSS";
        public string DayNames
        {
            get => _DayNames;
            set
            {
                if (value.Length == 7)
                {
                    _DayNames = value;
                }
                else if (value.Length > 7)
                {
                    _DayNames = value.Substring(0, 7);
                }
                else
                {
                    _DayNames = "MTWTFSS";
                }
            }
        }

        private readonly int DateRectDefaultSize;
        private int HoverX;
        private int HoverY;
        private int SelectedX;
        private int SelectedY;

        private bool previousYearHovered;
        private bool previousMonthHovered;
        private bool nextMonthHovered;
        private bool nextYearHovered;

        #endregion

        #region Variables
        public delegate void DateChanged(DateTime newDateTime);
        public event DateChanged onDateChanged;
        #endregion

        #region Events
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (DateRectangles[i][j].Drawn)
                    {
                        if (DateRectangles[i][j].Rect.Contains(e.Location))
                        {
                            if (HoverX != i || HoverY != j)
                            {
                                HoverX = i;
                                previousYearHovered = false;
                                nextYearHovered = false;
                                HoverY = j;
                                Invalidate();
                            }
                            return;
                        }
                    }
                }
            }

            if (PreviousYearRect.Contains(e.Location))
            {
                previousYearHovered = true;
                HoverX = -1;
                Invalidate();
                return;
            }
            if (PreviousMonthRect.Contains(e.Location))
            {
                previousMonthHovered = true;
                HoverX = -1;
                Invalidate();
                return;
            }
            if (NextMonthRect.Contains(e.Location))
            {
                nextMonthHovered = true;
                HoverX = -1;
                Invalidate();
                return;
            }
            if (NextYearRect.Contains(e.Location))
            {
                nextYearHovered = true;
                HoverX = -1;
                Invalidate();
                return;
            }
            if (HoverX >= 0 || previousYearHovered || previousMonthHovered || nextMonthHovered || nextYearHovered)
            {
                HoverX = -1;
                previousYearHovered = false;
                previousMonthHovered = false;
                nextMonthHovered = false;
                nextYearHovered = false;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (HoverX >= 0)
            {
                SelectedX = HoverX;
                SelectedY = HoverY;
                CurrentDate = DateRectangles[SelectedX][SelectedY].Date;
                Invalidate();
                onDateChanged?.Invoke(CurrentDate);
                return;
            }

            if (PreviousYearRect.Contains(e.Location))
            {
                CurrentDate = FirstDayOfMonth(CurrentDate.AddYears(-1));
            }

            if (PreviousMonthRect.Contains(e.Location))
            {
                CurrentDate = FirstDayOfMonth(CurrentDate.AddMonths(-1));
            }

            if (NextMonthRect.Contains(e.Location))
            {
                CurrentDate = FirstDayOfMonth(CurrentDate.AddMonths(1));
            }

            if (NextYearRect.Contains(e.Location))
            {
                CurrentDate = FirstDayOfMonth(CurrentDate.AddYears(1));
            }

            CalculateRectangles();
            Invalidate();
            onDateChanged?.Invoke(CurrentDate);
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            HoverX = -1;
            HoverY = -1;
            previousYearHovered = false;
            previousMonthHovered = false;
            nextMonthHovered = false;
            nextYearHovered = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        #endregion

        private void CalculateRectangles()
        {
            DateRectangles = new List<List<Util.HopeBase.DateRectHopeBase>>();
            for (int i = 0; i < 7; i++)
            {
                DateRectangles.Add(new List<Util.HopeBase.DateRectHopeBase>());
                for (int j = 0; j < 7; j++)
                {
                    DateRectangles[i].Add(new Util.HopeBase.DateRectHopeBase(new RectangleF(10 + (j * (Width - 20) / 7), WeekRect.Y + WeekRect.Height + (i * DateRectDefaultSize), DateRectDefaultSize, DateRectDefaultSize)));
                }
            }
            DateTime FirstDay = FirstDayOfMonth(CurrentDate);
            int temp = 0;
            for (int i = FirstDay.DayOfWeek == DayOfWeek.Sunday ? 6 : (int)FirstDay.DayOfWeek - 1; i > 0; i--, temp++)
            {
                DateRectangles[temp / 7][temp % 7].Drawn = false;
                DateRectangles[temp / 7][temp % 7].Date = FirstDay.AddDays(0 - i);
            }
            for (DateTime date = FirstDay; date <= LastDayOfMonth(CurrentDate); date = date.AddDays(1), temp++)
            {
                if (date.DayOfYear == CurrentDate.DayOfYear && date.Year == CurrentDate.Year)
                {
                    SelectedX = temp / 7;
                    SelectedY = temp % 7;
                }
                DateRectangles[temp / 7][temp % 7].Drawn = true;
                DateRectangles[temp / 7][temp % 7].Date = date;
            }
            DateTime LastDay = LastDayOfMonth(CurrentDate);
            for (int i = 0; temp < 42; i++, temp++)
            {
                DateRectangles[temp / 7][temp % 7].Drawn = false;
                DateRectangles[temp / 7][temp % 7].Date = LastDay.AddDays(i + 1);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            Width = 250;
            Height = 270;
        }

        public HopeDatePicker()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            Width = 250;
            Height = 260;

            BackColor = Color.White;

            DateRectDefaultSize = (Width - 20) / 7;
            TopDateRect = new(20, 5, Width - 40, DateRectDefaultSize);
            WeekRect = new(0, TopDateRect.Y + TopDateRect.Height, DateRectDefaultSize, DateRectDefaultSize);

            PreviousYearRect = new(10, TopDateRect.Y, 20, DateRectDefaultSize);
            PreviousMonthRect = new(35, TopDateRect.Y + 1, 20, DateRectDefaultSize);
            NextMonthRect = new(Width - 55, TopDateRect.Y + 1, 20, DateRectDefaultSize);
            NextYearRect = new(Width - 30, TopDateRect.Y, 20, DateRectDefaultSize);

            CurrentDate = DateTime.Now;

            HoverX = -1;
            HoverY = -1;
            CalculateRectangles();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graphics = e.Graphics;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(Parent.BackColor);

            GraphicsPath bg = RoundRectangle.CreateRoundRect(1f, 1f, Width - 2, Height - 2, 3);
            graphics.FillPath(new SolidBrush(BackColor), bg);
            graphics.DrawPath(new(_BorderColor), bg);

            graphics.DrawString(string.Format(_HeaderFormat, CurrentDate.Year, CurrentDate.Month), new Font("Segoe UI", 12f), new SolidBrush(_HeaderTextColor), TopDateRect, HopeStringAlign.Center);

            graphics.DrawString("7", new Font("webdings", 12f), new SolidBrush(previousYearHovered ? _PYHoverColor : _PYColor), PreviousYearRect, HopeStringAlign.Center);
            graphics.DrawString("3", new Font("webdings", 12f), new SolidBrush(previousMonthHovered ? _PMHoverColor : _PMColor), PreviousMonthRect, HopeStringAlign.Center);
            graphics.DrawString("4", new Font("webdings", 12f), new SolidBrush(nextMonthHovered ? _NMHoverColor : _NMColor), NextMonthRect, HopeStringAlign.Center);
            graphics.DrawString("8", new Font("webdings", 12f), new SolidBrush(nextYearHovered ? _NYHoverColor : _NYColor), NextYearRect, HopeStringAlign.Center);

            string s = _DayNames;
            for (int i = 0; i < 7; i++)
            {
                graphics.DrawString(s[i].ToString(), new Font("Segoe UI", 10f), new SolidBrush(_DaysTextColor), new RectangleF(10 + i * (Width - 20) / 7, WeekRect.Y, WeekRect.Width, WeekRect.Height), HopeStringAlign.Center);
            }

            graphics.DrawLine(new(_HeadLineColor, 0.5f), 10, WeekRect.Y + WeekRect.Height, Width - 10, WeekRect.Y + WeekRect.Height);

            //DateTime FirstDay = FirstDayOfMonth(CurrentDate);
            for (int i = 0; i < 42; i++)
            {
                Util.HopeBase.DateRectHopeBase tempDate = DateRectangles[i / 7][i % 7];
                SolidBrush brush = new(_DayTextColorA);

                if (HoverX == i / 7 && HoverY == i % 7)
                {
                    RectangleF rect1 = tempDate.Rect;
                    GraphicsPath bg1 = RoundRectangle.CreateRoundRect(rect1.X + 2, rect1.Y + 2, rect1.Width - 4, rect1.Width - 4, 3);
                    graphics.FillPath(new SolidBrush(_HoverColor), bg1);
                    //graphics.FillRectangle(new SolidBrush(HopeColors.ThreeLevelBorder), new RectangleF(rect1.X + 3, rect1.Y + 3, rect1.Width - 6, rect1.Width - 6));
                }

                if (tempDate.Date == DateTime.Today)
                {
                    brush = new(_ValueTextColor);
                }

                if (tempDate.Date == Date)
                {
                    RectangleF rect1 = tempDate.Rect;
                    GraphicsPath bg1 = RoundRectangle.CreateRoundRect(rect1.X + 2, rect1.Y + 2, rect1.Width - 4, rect1.Width - 4, 3);
                    graphics.FillPath(new SolidBrush(_SelectedBackColor), bg1);

                    //graphics.FillRectangle(new SolidBrush(HopeColors.PrimaryColor), new RectangleF(rect1.X+3,rect1.Y+3,rect1.Width-6,rect1.Width-6));
                    brush = new(_SelectedTextColor);
                }
                graphics.DrawString(DateRectangles[i / 7][i % 7].Date.Day.ToString(), Font, DateRectangles[i / 7][i % 7].Drawn ? brush : new SolidBrush(_DayTextColorB), DateRectangles[i / 7][i % 7].Rect, HopeStringAlign.Center);
            }
        }

        public static DateTime FirstDayOfMonth(DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        public DateTime LastDayOfMonth(DateTime value)
        {
            return new DateTime(value.Year, value.Month, DateTime.DaysInMonth(value.Year, value.Month));
        }
    }

    #endregion
}