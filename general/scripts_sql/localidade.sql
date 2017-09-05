/*County location*/
select * from GENERICO.LOCALIDADE t
where t.nom_localidade LIKE '%RIB%'
AND t.sgl_uf = 'SP'
