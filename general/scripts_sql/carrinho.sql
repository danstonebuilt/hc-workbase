SELECT *
  FROM ITENS_LISTA_CONTROLE X
 WHERE X.SEQ_LISTA_CONTROLE = 75
   --AND X.COD_MATERIAL IN ('07098406', '07098364', '07073859', '07032511');
   AND X.COD_MATERIAL IN ('07031907', '77100104')
   FOR UPDATE;
   
   --07031907
 --77100104
 
