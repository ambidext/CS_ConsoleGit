using System;

namespace ClassCompare
{
    class Program
    {
        public class CompareExam : IComparable<CompareExam>
        {
            public string name;
            public int point;

            public CompareExam() { }
            public CompareExam(string n, int p)
            {
                name = n;
                point = p;
            }

            public int CompareTo(CompareExam other)
            {
                if (point == other.point)
                {
                    return name.CompareTo(other.name);  // ascending
                }
                else
                {
                    return (point < other.point ? 1 : -1);  // descending
                }
            }

            public override string ToString()
            {
                return string.Format("{0}:{1}",name,point);
            }
        }
        static void Main(string[] args)
        {
            CompareExam[] ce = new CompareExam[5];
            ce[0] = new CompareExam("Kim", 80);
            ce[1] = new CompareExam("Park", 90);
            ce[2] = new CompareExam("Choi", 80);
            ce[3] = new CompareExam("Lee", 100);
            ce[4] = new CompareExam("Kwak", 90);

            Array.Sort(ce);

            foreach (var item in ce)
                Console.WriteLine(item.ToString());
        }
    }
}
