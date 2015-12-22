using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using Hcrp.Classes.ComunicaoWS;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL
{
    public class Material
    {
        /// <summary>
        /// Método que obtem uma lista de materias
        /// </summary>
        /// <param name="codigoMaterial">código do material</param>
        /// <param name="nomeMaterial">nome do material</param>
        /// <param name="codigoAlinea">código alinea</param>
        /// <returns></returns>
        public List<Entity.Material> ObterListaMaterias(string codigoMaterial, string nomeMaterial, int? codigoAlinea)
        {
            DAL.Material dalMaterial = new DAL.Material();

            return dalMaterial.ObterListaMateriais(codigoMaterial, nomeMaterial, codigoAlinea);
        }

        /// <summary>
        /// Obter lote, série, local atendente e seq insumo.
        /// </summary>               
        public void ObterLoteSerieLocalAtendenteESeqInsumo(long seqInsumoUnitario, out long numLote)
        {
            new DAL.Material().ObterLoteSerieLocalAtendenteESeqInsumo(seqInsumoUnitario, out numLote);
        }

        /// <summary>
        /// Obter o código de material e código de unidade do material por seq insumo.
        /// </summary>               
        public void ObterCodigoEUnidadeDoMaterialPorSeqInsumo(long seqInsumo, out int? codigoDaUnidade, out string codigoMaterial)
        {
            new DAL.Material().ObterCodigoEUnidadeDoMaterialPorSeqInsumo(seqInsumo, out codigoDaUnidade, out codigoMaterial);
        }

        /// <summary>
        /// Obter dados da instuição para livre esclarecido por código do instituto.
        /// </summary>        
        public DataView ObterEtiquetaBeiraLeitoPorNumeroDoLoteEmBase36(string numLoteEmBase36)
        {
            return new DAL.Material().ObterEtiquetaBeiraLeitoPorNumeroDoLoteEmBase36(numLoteEmBase36);
        }

        /// <summary>
        /// Obter o local do material pelo número do lote.
        /// </summary>               
        public int ObterLocalDoMaterialPeloNumeroDeLote(long numeroLote)
        {
            return new DAL.Material().ObterLocalDoMaterialPeloNumeroDeLote(numeroLote);
        }

        /// <summary>
        /// Obter o código de material e código de unidade do material por seq insumo unitário.
        /// </summary>               
        public void ObterCodigoEUnidadeDoMaterialPorSeqInsumoUnitario(long seqInsumoUnitario, out int? codigoDaUnidade, out string codigoMaterial)
        {
            new DAL.Material().ObterCodigoEUnidadeDoMaterialPorSeqInsumoUnitario(seqInsumoUnitario, out codigoDaUnidade, out codigoMaterial);
        }

        /// <summary>
        /// Método que retorna o material
        /// </summary>
        /// <param name="codigoMaterial">código do material</param>
        /// <returns>entidade material</returns>
        public List<Entity.Material> ObterMaterial(string codigoMaterial)
        {
            DAL.Material dalMaterial = new DAL.Material();

            return dalMaterial.ObterMaterial(codigoMaterial);
        }

        #region Código de barras
        /// <summary>
        /// Verificar se é um caracter de tipo de código de barras.
        /// </summary>               
        public bool EhCaracterDeTipoDeCodigoDeBarras(string caracter)
        {
            return new DAL.Material().EhCaracterDeTipoDeCodigoDeBarras(caracter);
        }

        /// <summary>
        /// Obter a unidade do material.
        /// </summary>               
        public void ObterCodigoDaUnidadeDoMaterial(string codigoMaterial, out string codMaterialEncontrado, out int? codUnidade)
        {
            new DAL.Material().ObterCodigoDaUnidadeDoMaterial(codigoMaterial, out codMaterialEncontrado, out codUnidade);
        }

        /// <summary>
        /// Obter a unidade do material.
        /// </summary>               
        public void ObterCodigoDaUnidadeDoMaterialComercial(string codigoMaterial, out string codMaterialEncontrado, out int? codUnidade)
        {
            new DAL.Material().ObterCodigoDaUnidadeDoMaterialComercial(codigoMaterial, out codMaterialEncontrado, out codUnidade);
        }

        public void ObtemInsumos(string codigoMaterial, out string codMaterialPuro, out long numLote)
        {
            codMaterialPuro = string.Empty;
            numLote = 0;

            try
            {
                #region ObterMaterial
                /*string subCaracter = string.Empty;
                var material = new BLL.Material();

                int? codUnidade = 0;
                var codMaterial = string.Empty;
                long numeroLote = 0;
                long seqInsumoUnitario = 0;
                long seqInsumo = 0;
                //var numSerie = null;



                long auxSeqNumSerie = 0;

                if (!string.IsNullOrWhiteSpace(codigoMaterial))
                {
                    codigoMaterial = codigoMaterial.ToUpper().Trim();

                    // Validar tipo de identificação de código de barra.
                    if (codigoMaterial.Length >= 2)
                    {
                        subCaracter = codigoMaterial.Substring(0, 2);

                        if (material.EhCaracterDeTipoDeCodigoDeBarras(subCaracter) == false)
                        {
                            #region condicional 1

                            // CONTAR O TOTAL DE CARACTERES INSERIDOS
                            // SE HOUVER EXATAMENTE 8 CARACTERES INFORMADOS RODAR A [query2].

                            if (codigoMaterial.Length == 8)
                            {
                                // Obter código da unidade.
                                material.ObterCodigoDaUnidadeDoMaterial(codigoMaterial, out codMaterial, out codUnidade);

                                if (string.IsNullOrWhiteSpace(codMaterial))
                                {
                                    material.ObterCodigoDaUnidadeDoMaterialComercial(codigoMaterial, out codMaterial, out codUnidade);
                                }

                                if (!string.IsNullOrWhiteSpace(codMaterial) && codUnidade != null)
                                {

                                }
                                else if (!string.IsNullOrWhiteSpace(codMaterial) && codUnidade == null)
                                {
                                    //this.ExibirAlertaLocal("alerta", "Não foi possível obter a unidade de medida para o item informado.");
                                }
                                else
                                {
                                    //if (this.RecursoBuscaBeiraLeito(codigoMaterial, out codMaterial, out numeroLote, out codUnidade) == false)
                                    //    return;
                                    if (RecursoBuscaBeiraLeito(codigoMaterial, out codMaterial, out numeroLote, out codUnidade) == false)
                                        return;
                                }
                            }
                            else
                            {
                                material.ObterCodigoDaUnidadeDoMaterialComercial(codigoMaterial, out codMaterial, out codUnidade);

                                if (!string.IsNullOrWhiteSpace(codMaterial) && codUnidade != null)
                                {
                                    // É um material.                                                                 
                                    // tipoInser = TipoDeInsercao.InsumoNaoIdentificadoTipoCodigoBarra;
                                }
                                else // if (!string.IsNullOrWhiteSpace(codMaterial) && codUnidade == null)
                                {
                                    string NumeroMaterial = "";
                                    if (codigoMaterial.Length >= 14)
                                    {
                                        string valor = codigoMaterial;
                                        string DataProducao = "";
                                        string DurabilidadeMinima = "";
                                        string DurabilidadeMaxima = "";
                                        string NumeroLote = "";
                                        string Quantidade = "";

                                        while (valor.Length > 0)
                                        {
                                            switch (Convert.ToInt32(valor.Substring(0, 2)))
                                            { // tratamento das informações fixas
                                                case 0:// Código de série de unidade de despacho
                                                    {// Informação desprezada
                                                        valor = valor.Remove(0, 20);
                                                        break;
                                                    }
                                                case 1:
                                                case 2:// Numero EAN do artigo
                                                    {
                                                        NumeroMaterial = (Convert.ToDouble(valor.Substring(2, 14))).ToString();
                                                        valor = valor.Remove(0, 16);
                                                        break;
                                                    }
                                                case 3:
                                                    {
                                                        valor = valor.Remove(0, 16);
                                                        break;
                                                    }
                                                case 4:
                                                    {
                                                        valor = valor.Remove(0, 18);
                                                        break;
                                                    }
                                                case 11: // Data de produção
                                                    {
                                                        DataProducao = valor.Substring(2, 6);
                                                        valor = valor.Remove(0, 8);
                                                        break;
                                                    }
                                                case 12:
                                                case 13:
                                                case 14:
                                                    {
                                                        valor = valor.Remove(0, 8);
                                                        break;
                                                    }
                                                case 15: // DurabilidadeMinima
                                                    {
                                                        DurabilidadeMinima = valor.Substring(2, 6);
                                                        valor = valor.Remove(0, 8);
                                                        break;
                                                    }
                                                case 16:
                                                    {
                                                        valor = valor.Remove(0, 8);
                                                        break;
                                                    }
                                                case 17: // DurabilidadeMaxima
                                                    {
                                                        DurabilidadeMaxima = valor.Substring(2, 6);
                                                        valor = valor.Remove(0, 8);
                                                        break;
                                                    }
                                                case 18:
                                                case 19:
                                                    {
                                                        valor = valor.Remove(0, 8);
                                                        break;
                                                    }
                                                case 20:
                                                    {
                                                        valor = valor.Remove(0, 4);
                                                        break;
                                                    }
                                                case 31:
                                                case 32:
                                                case 33:
                                                case 34:
                                                case 35:
                                                case 36:
                                                    {
                                                        valor = valor.Remove(0, 10);
                                                        break;
                                                    }
                                                case 41:
                                                    {
                                                        valor = valor.Remove(0, 10);
                                                        break;
                                                    }
                                                default:
                                                    { // Valores variáveis (Verificar caracteres de finalização)
                                                        if (ProcuraSeparador(valor) > 0) // se encontrar um caracter separador
                                                        { // rotina ainda não tratada
                                                            string ValorVariavel;
                                                            int Separador = ProcuraSeparador(valor);
                                                            if (valor.Length == Separador)
                                                                ValorVariavel = valor.Substring(2, Separador);
                                                            else
                                                                ValorVariavel = valor.Substring(2, Separador - 3);

                                                            switch (Convert.ToInt32(valor.Substring(0, 2)))
                                                            {

                                                                case 10: // Numero de Lote
                                                                    {
                                                                        NumeroLote = ValorVariavel;
                                                                        break;
                                                                    }
                                                                case 37: // Quantidade
                                                                    {
                                                                        Quantidade = ValorVariavel;
                                                                        break;
                                                                    }
                                                            }// fim case
                                                            valor = valor.Remove(0, Separador);
                                                        }// fim se encontrou caracter separador
                                                        else valor = "";
                                                        break;
                                                    }// fim case valor fixo

                                            }
                                        }// Fim enquanto
                                    }

                                    material.ObterCodigoDaUnidadeDoMaterialComercial(NumeroMaterial, out codMaterial, out codUnidade);

                                    if (string.IsNullOrWhiteSpace(codMaterial))
                                    {
                                        material.ObterCodigoDaUnidadeDoMaterialComercial(NumeroMaterial, out codMaterial, out codUnidade);
                                    }

                                    if (!string.IsNullOrWhiteSpace(codMaterial) && codUnidade != null)
                                    {
                                        // É um material.                                                                 
                                        //tipoInser = TipoDeInsercao.InsumoNaoIdentificadoTipoCodigoBarra;
                                    }
                                    else if (!string.IsNullOrWhiteSpace(codMaterial) && codUnidade == null)
                                    {
                                        //this.ExibirAlertaLocal("alerta", "Não foi possível obter a unidade de medida para o item informado.");
                                    }
                                }
                                //else
                                //{
                                //    //if (this.RecursoBuscaBeiraLeito() == false)
                                //    //    return;
                                //}
                            }

                            #endregion
                        }
                        else if (subCaracter == "IR")
                        {
                            #region condicional 2

                            // O RESTANTE DOS CARACTERES É O SEQ_INSUMO_UNITARIO
                            if (Int64.TryParse(codigoMaterial.Replace("IR", ""), out seqInsumoUnitario) == true)
                            {
                                material.ObterCodigoEUnidadeDoMaterialPorSeqInsumoUnitario(seqInsumoUnitario, out codUnidade, out codMaterial);

                                if (!string.IsNullOrWhiteSpace(codMaterial) && codUnidade != null)
                                {
                                    material.ObterLoteSerieLocalAtendenteESeqInsumo(seqInsumoUnitario, out numeroLote);
                                }
                                else
                                {
                                    /*if (string.IsNullOrWhiteSpace(this.codMaterial))
                                        this.ExibirAlertaLocal("alerta", "Não foi possível obter o código do material para o item informado.");
                                    else if (this.codUnidade == null)
                                        this.ExibirAlertaLocal("alerta", "Não foi possível obter a unidade de medida para o item informado.");

                                    return;
                                }
                            }
                            else
                            {
                                //this.MensagemPadraoDematerialNaoReconhecido();
                                return;
                            }

                            #endregion
                        }
                        else if (subCaracter == "IL")
                        {
                            #region condicional 3

                            // CAPTURAR SEQ_INSUMO (12 CARACTERES A PARTIR DA 3ª CASA
                            // CAPTURAR NUM_LOTE (12 CARACTERES A PARTIR DA 16ª CASA 

                            if (codigoMaterial.Length > 26)
                            {
                                if ((long.TryParse(codigoMaterial.Substring(2, 12), out seqInsumo) == true) &&
                                    (long.TryParse(codigoMaterial.Substring(15, 12), out numeroLote) == true))
                                {
                                    material.ObterCodigoEUnidadeDoMaterialPorSeqInsumo(seqInsumo, out codUnidade, out codMaterial);

                                    if (!string.IsNullOrWhiteSpace(codMaterial) && codUnidade != null)
                                    {

                                    }
                                    else
                                    {
                                        /*if (string.IsNullOrWhiteSpace(codMaterial))
                                            this.ExibirAlertaLocal("alerta", "Não foi possível obter o código do material para o item informado.");
                                        else if (this.codUnidade == null)
                                            this.ExibirAlertaLocal("alerta", "Não foi possível obter a unidade de medida para o item informado.");
                                        
                                        return;
                                    }

                                }
                                else
                                {
                                    /*if (this.seqInsumo == 0)
                                        this.ExibirAlertaLocal("alerta", "Não foi possível obter o número sequencial do insumo para o item informado.");
                                    else if (this.numeroLote == 0)
                                        this.ExibirAlertaLocal("alerta", "Não foi possível obter o número do lote para o item informado.");
                                    
                                    return;
                                }

                            }
                            else
                            {
                                //this.MensagemPadraoDematerialNaoReconhecido();

                                return;
                            }

                            #endregion
                        }
                        else if (subCaracter == "IS")
                        {
                            #region condicional 4

                            // CAPTURAR SEQ_INSUMO (12 CARACTERES A PARTIR DA 3ª CASA
                            // CAPTURAR SEQ_NUM_SERIE (12 CARACTERES A PARTIR DA 16ª CASA

                            if (codigoMaterial.Length > 26)
                            {

                                if ((long.TryParse(codigoMaterial.Substring(2, 12), out seqInsumo) == true) &&
                                    (long.TryParse(codigoMaterial.Substring(15, 12), out auxSeqNumSerie) == true))
                                {
                                    material.ObterCodigoEUnidadeDoMaterialPorSeqInsumo(seqInsumo, out codUnidade, out codMaterial);

                                    if (!string.IsNullOrWhiteSpace(codMaterial) && codUnidade != null)
                                    {

                                        /*if (this.numSeqLocal > 0 && this.numeroLote > 0)
                                        {
                                            this.numSerie = auxSeqNumSerie;

                                            this.tipoInser = TipoDeInsercao.InsumoIdentificadoTipoCodigoBarraIS;
                                        }
                                        else
                                        {
                                            if (this.numSeqLocal == 0)
                                                this.ExibirAlertaLocal("alerta", "Não foi possível obter o local para o item informado.");
                                            else if (this.numeroLote == 0)
                                                this.ExibirAlertaLocal("alerta", "Não foi possível obter o número do lote para o item informado.");

                                            return;
                                        }
                                    }
                                    else
                                    {
                                        /*if (string.IsNullOrWhiteSpace(this.codMaterial))
                                            this.ExibirAlertaLocal("alerta", "Não foi possível obter o código do material para o item informado.");
                                        else if (this.codUnidade == null)
                                            this.ExibirAlertaLocal("alerta", "Não foi possível obter a unidade de medida para o item informado.");
                                       
                                        return;
                                    }
                                }
                                else
                                {
                                    /*if (this.seqInsumo == 0)
                                        this.ExibirAlertaLocal("alerta", "Não foi possível obter o número sequencial do insumo para o item informado.");
                                    else if (auxSeqNumSerie == 0)
                                        this.ExibirAlertaLocal("alerta", "Não foi possível obter o número de série para o item informado.");
                                   
                                    return;
                                }

                            }
                            else
                            {
                                //this.MensagemPadraoDematerialNaoReconhecido();
                                return;
                            }

                            #endregion
                        }
                        else if (subCaracter == "LR")
                        {
                            #region condicional 5

                            codigoMaterial = codigoMaterial.Substring(2);

                            if (RecursoBuscaBeiraLeito(codigoMaterial, out codMaterial, out numeroLote, out codUnidade) == false)
                                return;

                            #endregion
                        }
                    }

                    // Conversão de para unidade de consumo, se existir, muda o código de unidade, senão, mantem o que está

                    /*long codUnidadeConvertido = 0;
                    decimal qtdFatorConvertido = 0;
                    new Infra.Classes.MaterialUsoCirurgia().ObterConversaoUnidadeMaterial(codMaterial, out codUnidadeConvertido, out qtdFatorConvertido);

                    if (codUnidadeConvertido == 0)
                    {
                        qtdFator = 1;
                    }
                    else
                    {
                        codUnidade = (int)codUnidadeConvertido;
                        qtdFator = qtdFatorConvertido == 0 ? 1 : qtdFatorConvertido;
                    }*/

                // Se estiver flgado para inserir automaticamente, então fazer.
                // INserir padrão 1 unidade.
                /*if (this.chkIncluirAoFazerLeitura.Checked == true)
                {
                    this.qtdInformada = 1;
                    this.InserirInsumo();
                    codigoMaterial = "";
                }
                else
                {
                    this.ObterOTipoItem();
                }
            }
            else
            {
                //this.ExibirAlertaLocal("alerta", "A identificação do insumo deve ser informada.");
            }*/
                #endregion

                Classes.ComunicaoWS.Material dadosMaterial = null;
                try
                {

                    string ws = Parametrizacao.Instancia().EnderecoServico;
                    string metodo = Parametrizacao.Instancia().ServicoConsultaMaterial + codigoMaterial.ToUpper();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ws);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = client.GetAsync(metodo).Result;
                        //response.EnsureSuccessStatusCode();

                        if (response.IsSuccessStatusCode)
                        {
                            var streamJson = response.Content.ReadAsStreamAsync().Result;
                            var ser = new DataContractJsonSerializer(typeof(Classes.ComunicaoWS.Material));
                            dadosMaterial = (Classes.ComunicaoWS.Material)ser.ReadObject(streamJson);

                            if (dadosMaterial.Retorno == StatusRetorno.Erro)
                            {
                                //throw new Exception(dadosMaterial.MensagemRetorno);
                                return;
                            }

                            codMaterialPuro = dadosMaterial.CodigoPuro;

                            long.TryParse(dadosMaterial.Lote, out numLote);
                        }
                        else
                        {
                            // throw new Exception("Ocorreu um erro ao obter o material informado. Tente novamente.");
                            //return;
                        }
                    }
                }
                catch (Exception)
                {
                    //return; //throw new Exception("Ocorreu um erro ao obter o material informado. Tente novamente.");
                }
            }
            catch (Exception ex)
            {
                //throw;
                //this.lblErro.Text = "O seguinte erro ocorreu: " + ex.ToString();
            }
        }

        /// <summary>
        /// Recurso de busca Beira Leito.
        /// </summary>
        protected bool RecursoBuscaBeiraLeito(string codigoMaterial, out string codMaterial, out long numLote, out int? codUnidade)
        {
            bool encontrado = false;

            try
            {
                var material = new BLL.Material();
                numLote = 0;
                codMaterial = string.Empty;
                codUnidade = null;

                // Busca etiqueta beira leito.
                // Buscar somente se o primiero caracter for "1".
                if (codigoMaterial.Substring(0, 1) == "1")
                {
                    DataView dtView = material.ObterEtiquetaBeiraLeitoPorNumeroDoLoteEmBase36(codigoMaterial.Substring(1));
                    if (dtView != null && dtView.Count > 0)
                    {
                        if (dtView[0]["NUM_LOTE"] != DBNull.Value)
                            numLote = Convert.ToInt64(dtView[0]["NUM_LOTE"]);

                        if (dtView[0]["COD_MATERIAL"] != DBNull.Value)
                            codMaterial = dtView[0]["COD_MATERIAL"].ToString();

                        if (dtView[0]["COD_UNIDADE"] != DBNull.Value)
                            codUnidade = Convert.ToInt32(dtView[0]["COD_UNIDADE"]);

                        if (numLote > 0 &&
                            !string.IsNullOrWhiteSpace(codMaterial) &&
                            codUnidade.HasValue)
                        {
                            encontrado = true;
                        }
                        else
                        {
                            /*if (this.numeroLote == 0)
                                this.ExibirAlertaLocal("alerta", "Não foi possível obter o número do lote para o item informado.");
                            else if (string.IsNullOrWhiteSpace(this.codMaterial))
                                this.ExibirAlertaLocal("alerta", "Não foi possível obter o código do material para o item informado.");
                            else if (this.codUnidade == null)
                                this.ExibirAlertaLocal("alerta", "Não foi possível obter a unidade de medida para o item informado.");*/
                            //else if (this.numSeqLocal == 0)
                            //    this.ExibirAlertaLocal("alerta", "Não foi possível obter o local para o item informado.");
                        }
                    }
                    else
                    {
                        //this.MensagemPadraoDematerialNaoReconhecido();
                    }

                }
                else
                {
                    //this.ExibirMensagemBeiraLeitoNaoRegistra(this.txtCodBarras.Text.Substring(0, 1));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return encontrado;
        }

        public int ProcuraSeparador(string ValorLido)
        {

            int i = 0;
            bool achouSeparador = false;

            int posicao_separador = 0;

            while ((i <= ValorLido.Length) && (!achouSeparador))
            {
                if (!(ValorLido[i].ToString().All(Char.IsLetterOrDigit)))
                {
                    //           (ValorLido[i]= not in []) AND // verificar lista de caracteres possíveis
                    //           (ValorLido[i]='&') or // utilizados como separadores
                    //           (ValorLido[i]='*') then
                    posicao_separador = i;
                    achouSeparador = true;
                }
                else
                    i = i + 1;
            }// fim procurando separador

            if (!achouSeparador)
            {
                posicao_separador = ValorLido.Length;
            }


            return posicao_separador;
        }
        #endregion
    }
}
