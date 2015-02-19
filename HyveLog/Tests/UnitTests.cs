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

        const String relativePath = @"errors\errorlog.txt";
        String defaultPath;
        //String absolutePath = @"C:\Errors\errorlog.txt";
        String absolutePath;
        const String logFile = "errorlog.txt";

        [TestFixtureSetUp, Description("")]
        public void InitTestFixture()
        {
            //set default relative path, use to set absolute path
            defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Errors\\ErrorLog.txt");
            //should be c:\\ or / in mono
            //int OS = (int)Environment.OSVersion.Platform;
            //if ((OS == 4) || (OS == 6) || (OS == 128))
            //{
            //    absolutePath = Path.Combine(Path.GetPathRoot(defaultPath), "errors/errorlog.txt");
            //}
            //else
            //{
            //    absolutePath = Path.Combine(Path.GetPathRoot(defaultPath), relativePath);
            //}
            absolutePath = Path.Combine(Path.GetDirectoryName(defaultPath),"aboslutePath", "errorLog.txt");

            //clear out test artifacts
            if (Directory.Exists(Path.GetDirectoryName(defaultPath)))
            {
                File.Delete(defaultPath);
            }
            if (Directory.Exists(Path.GetDirectoryName(relativePath)))
            {
                Directory.Delete(Path.GetDirectoryName(relativePath), true);
            }
            if (Directory.Exists(Path.GetDirectoryName(absolutePath)))
            {
                Directory.Delete(Path.GetDirectoryName(absolutePath), true);
            }
            File.Delete(logFile);
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
            var logger = new Logger(Logger.LogTarget.File);
            logger.Log(errorMessage);
            Assert.IsTrue(File.Exists(logger.LogFileFullPath));
            var writtenMessage = File.ReadAllText(logger.LogFileFullPath).Split(new String[] {Environment.NewLine}, StringSplitOptions.None)[0];
            Assert.AreEqual(errorMessage, writtenMessage);
        }
        [Test, Description("Write to File valid path.")]
        public void TestConstructorAbsolutePath()
        {
            var logger = new Logger(Logger.LogTarget.File, absolutePath);
            logger.Log(errorMessage);
            Assert.IsTrue(File.Exists(logger.LogFileFullPath));
            var writtenMessage = File.ReadAllText(logger.LogFileFullPath).Split(new String[] { Environment.NewLine }, StringSplitOptions.None)[0];
            Assert.AreEqual(errorMessage, writtenMessage);
        }
        [Test, Description("Write to File with relative path to calling directory.")]
        public void TestConstructorRelativeLogFilePath()
        {
            var logger = new Logger(Logger.LogTarget.File, logFile);
            logger.Log(errorMessage);
            Assert.IsTrue(File.Exists(logger.LogFileFullPath));
            var writtenMessage = File.ReadAllText(logger.LogFileFullPath).Split(new String[] { Environment.NewLine }, StringSplitOptions.None)[0];
            Assert.AreEqual(errorMessage, writtenMessage);
        }
        [Test, Description("Write to File path with with directory that doesn't exist, directory will be created.")]
        public void TestCreateDirectoryBeforeWritingFile()
        {
            var logger = new Logger(Logger.LogTarget.File, relativePath);
            logger.Log(errorMessage);
            Assert.IsTrue(File.Exists(logger.LogFileFullPath));
            var writtenMessage = File.ReadAllText(logger.LogFileFullPath).Split(new String[] { Environment.NewLine }, StringSplitOptions.None)[0];
            Assert.AreEqual(errorMessage, writtenMessage);
        }
        [Test, Description("Write to console")]
        public void TestWritingToConsole()
        {
            var logger = new Logger();

            TextWriter stdout = Console.Out;
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            try
            {
                logger.Log(errorMessage);
            }
            finally
            {
                Console.SetOut(stdout);
                string output = sw.ToString();
                sw.Close();
                Assert.AreEqual(errorMessage + Environment.NewLine, output);
            }
        }
    }
}