using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Dal
{
    public class LogExtraCorporea
    {
        public Boolean Inserir(Hcrp.Framework.Classes.LogExtraCorporea LogExtraCorporea)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("LOG_EXTRACORPOREA");
                    comando.Params["NUM_USER_BANCO"] = LogExtraCorporea.Usuario.NumUserBanco;
                    comando.Params["COD_PACIENTE"] = LogExtraCorporea.Paciente.RegistroPaciente;
                    comando.Params["NUM_ALTURA"] = LogExtraCorporea.Altura;
                    comando.Params["NUM_PESO"] = LogExtraCorporea.Peso;
                    comando.Params["NUM_SUPERFICIE_CORPOREA"] = LogExtraCorporea.SuperficieCorporea;
                    comando.Params["NUM_CANULA_ARTERIAL_AO_ABAIXO"] = LogExtraCorporea.CanulaArterialAbaixo;
                    comando.Params["NUM_CANULA_ARTERIAL_AO_OTIMA"] = LogExtraCorporea.CanulaArterialOtima;
                    comando.Params["NUM_CANULA_ARTERIAL_AO_ACIMA"] = LogExtraCorporea.CanulaArterialAcima;
                    comando.Params["NUM_CANULA_ARTERIAL_FEMURAL"] = LogExtraCorporea.CanulaArterialFemural;
                    comando.Params["NUM_CANULA_VENOSA_SUPERIOR"] = LogExtraCorporea.CanulaVenosaSuperior;
                    comando.Params["NUM_CANULA_VENOSA_INFERIOR"] = LogExtraCorporea.CanulaVenosaInferior;
                    comando.Params["NUM_CANULA_VENOSA_UNICA"] = LogExtraCorporea.CanulaVenosaUnica;
                    comando.Params["NUM_HEPARINA_INICIAL_MG"] = LogExtraCorporea.HeparinaInicialMg;
                    comando.Params["NUM_HEPARINA_INICIAL_ML"] = LogExtraCorporea.HeparinaInicialMl;
                    comando.Params["NUM_SUP_CORP_ARREDONDADO"] = LogExtraCorporea.SuperficieCorporeaArredondado;
                    comando.Params["NUM_MITRAL"] = LogExtraCorporea.Mitral;
                    comando.Params["NUM_TRICUSPIDE"] = LogExtraCorporea.Tricuspide;
                    comando.Params["NUM_AORTICO"] = LogExtraCorporea.Aortico;
                    comando.Params["NUM_PULMONAR"] = LogExtraCorporea.Pulmonar;

                    // Executar o insert
                    ctx.ExecuteInsert(comando);
                    return true;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                return false;
            }
        }
    }
}
