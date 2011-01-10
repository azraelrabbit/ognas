using System;
using System.Collections;
using System.Collections.Generic;

namespace Ognas.Lib
{
    public class Utility
    {
        private Utility() { }

        /// <summary>
        /// 获取随机数组
        /// </summary>
        /// <typeparam name="T">数组</typeparam>
        /// <param name="array">源数组</param>
        /// <param name="length">要获取的数组的长度</param>
        /// <returns>特定长度的随机数组</returns>
        public static List<T> GetRandomList<T>(List<T> list, int length)
        {
            Random r = new Random();

            List<T> temp = new List<T>();
            foreach (T o in list)
            {
                temp.Add(o);
            }

            for (int i = 0; i < temp.Count; i++)
            {
                int r1 = r.Next( 0,temp.Count );
                int r2 = r.Next(0, temp.Count);

                T t;
                t = temp[r1];
                temp[r1] = temp[r2];
                temp[r2] = t;
            }

            List<T> tR = new List<T>();
            int iS = r.Next(0, temp.Count);

            for (int j = iS; j < length + iS; j++)
            {
                int index = j;
                if (j >= length)
                {
                    index -= length;
                }
                tR.Add(temp[index]);
            }

            return tR;
        }

        /// <summary>
        /// 从给定的列表中移除特定的对象(组)
        /// </summary>
        /// <typeparam name="T">列表对象</typeparam>
        /// <param name="source">源列表</param>
        /// <param name="items">要移除的对象(组)</param>
        public static void RemoveItemsFromList<T>(List<T> source, List<T> items)
        {
            foreach (T t in items)
            {
                RemoveItemsFromList<T>(source, t);
            }
        }

        /// <summary>
        /// 从给定的列表中移除特定的对象
        /// </summary>
        /// <typeparam name="T">列表对象</typeparam>
        /// <param name="source">源列表</param>
        /// <param name="item">要移除的对象</param>
        public static void RemoveItemsFromList<T>(List<T> source, T item)
        {
            source.Remove(item);
        }
    }
}
