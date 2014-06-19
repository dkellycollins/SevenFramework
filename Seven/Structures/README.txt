Seven/Structures README.txt

https://github.com/53V3N1X/SevenFramework

IMPORTANT COMMENTS:-------------------------------------------------

The Seven/Structures directory contains a collection of generic
data structure implementations. The goal is to provide a complete 
library of all known data structure templates that all follow the
same design, are adaptable, and are directly compatible with
current implementations in the .NET Framework (such as ICollection,
IEnumerable, IList, etc.).

POLYMORPHISM TREE:--------------------------------------------------

    - Structure<Type>
    |
    |----- Link
    |    |--- Link<Type...>
    |
    |----- Array<Type>
    |    |--- Array_Array<Type>
    |
    |----- List<Type>
    |    |----- List_Linked<Type>
    |    |    |----- List_Linked_ReaderWriterLock<Type>
    |    |----- List_Array<Type>
    |         |----- List_Array_ReaderWriterLock<Type>
    |
    |----- Stack<Type>
    |    |----- Stack_Linked<Type>
    |    |----- Stack_Array<Type>
    |
    |----- Queue<Type>
    |    |----- Queue_Linked<Type>
    |    |----- Queue_Array<Type>
    |
    |----- Heap<Type>
    |    |----- Heap_Array_Static<Type>
    |    |----- Heap_Array_Dynamic<Type>
    |
    |----- HashTable<ValueType, KeyType>
    |    |----- HashTable_Linked<ValueType, KeyType>
    |
    |----- AvlTree<Type>
    |    |----- AvlTree_Linked<Type>
    |         |----- AvlTree_Linked_ReaderWriterLock<Type>
    |    |----- AvlTree_Array<Type>
    |
    |----- ReadBlackTree<Type>
    |    |----- RedBlackTree_Linked<Type>
    |         |----- RedBlackTree_Linked_ReaderWriterLock<Type>
    |    |----- RedBlackTree_Array<Type>
    |
    |----- Quadtree<Type>
    |    |----- Quadtree_Linked<Type>
    |
    |----- Octree<Type>
         |----- Octree_Linked<Type>

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

See "LISCENSE.txt" in th root project directory.

SUPPORT:-----------------------------------------------------------

See "README.txt" in the root project directory.