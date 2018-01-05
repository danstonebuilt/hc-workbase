SELECT GENERICO.FCN_LINHA_COLUNA('SELECT C.NOM_PROCEDIMENTO_HC FROM AGENDA_CIRURGIA A, AGENDA_PROCEDIMENTO_HC B, PROCEDIMENTO_HC C
   WHERE A.SEQ_AGENDA_CIRURGIA = B.SEQ_AGENDA_CIRURGIA
   AND B.COD_PROCEDIMENTO_HC = C.COD_PROCEDIMENTO_HC
   AND A.SEQ_AGENDA_CIRURGIA = ' || AC.SEQ_AGENDA_CIRURGIA || '', ', ') FROM DUAL;
