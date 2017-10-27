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
           JOIN material m
           ON pc.cod_material = m.cod_material
           JOIN fornecedor f
           ON pfp.cod_fornecedor = f.cod_fornecedor        
         WHERE PC.NUM_AGRUPAMENTO = 281626--281626
         AND pfp.idf_classificacao = '1'
         GROUP BY pfp.cod_fornecedor, f.nom_fornecedor ; 
         
         
            SELECT
               pfp.cod_fornecedor, f.nom_fornecedor,           
               listagg(pc.num_seq_agrupamento, ', ') WITHIN GROUP(ORDER BY pc.num_seq_agrupamento) num_seq_agrupamento
            FROM PRECO_FORNECEDOR_PEDIDO PFP
            JOIN PEDIDO_COMPRA PC
              ON PFP.NUM_PEDIDO_COMPRA = PC.NUM_PEDIDO_COMPRA
             AND PFP.ANO_PEDIDO_COMPRA = PC.ANO_PEDIDO_COMPRA
             JOIN material m
             ON pc.cod_material = m.cod_material
             JOIN fornecedor f
             ON pfp.cod_fornecedor = f.cod_fornecedor        
           WHERE PC.NUM_AGRUPAMENTO = 281626--281626
           AND pfp.idf_classificacao = '1'
           GROUP BY pfp.cod_fornecedor, f.nom_fornecedor
           ORDER BY nom_fornecedor DESC;
        
