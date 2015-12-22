using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class FormularioPesquisaOpiniaoConvite
    {
        public int Seq { get; set; }
        public string SeqHexa {
            get {
                // return string.Format("{0:x}", this.Seq);
                // Rodolfo

                string SeqHexa = string.Format("{0:x}", this.Seq);
                return SeqHexa + calculaDigito(SeqHexa);
            }
        }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataHoraConvite { get; set; }
        public bool Respondido { get; set; }
        public string ImgEnvio
        {
            get {
                if (string.IsNullOrWhiteSpace(this.Email))
                    return "http://10.165.5.50/InterfaceHC/imagens/impressora.jpg";
                else return "http://10.165.5.50/InterfaceHC/imagens/email.png";
            }
        }

        static public string calculaDigito(string valor)
        {


            var str = new string(valor.Reverse().ToArray());
            /*
            Int64 seqRev = Convert.ToInt64(str, 16);
            seqRev = seqRev % 16;
            */

            int[] arrP = { 4, 3, 1, 9, 2, 5, 7, 6, 8, 1, 3, 2, 6, 5, 4, 7, 9, 8 };
            var arr = str.ToArray();
            var n = 0;
            for (var i = 0; i < arr.Length; i++)
            {
                var num = arr[i] * arrP[i];
                n += num;
            }
            var seqRev = n % 16;

            /*
            Int64 seq = Convert.ToInt64(valor, 16);
            seq = seq % 10;
            */

            //int[] arrP = {4,3,1,9,2,5,7,6,8,1,3,2,6,5,4,7,9,8};

            arr = valor.ToArray();
            n = 0;
            for (var i = 0; i < arr.Length; i++)
            {
                var num = arr[i] * arrP[i];
                n += num;
            }
            var seq = n % 16;

            return seq.ToString("X") + seqRev.ToString("X");

        }

        static public string decodeChave(string chave)
        {
            var dig = chave.Substring(chave.Length - 2);
            var valor = chave.Substring(0, chave.Length - 2);

            var digcalc = calculaDigito(valor);

            if (Convert.ToInt64(dig, 16) != Convert.ToInt64(digcalc, 16))
                valor = "0";

            return valor;
        }



        public FormularioPesquisaOpiniaoConvite() { }

        public string VerificaConviteValido(Int64 chave, int formPesquisa)
        {
            return new Hcrp.Framework.Dal.FormularioPesquisaOpiniaoConvite().VerificaConviteValido(chave, formPesquisa);
        }
        
        public void MarcarConviteRespondido(Int64 chave)
        {
            new Hcrp.Framework.Dal.FormularioPesquisaOpiniaoConvite().MarcarConviteRespondido(chave);
        }

        public void MarcarConviteEnviado()
        {
            new Hcrp.Framework.Dal.FormularioPesquisaOpiniaoConvite().MarcarConviteEnviado(this);
        }

        public List<Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite> BuscarConvitesFormulario(Hcrp.Framework.Classes.FormularioPesquisaOpiniao formulario)
        {
            return new Hcrp.Framework.Dal.FormularioPesquisaOpiniaoConvite().BuscarConvitesFormulario(formulario);
        }

        public List<Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite> BuscarTodosConviteRelatorio(Int64 formulario)
        {
            return new Hcrp.Framework.Dal.FormularioPesquisaOpiniaoConvite().BuscarTodosConviteRelatorio(formulario);
        }

        public List<Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite> BuscarTodosConviteEmail(Int64 formulario)
        {
            return new Hcrp.Framework.Dal.FormularioPesquisaOpiniaoConvite().BuscarTodosConviteEmail(formulario);
        }

        public List<Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite> BuscarConviteRelatorio(int seq)
        {
            Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite Convite = new Hcrp.Framework.Dal.FormularioPesquisaOpiniaoConvite().BuscarConvite(seq);
            List<Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite> l = new List<FormularioPesquisaOpiniaoConvite>();
            l.Add(Convite);
            return l;
        }

        public Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite BuscarConvite(int seq)
        {
            return new Hcrp.Framework.Dal.FormularioPesquisaOpiniaoConvite().BuscarConvite(seq);
        }

        /// <summary>
        /// Grava um Convite
        /// </summary>
        /// <param name="seqForm">Número Inteiro que representa a Seq do Formulario desejado</param>
        /// <returns>true se gravou e false se teve algum problema</returns>
        public bool Gravar(int seqForm)
        {
            return new Hcrp.Framework.Dal.FormularioPesquisaOpiniaoConvite().Gravar(this, seqForm);
        }

        public bool Excluir()
        {
            return new Hcrp.Framework.Dal.FormularioPesquisaOpiniaoConvite().Excluir(this);
        }
    }
}
