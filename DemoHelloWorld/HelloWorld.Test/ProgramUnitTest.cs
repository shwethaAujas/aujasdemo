using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace HelloWorld.Test
{
    [TestClass]
    public class ProgramUnitTest
    {
        private const string Expected = "Hello, World!";
        [TestMethod]
        public void OutputTest()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);
            Program.Main();

            var result = sw.ToString().Trim();
            Assert.AreEqual(Expected, result);
        }
    }
}