using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class Paciente : Hcrp.Framework.Classes.Paciente
    {
        public Hcrp.Framework.Classes.Paciente BuscarPacienteRegistro(String codigoPaciente)
        {
            List<Hcrp.Framework.Classes.ParametrosOracle> parametros = new List<Classes.ParametrosOracle>();
            Hcrp.Framework.Classes.ParametrosOracle p = new Hcrp.Framework.Classes.ParametrosOracle();
            p.CampoOracle = "COD_PACIENTE";
            p.ValorOracle = codigoPaciente;
            parametros.Add(p);
            List<Hcrp.Framework.Classes.Paciente> l = this.BuscarPacientes(parametros);
            if (l.Count > 0)
                return l[0];
            else
                return null;
        }

        public List<Framework.Classes.Paciente> BuscarPacientes(String codigoPaciente, String nomePaciente, String sobrenomePaciente, Boolean pesquisaFonetica)
        {
            List<Hcrp.Framework.Classes.ParametrosOracle> parametros = new List<Classes.ParametrosOracle>();
            Hcrp.Framework.Classes.ParametrosOracle p = new Hcrp.Framework.Classes.ParametrosOracle();
            if (!string.IsNullOrEmpty(codigoPaciente))
            {
                p.CampoOracle = "COD_PACIENTE";
                p.ValorOracle = codigoPaciente;
                parametros.Add(p);
            }
            if (!string.IsNullOrEmpty(nomePaciente))
            {
                p = new Hcrp.Framework.Classes.ParametrosOracle();
                p.CampoOracle = pesquisaFonetica ? "NOM_FONETICO" : "NOM_PACIENTE";
                p.TipoAssociacao = Hcrp.Framework.Classes.ParametrosOracle.eTipoAssociacao.Like;
                p.ValorOracle = "%" + (pesquisaFonetica ? nomePaciente.ToLower() : nomePaciente) + "%";
                parametros.Add(p);
            }
            if (!string.IsNullOrEmpty(sobrenomePaciente))
            {
                p = new Hcrp.Framework.Classes.ParametrosOracle();
                p.CampoOracle = pesquisaFonetica ? "SBN_FONETICO" : "SBN_PACIENTE";
                p.TipoAssociacao = Hcrp.Framework.Classes.ParametrosOracle.eTipoAssociacao.Like;
                p.ValorOracle = "%" + (pesquisaFonetica ? sobrenomePaciente.ToLower() : sobrenomePaciente) + "%";
                parametros.Add(p);
            }

            List<Hcrp.Framework.Classes.Paciente> l = new List<Hcrp.Framework.Classes.Paciente>();
            if(parametros.Count > 0)
                l = this.BuscarPacientes(parametros);
            return l;
        }


        public List<Hcrp.Framework.Classes.Paciente> BuscarPacientes(List<Hcrp.Framework.Classes.ParametrosOracle> filtros)
        {
            List<Hcrp.Framework.Classes.Paciente> l = new List<Hcrp.Framework.Classes.Paciente>();

            try
            {
                #region Tratamento de filtros.
                // Como está chamada é realizada por filtros que são utilizados na busca de paciente
                // e busca de dados do paciente em pedido atendimento, alguns nome de campos são diferentes entre
                // paciente e pedido atendimento. Então um de-para é realizado.
                List<Hcrp.Framework.Classes.ParametrosOracle> filtrosL = new List<Classes.ParametrosOracle>();
                Hcrp.Framework.Classes.ParametrosOracle deParaFiltro = null;
                foreach (Hcrp.Framework.Classes.ParametrosOracle item in filtros)
                {
                    deParaFiltro = new Classes.ParametrosOracle();

                    if (!string.IsNullOrWhiteSpace(item.CampoOracle))
                        deParaFiltro.CampoOracle = item.CampoOracle;
                    
                    deParaFiltro.TipoAssociacao = item.TipoAssociacao;

                    if (item.ValorOracle.ToString() != "")
                        deParaFiltro.ValorOracle = item.ValorOracle;

                    if (deParaFiltro.CampoOracle == "NUM_CPF")
                        deParaFiltro.CampoOracle = "CPF_PACIENTE";

                    filtrosL.Add(deParaFiltro);
                }
                #endregion


                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    //sb.Append(" SELECT B.COD_PACIENTE REGISTRO, " + Environment.NewLine);
                    //sb.Append("        B.NOM_PACIENTE, B.SBN_PACIENTE,  " + Environment.NewLine);
                    //sb.Append("        DECODE(B.IDF_SEXO,'M','MASCULINO','F','FEMININO','DESCONHECIDO') SEXO, " + Environment.NewLine);
                    //sb.Append("        B.DTA_NASCIMENTO, " + Environment.NewLine);
                    //sb.Append("        B.NOM_MAE, " + Environment.NewLine);
                    //sb.Append("        B.IDF_COR, " + Environment.NewLine);
                    //sb.Append("        FCN_DIFERENCA_DATA(SYSDATE,DTA_NASCIMENTO,1) IDADE " + Environment.NewLine);
                    //sb.Append(" FROM PACIENTE B " + Environment.NewLine);
                    //sb.Append(" WHERE 1 = 1 " + Environment.NewLine);

                    sb.AppendLine(" SELECT  DISTINCT ");
                    sb.AppendLine("  B.COD_PACIENTE REGISTRO, ");
                    sb.AppendLine("  B.NOM_PACIENTE, ");
                    sb.AppendLine("  B.SBN_PACIENTE, ");
                    sb.AppendLine("  DECODE(B.IDF_SEXO,'M','MASCULINO','F','FEMININO','DESCONHECIDO') SEXO, ");
                    sb.AppendLine("  B.DTA_NASCIMENTO, ");
                    sb.AppendLine("  B.NOM_MAE,  ");
                    sb.AppendLine("  B.IDF_COR, ");
                    sb.AppendLine("  FCN_DIFERENCA_DATA(SYSDATE,DTA_NASCIMENTO,1) DSC_IDADE_APARENTE, ");
                    sb.AppendLine("  B.CEP_PACIENTE,  ");
                    sb.AppendLine("  B.NUM_ENDERECO, ");
                    sb.AppendLine("  B.DSC_TIPO_LOGRADOURO, ");
                    sb.AppendLine("  B.CPL_ENDERECO, ");
                    sb.AppendLine("  B.NOM_BAIRRO, ");
                    sb.AppendLine("  B.NOM_CIDADE, ");
                    sb.AppendLine("  B.DSC_ENDERECO, ");
                    sb.AppendLine("  B.NUM_CNS,  ");
                    sb.AppendLine("  B.NUM_TELEFONE, ");
                    sb.AppendLine("  B.DSC_EMAIL, ");
                    sb.AppendLine("  B.CPF_PACIENTE, ");
                    sb.AppendLine("  B.NUM_RG, ");
                    sb.AppendLine("  B.COD_ORGAO_EMISSOR, ");
                    sb.AppendLine("  B.IDF_UF_RG,  ");
                    sb.AppendLine("  B.COD_GRAU_INSTRUCAO, ");
                    sb.AppendLine("  B.DTA_OBITO, ");
                    sb.AppendLine("  B.SGL_PAIS || '|' || B.SGL_UF || '|' || B.COD_LOCALIDADE CHAVE_LOCALIDADE  ");
                    sb.AppendLine(" FROM PACIENTE B ");
                    sb.AppendLine(" WHERE 1 = 1 ");

                    foreach (var item in filtrosL)
                    {
                        if (item.CampoOracle != "COD_INSTITUICAO")
                        {
                            if (item.TipoAssociacao == Hcrp.Framework.Classes.ParametrosOracle.eTipoAssociacao.Igual)
                                sb.Append(" AND B." + item.CampoOracle + " = :" + item.CampoOracle + Environment.NewLine);
                            else if (item.TipoAssociacao == Hcrp.Framework.Classes.ParametrosOracle.eTipoAssociacao.Like)
                                sb.Append(" AND B." + item.CampoOracle + " LIKE :" + item.CampoOracle + Environment.NewLine);
                        }
                    }
                    sb.Append(" ORDER BY B.NOM_PACIENTE, B.SBN_PACIENTE, B.DTA_NASCIMENTO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    foreach (var item in filtrosL)
                    {
                        if (item.CampoOracle != "COD_INSTITUICAO")
                        {
                            if (item.TipoAssociacao == Hcrp.Framework.Classes.ParametrosOracle.eTipoAssociacao.Igual)
                                query.Params[item.CampoOracle] = item.ValorOracle;
                            else if (item.TipoAssociacao == Hcrp.Framework.Classes.ParametrosOracle.eTipoAssociacao.Like)
                                query.Params[item.CampoOracle] = item.ValorOracle + "%";
                        }
                    }

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.Paciente p = new Hcrp.Framework.Classes.Paciente();

                        if (dr["REGISTRO"] != DBNull.Value)
                            p.RegistroPaciente = Convert.ToString(dr["REGISTRO"]);

                        if (dr["NOM_PACIENTE"] != DBNull.Value)
                            p.NomePaciente = Convert.ToString(dr["NOM_PACIENTE"]);

                        if (dr["SBN_PACIENTE"] != DBNull.Value)
                            p.SobrenomePaciente = Convert.ToString(dr["SBN_PACIENTE"]);

                        if (dr["SEXO"] != DBNull.Value)
                            p.SexoPaciente = Convert.ToString(dr["SEXO"]);

                        if (dr["DTA_NASCIMENTO"] != DBNull.Value)
                            p.DataNascimento = Convert.ToDateTime(dr["DTA_NASCIMENTO"]);

                        if (dr["NOM_MAE"] != DBNull.Value)
                            p.MaePaciente = Convert.ToString(dr["NOM_MAE"]);

                        //if (dr["IDADE"] != DBNull.Value)
                        //    p.Idade = Convert.ToString(dr["IDADE"]);

                        if (dr["IDF_COR"] != DBNull.Value)
                            p.Cor = (ECorPaciente)Convert.ToInt32(dr["IDF_COR"]);

                        if (dr["DSC_IDADE_APARENTE"] != DBNull.Value)
                            p.Idade =dr["DSC_IDADE_APARENTE"].ToString();

                        if (dr["CEP_PACIENTE"] != DBNull.Value)
                            p.CepEndereco = dr["CEP_PACIENTE"].ToString();

                        if (dr["NUM_ENDERECO"] != DBNull.Value)
                            p.NumeroEndereco = dr["NUM_ENDERECO"].ToString();

                        if (dr["DSC_TIPO_LOGRADOURO"] != DBNull.Value)
                            p.TipoLogradouro = dr["DSC_TIPO_LOGRADOURO"].ToString();

                        if (dr["CPL_ENDERECO"] != DBNull.Value)
                            p.ComplementoEndereco = dr["CPL_ENDERECO"].ToString();

                        if (dr["NOM_BAIRRO"] != DBNull.Value)
                            p.NomeBairro = dr["NOM_BAIRRO"].ToString();

                        if (dr["NOM_CIDADE"] != DBNull.Value)
                            p.NomeCidade = dr["NOM_CIDADE"].ToString();

                        if (dr["DSC_ENDERECO"] != DBNull.Value)
                            p.DescricaoEndereco = dr["DSC_ENDERECO"].ToString();

                        if (dr["NUM_CNS"] != DBNull.Value)
                            p.NumeroCNS = dr["NUM_CNS"].ToString();

                        if (dr["NUM_TELEFONE"] != DBNull.Value)
                            p.TelefoneContato = dr["NUM_TELEFONE"].ToString();

                        if (dr["DSC_EMAIL"] != DBNull.Value)
                            p.Email = dr["DSC_EMAIL"].ToString();

                        if (dr["CPF_PACIENTE"] != DBNull.Value)
                            p.CPFPaciente = dr["CPF_PACIENTE"].ToString();

                        if (dr["NUM_RG"] != DBNull.Value)
                            p.RGPaciente = dr["NUM_RG"].ToString();

                        if (dr["COD_ORGAO_EMISSOR"] != DBNull.Value)
                            p.CodOrgaoEmissor = Convert.ToInt32(dr["COD_ORGAO_EMISSOR"]);

                        if (dr["IDF_UF_RG"] != DBNull.Value)
                            p.IdfUfRG = dr["IDF_UF_RG"].ToString();

                        if (dr["COD_GRAU_INSTRUCAO"] != DBNull.Value)
                            p._codGrauInstrucao = Convert.ToInt32(dr["COD_GRAU_INSTRUCAO"]);

                        if (dr["CHAVE_LOCALIDADE"] != DBNull.Value)
                            p._chaveMunicipio = dr["CHAVE_LOCALIDADE"].ToString();

                        if (dr["DTA_OBITO"] != DBNull.Value)
                            p.DataHoraObito = Convert.ToDateTime(dr["DTA_OBITO"].ToString());

                        

                        l.Add(p);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Hcrp.Framework.Classes.Paciente> BuscaPacientesEspecialidade(Hcrp.Framework.Classes.Especialidade especialidade, int qtdDiasAnteriores = 10)
        {
            List<Hcrp.Framework.Classes.Paciente> l = new List<Hcrp.Framework.Classes.Paciente>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT DISTINCT A.COD_PACIENTE REGISTRO, " + Environment.NewLine);
                    sb.Append("        A.NOM_PACIENTE, A.SBN_PACIENTE,  " + Environment.NewLine);
                    sb.Append("        DECODE(A.IDF_SEXO,'M','MASCULINO','F','FEMININO','DESCONHECIDO') SEXO, " + Environment.NewLine);
                    sb.Append("        A.DTA_NASCIMENTO, " + Environment.NewLine);
                    sb.Append("        A.NOM_MAE, " + Environment.NewLine);
                    sb.Append("        A.IDF_COR, " + Environment.NewLine);
                    sb.Append("        FCN_DIFERENCA_DATA(SYSDATE,A.DTA_NASCIMENTO,3) IDADE " + Environment.NewLine);
                    sb.Append(" FROM PACIENTE A, ATENDIMENTO_PACIENTE B, MOVIMENTACAO_PACIENTE C " + Environment.NewLine);
                    sb.Append(" WHERE A.COD_PACIENTE = B.COD_PACIENTE " + Environment.NewLine);
                    sb.Append("   AND B.SEQ_ATENDIMENTO = C.SEQ_ATENDIMENTO " + Environment.NewLine);
                    sb.Append("   AND C.COD_ESPECIALIDADE_HC = :COD_ESPECIALIDADE_HC " + Environment.NewLine);
                    sb.Append("   AND C.DTA_HOR_ENTRADA > SYSDATE - " + qtdDiasAnteriores.ToString() + Environment.NewLine);
                    sb.Append(" ORDER BY A.NOM_PACIENTE, A.SBN_PACIENTE, A.DTA_NASCIMENTO " + Environment.NewLine);
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                    query.Params["COD_ESPECIALIDADE_HC"] = especialidade.Codigo;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.Paciente p = new Hcrp.Framework.Classes.Paciente();
                        p.RegistroPaciente = Convert.ToString(dr["REGISTRO"]);
                        p.NomePaciente = Convert.ToString(dr["NOM_PACIENTE"]);
                        p.SobrenomePaciente = Convert.ToString(dr["SBN_PACIENTE"]);
                        p.SexoPaciente = Convert.ToString(dr["SEXO"]);
                        p.DataNascimento = Convert.ToDateTime(dr["DTA_NASCIMENTO"]);
                        p.MaePaciente = Convert.ToString(dr["NOM_MAE"]);
                        p.Idade = Convert.ToString(dr["IDADE"]);
                        p.Cor = (ECorPaciente)Convert.ToInt32(dr["IDF_COR"]);
                        l.Add(p);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Realiza a busca pelo valor padrao de medicamentos para o paciente
        /// </summary>
        /// <param name="codigoPaciente">Codigo do paciente</param>
        /// <param name="dataAtendimento">Data em que o atendimento medico esta sendo efetuado</param>
        /// <param name="mostrarPosologia">Flag que indica se o resultado deve mostrar posologia</param>
        /// <param name="formatarHtml">Flag que indica se o resultado deve estar formatado em HTML</param>
        /// <returns>Texto com valor padrao de medicamentos para o paciente</returns>
        public string ObterChecoMedicamentoPadrao(string codigoPaciente, DateTime dataAtendimento, bool? mostrarPosologia, bool? formatarHtml)
        {
            AlterarSessaoOracle();

            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
            {
                ctx.Open();

                string sql = string.Format("SELECT GENERICO.FCN_CHECO_MEDICAMENTO('{0}', TO_DATE('{1}', 'DD/MM/YYYY'), {2}, {3}) FROM DUAL",
                                                                                                                        codigoPaciente,
                                                                                                                        dataAtendimento.ToString("dd/MM/yyyy"),
                                                                                                                        mostrarPosologia.HasValue ? mostrarPosologia.Value ? "'S'" : "'N'" : "NULL",
                                                                                                                        formatarHtml.HasValue ? formatarHtml.Value ? "'S'" : "'N'" : "NULL");

                Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql);

                ctx.ExecuteQuery(query);

                while (ctx.Reader.Read())
                {
                    if (ctx.Reader[0] != DBNull.Value)
                    {
                        return ctx.Reader[0].ToString();
                    }
                }

            }
            return null;
        }


        /// <summary>
        /// Realiza a busca pelo valor padrao de exames para o paciente
        /// </summary>
        /// <param name="codigoPaciente">Codigo do paciente</param>
        /// <returns>Texto com valor padrao de exame para o paciente</returns>
        public string ObterChecoExamePadrao(string codigoPaciente)
        {
            AlterarSessaoOracle();

            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
            {
                ctx.Open();

                string sql = string.Format("SELECT GENERICO.FCN_CHECO_EXAME('{0}') FROM DUAL", codigoPaciente);

                Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql);

                ctx.ExecuteQuery(query);

                while (ctx.Reader.Read())
                {
                    if (ctx.Reader[0] != DBNull.Value)
                    {
                        return ctx.Reader[0].ToString();
                    }
                }

            }
            return null;
        }

        /// <summary>
        /// evitar erro de regiao. 
        /// </summary>        
        public void AlterarSessaoOracle()
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(" ALTER SESSION SET NLS_NUMERIC_CHARACTERS = '.,' ");
                    ctx.ExecuteNonQuery(query);

                    query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(" ALTER SESSION SET NLS_LANGUAGE = 'BRAZILIAN PORTUGUESE' ");
                    ctx.ExecuteNonQuery(query);

                    query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(" ALTER SESSION SET NLS_DATE_FORMAT = 'DD/MM/YYYY HH24:MI:SS' ");
                    ctx.ExecuteNonQuery(query);

                    query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(" ALTER SESSION SET NLS_TERRITORY = AMERICA ");
                    ctx.ExecuteNonQuery(query);

                    ctx.Close();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }
    }
}
