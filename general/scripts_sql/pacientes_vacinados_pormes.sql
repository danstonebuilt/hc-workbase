WITH qry_tmpvr AS  (
SELECT *
  FROM VACINA_RECEPCAO VR
 WHERE EXTRACT(YEAR FROM VR.DTA_HOR_CADASTRO) = 2017
   AND (VR.NUM_SEQ_LOCAL = 12401) )
   
   
   SELECT 
      COUNT(*) pac_hc FROM qry_tmpvr t
      WHERE t.cod_paciente IS NOT NULL
    GROUP BY EXTRACT(MONTH FROM t.dta_hor_cadastro) ;

SELECT CASE EXTRACT(MONTH FROM vr.dta_hor_cadastro)
            WHEN 1 THEN 'JANEIRO'
            WHEN 2 THEN 'FEVEREIRO'
            WHEN 3 THEN 'MARÇO'
            WHEN 4 THEN 'ABRIL'
            WHEN 5 THEN 'MAIO'
            WHEN 6 THEN 'JUNHO'
            WHEN 7 THEN 'JULHO' 
       END "MÊS",
       COUNT(*) pacientes_atendidos
  FROM VACINA_RECEPCAO VR
 WHERE EXTRACT(YEAR FROM VR.DTA_HOR_CADASTRO) = 2017
   AND (VR.NUM_SEQ_LOCAL = 12400 OR vr.num_seq_local IS NULL)
   GROUP BY EXTRACT(MONTH FROM vr.dta_hor_cadastro);
   
select * from GENERICO.VACINA_RECEPCAO_APLICACAO t;


SELECT * FROM vacina_recepcao vr
WHERE trunc(vr.dta_hor_cadastro) = TRUNC(SYSDATE);
   

