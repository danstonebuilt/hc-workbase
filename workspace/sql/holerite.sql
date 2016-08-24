--Hole
select T3.DATPAG, TO_CHAR(T3.DATGER,'DD/MM/YYYY')||' '||TRIM(TO_CHAR(TRUNC(FC_TO_NUMBER(T3.HORGER)/60),'00'))||':'||TRIM(TO_CHAR(FC_TO_NUMBER(T3.HORGER)-TRUNC(FC_TO_NUMBER(T3.HORGER)/60)*60,'00')) DATGER,
       T.CODCAL, T.CODEVE, T.REFEVE, T.VALEVE, T2.DESEVE, T3.VALPAG
from SENIOR.R046FFR T, R008EVC t2, R046PAG T3
where t3.numemp = 1
and   t3.tipcol = 1
and   t3.numcad = 6373
AND   t.numemp(+) = t3.numemp
and   t.tipcol(+) = t3.tipcol
and   t.numcad(+) = t3.numcad
AND T.CODCAL(+) = T3.CODCAL
AND T.CODEVE = T2.CODEVE(+)
AND T.TABEVE = T2.CODTAB(+)
AND T3.DATPAG> Trunc(SYSDATE-10,'MM');

--Hole ver antes do dia do pagto
select 
    c.codcal,
    c.datpag as DataPagamento,
    e.codeve as Evento,
    e.deseve as "Descricao Evento",
    f.valeve as Valor,
    f.refeve as Referencia
from  r046ver f 
      left join R008EVC e on e.codtab = f.tabeve and e.codeve = f.codeve
      left join R044CAL c on c.codcal = f.codcal and c.numemp = f.numemp 
where 
   f.numemp = 1 
   and f.tipcol = 1 
   and f.numcad =  6373 
   and f.codcal in (select codcal
                 from SENIOR.R044CAL 
                 where numemp = 1 
                       and extract(month from datpag) = extract(month from sysdate)
                       and extract(year from datpag) = extract(year from sysdate)
                       )
order by datapagamento, codcal, evento;
