using Hcrp.CarroUrgenciaPsicoativo.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Hcrp.CarroUrgenciaPsicoativo.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL.Test
{
    
    
    /// <summary>
    ///This is a test class for IdentificacaoLocalTest and is intended
    ///to contain all IdentificacaoLocalTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IdentificacaoLocalTest
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
        ///A test for setIdentificacaoLocal
        ///</summary>
        [TestMethod(), Priority(10)]
        public void setIdentificacaoLocalTest()
        {
            string pOutError = string.Empty; // TODO: Initialize to an appropriate value
            string pOutErrorExpected = string.Empty; // TODO: Initialize to an appropriate value
            Entity.IdentificacaoLocal pObjetoGravar = new Entity.IdentificacaoLocal(); // TODO: Initialize to an appropriate value
            pObjetoGravar.dsc_id_local = string.Format("TESTE DE GRAVAÇÃO {0} {1} ", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());

            bool pControlarTransacao = false; // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Hcrp.CarroUrgenciaPsicoativo.DAL.IdentificacaoLocal.setIdentificacaoLocal(ref pOutError, pObjetoGravar, pControlarTransacao);
            Assert.AreEqual(pOutErrorExpected, pOutError);
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for getIdentificacaoLocal
        ///</summary>
        [TestMethod(), Priority(20)]
        public void getIdentificacaoLocalTest()
        {
            string pDscIDLocal = "TESTE"; // TODO: Initialize to an appropriate value
            long pNumID = 0;

            List<Entity.IdentificacaoLocal> actual = Hcrp.CarroUrgenciaPsicoativo.DAL.IdentificacaoLocal.getIdentificacaoLocal(pDscIDLocal, pNumID);
            Assert.IsNotNull(actual);
            Assert.AreNotEqual(actual.Count, 0);

        }

        /// <summary>
        ///A test for getIdentificacaoLocal
        ///</summary>
        [TestMethod(), Priority(20)]
        public void getIdentificacaoLocalTestID()
        {
            long pNumID = 1; // TODO: Initialize to an appropriate value
            Entity.IdentificacaoLocal actual = Hcrp.CarroUrgenciaPsicoativo.DAL.IdentificacaoLocal.getIdentificacaoLocal(pNumID);
            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.num_id_local, pNumID);
        }
    }
}
