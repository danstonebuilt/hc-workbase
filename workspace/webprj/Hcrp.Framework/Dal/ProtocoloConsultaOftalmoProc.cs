using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Dal
{
    public class ProtocoloConsultaOftalmoProc
    {
        /// <summary>
        /// Inserir protocolo consulta procedimento
        /// </summary>
        /// <param name="protoc"></param>
        public void Inserir(Framework.Classes.ProtocoloConsultaOftalmoProc protoc)
        {
            try
            {

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PROTOCOLO_CONS_OFTALMO_EXAMES");
                    comando.Params["SEQ_PROT_CONS_OFTALMOLOGIA"] = protoc.NumSeqProtocoloConsultaOftalmologia;
                    comando.Params["IDF_PROCEDIMENTO"] = protoc.IdfProcedimento;
                    
                    if (!string.IsNullOrWhiteSpace(protoc.DescricaoOutroProc))
                        comando.Params["DSC_OUTRO_PROCEDIMENTO"] = protoc.DescricaoOutroProc;

                    // Executar o insert
                    ctx.ExecuteInsert(comando);

                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
