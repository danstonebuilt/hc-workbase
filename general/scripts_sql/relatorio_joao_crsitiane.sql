/*Agrupado*/
/*---------------------------------------------------*/
SELECT          nvl(LIC.NUM_LICITACAO||'/'||LIC.ANO_LICITACAO, '') LICITACAO,
                nvl(LIC.NUM_DOCUMENTO||'/'||LIC.ANO_DOCUMENTO, '') PROCESSO,
                B.OBS_AUTORIZACAO CONTRATO,
                decode(AC.IDF_DIRECIONAMENTO ,1, 'ATA DA SERCRETADIA DE SAÚDE',
                                       2, 'DISPENSA',
                                       4, 'LICITAÇÃO',
                                       5, 'REGISTRO DE PREÇOS',
                                       6, 'COMPRA DEPARTAMENTOS',
                                       7, 'IMPORTACAO',
                                       8, 'ENGENHARIA',
                                       9,  'CEMB',
                                       15, 'COMPRA DIRETA GERAL',
                                       16, 'CIA',
                                       20, 'FAT.SERVICOS FAEPA') DIRECIONAMENTO,
                D.NUM_PEDIDO_COMPRA||'/'||D.Ano_Pedido_Compra PEDIDO_COMPRA,                
                sum(L.QTD_ITEM) QTD_ITEM,
                to_char(SUM( B.VLR_MATERIAL), 'FM999G999G990D90') VLR_TOTAL_CONSUMIDO,                           
                F.COD_MATERIAL,
                M.NOM_MATERIAL,
                DECODE(extract(MONTH FROM H.DTA_UTILIZACAO),
                                1, 'JANEIRO',
                                2, 'FEVEREIRO',                                
                                3, 'MARÇO',
                                4, 'ABRIL',
                                5, 'MAIO',
                                6, 'JUNHO',
                                7, 'JULHO',
                                8, 'AGOSTO',
                                9, 'SETEMBRO',
                                10, 'OUTUBRO',
                                11, 'NOVEMBRO',
                                12, 'DEZEMBRO')  MES_CONSUMO ,
                FORN.COD_FORNECEDOR || ' - ' || FORN.NOM_FORNECEDOR FORNECEDOR,
                LISTAGG(H.COD_PACIENTE||' - '||J.NOM_PACIENTE || ' ' || J.SBN_PACIENTE||' - '|| ML.NOM_LOCAL||' - '||
                DECODE(H.IDF_ORIGEM, '0', 'SOLICITACAO AVULSA',  '1',
                      (SELECT '(C3) ' || E.NOM_ESPECIALIDADE_HC
                         FROM AGENDA_CIRURGIA AC, ESPECIALIDADE_HC E
                        WHERE AC.SEQ_AGENDA_CIRURGIA = H.NUM_ORIGEM
                          AND AC.COD_ESPECIALIDADE_HC = E.COD_ESPECIALIDADE_HC),'3', 'EXAME', '')||', '|| H.DTA_UTILIZACAO||' - '||
                             CASE
                               WHEN H.IDF_ORIGEM = '1' THEN
                                (SELECT C.NOM_CONVENIO
                                   FROM AGENDA_CIRURGIA AC, CONVENIO C
                                  WHERE AC.SEQ_AGENDA_CIRURGIA = H.NUM_ORIGEM
                                    AND AC.COD_CONVENIO = C.COD_CONVENIO
                                    AND ROWNUM = 1) -- Avaliar
                               ELSE
                                ''
                             END,                             
                                         
                ','||CHR(10)) WITHIN GROUP(ORDER BY H.DTA_UTILIZACAO) PACIENTES               
          FROM COMPLEMENTO_REQUISICAO G,
               PACIENTE J,
               PEDIDO_COMPRA D,
               SUGESTAO_COMPRA A,
               (SELECT F.NUM_REQUISICAO,
                       F.ANO_REQUISICAO_MATERIAL,
                       F.COD_MATERIAL,
                       F.NUM_SUGESTAO_COMPRA,
                       SUM(F.QTD_FORNE_RECEBIDA) QTD_FORNE_RECEBIDA
                  FROM ITEM_REQUISICAO_CONSIGNACAO F
                 GROUP BY F.NUM_REQUISICAO,
                          F.ANO_REQUISICAO_MATERIAL,
                          F.COD_MATERIAL,
                          F.NUM_SUGESTAO_COMPRA) F, --Avaliar
               AUTORIZACAO_COMPRA_MATERIAL B,
               SOLICITACAO_INSUMO H,
               SOLICITACAO_INSUMO_ITEM L,
               REQUISICAO_MATERIAL R,
               LICITACAO LIC,
               MATERIAL M,
               LOTE LT,
               FORNECEDOR FORN,
               MAPEAMENTO_LOCAL ML,
               AGRUPAMENTO_COMPRA AC
         WHERE G.IDF_TIPO_DOCUMENTO = 21
           AND A.NUM_AUTORIZACAO_COMPRA = B.NUM_AUTORIZACAO_COMPRA
           AND B.COD_TIPO_AUTORIZACAO = 4
           AND B.NUM_PEDIDO_COMPRA = D.NUM_PEDIDO_COMPRA(+)
           AND B.ANO_PEDIDO_COMPRA = D.ANO_PEDIDO_COMPRA(+)
            AND ac.num_agrupamento = D.Num_Agrupamento
           AND A.NUM_SUGESTAO_COMPRA = F.NUM_SUGESTAO_COMPRA
           AND F.ANO_REQUISICAO_MATERIAL = G.ANO_REQUISICAO_MATERIAL
           AND F.NUM_REQUISICAO = G.NUM_REQUISICAO
           AND F.NUM_REQUISICAO = R.NUM_REQUISICAO
           AND F.ANO_REQUISICAO_MATERIAL = R.ANO_REQUISICAO_MATERIAL
           AND R.IDF_TIPO_REQUISICAO = 0
           AND NOT EXISTS
             (SELECT 1
                      FROM REQUISICAO_MATERIAL RM
                     WHERE RM.NUM_REQUISICAO = R.NUM_REQ_ORIGEM_DEVOLUCAO
                       AND RM.ANO_REQUISICAO_MATERIAL =  R.ANO_REQ_ORIGEM_DEVOLUCAO) --avaliar
           AND G.NUM_REQUISICAO = L.NUM_REQUISICAO
           AND G.ANO_REQUISICAO_MATERIAL = L.ANO_REQUISICAO_MATERIAL
           AND L.SEQ_SOLICITACAO_INSUMO = H.SEQ_SOLICITACAO_INSUMO
           AND D.NUM_AGRUPAMENTO = LIC.NUM_AGRUPAMENTO(+)
           AND B.COD_FORNECEDOR = FORN.COD_FORNECEDOR
           AND F.COD_MATERIAL = M.COD_MATERIAL
           AND L.NUM_LOTE = LT.NUM_LOTE(+)          
           AND H.NUM_SEQ_LOCAL_SOLICITANTE = ML.NUM_SEQ_LOCAL           
           AND F.COD_MATERIAL = L.COD_MATERIAL_REQUISICAO
           AND H.COD_PACIENTE = J.COD_PACIENTE(+)
           AND A.IDF_ESTAGIO_SUGESTAO_EMPENHO = '9'
           --AND D.Num_Agrupamento = 267043
           AND extract(YEAR FROM H.DTA_UTILIZACAO) = 2017 --BETWEEN TO_DATE('01/01/2017', 'DD/MM/YYYY') AND  TO_DATE(SYSDATE, 'DD/MM/YYYY')
          -- AND D.Num_Pedido_Compra = 29867
          --AND D.Ano_Pedido_Compra = 2016
           GROUP BY extract(MONTH FROM H.DTA_UTILIZACAO),
                  F.COD_MATERIAL,
                   M.NOM_MATERIAL,
                   FORN.COD_FORNECEDOR,
                   FORN.NOM_FORNECEDOR,
                   B.OBS_AUTORIZACAO,
                   D.NUM_PEDIDO_COMPRA,
                   D.Ano_Pedido_Compra,
                   LIC.NUM_LICITACAO,LIC.ANO_LICITACAO,
                   LIC.NUM_DOCUMENTO,LIC.ANO_DOCUMENTO,
                   ac.idf_direcionamento;
/*Não Agrupado*/
/*---------------------------------------------------*/  
SELECT          LIC.NUM_LICITACAO LICITACAO,
                LIC.ANO_LICITACAO,
                LIC.NUM_DOCUMENTO PROCESSO, LIC.ANO_DOCUMENTO,
                B.OBS_AUTORIZACAO CONTRATO,
                decode(AC.IDF_DIRECIONAMENTO ,1, 'ATA DA SERCRETADIA DE SAÚDE',
                                       2, 'DISPENSA',
                                       4, 'LICITAÇÃO',
                                       5, 'REGISTRO DE PREÇOS',
                                       6, 'COMPRA DEPARTAMENTOS',
                                       7, 'IMPORTACAO',
                                       8, 'ENGENHARIA',
                                       9,  'CEMB',
                                       15, 'COMPRA DIRETA GERAL',
                                       16, 'CIA',
                                       20, 'FAT.SERVICOS FAEPA') DIRECIONAMENTO,
                D.NUM_PEDIDO_COMPRA||'/'||D.Ano_Pedido_Compra PEDIDO_COMPRA,                
                L.QTD_ITEM QTD_ITEM,
                to_char( B.VLR_MATERIAL, 'FM999G999G990D90') VLR_TOTAL_CONSUMIDO,                           
                F.COD_MATERIAL,
                M.NOM_MATERIAL,
                DECODE(extract(MONTH FROM H.DTA_UTILIZACAO),
                                1, 'JANEIRO',
                                2, 'FEVEREIRO',                                
                                3, 'MARÇO',
                                4, 'ABRIL',
                                5, 'MAIO',
                                6, 'JUNHO',
                                7, 'JULHO',
                                8, 'AGOSTO',
                                9, 'SETEMBRO',
                                10, 'OUTUBRO',
                                11, 'NOVEMBRO',
                                12, 'DEZEMBRO')  MES_CONSUMO ,
                FORN.COD_FORNECEDOR || ' - ' || FORN.NOM_FORNECEDOR FORNECEDOR,
                H.COD_PACIENTE,J.NOM_PACIENTE,J.SBN_PACIENTE, ML.NOM_LOCAL,
                
                DECODE(H.IDF_ORIGEM, '0', 'SOLICITACAO AVULSA',  '1',
                      (SELECT '(C3) ' || E.NOM_ESPECIALIDADE_HC
                         FROM AGENDA_CIRURGIA AC, ESPECIALIDADE_HC E
                        WHERE AC.SEQ_AGENDA_CIRURGIA = H.NUM_ORIGEM
                          AND AC.COD_ESPECIALIDADE_HC = E.COD_ESPECIALIDADE_HC),'3', 'EXAME', '') exame,
                           H.DTA_UTILIZACAO,
                          
                             CASE
                               WHEN H.IDF_ORIGEM = '1' THEN
                                (SELECT C.NOM_CONVENIO
                                   FROM AGENDA_CIRURGIA AC, CONVENIO C
                                  WHERE AC.SEQ_AGENDA_CIRURGIA = H.NUM_ORIGEM
                                    AND AC.COD_CONVENIO = C.COD_CONVENIO
                                    AND ROWNUM = 1) -- Avaliar
                               ELSE
                                ''
                             END convenio                             
                                         
                            
          FROM COMPLEMENTO_REQUISICAO G,
               PACIENTE J,
               PEDIDO_COMPRA D,
               SUGESTAO_COMPRA A,
               (SELECT F.NUM_REQUISICAO,
                       F.ANO_REQUISICAO_MATERIAL,
                       F.COD_MATERIAL,
                       F.NUM_SUGESTAO_COMPRA,
                       SUM(F.QTD_FORNE_RECEBIDA) QTD_FORNE_RECEBIDA
                  FROM ITEM_REQUISICAO_CONSIGNACAO F
                 GROUP BY F.NUM_REQUISICAO,
                          F.ANO_REQUISICAO_MATERIAL,
                          F.COD_MATERIAL,
                          F.NUM_SUGESTAO_COMPRA) F, --Avaliar
               AUTORIZACAO_COMPRA_MATERIAL B,
               SOLICITACAO_INSUMO H,
               SOLICITACAO_INSUMO_ITEM L,
               REQUISICAO_MATERIAL R,
               LICITACAO LIC,
               MATERIAL M,
               LOTE LT,
               FORNECEDOR FORN,
               MAPEAMENTO_LOCAL ML,
               AGRUPAMENTO_COMPRA AC
         WHERE G.IDF_TIPO_DOCUMENTO = 21
           AND A.NUM_AUTORIZACAO_COMPRA = B.NUM_AUTORIZACAO_COMPRA
           AND B.COD_TIPO_AUTORIZACAO = 4         
           AND B.NUM_PEDIDO_COMPRA = D.NUM_PEDIDO_COMPRA(+)
           AND B.ANO_PEDIDO_COMPRA = D.ANO_PEDIDO_COMPRA(+)
            AND ac.num_agrupamento = D.Num_Agrupamento
           AND A.NUM_SUGESTAO_COMPRA = F.NUM_SUGESTAO_COMPRA
           AND F.ANO_REQUISICAO_MATERIAL = G.ANO_REQUISICAO_MATERIAL
           AND F.NUM_REQUISICAO = G.NUM_REQUISICAO
           AND F.NUM_REQUISICAO = R.NUM_REQUISICAO
           AND F.ANO_REQUISICAO_MATERIAL = R.ANO_REQUISICAO_MATERIAL
           AND R.IDF_TIPO_REQUISICAO = 0
           AND NOT EXISTS
             (SELECT 1
                      FROM REQUISICAO_MATERIAL RM
                     WHERE RM.NUM_REQUISICAO = R.NUM_REQ_ORIGEM_DEVOLUCAO
                       AND RM.ANO_REQUISICAO_MATERIAL =  R.ANO_REQ_ORIGEM_DEVOLUCAO) --avaliar
           AND G.NUM_REQUISICAO = L.NUM_REQUISICAO
           AND G.ANO_REQUISICAO_MATERIAL = L.ANO_REQUISICAO_MATERIAL
           AND L.SEQ_SOLICITACAO_INSUMO = H.SEQ_SOLICITACAO_INSUMO
           AND D.NUM_AGRUPAMENTO = LIC.NUM_AGRUPAMENTO(+)
           AND B.COD_FORNECEDOR = FORN.COD_FORNECEDOR
           AND F.COD_MATERIAL = M.COD_MATERIAL
           AND L.NUM_LOTE = LT.NUM_LOTE(+)          
           AND H.NUM_SEQ_LOCAL_SOLICITANTE = ML.NUM_SEQ_LOCAL           
           AND F.COD_MATERIAL = L.COD_MATERIAL_REQUISICAO
           AND H.COD_PACIENTE = J.COD_PACIENTE(+)
           AND A.IDF_ESTAGIO_SUGESTAO_EMPENHO = '9'
          -- AND D.Num_Agrupamento = 267043
           AND extract(YEAR FROM H.DTA_UTILIZACAO) = 2017 --BETWEEN TO_DATE('01/01/2017', 'DD/MM/YYYY') AND  TO_DATE(SYSDATE, 'DD/MM/YYYY')
           ORDER BY H.DTA_UTILIZACAO;
          -- AND D.Num_Pedido_Compra = 29867
          --AND D.Ano_Pedido_Compra = 2016
          
