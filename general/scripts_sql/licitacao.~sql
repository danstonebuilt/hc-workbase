  --*********Fluxo*****************
  SELECT * FROM licitacao l
                 WHERE 1 = 1                 
                 AND l.ano_licitacao = 2017
                 AND l.cod_instituicao IN (1, 2)
                 ORDER BY 1 DESC;
 --Etp 01
 select * from ESTOQUE.AGRUPAMENTO_COMPRA t
where t.num_agrupamento = 297196;

--Etp 02
select * from ESTOQUE.PEDIDO_COMPRA t
WHERE 1 = 1
--AND t.num_pedido_compra = 13115
AND t.num_agrupamento = 282394
AND t.ano_pedido_compra = 2017;


--Etp 03
SELECT   *
         FROM PRECO_FORNECEDOR_PEDIDO PFP
  WHERE 1 = 1
  AND pfp.num_pedido_compra = 11710
  AND PFP.ANO_PEDIDO_COMPRA = 2017;
  
SELECT * FROM material m
WHERE m.cod_material = '90051142';

SELECT F.COD_FORNECEDOR,
       F.NOM_FORNECEDOR,
       m.nom_material,
       PFP.*
  FROM PRECO_FORNECEDOR_PEDIDO PFP
  JOIN FORNECEDOR F
    ON PFP.COD_FORNECEDOR = F.COD_FORNECEDOR
  JOIN material m
    ON pfp.cod_material = m.cod_material
 WHERE 1 = 1
   AND PFP.NUM_PEDIDO_COMPRA IN
       (5943,5944,5945,5946,7682)
   AND PFP.ANO_PEDIDO_COMPRA = 2017
   AND PFP.IDF_CLASSIFICACAO = '1';

--Etp consultar pedidos de compras vencedores?
SELECT COUNT(*) qtd, pc.num_agrupamento
  FROM PEDIDO_COMPRA PC
 WHERE PC.NUM_PEDIDO_COMPRA IN (SELECT PFP.NUM_PEDIDO_COMPRA
                                  FROM PRECO_FORNECEDOR_PEDIDO PFP
                                 WHERE PFP.ANO_PEDIDO_COMPRA = 2017
                                   AND PFP.IDF_CLASSIFICACAO = '1'
                                   AND ROWNUM <= 500 )
   AND PC.COD_INST_SISTEMA IN (1, 2)
   AND pc.ano_pedido_compra = 2017
   GROUP BY pc.num_agrupamento
   HAVING COUNT(*) > 1
   ORDER BY qtd DESC;
   
--*****Pedido de compras ganhadores agrupado por fornecedores 
SELECT pfp.cod_fornecedor,
       f.nom_fornecedor,      
       COUNT(pfp.num_pedido_compra) qtd_pedi_fornecedor      
  FROM PRECO_FORNECEDOR_PEDIDO PFP 
     JOIN fornecedor f
       ON pfp.cod_fornecedor = f.cod_fornecedor    
 WHERE PFP.ANO_PEDIDO_COMPRA = 2017
   AND PFP.IDF_CLASSIFICACAO = '1'
   AND ROWNUM <= 500
   GROUP BY pfp.cod_fornecedor, f.nom_fornecedor;
           
   SELECT  f.cod_fornecedor,
           f.nom_fornecedor,
           pfp.* FROM 
         PRECO_FORNECEDOR_PEDIDO pfp
         JOIN fornecedor f
       ON pfp.cod_fornecedor = f.cod_fornecedor
   WHERE pfp.cod_fornecedor = 1861
    AND PFP.ANO_PEDIDO_COMPRA = 2017
   AND PFP.IDF_CLASSIFICACAO = '1'
   AND ROWNUM <= 500 ;
   
   

--*******Tela Licitação Gestão*******
SELECT B.Num_Licitacao,
       A.NUM_AGRUPAMENTO,
       D.NOM_FANTASIA,
       C.DSC_MODALIDADE_LICITACAO,
       A.DTA_HOR_AGRUPAMENTO,
       B.COD_INSTITUICAO,
       A.OBS_COMPRA,
       DECODE(A.IDF_NACIONAL_IMPORTACAO, '0', 'NACIONAL', '1', 'IMPORTACAO') TIPO,
       DECODE(A.IDF_NORMAL_URGENTE, 'N', 'NORMAL', 'U', 'URGENTE') PRIORIDADE,
       DECODE(B.NUM_LICITACAO,
              NULL,
              NULL,
              B.NUM_LICITACAO || '/' || B.ANO_LICITACAO) NUM_LICITACAO,
       DECODE(B.NUM_DOCUMENTO,
              NULL,
              NULL,
              B.NUM_DOCUMENTO || '/' || B.ANO_DOCUMENTO) NUM_PROCESSO,
       A.DSC_OBJETO,
       COUNT(E.NUM_PEDIDO_COMPRA) QTD_ITENS,
       SUM(VLR_UNITARIO * QTD_APROVADA) VLR_ESTIMADO
  FROM AGRUPAMENTO_COMPRA   A,
       LICITACAO            B,
       MODALIDADE_LICITACAO C,
       INSTITUICAO          D,
       PEDIDO_COMPRA        E
 WHERE A.IDF_ESTAGIO_AGRUPAMENTO = '1'
   AND A.IDF_DIRECIONAMENTO = 4
   AND A.NUM_AGRUPAMENTO = B.NUM_AGRUPAMENTO
   AND B.COD_MODALIDADE_LICITACAO = C.COD_MODALIDADE_LICITACAO
   AND B.COD_INSTITUICAO = D.COD_INSTITUICAO
   AND A.NUM_AGRUPAMENTO = E.NUM_AGRUPAMENTO
 GROUP BY A.NUM_AGRUPAMENTO,
          D.NOM_FANTASIA,
          C.DSC_MODALIDADE_LICITACAO,
          A.DTA_HOR_AGRUPAMENTO,
          B.COD_INSTITUICAO,
          B.NUM_LICITACAO,
          B.ANO_LICITACAO,
          A.OBS_COMPRA,
          A.IDF_NACIONAL_IMPORTACAO,
          A.IDF_NORMAL_URGENTE,
          B.NUM_DOCUMENTO,
          B.ANO_DOCUMENTO,
          A.DSC_OBJETO
 ORDER BY PRIORIDADE DESC, A.DTA_HOR_AGRUPAMENTO;
 
 --*****************************
 select * from DOCUMENTO.DOCUMENTO t
where t.cod_especie = 1
and   t.cod_instituicao = 2
and   t.num_documento = 336
and   t.ano_documento = 2017;

 --*********Processo Compras
SELECT q.*
  FROM 
(SELECT A.NUM_AGRUPAMENTO,
       B.NUM_DOCUMENTO || '/' || B.ANO_DOCUMENTO NUMERO_DOC,
       B.NUM_DOCUMENTO NUMERO,
       B.ANO_DOCUMENTO ANO,
       C.DSC_MODALIDADE_LICITACAO || ' Nº ' || B.NUM_LICITACAO || '/' || B.ANO_LICITACAO DESCRICAO
  FROM AGRUPAMENTO_COMPRA A, LICITACAO B, MODALIDADE_LICITACAO C
 WHERE A.IDF_ESTAGIO_AGRUPAMENTO = '3'
   AND A.IDF_DIRECIONAMENTO = '4'
   AND A.NUM_AGRUPAMENTO = B.NUM_AGRUPAMENTO
   AND B.COD_INSTITUICAO = 2
   AND B.COD_MODALIDADE_LICITACAO = C.COD_MODALIDADE_LICITACAO
UNION ALL
SELECT A.NUM_AGRUPAMENTO,
       B.NUM_ORCAMENTO || '/' || B.ANO_ORCAMENTO NUMERO_DOC,
       B.NUM_ORCAMENTO NUMERO,
       B.ANO_ORCAMENTO ANO,
       'DISPENSA Nº ' || B.NUM_ORCAMENTO || '/' || B.ANO_ORCAMENTO DESCRICAO
  FROM AGRUPAMENTO_COMPRA A, DISPENSA B
 WHERE A.IDF_ESTAGIO_AGRUPAMENTO = '3'
   AND A.IDF_DIRECIONAMENTO = '2'
   AND A.NUM_AGRUPAMENTO = B.NUM_AGRUPAMENTO
   AND B.COD_INSTITUICAO = 2   
 ORDER BY ANO, NUMERO) q
 WHERE q.numero = ;

 --*****************************
 SELECT A.NUM_AGRUPAMENTO,
       B.NUM_DOCUMENTO || '/' || B.ANO_DOCUMENTO NUMERO_DOC,
       B.NUM_DOCUMENTO NUMERO,
       B.ANO_DOCUMENTO ANO,
       C.DSC_MODALIDADE_LICITACAO || ' Nº ' || B.NUM_LICITACAO || '/' || B.ANO_LICITACAO DESCRICAO
  FROM AGRUPAMENTO_COMPRA A, LICITACAO B, MODALIDADE_LICITACAO C
 WHERE A.IDF_ESTAGIO_AGRUPAMENTO = '3'
   AND A.IDF_DIRECIONAMENTO = '4'
   AND A.NUM_AGRUPAMENTO = B.NUM_AGRUPAMENTO
   AND B.COD_INSTITUICAO = 2
   AND B.COD_MODALIDADE_LICITACAO = C.COD_MODALIDADE_LICITACAO
   AND b.num_documento = 3487;
 --*****************************

--*******Exportar para DOE
 (SELECT AC.NUM_AGRUPAMENTO, ML.DSC_MODALIDADE_LICITACAO, 
                     L.NUM_LICITACAO||'/'||L.ANO_LICITACAO NUM_LICITACAO, 
                      L.NUM_DOCUMENTO||'/'||L.ANO_DOCUMENTO NUM_PROCESSO, 
                       PC.NUM_SEQ_AGRUPAMENTO ITEM, NVL(US.DSC_UNIDADE,U.NOM_ABREVIADO) UNIDADE, 
                       ROUND(PFP.VLR_MATERIAL*NVL(SM.FAT_CONVERSAO,1),6) VLR_UNITARIO, 
                       F.NOM_FORNECEDOR, PC.DSC_MATERIAL,   M.NOM_MATERIAL, L.DTA_HOMOLOGACAO, 
                       L.DTA_PUBLICACAO 
                FROM AGRUPAMENTO_COMPRA AC, LICITACAO L , MODALIDADE_LICITACAO ML, 
                     PEDIDO_COMPRA PC, PRECO_FORNECEDOR_PEDIDO PFP, FORNECEDOR F, 
                     MATERIAL M, COMPLEMENTO_MATERIAL CM, 
                     UNIDADE U, SIAFISICO_MATERIAL SM, UNIDADE_SIAFISICO US 
                WHERE AC.NUM_AGRUPAMENTO=L.NUM_AGRUPAMENTO 
                  AND L.COD_MODALIDADE_LICITACAO=ML.COD_MODALIDADE_LICITACAO 
                  AND AC.NUM_AGRUPAMENTO=PC.NUM_AGRUPAMENTO 
                  AND AC.NUM_AGRUPAMENTO= &numAgrupamento 
                  AND PC.NUM_PEDIDO_COMPRA=PFP.NUM_PEDIDO_COMPRA 
                  AND PC.ANO_PEDIDO_COMPRA=PFP.ANO_PEDIDO_COMPRA 
                  AND (TRIM(PFP.IDF_CLASSIFICACAO) ='1' OR PFP.IDF_CLASSIFICACAO ='01') 
                  AND PFP.COD_FORNECEDOR=F.COD_FORNECEDOR 
                  AND PC.COD_MATERIAL=M.COD_MATERIAL 
                  AND M.COD_MATERIAL=CM.COD_MATERIAL(+) 
                  AND M.COD_UNIDADE=U.COD_UNIDADE 
                  AND PC.COD_MATERIAL=SM.COD_MATERIAL(+) 
                  AND PC.COD_SIAFISICO=SM.COD_SIAFISICO(+) 
                  AND SM.COD_UNIDADE_SIAFISICO=US.COD_UNIDADE_SIAFISICO(+) 
                                )  
                  UNION ALL   
                (         
                  SELECT AC.NUM_AGRUPAMENTO, ML.DSC_MODALIDADE_LICITACAO,  
                      L.NUM_LICITACAO||'/'||L.ANO_LICITACAO NUM_LICITACAO,  
                       L.NUM_DOCUMENTO||'/'||L.ANO_DOCUMENTO NUM_PROCESSO,  
                       PC.NUM_SEQ_AGRUPAMENTO ITEM,
                        NVL(US.DSC_UNIDADE,U.NOM_ABREVIADO) UNIDADE,  
                      0 VLR_UNITARIO,  
                       'FRACASSADO/DESERTO' NOM_FORNECEDOR,
                        PC.DSC_MATERIAL,   M.NOM_MATERIAL, L.DTA_HOMOLOGACAO,  
                       L.DTA_PUBLICACAO  
                  FROM AGRUPAMENTO_COMPRA AC, LICITACAO L , MODALIDADE_LICITACAO ML,  
                     PEDIDO_COMPRA PC,  
                     MATERIAL M, COMPLEMENTO_MATERIAL CM,  
                     UNIDADE U, SIAFISICO_MATERIAL SM, UNIDADE_SIAFISICO US  
                  WHERE AC.NUM_AGRUPAMENTO=L.NUM_AGRUPAMENTO  
                  AND L.COD_MODALIDADE_LICITACAO=ML.COD_MODALIDADE_LICITACAO   
                  AND AC.NUM_AGRUPAMENTO=PC.NUM_AGRUPAMENTO          
                               
                  AND AC.NUM_AGRUPAMENTO= &numAgrupamento 
                  AND NOT EXISTS (                
                  SELECT 1 FROM PRECO_FORNECEDOR_PEDIDO PFP     
                  WHERE PC.NUM_PEDIDO_COMPRA=PFP.NUM_PEDIDO_COMPRA    
                  AND PC.ANO_PEDIDO_COMPRA=PFP.ANO_PEDIDO_COMPRA      
                  AND (TRIM(PFP.IDF_CLASSIFICACAO)= '1' OR PFP.IDF_CLASSIFICACAO = '01')    
                  )          
                  AND PC.COD_MATERIAL=M.COD_MATERIAL        
                 AND M.COD_MATERIAL=CM.COD_MATERIAL(+)    
                   AND M.COD_UNIDADE=U.COD_UNIDADE           
                  AND PC.COD_MATERIAL=SM.COD_MATERIAL(+)    
                  AND PC.COD_SIAFISICO=SM.COD_SIAFISICO(+)     
                 AND SM.COD_UNIDADE_SIAFISICO=US.COD_UNIDADE_SIAFISICO(+)   
                )   
                ORDER BY ITEM;
                
                --*****************************
                SELECT pc.num_pedido_compra, AC.NUM_AGRUPAMENTO, ML.DSC_MODALIDADE_LICITACAO,  
                      L.NUM_LICITACAO||'/'||L.ANO_LICITACAO NUM_LICITACAO,  
                       L.NUM_DOCUMENTO||'/'||L.ANO_DOCUMENTO NUM_PROCESSO,  
                       PC.NUM_SEQ_AGRUPAMENTO ITEM,
                        NVL(US.DSC_UNIDADE,U.NOM_ABREVIADO) UNIDADE,  
                      0 VLR_UNITARIO,  
                       'FRACASSADO/DESERTO' NOM_FORNECEDOR,
                        PC.DSC_MATERIAL,   M.NOM_MATERIAL, L.DTA_HOMOLOGACAO,  
                       L.DTA_PUBLICACAO  
                  FROM AGRUPAMENTO_COMPRA AC, LICITACAO L , MODALIDADE_LICITACAO ML,  
                     PEDIDO_COMPRA PC,  
                     MATERIAL M, COMPLEMENTO_MATERIAL CM,  
                     UNIDADE U, SIAFISICO_MATERIAL SM, UNIDADE_SIAFISICO US  
                  WHERE AC.NUM_AGRUPAMENTO=L.NUM_AGRUPAMENTO  
                  AND L.COD_MODALIDADE_LICITACAO=ML.COD_MODALIDADE_LICITACAO   
                  AND AC.NUM_AGRUPAMENTO=PC.NUM_AGRUPAMENTO          
                               
                  AND AC.NUM_AGRUPAMENTO= 284592
                  AND NOT EXISTS (                
                  SELECT 1 FROM PRECO_FORNECEDOR_PEDIDO PFP     
                  WHERE PC.NUM_PEDIDO_COMPRA=PFP.NUM_PEDIDO_COMPRA    
                  AND PC.ANO_PEDIDO_COMPRA=PFP.ANO_PEDIDO_COMPRA      
                  AND (TRIM(PFP.IDF_CLASSIFICACAO)= '1' OR PFP.IDF_CLASSIFICACAO = '01')    
                  )          
                  AND PC.COD_MATERIAL=M.COD_MATERIAL        
                 AND M.COD_MATERIAL=CM.COD_MATERIAL(+)    
                   AND M.COD_UNIDADE=U.COD_UNIDADE           
                  AND PC.COD_MATERIAL=SM.COD_MATERIAL(+)    
                  AND PC.COD_SIAFISICO=SM.COD_SIAFISICO(+)     
                 AND SM.COD_UNIDADE_SIAFISICO=US.COD_UNIDADE_SIAFISICO(+);
                 --********************************
           
              
               
