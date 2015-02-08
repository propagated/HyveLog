using System;
using System.Collections.Generic;
using System.Text;
using HyveLog;

using NUnit.Framework;
using System.IO;

namespace HyveLog.UnitTests
{
    internal static class UnitTestSetup
    {
        //methods to mock anything necessary
    }

    [TestFixture]
    internal class TestHyveLog
    {
        [TestFixtureSetUp, Description("")]
        public void InitTestFixture()
        {
            //fires once at load before any test
        }
        [TestFixtureTearDown]
        public void EndAllTests()
        {
            //fires after all tests are complete
        }
        [SetUp]
        public void InitTest()
        {
            var defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Errors\\ErrorLog.txt");
            //fires before each test
            if (File.Exists(defaultPath))
            {
                File.Delete(defaultPath);
            }
        }
        [TearDown]
        public void EndTest()
        {
            //fires after each test
        }

        [Test, Description(@"Write to File, no path passed. errors\errorlog.txt will be created and written to in user\appdata\local folder.")]
        public void TestConstructorNoParams()
        {
            var logger = new Logger(Logger.LogTarget.File);
            var testMessage= "This is a Test Error Message";
            logger.Log(testMessage);
            Assert.IsTrue(File.Exists(logger.LogFileFullPath));
            Assert.AreEqual(testMessage, File.ReadAllText(logger.LogFileFullPath));
        }
        [Test, Description("Write to File valid path.")]
        public void TestGetSomeData1()
        {
            Assert.IsNotNull("");
        }
        [Test, Description("Write to File with relative path to calling directory.")]
        public void TestGetSomeData2()
        {
            Assert.IsNotNull("");
        }
        [Test, Description("Write to File path with with directory that doesn't exist, directory will be created.")]
        public void TestGetSomeData3()
        {
            Assert.IsNotNull("");
        }
        [Test, Description("Write to console")]
        public void TestGetSomeData4()
        {
            var logger = new Logger(Logger.LogTarget.File);
            var testMessage = "This is a Test Error Message";
            logger.Log(testMessage);
            Assert.IsNotNull("");
        }

    }

}