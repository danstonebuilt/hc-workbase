SELECT * FROM usuario u
WHERE u.nom_usuario LIKE UPPER('SILVANDIRA ANGELA');

SELECT * FROM usuario u
WHERE upper(u.nom_usuario_banco) = UPPER('daanselmo');
--66827

/*Das profil verbinden zum Benutzer*/
SELECT T.NUM_USER_BANCO,
       P.COD_ROLE,
       P.NOM_ROLE,
       P.DSC_ROLE
  FROM ACESSO.USUARIO_ROLE T
  JOIN PERFIL P
    ON T.COD_ROLE = P.COD_ROLE
 WHERE EXISTS (SELECT 1
          FROM USUARIO U
         WHERE UPPER(U.NOM_USUARIO_BANCO) = UPPER('genari')
           AND T.NUM_USER_BANCO = U.NUM_USER_BANCO)
 ORDER BY P.NOM_ROLE;
 
 -----------------------------------------------------------
 SELECT cod_role, NOM_ROLE,  COUNT(*) FROM (
 SELECT T.NUM_USER_BANCO,
       P.COD_ROLE,
       P.NOM_ROLE,
       P.DSC_ROLE
  FROM ACESSO.USUARIO_ROLE T
  JOIN PERFIL P
    ON T.COD_ROLE = P.COD_ROLE
 WHERE EXISTS (SELECT 1
          FROM USUARIO U
         WHERE UPPER(U.NOM_USUARIO_BANCO) = UPPER('rsantana')
           AND T.NUM_USER_BANCO = U.NUM_USER_BANCO)
 ORDER BY P.NOM_ROLE
 ) GROUP BY cod_role, NOM_ROLE;



/*Deleting remaining profiles*/
DELETE FROM USUARIO_ROLE t
WHERE t.num_user_banco = 2179
AND t.cod_role IN (

SELECT ur.cod_role FROM USUARIO_ROLE ur
WHERE ur.num_user_banco = 2179);
-----------------------------------------------------------

SELECT * FROM sistema s
WHERE s.nom_sistema LIKE UPPER('%SIH%');


--sehen profil vollständig
SELECT * FROM perfil p
      WHERE 1 = 1
--AND p.nom_role LIKE UPPER('%RL_DADOS_ESTAT_OPERADOR%');
AND p.cod_role = 9072;

/*finden Vater*/
select * from ACESSO.PERFIL t
where t.cod_role = 767;
---------------------------------------------------------
SELECT *
  FROM ACESSO.PERFIL T
 WHERE T.COD_ROLE
  IN (SELECT P.COD_ROLE_PAI
        FROM PERFIL P
        WHERE 1 = 1
        AND P.NOM_ROLE LIKE UPPER('medico%'));
        
        SELECT *
  FROM ACESSO.PERFIL T
 WHERE T.COD_ROLE = 12;



SELECT * FROM programa p
WHERE 1 = 1
AND p.nom_programa LIKE UPPER('%MI_ALTERAR_INSTITUTO%');


/*Systeme verbinden das profil*/
select t.cod_inst_sistema,
       (SELECT i.nom_instituicao FROM instituicao i
            WHERE i.cod_instituicao = t.cod_inst_sistema) instituicao,
       t.cod_role,
       per.Nom_Role,
       t.cod_sistema,
       sis.nom_sistema        
     from ACESSO.SISTEMA_PERFIL t
     INNER JOIN perfil PER
     ON t.cod_role = PER.COD_ROLE
     INNER JOIN sistema sis
     ON t.cod_sistema = sis.cod_sistema
where t.cod_role = 1054;


/*Programmname*/
SELECT * FROM sistema s
WHERE 1 = 1
AND s.nom_sistema LIKE UPPER('SIH');

/*Programas assossiados a sistemas*/ 
 
 SELECT sis.cod_sistema,
        sis.nom_sistema,
        pro.cod_programa,
        pro.nom_programa
       FROM sistema_programa sp
       LEFT JOIN sistema sis
       ON sp.cod_sistema = sis.cod_sistema
       LEFT JOIN programa pro
       ON sp.cod_programa = pro.cod_programa
  WHERE sp.cod_sistema = 114
  ORDER BY pro.nom_programa;
 
 
 
 /*Programas associados a roles*/
 SELECT k.cod_role,
        k.nom_role,
        p.cod_programa,
        p.nom_programa,        
        (
          SELECT sis.nom_sistema FROM sistema sis
         WHERE EXISTS
         (
            SELECT 1 FROM sistema_programa t
            WHERE t.cod_programa = p.cod_programa
            AND sis.cod_sistema = t.cod_sistema)
            AND ROWNUM = 1
         ) sistema
      FROM Role_Programa rp
      JOIN programa p
      ON rp.cod_programa = p.cod_programa
      JOIN perfil k
      ON rp.cod_role = k.cod_role
      AND k.cod_role = 1260
      ORDER BY sistema;     
      
---------------------------------------------------------------
      SELECT sis.cod_sistema, sis.nom_sistema FROM sistema sis
          WHERE EXISTS
         (
            SELECT 1 FROM sistema_programa t
            WHERE t.cod_programa = 4
            AND sis.cod_sistema = t.cod_sistema);            
         

      
     /*Existencia de roles de banco*/ 
     SELECT * FROM dba_roles atp
     WHERE atp.role LIKE '%CONSULTA_MATERIAL%';

/*Roles de banco associada ao perfil*/     
SELECT * FROM perfil_role_banco prb
     WHERE 1 = 1
     AND prb.cod_role = 9193;
  --   AND prb.nom_role_banco = 'ROLE_ACESSO_QUALIS';
  
  SELECT * FROM ROLE_TAB_PRIVS WHERE ROLE = 'ROLE_MATERIAL_PRONTO_USO';
  
  /*Ver objetos de banco associados a roles*/
  SELECT * FROM DBA_TAB_PRIVS WHERE GRANTEE LIKE '%ROLE_CONSULTA_MATERIAIS%';
     
      


