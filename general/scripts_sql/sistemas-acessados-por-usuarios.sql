WITH func_da AS(

SELECT 
   H.CODLOC,
   O.NOMLOC,
   U.NUM_USER_BANCO,
   U.NOM_USUARIO_BANCO,
   F.NOMFUN
FROM R034FUN F
    LEFT JOIN R016ORN O ON O.TABORG = F.TABORG AND O.NUMLOC = F.NUMLOC
    LEFT JOIN R016HIE H ON H.TABORG = F.TABORG AND H.NUMLOC = F.NUMLOC AND SYSDATE BETWEEN H.DATINI AND H.DATFIM
    LEFT JOIN USUARIO U ON U.NUM_DOCUMENTO = LPAD(F.NUMCPF,11,'0')     
WHERE      
  F.SITAFA <> 7
  AND F.TIPCOL = 1
  AND H.CODLOC LIKE 'DA.%'  
ORDER BY CODLOC) 


SELECT DISTINCT *
  FROM (SELECT --ur.num_user_banco,
         (SELECT U.NOM_USUARIO || ' ' || U.SBN_USUARIO
            FROM USUARIO U
           WHERE U.NUM_USER_BANCO = UR.NUM_USER_BANCO) NOME,
         (SELECT NOMLOC
            FROM FUNC_DA
           WHERE NUM_USER_BANCO = UR.NUM_USER_BANCO
             AND ROWNUM = 1) DEPARTAMENTO,
         --ur.cod_inst_sistema,                 
         G.NOM_SISTEMA SISTEMAS
          FROM ACESSO.USUARIO_ROLE   UR,
               ACESSO.SISTEMA_PERFIL T,
               ACESSO.SISTEMA        G
         WHERE UR.COD_ROLE = T.COD_ROLE
           AND UR.COD_INST_SISTEMA = T.COD_INST_SISTEMA
           AND G.COD_SISTEMA = T.COD_SISTEMA
           AND UR.NUM_USER_BANCO IN (SELECT NUM_USER_BANCO FROM FUNC_DA) --Temporary query
        
        )
 ORDER BY NOME;            

-----------------------------------------------------------

select f.num_user_banco,
       f.cod_role,
       f.cod_inst_sistema,
       t.cod_sistema,
       g.nom_sistema
     from ACESSO.USUARIO_ROLE f,
          ACESSO.SISTEMA_PERFIL t,
          ACESSO.SISTEMA g          
   WHERE f.cod_role = t.cod_role
   AND f.cod_inst_sistema = t.cod_inst_sistema
   AND g.cod_sistema = t.cod_sistema     
   AND f.num_user_banco = 66827;


SELECT B.COD_LOCAL, B.SGL_LOCAL || ' - ' || B.NOM_LOCAL NOM_LOCAL, B.SGL_LOCAL
FROM USUARIO_LOCAL A, LOCAL B
WHERE A.NUM_USER_BANCO = 66827
--AND B.IDF_ATIVIDADE = 'A'
AND A.COD_LOCAL = B.COD_LOCAL;


SELECT * FROM usuario u
WHERE u.num_user_banco = 66827;
 

