SELECT aps.num_agenda_procedimento,
       aps.cod_paciente , --nullable
       aps.cod_tipo_consulta||' - '||tcs.dsc_tipo_consulta tipo_consulta, --nullable
       aps.cod_especialidade_hc||' - '||eh.nom_especialidade_hc especialidade_hc,
       aps.num_seq_local||' - '||ml.nom_local local_consulta,
       aps.dta_hor_agendamento,
       aps.dta_hor_consulta,
       aps.cod_situacao_pos_atend||' - '||spa.dsc_situacao_pos_atend situacao_pos_atend, --nullable
       aps.seq_movimentacao_paciente movimet_pac --nullable
    FROM AGENDA_PROCEDIMENTO_SUS aps
    LEFT JOIN TIPO_CONSULTA_SUS tcs ON aps.cod_tipo_consulta = tcs.cod_tipo_consulta    
    INNER JOIN ESPECIALIDADE_HC eh ON aps.cod_especialidade_hc = eh.cod_especialidade_hc
    INNER JOIN mapeamento_local ml ON aps.num_seq_local = ml.num_seq_local
    LEFT JOIN SITUACAO_POS_ATENDIMENTO spa ON aps.cod_situacao_pos_atend = spa.cod_situacao_pos_atend
WHERE aps.cod_paciente = '0578030F'
AND aps.cod_situacao_pos_atend <> 6;
