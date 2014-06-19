// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

// THIS CLASS IS FOR COMPATIBILITY ONLY
// DO NOT EXPLICITLY INVOKE ANYTHING IN THIS FILE
// OR I WILL FIND YOU... AND KILL YOU BECAUSE
// YOU SUCK AT PROGRAMMING!!!!!!!!!!!!!!!

using System;
using System.Collections;
using System.Collections.Generic;

namespace Seven.Structures
{
  /// <summary>DO NOT EXPLICITLY USE.</summary>
  public static class Compatibility
  {
    /// <summary>DO NOT EXPLICITLY USE.</summary>
    public class IList_Adapter<Type> //: IList<Type>
    {
      private List<Type> _list;

      /// <summary>DO NOT EXPLICITLY USE.</summary>
      public Type this[int index]
      {
        get
        {
          int i = 0;
          Type temp = default(Type);
          _list.Foreach(
            (Type current) =>
            {
              if (i++ == index)
              {
                temp = current;
                return ForeachStatus.Break;
              }
              return ForeachStatus.Continue;
            });
          return temp;
        }
        set
        {
          int i = 0;
          _list.Foreach(
            (ref Type current) =>
            {
              if (i++ == index)
              {
                current = value;
                return ForeachStatus.Break;
              }
              return ForeachStatus.Continue;
            });
        }
      }

      /// <summary>DO NOT EXPLICITLY USE.</summary>
      public int IndexOf(Type item)
      {
        int temp = 0;
        int i = 0;
        _list.Foreach(
          (Type current) =>
          {
            if (current.Equals(item))
            {
              temp = i;
              return ForeachStatus.Break;
            }
            i++;
            return ForeachStatus.Continue;
          });
        return temp;
      }

      /// <summary>DO NOT EXPLICITLY USE.</summary>
      void Insert(int index, Type item)
      {
        Type temp = default(Type);
        int i = 0;
        _list.Foreach(
          (ref Type current) =>
          {
            if (i == index)
            {
              temp = current;
              current = item;
            }
            else if (i >= index)
            {
              Type temp2 = current;
              current = temp;
              temp = temp2;
            }
            i++;
          });
        this._list.Add(temp);
      }

      /// <summary>DO NOT EXPLICITLY USE.</summary>
      void RemoveAt(int index)
      {

      }

      /// <summary>DO NOT EXPLICITLY USE.</summary>
      internal IList_Adapter(List<Type> from)
      {
        this._list = from;
      }
      
    }
  }
}
