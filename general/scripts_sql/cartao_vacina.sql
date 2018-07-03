/*Ver Cartão de vácina*/
select 
  (SELECT tp.dsc_tipo_vacina FROM tipo_vacina tp
  WHERE tp.cod_tipo_vacina = t.cod_tipo_vacina) dsc_vacina,
t.*
 from VACINACAO t
where t.cod_paciente_vacinacao IN 
(select t.cod_paciente_vacinacao from PACIENTE_VACINACAO t
where t.cod_paciente = '1340268A');
