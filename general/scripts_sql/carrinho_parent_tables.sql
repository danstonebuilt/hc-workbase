/*Lista carrinho de Urgência*/
select * from LISTA_CONTROLE t
WHERE t.seq_lista_controle = 12;

    /*Itens da Lista*/
    SELECT * FROM itens_lista_controle ilc
    WHERE ilc.seq_lista_controle = 12;
    
         SELECT * FROM LACRE_REPOSITORIO_ITENS T
         WHERE T.SEQ_ITENS_LISTA_CONTROLE = 12;
      ----------------------------------------
    /*Carrinhos associados a lista*/
    SELECT * FROM repositorio_lista_controle rlc
    WHERE rlc.seq_lista_controle = 12;
        /*Carrinhos lacrados*/
        SELECT * FROM lacre_repositorio lr
        WHERE lr.seq_lacre_repositorio IN (23);
            --Itens no carrinho no momento do Lacre
            select * from LACRE_REPOSITORIO_ITENS t
            where t.seq_lacre_repositorio = 23;
            --Equipamento no carrinho no momento do Lacre
            select * from LACRE_REPOSIT_EQUIPAMENTO t
            where t.seq_lacre_repositorio = 23;
---------------------------------------

SELECT * FROM LISTA_CONTROLE lc
WHERE lc.nom_lista_controle LIKE '%NEUR%';
--AND lc.seq_lista_controle = 28;

select * from GENERICO.REPOSITORIO_LISTA_CONTROLE t
--where t.Dsc_Identificacao LIKE '%NEUR%';
--Lista controle 15
WHERE t.seq_repositorio = 7;


select * from GENERICO.ITENS_LISTA_CONTROLE t
where t.seq_lista_controle = 15;
-->
  select * from GENERICO.LACRE_REPOSITORIO_ITENS t
    where t.seq_itens_lista_controle = 2129;
