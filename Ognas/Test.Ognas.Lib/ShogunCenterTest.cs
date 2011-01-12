using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ognas.Lib;
using Ognas.Lib.Shoguns;

namespace Test.Ognas.Lib
{
    
    
    /// <summary>
    ///这是 ShogunCenterTest 的测试类，旨在
    ///包含所有 ShogunCenterTest 单元测试
    ///</summary>
    [TestClass()]
    public class ShogunCenterTest
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
        ///GetSubShogunList 的测试
        ///</summary>
        [TestMethod()]
        public void GetSubShogunListTest()
        {
            ShogunCenter target = new ShogunCenter(); // TODO: 初始化为适当的值
            TypeofInitialShogunList tisl = TypeofInitialShogunList.ForMaster; // TODO: 初始化为适当的值
            List<Shogun> actual;
            actual = target.GetSubShogunList(tisl);

            foreach( Shogun s in actual )
            {
                Trace.WriteLine(s.Name);
            }
            Assert.AreEqual(5, actual.Count);
        }

        /// <summary>
        ///GetSubShogunList 的测试
        ///</summary>
        [TestMethod()]
        public void GetSubShogunListTest2()
        {
            ShogunCenter target = new ShogunCenter(); // TODO: 初始化为适当的值
            TypeofInitialShogunList tisl = TypeofInitialShogunList.ForPlayer; // TODO: 初始化为适当的值
            List<Shogun> actual;
            actual = target.GetSubShogunList(tisl);

            foreach (Shogun s in actual)
            {
                Trace.WriteLine(s.Name);
            }
            Assert.AreEqual(3, actual.Count);
        }

        /// <summary>
        ///ShogunCenter 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void ShogunCenterConstructorTest()
        {
            ShogunCenter target = new ShogunCenter();
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }
    }
}
