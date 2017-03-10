SELECT ROWNUM, AAS.*, DECODE(AAS.IDF_ATIVO,0,'SIM',1,'NÃO') DSC_ATIVO
FROM ARQUIVO_ANEXADO_SISTEMA AAS
WHERE AAS.SEQ_CODIGO_SISTEMA_ORIGEM = 803232
 AND AAS.IDF_ATIVO = 0
ORDER BY AAS.DTA_HOR_CADASTRO DESC;


/*Ordem de Serviço*/
WITH ordem_servico AS (
   SELECT * FROM ordem_servico os
      WHERE os.num_ordem_servico = 45131
    AND os.ano_ordem_servico = 2016;
)

/*Histórico de ordem de serviço*/
