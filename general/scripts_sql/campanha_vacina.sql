WITH FUNC_HC AS
 (SELECT VRH.NUMCAD,
         P.COD_PACIENTE,
         P.NOM_PACIENTE || ' ' || P.SBN_PACIENTE NOME,       
         P.DTA_NASCIMENTO,
         P.IDF_SEXO,
         FNC_CALCULA_TEMPO(SYSDATE, P.DTA_NASCIMENTO, 'aammdd') IDADE,         
         X.LOCAL,
         EMP.APEEMP,
         CAR.TITRED  
    FROM PACIENTE           P,
         V_FUNCIONARIO_RUBI VRH,
         
         (SELECT *
            FROM (SELECT DISTINCT C.CODLOC,
                                  A.NOMLOC,
                                  C.CODLOC || ' - ' || A.NOMLOC AS LOCAL,
                                  C.NUMLOC,
                                  B.NUMEMP
                    FROM R016ORN A, R034FUN B, R016HIE C
                   WHERE B.NUMEMP IN (1, 2, 3, 4, 5, 6, 7) --Todas as intituições
                     AND B.TIPCOL = 1
                     AND B.SITAFA <> 7
                     AND A.NUMLOC = B.NUMLOC
                     AND A.NUMLOC = C.NUMLOC
                     AND A.TABORG =
                         (SELECT MAX(TABORG)
                            FROM R016ORN X
                           WHERE X.NUMLOC = B.NUMLOC
                             AND X.NUMLOC = C.NUMLOC
                             AND A.DATEXT = TO_DATE('31/12/1900', 'DD/MM/YYYY')))) X,
         
         (SELECT V.NOMEMP, V.APEEMP, V.NUMCGC, V.NUMEMP, V.SIGEMP
            FROM V_EMPRESA_RUBI V) EMP,
         
         (SELECT F.NUMEMP, 
                 F.NUMCAD,
                 C.TITRED
            FROM SENIOR.R034FUN F
            LEFT JOIN R024CAR C
              ON C.ESTCAR = F.ESTCAR
             AND C.CODCAR = F.CODCAR
            LEFT JOIN R030EMP E
              ON E.NUMEMP = F.NUMEMP
           WHERE F.TIPCOL = 1 -- sempre 1
                -- filtro de demitido/admitido  
             AND F.SITAFA NOT IN
                 (SELECT S.CODSIT FROM R010SIT S WHERE S.TIPSIT = 7)) CAR
  
   WHERE TRIM(TO_CHAR(VRH.NUMCPF, '00000000009')) = P.CPF_PACIENTE
     AND VRH.NUMEMP = X.NUMEMP
     AND VRH.NUMEMP = EMP.NUMEMP
     AND VRH.NUMLOC = X.NUMLOC
     AND VRH.NUMCAD = CAR.NUMCAD
     AND VRH.NUMEMP = CAR.NUMEMP 
  )
--****End with

SELECT *
  FROM (SELECT FHC.*,
               VN.DTA_VACINACAO DATA_VACINA,
               TV.DSC_TIPO_VACINA,
               D.DSC_DOSE,
               ' Aplicada ' STATUS,
               VN.DSC_OBSERVACAO,
               TO_CHAR(L.NUM_LOTE_VACINA) NUM_LOTE_VACINA
          FROM VACINACAO          VN,
               PACIENTE_VACINACAO PV,
               TIPO_VACINA        TV,
               DOSE               D,
               VACINA             V,
               PACIENTE           P,
               LOTE_VACINA        L,
               FUNC_HC            FHC
         WHERE VN.COD_PACIENTE_VACINACAO = PV.COD_PACIENTE_VACINACAO            
           AND VN.COD_TIPO_VACINA = V.COD_TIPO_VACINA
           AND VN.NUM_DOSE = V.NUM_DOSE
           AND V.COD_TIPO_VACINA = TV.COD_TIPO_VACINA
           AND V.NUM_DOSE = D.NUM_DOSE
           AND P.COD_PACIENTE = PV.COD_PACIENTE
           AND L.COD_LOTE_VACINA = VN.COD_LOTE_VACINA
           AND PV.COD_PACIENTE = FHC.COD_PACIENTE
           AND TV.COD_TIPO_VACINA IN (15, 60) --Apenas vacina gripe
           AND EXTRACT(YEAR FROM VN.DTA_VACINACAO) = 2017
        UNION ALL
        SELECT FHC.*,
               AV.DTA_AGENDA_VACINACAO DATA_VACINA,
               TV.DSC_TIPO_VACINA,
               D.DSC_DOSE,
               DECODE(AV.IDF_AGENDA_VACINACAO, 1, 'Agendado', 2,'Confirmado', 3,'Notificado', 4, 'Cancelado') STATUS,
               '' DSC_OBSERVACAO,
               '' NUM_LOTE_VACINA
          FROM AGENDA_VACINACAO   AV,
               PACIENTE_VACINACAO PV,
               TIPO_VACINA        TV,
               DOSE               D,
               VACINA             V,
               PACIENTE           P,
               FUNC_HC            FHC
         WHERE AV.DTA_AGENDA_VACINACAO >= TRUNC(SYSDATE)
           AND AV.COD_PACIENTE_VACINACAO = PV.COD_PACIENTE_VACINACAO             
           AND AV.COD_TIPO_VACINA = V.COD_TIPO_VACINA
           AND AV.NUM_DOSE = V.NUM_DOSE
           AND V.COD_TIPO_VACINA = TV.COD_TIPO_VACINA
           AND V.NUM_DOSE = D.NUM_DOSE
           AND P.COD_PACIENTE = PV.COD_PACIENTE
           AND PV.COD_PACIENTE = FHC.COD_PACIENTE
           AND TV.COD_TIPO_VACINA IN (15, 60) --Apenas vacina gripe
           AND EXTRACT(YEAR FROM AV.DTA_AGENDA_VACINACAO) = 2017
        UNION ALL
        SELECT FHC.*,
               COALESCE(VA.DTA_HOR_APLICACAO, UMAT.DTA_HOR_LANCAMENTO) DATA_VACINA,
               TV.DSC_TIPO_VACINA,
               D.DSC_DOSE,
               DECODE(VA.IDF_STATUS, 0, 'CANCELADO', 1, 'AGUARDANDO', 2,'APLICADA') STATUS,
               NVL(VA.DSC_JUSTIFICATIVA, IND.DSC_INDICACAO) DSC_OBSERVACAO,
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
               VACINA_CONFIGURACAO       VC,
               ESTRATEGIA_VACINACAO      EV,
               FUNC_HC                   FHC
         WHERE VR.SEQ_VACINA_RECEPCAO = VA.SEQ_VACINA_RECEPCAO
           AND VA.SEQ_UTILIZACAO_MATERIAL = UMAT.SEQ_UTILIZACAO_MATERIAL(+)
           AND VA.NUM_DOSE = D.NUM_DOSE
           AND VA.SEQ_IND_VACINA_MATERIAL =  INDMAT.SEQ_IND_VACINA_MATERIAL(+)
           AND INDMAT.SEQ_INDICACAO_VACINA = IND.SEQ_INDICACAO_VACINA(+)
           AND VA.COD_TIPO_VACINA = TV.COD_TIPO_VACINA
           AND UMAT.NUM_LOTE = L.NUM_LOTE(+)
           AND VR.COD_PACIENTE = P.COD_PACIENTE
           AND VA.SEQ_VACINA_CONFIGURACAO = VC.SEQ_VACINA_CONFIGURACAO
           AND VC.SEQ_ESTRATEGIA_VACINA = EV.SEQ_ESTRATEGIA_VACINA               
           AND P.COD_PACIENTE = FHC.COD_PACIENTE
           AND TV.COD_TIPO_VACINA IN (15, 60) --Apenas vacina gripe
           AND EXTRACT(YEAR FROM COALESCE(VA.DTA_HOR_APLICACAO,
                                UMAT.DTA_HOR_LANCAMENTO)) = 2017) X
 ORDER BY X.LOCAL;
