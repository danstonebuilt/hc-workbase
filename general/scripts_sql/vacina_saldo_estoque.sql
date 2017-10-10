SELECT * FROM VACINA_RECEPCAO VR
WHERE TRUNC(VR.DTA_HOR_CADASTRO) = TRUNC(SYSDATE)
ORDER BY 1 DESC;

select * from GENERICO.VACINA_RECEPCAO_APLICACAO t
where t.seq_vacina_recepcao = 7986;

--*********Consumo de MPU
select * from GENERICO.UTILIZACAO_MATERIAL t
where 1 = 1
AND t.Cod_Cencusto = 'CACD02010'
AND t.seq_utilizacao_material IN ('5010925', '5010924');


--*****saldo MPU
SELECT * FROM ESTOQUE_PRONTO_USO epu
WHERE epu.cod_cencusto = 'CACD02010'
AND epu.cod_material IN  ('7010430X','70104268')
ORDER BY epu.dta_ult_saida DESC;

--********Saldo oficial de estoque
SELECT m.nom_material,
        em.*
  FROM ESTOQUE_MATERIAL EM
   JOIN material m
  ON m.cod_material = em.cod_material  
 WHERE EM.COD_CENCUSTO = 'CACD02022'
 AND em.cod_material IN ('7010430X', '70104268')
 ORDER BY EM.DTA_ULT_SAIDA DESC;

--*****Saldo por dia
select * from ESTOQUE.CONSUMO_DIARIO_MATERIAL t
WHERE t.cod_material = '7010380X'
AND t.cod_cencusto = 'CACD02010'
ORDER BY  t.dta_movimento DESC;


/*Encontrar requisicao por meio de material*/
SELECT * FROM item_requisicao_material irm
WHERE irm.ano_requisicao_material = 2017
AND irm.cod_material IN ('70104268', '7010380X')
ORDER BY irm.num_requisicao DESC;


SELECT * FROM REQUISICAO_MATERIAL RM
WHERE RM.COD_CENCUSTO_SOLICITANTE = 'CACD02010'
AND rm.num_requisicao IN (1215878, 70104268)
ORDER BY RM.DTA_REQUISICAO DESC;

SELECT * FROM material m
WHERE m.cod_material = '70103653';

SELECT * FROM estoque_material em
WHERE em.cod_material = '70103653';

