
/*Ordem de Serviço*/
WITH os_qry AS (
   SELECT * FROM ordem_servico os
      WHERE os.num_ordem_servico = 45131
    AND os.ano_ordem_servico = 2016
)

/*Histórico de ordem de serviço*/
select * from GENERICO.HISTORICO_ORDEM_SERVICO t
     where t.num_ordem = (SELECT num_ordem_servico FROM os_qry);
