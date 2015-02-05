_Problem: Given a linked-list of numbers, return the number of sequences of elements (groups of consecutive elements). For example, given [9,1,3,7,8,5,2], output 3, representing these sequences: [1,2,3], [5], [7,8,9]._

The simple answer would be to run a quicksort on the list and then break it into consecutive elements, which should have `nlog(n)` average performance and `n^2` worst case performance. But we should be able to do better, since this is a more restricted problem then sorting. So here's a way to get `nlog(n)` worst case performance:

1. Convert every number to an open interval. For example, convert the integer 3 to the interval (2, 4).
2. Merge overlapping sequences. For example, (1, 3) and (2, 4) become (1, 4).
3. Repeat until this can't be done anymore (can you see why this will take several iterations?), then count the remaining sequences.

What's important here is that checking for merges can be done by a binary search as long as the sequences are ordered. Sequences definitely can't overlap if one's upper bound is less than the other's lower bound. If we compare this way, we can also eliminate every sequence is a greater lower bound as a merge candidate. The worst case is that there are no overlapping sequences. In this case we'll make `C*log(n)` checks for `n` numbers, for a total time complexity of `O(nlog(n))`, as desired.

((In practice we'll be making more than `log(n)` checks, because we also have to compare against the upper bounds. But we can reuse some information we got from comparing against the lower bounds, so the constant should be less than 2.))

Note that this implementation isn't perfect, in that it doesn't have the binary search. This is because I'm still a little new to functional programming and haven't figured out how to do easily process a linked list while keeping it in order. And also because you can't binary search linked lists.
