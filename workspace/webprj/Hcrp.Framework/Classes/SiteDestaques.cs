using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hcrp.Framework.Classes
{
    public class SiteDestaques
    {
        public int Seq { get; set; }
        public int IdLocal { get; set; }
        public int _IdInformacao { get; set; }
        public int _IdMenuHorizontal { get; set; }
        public int _IdMenuVertical { get; set; }
        public int _IdTipoDestaque { get; set; }

        public enum ETipoDestaque
        {
            A_PrincipalRotativo1 = 1,
            A_PrincipalRotativo2 = 2,
            A_PrincipalRotativo3 = 3,
            B_Trigemeos1 = 4,
            B_Trigemeos2 = 5,
            B_Trigemeos3 = 6,
            C_InicialEsquerdo = 7,
            D_GemeosBase1 = 8,
            D_GemeosBase2 = 9,
            E_SubDestaquePrincipal = 10,
            F_SubDestaqueDuplaBase1 = 11,
            F_SubDestaqueDuplaBase2 = 12,
            G_SubDestaqueLateralDireita = 13,
            H_DestaqueInformacaoLateral1 = 14,
            H_DestaqueInformacaoLateral2 = 15,
            Todos = -1
        }

        public SiteDestaques()
        {

        }

        public SiteDestaques(int seq)
        {
            if (seq > 0)
            {
                this.Seq = seq;
                new Hcrp.Framework.Dal.SiteDestaques().BuscarConfigDestaque(this);
            }
        }

        

        public Hcrp.Framework.Classes.SiteInformacao Informacao
        { 
            get
            {
                return new Hcrp.Framework.Classes.SiteInformacao().BuscaNoticiaId(this._IdInformacao);
            }
        }

           
        public List < Hcrp.Framework.Classes.SiteDestaques> Destaque(int CodSite)
        {            
            return new Hcrp.Framework.Dal.SiteDestaques().BuscarDestaque(CodSite);
        }


        public int BuscarSeqDestaque(int codSite, int idfLocal, int CodMenuHorizontal)
        {
            return new Hcrp.Framework.Dal.SiteDestaques().BuscarSeqDestaque(codSite, idfLocal, CodMenuHorizontal);
        }


        public int BuscarSeqDestaque(int codSite, int idfLocal)
        {
            return new Hcrp.Framework.Dal.SiteDestaques().BuscarSeqDestaque(codSite, idfLocal);
        }

        
        public bool GravarConfiguracoesDestaque()
        {
            return new Hcrp.Framework.Dal.SiteDestaques().GravarConfiguracoesDestaque(this);
        }

    }

}
