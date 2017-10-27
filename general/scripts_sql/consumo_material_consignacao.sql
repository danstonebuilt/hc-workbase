SELECT DECODE(A.IDF_ESTAGIO_SUGESTAO_EMPENHO,
                            '1',
                            'Analise do PEDIDO DE EMPENHO pelo GAC',
                            '2',
                            'Direcionamento do PE (Verificar se não existe preço menor em alguma outra forma de pagamento',
                            '3',
                            'Oneração do empenho',
                            '4',
                            'A Faturar',
                            '5',
                            'Sugestão devolvida para emissor',
                            '8',
                            'Cancelado',
                            '9',
                            'Faturado (' ||
                            (SELECT IAE.NUM_AUTORIZACAO_ENTREGA || '/' ||
                                    IAE.ANO_AUTORIZACAO_ENTREGA
                               FROM ITENS_AUTORIZACAO_ENTREGA IAE
                              WHERE IAE.NUM_PEDIDO_COMPRA = D.NUM_PEDIDO_COMPRA
                                AND IAE.ANO_PEDIDO_COMPRA = D.ANO_PEDIDO_COMPRA
                                AND IAE.NUM_SUGESTAO_COMPRA = A.NUM_SUGESTAO_COMPRA
                                AND ROWNUM = 1) || ')',
                            '?')                       SITUACAO, --Avaliar
                                                       L.QTD_ITEM,
                                                       B.VLR_MATERIAL,
                                                       L.QTD_ITEM * B.VLR_MATERIAL VLR_TOTAL,
                                                       F.COD_MATERIAL,
                                                       LT.NUM_LOTE_FABRICANTE,
                                                       M.NOM_MATERIAL,
                                                       H.COD_PACIENTE,
                                                       H.DTA_UTILIZACAO DTA_CIRURGIA,
                                                       J.NOM_PACIENTE || ' ' || J.SBN_PACIENTE NOME_PACIENTE,
               DECODE(H.IDF_ORIGEM, '0', 'SOLICITACAO AVULSA',  '1',
                      (SELECT '(C3) ' || E.NOM_ESPECIALIDADE_HC
                         FROM AGENDA_CIRURGIA AC, ESPECIALIDADE_HC E
                        WHERE AC.SEQ_AGENDA_CIRURGIA = H.NUM_ORIGEM
                          AND AC.COD_ESPECIALIDADE_HC = E.COD_ESPECIALIDADE_HC),'3', 'EXAME', '') NOM_ESPECIALIDADE, --Avaliar
                                               FORN.COD_FORNECEDOR || ' - ' || FORN.NOM_FORNECEDOR FORNECEDOR,
                                                                         ML.NOM_LOCAL LOCAL_UTILIZACAO,
               CASE
                 WHEN H.IDF_ORIGEM = '1' THEN
                  (SELECT C.NOM_CONVENIO
                     FROM AGENDA_CIRURGIA AC, CONVENIO C
                    WHERE AC.SEQ_AGENDA_CIRURGIA = H.NUM_ORIGEM
                      AND AC.COD_CONVENIO = C.COD_CONVENIO
                      AND ROWNUM = 1) -- Avaliar
                 ELSE
                  ''
               END CONVENIO
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
               MAPEAMENTO_LOCAL ML
         WHERE G.IDF_TIPO_DOCUMENTO = 21
           AND A.NUM_AUTORIZACAO_COMPRA = B.NUM_AUTORIZACAO_COMPRA
           AND B.COD_TIPO_AUTORIZACAO = 4
           AND B.NUM_PEDIDO_COMPRA = D.NUM_PEDIDO_COMPRA(+)
           AND B.ANO_PEDIDO_COMPRA = D.ANO_PEDIDO_COMPRA(+)
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
           AND H.DTA_UTILIZACAO BETWEEN TO_DATE('01/01/2017', 'DD/MM/YYYY') AND  TO_DATE('24/10/2017', 'DD/MM/YYYY')
           AND F.COD_MATERIAL = L.COD_MATERIAL_REQUISICAO
           AND H.COD_PACIENTE = J.COD_PACIENTE(+)
           ORDER BY H.DTA_UTILIZACAO DESC;

 
