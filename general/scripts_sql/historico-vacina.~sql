SELECT p.cod_paciente,
        VN.DTA_VACINACAO DATA_VACINA, 
        TV.DSC_TIPO_VACINA, 
        D.DSC_DOSE, 
        ' Aplicada ' STATUS, 
        VN.DSC_OBSERVACAO,       
        to_char(l.NUM_LOTE_VACINA) NUM_LOTE_VACINA        
   FROM 
        VACINACAO VN, 
        PACIENTE_VACINACAO PV, 
        TIPO_VACINA TV, 
        DOSE D, 
        VACINA V, 
        PACIENTE P, 
        lote_vacina L 
  WHERE 
        VN.COD_PACIENTE_VACINACAO = PV.COD_PACIENTE_VACINACAO 
    AND PV.COD_PACIENTE           = '1340268A' 
    AND VN.COD_TIPO_VACINA        = V.COD_TIPO_VACINA 
    AND VN.NUM_DOSE               = V.NUM_DOSE 
    AND V.COD_TIPO_VACINA         = TV.COD_TIPO_VACINA 
    AND V.NUM_DOSE                = D.NUM_DOSE 
    AND P.COD_PACIENTE            = PV.COD_PACIENTE 
    AND L.COD_LOTE_VACINA         = vn.cod_lote_vacina
    AND tv.cod_tipo_vacina IN (15, 60)
    AND  EXTRACT(YEAR FROM VN.DTA_VACINACAO) = 2017
UNION ALL 
 SELECT 
        p.cod_paciente,
        AV.DTA_AGENDA_VACINACAO DATA_VACINA, 
        TV.DSC_TIPO_VACINA, 
        D.DSC_DOSE, 
        DECODE(AV.IDF_AGENDA_VACINACAO, 1, 'Agendado', 2, 'Confirmado', 3, 'Notificado', 4, 'Cancelado') STATUS, 
        '' DSC_OBSERVACAO,        
        '' NUM_LOTE_VACINA        
   FROM 
        AGENDA_VACINACAO AV, 
        PACIENTE_VACINACAO PV, 
        TIPO_VACINA TV, 
        DOSE D, 
        VACINA V, 
        PACIENTE P 
  WHERE 
        AV.DTA_AGENDA_VACINACAO  >= TRUNC(SYSDATE) 
    AND AV.COD_PACIENTE_VACINACAO = PV.COD_PACIENTE_VACINACAO 
    AND PV.COD_PACIENTE           = '1340268A' 
    AND AV.COD_TIPO_VACINA        = V.COD_TIPO_VACINA 
    AND AV.NUM_DOSE               = V.NUM_DOSE 
    AND V.COD_TIPO_VACINA         = TV.COD_TIPO_VACINA 
    AND V.NUM_DOSE                = D.NUM_DOSE 
    AND P.COD_PACIENTE            = PV.COD_PACIENTE
     AND tv.cod_tipo_vacina IN (15, 60)
      AND  EXTRACT(YEAR FROM AV.DTA_AGENDA_VACINACAO) = 2017  
 UNION ALL 
         SELECT
            p.cod_paciente, 
            COALESCE(VA.DTA_HOR_APLICACAO,UMAT.DTA_HOR_LANCAMENTO) DATA_VACINA, 
            TV.DSC_TIPO_VACINA,
            D.DSC_DOSE,
            DECODE(VA.IDF_STATUS,
                   0,
                   'CANCELADO',
                   1,
                   'AGUARDANDO',
                   2,
                   'APLICADA') STATUS,
            nvl(VA.DSC_JUSTIFICATIVA, ind.dsc_indicacao) DSC_OBSERVACAO,      
            L.NUM_LOTE_FABRICANTE NUM_LOTE_VACINA                   
       FROM PACIENTE                  P,
            VACINA_RECEPCAO           VR,
            VACINA_RECEPCAO_APLICACAO VA,
            DOSE                      D,
            INDICACAO_VACINA          IND,
            UTILIZACAO_MATERIAL       UMAT,
            INDICACAO_VACINA_MATERIAL INDMAT,
            TIPO_VACINA               TV,
            LOTE                      L,
            vacina_configuracao       vc, 
            estrategia_vacinacao      ev 
      WHERE VR.SEQ_VACINA_RECEPCAO = VA.SEQ_VACINA_RECEPCAO 
        AND VA.SEQ_UTILIZACAO_MATERIAL = UMAT.SEQ_UTILIZACAO_MATERIAL(+) 
        AND VA.NUM_DOSE = D.NUM_DOSE 
        AND VA.SEQ_IND_VACINA_MATERIAL = INDMAT.SEQ_IND_VACINA_MATERIAL(+) 
        AND INDMAT.SEQ_INDICACAO_VACINA = IND.SEQ_INDICACAO_VACINA(+) 
        AND VA.COD_TIPO_VACINA = TV.COD_TIPO_VACINA 
        AND UMAT.NUM_LOTE = L.NUM_LOTE(+) 
        AND VR.COD_PACIENTE = P.COD_PACIENTE 
        and va.seq_vacina_configuracao = vc.seq_vacina_configuracao 
        and vc.seq_estrategia_vacina = ev.seq_estrategia_vacina 
        AND P.COD_PACIENTE = '1340268A'
        AND tv.cod_tipo_vacina IN (15, 60)
        AND  EXTRACT(YEAR FROM COALESCE(VA.DTA_HOR_APLICACAO,UMAT.DTA_HOR_LANCAMENTO)) = 2017 
