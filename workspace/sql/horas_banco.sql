select * from SENIOR.R034FUN t
WHERE 1 = 1
AND t.nomfun LIKE UPPER('%Ana Paula Loure%');
-- t.numemp = 1
--and   t.tipcol = 1
--and   t.numcad = 6373


SELECT TRUNC(SUM(DECODE(SINLAN, '+', QTDHOR, '-', -QTDHOR, 0)) / 60) AS HORA,
       ((SUM(DECODE(SINLAN, '+', QTDHOR, '-', -QTDHOR, 0)) / 60) -
       TRUNC(SUM(DECODE(SINLAN, '+', QTDHOR, '-', -QTDHOR, 0)) / 60)) * 60 AS MINUTO,
       MAX(DATLAN)
  FROM R011LAN
 WHERE NUMCAD = 6373 --6010
   AND NUMEMP = 1
   AND TIPCOL = 1;
   
   
   --pto saldo por dia

SELECT SUM( minutos_horas )
FROM (  
select A.DATLAN, sum(to_number(sinlan||qtdhor)) minutos_horas
from R011LAN A, R010SIT B
where numcad = 6373 and numemp = 1 and tipcol = 1
   AND A.DATLAN > TRUNC(SYSDATE-15,'MM')
   AND A.CODSIT = B.CODSIT
   group By A.DATLAN
   );

--pto extratificado do mes
select A.DATLAN, B.DESSIT,
       sinlan|| trim(to_char(trunc(decode(sinlan, '+', qtdhor, '-', qtdhor, 0) / 60),'00'))||':'||
       trim(to_char(((decode(sinlan, '+', qtdhor, '-', qtdhor, 0)/60)-trunc((decode(sinlan, '+', qtdhor, '-', qtdhor, 0)) / 60))*60,'00')) Horas
from R011LAN A, R010SIT B
where numcad = 6373 and numemp = 1 and tipcol = 1
   AND A.DATLAN > TRUNC(SYSDATE-15,'MM')
   AND A.CODSIT = B.CODSIT
   and datlan < trunc(sysdate);
   
--Saldo por dia
select A.DATLAN, sum(to_number(sinlan||qtdhor))
from R011LAN A, R010SIT B
where numcad = 6373 and numemp = 1 and tipcol = 1
  AND A.DATLAN > TRUNC(SYSDATE-15,'MM')
  AND A.CODSIT = B.CODSIT
  group By A.DATLAN;

SELECT SUM(SALDO_MINUTOS), SUM(SALDO_HORAS)   
FROM (
SELECT X.DATA, -- HORARIOS, 
       TO_CHAR(X.ENTRADA_1, 'HH24:MI')     ENTRADA_1,
       TO_CHAR(X.SAIDA_1, 'HH24:MI')       SAIDA_1,
       TO_CHAR(X.ENTRADA_2, 'HH24:MI')     ENTRADA_2,
       TO_CHAR(X.SAIDA_2, 'HH24:MI')       SAIDA_2,
       TO_CHAR(X.EXTRA_1, 'HH24:MI')       EXTRA_1,
       TO_CHAR(X.EXTRA_2, 'HH24:MI')       EXTRA_2,
       TRIM(TO_CHAR(TRUNC(((X.SAIDA_1 - X.ENTRADA_1) +
                          (X.SAIDA_2 - X.ENTRADA_2)) * 24),
                    '00')) || ':' ||
       TRIM(TO_CHAR(TRUNC(((((X.SAIDA_1 - X.ENTRADA_1) +
                          (X.SAIDA_2 - X.ENTRADA_2)) * 24) -
                          TRUNC(((X.SAIDA_1 - X.ENTRADA_1) +
                                 (X.SAIDA_2 - X.ENTRADA_2)) * 24)) * 60,
                          0),
                    '00')) TOTAL,
       
       (((((((X.SAIDA_1 - TO_DATE('01-JAN-1800')) * 360) -
       ((X.ENTRADA_1 - TO_DATE('01-JAN-1800')) * 360)) +
       (((X.SAIDA_2 - TO_DATE('01-JAN-1800')) * 360) -
       ((X.ENTRADA_2 - TO_DATE('01-JAN-1800')) * 360)))) - 120) * 4) SALDO_MINUTOS,
       CASE
         WHEN ENTRADA_2 IS NULL AND SAIDA_2 IS NULL AND
              X.DATA < TRUNC(SYSDATE) THEN
          (((((X.SAIDA_1 - TO_DATE('01-JAN-1800')) * 360) -
          ((X.ENTRADA_1 - TO_DATE('01-JAN-1800')) * 360)) - 120) * 4) / 60
         ELSE
          (((((((X.SAIDA_1 - TO_DATE('01-JAN-1800')) * 360) -
          ((X.ENTRADA_1 - TO_DATE('01-JAN-1800')) * 360)) +
          (((X.SAIDA_2 - TO_DATE('01-JAN-1800')) * 360) -
          ((X.ENTRADA_2 - TO_DATE('01-JAN-1800')) * 360)))) - 120) * 4) / 60
       END SALDO_HORAS

  FROM (
         
         SELECT DATA,
                 HORARIOS,
                 DECODE(SUBSTR(HORARIOS, 01, 5),
                        NULL,
                        TO_DATE(NULL),
                        TO_DATE(TO_CHAR(DATA, 'DD/MM/YYYY') || ' ' ||
                                SUBSTR(HORARIOS, 01, 5),
                                'DD/MM/YYYY HH24:MI')) ENTRADA_1,
                 DECODE(SUBSTR(HORARIOS, 09, 5),
                        NULL,
                        TO_DATE(NULL),
                        TO_DATE(TO_CHAR(DATA, 'DD/MM/YYYY') || ' ' ||
                                SUBSTR(HORARIOS, 09, 5),
                                'DD/MM/YYYY HH24:MI')) SAIDA_1,
                 DECODE(SUBSTR(HORARIOS, 17, 5),
                        NULL,
                        TO_DATE(NULL),
                        TO_DATE(TO_CHAR(DATA, 'DD/MM/YYYY') || ' ' ||
                                SUBSTR(HORARIOS, 17, 5),
                                'DD/MM/YYYY HH24:MI')) ENTRADA_2,
                 DECODE(SUBSTR(HORARIOS, 25, 5),
                        NULL,
                        TO_DATE(NULL),
                        TO_DATE(TO_CHAR(DATA, 'DD/MM/YYYY') || ' ' ||
                                SUBSTR(HORARIOS, 25, 5),
                                'DD/MM/YYYY HH24:MI')) SAIDA_2,
                 DECODE(SUBSTR(HORARIOS, 33, 5),
                        NULL,
                        TO_DATE(NULL),
                        TO_DATE(TO_CHAR(DATA, 'DD/MM/YYYY') || ' ' ||
                                SUBSTR(HORARIOS, 33, 5),
                                'DD/MM/YYYY HH24:MI')) EXTRA_1,
                 DECODE(SUBSTR(HORARIOS, 41, 5),
                        NULL,
                        TO_DATE(NULL),
                        TO_DATE(TO_CHAR(DATA, 'DD/MM/YYYY') || ' ' ||
                                SUBSTR(HORARIOS, 41, 5),
                                'DD/MM/YYYY HH24:MI')) EXTRA_2
           FROM (SELECT TRUNC(A.DATAHORA) DATA,
                          MIN(SUBSTR(FCN_LINHA_COLUNA('SELECT TO_CHAR(X.DATAHORA,''HH24:mi'') HORA
FROM SERVCOMNET.COLETA X
WHERE X.PIS = ''' || A.PIS || '''' || '
AND TO_CHAR(X.DATAHORA,''DD/MM/YYYY'') = ''' ||
                                                       TO_CHAR(TRUNC(A.DATAHORA),
                                                               'DD/MM/YYYY') || '''' ||
                                                       'ORDER BY X.DATAHORA ASC',
                                                       ' | '),
                                      0,
                                      200)) HORARIOS
                   
                     FROM (SELECT TO_DATE(TO_CHAR(A.DATAHORA, 'DD/MM/YYYY HH24:MI'),
                                          'DD/MM/YYYY HH24:MI') DATAHORA,
                                  A.PIS
                             FROM SERVCOMNET.COLETA A, SERVCOMNET.PESSOA B
                            WHERE B.MATRICULA = LPAD('6373', 20, '0')
                              AND A.PIS = B.PIS
                              AND TO_CHAR(A.DATAHORA, 'YYYY/MM') >=    TO_CHAR(SYSDATE - 5, 'YYYY/MM')
                            ORDER BY TO_DATE(TO_CHAR(A.DATAHORA,
                                                     'DD/MM/YYYY HH24:MI'),
                                             'DD/MM/YYYY HH24:MI')) A
                    GROUP BY TRUNC(A.DATAHORA))) X
                    );
/*Completo*/
SELECT X.DATA,-- HORARIOS,
  TO_CHAR(X.ENTRADA_1, 'HH24:MI') ENTRADA_1,
  TO_CHAR(X.SAIDA_1, 'HH24:MI') SAIDA_1,
  TO_CHAR(X.ENTRADA_2, 'HH24:MI') ENTRADA_2,
  TO_CHAR(X.SAIDA_2, 'HH24:MI') SAIDA_2,
  TRIM(TO_CHAR(TRUNC(((X.SAIDA_1 - X.ENTRADA_1) + (X.SAIDA_2 - X.ENTRADA_2)) * 24),'00')) ||':'||
  TRIM(TO_CHAR(TRUNC(((((X.SAIDA_1 - X.ENTRADA_1) + (X.SAIDA_2 - X.ENTRADA_2)) * 24) - 
  TRUNC(((X.SAIDA_1 - X.ENTRADA_1) + (X.SAIDA_2 - X.ENTRADA_2)) * 24) ) * 60, 0),'00')) TOTAL
FROM (
SELECT DATA,
HORARIOS,
DECODE(SUBSTR(HORARIOS,01,5), NULL, TO_DATE(NULL),TO_DATE(TO_CHAR(DATA,'DD/MM/YYYY')||' '||SUBSTR(HORARIOS,01,5),'DD/MM/YYYY HH24:MI')) ENTRADA_1,
DECODE(SUBSTR(HORARIOS,09,5), NULL, TO_DATE(NULL),TO_DATE(TO_CHAR(DATA,'DD/MM/YYYY')||' '||SUBSTR(HORARIOS,09,5),'DD/MM/YYYY HH24:MI')) SAIDA_1,
DECODE(SUBSTR(HORARIOS,17,5), NULL, TO_DATE(NULL),TO_DATE(TO_CHAR(DATA,'DD/MM/YYYY')||' '||SUBSTR(HORARIOS,17,5),'DD/MM/YYYY HH24:MI')) ENTRADA_2, 
 DECODE(SUBSTR(HORARIOS,25,5), NULL, TO_DATE(NULL),TO_DATE(TO_CHAR(DATA,'DD/MM/YYYY')||' '||SUBSTR(HORARIOS,25,5),'DD/MM/YYYY HH24:MI')) SAIDA_2,
DECODE(SUBSTR(HORARIOS,33,5), NULL, TO_DATE(NULL),TO_DATE(TO_CHAR(DATA,'DD/MM/YYYY')||' '||SUBSTR(HORARIOS,33,5),'DD/MM/YYYY HH24:MI')) EXTRA_1, 
 DECODE(SUBSTR(HORARIOS,41,5), NULL, TO_DATE(NULL),TO_DATE(TO_CHAR(DATA,'DD/MM/YYYY')||' '||SUBSTR(HORARIOS,41,5),'DD/MM/YYYY HH24:MI')) EXTRA_2
FROM (
select TRUNC(A.DATAHORA) DATA,
MIN(SUBSTR(fcn_linha_coluna('SELECT TO_CHAR(X.DATAHORA,''HH24:mi'') HORA
FROM SERVCOMNET.COLETA X
WHERE X.PIS = '''||A.PIS||''''||'
AND TO_CHAR(X.DATAHORA,''DD/MM/YYYY'') = '''||TO_CHAR( TRUNC(A.DATAHORA),'DD/MM/YYYY')||''''||
'ORDER BY X.DATAHORA ASC',' | '),0,200)) HORARIOS
FROM (
SELECT TO_DATE(TO_CHAR(A.DATAHORA,'DD/MM/YYYY HH24:MI'),'DD/MM/YYYY HH24:MI') DATAHORA, A.PIS
FROM SERVCOMNET.COLETA A, SERVCOMNET.PESSOA B
WHERE B.MATRICULA = LPAD('6373',20,'0')
AND A.PIS = B.PIS
AND TO_CHAR(A.DATAHORA,'YYYY/MM') >= TO_CHAR(SYSDATE-5,'YYYY/MM')
ORDER BY TO_DATE(TO_CHAR(A.DATAHORA,'DD/MM/YYYY HH24:MI'),'DD/MM/YYYY HH24:MI')) A
GROUP BY TRUNC(A.DATAHORA))) X ;
                    

