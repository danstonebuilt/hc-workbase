/*SM*/
SELECT
  * FROM solicitacao_materiais sm
  WHERE sm.ano_solicitacao = 2017
  AND sm.idf_tipo_sm IN (5,6)
  ORDER BY 1 DESC;
  
/*Itens solicitação*/
select * from ESTOQUE.ITENS_SOLICITACAO t
where t.num_solicitacao = 7646
and   t.ano_solicitacao = 2017;


SELECT * FROM requisicao_material rm
WHERE rm.ano_requisicao_material = 2017
AND trunc(rm.dta_requisicao) = trunc(SYSDATE)
AND rm.cod_cencusto_solicitante = 'CACO01019'
ORDER BY 1 DESC;
