SELECT NUM_BEM,
       NUM_MANUTENCAO,
       COD_TIPO_BEM,
       NUM_SERIE,
       NUM_PATRIMONIO,
       COD_TIPO_PATRIMONIO,
       SUBSTR(DSC_COMPLEMENTAR, 1, 250) DSC_COMPLEMENTAR,
       COD_MARCA,
       DSC_MARCA_PROVISORIA,
       DSC_MODELO,
       DSC_MODELO_PATRIMONIO,
       CPL_LOCALIZACAO,
       DTA_FABRICACAO,
       DTA_VENCIMENTO_GARANTIA,
       IDF_USO,
       MOT_NAOUSO,
       OBS_BEM,
       NUM_INCORPORACAO,
       TX_DEPRECIACAO_ESP_ANUAL,
       TX_CORRECAO_MON_ESP_ANUAL,
       IDF_DESINCORPORACAO,
       DTA_DESINCORPORACAO,
       OBS_DESINCORPORACAO,
       NUM_USER_BANCO_DESINCORPORACAO,
       COD_ESPECIE,
       COD_INSTITUICAO,
       NUM_DOCUMENTO_DESINCORPORACAO,
       ANO_DOCUMENTO_DESINCORPORACAO,
       NUM_BEM_PAI,
       NUM_SEQ_RECEB_CEMB,
       OBS_PATRIMONIO,
       NUM_DOCUMENTO_DESINC_ANTIGO,
       DTA_CHAPA_PATRIMONIO,
       IDF_EMPRESTIMO,
       CASE IDF_FINALIDADE_USO
         WHEN 'E' THEN 'ENS/PESQUISA'
         WHEN 'A' THEN 'ASSIST�NCIA'
         ELSE
          'SEM RESTRI��O'
       END FINALIDADE,
       CASE (SELECT itp.idf_restricao_os FROM INSTITUICAO_TIPO_PATRIMONIO itp
                 WHERE itp.cod_tipo_patrimonio = bp.cod_tipo_patrimonio)         
        WHEN '1' THEN 'SIM' ELSE 'N�O'        
        END RESTRICAO_OS          
  FROM BEM_PATRIMONIAL BP
 WHERE BP.NUM_PATRIMONIO = 7354
   AND BP.COD_TIPO_PATRIMONIO = 34;
   
   
   SELECT TP.COD_TIPO_PATRIMONIO, TP.DSC_TIPO_PATRIMONIO
  FROM TIPO_PATRIMONIO TP
 INNER JOIN USUARIO_TIPO_PATRIMONIO UTP
    ON UTP.COD_TIPO_PATRIMONIO = TP.COD_TIPO_PATRIMONIO
   AND UTP.NUM_USER_BANCO = 66827
 INNER JOIN INSTITUICAO_TIPO_PATRIMONIO ITP
    ON ITP.COD_TIPO_PATRIMONIO = TP.COD_TIPO_PATRIMONIO
   AND ITP.COD_INST_SISTEMA = 1
   AND ITP.COD_TIPO_PATRIMONIO = 3
 ORDER BY TP.DSC_TIPO_PATRIMONIO;
 
 
 SELECT * FROM bem_patrimonial bp
 WHERE bp.cod_tipo_patrimonio = 3;

SELECT * FROM centro_custo cc
WHERE cc.nom_cencusto LIKE 'ENGENHARIA%';
