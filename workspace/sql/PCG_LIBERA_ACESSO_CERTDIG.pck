create or replace package PCG_LIBERA_ACESSO_CERTDIG is

  -- Author  : DANIEL ANSELMO
  -- Created : 29/04/2016 09:12:06
  -- Purpose : 
  
 FUNCTION usuario_existe(p_cpfuser profissional.cpf_profissional%TYPE) RETURN BOOLEAN;
 
 PROCEDURE get_usuario_by_cpf( p_cpfuser      profissional.cpf_profissional%TYPE,
                        p_retorno      OUT SYS_REFCURSOR );
 
 PROCEDURE get_usuario_by_name(p_nomuser VARCHAR2,
                               P_RETORNO OUT SYS_REFCURSOR); 
                        
 PROCEDURE get_cert_libera_acesso(P_CPFUSER  cert_liberacao_acesso.num_cpf%TYPE,
                                  P_RETORNO OUT SYS_REFCURSOR);
                                  
PROCEDURE get_cert_libera_acesso_by_seq(P_SEQUSER  cert_liberacao_acesso.seq_cert_liberacao_acesso%TYPE,
                                        P_RETORNO OUT SYS_REFCURSOR);
                                        
PROCEDURE get_cert_criticas_ocorrencia(  P_DATA_INICIAL IN VARCHAR2,
                                         P_DATA_FINAL IN VARCHAR2,
                                         P_RETORNO OUT SYS_REFCURSOR);                                
  
PROCEDURE update_libera_acesso(P_SEQUSER  cert_liberacao_acesso.seq_cert_liberacao_acesso%TYPE,
                                    P_DATALIMITE cert_liberacao_acesso.dta_limite%TYPE);

 PROCEDURE delete_liberacao_acesso(P_SEQUSER  cert_liberacao_acesso.seq_cert_liberacao_acesso%TYPE);

 PROCEDURE insert_libera_acesso(p_cpfuser       cert_liberacao_acesso.num_cpf%TYPE,
                                p_userinc       cert_liberacao_acesso.num_user_inclusao%TYPE,
                                p_codinstsist   cert_liberacao_acesso.cod_inst_sistema%TYPE,                               
                                p_dtalimite     cert_liberacao_acesso.dta_limite%TYPE,
                                p_idfstatus     NUMBER);                    

end PCG_LIBERA_ACESSO_CERTDIG;
/
create or replace package body PCG_LIBERA_ACESSO_CERTDIG is


  FUNCTION usuario_existe(p_cpfuser profissional.cpf_profissional%TYPE) RETURN BOOLEAN
   IS
       l_count      PLS_INTEGER;
  BEGIN
        SELECT COUNT(*)
             INTO l_count FROM usuario u
             WHERE u.num_documento = p_cpfuser;
        RETURN (l_count > 0);
  END;
  
  
 PROCEDURE get_usuario_by_cpf( P_CPFUSER PROFISSIONAL.CPF_PROFISSIONAL%TYPE,
                        P_RETORNO OUT SYS_REFCURSOR ) IS        
 BEGIN               
       
         OPEN P_RETORNO FOR
           SELECT u.nom_usuario||' '||u.sbn_usuario nom_usuario,
             u.nom_usuario_banco,
             u.dta_cadastro,
             u.dta_ultima_conexao,
             u.num_documento,
             u.idf_tipo_usuario,
             u.idf_tipo_documento
        FROM usuario u
            WHERE 1 = 1
              AND u.num_documento = P_CPFUSER;  
   
 END get_usuario_by_cpf;
 
 PROCEDURE get_usuario_by_name(p_nomuser VARCHAR2,
                               P_RETORNO OUT SYS_REFCURSOR ) IS
 BEGIN
   OPEN P_RETORNO FOR
       SELECT u.nom_usuario||' '||u.sbn_usuario nom_usuario,
         u.nom_usuario_banco,
         u.dta_cadastro,
         u.dta_ultima_conexao,
         u.idf_tipo_usuario,
         u.num_documento,
         u.idf_tipo_documento
    FROM usuario u
    WHERE u.nom_usuario||' '||u.sbn_usuario LIKE UPPER('%'||p_nomuser||'%');
 END get_usuario_by_name;
 
 
 PROCEDURE get_cert_libera_acesso(P_CPFUSER  cert_liberacao_acesso.num_cpf%TYPE,
                                  P_RETORNO OUT SYS_REFCURSOR) IS
 BEGIN
       IF P_CPFUSER IS NOT NULL THEN
           OPEN P_RETORNO FOR
             SELECT
                cla.seq_cert_liberacao_acesso, 
                cla.dta_inclusao,
                (SELECT u.num_user_banco||' - '||u.nom_usuario||' '||u.sbn_usuario FROM usuario u
                    WHERE u.num_user_banco = cla.num_user_inclusao
                  AND rownum = 1) num_user_inclusao,           
                cla.num_cpf,
                (SELECT u.num_user_banco||' - '||u.nom_usuario||' '||u.sbn_usuario FROM usuario u
                    WHERE u.num_documento = cla.num_cpf AND u.idf_tipo_documento = '1'
                  AND rownum = 1) nom_usuario,
                cla.cod_inst_sistema,
                cla.dta_limite,
                cla.idf_status
              FROM cert_liberacao_acesso cla
         WHERE 1 = 1
         AND cla.num_cpf = P_CPFUSER;
       ELSE
          OPEN P_RETORNO FOR 
           SELECT * FROM 
           (
              SELECT
                cla.seq_cert_liberacao_acesso, 
                cla.dta_inclusao,
                (SELECT u.num_user_banco||' - '||u.nom_usuario||' '||u.sbn_usuario FROM usuario u
                    WHERE u.num_user_banco = cla.num_user_inclusao
                  AND rownum = 1) num_user_inclusao,           
                cla.num_cpf,
                (SELECT u.num_user_banco||' - '||u.nom_usuario||' '||u.sbn_usuario FROM usuario u
                    WHERE u.num_documento = cla.num_cpf AND u.idf_tipo_documento = '1'
                  AND rownum = 1) nom_usuario,
                cla.cod_inst_sistema,
                cla.dta_limite,
                cla.idf_status
              FROM cert_liberacao_acesso cla
              ORDER BY cla.dta_inclusao DESC
              )
WHERE ROWNUM < 500;
       END IF;
 END get_cert_libera_acesso;
 
PROCEDURE get_cert_libera_acesso_by_seq(P_SEQUSER  cert_liberacao_acesso.seq_cert_liberacao_acesso%TYPE,
                                        P_RETORNO OUT SYS_REFCURSOR) IS
BEGIN
    OPEN P_RETORNO FOR
             SELECT
                cla.seq_cert_liberacao_acesso, 
                cla.dta_inclusao,
                (SELECT u.num_user_banco||' - '||u.nom_usuario||' '||u.sbn_usuario FROM usuario u
                    WHERE u.num_user_banco = cla.num_user_inclusao
                  AND rownum = 1) num_user_inclusao,           
                cla.num_cpf,
                (SELECT u.num_user_banco||' - '||u.nom_usuario||' '||u.sbn_usuario FROM usuario u
                    WHERE u.num_documento = cla.num_cpf AND u.idf_tipo_documento = '1'
                  AND rownum = 1) nom_usuario,
                cla.cod_inst_sistema,
                cla.dta_limite,
                cla.idf_status
              FROM cert_liberacao_acesso cla
         WHERE 1 = 1
         AND cla.seq_cert_liberacao_acesso = P_SEQUSER;      
END;

PROCEDURE get_cert_criticas_ocorrencia(  P_DATA_INICIAL IN VARCHAR2,
                                         P_DATA_FINAL IN VARCHAR2,
                                         P_RETORNO OUT SYS_REFCURSOR) IS
  BEGIN
    
        IF P_DATA_INICIAL IS NULL OR P_DATA_FINAL IS NULL THEN
            OPEN P_RETORNO FOR
               SELECT *
                    FROM (SELECT COUNT(*) QTD,
                                 MAX(C.DTA_INCLUSAO) ULTIMA_INCLUSAO,
                                 C.NUM_CPF,
                                 U.NOM_USUARIO || ' ' || U.SBN_USUARIO NOME_USUARIO,
                                 DECODE(C.IDF_JUSTIFICATIVA,
                                        '1',
                                        'PROBLEMAS COM O CARTÃO',
                                        'PROBLEMAS TECNICOS') ULTIMA_OCORRENCIA,
                                 C.DSC_JUSTIFICATIVA
                            FROM CERT_LIBERACAO_ACESSO C, USUARIO U
                           WHERE U.NUM_DOCUMENTO = C.NUM_CPF
                             AND U.IDF_TIPO_DOCUMENTO = '1'
                             AND c.dta_inclusao > add_months(SYSDATE, -1) -- Incluidos nos ultimos 30 dias.                        
                           GROUP BY C.NUM_CPF,
                                    U.NOM_USUARIO || ' ' || U.SBN_USUARIO,
                                    DECODE(C.IDF_JUSTIFICATIVA,
                                           '1',
                                           'PROBLEMAS COM O CARTÃO',
                                           'PROBLEMAS TECNICOS'),
                                    C.DSC_JUSTIFICATIVA)
                   WHERE QTD > 1
                        ORDER BY ULTIMA_INCLUSAO DESC;
          ELSE
              OPEN P_RETORNO FOR
                SELECT *
                    FROM (SELECT COUNT(*) QTD,
                                 MAX(C.DTA_INCLUSAO) ULTIMA_INCLUSAO,
                                 C.NUM_CPF,
                                 U.NOM_USUARIO || ' ' || U.SBN_USUARIO NOME_USUARIO,
                                 DECODE(C.IDF_JUSTIFICATIVA,
                                        '1',
                                        'PROBLEMAS COM O CARTÃO',
                                        'PROBLEMAS TECNICOS') ULTIMA_OCORRENCIA,
                                 C.DSC_JUSTIFICATIVA
                            FROM CERT_LIBERACAO_ACESSO C, USUARIO U
                           WHERE U.NUM_DOCUMENTO = C.NUM_CPF
                             AND U.IDF_TIPO_DOCUMENTO = '1'
                             
                             AND trunc(C.DTA_INCLUSAO) 
                             BETWEEN  to_date(P_DATA_INICIAL, 'DD/MM/YYYY') AND to_date(P_DATA_FINAL, 'DD/MM/YYYY')                                                     
                           GROUP BY C.NUM_CPF,
                                    U.NOM_USUARIO || ' ' || U.SBN_USUARIO,
                                    DECODE(C.IDF_JUSTIFICATIVA,
                                           '1',
                                           'PROBLEMAS COM O CARTÃO',
                                           'PROBLEMAS TECNICOS'),
                                    C.DSC_JUSTIFICATIVA)
                   WHERE QTD > 1
                        ORDER BY ULTIMA_INCLUSAO DESC;
           END IF; 
              
  END get_cert_criticas_ocorrencia;
 
 PROCEDURE update_libera_acesso(P_SEQUSER  cert_liberacao_acesso.seq_cert_liberacao_acesso%TYPE,
                                    P_DATALIMITE cert_liberacao_acesso.dta_limite%TYPE) IS 
                   
 BEGIN
   
    UPDATE cert_liberacao_acesso cla
        SET cla.dta_limite = P_DATALIMITE
    WHERE cla.seq_cert_liberacao_acesso = P_SEQUSER;    
    COMMIT;   
      
EXCEPTION 
     WHEN OTHERS THEN
        dbms_output.put_line(Sqlcode||' - '||Sqlerrm);       
        ROLLBACK;
       
 END update_libera_acesso;
 
 
 PROCEDURE delete_liberacao_acesso(P_SEQUSER  cert_liberacao_acesso.seq_cert_liberacao_acesso%TYPE) IS
   BEGIN
     DELETE FROM cert_liberacao_acesso cla
     WHERE cla.seq_cert_liberacao_acesso = P_SEQUSER;     
     COMMIT;   
      
EXCEPTION 
     WHEN OTHERS THEN
        dbms_output.put_line(Sqlcode||' - '||Sqlerrm);       
        ROLLBACK;
 END delete_liberacao_acesso;
 
 
 PROCEDURE insert_libera_acesso(p_cpfuser       cert_liberacao_acesso.num_cpf%TYPE,
                                p_userinc       cert_liberacao_acesso.num_user_inclusao%TYPE,
                                p_codinstsist   cert_liberacao_acesso.cod_inst_sistema%TYPE,                               
                                p_dtalimite     cert_liberacao_acesso.dta_limite%TYPE,
                                p_idfstatus     NUMBER) IS
 BEGIN
      
      INSERT INTO cert_liberacao_acesso(dta_inclusao,
                                        num_user_inclusao,
                                        num_cpf,
                                        cod_inst_sistema,
                                        dta_limite,
                                        idf_status )
                           VALUES(SYSDATE,
                                  p_userinc,
                                  p_cpfuser,
                                  p_codinstsist,
                                  p_dtalimite,
                                  p_idfstatus);
                                  
      COMMIT;                       
EXCEPTION
   WHEN OTHERS THEN
     dbms_output.put_line(Sqlcode||' - '||Sqlerrm);
     ROLLBACK;    
END insert_libera_acesso;


  
END PCG_LIBERA_ACESSO_CERTDIG;
/
