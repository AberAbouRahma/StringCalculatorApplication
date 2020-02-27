using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringCalculatorProj;
namespace StringCalculatorProj.Tests
{
    [TestClass]
    public class StringCalculatorTests
    {
        private static StringCalcualtor stringCalc;

        [TestInitialize]
        public void TestSetup()
        {
            stringCalc = new StringCalcualtor();
        }

        [TestMethod]
        public void EmptyString_Returns0()
        {

            string input = "";
            var actualResult = stringCalc.Add(input);
            Assert.AreEqual(0, actualResult);
        }

        [TestMethod]
        public void SinglePosNumStringLT1000_ReturnsSameNum()
        {
            string input = "1";
            var actualResult = stringCalc.Add(input);
            Assert.AreEqual(int.Parse(input), actualResult);
        }
        [TestMethod]
        public void SinglePosNumStringEQ1000_ReturnsSameNum()
        {
            string input = "1000";
            var actualResult = stringCalc.Add(input);
            Assert.AreEqual(int.Parse(input), actualResult);
        }
        [TestMethod]
        public void SingleNumStringEQ0_Returns0()
        {
            string input = "0";
            var actualResult = stringCalc.Add(input);
            Assert.AreEqual(int.Parse(input), actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SingleNegNumString_ShouldNotProcessNeg()
        {
            string input = "-1";
            var actualResult = stringCalc.Add(input);
        }
        [TestMethod]
        public void SingleNumStringGT1000_Returns0()
        {
            string input = "1001";
            var actualResult = stringCalc.Add(input);
            Assert.AreEqual(0, actualResult);
        }
       
        /// <summary>
        /// Refactor, comma
        /// </summary>
        [TestMethod]
        [DataRow(0, 1000)]
        [DataRow(1, 1000)]
        [DataRow(1000, 1000)]
        [DataRow(1000, 0)]
        [DataRow(1000, 1)]
        [DataRow(0, 0)]
        public void TwonNonNegNumsLTorEQ1000WithCommaDelim_ReturnsSum(int a, int b)
        {
            string input = a + "," + b;
            var actualResult = stringCalc.Add(input);
            Assert.AreEqual(a + b, actualResult);
        }

        /// <summary>
        /// Refactor, newLine
        /// </summary>
        [TestMethod]
        [DataRow(0, 1000)]
        [DataRow(1, 1000)]
        [DataRow(1000, 1000)]
        [DataRow(1000, 0)]
        [DataRow(1000, 1)]
        [DataRow(0, 0)]
        public void TwonNonNegNumsLTorEQ1000WithNewLineDelim_ReturnsSum(int a, int b)
        {
            string input = a + "\n" + b;
            var actualResult = stringCalc.Add(input);
            Assert.AreEqual(a + b, actualResult);
        }


        /// <summary>
        /// Greater than 1000, comma or newLine delimiters
        /// </summary>
        [TestMethod]
        [DataRow(1001, ",", 1001)]
        [DataRow(1, ",", 1001)]
        [DataRow(1001, ",", 1)]
        [DataRow(1001, ",", 0)]
        [DataRow(0, ",", 1001)]
        [DataRow(1001, "\n", 1001)]
        [DataRow(1, "\n", 1001)]
        [DataRow(1001, "\n", 1)]
        [DataRow(1001, "\n", 0)]
        [DataRow(0, "\n", 1001)]
        public void TwoNonNegNumsLTorEQ1000WithNewLineDelim_ProcessIgnoreGT1000(int a, string c, int b)
        {
            string input = a + c + b;
            var actualResult = stringCalc.Add(input);
            if (a > 1000 && b > 1000)
                Assert.AreEqual(0, actualResult);
            else if (a > 1000)
                Assert.AreEqual(b, actualResult);
            else
                Assert.AreEqual(a, actualResult);
        }

        /// <summary>
        /// GetCalledCountTest
        /// </summary>
        [TestMethod]
        public void GetCalledCount()
        {
            var NumOfTimesAddCalled = stringCalc.GetCalledCount();

            Assert.AreEqual(StringCalcualtor.AddCalledCount, NumOfTimesAddCalled);
        }
        /// <summary>
        /// -ve and GT 1000, comma or newLine delimiters
        /// </summary>
        [TestMethod]
        [DataRow(-1, ",", 0)]
        [DataRow(-1, ",", -1)]
        [DataRow(1001, ",", -1)]
        [DataRow(-1, ",", 1001)]
        [DataRow(-1, "\n", 0)]
        [DataRow(-1, "\n", -1)]
        [DataRow(1001, "\n", -1)]
        [DataRow(-1, "\n", 1001)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void OneOr2NegNumsLTorEQ1000WithNewLineDelim_ShouldNotProcess(int a, string c, int b)
        {
            string input = a + c + b;
            var actualResult = stringCalc.Add(input);
        }

        /// <summary>
        /// test AddOccured event triggered with Legitimate parameters
        /// </summary>
        [TestMethod]
        [DataRow(1001, ",", 1001)]
        [DataRow(1, ",", 1001)]
        [DataRow(1001, ",", 1)]
        [DataRow(1001, ",", 0)]
        [DataRow(0, ",", 1001)]
        [DataRow(1001, "\n", 1001)]
        [DataRow(1, "\n", 1001)]
        [DataRow(1001, "\n", 1)]
        [DataRow(1001, "\n", 0)]
        [DataRow(0, "\n", 1001)]

        public void AddOccuredTestIlegitParamsGT1000_ShouldPass(int a, string c, int b)
        {
            string giveninput = null;

            stringCalc.AddOccured += delegate (string input, int result)
            {
                giveninput = input;
            };
            var actualResult = stringCalc.Add(a + c + b);
            Assert.AreEqual(a + c + b, giveninput);
        }

        /// <summary>
        /// test AddOccured event triggered with -VE parameters
        /// </summary>
        [DataRow(-1, ",", 0)]
        [DataRow(-1, ",", -1)]
        [DataRow(1001, ",", -1)]
        [DataRow(-1, ",", 1001)]
        [DataRow(-1, "\n", 0)]
        [DataRow(-1, "\n", -1)]
        [DataRow(1001, "\n", -1)]
        [DataRow(-1, "\n", 1001)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddOccuredTestNegativeParams_ShouldNotPass(int a, string c, int b)
        {
            string giveninput = null;

            stringCalc.AddOccured += delegate (string input, int result)
            {
                giveninput = input;
            };
            var actualResult = stringCalc.Add(a + c + b);
            
        }

        /// <summary>
        /// test AddOccured event triggered with Legit parameters
        /// </summary>
        [TestMethod]
        [DataRow(0, ",", 1000)]
        [DataRow(1, ",", 1000)]
        [DataRow(1000, ",", 1000)]
        [DataRow(1000, ",", 0)]
        [DataRow(1000, ",", 1)]
        [DataRow(0, ",", 0)]
        public void AddOccuredTestWithLegitParamsInRange_ShouldPass(int a, string c, int b)
        {
            string giveninput = null;

            stringCalc.AddOccured += delegate (string input, int result)
            {
                giveninput = input;
            };
            var actualResult = stringCalc.Add(a + c + b);
            Assert.AreEqual(a + c + b, giveninput);
        }
        /// <summary>
        /// Illegitmate delimiters, parameters, and legitmate delimeters without numbers
        /// </summary>
        [TestMethod]
        [DataRow("\n")]
        [DataRow(",")]
        [DataRow("NaN")]
        [DataRow("Aber")]
        [DataRow("//;\n")]
        [DataRow("//]]\n")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void HandlingNANOrDelimsWithoutNums_ShouldNotProcess(string c)
        {
            string input = c;
            var actualResult = stringCalc.Add(input);
        }

        /// <summary>
        /// Legitmate delimiters with one Legit
        /// </summary>
        [TestMethod]
        [DataRow("\n1000")]
        [DataRow("1000\n")]
        [DataRow("\n1")]
        [DataRow("1\n")]
        [DataRow("1000,")]
        [DataRow(",1000")]
        [DataRow("1,")]
        [DataRow(",1")]
        [DataRow("//;\n1000;")]
        [DataRow("//;\n;1000")]
        [DataRow("//]]\n]]1000")]
        [DataRow("//]]\n1000]]")]
        [DataRow("//;\n1;")]
        [DataRow("//;\n;1")]
        [DataRow("//]]\n]]1")]
        [DataRow("//]]\n1]]")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void HandlingLegitDelimsWithOneLegitNum_ShouldNotProcess(string c)
        {
            string input = c;
            var actualResult = stringCalc.Add(input);
        }

        /// <summary>
        /// Using delimiters other than the cstom delimiters specified between "//" and"\n"
        /// </summary>
        [TestMethod]
        [DataRow("//;\n1000||")]
        [DataRow("//;\n||1000")]
        [DataRow("//]]\n[[1000")]
        [DataRow("//]]\n1000[[")]
        [DataRow("//;\n1,")]
        [DataRow("//;\n,1")]
        [DataRow("//]]\n[[1")]
        [DataRow("//]]\n1[[")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void HandleDifferentDelimsThanCustomSpecifiedWithOneLegitNum_ShouldNotProcess(string c)
        {
            string input = c;
            var actualResult = stringCalc.Add(input);
        }


        /// <summary>
        /// Using delimiters other than the cstom delimiters specified between "//" and"\n"
        /// </summary>
        [TestMethod]
        [DataRow("//;\n1000||1000")]
        [DataRow("//]]\n1000[[1000")]
        [DataRow("//;\n1,1")]
        [DataRow("//]]\n1[[1")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void HandleDifferentDelimsThanCustomSpecifiedWith2LegitNums_ShouldNotProcess(string c)
        {
            string input = c;
            var actualResult = stringCalc.Add(input);
        }


        /// <summary>
        /// Legit Custom Delimeter with one or two negative nums
        /// </summary>
        [TestMethod]
        [DataRow(1000, ";", -1)]
        [DataRow(-1, ";;", 0)]
        [DataRow(-1, "[[[", -1)]
        [DataRow(-1, "]]]", 1000)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LegitCustomDelimiterWith1Or2NegativeNum_ShouldNotProcess(int a, string c, int b)
        {
            string input = "//" + c + "\n" + a + c + b;
            var actualResult = stringCalc.Add(input);

        }
        /// <summary>
        /// Attempt to process more than 2 parameters
        /// </summary>
        [TestMethod]
        [DataRow(1, ",", 0, "\n", 9)]
        [DataRow(1, ",", 1, "\n", 10)]
        [DataRow(1000, ",", 1, "\n", 1)]
        [DataRow(1, ",", 1000, "\n", 3)]
        [DataRow(1, "\n", 0, ",", 9)]
        [DataRow(1, "\n", 1, ",", 10)]
        [DataRow(1000, "\n", 1, ",", 1)]
        [DataRow(1, "\n", 1000, ",", 3)]
        [DataRow(1, ",", 0, ",", 2)]
        [DataRow(1, ",", 1, ",", 2)]
        [DataRow(1000, ",", 1, ",", 3)]
        [DataRow(1, ",", 1000, ",", 5)]
        [DataRow(1, "\n", 0, "\n", 9)]
        [DataRow(1, "\n", 1, "\n", 10)]
        [DataRow(1000, "\n", 1, "\n", 1)]
        [DataRow(1, "\n", 1000, "\n", 3)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GT3LegitParamsWithLegitAndNonLegitHypridDelims_ShouldNotProcess(int a, string c, int b, string d, int e)
        {
            string input = a + c + b + d + e;
            var actualResult = stringCalc.Add(input);
        }
        /// <summary>
        /// Legit Custom Delimeter
        /// </summary>
        [TestMethod]
        [DataRow(1000, ";", 1000)]
        [DataRow(1, ";;", 1001)]
        [DataRow(1001, "[[[", 1000)]
        [DataRow(1000, "]]]", 1001)]
        public void CustomDelimiter_ShouldProcess(int a, string c, int b)
        {
            string input = "//" + c + "\n" + a + c + b;
            var actualResult = stringCalc.Add(input);
            if (a > 1000 && b > 1000)
                Assert.AreEqual(0, actualResult);
            else if (a > 1000 && b <= 1000)
                Assert.AreEqual(b, actualResult);
            else if (a <= 1000 && b > 1000)
                Assert.AreEqual(a, actualResult);
            else
                Assert.AreEqual(a + b, actualResult);
        }

        /// <summary>
        /// Custom Delimeter As a Number, should throw exception
        /// </summary>
        [TestMethod]
        [DataRow(1000, "10", 1000)]
        [DataRow(1, "1", 1000)]
        [DataRow(1, "-2", 1000)]
        [DataRow(0, "0", 0)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CustomDelimiterAsNumber_ShouldNotProcess(int a, string c, int b)
        {
            string input = "//" + c + "\n" + a + c + b;
            var actualResult = stringCalc.Add(input);
        }
    }
}
