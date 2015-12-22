using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hcrp.Framework.Testes
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class PedidoAtendimentoTest
    {
        public PedidoAtendimentoTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        Hcrp.Framework.Classes.PedidoAtendimento _pedidoAtendimento = new Classes.PedidoAtendimento();
        Hcrp.Framework.Classes.PedidoAtendimento _pedidoAtendimento2 = new Classes.PedidoAtendimento(true);

        [TestMethod]
        public void PropriedadeTipoProtocoloAtendimentoTahValida()
        {
            try
            {
                // obter a propriedade para utilização em POCO + DTO
                Hcrp.Framework.Classes.PedidoAtendimento _pedidoAtendimento = new Classes.PedidoAtendimento();
                Classes.TipoProtocoloAtendimento tipoProtocoloAtendimento = _pedidoAtendimento.TipoProtocoloAtendimento;

                bool a = _pedidoAtendimento.TipoProtocoloAtendimento.Ativo;

                if (tipoProtocoloAtendimento == null)
                    Assert.Fail("Objeto null");

                // obter a propriedade com carregamento automatico via BD
                
                // Instancia
                Hcrp.Framework.Classes.PedidoAtendimento _pedidoAtendimento2 = new Classes.PedidoAtendimento(true);
                
                // Passa o id da property relacionada
                _pedidoAtendimento2._CodProtocoloAtendimento = 1;

                // obtem o objeto carregado do bd via propriedade
                Classes.TipoProtocoloAtendimento tipoProtocoloAtendimento2 = _pedidoAtendimento2.TipoProtocoloAtendimento;
                
                // valida que não eh criada uma nova instancia
                Classes.TipoProtocoloAtendimento tipoProtocoloAtendimento3 = _pedidoAtendimento2.TipoProtocoloAtendimento;

                // limpamos a property para forçar uma nova busca no mesmo objeto
                _pedidoAtendimento2.TipoProtocoloAtendimento = null;

                // carrega a property via bd novamente para o mesmo objeto
                Classes.TipoProtocoloAtendimento tipoProtocoloAtendimento4 = _pedidoAtendimento2.TipoProtocoloAtendimento;

                if (tipoProtocoloAtendimento2 == null)
                    Assert.Fail("Objeto null");
            }
            catch (Exception ex)
            {
                Assert.Fail("Teste falhou: {0}", ex.Message);
            }
        }

        [TestMethod]
        public void PropriedadeMunicipioPedidoTahValido()
        {
            try
            {
                // obter a propriedade para utilização em POCO + DTO
                Classes.Municipio municipioPedido = _pedidoAtendimento.MunicipioPedido;

                var a = municipioPedido.Nome;

                if (municipioPedido == null)
                    Assert.Fail("Objeto null");

                // Passa o id da property relacionada
                _pedidoAtendimento2._chaveMunicipioPedido = "BR|RS|068101";

                // obtem o objeto carregado do bd via propriedade
                var municipio_1 = _pedidoAtendimento2.MunicipioPedido;

                // valida que não eh criada uma nova instancia
                var municipio_2 = _pedidoAtendimento2.MunicipioPedido;

                // limpamos a property para forçar uma nova busca no mesmo objeto
                _pedidoAtendimento2.MunicipioPedido = null;

                // Informar uma chave invalida
                _pedidoAtendimento2._chaveMunicipioPedido = "asa";

                // carrega a property via bd novamente para o mesmo objeto
                var municipio_3 = _pedidoAtendimento2.MunicipioPedido;

                if (municipio_3 != null)
                    Assert.Fail("Objeto não nullo");
            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod]
        public void PropriedadeMunicipioTahValido()
        {
            try
            {
                // obter a propriedade para utilização em POCO + DTO
                Classes.Municipio municipioPedido = _pedidoAtendimento.Municipio;

                var a = municipioPedido.Nome;

                if (municipioPedido == null)
                    Assert.Fail("Objeto null");

                // Passa o id da property relacionada
                _pedidoAtendimento2._chaveMunicipio = "BR|RS|068101";

                // obtem o objeto carregado do bd via propriedade
                var municipio_1 = _pedidoAtendimento2.Municipio;

                // valida que não eh criada uma nova instancia
                var municipio_2 = _pedidoAtendimento2.Municipio;

                // limpamos a property para forçar uma nova busca no mesmo objeto
                _pedidoAtendimento2.Municipio = null;

                // Informar uma chave invalida
                _pedidoAtendimento2._chaveMunicipio = "asa";

                // carrega a property via bd novamente para o mesmo objeto
                var municipio_3 = _pedidoAtendimento2.Municipio;

                if (municipio_3 != null)
                    Assert.Fail("Objeto não nullo");
            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod]
        public void PropriedadeServicoSadtTahValida()
        {
            try
            {
                // obter a propriedade para utilização em POCO + DTO
                Classes.ServicoSadt servicoSadt = _pedidoAtendimento.ServicoSadt;

                var a = servicoSadt.Descricao;

                if (servicoSadt == null)
                    Assert.Fail("Objeto null");

                // Passa o id da property relacionada
                _pedidoAtendimento2.Seq = 43094;

                // obtem o objeto carregado do bd via propriedade
                var obj_1 = _pedidoAtendimento2.ServicoSadt;

                // valida que não eh criada uma nova instancia
                var obj_2 = _pedidoAtendimento2.ServicoSadt;

                // limpamos a property para forçar uma nova busca no mesmo objeto
                _pedidoAtendimento2.ServicoSadt = null;

                // Informar uma chave invalida
                _pedidoAtendimento2.Seq = 9999999;

                // carrega a property via bd novamente para o mesmo objeto
                var obj_3 = _pedidoAtendimento2.ServicoSadt;

                if (obj_3 == null)
                    Assert.Fail("Objeto não nullo");
            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod]
        public void PropriedadeLogradouroTahValida()
        {

            // obter a propriedade para utilização em POCO + DTO
            Classes.Logradouro logradouro = _pedidoAtendimento.Logradouro;

            if (logradouro == null)
                Assert.Fail("Objeto null");

            var a = logradouro.Nome;

            // Passa o id da property relacionada
            _pedidoAtendimento2._codTipoLogradouro = "733";

            // obtem o objeto carregado do bd via propriedade
            var obj_1 = _pedidoAtendimento2.Logradouro;

            // valida que não eh criada uma nova instancia
            var obj_2 = _pedidoAtendimento2.Logradouro;

            // limpamos a property para forçar uma nova busca no mesmo objeto
            _pedidoAtendimento2.Logradouro = null;

            // Informar uma chave invalida
            _pedidoAtendimento2._codTipoLogradouro = "as";

            // carrega a property via bd novamente para o mesmo objeto
            var obj_3 = _pedidoAtendimento2.Logradouro;

            if (obj_3 != null)
                Assert.Fail("Objeto não nullo");
        }

        [TestMethod]
        public void PropriedadePostoMedicoTahValida()
        {

            // obter a propriedade para utilização em POCO + DTO
            Classes.Instituicao postomedico = _pedidoAtendimento.PostoMedico;

            if (postomedico == null)
                Assert.Fail("Objeto null");

            var a = postomedico.Nome;

            // Passa o id da property relacionada
            _pedidoAtendimento2._codPostoMedico = 113;

            // obtem o objeto carregado do bd via propriedade
            var obj_1 = _pedidoAtendimento2.PostoMedico;

            // valida que não eh criada uma nova instancia
            var obj_2 = _pedidoAtendimento2.PostoMedico;

            // limpamos a property para forçar uma nova busca no mesmo objeto
            _pedidoAtendimento2.PostoMedico = null;

            // Informar uma chave invalida
            _pedidoAtendimento2._codPostoMedico = 99999;

            // carrega a property via bd novamente para o mesmo objeto
            var obj_3 = _pedidoAtendimento2.PostoMedico;

            if (obj_3 != null)
                Assert.Fail("Objeto não nullo");
        }

        [TestMethod]
        public void PropriedadePrioridadePedidoTest()
        {

            // obter a propriedade para utilização em POCO + DTO
            Classes.PedidoAtendimento.ETipoPedido etipopedido = _pedidoAtendimento.TipoPedido;

            etipopedido = Classes.PedidoAtendimento.ETipoPedido.Consulta;

            var valor = Convert.ToChar(etipopedido);
            var descricao = Hcrp.Framework.Infra.Util.EnumUtil.StringValueOf(etipopedido);
        }

        [TestMethod]
        public void PropriedadeMedicoSolicitanteTahValida()
        {

            // obter a propriedade para utilização em POCO + DTO
            Classes.Profissional medicosolicitante = _pedidoAtendimento.MedicoSolicitante;

            if (medicosolicitante == null)
                Assert.Fail("Objeto null");

            var a = medicosolicitante.Nome;

            // Passa o id da property relacionada
            _pedidoAtendimento2._codMedicoSolicitante = 329;

            // obtem o objeto carregado do bd via propriedade
            var obj_1 = _pedidoAtendimento2.MedicoSolicitante;

            // valida que não eh criada uma nova instancia
            var obj_2 = _pedidoAtendimento2.MedicoSolicitante;

            // limpamos a property para forçar uma nova busca no mesmo objeto
            _pedidoAtendimento2.MedicoSolicitante = null;

            // Informar uma chave invalida
            _pedidoAtendimento2._codMedicoSolicitante = 99999;

            // carrega a property via bd novamente para o mesmo objeto
            var obj_3 = _pedidoAtendimento2.MedicoSolicitante;

            if (obj_3 != null)
                Assert.Fail("Objeto não nullo");
        }
    }
}

