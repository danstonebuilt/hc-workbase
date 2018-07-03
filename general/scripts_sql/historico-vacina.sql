SELECT P.COD_PACIENTE,       
       VN.DTA_VACINACAO DATA_VACINA,       
       TV.DSC_TIPO_VACINA,       
       D.DSC_DOSE,       
       ' Aplicada ' STATUS,
       FNC_CALCULA_TEMPO(VN.DTA_VACINACAO, P.DTA_NASCIMENTO, 'AAMMDD')  IDADE_NA_EPOCA_APLICACAO,       
       VN.DSC_OBSERVACAO,       
       TO_CHAR(L.NUM_LOTE_VACINA) NUM_LOTE_VACINA
  FROM VACINACAO VN,       
       PACIENTE_VACINACAO PV,       
       TIPO_VACINA TV,       
       DOSE D,       
       VACINA V,       
       PACIENTE P,       
       LOTE_VACINA L
 WHERE

 VN.COD_PACIENTE_VACINACAO = PV.COD_PACIENTE_VACINACAO
 AND PV.COD_PACIENTE = '1340268A'
 AND VN.COD_TIPO_VACINA = V.COD_TIPO_VACINA
 AND VN.NUM_DOSE = V.NUM_DOSE
 AND V.COD_TIPO_VACINA = TV.COD_TIPO_VACINA
 AND V.NUM_DOSE = D.NUM_DOSE
 AND P.COD_PACIENTE = PV.COD_PACIENTE
 AND L.COD_LOTE_VACINA = VN.COD_LOTE_VACINA

UNION ALL

SELECT
 P.COD_PACIENTE, 
 AV.DTA_AGENDA_VACINACAO DATA_VACINA, 
 TV.DSC_TIPO_VACINA, 
 D.DSC_DOSE, 
 DECODE(AV.IDF_AGENDA_VACINACAO,
        1,
        'Agendado',
        2,
        'Confirmado',
        3,
        'Notificado',
        4,
        'Cancelado') STATUS,
  
FNC_CALCULA_TEMPO(AV.DTA_AGENDA_VACINACAO, P.DTA_NASCIMENTO, 'AAMMDD')  IDADE_NA_EPOCA_APLICACAO,
 '' DSC_OBSERVACAO, 
 '' NUM_LOTE_VACINA
  FROM AGENDA_VACINACAO AV,       
       PACIENTE_VACINACAO PV,       
       TIPO_VACINA TV,       
       DOSE D,       
       VACINA V,       
       PACIENTE P
 WHERE
 AV.DTA_AGENDA_VACINACAO >= TRUNC(SYSDATE)
 AND AV.COD_PACIENTE_VACINACAO = PV.COD_PACIENTE_VACINACAO
 AND PV.COD_PACIENTE = '1340268A'
 AND AV.COD_TIPO_VACINA = V.COD_TIPO_VACINA
 AND AV.NUM_DOSE = V.NUM_DOSE
 AND V.COD_TIPO_VACINA = TV.COD_TIPO_VACINA
 AND V.NUM_DOSE = D.NUM_DOSE
 AND P.COD_PACIENTE = PV.COD_PACIENTE

UNION ALL

SELECT
 P.COD_PACIENTE, 
 COALESCE(VA.DTA_HOR_APLICACAO, UMAT.DTA_HOR_LANCAMENTO) DATA_VACINA, 
 TV.DSC_TIPO_VACINA, 
 D.DSC_DOSE, 
 DECODE(VA.IDF_STATUS,        
        0,        
        'CANCELADO',        
        1,        
        'AGUARDANDO',        
        2,        
        'APLICADA') STATUS,
  FNC_CALCULA_TEMPO(COALESCE(VA.DTA_HOR_APLICACAO, UMAT.DTA_HOR_LANCAMENTO), P.DTA_NASCIMENTO, 'AAMMDD')  IDADE_NA_EPOCA_APLICACAO,
 NVL(VA.DSC_JUSTIFICATIVA, IND.DSC_INDICACAO) DSC_OBSERVACAO, 
 L.NUM_LOTE_FABRICANTE NUM_LOTE_VACINA
  FROM PACIENTE P,       
       VACINA_RECEPCAO VR,       
       VACINA_RECEPCAO_APLICACAO VA,       
       DOSE D,       
       INDICACAO_VACINA IND,       
       UTILIZACAO_MATERIAL UMAT,       
       INDICACAO_VACINA_MATERIAL INDMAT,       
       TIPO_VACINA TV,       
       LOTE L,       
       VACINA_CONFIGURACAO VC,       
       ESTRATEGIA_VACINACAO EV
 WHERE VR.SEQ_VACINA_RECEPCAO = VA.SEQ_VACINA_RECEPCAO      
   AND VA.SEQ_UTILIZACAO_MATERIAL = UMAT.SEQ_UTILIZACAO_MATERIAL(+)      
   AND VA.NUM_DOSE = D.NUM_DOSE      
   AND VA.SEQ_IND_VACINA_MATERIAL = INDMAT.SEQ_IND_VACINA_MATERIAL(+)      
   AND INDMAT.SEQ_INDICACAO_VACINA = IND.SEQ_INDICACAO_VACINA(+)      
   AND VA.COD_TIPO_VACINA = TV.COD_TIPO_VACINA      
   AND UMAT.NUM_LOTE = L.NUM_LOTE(+)      
   AND VR.COD_PACIENTE = P.COD_PACIENTE      
   AND VA.SEQ_VACINA_CONFIGURACAO = VC.SEQ_VACINA_CONFIGURACAO      
   AND VC.SEQ_ESTRATEGIA_VACINA = EV.SEQ_ESTRATEGIA_VACINA      
   AND P.COD_PACIENTE = '1340268A';
