SELECT OS.NUM_ORDEM,       
       OS.IDF_PRIORIZADO,       
       S4.NUM_TRIAGEM_OS,       
       TRIM(OS.NUM_ORDEM_SERVICO || '/' || OS.ANO_ORDEM_SERVICO) NUM_ORDEM_SERVICO,       
       OS.NUM_ORDEM_SERVICO NUMERO_OS,       
       OS.ANO_ORDEM_SERVICO ANO_OS,       
       TRUNC(TROS.DTA_HOR_TRIAGEM) DTA_HOR_TRIAGEM,       
       CC.COD_CENCUSTO || '-' || CC.NOM_CENCUSTO UNID_SOLICITANTE,       
       DECODE(OS.SGL_TAREFA,
              NULL,
              SUBSTR(NVL(OS.DSC_TITULO_OS, OS.DSC_SERVICO), 1, 240),
              OS.SGL_TAREFA || ' - ' ||
              SUBSTR(NVL(OS.DSC_TITULO_OS, OS.DSC_SERVICO), 1, 240)) DSC_SERVICO,       
       TROS.ORD_EXECUCAO ORD_EXECUCAO,       
       S4.QTD_APF,       
       S4.QTD_HOR_PREVISTA QTD_HORAS_PREVISTAS_REAL,       
       S4.QTD_ATV_CONCLUIDAS / S4.QTD_ATIVIDADES * 100 PCT_CONCLUIDA,       
       DECODE(S4.QTD_ATIVIDADES, 0, 1, S4.QTD_ATIVIDADES) QTD_TAREFA_OS,       
       S4.QTD_NAO_TRIADAS,       
       S4.QTD_TRIAGEM_PEND,       
       S4.QTD_ATV_CONCLUIDAS,       
       S4.HOR_APONTADAS * 24 TOTAL_APONTAMENTOS_REAL,       
       TROS.COD_CLASSIFICACAO_OS,       
       TS.DSC_SITUACAO_OS,       
       S.NOM_SISTEMA
  FROM (SELECT S3.NUM_ORDEM,
               
               S3.NUM_TRIAGEM_OS,
               
               SUM(S3.QTD_APF) QTD_APF,
               
               SUM(QTD_HORAS_PREVISTA) QTD_HOR_PREVISTA,
               
               SUM(QTD_ATIVIDADES) QTD_ATIVIDADES,
               
               SUM(QTD_ATV_CONCLUIDAS) QTD_ATV_CONCLUIDAS,
               
               SUM(HOR_APONTADAS) HOR_APONTADAS,
               
               SUM(QTD_NAO_TRIADAS) QTD_NAO_TRIADAS,
               
               SUM(QTD_TRIAGEM_PEND) QTD_TRIAGEM_PEND
        
          FROM (SELECT NUM_ORDEM,                       
                       NUM_TRIAGEM_OS,                       
                       SEQ_TAREFA_ORDEM_SERVICO,                       
                       QTD_APF,                       
                       QTD_HORAS_PREVISTA,                       
                       COUNT(*) QTD_ATIVIDADES,                       
                       SUM(DECODE(SEQ_FUNC_ORDEM_SERVICO, NULL, 1, 0)) QTD_NAO_TRIADAS,                       
                       SUM(DECODE(NVL(DTA_HOR_FINALIZACAO,                                      
                                      TO_DATE('01/01/1800', 'DD/MM/YYYY')),                                  
                                  TO_DATE('01/01/1800', 'DD/MM/YYYY'),        
                                  0,                                  
                                  1)) QTD_ATV_CONCLUIDAS,                       
                       SUM(DECODE(DTA_HOR_FINALIZACAO,                                  
                                  TO_DATE('01/01/1800', 'DD/MM/YYYY'),                                 
                                  1,
                                  
                                  0)) QTD_TRIAGEM_PEND,
                       
                       SUM(HOR_APONTADAS) HOR_APONTADAS
                
                  FROM (SELECT S1.NUM_ORDEM,                               
                               S1.NUM_TRIAGEM_OS,                               
                               S1.SEQ_TAREFA_ORDEM_SERVICO,                               
                               S1.QTD_APF,                               
                               S1.QTD_HORAS_PREVISTA,                               
                               FOS.SEQ_FUNC_ORDEM_SERVICO,                               
                               FOS.DTA_HOR_FINALIZACAO,
                               
                               NVL(SUM((SELECT SUM(DTA_HOR_FIM_ATIVIDADE -        
                                                   DTA_HOR_INICIO_ATIVIDADE)
                                        
                                          FROM APONTAMENTO_ATIVIDADE S2
                                        
                                         WHERE S2.SEQ_FUNC_ORDEM_SERVICO =                                              
                                               FOS.SEQ_FUNC_ORDEM_SERVICO)),
                                    
                                    0)
                               
                               -- Somando as OS apontadas pelo sistema anterior
                               
                                + NVL(SUM((SELECT SUM(DTA_HOR_FIM_ATIVIDADE -
                                                     
                                                     DTA_HOR_INICIO_ATIVIDADE)
                                          
                                            FROM APONTAMENTO_ATIVIDADE S2
                                          
                                           WHERE S2.SEQ_FUNC_ORDEM_SERVICO IS NULL
                                                
                                             AND S2.NUM_TRIAGEM_OS =
                                                
                                                 FOS.NUM_TRIAGEM_OS
                                                
                                             AND S2.NUM_FUNC_UNIDADE =
                                                
                                                 FOS.NUM_FUNC_UNIDADE)),
                                      
                                      0) HOR_APONTADAS
                        
                          FROM (SELECT TOS.NUM_ORDEM,                                       
                                       TOS.NUM_TRIAGEM_OS,                                       
                                       TRF.SEQ_TAREFA_ORDEM_SERVICO,                                       
                                       NVL(TRF.QTD_APF, 0) QTD_APF,                                       
                                       NVL(TRF.QTD_HORAS_PREVISTA, 0) QTD_HORAS_PREVISTA                                
                                  FROM TRIAGEM_ORDEM_SERVICO TOS,                                       
                                       TAREFA_ORDEM_SERVICO TRF                                
                                 WHERE TOS.COD_UNIDADE_EXECUTANTE = 13                                      
                                   AND TOS.DTA_HOR_FIM_TRIAGEM =
                                       TO_DATE('01/01/1800', 'DD/MM/YYYY')                                      
                                   AND TOS.NUM_ORDEM = TRF.NUM_ORDEM(+)                                      
                                   AND TOS.COD_UNIDADE_EXECUTANTE =
                                       TRF.COD_UNIDADE_EXECUTANTE(+)) S1,
                               
                               FUNCIONARIO_ORDEM_SERVICO FOS
                        
                         WHERE S1.NUM_TRIAGEM_OS = FOS.NUM_TRIAGEM_OS(+)
                              
                           AND NVL(S1.SEQ_TAREFA_ORDEM_SERVICO, -1) =
                              
                               NVL(FOS.SEQ_TAREFA_ORDEM_SERVICO(+), -1)
                        
                         GROUP BY S1.NUM_ORDEM,
                                  
                                  S1.NUM_TRIAGEM_OS,
                                  
                                  S1.SEQ_TAREFA_ORDEM_SERVICO,
                                  
                                  S1.QTD_APF,
                                  
                                  S1.QTD_HORAS_PREVISTA,
                                  
                                  FOS.SEQ_FUNC_ORDEM_SERVICO,
                                  
                                  FOS.DTA_HOR_FINALIZACAO) S5
                
                 GROUP BY NUM_ORDEM,
                          
                          NUM_TRIAGEM_OS,
                          
                          SEQ_TAREFA_ORDEM_SERVICO,
                          
                          QTD_APF,
                          
                          QTD_HORAS_PREVISTA) S3
        
         GROUP BY S3.NUM_ORDEM, S3.NUM_TRIAGEM_OS) S4,
       
       ORDEM_SERVICO OS,
       
       CENTRO_CUSTO CC,
       
       TRIAGEM_ORDEM_SERVICO TROS,
       
       TIPO_SITUACAO_OS TS,
       
       SISTEMA S

 WHERE S4.NUM_ORDEM = OS.NUM_ORDEM
      
   AND OS.COD_CENCUSTO = CC.COD_CENCUSTO
      
   AND S4.NUM_TRIAGEM_OS = TROS.NUM_TRIAGEM_OS
      
   AND OS.COD_SITUACAO_OS = TS.COD_SITUACAO_OS;
