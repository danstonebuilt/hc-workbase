using Hcrp.CarroUrgenciaPsicoativo.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL.Test
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
            Hcrp.CarroUrgenciaPsicoativo.Entity.IdentificacaoLocal pObjetoGravar = new Hcrp.CarroUrgenciaPsicoativo.Entity.IdentificacaoLocal();

            bool pControlarTransacao = false; // TODO: Initialize to an appropriate value
            
            // Testando validação de obrigatoriedade [FB]
            bool actual = IdentificacaoLocal.setIdentificacaoLocal(ref pOutError, pObjetoGravar, pControlarTransacao);
            Assert.AreEqual(actual, false);
            Assert.AreNotEqual(pOutError, string.Empty);

            // Testando o sucesso da inserção [FB]
            pObjetoGravar.dsc_id_local = string.Format("TESTE BLL {0} {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
            actual = IdentificacaoLocal.setIdentificacaoLocal(ref pOutError, pObjetoGravar, pControlarTransacao);
            Assert.AreEqual(actual, true);
            Assert.AreEqual(pOutError, string.Empty);

            // Testando o sucesso da alteração [FB]
            pObjetoGravar.dsc_id_local += string.Format(" ALTERADO EM {0} {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
            actual = IdentificacaoLocal.setIdentificacaoLocal(ref pOutError, pObjetoGravar, pControlarTransacao);
            Assert.AreEqual(actual, true);
            Assert.AreEqual(pOutError, string.Empty);

        }
    }
}
