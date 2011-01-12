using Ognas.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Test.Ognas.Lib
{
    
    
    /// <summary>
    ///这是 UtilityTest 的测试类，旨在
    ///包含所有 UtilityTest 单元测试
    ///</summary>
    [TestClass()]
    public class UtilityTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///RemoveItemsFromList 的测试
        ///</summary>
        public void RemoveItemsFromListTestHelper<T>()
        {
            List<T> source = null; // TODO: 初始化为适当的值
            List<T> items = null; // TODO: 初始化为适当的值
            Utility.RemoveItemsFromList<T>(source, items);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        public void RemoveItemsFromListTest()
        {
            RemoveItemsFromListTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///RemoveItemsFromList 的测试
        ///</summary>
        public void RemoveItemsFromListTest1Helper<T>()
        {
            List<T> source = null; // TODO: 初始化为适当的值
            T item = default(T); // TODO: 初始化为适当的值
            Utility.RemoveItemsFromList<T>(source, item);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        public void RemoveItemsFromListTest1()
        {
            RemoveItemsFromListTest1Helper<GenericParameterHelper>();
        }

        /// <summary>
        ///GetRandomList 的测试
        ///</summary>
        public void GetRandomListTestHelper<T>()
        {
            List<T> list = null; // TODO: 初始化为适当的值
            int length = 0; // TODO: 初始化为适当的值
            List<T> expected = null; // TODO: 初始化为适当的值
            List<T> actual;
            actual = Utility.GetRandomList<T>(list, length);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void GetRandomListTest()
        {
            GetRandomListTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///Utility 构造函数 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Ognas.Lib.dll")]
        public void UtilityConstructorTest()
        {
            Utility_Accessor target = new Utility_Accessor();
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        /// <summary>
        ///GetRandomList 的测试
        ///</summary>
        public void GetRandomListTest1Helper<T>()
        {
            List<T> list = null; // TODO: 初始化为适当的值
            Dictionary<T, int> dic = null; // TODO: 初始化为适当的值
            int count = 0; // TODO: 初始化为适当的值
            List<T> expected = null; // TODO: 初始化为适当的值
            List<T> actual;
            actual = Utility.GetRandomList<T>(list, dic, count);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void GetRandomListTest1()
        {
            GetRandomListTest1Helper<GenericParameterHelper>();
        }
    }
}
