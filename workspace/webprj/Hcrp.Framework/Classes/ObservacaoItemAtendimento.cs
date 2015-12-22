using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ObservacaoItemAtendimento
    {
        public int _NumUserObservacao { get; set; }

        public int Seq { get; set; }
        public Hcrp.Framework.Classes.Usuario UsuarioObservacao
        {
            get
            {
                return new Hcrp.Framework.Classes.Usuario().BuscarUsuarioCodigo(this._NumUserObservacao);
            }
            set { }
        }        
        public ETipoObservacao TipoObservacao { get; set; }        
        public DateTime DataObservacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Descricao { get; set; }
        public ItemPedidoAtendimento ItemPedidoAtendimento { get; set; }

        public enum ETipoObservacao
        {
            JustificativaDevolucao = 1
        }
        public ObservacaoItemAtendimento() { }        

        public List<Hcrp.Framework.Classes.ObservacaoItemAtendimento> BuscarObservacoesItem(int ItemPedidoAtendimento)
        {
            return new Hcrp.Framework.Dal.ObservacaoItemAtendimento().BuscarObservacoesItem(ItemPedidoAtendimento);
        }
        public Boolean Gravar()
        {
            return new Hcrp.Framework.Dal.ObservacaoItemAtendimento().Gravar(this);
        }
    }
}
