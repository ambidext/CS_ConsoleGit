using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace DiskController
{
    class Program
    {
        public class Solution
        {
            // 문제 : https://programmers.co.kr/learn/courses/30/lessons/42627
            // 풀이 : https://junboom.tistory.com/24
            public abstract class Heap<T> : IEnumerable<T>
            {
                private const int InitialCapacity = 0;
                private const int GrowFactor = 2;
                private const int MinGrow = 1;
                private int _capacity = InitialCapacity;
                private T[] _heap = new T[InitialCapacity];
                private int _tail = 0;

                public int Count { get { return _tail; } }
                public int Capacity { get { return _capacity; } }
                protected Comparer<T> Comparer { get; private set; }
                protected abstract bool Dominates(T x, T y);
                protected Heap() : this(Comparer<T>.Default)
                {
                }
                protected Heap(Comparer<T> comparer) : this(Enumerable.Empty<T>(), comparer)
                {
                }
                protected Heap(IEnumerable<T> collection)
                    : this(collection, Comparer<T>.Default)
                {
                }
                protected Heap(IEnumerable<T> collection, Comparer<T> comparer)
                {
                    if (collection == null) throw new ArgumentNullException("collection");
                    if (comparer == null) throw new ArgumentNullException("comparer");
                    Comparer = comparer;
                    foreach (var item in collection)
                    {
                        if (Count == Capacity)
                            Grow();

                        _heap[_tail++] = item;
                    }

                    for (int i = Parent(_tail - 1); i >= 0; i--)
                        BubbleDown(i);
                }
                public void Add(T item)
                {
                    if (Count == Capacity)
                        Grow();

                    _heap[_tail++] = item;
                    BubbleUp(_tail - 1);
                }

                private void BubbleUp(int i)
                {
                    if (i == 0 || Dominates(_heap[Parent(i)], _heap[i]))
                        return; //correct domination (or root) 

                    Swap(i, Parent(i));
                    BubbleUp(Parent(i));
                }

                public T GetMin()
                {
                    if (Count == 0) throw new InvalidOperationException("Heap is empty");
                    return _heap[0];
                }
                public T ExtractDominating()
                {
                    if (Count == 0) throw new InvalidOperationException("Heap is empty");
                    T ret = _heap[0];
                    _tail--;
                    Swap(_tail, 0);
                    BubbleDown(0);
                    return ret;
                }
                private void BubbleDown(int i)
                {
                    int dominatingNode = Dominating(i);
                    if (dominatingNode == i) return;
                    Swap(i, dominatingNode);
                    BubbleDown(dominatingNode);
                }

                private int Dominating(int i)
                {
                    int dominatingNode = i;
                    dominatingNode = GetDominating(YoungChild(i), dominatingNode);
                    dominatingNode = GetDominating(OldChild(i), dominatingNode);

                    return dominatingNode;
                }

                private int GetDominating(int newNode, int dominatingNode)
                {
                    if (newNode < _tail && !Dominates(_heap[dominatingNode], _heap[newNode]))
                        return newNode;
                    else
                        return dominatingNode;
                }

                private void Swap(int i, int j)
                {
                    T tmp = _heap[i];
                    _heap[i] = _heap[j];
                    _heap[j] = tmp;
                }

                private static int Parent(int i)
                {
                    return (i + 1) / 2 - 1;
                }

                private static int YoungChild(int i)
                {
                    return (i + 1) * 2 - 1;
                }

                private static int OldChild(int i)
                {
                    return YoungChild(i) + 1;
                }

                private void Grow()
                {
                    int newCapacity = _capacity * GrowFactor + MinGrow;
                    var newHeap = new T[newCapacity];
                    Array.Copy(_heap, newHeap, _capacity);
                    _heap = newHeap;
                    _capacity = newCapacity;
                }

                public IEnumerator<T> GetEnumerator()
                {
                    return _heap.Take(Count).GetEnumerator();
                }

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return GetEnumerator();
                }
            }
            public class MaxHeap<T> : Heap<T>
            {
                public MaxHeap()
                    : this(Comparer<T>.Default)
                {
                }

                public MaxHeap(Comparer<T> comparer)
                    : base(comparer)
                {
                }

                public MaxHeap(IEnumerable<T> collection, Comparer<T> comparer)
                    : base(collection, comparer)
                {
                }
                public MaxHeap(IEnumerable<T> collection) : base(collection)
                {
                }

                protected override bool Dominates(T x, T y)
                {
                    return Comparer.Compare(x, y) >= 0;
                }
            }

            public class MinHeap<T> : Heap<T>
            {
                public MinHeap()
                    : this(Comparer<T>.Default)
                {
                }

                public MinHeap(Comparer<T> comparer)
                    : base(comparer)
                {
                }

                public MinHeap(IEnumerable<T> collection) : base(collection)
                {
                }

                public MinHeap(IEnumerable<T> collection, Comparer<T> comparer)
                    : base(collection, comparer)
                {
                }

                protected override bool Dominates(T x, T y) 
                {
                    return Comparer.Compare(x, y) <= 0;
                }
            }


            public int solution(int[,] jobs)
            {
                int answer = 0;
                int len = jobs.GetLength(0);
                int time = 0;
                int idx = 0;
                MinHeap<Nums> q = new MinHeap<Nums>();

                Nums[] nums1 = new Nums[len];
                for (int i=0; i<len; i++)
                {
                    nums1[i] = new Nums(jobs[i, 0], jobs[i, 1], 1);
                }

                Array.Sort(nums1);
                
                while (idx < len || q.Count > 0)
                {
                    while (idx < len && nums1[idx].first <= time)
                    {
                        nums1[idx].sortMethod = 2;
                        q.Add(nums1[idx++]);
                    }

                    if (q.Count == 0)
                        time = nums1[idx].first;
                    else
                    {                        
                        Nums job = q.ExtractDominating();
                        answer += time - job.first + job.last;
                        time += job.last;

                    }
                        
                }
                return answer / len;
            }
        }
        public class Nums : IComparable<Nums>
        {
            public int first;
            public int last;
            public int sortMethod; // 1, 2
            public Nums(int first, int last, int sortMethod)
            {
                this.first = first;
                this.last = last;
                this.sortMethod = sortMethod;
            }

            public int CompareTo(Nums other)
            {
                int n1, n2;
                if (sortMethod == 1)
                {
                    n1 = this.first;
                    n2 = other.first;
                }
                else
                {
                    n1 = this.last;
                    n2 = other.last;
                }

                if (n1 < n2) return -1;
                else if (n1 > n2) return 1;

                else return 0;
            }
        }

        static void Main(string[] args)
        {
            Solution sol = new Solution();
            int answer = sol.solution(new int[,] { {0,3 },{1,9 },{ 2, 6 } });
            Console.WriteLine(answer);
        }
    }
}
