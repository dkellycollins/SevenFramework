using System;
using System.Threading;

namespace Seven
{
  public class BackgroundThread
  {
    public class RecreateEventArgs : EventArgs
    {
      private object argument;

      public object Argument { get { return this.argument; } }

      public RecreateEventArgs(object argument) { this.argument = argument; }
    }

    public class RunEventArgs : EventArgs
    {
      private object result;
      private object argument;
      private bool cancel;

      public object Argument { get { return this.argument; } }
      public object Result { get { return this.result; } set { this.result = value; } }
      public bool Cancel { get { return this.cancel; } set { this.cancel = value; } }

      public RunEventArgs(object argument) { this.argument = argument; }
    }

    public class ReportEventArgs : EventArgs
    {
      private readonly int progressPercentage;
      private readonly object userState;

      public int ProgressPercentage { get { return this.progressPercentage; } }

      public object UserState { get { return this.userState; } }

      public ReportEventArgs(int progressPercentage, object userState)
      {
        this.progressPercentage = progressPercentage;
        this.userState = userState;
      }
    }

    public class ResultEventArgs : EventArgs
    {
      private readonly Exception error;
      private readonly bool cancelled;
      private readonly object userState;

      public bool Cancelled { get { return this.cancelled; } }
      public Exception Error { get { return this.error; } }
      public object UserState { get { return this.userState; } }

      private object result;
      public object Result { get { this.RaiseExceptionIfNecessary(); return this.result; } }

      public ResultEventArgs(object result, Exception error, bool cancelled)
      {
        this.result = result;
        this.error = error;
        this.cancelled = cancelled;
        this.userState = (object)null;
      }

      protected void RaiseExceptionIfNecessary()
      {
        if (this.Error != null)
          throw new BackgroundThreadException("BS");
        if (this.Cancelled)
          throw new BackgroundThreadException("BS");
      }
    }

    public sealed class AsyncOperation
    {
      private SynchronizationContext syncContext;
      private object userSuppliedState;
      private bool alreadyCompleted;

      public object UserSuppliedState { get { return this.userSuppliedState; } }

      public SynchronizationContext SynchronizationContext { get { return this.syncContext; } }

      private AsyncOperation(object userSuppliedState, SynchronizationContext syncContext)
      {
        this.userSuppliedState = userSuppliedState;
        this.syncContext = syncContext;
        this.alreadyCompleted = false;
        this.syncContext.OperationStarted();
      }

      ~AsyncOperation()
      {
        if (this.alreadyCompleted || this.syncContext == null)
          return;
        this.syncContext.OperationCompleted();
      }

      public void Post(SendOrPostCallback d, object arg)
      {
        VerifyNotCompleted();
        VerifyDelegateNotNull(d);
        syncContext.Post(d, arg);
      }

      public void PostOperationCompleted(SendOrPostCallback d, object arg)
      {
        this.Post(d, arg);
        this.OperationCompletedCore();
      }

      public void OperationCompleted()
      {
        this.VerifyNotCompleted();
        this.OperationCompletedCore();
      }

      internal static AsyncOperation CreateOperation(object userSuppliedState, SynchronizationContext syncContext)
      {
        return new AsyncOperation(userSuppliedState, syncContext);
      }

      private void OperationCompletedCore()
      {
        try { this.syncContext.OperationCompleted(); }
        finally
        {
          this.alreadyCompleted = true;
          GC.SuppressFinalize((object)this);
        }
      }

      private void VerifyNotCompleted()
      {
        if (this.alreadyCompleted)
          throw new Exception("No hacks for you.");
      }

      private void VerifyDelegateNotNull(SendOrPostCallback d)
      {
        if (d == null)
          throw new Exception("No hacks for you.");
      }
    }

    public delegate void RecreateEventHandler(object sender, RecreateEventArgs e);
    public delegate void RunEventHandler(object sender, RunEventArgs e);
    public delegate void ReportEventHandler(object sender, ReportEventArgs e);
    public delegate void ResultEventHandler(object sender, ResultEventArgs e);

    public event RecreateEventHandler Recreate { add { _recreateEventHandler = value; } remove { _recreateEventHandler = null; } }
    public event RunEventHandler Run { add { _runEventHandler = value; } remove { _runEventHandler = null; } }
    public event ReportEventHandler Report { add { _reportEventHandler = value; } remove { _reportEventHandler = null; } }
    public event ResultEventHandler Result { add { _resultEventHandler = value; } remove { _resultEventHandler = null; } }

    protected RecreateEventHandler _recreateEventHandler;
    protected RunEventHandler _runEventHandler;
    protected ReportEventHandler _reportEventHandler;
    protected ResultEventHandler _resultEventHandler;

    private delegate void WorkerThreadStartDelegate(object argument);

    private bool _cancellationPending;
    private bool _hasResulted;

    public bool CancellationPending { get { return _cancellationPending; } }
    public bool HasResulted { get { return _hasResulted; } }

    private AsyncOperation _asyncOperation;
    private readonly WorkerThreadStartDelegate _threadStart;
    private readonly SendOrPostCallback _operationCompleted;
    private readonly SendOrPostCallback _progressReporter;

    public BackgroundThread()
    {
      _threadStart = new WorkerThreadStartDelegate(WorkerThreadStart);
      _operationCompleted = new SendOrPostCallback(AsyncOperationCompleted);
      _progressReporter = new SendOrPostCallback(ProgressReporter);
      _hasResulted = true;
    }

    protected virtual void OnRecreate(RecreateEventArgs e)
    {
      if (_recreateEventHandler == null) return;
      _recreateEventHandler((object)this, e);
    }

    protected virtual void OnRun(RunEventArgs e)
    {
      if (_runEventHandler == null) return;
      _runEventHandler((object)this, e);
    }

    protected virtual void OnReport(ReportEventArgs e)
    {
      if (_reportEventHandler == null) return;
      _reportEventHandler((object)this, e);
    }

    protected virtual void OnResult(ResultEventArgs e)
    {
      if (_resultEventHandler == null) return;
      _resultEventHandler((object)this, e);
    }

    public void ReportProgress(int percentProgress)
    {
      this.ReportProgress(percentProgress, (object)null);
    }

    public void ReportProgress(int percentProgress, object userState)
    {
      ReportEventArgs changedEventArgs = new ReportEventArgs(percentProgress, userState);
      if (_asyncOperation != null)
        _asyncOperation.Post(this._progressReporter, (object)changedEventArgs);
      else
        _progressReporter((object)changedEventArgs);
    }

    public void Trigger() { this.Trigger(null); }

    public void Cancel() { _cancellationPending = true; }

    public void Trigger(object argument)
    {
      if (!_hasResulted)
        throw new BackgroundThreadException("BS");
      _hasResulted = false;
      _cancellationPending = false;

      if (SynchronizationContext.Current == null)
        SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
      _asyncOperation = AsyncOperation.CreateOperation(null, SynchronizationContext.Current);

      _threadStart.BeginInvoke(argument, (AsyncCallback)null, (object)null);
    }

    private void AsyncOperationCompleted(object arg)
    {
      _hasResulted = true;
      _cancellationPending = false;
      OnResult((ResultEventArgs)arg);
    }

    private void ProgressReporter(object arg)
    {
      this.OnReport((ReportEventArgs)arg);
    }

    private void WorkerThreadStart(object argument)
    {
      object result = (object)null;
      Exception error = (Exception)null;
      bool cancelled = false;
      try
      {
        RunEventArgs e = new RunEventArgs(argument);
        this.OnRun(e);
        if (e.Cancel) cancelled = true;
        else result = e.Result;
      }
      catch (Exception ex) { error = ex; }
      this._asyncOperation.PostOperationCompleted(this._operationCompleted, (object)new ResultEventArgs(result, error, cancelled));
    }

    /// <summary>This is used for throwing BackgroundThread Tree exceptions only to make debugging faster.</summary>
    protected class BackgroundThreadException : Error
    {
      public BackgroundThreadException(string message) : base(message) { }
    }
  }
}
