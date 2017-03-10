object Frm_bem_patrimonial: TFrm_bem_patrimonial
  Left = 0
  Top = 0
  BorderStyle = bsDialog
  Caption = 'Bem Patrim'#244'nial'
  ClientHeight = 255
  ClientWidth = 467
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnShow = FormShow
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 24
    Top = 37
    Width = 65
    Height = 13
    Caption = 'N'#186' Patrim'#244'nio'
  end
  object Label3: TLabel
    Left = 24
    Top = 93
    Width = 50
    Height = 13
    Caption = 'Nome Bem'
  end
  object Label4: TLabel
    Left = 16
    Top = 168
    Width = 69
    Height = 13
    Caption = 'Finalidade Uso'
  end
  object Label5: TLabel
    Left = 212
    Top = 168
    Width = 77
    Height = 13
    Caption = 'Restri'#231#227'o de OS'
  end
  object TLabel
    Left = 168
    Top = 37
    Width = 3
    Height = 13
  end
  object Label2: TLabel
    Left = 168
    Top = 37
    Width = 73
    Height = 13
    Caption = 'Tipo Patrim'#244'nio'
  end
  object cbTipoPatrimonio: TComboBox
    Left = 143
    Top = 187
    Width = 50
    Height = 21
    ItemHeight = 13
    TabOrder = 0
    OnChange = cbTipoPatrimonioChange
    Items.Strings = (
      'E'
      'A')
  end
  object btGravar: TButton
    Left = 399
    Top = 183
    Width = 58
    Height = 25
    Caption = 'Gravar'
    TabOrder = 1
    OnClick = btGravarClick
  end
  object dbEdtFinUso: TDBEdit
    Left = 16
    Top = 187
    Width = 121
    Height = 21
    DataField = 'FINALIDADE'
    DataSource = ds_bempat
    TabOrder = 2
  end
  object dbEdtNomeBem: TDBEdit
    Left = 24
    Top = 112
    Width = 433
    Height = 21
    DataField = 'DSC_COMPLEMENTAR'
    DataSource = ds_bempat
    TabOrder = 3
  end
  object dbEdtRestOS: TDBEdit
    Left = 212
    Top = 187
    Width = 121
    Height = 21
    DataField = 'RESTRICAO_OS'
    DataSource = ds_bempat
    TabOrder = 4
  end
  object cbRestOS: TComboBox
    Left = 339
    Top = 187
    Width = 50
    Height = 21
    ItemHeight = 13
    TabOrder = 5
    OnChange = cbRestOSChange
    Items.Strings = (
      'S'
      'N')
  end
  object dblkpTipoPatrimonio: TDBLookupComboBox
    Left = 168
    Top = 56
    Width = 145
    Height = 21
    KeyField = 'COD_TIPO_PATRIMONIO'
    ListField = 'DSC_TIPO_PATRIMONIO'
    ListSource = ds_tipopatrimonio
    TabOrder = 6
  end
  object edtNumPatrimonio: TEdit
    Left = 24
    Top = 56
    Width = 121
    Height = 21
    TabOrder = 7
    OnExit = edtNumPatrimonioExit
    OnKeyPress = edtNumPatrimonioKeyPress
  end
  object qry_bempat: TQuery
    DatabaseName = 'db_hcrp'
    SQL.Strings = (
      'SELECT NUM_BEM,'
      '       NUM_MANUTENCAO,'
      '       COD_TIPO_BEM,'
      '       NUM_SERIE,'
      '       NUM_PATRIMONIO,'
      '       COD_TIPO_PATRIMONIO,'
      '       SUBSTR(DSC_COMPLEMENTAR, 1, 250) DSC_COMPLEMENTAR,'
      '       COD_MARCA,'
      '       DSC_MARCA_PROVISORIA,'
      '       DSC_MODELO,'
      '       DSC_MODELO_PATRIMONIO,'
      '       CPL_LOCALIZACAO,'
      '       DTA_FABRICACAO,'
      '       DTA_VENCIMENTO_GARANTIA,'
      '       IDF_USO,'
      '       MOT_NAOUSO,'
      '       OBS_BEM,'
      '       NUM_INCORPORACAO,'
      '       TX_DEPRECIACAO_ESP_ANUAL,'
      '       TX_CORRECAO_MON_ESP_ANUAL,'
      '       IDF_DESINCORPORACAO,'
      '       DTA_DESINCORPORACAO,'
      '       OBS_DESINCORPORACAO,'
      '       NUM_USER_BANCO_DESINCORPORACAO,'
      '       COD_ESPECIE,'
      '       COD_INSTITUICAO,'
      '       NUM_DOCUMENTO_DESINCORPORACAO,'
      '       ANO_DOCUMENTO_DESINCORPORACAO,'
      '       NUM_BEM_PAI,'
      '       NUM_SEQ_RECEB_CEMB,'
      '       OBS_PATRIMONIO,'
      '       NUM_DOCUMENTO_DESINC_ANTIGO,'
      '       DTA_CHAPA_PATRIMONIO,'
      '       IDF_EMPRESTIMO,'
      '       CASE IDF_FINALIDADE_USO'
      '         WHEN '#39'E'#39' THEN '#39'ENS/PESQUISA'#39
      '         WHEN '#39'A'#39' THEN '#39'ASSIST'#202'NCIA'#39
      '         ELSE'
      '          '#39'SEM RESTRI'#199#195'O'#39
      '       END FINALIDADE,'
      
        '       CASE (SELECT itp.idf_restricao_os FROM INSTITUICAO_TIPO_P' +
        'ATRIMONIO itp'
      
        '                 WHERE itp.cod_tipo_patrimonio = bp.cod_tipo_pat' +
        'rimonio)         '
      '        WHEN '#39'1'#39' THEN '#39'SIM'#39' ELSE '#39'N'#195'O'#39'        '
      '        END RESTRICAO_OS          '
      '  FROM BEM_PATRIMONIAL BP'
      ' WHERE BP.NUM_PATRIMONIO = :NUM_PATRIMONIO'
      '   AND BP.COD_TIPO_PATRIMONIO = :COD_TIPO_PATRIMONIO')
    Left = 416
    Top = 8
    ParamData = <
      item
        DataType = ftFloat
        Name = 'NUM_PATRIMONIO'
        ParamType = ptInput
      end
      item
        DataType = ftFloat
        Name = 'COD_TIPO_PATRIMONIO'
        ParamType = ptInput
      end>
    object qry_bempatNUM_BEM: TFloatField
      FieldName = 'NUM_BEM'
    end
    object qry_bempatNUM_MANUTENCAO: TFloatField
      FieldName = 'NUM_MANUTENCAO'
    end
    object qry_bempatCOD_TIPO_BEM: TFloatField
      FieldName = 'COD_TIPO_BEM'
    end
    object qry_bempatNUM_SERIE: TStringField
      FieldName = 'NUM_SERIE'
      Size = 100
    end
    object qry_bempatNUM_PATRIMONIO: TFloatField
      FieldName = 'NUM_PATRIMONIO'
    end
    object qry_bempatCOD_TIPO_PATRIMONIO: TFloatField
      FieldName = 'COD_TIPO_PATRIMONIO'
    end
    object qry_bempatDSC_COMPLEMENTAR: TStringField
      FieldName = 'DSC_COMPLEMENTAR'
      Size = 250
    end
    object qry_bempatCOD_MARCA: TFloatField
      FieldName = 'COD_MARCA'
    end
    object qry_bempatDSC_MARCA_PROVISORIA: TStringField
      FieldName = 'DSC_MARCA_PROVISORIA'
      Size = 30
    end
    object qry_bempatDSC_MODELO: TStringField
      FieldName = 'DSC_MODELO'
      Size = 30
    end
    object qry_bempatDSC_MODELO_PATRIMONIO: TStringField
      FieldName = 'DSC_MODELO_PATRIMONIO'
      Size = 30
    end
    object qry_bempatCPL_LOCALIZACAO: TStringField
      FieldName = 'CPL_LOCALIZACAO'
      Size = 150
    end
    object qry_bempatDTA_FABRICACAO: TDateTimeField
      FieldName = 'DTA_FABRICACAO'
    end
    object qry_bempatDTA_VENCIMENTO_GARANTIA: TDateTimeField
      FieldName = 'DTA_VENCIMENTO_GARANTIA'
    end
    object qry_bempatIDF_USO: TStringField
      FieldName = 'IDF_USO'
      Size = 1
    end
    object qry_bempatMOT_NAOUSO: TStringField
      FieldName = 'MOT_NAOUSO'
      Size = 50
    end
    object qry_bempatOBS_BEM: TMemoField
      FieldName = 'OBS_BEM'
      BlobType = ftMemo
      Size = 1000
    end
    object qry_bempatNUM_INCORPORACAO: TFloatField
      FieldName = 'NUM_INCORPORACAO'
    end
    object qry_bempatTX_DEPRECIACAO_ESP_ANUAL: TFloatField
      FieldName = 'TX_DEPRECIACAO_ESP_ANUAL'
    end
    object qry_bempatTX_CORRECAO_MON_ESP_ANUAL: TFloatField
      FieldName = 'TX_CORRECAO_MON_ESP_ANUAL'
    end
    object qry_bempatIDF_DESINCORPORACAO: TStringField
      FieldName = 'IDF_DESINCORPORACAO'
      Size = 1
    end
    object qry_bempatDTA_DESINCORPORACAO: TDateTimeField
      FieldName = 'DTA_DESINCORPORACAO'
    end
    object qry_bempatOBS_DESINCORPORACAO: TStringField
      FieldName = 'OBS_DESINCORPORACAO'
      Size = 80
    end
    object qry_bempatNUM_USER_BANCO_DESINCORPORACAO: TFloatField
      FieldName = 'NUM_USER_BANCO_DESINCORPORACAO'
    end
    object qry_bempatCOD_ESPECIE: TFloatField
      FieldName = 'COD_ESPECIE'
    end
    object qry_bempatCOD_INSTITUICAO: TFloatField
      FieldName = 'COD_INSTITUICAO'
    end
    object qry_bempatNUM_DOCUMENTO_DESINCORPORACAO: TFloatField
      FieldName = 'NUM_DOCUMENTO_DESINCORPORACAO'
    end
    object qry_bempatANO_DOCUMENTO_DESINCORPORACAO: TFloatField
      FieldName = 'ANO_DOCUMENTO_DESINCORPORACAO'
    end
    object qry_bempatNUM_BEM_PAI: TFloatField
      FieldName = 'NUM_BEM_PAI'
    end
    object qry_bempatNUM_SEQ_RECEB_CEMB: TFloatField
      FieldName = 'NUM_SEQ_RECEB_CEMB'
    end
    object qry_bempatOBS_PATRIMONIO: TStringField
      FieldName = 'OBS_PATRIMONIO'
      Size = 80
    end
    object qry_bempatNUM_DOCUMENTO_DESINC_ANTIGO: TStringField
      FieldName = 'NUM_DOCUMENTO_DESINC_ANTIGO'
      Size = 10
    end
    object qry_bempatDTA_CHAPA_PATRIMONIO: TDateTimeField
      FieldName = 'DTA_CHAPA_PATRIMONIO'
    end
    object qry_bempatIDF_EMPRESTIMO: TFloatField
      FieldName = 'IDF_EMPRESTIMO'
    end
    object qry_bempatFINALIDADE: TStringField
      FieldName = 'FINALIDADE'
      Size = 13
    end
    object qry_bempatRESTRICAO_OS: TStringField
      FieldName = 'RESTRICAO_OS'
      FixedChar = True
      Size = 3
    end
  end
  object ds_bempat: TDataSource
    DataSet = qry_bempat
    Left = 360
    Top = 8
  end
  object ds_tipopatrimonio: TDataSource
    DataSet = qry_tipopat
    Left = 360
    Top = 64
  end
  object qry_tipopat: TQuery
    DatabaseName = 'db_hcrp'
    SQL.Strings = (
      'SELECT TP.COD_TIPO_PATRIMONIO, TP.DSC_TIPO_PATRIMONIO'
      '  FROM TIPO_PATRIMONIO TP'
      ' INNER JOIN USUARIO_TIPO_PATRIMONIO UTP'
      '    ON UTP.COD_TIPO_PATRIMONIO = TP.COD_TIPO_PATRIMONIO'
      '   AND UTP.NUM_USER_BANCO = :NUM_USER_BANCO'
      ' INNER JOIN INSTITUICAO_TIPO_PATRIMONIO ITP'
      '    ON ITP.COD_TIPO_PATRIMONIO = TP.COD_TIPO_PATRIMONIO'
      '   AND ITP.COD_INST_SISTEMA = :COD_INST_SISTEMA'
      ' ORDER BY TP.DSC_TIPO_PATRIMONIO')
    Left = 400
    Top = 64
    ParamData = <
      item
        DataType = ftFloat
        Name = 'NUM_USER_BANCO'
        ParamType = ptInput
      end
      item
        DataType = ftInteger
        Name = 'COD_INST_SISTEMA'
        ParamType = ptInput
      end>
    object qry_tipopatCOD_TIPO_PATRIMONIO: TFloatField
      FieldName = 'COD_TIPO_PATRIMONIO'
    end
    object qry_tipopatDSC_TIPO_PATRIMONIO: TStringField
      FieldName = 'DSC_TIPO_PATRIMONIO'
      Size = 10
    end
  end
  object ds_btgravar: TDataSource
    Left = 328
    Top = 208
  end
  object qry_aux: TQuery
    DatabaseName = 'db_hcrp'
    Left = 384
    Top = 208
  end
end
