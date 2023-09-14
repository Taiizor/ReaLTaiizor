#region Imports

using ReaLTaiizor.Enum.Metro;
using System;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Animate.Metro
{
    #region AnimateAnimate

    public abstract class Animate<T> : IDisposable
    {
        public Action<T> Update { get; set; }

        public MethodInvoker Complete { get; set; }

        #region Internal Vars
        private DateTime _lastUpdateTime;
        private readonly Timer _animateTimer;
        private bool _reverse;
        #endregion

        #region Constructors
        public Animate(int updateInterval = 16)
        {
            _animateTimer = new Timer()
            {
                Interval = updateInterval,
                Enabled = false,
            };
            _animateTimer.Tick += OnFrameUpdate;
            _reverse = false;
            Alpha = 0.0;
        }
        #endregion

        #region Functions
        public void Setting(int duration, T initial, T end, EasingType easing = EasingType.Linear)
        {
            InitialValue = initial;
            EndValue = end;
            EasingType = easing;
            Duration = duration;
        }

        public void Start()
        {
            _reverse = false;
            Alpha = 0.0;
            Play();
        }

        public void Back()
        {
            _reverse = true;
            Alpha = 1.0;
            Play();
        }

        public void Start(int duration)
        {
            _reverse = false;
            Alpha = 0.0;
            Duration = duration;
            Play();
        }

        public void Back(int duration)
        {
            _reverse = true;
            Alpha = 1.0;
            Duration = duration;
            Play();
        }

        public void Reverse()
        {
            Reverse(!_reverse);
        }

        public void Reverse(bool val)
        {
            _reverse = val;

            if (!Active)
            {
                Play();
            }
        }

        public void Play()
        {
            _lastUpdateTime = DateTime.Now;
            Active = true;
            _animateTimer.Enabled = true;
            _animateTimer.Start();
        }

        public void Pause()
        {
            _animateTimer.Stop();
            _animateTimer.Enabled = false;
            Active = false;
        }

        public void Stop()
        {
            Pause();
            Alpha = _reverse ? 1.0 : 0.0;
        }

        public void Start(int duration, T initial, T end, EasingType easing = EasingType.Linear)
        {
            Setting(duration, initial, end, easing);
            Start();
        }

        public void Back(int duration, T initial, T end, EasingType easing = EasingType.Linear)
        {
            Setting(duration, initial, end, easing);
            Back();
        }
        #endregion

        #region Events
        private void OnFrameUpdate(object sender, EventArgs e)
        {
            DateTime updateTime = DateTime.Now;
            double elapsed;

            if (Duration == 0)
            {
                elapsed = 1.0;
            }
            else
            {
                elapsed = (updateTime - _lastUpdateTime).TotalMilliseconds / Duration;
            }

            _lastUpdateTime = updateTime;
            Alpha = Math.Max(0.0, Math.Min(Alpha + (_reverse ? -elapsed : elapsed), 1.0));

            Update?.Invoke(Value);

            if (Alpha is not 0.0 and not 1.0)
            {
                return;
            }

            Pause();
            Complete?.Invoke();
        }
        #endregion

        #region Properties
        public double Alpha { get; set; }

        public int Duration { get; set; }

        public T InitialValue { get; private set; }

        public abstract T Value { get; }

        public T EndValue { get; private set; }

        public EasingType EasingType { get; private set; }

        public bool Active { get; private set; }

        public object Tag { get; set; }
        #endregion

        #region Dispose
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
            _animateTimer.Dispose();
        }
        #endregion
    }

    #endregion
}