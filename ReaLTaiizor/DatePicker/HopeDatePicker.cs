#region Imports

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

#endregion

namespace ReaLTaiizor
{
    #region HopeDatePicker

    public partial class HopeDatePicker : Control
    {
        private RectangleF TopDateRect;
        private RectangleF WeekRect;

        private List<List<HopeBase.DateRectHopeBase>> DateRectangles;

        private RectangleF PreviousMonthRect;
        private RectangleF NextMonthRect;
        private RectangleF PreviousYearRect;
        private RectangleF NextYearRect;

        private DateTime CurrentDate;
        public DateTime Date { get { return CurrentDate.Date; } set { CurrentDate = value; Invalidate(); } }

        private Color _SelectedTextColor = Color.White;
        private Color _SelectedBackColor = HopeColors.PrimaryColor;
        private Color _ValueTextColor = HopeColors.DarkPrimary;
        private Color _HoverColor = HopeColors.ThreeLevelBorder;
        private Color _DayTextColorA = HopeColors.MainText;
        private Color _DayTextColorB = HopeColors.MainText; ////////////
        private Color _HeadLineColor = HopeColors.TwoLevelBorder;
        private Color _DaysTextColor = HopeColors.RegularText;
        private Color _BorderColor = HopeColors.OneLevelBorder;
        private Color _HeaderTextColor = HopeColors.MainText;

        private string _HeaderFormat = "{0} Y - {1} M"; //"{0} Y - {1,2} M"
        public string HeaderFormat
        {
            get
            {
                return _HeaderFormat;
            }
            set
            {
                try
                {
                    if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) || value.Length < 9)
                        _HeaderFormat = "{0} Y - {1} M";
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
            get
            {
                return _DayNames;
            }
            set
            {
                if (value.Length == 7)
                    _DayNames = value;
                else if (value.Length > 7)
                    _DayNames = value.Substring(0, 7);
                else
                    _DayNames = "MTWTFSS";
            }
        }

        private int DateRectDefaultSize;
        private int HoverX;
        private int HoverY;
        private int SelectedX;
        private int SelectedY;

        private bool previousYearHovered;
        private bool previousMonthHovered;
        private bool nextMonthHovered;
        private bool nextYearHovered;

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
                CurrentDate = FirstDayOfMonth(CurrentDate.AddYears(-1));
            if (PreviousMonthRect.Contains(e.Location))
                CurrentDate = FirstDayOfMonth(CurrentDate.AddMonths(-1));
            if (NextMonthRect.Contains(e.Location))
                CurrentDate = FirstDayOfMonth(CurrentDate.AddMonths(1));
            if (NextYearRect.Contains(e.Location))
                CurrentDate = FirstDayOfMonth(CurrentDate.AddYears(1));
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
            DateRectangles = new List<List<HopeBase.DateRectHopeBase>>();
            for (int i = 0; i < 7; i++)
            {
                DateRectangles.Add(new List<HopeBase.DateRectHopeBase>());
                for (int j = 0; j < 7; j++)
                    DateRectangles[i].Add(new HopeBase.DateRectHopeBase(new RectangleF(10 + (j * (Width - 20) / 7), WeekRect.Y + WeekRect.Height + (i * DateRectDefaultSize), DateRectDefaultSize, DateRectDefaultSize)));
            }
            DateTime FirstDay = FirstDayOfMonth(CurrentDate);
            var temp = 0;
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
            TopDateRect = new RectangleF(20, 5, Width - 40, DateRectDefaultSize);
            WeekRect = new RectangleF(0, TopDateRect.Y + TopDateRect.Height, DateRectDefaultSize, DateRectDefaultSize);

            PreviousYearRect = new RectangleF(10, TopDateRect.Y, 20, DateRectDefaultSize);
            PreviousMonthRect = new RectangleF(35, TopDateRect.Y + 1, 20, DateRectDefaultSize);
            NextMonthRect = new RectangleF(Width - 55, TopDateRect.Y + 1, 20, DateRectDefaultSize);
            NextYearRect = new RectangleF(Width - 30, TopDateRect.Y, 20, DateRectDefaultSize);

            CurrentDate = DateTime.Now;

            HoverX = -1;
            HoverY = -1;
            CalculateRectangles();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var graphics = e.Graphics;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(Parent.BackColor);

            var bg = RoundRectangle.CreateRoundRect(1f, 1f, Width - 2, Height - 2, 3);
            graphics.FillPath(new SolidBrush(BackColor), bg);
            graphics.DrawPath(new Pen(_BorderColor), bg);

            graphics.DrawString(string.Format(_HeaderFormat, CurrentDate.Year, CurrentDate.Month), new Font("Segoe UI", 12f), new SolidBrush(_HeaderTextColor), TopDateRect, HopeStringAlign.Center);

            graphics.DrawString("7", new Font("webdings", 12f), new SolidBrush(previousYearHovered ? HopeColors.PrimaryColor : HopeColors.PlaceholderText), PreviousYearRect, HopeStringAlign.Center);
            graphics.DrawString("3", new Font("webdings", 12f), new SolidBrush(previousMonthHovered ? HopeColors.PrimaryColor : HopeColors.PlaceholderText), PreviousMonthRect, HopeStringAlign.Center);
            graphics.DrawString("4", new Font("webdings", 12f), new SolidBrush(nextMonthHovered ? HopeColors.PrimaryColor : HopeColors.PlaceholderText), NextMonthRect, HopeStringAlign.Center);
            graphics.DrawString("8", new Font("webdings", 12f), new SolidBrush(nextYearHovered ? HopeColors.PrimaryColor : HopeColors.PlaceholderText), NextYearRect, HopeStringAlign.Center);

            string s = _DayNames;
            for (int i = 0; i < 7; i++)
                graphics.DrawString(s[i].ToString(), new Font("Segoe UI", 10f), new SolidBrush(_DaysTextColor), new RectangleF(10 + i * (Width - 20) / 7, WeekRect.Y, WeekRect.Width, WeekRect.Height), HopeStringAlign.Center);

            graphics.DrawLine(new Pen(_HeadLineColor, 0.5f), 10, WeekRect.Y + WeekRect.Height, Width - 10, WeekRect.Y + WeekRect.Height);

            DateTime FirstDay = FirstDayOfMonth(CurrentDate);
            for (int i = 0; i < 42; i++)
            {
                var tempDate = DateRectangles[i / 7][i % 7];
                var brush = new SolidBrush(_DayTextColorA);

                if (HoverX == i / 7 && HoverY == i % 7)
                {
                    var rect1 = tempDate.Rect;
                    var bg1 = RoundRectangle.CreateRoundRect(rect1.X + 2, rect1.Y + 2, rect1.Width - 4, rect1.Width - 4, 3);
                    graphics.FillPath(new SolidBrush(_HoverColor), bg1);
                    //graphics.FillRectangle(new SolidBrush(HopeColors.ThreeLevelBorder), new RectangleF(rect1.X + 3, rect1.Y + 3, rect1.Width - 6, rect1.Width - 6));
                }

                if (tempDate.Date == DateTime.Today)
                    brush = new SolidBrush(_ValueTextColor);

                if (tempDate.Date == Date)
                {
                    var rect1 = tempDate.Rect;
                    var bg1 = RoundRectangle.CreateRoundRect(rect1.X + 2, rect1.Y + 2, rect1.Width - 4, rect1.Width - 4, 3);
                    graphics.FillPath(new SolidBrush(_SelectedBackColor), bg1);

                    //graphics.FillRectangle(new SolidBrush(HopeColors.PrimaryColor), new RectangleF(rect1.X+3,rect1.Y+3,rect1.Width-6,rect1.Width-6));
                    brush = new SolidBrush(_SelectedTextColor);
                }
                graphics.DrawString(DateRectangles[i / 7][i % 7].Date.Day.ToString(), Font, DateRectangles[i / 7][i % 7].Drawn ? brush : new SolidBrush(HopeColors.SecondaryText), DateRectangles[i / 7][i % 7].Rect, HopeStringAlign.Center);
            }
        }

        public DateTime FirstDayOfMonth(DateTime value)
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