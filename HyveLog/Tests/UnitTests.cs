﻿using System;
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

        const String relativePath = "errors\\errorlog.txt";
        String defaultPath;
        const String absolutePath = @"C:\Errors\errorlog.txt";
        const String logFile = "errorlog.txt";

        [TestFixtureSetUp, Description("")]
        public void InitTestFixture()
        {
            //fires once at load before tests start
            defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Errors\\ErrorLog.txt");
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
                File.Delete(absolutePath);
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
            
            using (StringWriter writer = new StringWriter())
            {
                Console.SetOut(writer);
                logger.Log(errorMessage);
                Assert.AreEqual(errorMessage + Environment.NewLine, writer.ToString());
            }
        }
    }
}