using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;


namespace Hcrp.Framework.Dal
{
    public class PedidoAtendimento : Hcrp.Framework.Classes.PedidoAtendimento
    {
        public List<Hcrp.Framework.Classes.PedidoAtendimento> BuscarPacientes(List<Hcrp.Framework.Classes.ParametrosOracle> filtros)
        { 
            List<Hcrp.Framework.Classes.PedidoAtendimento> l = new List<Hcrp.Framework.Classes.PedidoAtendimento>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT DISTINCT A.COD_PROTOCOLO_ATENDIMENTO, B.COD_PACIENTE REGISTRO, A.NUM_PRONTUARIO_MUNICIPIO, " + Environment.NewLine); 
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.NOM_PACIENTE, B.NOM_PACIENTE) NOM_PACIENTE, " + Environment.NewLine); 
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.SBN_PACIENTE, B.SBN_PACIENTE) SBN_PACIENTE, " + Environment.NewLine);  
                    sb.Append(" DECODE(DECODE(B.COD_PACIENTE, NULL, A.IDF_SEXO, B.IDF_SEXO),'M','MASCULINO','F','FEMININO','DESCONHECIDO') SEXO, " + Environment.NewLine); 
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.DTA_NASCIMENTO, B.DTA_NASCIMENTO) DTA_NASCIMENTO, " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.NOM_MAE, B.NOM_MAE) NOM_MAE, "  + Environment.NewLine);
                    sb.Append(" DECODE(B.IDF_COR, NULL, A.IDF_COR, B.IDF_COR) IDF_COR, " + Environment.NewLine);                    
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.DSC_IDADE_APARENTE, B.DSC_IDADE_APARENTE) DSC_IDADE_APARENTE, " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.CEP_PACIENTE, B.CEP_PACIENTE) CEP_PACIENTE, " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.NUM_ENDERECO, B.NUM_ENDERECO) NUM_ENDERECO, " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.COD_TIPO_LOGRADOURO, '') COD_TIPO_LOGRADOURO, " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.DSC_TIPO_LOGRADOURO, B.DSC_TIPO_LOGRADOURO) DSC_TIPO_LOGRADOURO, " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.CPL_ENDERECO, B.CPL_ENDERECO) CPL_ENDERECO, " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.NOM_BAIRRO, B.NOM_BAIRRO) NOM_BAIRRO, " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.DSC_ENDERECO, B.DSC_ENDERECO) DSC_ENDERECO, " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.IDF_TIPO_CONTATO, '') IDF_TIPO_CONTATO, " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.NUM_CNS, B.NUM_CNS) NUM_CNS, " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.NUM_CELULAR, '') NUM_CELULAR, " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.NUM_TELEFONE, B.NUM_TELEFONE) NUM_TELEFONE, " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.NUM_TELEFONE_CONTATO, '') NUM_TELEFONE_CONTATO, " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.DSC_EMAIL, B.DSC_EMAIL) DSC_EMAIL, " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.NUM_PRONTUARIO_MUNICIPIO, '') NUM_PRONTUARIO_MUNICIPIO, " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.NUM_CPF, B.CPF_PACIENTE) NUM_CPF, " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.NUM_RG, B.NUM_RG) NUM_RG, " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.COD_ORGAO_EMISSOR, B.COD_ORGAO_EMISSOR) COD_ORGAO_EMISSOR, " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.IDF_UF_RG, B.IDF_UF_RG) IDF_UF_RG, " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.COD_GRAU_INSTRUCAO, B.COD_GRAU_INSTRUCAO) COD_GRAU_INSTRUCAO, " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.SGL_PAIS || '|' || A.SGL_UF || '|' || A.COD_LOCALIDADE, B.SGL_PAIS || '|' || B.SGL_UF || '|' || B.COD_LOCALIDADE) CHAVE_LOCALIDADE, " + Environment.NewLine);
                    sb.Append(" A.COD_INSTITUICAO, A.COD_PROFISSIONAL_SOLICITANTE, " + Environment.NewLine);
                    sb.Append(" A.DSC_QUADRO_CLINICO, A.DSC_DIAGNOSTICO, A.COD_CID_10_1, " + Environment.NewLine);
                    sb.Append(" A.COD_CID_10_2, A.COD_MOTIVO_ENCAMINHAMENTO, " + Environment.NewLine);
                    sb.Append(" A.DSC_EXAME_REALIZADO, A.DSC_CONDUTA_TERAPEUTICA, " + Environment.NewLine);
                    sb.Append(" A.COD_SERVICO_SADT, A.DSC_OBSERVACAO, " + Environment.NewLine);
                    sb.Append(" MAX(A.SEQ_PEDIDO_ATENDIMENTO) SEQ_PEDIDO_ATENDIMENTO " + Environment.NewLine);                    
                    
                    sb.Append(" FROM PEDIDO_ATENDIMENTO A, PACIENTE B " + Environment.NewLine);
                    sb.Append(" WHERE A.COD_PACIENTE = B.COD_PACIENTE(+)  " + Environment.NewLine);

                    foreach (var item in filtros)
                    {
                        if (item.TipoAssociacao != Hcrp.Framework.Classes.ParametrosOracle.eTipoAssociacao.IsNull)
                        {
                            if (item.TipoAssociacao == Hcrp.Framework.Classes.ParametrosOracle.eTipoAssociacao.Igual)
                                sb.Append(" AND A." + item.CampoOracle + " = :" + item.CampoOracle + Environment.NewLine);
                            else if (item.TipoAssociacao == Hcrp.Framework.Classes.ParametrosOracle.eTipoAssociacao.Like)
                                sb.Append(" AND A." + item.CampoOracle + " LIKE :" + item.CampoOracle + Environment.NewLine);
                        }
                    }
                    sb.Append(" GROUP BY A.COD_PROTOCOLO_ATENDIMENTO, B.COD_PACIENTE, A.NUM_PRONTUARIO_MUNICIPIO, DECODE(B.COD_PACIENTE, NULL, A.NOM_PACIENTE, B.NOM_PACIENTE), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.SBN_PACIENTE, B.SBN_PACIENTE), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.IDF_SEXO, B.IDF_SEXO),  " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.DTA_NASCIMENTO, B.DTA_NASCIMENTO),   " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.NOM_MAE, B.NOM_MAE),  " + Environment.NewLine);
                    sb.Append(" DECODE(B.IDF_COR, NULL, A.IDF_COR, B.IDF_COR), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.DSC_IDADE_APARENTE, B.DSC_IDADE_APARENTE), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.CEP_PACIENTE, B.CEP_PACIENTE), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.NUM_ENDERECO, B.NUM_ENDERECO), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.COD_TIPO_LOGRADOURO, ''), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.DSC_TIPO_LOGRADOURO, B.DSC_TIPO_LOGRADOURO), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.CPL_ENDERECO, B.CPL_ENDERECO), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.NOM_BAIRRO, B.NOM_BAIRRO), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.DSC_ENDERECO, B.DSC_ENDERECO), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.IDF_TIPO_CONTATO, ''), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.NUM_CNS, B.NUM_CNS), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.NUM_CELULAR, ''), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.NUM_TELEFONE, B.NUM_TELEFONE), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.NUM_TELEFONE_CONTATO, ''), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.DSC_EMAIL, B.DSC_EMAIL), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.NUM_PRONTUARIO_MUNICIPIO, ''), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.NUM_CPF, B.CPF_PACIENTE), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.NUM_RG, B.NUM_RG), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.COD_ORGAO_EMISSOR, B.COD_ORGAO_EMISSOR), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.IDF_UF_RG, B.IDF_UF_RG), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.COD_GRAU_INSTRUCAO, B.COD_GRAU_INSTRUCAO), " + Environment.NewLine);
                    sb.Append(" DECODE(B.COD_PACIENTE, NULL, A.SGL_PAIS || '|' || A.SGL_UF || '|' || A.COD_LOCALIDADE, B.SGL_PAIS || '|' || B.SGL_UF || '|' || B.COD_LOCALIDADE), " + Environment.NewLine);
                    sb.Append(" A.COD_INSTITUICAO, A.COD_PROFISSIONAL_SOLICITANTE, " + Environment.NewLine);
                    sb.Append(" A.DSC_QUADRO_CLINICO, A.DSC_DIAGNOSTICO, A.COD_CID_10_1, " + Environment.NewLine);
                    sb.Append(" A.COD_CID_10_2, A.COD_MOTIVO_ENCAMINHAMENTO, " + Environment.NewLine);
                    sb.Append(" A.DSC_EXAME_REALIZADO, A.DSC_CONDUTA_TERAPEUTICA, " + Environment.NewLine);
                    sb.Append(" A.COD_SERVICO_SADT, A.DSC_OBSERVACAO " + Environment.NewLine);

                    sb.Append(" ORDER BY 4,5,7 " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    foreach (var item in filtros)
                    {
                        if (item.TipoAssociacao != Hcrp.Framework.Classes.ParametrosOracle.eTipoAssociacao.IsNull)
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
                        Hcrp.Framework.Classes.PedidoAtendimento p = new Hcrp.Framework.Classes.PedidoAtendimento();
                        p.Seq = Convert.ToInt32(dr["SEQ_PEDIDO_ATENDIMENTO"]);
                        p.RegistroPaciente = Convert.ToString(dr["REGISTRO"]);
                        p.NomePaciente = Convert.ToString(dr["NOM_PACIENTE"]);
                        p.SobrenomePaciente = Convert.ToString(dr["SBN_PACIENTE"]);
                        p.SexoPaciente = Convert.ToString(dr["SEXO"]);
                        p.DataNascimento = Convert.ToDateTime(dr["DTA_NASCIMENTO"]);
                        p.MaePaciente = Convert.ToString(dr["NOM_MAE"]);
                        p.NumProntuarioMunicipio = Convert.ToString(dr["NUM_PRONTUARIO_MUNICIPIO"]);
                        p.CorPaciente = (ECorPaciente)Convert.ToInt32(dr["IDF_COR"]);
                        p.IdadeAparentePaciente =  Convert.ToString(dr["DSC_IDADE_APARENTE"]);
                        p.CepPaciente = Convert.ToString(dr["CEP_PACIENTE"]);
                        p.NumeroEnderecoPaciente = Convert.ToString(dr["NUM_ENDERECO"]);
                        if ((dr["COD_TIPO_LOGRADOURO"] != DBNull.Value) && (!string.IsNullOrWhiteSpace(Convert.ToString(dr["COD_TIPO_LOGRADOURO"]))))
                            p._codTipoLogradouro = Convert.ToString(dr["COD_TIPO_LOGRADOURO"]);
                        p.DescricaoLogradouroPaciente = Convert.ToString(dr["DSC_TIPO_LOGRADOURO"]);
                        p.ComplementoEnderecoPaciente = Convert.ToString(dr["CPL_ENDERECO"]);
                        p.BairroPaciente = Convert.ToString(dr["NOM_BAIRRO"]);
                        p.EnderecoPaciente = Convert.ToString(dr["DSC_ENDERECO"]);
                        
                        if (Convert.ToString(dr["IDF_TIPO_CONTATO"]) == "P")
                            p.TipoContato = ETipoContato.Pessoal;
                        else if (Convert.ToString(dr["IDF_TIPO_CONTATO"]) == "C")
                            p.TipoContato = ETipoContato.Contato;
                        else if (Convert.ToString(dr["IDF_TIPO_CONTATO"]) == "U")
                            p.TipoContato = ETipoContato.UnidadeSaude;
                        
                        p.CnsPaciente = Convert.ToString(dr["NUM_CNS"]);
                        p.CelularPaciente = Convert.ToString(dr["NUM_CELULAR"]);
                        p.FonePaciente = Convert.ToString(dr["NUM_TELEFONE"]);
                        p.FoneContato = Convert.ToString(dr["NUM_TELEFONE_CONTATO"]);
                        p.EmailPaciente = Convert.ToString(dr["DSC_EMAIL"]);
                        p.NumProntuarioMunicipio = Convert.ToString(dr["NUM_PRONTUARIO_MUNICIPIO"]);
                        p.CpfPaciente = Convert.ToString(dr["NUM_CPF"]);
                        p.RgPaciente = Convert.ToString(dr["NUM_RG"]);
                        if (dr["COD_ORGAO_EMISSOR"] != DBNull.Value)
                            p.CodOrgaoEmissor = Convert.ToInt32(dr["COD_ORGAO_EMISSOR"]);
                        p._idfUfRg = Convert.ToString(dr["COD_GRAU_INSTRUCAO"]);
                        if (dr["COD_ORGAO_EMISSOR"] != DBNull.Value)
                            p._codGrauInstrucao = Convert.ToInt32(dr["COD_GRAU_INSTRUCAO"]);
                        p._chaveMunicipio = Convert.ToString(dr["CHAVE_LOCALIDADE"]) ;
                        p._codPostoMedico = Convert.ToInt32(dr["COD_INSTITUICAO"]);
                        p._codMedicoSolicitante = Convert.ToInt32(dr["COD_PROFISSIONAL_SOLICITANTE"]);

                        if (dr["COD_PROTOCOLO_ATENDIMENTO"] != DBNull.Value)
                            p._CodProtocoloAtendimento = Convert.ToInt32(dr["COD_PROTOCOLO_ATENDIMENTO"]);

                        if (dr["DSC_QUADRO_CLINICO"] != DBNull.Value)
                            p.QuadroClinico = dr["DSC_QUADRO_CLINICO"].ToString();

                        if (dr["DSC_DIAGNOSTICO"] != DBNull.Value)
                            p.Diagnostico = dr["DSC_DIAGNOSTICO"].ToString();

                        if (dr["COD_CID_10_1"] != DBNull.Value)
                        {
                            p.Cid1 = new Classes.Cid();
                            p.Cid1.Codigo = dr["COD_CID_10_1"].ToString();
                        }

                        if (dr["COD_CID_10_2"] != DBNull.Value)
                        {
                            p.Cid2 = new Classes.Cid();
                            p.Cid2.Codigo = dr["COD_CID_10_2"].ToString();
                        }

                        if (dr["COD_MOTIVO_ENCAMINHAMENTO"] != DBNull.Value)
                            p._codMotivoEncaminhamento = Convert.ToInt32(dr["COD_MOTIVO_ENCAMINHAMENTO"]);

                        if (dr["DSC_EXAME_REALIZADO"] != DBNull.Value)
                            p.ExamesRealizados = dr["DSC_EXAME_REALIZADO"].ToString();

                        if (dr["DSC_CONDUTA_TERAPEUTICA"] != DBNull.Value)
                            p.CondutaTerapeutica = dr["DSC_CONDUTA_TERAPEUTICA"].ToString();

                        if (dr["COD_SERVICO_SADT"] != DBNull.Value)
                            p._codServicoSadt = Convert.ToInt32(dr["COD_SERVICO_SADT"]);

                        if (dr["DSC_OBSERVACAO"] != DBNull.Value)
                            p.Observacao = dr["DSC_OBSERVACAO"].ToString();
                        
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
        public List<Hcrp.Framework.Classes.PedidoAtendimento> BuscarPedidos(Hcrp.Framework.Classes.PedidoAtendimento pa)
        {
            List<Hcrp.Framework.Classes.PedidoAtendimento> l = new List<Hcrp.Framework.Classes.PedidoAtendimento>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.SEQ_PEDIDO_ATENDIMENTO, A.DTA_HOR_EMISSAO, " + Environment.NewLine);
                    sb.Append(" A.COD_SERVICO_SADT, A.COD_PROTOCOLO_ATENDIMENTO " + Environment.NewLine);
                    sb.Append(" FROM PEDIDO_ATENDIMENTO A " + Environment.NewLine);
                    if (string.IsNullOrWhiteSpace(pa.RegistroPaciente))
                    {
                        sb.Append(" WHERE A.NOM_PACIENTE = :NOM_PACIENTE " + Environment.NewLine);
                        sb.Append("   AND A.SBN_PACIENTE = :SBN_PACIENTE " + Environment.NewLine);
                        sb.Append("   AND A.DTA_NASCIMENTO = :DTA_NASCIMENTO " + Environment.NewLine);
                        sb.Append("   AND A.NOM_MAE = :NOM_MAE " + Environment.NewLine);
                    }
                    else
                    {
                        sb.Append(" WHERE A.COD_PACIENTE = :COD_PACIENTE " + Environment.NewLine);
                    }                    
                    sb.Append(" ORDER BY A.DTA_HOR_EMISSAO DESC " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    if (string.IsNullOrWhiteSpace(pa.RegistroPaciente))
                    {
                        query.Params["NOM_PACIENTE"] = pa.NomePaciente;
                        query.Params["SBN_PACIENTE"] = pa.SobrenomePaciente;
                        query.Params["DTA_NASCIMENTO"] = pa.DataNascimento;
                        query.Params["NOM_MAE"] = pa.MaePaciente;
                    }
                    else
                    {
                        query.Params["COD_PACIENTE"] = pa.RegistroPaciente;
                    }                    

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.PedidoAtendimento p = new Hcrp.Framework.Classes.PedidoAtendimento();
                        if (dr["DTA_HOR_EMISSAO"]!=DBNull.Value)
                            p.DataEmissao = Convert.ToDateTime(dr["DTA_HOR_EMISSAO"]);
                        if (dr["SEQ_PEDIDO_ATENDIMENTO"] != DBNull.Value)
                            p.Seq = Convert.ToInt32(dr["SEQ_PEDIDO_ATENDIMENTO"]);
                        if (dr["COD_SERVICO_SADT"] != DBNull.Value)
                            p._codServicoSadt = Convert.ToInt32(dr["COD_SERVICO_SADT"]);
                        if (dr["COD_PROTOCOLO_ATENDIMENTO"] != DBNull.Value)
                            p._CodProtocoloAtendimento = Convert.ToInt32(dr["COD_PROTOCOLO_ATENDIMENTO"]);
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
        public long Gravar(Framework.Classes.PedidoAtendimento pa)
        {
            try
            {
                long retorno = 0;
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    //Atualizar o CNES
                     new Hcrp.Framework.Classes.Instituicao().AtualizarCNES(pa.PostoMedico, pa._CnesPostoMedico);

                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PEDIDO_ATENDIMENTO");

                    string tipoPedido = "C";

                    if (pa.TipoPedido == ETipoPedido.Consulta)
                        tipoPedido = "C";
                    else if (pa.TipoPedido == ETipoPedido.ExamesComAgenda)
                        tipoPedido = "E";
                    else if (pa.TipoPedido == ETipoPedido.ExamesLaboratoriais)
                        tipoPedido = "L";

                    comando.Params["IDF_TIPO_PEDIDO"] = tipoPedido;

                    //if (pa.TipoProtocoloAtendimento != null)
                    //    comando.Params["COD_PROTOCOLO_ATENDIMENTO"] = pa.TipoProtocoloAtendimento.Seq;

                    if (pa.ServicoSadt != null && pa.ServicoSadt.Codigo > 0)
                        comando.Params["COD_SERVICO_SADT"] = pa.ServicoSadt.Codigo;

                    comando.Params["NUM_USER_SOLICITANTE"] = new Hcrp.Framework.Classes.UsuarioConexao().NumUserBanco;

                    if (pa.Logradouro != null)
                        comando.Params["COD_TIPO_LOGRADOURO"] = Convert.ToString(pa.Logradouro.Codigo).PadLeft(3, '0');

                    if (pa._codPostoMedico > 0)
                        comando.Params["COD_INSTITUICAO"] = pa._codPostoMedico;

                    if (pa.Municipio != null && pa.Municipio.Pais != null)
                        comando.Params["SGL_PAIS"] = pa.Municipio.Pais.Sigla.ToUpper();

                    if (pa.Municipio != null && pa.Municipio.UF != null)
                        comando.Params["SGL_UF"] = pa.Municipio.UF.Sigla.ToUpper();

                    if (pa.Municipio != null)
                        comando.Params["COD_LOCALIDADE"] = pa.Municipio.Codigo;

                    if (!string.IsNullOrWhiteSpace(pa.RegistroPaciente))
                        comando.Params["COD_PACIENTE"] = pa.RegistroPaciente.ToUpper();

                    comando.Params["NOM_PACIENTE"] = pa.NomePaciente.ToUpper();
                    comando.Params["SBN_PACIENTE"] = pa.SobrenomePaciente.ToUpper();
                    comando.Params["DTA_NASCIMENTO"] = pa.DataNascimento;

                    if (!string.IsNullOrWhiteSpace(pa.IdadeAparentePaciente))
                        comando.Params["DSC_IDADE_APARENTE"] = pa.IdadeAparentePaciente.Trim();

                    if (!string.IsNullOrWhiteSpace(pa.SexoPaciente))
                        comando.Params["IDF_SEXO"] = pa.SexoPaciente.ToUpper();

                    comando.Params["IDF_COR"] = (int)pa.CorPaciente;

                    if (!string.IsNullOrWhiteSpace(pa.MaePaciente))
                        comando.Params["NOM_MAE"] = pa.MaePaciente.ToUpper();

                    comando.Params["IDF_PRIORIDADE"] = (int)pa.PrioridadePedido;

                    if (pa.DataEmissao != DateTime.MinValue)
                        comando.Params["DTA_HOR_EMISSAO"] = pa.DataEmissao;
                    else
                        comando.Params["DTA_HOR_EMISSAO"] = System.DateTime.Now;

                    if (pa.MedicoSolicitante != null)
                        comando.Params["COD_PROFISSIONAL_SOLICITANTE"] = pa.MedicoSolicitante.Codigo;

                    if (pa.Cid1 != null)
                        comando.Params["COD_CID_10_1"] = pa.Cid1.Codigo;

                    if (pa.Cid2 != null)
                        comando.Params["COD_CID_10_2"] = pa.Cid2.Codigo;

                    if (!string.IsNullOrWhiteSpace(pa.CepPaciente))
                        comando.Params["CEP_PACIENTE"] = pa.CepPaciente.Replace("-", "");

                    if (!string.IsNullOrWhiteSpace(pa.NumeroEnderecoPaciente))
                        comando.Params["NUM_ENDERECO"] = pa.NumeroEnderecoPaciente.Trim().ToUpper();

                    if (pa.Logradouro != null && pa.Logradouro.Sigla != null)
                        comando.Params["DSC_TIPO_LOGRADOURO"] = pa.Logradouro.Sigla;

                    if (!string.IsNullOrWhiteSpace(pa.ComplementoEnderecoPaciente))
                        comando.Params["CPL_ENDERECO"] = pa.ComplementoEnderecoPaciente.Trim().ToUpper();

                    if (!string.IsNullOrWhiteSpace(pa.BairroPaciente))
                        comando.Params["NOM_BAIRRO"] = pa.BairroPaciente.Trim().ToUpper();

                    if (!string.IsNullOrWhiteSpace(pa.EnderecoPaciente))
                        comando.Params["DSC_ENDERECO"] = pa.EnderecoPaciente.Trim().ToUpper();

                    if (!string.IsNullOrEmpty(pa.Diagnostico))
                        comando.Params["DSC_DIAGNOSTICO"] = pa.Diagnostico.ToUpper();

                    if (!string.IsNullOrEmpty(pa.CondutaTerapeutica))
                        comando.Params["DSC_CONDUTA_TERAPEUTICA"] = pa.CondutaTerapeutica.ToUpper();

                    if (!string.IsNullOrEmpty(pa.QuadroClinico))
                        comando.Params["DSC_QUADRO_CLINICO"] = pa.QuadroClinico.ToUpper();

                    if (!string.IsNullOrEmpty(pa.ExamesRealizados))
                        comando.Params["DSC_EXAME_REALIZADO"] = pa.ExamesRealizados.ToUpper();

                    if (!string.IsNullOrEmpty(pa.JustificativaDevolucao))
                        comando.Params["JUS_DEVOLUCAO"] = pa.JustificativaDevolucao.ToUpper();

                    if (pa.TipoContato == ETipoContato.Contato)
                        comando.Params["IDF_TIPO_CONTATO"] = "C";
                    else if (pa.TipoContato == ETipoContato.Pessoal)
                        comando.Params["IDF_TIPO_CONTATO"] = "P";
                    else if (pa.TipoContato == ETipoContato.UnidadeSaude)
                        comando.Params["IDF_TIPO_CONTATO"] = "U";

                    if (!string.IsNullOrWhiteSpace(pa.CnsPaciente))
                        comando.Params["NUM_CNS"] = pa.CnsPaciente.Trim();

                    if (!string.IsNullOrWhiteSpace(pa.CelularPaciente))
                        comando.Params["NUM_CELULAR"] = pa.CelularPaciente.Trim();

                    if (!string.IsNullOrWhiteSpace(pa.FonePaciente))
                        comando.Params["NUM_TELEFONE"] = pa.FonePaciente.Trim();

                    if (!string.IsNullOrWhiteSpace(pa.FoneContato))
                        comando.Params["NUM_TELEFONE_CONTATO"] = pa.FoneContato.Trim();

                    if (!string.IsNullOrWhiteSpace(pa.EmailPaciente))
                        comando.Params["DSC_EMAIL"] = pa.EmailPaciente.ToLower();

                    if (!string.IsNullOrWhiteSpace(pa.NumProntuarioMunicipio))
                        comando.Params["NUM_PRONTUARIO_MUNICIPIO"] = pa.NumProntuarioMunicipio.ToUpper();

                    comando.Params["IDF_PRIORIDADE_EMISSAO"] = Convert.ToString((int)pa.PrioridadeEmissao);
                    comando.Params["IDF_VISUALIZOU_GUIA_EXT"] = "N";

                    if (!string.IsNullOrWhiteSpace(pa.CpfPaciente))
                        comando.Params["NUM_CPF"] = pa.CpfPaciente;

                    if (!string.IsNullOrWhiteSpace(pa.RgPaciente))
                        comando.Params["NUM_RG"] = pa.RgPaciente.ToUpper();

                    if (pa.CodOrgaoEmissor > 0)
                        comando.Params["COD_ORGAO_EMISSOR"] = pa.CodOrgaoEmissor;

                    if (!string.IsNullOrWhiteSpace(pa.UfRg))
                        comando.Params["IDF_UF_RG"] = pa.UfRg;

                    if (pa.GrauInstrucaoPaciente != null)
                        comando.Params["COD_GRAU_INSTRUCAO"] = pa.GrauInstrucaoPaciente.Codigo;

                    if (!string.IsNullOrWhiteSpace(pa.Observacao))
                        comando.Params["DSC_OBSERVACAO"] = pa.Observacao;

                    if (pa._codMotivoEncaminhamento > 0)
                        comando.Params["COD_MOTIVO_ENCAMINHAMENTO"] = pa._codMotivoEncaminhamento;

                    if (pa._CodProtocoloAtendimento > 0)
                        comando.Params["COD_PROTOCOLO_ATENDIMENTO"] = pa._CodProtocoloAtendimento;

                    if (pa._codServicoSadt > 0)
                        comando.Params["COD_SERVICO_SADT"] = pa._codServicoSadt;

                    // Executar o insert
                    ctx.ExecuteInsert(comando);

                    // Pegar o último ID
                    retorno = ctx.GetSequenceValue("GENERICO.SEQ_PEDIDO_ATENDIMENTO", false);

                    if (pa.ItemsPedido.Count > 0)
                    {
                        foreach (var itemPedido in pa.ItemsPedido)
                        {
                            itemPedido.Gravar(retorno);       
                        }
                    }
                }
                return retorno;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public Hcrp.Framework.Classes.PedidoAtendimento BuscarPedidoItem(int seq_item)
        {
            Hcrp.Framework.Classes.PedidoAtendimento p = new Hcrp.Framework.Classes.PedidoAtendimento();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.SEQ_PEDIDO_ATENDIMENTO, A.COD_PACIENTE REGISTRO, A.NUM_PRONTUARIO_MUNICIPIO, " + Environment.NewLine);
                    sb.Append(" A.NOM_PACIENTE, " + Environment.NewLine);
                    sb.Append(" A.SBN_PACIENTE, " + Environment.NewLine);
                    sb.Append(" DECODE(A.IDF_SEXO, 'M','MASCULINO','F','FEMININO','DESCONHECIDO') SEXO, " + Environment.NewLine);
                    sb.Append(" A.DTA_NASCIMENTO, " + Environment.NewLine);
                    sb.Append(" A.NOM_MAE, " + Environment.NewLine);
                    sb.Append(" A.IDF_COR, " + Environment.NewLine);
                    sb.Append(" A.DSC_IDADE_APARENTE, " + Environment.NewLine);
                    sb.Append(" A.CEP_PACIENTE, " + Environment.NewLine);
                    sb.Append(" A.NUM_ENDERECO, " + Environment.NewLine);
                    sb.Append(" A.COD_TIPO_LOGRADOURO, " + Environment.NewLine);
                    sb.Append(" A.DSC_TIPO_LOGRADOURO, " + Environment.NewLine);
                    sb.Append(" A.CPL_ENDERECO, " + Environment.NewLine);
                    sb.Append(" A.NOM_BAIRRO, " + Environment.NewLine);
                    sb.Append(" A.DSC_ENDERECO, " + Environment.NewLine);
                    sb.Append(" A.IDF_TIPO_CONTATO, " + Environment.NewLine);
                    sb.Append(" A.NUM_CNS, " + Environment.NewLine);
                    sb.Append(" A.NUM_CELULAR, " + Environment.NewLine);
                    sb.Append(" A.NUM_TELEFONE, " + Environment.NewLine);
                    sb.Append(" A.NUM_TELEFONE_CONTATO, " + Environment.NewLine);
                    sb.Append(" A.DSC_EMAIL, " + Environment.NewLine);
                    sb.Append(" A.NUM_PRONTUARIO_MUNICIPIO, " + Environment.NewLine);
                    sb.Append(" A.NUM_CPF, " + Environment.NewLine);
                    sb.Append(" A.NUM_RG, " + Environment.NewLine);
                    sb.Append(" A.COD_ORGAO_EMISSOR, " + Environment.NewLine);
                    sb.Append(" A.IDF_UF_RG, " + Environment.NewLine);
                    sb.Append(" A.COD_GRAU_INSTRUCAO, " + Environment.NewLine);
                    sb.Append(" A.SGL_PAIS || '|' || A.SGL_UF || '|' || A.COD_LOCALIDADE CHAVE_LOCALIDADE, " + Environment.NewLine);
                    sb.Append(" A.COD_INSTITUICAO, A.COD_PROFISSIONAL_SOLICITANTE, A.COD_PROTOCOLO_ATENDIMENTO, A.DTA_HOR_EMISSAO " + Environment.NewLine);
                    sb.Append(" FROM PEDIDO_ATENDIMENTO A, ITEM_PEDIDO_ATENDIMENTO B " + Environment.NewLine);
                    sb.Append(" WHERE A.SEQ_PEDIDO_ATENDIMENTO = B.SEQ_PEDIDO_ATENDIMENTO " + Environment.NewLine);
                    sb.Append("   AND B.SEQ_ITEM_PEDIDO_ATENDIMENTO = :SEQ_ITEM_PEDIDO_ATENDIMENTO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                    query.Params["SEQ_ITEM_PEDIDO_ATENDIMENTO"] = seq_item;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        if (dr["SEQ_PEDIDO_ATENDIMENTO"] != DBNull.Value)
                            p.Seq = Convert.ToInt32(dr["SEQ_PEDIDO_ATENDIMENTO"]);

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

                        if (dr["NUM_PRONTUARIO_MUNICIPIO"] != DBNull.Value)
                            p.NumProntuarioMunicipio = Convert.ToString(dr["NUM_PRONTUARIO_MUNICIPIO"]);

                        if (dr["IDF_COR"] != DBNull.Value)
                            p.CorPaciente = (ECorPaciente)Convert.ToInt32(dr["IDF_COR"]);

                        if (dr["DSC_IDADE_APARENTE"] != DBNull.Value)
                            p.IdadeAparentePaciente = Convert.ToString(dr["DSC_IDADE_APARENTE"]);

                        if (dr["CEP_PACIENTE"] != DBNull.Value)
                            p.CepPaciente = Convert.ToString(dr["CEP_PACIENTE"]);

                        if (dr["NUM_ENDERECO"] != DBNull.Value)
                            p.NumeroEnderecoPaciente = Convert.ToString(dr["NUM_ENDERECO"]);

                        if ((dr["COD_TIPO_LOGRADOURO"] != DBNull.Value) && (!string.IsNullOrWhiteSpace(Convert.ToString(dr["COD_TIPO_LOGRADOURO"]))))
                            p._codTipoLogradouro = Convert.ToString(dr["COD_TIPO_LOGRADOURO"]);

                        if (dr["DSC_TIPO_LOGRADOURO"] != DBNull.Value)
                            p.DescricaoLogradouroPaciente = Convert.ToString(dr["DSC_TIPO_LOGRADOURO"]);

                        if (dr["CPL_ENDERECO"] != DBNull.Value)
                            p.ComplementoEnderecoPaciente = Convert.ToString(dr["CPL_ENDERECO"]);

                        if (dr["NOM_BAIRRO"] != DBNull.Value)
                            p.BairroPaciente = Convert.ToString(dr["NOM_BAIRRO"]);

                        if (dr["DSC_ENDERECO"] != DBNull.Value)
                            p.EnderecoPaciente = Convert.ToString(dr["DSC_ENDERECO"]);

                        if (dr["IDF_TIPO_CONTATO"] != DBNull.Value)
                        {
                            if (Convert.ToString(dr["IDF_TIPO_CONTATO"]) == "P")
                                p.TipoContato = ETipoContato.Pessoal;
                            else if (Convert.ToString(dr["IDF_TIPO_CONTATO"]) == "C")
                                p.TipoContato = ETipoContato.Contato;
                            else if (Convert.ToString(dr["IDF_TIPO_CONTATO"]) == "U")
                                p.TipoContato = ETipoContato.UnidadeSaude;
                        }

                        if (dr["NUM_CNS"] != DBNull.Value)
                            p.CnsPaciente = Convert.ToString(dr["NUM_CNS"]);

                        if (dr["NUM_CELULAR"] != DBNull.Value)
                            p.CelularPaciente = Convert.ToString(dr["NUM_CELULAR"]);

                        if (dr["NUM_TELEFONE"] != DBNull.Value)
                            p.FonePaciente = Convert.ToString(dr["NUM_TELEFONE"]);

                        if (dr["NUM_TELEFONE_CONTATO"] != DBNull.Value)
                            p.FoneContato = Convert.ToString(dr["NUM_TELEFONE_CONTATO"]);

                        if (dr["DSC_EMAIL"] != DBNull.Value)
                            p.EmailPaciente = Convert.ToString(dr["DSC_EMAIL"]);

                        if (dr["NUM_PRONTUARIO_MUNICIPIO"] != DBNull.Value)
                            p.NumProntuarioMunicipio = Convert.ToString(dr["NUM_PRONTUARIO_MUNICIPIO"]);

                        if (dr["NUM_CPF"] != DBNull.Value)
                            p.CpfPaciente = Convert.ToString(dr["NUM_CPF"]);

                        if (dr["NUM_RG"] != DBNull.Value)
                            p.RgPaciente = Convert.ToString(dr["NUM_RG"]);

                        if (dr["COD_ORGAO_EMISSOR"] != DBNull.Value)
                            p.CodOrgaoEmissor = Convert.ToInt32(dr["COD_ORGAO_EMISSOR"]);

                        if (dr["COD_GRAU_INSTRUCAO"] != DBNull.Value)
                            p._codGrauInstrucao = Convert.ToInt32(dr["COD_GRAU_INSTRUCAO"]);

                        if (dr["CHAVE_LOCALIDADE"] != DBNull.Value)
                            p.Municipio = Municipio.BuscaMunicipiosChave(Convert.ToString(dr["CHAVE_LOCALIDADE"]));

                        if (dr["CHAVE_LOCALIDADE"] != DBNull.Value)
                            p._chaveMunicipio = Convert.ToString(dr["CHAVE_LOCALIDADE"]);

                        if (dr["COD_INSTITUICAO"] != DBNull.Value)
                            p._codPostoMedico = Convert.ToInt32(dr["COD_INSTITUICAO"]);

                        if (dr["COD_PROFISSIONAL_SOLICITANTE"] != DBNull.Value)
                            p._codMedicoSolicitante = Convert.ToInt32(dr["COD_PROFISSIONAL_SOLICITANTE"]);

                        if (dr["COD_PROTOCOLO_ATENDIMENTO"] != DBNull.Value)
                            p._CodProtocoloAtendimento = Convert.ToInt32(dr["COD_PROTOCOLO_ATENDIMENTO"]);

                        if (dr["IDF_UF_RG"] != DBNull.Value)
                            p._idfUfRg = dr["IDF_UF_RG"].ToString();

                        if (dr["DTA_HOR_EMISSAO"] != DBNull.Value)
                            p.DataEmissao = Convert.ToDateTime(dr["DTA_HOR_EMISSAO"]);

                    }
                }
                return p;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
