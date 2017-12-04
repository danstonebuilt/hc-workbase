SELECT *
  FROM (SELECT X.*,
               E.NOM_ESPECIALIDADE_HC,
               (SELECT GENERICO.FCN_LINHA_COLUNA('SELECT C.NOM_PROCEDIMENTO_HC FROM AGENDA_CIRURGIA A, AGENDA_PROCEDIMENTO_HC B, PROCEDIMENTO_HC C
                WHERE A.SEQ_AGENDA_CIRURGIA = B.SEQ_AGENDA_CIRURGIA
                AND B.COD_PROCEDIMENTO_HC = C.COD_PROCEDIMENTO_HC
                AND A.SEQ_AGENDA_CIRURGIA = ' || AC.SEQ_AGENDA_CIRURGIA || '', ', ')
                  FROM DUAL) NOME_PROCEDIMENTO,
               C.NOM_CONVENIO
          FROM (SELECT S.SEQ_SOLICITACAO_INSUMO_ITEM,
                       SOL.DTA_UTILIZACAO,
                       SOL.COD_PACIENTE,
                       M.COD_MATERIAL,
                       M.NOM_MATERIAL,
                       S.QTD_ITEM,
                       SOL.IDF_ORIGEM,
                       SOL.NUM_ORIGEM,
                       SOL.COD_PROCEDIMENTO_HC,
                       ML.NOM_LOCAL LOCAL_UTILIZACAO,
                       ACM.VLR_MATERIAL
                  FROM SOLICITACAO_INSUMO_ITEM S,
                       (SELECT F.NUM_REQUISICAO,
                               F.ANO_REQUISICAO_MATERIAL,
                               F.COD_MATERIAL,
                               F.NUM_SUGESTAO_COMPRA,
                               SUM(F.QTD_FORNE_RECEBIDA) QTD_FORNE_RECEBIDA
                          FROM ITEM_REQUISICAO_CONSIGNACAO F
                         GROUP BY F.NUM_REQUISICAO,
                                  F.ANO_REQUISICAO_MATERIAL,
                                  F.COD_MATERIAL,
                                  F.NUM_SUGESTAO_COMPRA) IRC,
                       SUGESTAO_COMPRA SC,
                       AUTORIZACAO_COMPRA_MATERIAL ACM,
                       MATERIAL M,
                       SOLICITACAO_INSUMO SOL,
                       MAPEAMENTO_LOCAL ML
                 WHERE S.NUM_REQUISICAO = IRC.NUM_REQUISICAO
                   AND S.ANO_REQUISICAO_MATERIAL = IRC.ANO_REQUISICAO_MATERIAL
                   AND IRC.NUM_SUGESTAO_COMPRA = SC.NUM_SUGESTAO_COMPRA
                   AND SC.NUM_AUTORIZACAO_COMPRA = ACM.NUM_AUTORIZACAO_COMPRA
                   AND SOL.SEQ_SOLICITACAO_INSUMO = S.SEQ_SOLICITACAO_INSUMO
                   AND ACM.COD_MATERIAL = M.COD_MATERIAL
                   AND SOL.NUM_SEQ_LOCAL_SOLICITANTE = ML.NUM_SEQ_LOCAL
                
                --   AND SOL.SEQ_SOLICITACAO_INSUMO = 20954
                ) X,
               AGENDA_CIRURGIA AC,
               CONVENIO C,
               ESPECIALIDADE_HC E
         WHERE X.IDF_ORIGEM = 1
           AND AC.COD_ESPECIALIDADE_HC = E.COD_ESPECIALIDADE_HC
           AND AC.SEQ_AGENDA_CIRURGIA = X.NUM_ORIGEM
           AND AC.COD_CONVENIO = C.COD_CONVENIO
        UNION ALL
        SELECT X.*,
               'SOLICITACAO AVULSA' NOM_ESPECIALIDADE_HC,
               PH.NOM_PROCEDIMENTO_HC NOME_PROCEDIMENTO,
               'SOLICITACAO AVULSA' CONVENIO
          FROM (SELECT S.SEQ_SOLICITACAO_INSUMO_ITEM,
                       SOL.DTA_UTILIZACAO,
                       SOL.COD_PACIENTE,
                       M.COD_MATERIAL,
                       M.NOM_MATERIAL,
                       S.QTD_ITEM,
                       SOL.IDF_ORIGEM,
                       SOL.NUM_ORIGEM,
                       SOL.COD_PROCEDIMENTO_HC,
                       ML.NOM_LOCAL LOCAL_UTILIZACAO,
                       ACM.VLR_MATERIAL
                  FROM SOLICITACAO_INSUMO_ITEM S,
                       (SELECT F.NUM_REQUISICAO,
                               F.ANO_REQUISICAO_MATERIAL,
                               F.COD_MATERIAL,
                               F.NUM_SUGESTAO_COMPRA,
                               SUM(F.QTD_FORNE_RECEBIDA) QTD_FORNE_RECEBIDA
                          FROM ITEM_REQUISICAO_CONSIGNACAO F
                         GROUP BY F.NUM_REQUISICAO,
                                  F.ANO_REQUISICAO_MATERIAL,
                                  F.COD_MATERIAL,
                                  F.NUM_SUGESTAO_COMPRA) IRC,
                       SUGESTAO_COMPRA SC,
                       AUTORIZACAO_COMPRA_MATERIAL ACM,
                       MATERIAL M,
                       SOLICITACAO_INSUMO SOL,
                       MAPEAMENTO_LOCAL ML
                 WHERE S.NUM_REQUISICAO = IRC.NUM_REQUISICAO
                   AND S.ANO_REQUISICAO_MATERIAL = IRC.ANO_REQUISICAO_MATERIAL
                   AND IRC.NUM_SUGESTAO_COMPRA = SC.NUM_SUGESTAO_COMPRA
                   AND SC.NUM_AUTORIZACAO_COMPRA = ACM.NUM_AUTORIZACAO_COMPRA
                   AND SOL.SEQ_SOLICITACAO_INSUMO = S.SEQ_SOLICITACAO_INSUMO
                   AND ACM.COD_MATERIAL = M.COD_MATERIAL
                   AND SOL.NUM_SEQ_LOCAL_SOLICITANTE = ML.NUM_SEQ_LOCAL) X,
               PROCEDIMENTO_HC PH
         WHERE X.IDF_ORIGEM IN (0, 3)
           AND X.COD_PROCEDIMENTO_HC = PH.COD_PROCEDIMENTO_HC
        UNION ALL
        SELECT X.*,
               'CARDIOLOGIA HEMODINAMICA' NOM_ESPECIALIDADE_HC,
               PH.NOM_PROCEDIMENTO_HC NOME_PROCEDIMENTO,
               C.NOM_CONVENIO
          FROM (SELECT S.SEQ_SOLICITACAO_INSUMO_ITEM,
                       SOL.DTA_UTILIZACAO,
                       SOL.COD_PACIENTE,
                       M.COD_MATERIAL,
                       M.NOM_MATERIAL,
                       S.QTD_ITEM,
                       SOL.IDF_ORIGEM,
                       SOL.NUM_ORIGEM,
                       SOL.COD_PROCEDIMENTO_HC,
                       ML.NOM_LOCAL LOCAL_UTILIZACAO,
                       ACM.VLR_MATERIAL
                  FROM SOLICITACAO_INSUMO_ITEM S,
                       (SELECT F.NUM_REQUISICAO,
                               F.ANO_REQUISICAO_MATERIAL,
                               F.COD_MATERIAL,
                               F.NUM_SUGESTAO_COMPRA,
                               SUM(F.QTD_FORNE_RECEBIDA) QTD_FORNE_RECEBIDA
                          FROM ITEM_REQUISICAO_CONSIGNACAO F
                         GROUP BY F.NUM_REQUISICAO,
                                  F.ANO_REQUISICAO_MATERIAL,
                                  F.COD_MATERIAL,
                                  F.NUM_SUGESTAO_COMPRA) IRC,
                       SUGESTAO_COMPRA SC,
                       AUTORIZACAO_COMPRA_MATERIAL ACM,
                       MATERIAL M,
                       SOLICITACAO_INSUMO SOL,
                       MAPEAMENTO_LOCAL ML
                 WHERE S.NUM_REQUISICAO = IRC.NUM_REQUISICAO
                   AND S.ANO_REQUISICAO_MATERIAL = IRC.ANO_REQUISICAO_MATERIAL
                   AND IRC.NUM_SUGESTAO_COMPRA = SC.NUM_SUGESTAO_COMPRA
                   AND SC.NUM_AUTORIZACAO_COMPRA = ACM.NUM_AUTORIZACAO_COMPRA
                   AND SOL.SEQ_SOLICITACAO_INSUMO = S.SEQ_SOLICITACAO_INSUMO
                   AND ACM.COD_MATERIAL = M.COD_MATERIAL
                   AND SOL.NUM_SEQ_LOCAL_SOLICITANTE = ML.NUM_SEQ_LOCAL) X,
               AGENDA_EXAME_COMPLEMENTAR A,
               PROCEDIMENTO_HC PH,
               MAPEAMENTO_LOCAL_PROCEDIMENTO D,
               CONVENIO C
         WHERE X.IDF_ORIGEM = 4
           AND A.NUM_AGENDA_EXAME = X.NUM_ORIGEM
           AND D.COD_PROCEDIMENTO_HC = PH.COD_PROCEDIMENTO_HC
           AND D.COD_PROCEDIMENTO_HC = A.COD_PROCEDIMENTO_HC
           AND A.COD_CONVENIO = C.COD_CONVENIO) Z
 WHERE EXTRACT(YEAR FROM Z.DTA_UTILIZACAO) = 2017;
