// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

namespace Seven.Mathematics
{
  //public class Tween<T>
  //{
  //  public enum Interpolation { Linear, EaseOutExpo, EaseInExpo, EaseOutCirc, EaseInCirc }

  //  T _original;
  //  T _distance;
  //  T _current;
  //  T _totalTimePassed = 0;
  //  T _totalDuration = 5;
  //  bool _finished = false;
  //  TweenFunction _tweenF = null;

  //  private delegate T TweenFunction(T timePassed, T start, T distance, T duration);

  //  public T Value { get { return _current; } }

  //  public bool IsFinished { get { return _finished; } }

  //  public Tween(T start, T end, T time) { Construct(start, end, time, Interpolation.Linear); }

  //  public Tween(T start, T end, T time, Interpolation tweenF) { Construct(start, end, time, tweenF); }

  //  private void Construct(T start, T end, T time, Interpolation tweenF)
  //  {
  //    _distance = end - start;
  //    _original = start;
  //    _current = start;
  //    _totalDuration = time;

  //    switch (tweenF)
  //    {
  //      case Interpolation.Linear:
  //        _tweenF = Tween<T>.Linear;
  //        break;
  //      case Interpolation.EaseOutExpo:
  //        _tweenF = Tween<T>.EaseOutExpo;
  //        break;
  //      case Interpolation.EaseInExpo:
  //        _tweenF = Tween<T>.EaseInExpo;
  //        break;
  //      case Interpolation.EaseOutCirc:
  //        _tweenF = Tween<T>.EaseOutCirc;
  //        break;
  //      case Interpolation.EaseInCirc:
  //        _tweenF = Tween<T>.EaseInCirc;
  //        break;
  //    }
  //  }

  //  public void Update(T elapsedTime)
  //  {
  //    _totalTimePassed += elapsedTime;
  //    _current = _tweenF(_totalTimePassed, _original, _distance, _totalDuration);

  //    if (_totalTimePassed > _totalDuration)
  //    {
  //      _current = _original + _distance;
  //      _finished = true;
  //    }
  //  }

  //  private static T Linear(T timePassed, T start, T distance, T duration)
  //  {
  //    return distance * timePassed / duration + start;
  //  }

  //  private static float EaseOutExpo(T timePassed, T start, T distance, T duration)
  //  {
  //    if (timePassed == duration)
  //      return start + distance;
  //    return (float)(distance * (-System.Math.Pow(2, -10 * timePassed / duration) + 1) + start);
  //  }

  //  private static float EaseInExpo(T timePassed, T start, T distance, T duration)
  //  {
  //    if (timePassed == 0)
  //      return start;
  //    return (float)(distance * System.Math.Pow(2, 10 * (timePassed / duration - 1)) + start);
  //  }

  //  private static float EaseOutCirc(T timePassed, T start, T distance, T duration)
  //  {
  //    return (float)(distance * System.Math.Sqrt(1 - (timePassed = timePassed / duration - 1) * timePassed) + start);
  //  }

  //  private static float EaseInCirc(T timePassed, T start, T distance, T duration)
  //  {
  //    return (float)(-distance * (System.Math.Sqrt(1 - (timePassed /= duration) * timePassed) - 1) + start);
  //  }
  //}
}