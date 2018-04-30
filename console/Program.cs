using System;

namespace console
{
    internal class Program
    {
        private static void Main()
        {
            var lev = new ComputeLevenshteinDistance("caine", "pisica");
            Console.WriteLine(lev.DistanceRecursively);
            //Console.WriteLine(lev.DistanceMetric);
            Console.Read();
        }
    }

    public class ComputeLevenshteinDistance
    {
        private readonly string _s1;
        private readonly string _s2;
        private readonly int _s1Len;
        private readonly int _s2Len;

        public int DistanceRecursively => ComputeRecursively(_s1, _s1Len, _s2, _s2Len);
        public int DistanceMetric => ComputeWithMatrix(_s1, _s1Len, _s2, _s2Len);

        public ComputeLevenshteinDistance(string s1, string s2)
        {
            _s1 = s1;
            _s2 = s2;
            _s1Len = s1.Length;
            _s2Len = s2.Length;
        }

        private static int ComputeRecursively(string s1, int s1Len, string s2, int s2Len)
        {
            if (s1Len == 0)
            {
                return s2Len;
            }
            if (s2Len == 0)
            {
                return s1Len;
            }
            var cost = s1[s1Len - 1] == s2[s2Len - 1] ? 0 : 1;

            return Math.Min(ComputeRecursively(s1, s1Len - 1, s2, s2Len - 1) + 1, Math.Min(ComputeRecursively(s1, s1Len, s2, s2Len - 1) + 1,
                ComputeRecursively(s1, s1Len - 1, s2, s2Len - 1) + cost));

        }

        private static int ComputeWithMatrix(string s1, int s1Len, string s2, int s2Len)
        {
            int m = s1Len + 1, n = s2Len + 1, substitutionCost = 0;
            var matrix = new int[m, n];
            for (var i = 1; i < m; i++)
            {
                matrix[i, 0] = i;
            }

            for (var i = 1; i < n; i++)
            {
                matrix[0, i] = i;
            }

            for (var i = 1; i < s2Len; i++)
            {
                for (var j = 1; j < s1Len; j++)
                {
                    substitutionCost = s2[i-1] == s1[j-1] ? 0 : 1;
                    matrix[j, i] = Math.Min(matrix[j - 1, i] + 1,
                        Math.Min(matrix[j, i - 1], matrix[j - 1, i - 1] + substitutionCost));
                }
            }

            return matrix[s1Len+1, s2Len+1];
        }
    }
}
