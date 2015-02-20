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
        const String logFile = "errorlog.txt";
        const String consoleLogFile = "consolelog.txt";
        String relativePath = Path.Combine("errors", logFile);
        String defaultPath;
        String absolutePath;
        

        [TestFixtureSetUp, Description("")]
        public void InitTestFixture()
        {
            //set default relative path, use to set absolute path
            defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Errors", "ErrorLog.txt");
            absolutePath = Path.Combine(Path.GetDirectoryName(defaultPath),"aboslutePath", "errorLog.txt");

            //clear out any test artifacts
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
            File.Delete(consoleLogFile);
        }
        [TestFixtureTearDown]
        public void EndAllTests()
        {
            //fires after all tests are complete
            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
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
            FileStream fs = new FileStream("consoleError.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            Console.SetOut(sw);
            logger.Log(errorMessage);
            sw.Flush();
            fs.Flush();
            fs.Close();
            var writtenMessage = File.ReadAllText("consoleError.txt").Split(new String[] { Environment.NewLine }, StringSplitOptions.None)[0];
            Assert.AreEqual(errorMessage, writtenMessage);

            //TextWriter stdout = Console.Out;
            //StringWriter sw = new StringWriter();
            //Console.SetOut(sw);
            //try
            //{
            //    logger.Log(errorMessage);
            //}
            //finally
            //{
            //    Console.SetOut(stdout);
            //    string output = sw.ToString();
            //    sw.Close();
            //    Assert.AreEqual(errorMessage + Environment.NewLine, output);
            //}
        }
    }
}