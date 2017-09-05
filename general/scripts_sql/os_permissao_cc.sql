SELECT * FROM centro_custo cc
WHERE cc.cod_cencusto = 'CAAA00164';

SELECT ccu.*,
       u.*
       FROM
    centro_custo_usuario ccu
    JOIN usuario u
    ON ccu.num_user_banco = u.num_user_banco
WHERE ccu.cod_cencusto = 'CAAA00164';


--51859
SELECT * FROM centro_custo_usuario_permissao c
WHERE c.num_user_banco = 66827 ;

SELECT * FROM PER_CENTRO_CUSTO_USUARIO p
WHERE p.num_seq_permissao IN (43570,43403);
--WHERE p.num_seq_permissao IN (17488,17489,17490,17491,17492,28311);
