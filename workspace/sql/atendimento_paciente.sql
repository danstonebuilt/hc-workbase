SELECT ap.seq_atendimento,
       ap.cod_paciente, --nullable
       ap.cod_tipo_atendimento||' - '||ta.nom_tipo_atendimento tipo_atendimento,
       ap.dta_hor_cadastro,
       ap.dta_hor_abertura,
       ap.dta_hor_fechamento,
       ap.cod_convenio||' - '||c.nom_convenio convenio
  FROM atendimento_paciente ap
  INNER JOIN TIPO_ATENDIMENTO_HC ta ON ap.cod_tipo_atendimento = ta.cod_tipo_atendimento
  INNER JOIN CONVENIO c ON ap.cod_convenio = c.cod_convenio
WHERE ap.cod_paciente = '0578030F';
