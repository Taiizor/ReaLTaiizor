#region Imports

using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using SysAction = System.Action;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotExtendedFileSystemWatcher

    public class ParrotExtendedFileSystemWatcher : Component
    {
        public ParrotExtendedFileSystemWatcher()
        {
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Attributes | NotifyFilters.Size | NotifyFilters.LastWrite | NotifyFilters.LastAccess | NotifyFilters.CreationTime | NotifyFilters.Security;
            watcher.Filter = "*.*";
            watcher.IncludeSubdirectories = true;
            watcher.Created += OnCreated;
            watcher.Changed += OnChanged;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;
            watcher.EnableRaisingEvents = false;
            watcher.InternalBufferSize = 131072;
        }

        public event EventHandler ServiceStarted;

        protected virtual void OnServiceStarted()
        {
            ServiceStarted?.Invoke(this, null);
        }

        public event EventHandler ServiceStopped;

        protected virtual void OnServiceStopped(FileSystemEventArgs e)
        {
            ServiceStopped?.Invoke(this, null);
        }

        public event FileSystemEventHandler FileCreated;

        protected virtual void OnFileCreated(FileSystemEventArgs e)
        {
            FileCreated?.Invoke(this, e);
        }

        public event FileSystemEventHandler FileDeleted;

        protected virtual void OnFileDeleted(FileSystemEventArgs e)
        {
            FileDeleted?.Invoke(this, e);
        }

        public event FileSystemEventHandler FileChanged;

        protected virtual void OnFileChanged(FileSystemEventArgs e)
        {
            FileChanged?.Invoke(this, e);
        }

        public event RenamedEventHandler FileRenamed;

        protected virtual void OnFileRenamed(RenamedEventArgs e)
        {
            FileRenamed?.Invoke(this, e);
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Choose when the watcher updates")]
        public NotifyFilters UpdateOn
        {
            get => watcher.NotifyFilter;
            set => watcher.NotifyFilter = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Watch subdirectories")]
        public bool WatchSubdirectories
        {
            get => watcher.IncludeSubdirectories;
            set => watcher.IncludeSubdirectories = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The path to watch")]
        public string WatchPath
        {
            get => watcher.Path;
            set
            {
                if (Directory.Exists(value))
                {
                    watcher.Path = value;
                    return;
                }
                if (File.Exists(value))
                {
                    watcher.Path = Path.GetDirectoryName(value);
                    watcher.Filter = Path.GetFileName(value);
                }
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Filter for certin files")]
        public string Filters
        {
            get => watcher.Filter;
            set => watcher.Filter = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The control to output to(via Text)")]
        public Control OutputControl { get; set; }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Remove WatchPath from output")]
        public bool SlimOutput { get; set; } = true;

        public void StartService()
        {
            ServiceStarted(this, null);
            if (!watcher.EnableRaisingEvents)
            {
                watcher.EnableRaisingEvents = true;
            }
        }

        public void StopService()
        {
            ServiceStopped(this, null);
            if (watcher.EnableRaisingEvents)
            {
                watcher.EnableRaisingEvents = false;
            }
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            DateTime lastWriteTime = File.GetLastWriteTime(e.FullPath);
            if (lastWriteTime != lastRead && File.Exists(e.FullPath))
            {
                lastRead = lastWriteTime;
                if (SlimOutput)
                {
                    OutputControl.Invoke(new SysAction(delegate ()
                    {
                        OutputControl.Text = OutputControl.Text + "\nChanged: " + e.FullPath.Replace(watcher.Path, "");
                    }));
                }
                else
                {
                    OutputControl.Invoke(new SysAction(delegate ()
                    {
                        OutputControl.Text = OutputControl.Text + "\nChanged: " + e.FullPath;
                    }));
                }
                OnFileChanged(e);
            }
        }

        private void OnDeleted(object source, FileSystemEventArgs e)
        {
            DateTime lastWriteTime = File.GetLastWriteTime(e.FullPath);
            lastRead = lastWriteTime;
            if (SlimOutput)
            {
                if (OutputControl.InvokeRequired)
                {
                    OutputControl.Invoke(new SysAction(delegate ()
                    {
                        OutputControl.Text = OutputControl.Text + "\nDeleted: " + e.FullPath.Replace(watcher.Path, "");
                    }));
                }
            }
            else if (OutputControl.InvokeRequired)
            {
                OutputControl.Invoke(new SysAction(delegate ()
                {
                    OutputControl.Text = OutputControl.Text + "\nDeleted: " + e.FullPath;
                }));
            }
            OnFileDeleted(e);
        }

        private void OnCreated(object source, FileSystemEventArgs e)
        {
            DateTime lastWriteTime = File.GetLastWriteTime(e.FullPath);
            if (lastWriteTime != lastRead)
            {
                lastRead = lastWriteTime;
                if (SlimOutput)
                {
                    if (OutputControl.InvokeRequired)
                    {
                        OutputControl.Invoke(new SysAction(delegate ()
                        {
                            OutputControl.Text = OutputControl.Text + "\nCreated: " + e.FullPath.Replace(watcher.Path, "");
                        }));
                    }
                }
                else if (OutputControl.InvokeRequired)
                {
                    OutputControl.Invoke(new SysAction(delegate ()
                    {
                        OutputControl.Text = OutputControl.Text + "\nCreated: " + e.FullPath;
                    }));
                }
                OnFileCreated(e);
            }
        }

        public void OnRenamed(object source, RenamedEventArgs e)
        {
            DateTime lastWriteTime = File.GetLastWriteTime(e.FullPath);
            if (lastWriteTime != lastRead)
            {
                lastRead = lastWriteTime;
                if (SlimOutput)
                {
                    if (OutputControl.InvokeRequired)
                    {
                        OutputControl.Invoke(new SysAction(delegate ()
                        {
                            OutputControl.Text = OutputControl.Text + "\nRenamed: " + e.FullPath.Replace(watcher.Path, "");
                        }));
                    }
                }
                else if (OutputControl.InvokeRequired)
                {
                    OutputControl.Invoke(new SysAction(delegate ()
                    {
                        OutputControl.Text = OutputControl.Text + "\nRenamed: " + e.FullPath;
                    }));
                }
                OnFileRenamed(e);
            }
        }

        private readonly FileSystemWatcher watcher = new();

        private DateTime lastRead = DateTime.MinValue;
    }

    #endregion
}