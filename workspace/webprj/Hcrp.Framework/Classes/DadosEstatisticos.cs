using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework
{
    public class AtividadeEstatistica
    {
        private int seq_atividade_estatistica;
        private String dsc_atividade;
        private Boolean idf_Ativo;
        
        public int Codigo
        {            
            get
            {                
                return seq_atividade_estatistica;
            }
            
        }

        public string Nome
        {
            get
            {                
                return dsc_atividade;
            }
            set
            {
                Nome = value;
            }
        }
        
        public bool Ativo
        {
            get
            {                
                return idf_Ativo;
            }

            set
            {
                idf_Ativo = value;
            }
        }

        public List<AtividadeEstatistica> BuscarTodos()
        {
            throw new System.NotImplementedException();
        }

        public void Excluir(int CodigoAtividade)
        {
            throw new System.NotImplementedException();
        }

        public void Gravar()
        {
            throw new System.NotImplementedException();
        }

        public AtividadeEstatistica BuscarPorCodigo(int Codigo)
        {
            throw new System.NotImplementedException();
        }
    }

    public class FavorecidoEstatistico
    {
        private int seq_favorecido_estatistica;
        private String nom_favorecido;
        private Boolean idf_Ativo;

        public int Codigo
        {
            get
            {                
                return seq_favorecido_estatistica;
            }
            set
            {
                seq_favorecido_estatistica = value;
            }
        }

        public string Nome
        {
            get
            {                
                return nom_favorecido;
            }
            set
            {
                nom_favorecido = value;
            }
        }

        public bool Ativo
        {
            get
            {                
                return idf_Ativo;
            }
            set
            {
                idf_Ativo = value;
            }
        }

        public void Gravar()
        {
            throw new System.NotImplementedException();
        }

        public List<FavorecidoEstatistico> BuscarTodos()
        {
            throw new System.NotImplementedException();
        }

        public void Excluir()
        {
            throw new System.NotImplementedException();
        }

        public FavorecidoEstatistico BuscarPorCodigo(int codigo)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MetricaEstatistica
    {
        private int seq_metrica;
        private String nom_metrica;
        private bool idf_Ativo;
        private eTipoDado idf_formato;

        public int Codigo
        {
            get
            {                
                return seq_metrica;
            }
            set
            {
                seq_metrica = value;
            }
        }

        public String Nome
        {
            get
            {                
                return nom_metrica;
            }
            set
            {
                nom_metrica = value;
            }
        }

        public bool Ativo
        {
            get
            {                
                return idf_Ativo;
            }
            set
            {
                idf_Ativo = value;   
            }
        }

        public eTipoDado TipoDado
        {
            get
            {                
                return idf_formato;
            }
            set
            {
                idf_formato = value;
            }
        }

        public List<MetricaEstatistica> BuscarTodos()
        {
            throw new System.NotImplementedException();
        }
    }

    public enum eTipoDado
    {
        NUMERO = 1,
        HORA = 2,
    }

    public class AssociacaoAtividadeLocalEstatistica
    {
        private Hcrp.Framework.Classes.MapeamentoLocal num_seq_local;
        private AtividadeEstatistica seq_atividade_estatistica;
        private List<MetricaEstatistica> MetricasEstatisticas;
        private String dsc_grupo;
        private int num_ordem;
        private Hcrp.Framework.Classes.Usuario num_user_banco;

        public AtividadeEstatistica Atividade
        {
            get
            {                
                return seq_atividade_estatistica;
            }
            set
            {
                seq_atividade_estatistica = value;
            }
        }

        public string GrupoEstatistico
        {
            get
            {
                return dsc_grupo;
            }
            set
            {
                dsc_grupo = value;
            }
        }

        public Hcrp.Framework.Classes.MapeamentoLocal Local
        {
            get
            {
                return num_seq_local; 
            }
            set
            {
                num_seq_local = value;
            }
        }

        public List<MetricaEstatistica> Metricas
        {
            get
            {                
                return MetricasEstatisticas;
            }
            set
            {
                MetricasEstatisticas = value;
            }
        }

        public int Ordem
        {
            get
            {
                return num_ordem; 
            }
            set
            {
                num_ordem = value;
            }
        }

        public Hcrp.Framework.Classes.Usuario Usuario
        {
            get
            {                
                return num_user_banco; 
            }
            set
            {
                num_user_banco = value;
            }
        }

        public void Gravar()
        {
            throw new System.NotImplementedException();
        }

        public void Excluir()
        {
            throw new System.NotImplementedException();
        }

        public List<AssociacaoAtividadeLocalEstatistica> BuscarAssociacaoLocal(Hcrp.Framework.Classes.MapeamentoLocal Local)
        {
            throw new System.NotImplementedException();
        }

        public AssociacaoAtividadeLocalEstatistica BuscarAssociacaoLocal(Hcrp.Framework.Classes.MapeamentoLocal Local, AtividadeEstatistica Atividade)
        {
            throw new System.NotImplementedException();
        }
    }

    public class AssociacaoFavorecidoLocalEstatistica
    {
        private Hcrp.Framework.Classes.MapeamentoLocal num_seq_local;
        private FavorecidoEstatistico seq_favorecido;
        private int num_ordem;

        public Hcrp.Framework.Classes.MapeamentoLocal Local
        {
            get
            {                
                return num_seq_local;
            }
            set
            {
                num_seq_local = value;
            }
        }

        public FavorecidoEstatistico Favorecido
        {
            get
            {
                return seq_favorecido;
            }
            set
            {
                seq_favorecido = value;
            }
        }

        public int Ordem
        {
            get
            {                
                return num_ordem;
            }
            set
            {
                num_ordem = value;
            }
        }

        public void Gravar()
        {
            throw new System.NotImplementedException();
        }

        public void Excluir()
        {
            throw new System.NotImplementedException();
        }

        public List<AssociacaoFavorecidoLocalEstatistica> BuscarAssociacaoLocal(Hcrp.Framework.Classes.MapeamentoLocal Local)
        {
            throw new System.NotImplementedException();
        }

        public AssociacaoFavorecidoLocalEstatistica BuscarAssociacaoLocal(Hcrp.Framework.Classes.MapeamentoLocal Local, FavorecidoEstatistico Favorecido)
        {
            throw new System.NotImplementedException();
        }
    }

    public class FechamentoEstatistica
    {
        private int seq_fechamento_estat_gad;
        /// <remarks>
        /// Este campo armazena o mês e mes do fechameno estatístico na seguinte forma:
        /// Ex.: 01/01/2012 - mês Janeiro , mes 2012
        /// </remarks>
        private DateTime dta_referencia;
        private int cod_inst_sistema;
        private Hcrp.Framework.Classes.Usuario num_user_fechamento;
        private DateTime dta_hor_fechamento;

        public int CodigoFechto
        {
            get
            {                
                return seq_fechamento_estat_gad;
            }
            set
            {
                seq_fechamento_estat_gad = value;
            }
        }

        public DateTime DataReferencia
        {
            get
            {                
                return dta_referencia;
            }
            set
            {
                dta_referencia = value;
            }
        }

        public int CodInstSistema
        {
            get
            {                
                return cod_inst_sistema;
            }
            set
            {
                cod_inst_sistema = value;
            }
        }

        public Hcrp.Framework.Classes.Usuario UsuarioFechamento
        {
            get
            {                
                return num_user_fechamento;
            }
            set
            {
                num_user_fechamento = value;
            }
        }

        public DateTime DataHoraFechamento
        {
            get
            {                
                return dta_hor_fechamento;
            }
            set
            {
                dta_hor_fechamento = value;
            }
        }

        public List<FechamentoEstatistica> BuscarDadosFechamento(int Ano)
        {
            throw new System.NotImplementedException();
        }

        public void FecharMes()
        {
            throw new System.NotImplementedException();
        }

        public void ReabrirMes(int mes, int ano)
        {
            throw new System.NotImplementedException();
        }

        public FechamentoEstatistica BuscarFechamentoPorCodigo(int CodigoFechto)
        {
            throw new System.NotImplementedException();
        }
    }

    public class LancamentoEstatistico
    {
        private int seq_lancamento_local_estat;
        private DateTime dta_referencia;
        private Hcrp.Framework.Classes.MapeamentoLocal num_seq_local;
        private AtividadeEstatistica seq_atividade;
        private FavorecidoEstatistico seq_favorecido;
        private MetricaEstatistica seq_metrica;
        private float qtd_atividade;

        public AtividadeEstatistica Atividade
        {
            get
            {                
                return seq_atividade;
            }
            set
            {
                seq_atividade = value;
            }
        }

        public int CodigoLancamento
        {
            get
            {
                return seq_lancamento_local_estat;
            }
            set
            {
                seq_lancamento_local_estat = value;
            }
        }

        public DateTime DataReferencia
        {
            get
            {
                return dta_referencia;
            }
            set
            {
                dta_referencia = value;
            }
        }

        public FavorecidoEstatistico Favorecido
        {
            get
            {
                return seq_favorecido;
            }
            set
            {
                seq_favorecido = value;
            }
        }

        public Hcrp.Framework.Classes.MapeamentoLocal Local
        {
            get
            {
                return num_seq_local;
            }
            set
            {
                num_seq_local = value;
            }
        }

        public MetricaEstatistica Metrica
        {
            get
            {
                return seq_metrica;
            }
            set
            {
                seq_metrica = value;
            }
        }

        public float Quantidade
        {
            get
            {
                return qtd_atividade;
            }
            set
            {
                qtd_atividade = value;
            }
        }

        public List<LancamentoEstatistico> BuscarLancamentos(Hcrp.Framework.Classes.MapeamentoLocal Local, int mes, int ano)
        {
            throw new System.NotImplementedException();
        }

        /// <remarks>A função deve retornar o código gerado para o lançamento cadastrado.</remarks>
        public void Gravar()
        {
            throw new System.NotImplementedException();
        }

        public void Excluir()
        {
            throw new System.NotImplementedException();
        }
    }
}
