SELECT vr.cod_paciente,
      extract(MONTH FROM vr.dta_hor_cadastro) mes,
      COUNT(*) qtd
       FROM vacina_recepcao vr
WHERE extract(YEAR FROM vr.dta_hor_cadastro) = 2017
GROUP BY extract(MONTH FROM vr.dta_hor_cadastro),
        vr.cod_paciente;
        
   SELECT vr.cod_paciente,
         sum(decode(extract(MONTH FROM vr.dta_hor_cadastro), 1, extract(MONTH FROM vr.dta_hor_cadastro))) janeiro,
         sum(decode(extract(MONTH FROM vr.dta_hor_cadastro), 2, extract(MONTH FROM vr.dta_hor_cadastro))) fevereiro,
         sum(decode(extract(MONTH FROM vr.dta_hor_cadastro), 3, extract(MONTH FROM vr.dta_hor_cadastro))) março,
         sum(decode(extract(MONTH FROM vr.dta_hor_cadastro), 4, extract(MONTH FROM vr.dta_hor_cadastro))) abril,
         sum(decode(extract(MONTH FROM vr.dta_hor_cadastro), 5, extract(MONTH FROM vr.dta_hor_cadastro))) maio,
         sum(decode(extract(MONTH FROM vr.dta_hor_cadastro), 6, extract(MONTH FROM vr.dta_hor_cadastro))) junho,
         sum(decode(extract(MONTH FROM vr.dta_hor_cadastro), 7, extract(MONTH FROM vr.dta_hor_cadastro))) julho,
         sum(decode(extract(MONTH FROM vr.dta_hor_cadastro), 8, extract(MONTH FROM vr.dta_hor_cadastro))) agosto,
         sum(decode(extract(MONTH FROM vr.dta_hor_cadastro), 9, extract(MONTH FROM vr.dta_hor_cadastro))) setembro,
         sum(decode(extract(MONTH FROM vr.dta_hor_cadastro), 10, extract(MONTH FROM vr.dta_hor_cadastro))) outubro,
         sum(decode(extract(MONTH FROM vr.dta_hor_cadastro), 11, extract(MONTH FROM vr.dta_hor_cadastro))) novembro,
         sum(decode(extract(MONTH FROM vr.dta_hor_cadastro), 12, extract(MONTH FROM vr.dta_hor_cadastro))) dezembro
        
       FROM vacina_recepcao vr
WHERE extract(YEAR FROM vr.dta_hor_cadastro) = 2017
GROUP BY extract(MONTH FROM vr.dta_hor_cadastro), vr.cod_paciente;
         

 SELECT VR.COD_PACIENTE,
        TRUNC(VR.DTA_HOR_CADASTRO),
        COUNT(VR.COD_PACIENTE)
        
   FROM VACINA_RECEPCAO VR
        
  WHERE TRUNC(VR.DTA_HOR_CADASTRO) = TRUNC(SYSDATE)
  GROUP BY TRUNC(VR.DTA_HOR_CADASTRO), VR.COD_PACIENTE;
 -- ORDER BY VR.DTA_HOR_CADASTRO DESC;

SELECT VR.COD_PACIENTE,
       TRUNC(VR.DTA_HOR_CADASTRO),
       COUNT(VR.COD_PACIENTE) VACINAS_TOMADAS,
      LISTAGG(TC.DSC_TIPO_VACINA,','||CHR(10)) WITHIN GROUP(ORDER BY TC.DSC_TIPO_VACINA) DSC_TIPO_VACINA     
  FROM VACINA_RECEPCAO VR
  JOIN VACINA_RECEPCAO_APLICACAO VRA
    ON VRA.SEQ_VACINA_RECEPCAO = VR.SEQ_VACINA_RECEPCAO
  JOIN TIPO_VACINA TC
    ON TC.COD_TIPO_VACINA = VRA.COD_TIPO_VACINA
 WHERE TRUNC(VR.DTA_HOR_CADASTRO) = TRUNC(SYSDATE)
 GROUP BY VR.COD_PACIENTE,
      TRUNC(VR.DTA_HOR_CADASTRO);
    
 
  --listagg(pc.num_seq_agrupamento, ', ') WITHIN GROUP(ORDER BY pc.num_seq_agrupamento) num_seq_agrupamento
     
     (SELECT TC.DSC_TIPO_VACINA
           FROM VACINA_RECEPCAO_APLICACAO VRA
           JOIN TIPO_VACINA TC
             ON TC.COD_TIPO_VACINA = VRA.COD_TIPO_VACINA
          WHERE VRA.SEQ_VACINA_RECEPCAO = VR.SEQ_VACINA_RECEPCAO) 
