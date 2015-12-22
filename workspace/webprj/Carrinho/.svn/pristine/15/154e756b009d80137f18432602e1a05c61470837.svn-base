using Hcrp.CarroUrgenciaPsicoativo.DAL;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Hcrp.CarroUrgenciaPsicoativo.Entity;


namespace Hcrp.CarroUrgenciaPsicoativo.DAL.Test
{
    
    
    /// <summary>
    ///This is a test class for TempGenericaHCTest and is intended
    ///to contain all TempGenericaHCTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TempGenericaHCTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for getTempGenericaHC
        ///</summary>
        [TestMethod()]
        [Priority(20)]
        public void getTempGenericaHCTest()
        {
            long pNumID = Hcrp.CarroUrgenciaPsicoativo.DAL.TempGenericaHC.getTempGenericaHC().FirstOrDefault().num_sequencia ; // TODO: Initialize to an appropriate value
            Entity.TempGenericaHC expected = null; // TODO: Initialize to an appropriate value
            Entity.TempGenericaHC actual;
            actual = Hcrp.CarroUrgenciaPsicoativo.DAL.TempGenericaHC.getTempGenericaHC(pNumID);
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for setTempGenericaHC
        ///</summary>
        [TestMethod()]
        [Priority(10)]
        public void setTempGenericaHCTest()
        {
            string pOutError = string.Empty; // TODO: Initialize to an appropriate value
            string pOutErrorExpected = string.Empty; // TODO: Initialize to an appropriate value
            Entity.TempGenericaHC pObjetoGravar = new Entity.TempGenericaHC(); // TODO: Initialize to an appropriate value

            pObjetoGravar.num_campo1 = 1;
            pObjetoGravar.dsc_conteudo = "Teste";

            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Hcrp.CarroUrgenciaPsicoativo.DAL.TempGenericaHC.setTempGenericaHC(ref pOutError, pObjetoGravar, false);
            Assert.AreEqual(pOutErrorExpected, pOutError);
            Assert.AreEqual(expected, actual);
            
        }
    }
}
