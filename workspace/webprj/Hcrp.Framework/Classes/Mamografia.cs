using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class Mamografia
    {
        public int Seq { get; set; }
        public PedidoAtendimento PedidoAtendimento { get; set; }
        public long seqPedidoAtendimento { get; set; }
        public int NoduloCarocoMamaDireita { get; set; }
        public int NoduloCarocoMamaEsquerda { get; set; }
        public int RiscoElevadoCancer { get; set; }
        public int ExamePrevio { get; set; }
        public int MamografiaPrevia { get; set; }
        public int AnoMamografiaPrevia { get; set; }
        public DateTime DataExameClinico { get; set; }
        public int TipoMamografia { get; set; }
        public int MamaMamografiaDiagnostica { get; set; }
        public int TipoMamografiaDiagnostica { get; set; }
        public int AvaliacaoRespostaQT { get; set; }
        public List<MamaControleLesao> MamaControleLesao { get; set; }
        public List<MamaLocalizacao> MamaLocalizacao { get; set; }
        public List<MamaAchado> MamaAchado { get; set; }

        public string NoduloCarocoMamaDireitaExtenso 
        {
            get
            {
                if (this.NoduloCarocoMamaDireita == 0)
                    return "Não";
                else if (this.NoduloCarocoMamaDireita == 3)
                    return "Sim";
                else
                    return "";
            }
        }
        public string NoduloCarocoMamaEsquerdaExtenso
        {
            get
            {
                if (this.NoduloCarocoMamaEsquerda == 0)
                    return "Não";
                else if (this.NoduloCarocoMamaEsquerda == 3)
                    return "Sim";
                else
                    return "";
            }
        }
        public string RiscoElevadoCancerExtenso
        {
            get
            {
                if (this.RiscoElevadoCancer == 1)
                    return "Não";
                else if (this.RiscoElevadoCancer == 2)
                    return "Não sabe";
                else if (this.RiscoElevadoCancer == 3)
                    return "Sim";
                else
                    return "";
            }
        }
        public string ExamePrevioExtenso
        {
            get
            {
                if (this.ExamePrevio == 3)
                    return "Sim";
                else if (this.ExamePrevio == 2)
                    return "Nunca foram examinadas anteriormente";                
                else
                    return "";
            }
        }
        public string MamografiaPreviaExtenso
        {
            get
            {
                if (this.MamografiaPrevia == 1)
                    return "Não";
                else if (this.MamografiaPrevia == 3)
                    return "Sim";
                else if (this.MamografiaPrevia == 2)
                    return "Não sabe";
                else
                    return "";
            }
        }
        public string TipoMamografiaExtenso
        {
            get
            {
                if (this.TipoMamografia == 1)
                    return "Diagnóstica";
                else if (this.TipoMamografia == 3)
                    return "De rastreamento";                
                else
                    return "";
            }
        }
        public string MamaMamografiaDiagnosticaExtenso
        {
            get
            {
                if (this.MamaMamografiaDiagnostica == 1)
                    return "Direita";
                else if (this.MamaMamografiaDiagnostica == 2)
                    return "Esquerda";
                else if (this.MamaMamografiaDiagnostica == 3)
                    return "Ambas";
                else
                    return "";
            }
        }
        public string TipoMamografiaDiagnosticaExtenso
        {
            get
            {
                if (this.TipoMamografiaDiagnostica == 1)
                    return "Achados no exame clínico";
                else if (this.TipoMamografiaDiagnostica == 2)
                    return "Controle radiológico Categoria 3";
                else if (this.TipoMamografiaDiagnostica == 3)
                    return "Lesão com diagnóstico de câncer";
                else if (this.TipoMamografiaDiagnostica == 4)
                    return "Avaliação de resposta de QT neo-adjuvante";
                else
                    return "";
            }
        }
        public string AvaliacaoRespostaQTExtenso
        {
            get
            {
                if (this.AvaliacaoRespostaQT == 3)
                    return "Sim";                
                else
                    return "";
            }
        }

        public Mamografia()
        {}

        public long Gravar()
        {
            return new Hcrp.Framework.Dal.Mamografia().Gravar(this);
        }

        public long GravarTrans(Hcrp.Infra.AcessoDado.TransacaoDinamica transacao)
        {
            return new Hcrp.Framework.Dal.Mamografia(transacao).GravarTrans(this);
        }
    }
}
