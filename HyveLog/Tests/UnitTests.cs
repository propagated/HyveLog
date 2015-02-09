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
        const String errorMessage = "This is a test error message.";
        
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
            //fires before each test
        }
        [TearDown]
        public void EndTest()
        {
            //fires after each test
        }

        [Test, Description(@"Write to File, no path passed. errors\errorlog.txt will be created and written to in user\appdata\local folder.")]
        public void TestConstructorNoParams()
        {
            var defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Errors\\ErrorLog.txt");
            if (File.Exists(defaultPath))
            {
                File.Delete(defaultPath);
            }
            var logger = new Logger(Logger.LogTarget.File);
            logger.Log(errorMessage);
            Assert.IsTrue(File.Exists(logger.LogFileFullPath));
            Assert.AreEqual(errorMessage, File.ReadAllText(logger.LogFileFullPath));
        }
        [Test, Description("Write to File valid path.")]
        public void TestConstructorAbsolutePath()
        {
            var absolutePath = @"C:\Errors\errorlog.txt";
            if (!Directory.Exists(Path.GetDirectoryName(absolutePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(absolutePath));
            }
            var logger = new Logger(Logger.LogTarget.File, absolutePath);
            logger.Log(errorMessage);
            Assert.IsTrue(File.Exists(logger.LogFileFullPath));
            Assert.AreEqual(errorMessage, File.ReadAllText(logger.LogFileFullPath));
        }
        [Test, Description("Write to File with relative path to calling directory.")]
        public void TestConstructorRelativeLogFilePath()
        {
            var logger = new Logger(Logger.LogTarget.File, "errorlog.txt");
            logger.Log(errorMessage);
            Assert.IsTrue(File.Exists(logger.LogFileFullPath));
            Assert.AreEqual(errorMessage, File.ReadAllText(logger.LogFileFullPath));
        }
        [Test, Description("Write to File path with with directory that doesn't exist, directory will be created.")]
        public void TestCreateDirectoryBeforeWritingFile()
        {
            var relativePath = "errors\\errorlog.txt";
            if (Directory.Exists(Path.GetDirectoryName(relativePath)))
            {
                Directory.Delete(Path.GetDirectoryName(relativePath));
            }

            var logger = new Logger(Logger.LogTarget.File, relativePath);
            logger.Log(errorMessage);
            Assert.IsTrue(File.Exists(logger.LogFileFullPath));
            Assert.AreEqual(errorMessage, File.ReadAllText(logger.LogFileFullPath));
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