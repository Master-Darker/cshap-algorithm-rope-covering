using System;

/// <summary>
/// 给定一个有序数组arr，代表坐落在X轴上的点
/// 给定一个整数k，代表绳子的长度
/// 返回绳子最多压中几个点？
/// 即使绳子边缘处盖住点也算盖住
/// </summary>
namespace 绳子覆盖最多的点数
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[]{ 1, 3, 4, 6, 7, 9, 10, 11, 12, 13, 16, 17 };
            var k = 3;
            //var res = GetMaxCover_O_NxLogN(arr, k, out var brr);
            var res = GetMaxCover_O_N(arr, k, out var brr);
            Console.WriteLine($"长度为{k}的绳子最多压中{res}个点：");
            foreach (var item in brr)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }

        /// <summary>
        /// 获取绳子覆盖最多的点数
        /// </summary>
        /// <param name="arr">有序数组</param>
        /// <param name="k">绳子长度</param>
        /// <param name="brr">覆盖点数最多时的数组</param>
        /// <returns>绳子覆盖最多的点数</returns>
        static int GetMaxCover_O_NxLogN(int[] arr, int k, out int[] brr)
        {
            var max = 0;
            var maxIndex = 0;
            for (var i = 0; i < arr.Length; i++)
            {
                var len = i + 1;
                var crr = new int[len];
                Array.Copy(arr, crr, len);
                var cover = GetCover_O_NxLogN(crr, crr[i] - k);
                if (max < cover)
                {
                    max = cover;
                    maxIndex = i;
                }
            }
            brr = new int[max];
            Array.Copy(arr, maxIndex - max + 1, brr, 0, max);
            return max;
        }

        /// <summary>
        /// 获取有序数组中大于等于p的点数，二分法
        /// </summary>
        /// <param name="arr">有序数组</param>
        /// <param name="p">最小值</param>
        /// <returns>大于等于p的点数</returns>
        static int GetCover_O_NxLogN(int[] arr, int p)
        {
            if (arr.Length == 0) return 0;
            if (arr.Length == 1) return arr[0] >= p ? 1 : 0;
            var mid = arr.Length % 2 == 0 ? arr.Length / 2 - 1 : arr.Length / 2;
            if (arr[mid] >= p)
            {
                var len = mid + 1;
                var brr = new int[len];
                Array.Copy(arr, brr, len);
                return GetCover_O_NxLogN(brr, p) + arr.Length - len;
            }
            else
            {
                var len = arr.Length - mid - 1;
                var brr = new int[len];
                Array.Copy(arr, mid + 1, brr, 0, len);
                return GetCover_O_NxLogN(brr, p);
            }
        }

        /// <summary>
        /// 获取绳子覆盖最多的点数
        /// </summary>
        /// <param name="arr">有序数组</param>
        /// <param name="k">绳子长度</param>
        /// <param name="brr">覆盖点数最多时的数组</param>
        /// <returns>绳子覆盖最多的点数</returns>
        static int GetMaxCover_O_N(int[] arr, int k, out int[] brr)
        {
            var l = 0;
            var r = 0;
            var max = 0;
            var index = 0;
            while (l < arr.Length)
            {
                while (r < arr.Length && arr[r] - arr[l] <= k)
                {
                    r++;
                }
                var len = r - l++;
                if (max < len)
                {
                    max = len;
                    index = l - 1;
                }
            }
            brr = new int[max];
            Array.Copy(arr, index, brr, 0, max);
            return max;
        }
    }
}
