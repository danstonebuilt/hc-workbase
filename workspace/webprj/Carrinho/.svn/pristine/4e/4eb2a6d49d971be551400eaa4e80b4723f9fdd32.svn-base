using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL
{
    public class Util
    {
        /// <summary>
        /// Obter o mês por extenso.
        /// </summary>        
        public string ObterMesPorExtenso(Int32 numeroMes)
        {
            string retornoMes = "-";

            try
            {
                if (numeroMes == 1)
                    retornoMes = "JANEIRO";
                else if (numeroMes == 2)
                    retornoMes = "FEVEREIRO";
                else if (numeroMes == 3)
                    retornoMes = "MARÇO";
                else if (numeroMes == 4)
                    retornoMes = "ABRIL";
                else if (numeroMes == 5)
                    retornoMes = "MAIO";
                else if (numeroMes == 6)
                    retornoMes = "JUNHO";
                else if (numeroMes == 7)
                    retornoMes = "JULHO";
                else if (numeroMes == 8)
                    retornoMes = "AGOSTO";
                else if (numeroMes == 9)
                    retornoMes = "SETEMBRO";
                else if (numeroMes == 10)
                    retornoMes = "OUTUBRO";
                else if (numeroMes == 11)
                    retornoMes = "NOVEMBRO";
                else if (numeroMes == 12)
                    retornoMes = "DEZEMBRO";
            }
            catch (Exception)
            {
                throw;
            }

            return retornoMes;
        }

        public byte[] GerarByteCodigoDeBarra(string codigo)
        {
            try
            {
                string url = System.Configuration.ConfigurationManager.AppSettings["UrlSistema"] + "/GerarCodigoDeBarra.aspx?code=" + codigo;

                //System.Net.WebRequest req = System.Net.WebRequest.Create(url);
                //System.Net.WebResponse res = req.GetResponse();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = true;
                request.AllowAutoRedirect = true;
                request.ServicePoint.Expect100Continue = false;
                request.CookieContainer = new CookieContainer();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                byte[] imageData = new byte[response.ContentLength];
                Stream s = response.GetResponseStream();
                s.Read(imageData, 0, (int)response.ContentLength);

                return imageData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Método acha idade.
        /// </summary>        
        public string AchaIdade(DateTime dataNascimento)
        {
            string valorRetorno = string.Empty;

            try
            {
                string Qdm = "312831303130313130313031";   // Qtde dia no mes, sera +1 quando for bissexto
                DateTime dataHojeCalc = DateTime.MinValue;
                DateTime dataNascimentoCalc = DateTime.MinValue;

                Int32 anos = 0, meses = 0, dias = 0, nrd = 0;
                Int32 anoAux = 0, mesAux = 0, diaAux = 0;

                string aux_idade = string.Empty;

                if (dataNascimento != DateTime.MinValue)
                {
                    this.DecodificarData(this.DataServidor(false), out anoAux, out mesAux, out diaAux);                    
                    dataHojeCalc = new DateTime(anoAux, mesAux, diaAux);
                    //dataHojeCalc = new DateTime(2012, 11, 07);                    

                    this.DecodificarData(dataNascimento, out anoAux, out mesAux, out diaAux);
                    dataNascimentoCalc = new DateTime(anoAux, mesAux, diaAux);

                    anos = dataHojeCalc.Year - dataNascimentoCalc.Year;
                    meses = dataHojeCalc.Month - dataNascimentoCalc.Month;
                    dias = dataHojeCalc.Day - dataNascimentoCalc.Day;

                    if (dias < 0)
                    {
                        nrd = Convert.ToInt32(Qdm.Substring((dataHojeCalc.Month - 1) * (2 - 1), 2));

                        if (dataHojeCalc.Month == 1)
                            nrd = Convert.ToInt32(Qdm.Substring(23, 2));

                        if ((dataHojeCalc.Month - 1) == 2 && this.AnoBissexto(DateTime.Now.Date) == true)
                        {
                            nrd = nrd + 1;
                        }

                        dias = dias + nrd;
                        meses = meses - 1;
                    }

                    if (meses < 0)
                    {
                        anos = anos - 1;
                        meses = meses + 12;
                    }

                    aux_idade = "";

                    if (anos != 0)
                        aux_idade = anos.ToString() + " Ano(s) ";

                    if (meses != 0)
                        aux_idade = aux_idade + meses.ToString() + " Mes(es) ";

                    if (dias != 0)
                        aux_idade = aux_idade + dias.ToString() + " Dia(s) ";

                    valorRetorno = aux_idade;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return valorRetorno;
        }

        /// <summary>
        /// Validar se o ano é bissexto.
        /// </summary>        
        public bool AnoBissexto(DateTime data)
        {
            bool ehBissexto = false;

            try
            {
                int dia = 0, mes = 0, ano = 0;

                this.DecodificarData(data, out ano, out mes, out dia);

                //int resto = 3 % 2; 

                if ((ano % 4) != 0)
                {
                    ehBissexto = false;
                }
                else if ((ano % 100) != 0)
                {
                    ehBissexto = true;
                }
                else if ((ano % 400) != 0)
                {
                    ehBissexto = false;
                }
                else
                {
                    ehBissexto = true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return ehBissexto;
        }

        /// <summary>
        /// Decodificar a data.
        /// </summary>        
        protected void DecodificarData(DateTime data, out int ano, out int mes, out int dia)
        {
            ano = 0;
            mes = 0;
            dia = 0;

            try
            {
                ano = data.Year;
                mes = data.Month;
                dia = data.Day;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Data do servidor.
        /// </summary>        
        protected DateTime DataServidor(bool dataComHora)
        {
            DateTime dataHoraRetorno = DateTime.MinValue;

            try
            {
                DateTime dataHoraDoBancoDeDados = new DAL.Util().ObterDataEHoraDoBanco(dataComHora);
                string stmp = string.Empty;

                dataHoraRetorno = dataHoraDoBancoDeDados;
            }
            catch (Exception)
            {
                throw;
            }

            return dataHoraRetorno;
        }

        public string ReplaceWithBR(string target) 
        { 
            Regex regex = new Regex(@"(\r\n|\r|\n)+"); 
            string newText = regex.Replace(target, "<br />");

            return newText;
        }

        /// <summary>
        /// Obter servidor de imagem
        /// </summary>        
        public string ObterServidorDeImagem()
        {
            return new DAL.Util().ObterServidorDeImagem();
        }

        /// <summary>
        /// Obter servidor de imagem e servidor web.
        /// </summary>        
        public DataView ObterServidorDeImagemEServidorWeb(string servidorImagem, Int64 numExame, Int32 digito)
        {
            return new DAL.Util().ObterServidorDeImagemEServidorWeb(servidorImagem, numExame, digito);
        }

        /// <summary>
        /// Obter servidor web
        /// </summary>        
        public string ObterServidorWeb()
        {   
            return new DAL.Util().ObterServidorWeb();
        }

        /// <summary>
        /// Onter dia da semana atraves de uma data.
        /// </summary>        
        public string ObterDiaDaSemanaEmPortuguesAtravesDaData(DateTime data)
        {
            string retorno = string.Empty;
            
            try
            {
                if (data.DayOfWeek == DayOfWeek.Sunday)
                    retorno = "DOMINGO";
                else if (data.DayOfWeek == DayOfWeek.Monday)
                    retorno = "DOMINGO";
                else if (data.DayOfWeek == DayOfWeek.Tuesday)
                    retorno = "TERÇA";
                else if (data.DayOfWeek == DayOfWeek.Wednesday)
                    retorno = "QUARTA";
                else if (data.DayOfWeek == DayOfWeek.Thursday)
                    retorno = "QUINTA";
                else if (data.DayOfWeek == DayOfWeek.Friday)
                    retorno = "SEXTA";
                else if (data.DayOfWeek == DayOfWeek.Saturday)
                    retorno = "SÁBADO";
            }
            catch (Exception)
            {
                throw;
            }

            return retorno;
        }

        /// <summary>
        /// Montar mapeamento menu.
        /// </summary>
        public string MontarMapeamentoMenu(string nomeMenu, string ultimoNoMenu)
        {
            try
            {
                List<string> caminhoMontado = new List<string>();
                string retorno = string.Empty;

                Hcrp.CarroUrgenciaPsicoativo.Entity.Programa programa = null;

                // tentar em 10 laço no máximo.
                for (int i = 0; i < 10; i++)
                {
                    programa = new Hcrp.CarroUrgenciaPsicoativo.BLL.Programa().ObterProgramaPorNomePrograma(nomeMenu, Hcrp.CarroUrgenciaPsicoativo.BLL.Parametrizacao.Instancia().CodigoDoSistema);

                    if (programa != null && !string.IsNullOrWhiteSpace(programa.NomeExibicaoPrograma))
                    {
                        caminhoMontado.Add(programa.NomeExibicaoPrograma);
                    }

                    if (programa != null && !string.IsNullOrWhiteSpace(programa.NomeProgramaPai))
                        nomeMenu = programa.NomeProgramaPai;
                    else
                        break;
                }

                // Monta o menu.
                for (int i = caminhoMontado.Count - 1; i >= 0; i--)
                {
                    if (!string.IsNullOrWhiteSpace(retorno))
                        retorno = retorno + " - ";

                    retorno = retorno + caminhoMontado[i];
                }

                // adiciona o ultimo no do menu.
                if (!string.IsNullOrWhiteSpace(retorno))
                    retorno = retorno + " - ";

                retorno = retorno + ultimoNoMenu;

                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter diferença entre duas datas retornando em dias.
        /// </summary>        
        public string ObterDiferencaEntreDuasDatasRetornandoEmDias(DateTime dataInicial, DateTime dataFinal)
        {
            return new DAL.Util().ObterDiferencaEntreDuasDatasRetornandoEmDias(dataInicial, dataFinal);
        }

    }
}
