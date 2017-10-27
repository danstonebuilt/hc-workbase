  SELECT * FROM documento d
  WHERE d.num_documento = 4082
  AND d.ano_documento = 2017;
  
  --*********Fluxo*****************
  SELECT * FROM licitacao l
                 WHERE 1 = 1              
               --AND l.num_licitacao = 311
               --AND l.num_documento = 8511
               --AND l.cod_instituicao IN (1, 2)
               --AND l.ano_licitacao = 2016
               --AND l.num_agrupamento = 158856
               --AND l.num_documento = 15664
              -- AND l.ano_documento = 2014
              AND l.dsc_oferta_compra IS NOT NULL
               ORDER BY 1 DESC;


UPDATE licitacao l
   SET l.num_oferta_compra
       l.dsc_oferta_compra = &of_compra,
       l.ano_oferta_compra = &ano_ofcompra,
       l.dsc_parecer_juridico = &par_juridico,
       l.dta_parecer_juridico
WHERE l.num_agrupamento = &num_agrupamento;
   
            
 --Etp 01
 select * from ESTOQUE.AGRUPAMENTO_COMPRA t
where t.num_agrupamento = 284592;

--Etp 02
select * from ESTOQUE.PEDIDO_COMPRA t
WHERE 1 = 1
--AND t.num_pedido_compra = 13115
AND t.num_agrupamento = 290392
AND t.ano_pedido_compra = 2017;




--Etp 03
SELECT   *
         FROM PRECO_FORNECEDOR_PEDIDO PFP
  WHERE 1 = 1
  AND pfp.num_pedido_compra IN (24344,22378,23323,23822,25907,25908,23128)
  AND PFP.ANO_PEDIDO_COMPRA = 2017;
  
SELECT * FROM material m
WHERE m.cod_material = '62400150';

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
       (24344,22378,23323,23822,25907,25908,23128)
   AND PFP.ANO_PEDIDO_COMPRA = 2017
   --AND PFP.IDF_CLASSIFICACAO = '1'
   ORDER BY f.nom_fornecedor;

--Etp consultar pedidos de compras vencedores?
SELECT COUNT(*) qtd_pc, pc.num_agrupamento
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
   ORDER BY qtd_pc DESC;
   
   --********pedidos_de_compras
   --284535
   SELECT * FROM pedido_compra pc
   WHERE pc.num_agrupamento = 284535;
   
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
           
   SELECT  *
         FROM PRECO_FORNECEDOR_PEDIDO pfp    
    WHERE PFP.ANO_PEDIDO_COMPRA = 2017
   AND PFP.IDF_CLASSIFICACAO = '1'
   AND pfp.cod_material = '09020020'
   AND ROWNUM <= 500 ;
   
   SELECT * FROM pedido_compra pc
   WHERE pc.num_pedido_compra IN (2595,11004,14891)
   AND pc.ano_pedido_compra = 2017;
   

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
                               --283151
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
                 
                 /******************------------------------****************************/
                 
                 /*
224725
218005
203686
184694
175899
165783
161011
158856

*/

SELECT AC.NUM_AGRUPAMENTO,
       ML.DSC_MODALIDADE_LICITACAO,
       L.NUM_LICITACAO || '/' || L.ANO_LICITACAO NUM_LICITACAO,
       L.NUM_DOCUMENTO || '/' || L.ANO_DOCUMENTO NUM_PROCESSO,
       PC.NUM_SEQ_AGRUPAMENTO ITEM,
       NVL(US.DSC_UNIDADE, U.NOM_ABREVIADO) UNIDADE,
       ROUND(PFP.VLR_MATERIAL * NVL(SM.FAT_CONVERSAO, 1), 6) VLR_UNITARIO,
       F.NOM_FORNECEDOR,
       PC.DSC_MATERIAL,
       M.NOM_MATERIAL,
       L.DTA_HOMOLOGACAO,
       L.DTA_PUBLICACAO
  FROM AGRUPAMENTO_COMPRA      AC,
       LICITACAO               L,
       MODALIDADE_LICITACAO    ML,
       PEDIDO_COMPRA           PC,
       PRECO_FORNECEDOR_PEDIDO PFP,
       FORNECEDOR              F,
       MATERIAL                M,
       COMPLEMENTO_MATERIAL    CM,
       UNIDADE                 U,
       SIAFISICO_MATERIAL      SM,
       UNIDADE_SIAFISICO       US
 WHERE AC.NUM_AGRUPAMENTO = L.NUM_AGRUPAMENTO
   AND L.COD_MODALIDADE_LICITACAO = ML.COD_MODALIDADE_LICITACAO
   AND AC.NUM_AGRUPAMENTO = PC.NUM_AGRUPAMENTO
   AND AC.NUM_AGRUPAMENTO = &NUMAGRUPAMENTO
   AND PC.NUM_PEDIDO_COMPRA = PFP.NUM_PEDIDO_COMPRA
   AND PC.ANO_PEDIDO_COMPRA = PFP.ANO_PEDIDO_COMPRA
   AND (TRIM(PFP.IDF_CLASSIFICACAO) = '1' OR PFP.IDF_CLASSIFICACAO = '01')
   AND PFP.COD_FORNECEDOR = F.COD_FORNECEDOR
   AND PC.COD_MATERIAL = M.COD_MATERIAL
   AND M.COD_MATERIAL = CM.COD_MATERIAL(+)
   AND M.COD_UNIDADE = U.COD_UNIDADE
   AND PC.COD_MATERIAL = SM.COD_MATERIAL(+)
   AND PC.COD_SIAFISICO = SM.COD_SIAFISICO(+)
   AND SM.COD_UNIDADE_SIAFISICO = US.COD_UNIDADE_SIAFISICO(+);

SELECT L.NUM_AGRUPAMENTO,
       AC.IDF_DIRECIONAMENTO,
       AC.IDF_ESTAGIO_AGRUPAMENTO,
       L.NUM_DOCUMENTO,
       L.ANO_DOCUMENTO,
       L.NUM_LICITACAO,
       L.ANO_LICITACAO,
       L.DTA_HOR_ABERT_PROPOSTA,
       L.DSC_OFERTA_COMPRA
  FROM LICITACAO L
  JOIN AGRUPAMENTO_COMPRA AC
    ON L.NUM_AGRUPAMENTO = AC.NUM_AGRUPAMENTO
 WHERE 1 = 1
   AND L.NUM_AGRUPAMENTO IN (259428);

SELECT * FROM AGRUPAMENTO_COMPRA AC;

SELECT PC.NUM_AGRUPAMENTO, PFP.*
  FROM PRECO_FORNECEDOR_PEDIDO PFP
  JOIN PEDIDO_COMPRA PC
    ON PFP.NUM_PEDIDO_COMPRA = PC.NUM_PEDIDO_COMPRA
  JOIN AGRUPAMENTO_COMPRA AC
    ON PC.NUM_AGRUPAMENTO = AC.NUM_AGRUPAMENTO
   AND AC.IDF_DIRECIONAMENTO = '4'
   AND PC.ANO_PEDIDO_COMPRA = 2016
 WHERE PFP.IDF_CLASSIFICACAO = '1'
   AND PFP.ANO_PEDIDO_COMPRA = 2016
   AND ROWNUM <= 200
 ORDER BY PC.DTA_PEDIDO_COMPRA DESC;

SELECT * FROM pedido_compra pc
WHERE pc.num_agrupamento = 286077;

/*Licitação e documento/processo*/
SELECT * FROM licitacao l
JOIN agrupamento_compra ac
ON l.num_agrupamento = ac.num_agrupamento
AND ac.idf_direcionamento = 4
AND ac.idf_estagio_agrupamento IN (6, 9)
AND l.ano_documento = 2016
AND l.cod_instituicao = 1;

/*Itens ganhadores*/
SELECT  
         pfp.cod_fornecedor,     
         f.nom_fornecedor,         
         COUNT(*) qtd_num_itens
          FROM PRECO_FORNECEDOR_PEDIDO PFP
          JOIN PEDIDO_COMPRA PC
            ON PFP.NUM_PEDIDO_COMPRA = PC.NUM_PEDIDO_COMPRA
           AND PFP.ANO_PEDIDO_COMPRA = PC.ANO_PEDIDO_COMPRA
           JOIN fornecedor f
           ON pfp.cod_fornecedor = f.cod_fornecedor        
         WHERE PC.NUM_AGRUPAMENTO = 281626--281626
         AND pfp.idf_classificacao = '1' 
        GROUP BY pfp.cod_fornecedor, f.nom_fornecedor;

SELECT    pc.num_seq_agrupamento,
         pfp.cod_fornecedor,     
         f.nom_fornecedor,
        ROW_NUMBER() OVER ( ORDER BY num_seq_agrupamento) status
          FROM PRECO_FORNECEDOR_PEDIDO PFP
          JOIN PEDIDO_COMPRA PC
            ON PFP.NUM_PEDIDO_COMPRA = PC.NUM_PEDIDO_COMPRA
           AND PFP.ANO_PEDIDO_COMPRA = PC.ANO_PEDIDO_COMPRA
           JOIN fornecedor f
           ON pfp.cod_fornecedor = f.cod_fornecedor        
         WHERE PC.NUM_AGRUPAMENTO = 281626--281626
         AND pfp.idf_classificacao = '1' 
        ORDER BY num_seq_agrupamento; 
        
   SELECT  pfp.cod_fornecedor,
           f.nom_fornecedor,
           SUM(DECODE(pc.num_seq_agrupamento, 1, pc.cod_material)) item_01,
           SUM(DECODE(pc.num_seq_agrupamento, 2, pc.cod_material)) item_02, 
           SUM(DECODE(pc.num_seq_agrupamento, 3, pc.cod_material)) item_03, 
           SUM(DECODE(pc.num_seq_agrupamento, 4, pc.cod_material)) item_04,
           SUM(DECODE(pc.num_seq_agrupamento, 5, pc.cod_material)) item_05,
           SUM(DECODE(pc.num_seq_agrupamento, 6, pc.cod_material)) item_06, 
           SUM(DECODE(pc.num_seq_agrupamento, 7, pc.cod_material)) item_07, 
           SUM(DECODE(pc.num_seq_agrupamento, 8, pc.cod_material)) item_08, 
           SUM(DECODE(pc.num_seq_agrupamento, 9, pc.cod_material)) item_09, 
           SUM(DECODE(pc.num_seq_agrupamento, 10, pc.cod_material)) item_010,
           SUM(DECODE(pc.num_seq_agrupamento, 11, pc.cod_material)) item_011,
           SUM(DECODE(pc.num_seq_agrupamento, 12, pc.cod_material)) item_012,
           SUM(DECODE(pc.num_seq_agrupamento, 13, pc.cod_material)) item_013,
           SUM(DECODE(pc.num_seq_agrupamento, 14, pc.cod_material)) item_014,
            SUM(DECODE(pc.num_seq_agrupamento, 15, pc.cod_material)) item_015                       
          
          FROM PRECO_FORNECEDOR_PEDIDO PFP
          JOIN PEDIDO_COMPRA PC
            ON PFP.NUM_PEDIDO_COMPRA = PC.NUM_PEDIDO_COMPRA
           AND PFP.ANO_PEDIDO_COMPRA = PC.ANO_PEDIDO_COMPRA
           JOIN fornecedor f
           ON pfp.cod_fornecedor = f.cod_fornecedor        
         WHERE PC.NUM_AGRUPAMENTO = 281626--281626
         AND pfp.idf_classificacao = '1'
         GROUP BY pfp.cod_fornecedor, f.nom_fornecedor ;             
     
 
SELECT PFP.COD_FORNECEDOR,
       F.NOM_FORNECEDOR,
       LISTAGG(PC.NUM_SEQ_AGRUPAMENTO, ', ') WITHIN GROUP(ORDER BY PC.NUM_SEQ_AGRUPAMENTO) ITENS
  FROM PRECO_FORNECEDOR_PEDIDO PFP
  JOIN PEDIDO_COMPRA PC
    ON PFP.NUM_PEDIDO_COMPRA = PC.NUM_PEDIDO_COMPRA
   AND PFP.ANO_PEDIDO_COMPRA = PC.ANO_PEDIDO_COMPRA
  JOIN FORNECEDOR F
    ON PFP.COD_FORNECEDOR = F.COD_FORNECEDOR
 WHERE PC.NUM_AGRUPAMENTO = 281626--284592 --281626
   AND PFP.IDF_CLASSIFICACAO = '1'
 GROUP BY PFP.COD_FORNECEDOR, F.NOM_FORNECEDOR;
        
         



/*Itens fracassado*/
         
         
         SELECT --DISTINCT PC.NUM_SEQ_AGRUPAMENTO,
             --DISTINCT LISTAGG(PC.NUM_SEQ_AGRUPAMENTO, ', ') WITHIN GROUP(ORDER BY PC.NUM_SEQ_AGRUPAMENTO) ITENS,
             pc.cod_material,
             PFP.COD_FORNECEDOR,
             f.nom_fornecedor,
             PC.NUM_SEQ_AGRUPAMENTO
           FROM PRECO_FORNECEDOR_PEDIDO PFP
           JOIN PEDIDO_COMPRA PC
             ON PFP.NUM_PEDIDO_COMPRA = PC.NUM_PEDIDO_COMPRA
            AND PFP.ANO_PEDIDO_COMPRA = PC.ANO_PEDIDO_COMPRA
           JOIN FORNECEDOR F
             ON PFP.COD_FORNECEDOR = F.COD_FORNECEDOR
          WHERE PC.NUM_AGRUPAMENTO = 279553--284592
            AND PC.NUM_SEQ_AGRUPAMENTO NOT IN
                (SELECT PC.NUM_SEQ_AGRUPAMENTO
                   FROM PRECO_FORNECEDOR_PEDIDO PFP
                   JOIN PEDIDO_COMPRA PC
                     ON PFP.NUM_PEDIDO_COMPRA = PC.NUM_PEDIDO_COMPRA
                    AND PFP.ANO_PEDIDO_COMPRA = PC.ANO_PEDIDO_COMPRA
                   JOIN FORNECEDOR F
                     ON PFP.COD_FORNECEDOR = F.COD_FORNECEDOR
                  WHERE PC.NUM_AGRUPAMENTO = 279553--284592
                    AND PFP.IDF_CLASSIFICACAO = '1');
                    
                    
          SELECT --DISTINCT PC.NUM_SEQ_AGRUPAMENTO,
          LISTAGG(PC.NUM_SEQ_AGRUPAMENTO, ', ') WITHIN GROUP(ORDER BY PC.NUM_SEQ_AGRUPAMENTO) ITENS
          
            
            FROM PRECO_FORNECEDOR_PEDIDO PFP
            JOIN PEDIDO_COMPRA PC
              ON PFP.NUM_PEDIDO_COMPRA = PC.NUM_PEDIDO_COMPRA
             AND PFP.ANO_PEDIDO_COMPRA = PC.ANO_PEDIDO_COMPRA
            JOIN FORNECEDOR F
              ON PFP.COD_FORNECEDOR = F.COD_FORNECEDOR
           WHERE PC.NUM_AGRUPAMENTO = 279553--284592
             AND PC.NUM_SEQ_AGRUPAMENTO NOT IN
                 (SELECT PC.NUM_SEQ_AGRUPAMENTO
                    FROM PRECO_FORNECEDOR_PEDIDO PFP
                    JOIN PEDIDO_COMPRA PC
                      ON PFP.NUM_PEDIDO_COMPRA = PC.NUM_PEDIDO_COMPRA
                     AND PFP.ANO_PEDIDO_COMPRA = PC.ANO_PEDIDO_COMPRA
                    JOIN FORNECEDOR F
                      ON PFP.COD_FORNECEDOR = F.COD_FORNECEDOR
                   WHERE PC.NUM_AGRUPAMENTO = 279553--284592
                     AND PFP.IDF_CLASSIFICACAO = '1')
                     GROUP BY PC.NUM_SEQ_AGRUPAMENTO;
                   
         
         
/*Itens de propostas desertos*/
SELECT PC.NUM_SEQ_AGRUPAMENTO ITENS_DESERTOS
  FROM PEDIDO_COMPRA PC
 WHERE PC.NUM_AGRUPAMENTO = 290392
   AND PC.NUM_PEDIDO_COMPRA NOT IN
       (SELECT PFP.NUM_PEDIDO_COMPRA
          FROM PRECO_FORNECEDOR_PEDIDO PFP
          JOIN PEDIDO_COMPRA PC
            ON PFP.NUM_PEDIDO_COMPRA = PC.NUM_PEDIDO_COMPRA
           AND PFP.ANO_PEDIDO_COMPRA = PC.ANO_PEDIDO_COMPRA          
         WHERE PC.NUM_AGRUPAMENTO = 290392)
         ORDER BY ITENS_DESERTOS DESC ;
         
         
 SELECT LISTAGG(PC.NUM_SEQ_AGRUPAMENTO, ', ') WITHIN GROUP(ORDER BY PC.NUM_SEQ_AGRUPAMENTO) ITENS_DESERTOS
   FROM PEDIDO_COMPRA PC
  WHERE PC.NUM_AGRUPAMENTO = 290392
    AND PC.NUM_PEDIDO_COMPRA NOT IN
        (SELECT PFP.NUM_PEDIDO_COMPRA
           FROM PRECO_FORNECEDOR_PEDIDO PFP
           JOIN PEDIDO_COMPRA PC
             ON PFP.NUM_PEDIDO_COMPRA = PC.NUM_PEDIDO_COMPRA
            AND PFP.ANO_PEDIDO_COMPRA = PC.ANO_PEDIDO_COMPRA
          WHERE PC.NUM_AGRUPAMENTO = 290392);
         
         
         
--***************************************************************
     

SELECT * FROM licitacao l
WHERE l.num_documento = 1945--1945--4082--8511
AND l.ano_documento = 2017;


   


           
              
               
