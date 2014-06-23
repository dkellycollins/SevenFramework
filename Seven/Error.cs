// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

namespace Seven
{
  /// <summary>The polymorphism base of all the errors in the Seven framework.</summary>
  public class Error : System.Exception
  {
    private string _memberName;
    private string _sourceFilePath;
    private int _sourceLineNumber;

    public string MemberName { get { return this._memberName; } }
    public string SourceFilePath { get { return this._sourceFilePath; } }
    public int LineNimber { get { return this._sourceLineNumber; } }

    public Error(
      string message)
      //// REQUIRES .NET FRAMEWORK 4.5 (comment out for backwards compatibility)
      //[System.Runtime.CompilerServices.CallerMemberName] string memberName = "N/A",
      //[System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "N/A",
      //[System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = -1)
      : base(message)
    {
      //this._memberName = memberName;
      //this._sourceFilePath = sourceFilePath;
      //this._sourceLineNumber = sourceLineNumber;
       this._memberName = "REQUIRES .NET FRAMEWORK 4.5";
       this._sourceFilePath = "REQUIRES .NET FRAMEWORK 4.5";
       this._sourceLineNumber = -1;
    }

    public Error(
      string message,
      System.Exception exception)
      //// REQUIRES .NET FRAMEWORK 4.5 (comment out for backwards compatibility)
      //[System.Runtime.CompilerServices.CallerMemberName] string memberName = "N/A",
      //[System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "N/A",
      //[System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = -1)
      : base(message, exception)
    {
      //this._memberName = memberName;
      //this._sourceFilePath = sourceFilePath;
      //this._sourceLineNumber = sourceLineNumber;
      this._memberName = "REQUIRES .NET FRAMEWORK 4.5";
      this._sourceFilePath = "REQUIRES .NET FRAMEWORK 4.5";
      this._sourceLineNumber = -1;
    }

  }
}
