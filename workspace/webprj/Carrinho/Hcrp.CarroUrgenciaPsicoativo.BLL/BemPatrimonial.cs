using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL
{
    public class BemPatrimonial
    {
        #region Métodos

        /// <summary>
        /// Obter o bem patrimonio por número e tipo de patrimonio vinculados a itens lista controle.
        /// </summary>        
        public Entity.BemPatrimonial ObterPatrimonioPorNumeroETipoVinculadoItemListaControle(Int64? numBem, Int64? numPatrimonio, Int64? tipoPatrimonio)
        {
            return new DAL.BemPatrimonial().ObterPatrimonioPorNumeroETipoVinculadoItemListaControle(numBem, numPatrimonio, tipoPatrimonio);
        }

        /// <summary>
        /// Obter lista de bens patrimoniais.
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.BemPatrimonial> ObterListaDeBensPatrimoniais(Int64? numPatrimonio,
                                                                                                    string dscModelo,
                                                                                                    Int64 codTipoPatrimonio,
                                                                                                    Int64 seqItemListaControle,
                                                                                                    int paginaAtual,
                                                                                                    out int totalRegistro)
        {
            return new DAL.BemPatrimonial().ObterListaDeBensPatrimoniais(numPatrimonio,
                                                                        dscModelo,
                                                                        codTipoPatrimonio,
                                                                        seqItemListaControle,
                                                                        Parametrizacao.Instancia().QuantidadeRegistroPagina,
                                                                        paginaAtual,
                                                                        out totalRegistro);
        }       


        public Entity.BemPatrimonial ObterPatrimonioPorNumeroETipo(Int64? numPatrimonio, Int64? tipoPatrimonio)
        {
            return new DAL.BemPatrimonial().ObterPatrimonioPorNumeroETipo(numPatrimonio, tipoPatrimonio);
        }

        #endregion
    }
}
