﻿using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace HyveLog.UnitTests
{

    internal static class UnitTestSetup
    {

    }

    [TestFixture]
    internal class TestSomething
    {
        [TestFixtureSetUp, Description("")]
        public void InitTestFixture()
        {
            //fires at load before any test
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

        [Test, Description("Write to File, no path passed. errors\\errorlog.txt will be created and written to in user\\appdata\\local folder.")]
        public void TestGetSomeData()
        {   
            Assert.IsNotNull("");
        }
        [Test, Description("Write to File valid path.")]
        public void TestGetSomeData1()
        {
            Assert.IsNotNull("");
        }
        [Test, Description("Write to File path valid directory, no filename: errorlog.txt will be created.")]
        public void TestGetSomeData2()
        {
            Assert.IsNotNull("");
        }
        [Test, Description("Write to File path with with directory that doesn't exist, directory will be created, errorlog.txt will be created.")]
        public void TestGetSomeData3()
        {
            Assert.IsNotNull("");
        }
        [Test, Description("Write to console")]
        public void TestGetSomeData4()
        {
            Assert.IsNotNull("");
        }

    }

}