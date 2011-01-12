using System;
using System.Collections.Generic;
using System.Threading;

namespace Ognas.Lib
{
    public class Utility
    {
        private Utility() { }

        private static int _iRandom = 0;

        private static int ComputerIRandom()
        {
            _iRandom++;
            if (_iRandom > 9999)
            {
                _iRandom = 0;
            }

            return ("R" + _iRandom.ToString()).GetHashCode();
        }

        /// <summary>
        /// 获取随机列表
        /// </summary>
        /// <typeparam name="T">列表类型</typeparam>
        /// <param name="list">源列表</param>
        /// <returns>随机列表</returns>
        public static List<T> GetRandomList<T>(List<T> list)
        {
            // 随机数生成器
            Random r = new Random();

            // 用于重新排序的临时列表
            List<T> temp = new List<T>();
            // 复制源列表
            foreach (T o in list)
            {
                temp.Add(o);
            }

            for (int i = 0; i < temp.Count; i++)
            {
                // 产生两个随机数用来表示列表的两个位置
                int r1 = r.Next(0, temp.Count);
                int r2 = r.Next(0, temp.Count);

                // 交换该两个位置的元素 以产生新的列表
                T t = temp[r1];
                temp[r1] = temp[r2];
                temp[r2] = t;
            }

            return temp;
        }

        /// <summary>
        /// 从一个指定的列表中随机获取一个对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="list">指定的列表</param>
        /// <returns>一个随机对象</returns>
        public static T GetRandomObject<T>(List<T> list)
        {
            Random r = new Random(ComputerIRandom());
            return list[r.Next(list.Count)];
        }

        /// <summary>
        /// 从一个指定的列表，根据概率获取指定的对象，或者一个随机的对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="list">指定的列表</param>
        /// <param name="t">指定的对象</param>
        /// <param name="odds">几率</param>
        /// <returns>随机的对象</returns>
        public static T GetRandomObject<T>(List<T> list, T t, int odds)
        {
            // 参数判定：t必须不在列表内
            // 否则的话，会造成概率不准确
            if (list.IndexOf(t) >= 0)
            {
                throw new ArgumentOutOfRangeException("t");
            }

            // 参数判定：odds（几率）必须在0-100之间
            if (odds < 0 || odds > 100)
            {
                throw new ArgumentOutOfRangeException("odds", "0-100", "");
            }

            T rReturn = default(T);

            // 执行一次Roll以获取一个0-100的随机整数，如果该数字小于odds（几率），直接获取该对象
            if (Roll() <= odds)
            {
                rReturn = t;
            }
            // 否则，从列表里选出随机位置的一个对象并返回
            else
            {
                rReturn = GetRandomObject<T>(list);
            }

            return rReturn;
        }

        /// <summary>
        /// 从一个指定的列表获取指定长度的随机列表
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="list">源列表</param>
        /// <param name="count">长度</param>
        /// <returns>随机列表</returns>
        public static List<T> GetRandomList<T>(List<T> list, int count)
        {
            // 参数判定：随机列表长度应不大于原始列表
            if (count > list.Count)
            {
                throw new ArgumentOutOfRangeException("count");
            }

            List<T> listReturn = new List<T>();
            // 临时变量
            List<T> temp = new List<T>();
            // 临时变量
            T t;

            // 复制源列表到临时列表
            foreach (T o in list)
            {
                temp.Add(o);
            }

            //temp = GetRandomList<T>(temp);

            for (int i = 0; i < count; i++)
            { 
                // 获取随机对象
                t = GetRandomObject<T>(temp);
                // 把随机对象增加到返回列表
                listReturn.Add(t);
                // 从临时列表中删除已获取的对象，确保该对象下次不会被选中
                RemoveItemsFromList<T>(temp, t);
            }

            return listReturn;
        }

        public static List<T> GetRandomList<T>(List<T> list, Dictionary<T,int> dic, int count)
        {
            // 参数判定：随机列表长度应不大于原始列表
            if (count > list.Count)
            {
                throw new ArgumentOutOfRangeException("count");
            }

            // 参数判定：指定几率的对象个数不应大于目标列表的个数
            if (dic.Count > count)
            {
                throw new ArgumentOutOfRangeException("dic");
            }

            List<T> listReturn = new List<T>();
            // 临时变量
            List<T> temp = new List<T>();
            // 临时变量
            T t;

            // 复制源列表到临时列表
            foreach (T o in list)
            {
                // 不复制随机列表中的对象
                if (!dic.ContainsKey(o))
                {
                    temp.Add(o);
                }
            }

            //temp = GetRandomList<T>(temp);

            foreach( T p in dic.Keys )
            {
                // 获取随机对象
                t = GetRandomObject<T>(temp , p , dic[p]);
                // 把随机对象增加到返回列表
                listReturn.Add(t);

                // 如果取得的对象不在DIC列表中 则从劣势列表中删除，防止再次取得
                if (!t.Equals(p))
                {
                    RemoveItemsFromList(temp, t);
                }
            }

            listReturn.AddRange( GetRandomList<T>(temp , count - dic.Count ));
            
            return listReturn;
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

        public static void AddItemToList<T>(List<T> target, T item)
        {
            target.Add(item);
        }

        #region Roll
        /// <summary>
        /// 获取一个不大于指定值的随机非负整数
        /// </summary>
        /// <param name="maxValue">指定值</param>
        /// <returns>一个不大于指定值的随机非负整数</returns>
        public static int Roll(int maxValue)
        {
            Random r = new Random(ComputerIRandom());
            return r.Next(maxValue+1);
        }

        /// <summary>
        /// 获取0-100的随机整数
        /// </summary>
        /// <returns>0-100的随机整数</returns>
        public static int Roll()
        {
            return Roll(100);
        }
        #endregion
    }
}
