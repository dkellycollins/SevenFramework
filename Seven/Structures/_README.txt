Seven/Structures _README.txt

SUMMARY:------------------------------------------------------------

The Seven/Structures directory contains a collection of generic
data structure implementations. The goal is to provide a complete 
library of all known data structure templates that all follow the
same design, are adaptable, and are directly compatible with
current implementations in the .NET Framework (such as ICollection,
IEnumerable, IList, etc.).

POLYMORPHISM TREE:--------------------------------------------------

  --- Structure<Type> (interface)
    |
    |--- Link
    |  |--- Link<Type...>
    |
    |--- Array<Type>
    |  |--- Array_Array<Type>
    |
    |--- List<Type>
    |  |--- ListLinked<Type>
    |  |  |--- List_Linked_ReaderWriter<Type>
    |  |--- ListArray<Type>
    |
    |--- Stack<Type>
    |  |--- StackLinked<Type>
    |  |--- StackArray<Type>
    |
    |--- Queue<Type>
    |  |--- QueueLinked<Type>
    |  |--- QueueArray<Type>
    |
    |--- Heap<Type>
    |  |--- HeapArrayStatic<Type>
    |  |--- HeapArrayDynamic<Type>
    |
    |--- HashTable<ValueType, KeyType>
    |  |--- HashTableLinked<ValueType, KeyType>
    |
    |--- AvlTree<ValueType, Keytype>
    |  |--- AvlTreeLinked<ValueType, Keytype>
    |  |--- AvlTree<ValueType, FirstKeytype, SecondKeyType>
    |     |--- AvlTreeLinked<ValueType, FirstKeytype, SecondKeyType>
    |
    |--- ReadBlackTree<ValueType, Keytype>
    |  |--- RedBlackTreeLinked<ValueType, Keytype>
    |  |--- RedBlackTree<ValueType, FirstKeytype, SecondKeyType>
    |     |--- RedBlackTreeLinked<ValueType, FirstKeytype, SecondKeyType>
    |
    |--- Octree<ValueType, Keytype>
       |--+ OctreeLinked<ValueType, Keytype>

RUNTIME ANALYSIS:--------------------------------------------------

These data structure files contain runtime values.
The runtime should be located in the "remarks" tag on all public members.
All runtimes are in O-Notation. Here is a brief explanation:
- "O(x)": the member has an upper bound of runtime equation "x"
- "Omega(x)": the member has a lower bound of runtime equation "x"
- "Theta(x)": the member has an upper and lower bound of runtime equation "x"
- "EstAvg(x)": the runtime equation "x" to typically expect
Note that if the letter "n" is used, it typically means the current 
number of items within the data structure.

SORTING ALGORITHMS:------------------------------------------------

Various data structures may use sorting techniques on the items 
within them. For specific sorting, see the "Sort" class, which 
contains various sorting algorithms on arrays. All the data 
structures include "ToArray" methods, so that you can pull out
the contents and sort it.

LISCENSE:----------------------------------------------------------

See "_LISCENSE.txt" in th root project directory.

SUPPORT:-----------------------------------------------------------

See "_SUPPORT.txt" in the root project directory.