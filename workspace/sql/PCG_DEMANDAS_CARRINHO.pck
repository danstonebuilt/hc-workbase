create or replace package PCG_DEMANDAS_CARRINHO is

  -- Author  : Daniel Anselmo
  -- Created : 20/05/2016 10:18:44
  -- Purpose : Fazer opera??es no menu do carrinho, vista que fazer via objetos C#  que mapeiam objetos do banco tornariam o desenvolvimento
  -- muito trabalhoso
  
 /*
  PROCEDURE get_seq_through_item_control recebe como parametro uma sequencia de ItemListaControle, que nada mais é doque um item de uma lista,
  e devolve  uma SeqLacreRepositorio, que é o estado do repositorio com o lacre. Desta forma da para saber quantos itens existe no estado de cada 
  lacração.
   Abaixo segue os passos para debug via dbms_output.put_line.
  
  1 - Resgato a tabela Lista_Controle para obter o numero da lista.
  2- Tendo o numero da lista consigo encontrar o  repositorio.
  3 - Resgato a sequencia do lacre do repositorio, passando uma copia do repositorio para o cursor.
 */
 PROCEDURE get_seq_through_item_control( p_seqItensListaControle IN NUMBER, p_retorno OUT NUMBER);

end PCG_DEMANDAS_CARRINHO;
/
create or replace package body PCG_DEMANDAS_CARRINHO is


 PROCEDURE GET_SEQ_THROUGH_ITEM_CONTROL(P_SEQITENSLISTACONTROLE IN NUMBER,
                                        P_RETORNO               OUT NUMBER) IS
                                        
   L_ITENSLISTACONTROLE                 ITENS_LISTA_CONTROLE%ROWTYPE;
   L_REPOSITORIO_LISTA_CONTROLE         REPOSITORIO_LISTA_CONTROLE%ROWTYPE;
 
   CURSOR LACRE_REPOSITORIO_TPCUR(PL_SEQ_REPOSITORIO NUMBER) IS
     SELECT *
       FROM (SELECT *
               FROM LACRE_REPOSITORIO RL
              WHERE RL.SEQ_REPOSITORIO = PL_SEQ_REPOSITORIO
              ORDER BY RL.SEQ_LACRE_REPOSITORIO DESC)
      WHERE ROWNUM = 1;
 
   REC_LACRE_REPOSITORIO LACRE_REPOSITORIO%ROWTYPE;
 
 BEGIN
 
   SELECT *
     INTO L_ITENSLISTACONTROLE
     FROM ITENS_LISTA_CONTROLE ILC
    WHERE ILC.SEQ_ITENS_LISTA_CONTROLE = P_SEQITENSLISTACONTROLE;
 
   DBMS_OUTPUT.PUT_LINE(L_ITENSLISTACONTROLE.SEQ_LISTA_CONTROLE || ' - Lista controle');
 
   SELECT *
     INTO L_REPOSITORIO_LISTA_CONTROLE
     FROM REPOSITORIO_LISTA_CONTROLE RLC
    WHERE RLC.SEQ_LISTA_CONTROLE = L_ITENSLISTACONTROLE.SEQ_LISTA_CONTROLE;
 
   DBMS_OUTPUT.PUT_LINE(L_REPOSITORIO_LISTA_CONTROLE.SEQ_REPOSITORIO || ' - Repositorio');
 
   OPEN LACRE_REPOSITORIO_TPCUR(L_REPOSITORIO_LISTA_CONTROLE.SEQ_REPOSITORIO);
   FETCH LACRE_REPOSITORIO_TPCUR
     INTO REC_LACRE_REPOSITORIO;
   CLOSE LACRE_REPOSITORIO_TPCUR;
 
   DBMS_OUTPUT.PUT_LINE(REC_LACRE_REPOSITORIO.SEQ_LACRE_REPOSITORIO || ' - Sequencia do Repositorio');
 
   P_RETORNO := REC_LACRE_REPOSITORIO.SEQ_LACRE_REPOSITORIO;
 
 EXCEPTION
   WHEN OTHERS THEN
     P_RETORNO := -1;
   
 END GET_SEQ_THROUGH_ITEM_CONTROL;
 
END PCG_DEMANDAS_CARRINHO;
/
