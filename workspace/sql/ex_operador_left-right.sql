/*Right Outer Join*/      
SELECT
 lre.num_user_resp_teste user_lre,
 u.num_user_banco user_u
     FROM LACRE_REPOSIT_EQUIPAMENTO lre,  usuario u
WHERE lre.num_user_resp_teste(+) = u.num_user_banco;

/*Left Outer Join*/  
SELECT
 lre.num_user_resp_teste user_lre,
 u.num_user_banco user_u
     FROM LACRE_REPOSIT_EQUIPAMENTO lre,  usuario u
WHERE lre.num_user_resp_teste = u.num_user_banco(+);
