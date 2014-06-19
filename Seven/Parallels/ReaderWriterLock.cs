// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

using System;
using System.Threading;

namespace Seven.Parallels
{
  /// <summary>Provides mutual exclusion between reading and writing between parallel threads.</summary>
  /// <remarks>Possible starvation of writers.</remarks>
  public class ReaderWriterLock
  {
    protected object _lock;
    protected int _readers;
    protected int _writers;

    /// <summary>Creates an instance of a ReaderWriterLock.</summary>
    public ReaderWriterLock()
    {
      _lock = new object();
      _readers = 0; _writers = 0;
    }
    
    /// <summary>Thread safe enterance for readers.</summary>
    public void ReaderLock()
    {
      lock (_lock)
      {
        while (!(_writers == 0))
          Monitor.Wait(_lock);
        this._readers++;
      }
    }

    /// <summary>Thread safe exit for readers.</summary>
    public void ReaderUnlock()
    {
      lock (this._lock)
      {
        this._readers--;
        Monitor.Pulse(this._lock);
      }
    }

    /// <summary>Thread safe enterance for writers.</summary>
    public void WriterLock()
    {
      lock (this._lock)
      {
        while (!(this._writers == 0) && !(this._readers == 0))
          Monitor.Wait(this._lock);
        this._writers++;
      }
    }

    /// <summary>Thread safe exit for readers.</summary>
    public void WriterUnlock()
    {
      lock (this._lock)
      {
        this._writers--;
        Monitor.PulseAll(this._lock);
      }
    }
  }
}
