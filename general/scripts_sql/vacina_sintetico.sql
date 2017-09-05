/*Vacina sintético*/
WITH qry_vacina AS (
    SELECT *
       FROM vacina_recepcao vr
    WHERE to_date(vr.dta_hor_cadastro, 'DD/MM/YYYY') =  to_date(SYSDATE, 'DD/MM/YYYY')
),

 tot_day AS (
   SELECT COUNT(*) tot_day
      FROM qry_vacina
),

reg_pat AS(
    SELECT COUNT(*) reg_pat
      FROM qry_vacina WHERE cod_paciente IS NOT NULL
)


SELECT  tot_day total_vacinados,
        reg_pat paciente_hc,
        (tot_day - reg_pat) pac_sem_registro_hc
        FROM tot_day,
              reg_pat;



  

 

