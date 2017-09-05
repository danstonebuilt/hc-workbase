SELECT ROWNUM, AAS.*, DECODE(AAS.IDF_ATIVO,0,'SIM',1,'N�O') DSC_ATIVO
FROM ARQUIVO_ANEXADO_SISTEMA AAS
WHERE AAS.SEQ_CODIGO_SISTEMA_ORIGEM = 803232
 AND AAS.IDF_ATIVO = 0
ORDER BY AAS.DTA_HOR_CADASTRO DESC;


/*Ordem de Servi�o*/
WITH ordem_servico AS (
   SELECT * FROM ordem_servico os
      WHERE os.num_ordem_servico = 45131
    AND os.ano_ordem_servico = 2016;
)

/*Hist�rico de ordem de servi�o*/
