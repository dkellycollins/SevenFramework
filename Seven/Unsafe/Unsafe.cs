// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

#if Unsafe

namespace Seven.Unsafe
{
  public static class Unsafe
  {

    public static Pointer memset(Pointer dst, int filler, int count)
    {
      return Unsafe.memset(dst, filler, count);
    }

    private static unsafe byte* memset(byte* dst, int filler, int count)
    {
      int countUint = count >> 2;
      int countByte = count & 3;

      byte fillerByte = (byte) filler;
      uint fiilerUint = (uint) filler | ( (uint) filler << 8 ) |
                                        ( (uint) filler << 16 );// |
                                        //( (uint) filler << 24 );

      uint* dstUint = (uint*) dst;

      while ( countUint-- != 0 )
      {
          *dstUint++ = fiilerUint;
      }

      byte* dstByte = (byte*) dstUint;

      while ( countByte-- != 0 )
      {
          *dstByte++ = fillerByte;
      }
      return dst;
    }
  }
}

#endif
