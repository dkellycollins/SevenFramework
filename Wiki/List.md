>- [Home](https://github.com/53V3N1X/SevenFramework/wiki)<br />
>  - [Structures](https://github.com/53V3N1X/SevenFramework/wiki/Structures)<br />
>    - List

##### Summary

>A list is the most primitive data structure with dynamic size. It can be
>added to or removed from without worry of reaching the end of allocated
>memory (as oposed to an array).

##### Polymorphism Tree

>- Structure
>  - List
>     - List_Array
>     - List_Linked
>     - List_Recursive

##### Implementations

>**List_Array**
>
>Visual: | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
>
>This implementation of a list uses an array. It resizes to double its current
>size each time adding to it would otherwise cause an IndexOutOfBounds exception.
>It resizes to half its current size each time removing an element would result
>in the count being a fourth of its current size. The resizing can be controlled
>by setting a "MinimumCapacity." The minimum capacity will prevent downsizing of
>the list, but if use incorrectly could cause large, unused memory allocations.
>
>**List_Linked**
>
>Visual: 0 -> 1 -> 2 -> 3 -> 4 -> 5 -> 6 -> 7
>
>This implementation of a list uses nodes that are allocated each time an item
>is added to the list.
>
>**List_Recursive**
>
>Visual: 0( 1( 2( 3( 4( 5( 6( 7() ) ) ) ) ) ) )
>
>This implementation of a list uses recursive delegates. When adding to the
>list, delegates are made to recursively call each successive item. As of
>writing this wiki post, I have found no practical use for this implementation
> other than purely educational applications.

##### Runtimes and Memory

>See in-code xml documentations.

##### Usage

>Lists are very basic data structures, and they are often used when another data 
>structure should be chosen instead. A list should be used when:
>- processing unkown amounts of data
>- all the data will have to be processed every usage
>- the data can't/shouldn't be sorted
>
>As far as which implementations of a List to chose, use List_Array when the amount 
>of data is relatively static, and use List_Linked when constantly adding/removing
>from the list. As stated, the List_Recursive implementation should probably be 
>avoided.
