// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

#if Unsafe

namespace Seven.Unsafe
{
  public unsafe struct Pointer
  {
    void* _pointer;

    public unsafe Pointer(int value)
    {
      this._pointer = (void*)value;
    }

    public unsafe Pointer(long value)
    {
      this._pointer = (void*)checked((int)value);
    }

    public unsafe Pointer(void* value)
    {
      this._pointer = value;
    }

    public unsafe int ToInt32()
    {
      return (int)this._pointer;
    }

    public unsafe long ToInt64()
    {
      return (long)(int)this._pointer;
    }

    public override unsafe int GetHashCode()
    {
      return (int)this._pointer;
    }

    public override unsafe string ToString()
    {
      return ((long)this._pointer).ToString();
    }
  }
}

#endif
