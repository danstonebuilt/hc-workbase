{*************************************************************************************
AUTOR: DANIEL ANSELMO 05/12/2016
DEMANDA: [GDTI-3196]
ITEM: Nova tela de bem patrimônial, afim de poder alterar/atualizar patrimônios legados
com/sem status de ASSISTÊNCIA ou ocorrência de OS
***************************************************************************************}
unit u_bem_patrimonial;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, Mask, DBCtrls, DB, DBTables;

type
  TFrm_bem_patrimonial = class(TForm)
    cbTipoPatrimonio: TComboBox;
    Label1: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    btGravar: TButton;
    dbEdtFinUso: TDBEdit;
    dbEdtNomeBem: TDBEdit;
    dbEdtRestOS: TDBEdit;
    Label5: TLabel;
    cbRestOS: TComboBox;
    qry_bempat: TQuery;
    ds_bempat: TDataSource;
    Label2: TLabel;
    ds_tipopatrimonio: TDataSource;
    qry_tipopat: TQuery;
    dblkpTipoPatrimonio: TDBLookupComboBox;
    edtNumPatrimonio: TEdit;
    qry_bempatNUM_BEM: TFloatField;
    qry_bempatNUM_MANUTENCAO: TFloatField;
    qry_bempatCOD_TIPO_BEM: TFloatField;
    qry_bempatNUM_SERIE: TStringField;
    qry_bempatNUM_PATRIMONIO: TFloatField;
    qry_bempatCOD_TIPO_PATRIMONIO: TFloatField;
    qry_bempatDSC_COMPLEMENTAR: TStringField;
    qry_bempatCOD_MARCA: TFloatField;
    qry_bempatDSC_MARCA_PROVISORIA: TStringField;
    qry_bempatDSC_MODELO: TStringField;
    qry_bempatDSC_MODELO_PATRIMONIO: TStringField;
    qry_bempatCPL_LOCALIZACAO: TStringField;
    qry_bempatDTA_FABRICACAO: TDateTimeField;
    qry_bempatDTA_VENCIMENTO_GARANTIA: TDateTimeField;
    qry_bempatIDF_USO: TStringField;
    qry_bempatMOT_NAOUSO: TStringField;
    qry_bempatOBS_BEM: TMemoField;
    qry_bempatNUM_INCORPORACAO: TFloatField;
    qry_bempatTX_DEPRECIACAO_ESP_ANUAL: TFloatField;
    qry_bempatTX_CORRECAO_MON_ESP_ANUAL: TFloatField;
    qry_bempatIDF_DESINCORPORACAO: TStringField;
    qry_bempatDTA_DESINCORPORACAO: TDateTimeField;
    qry_bempatOBS_DESINCORPORACAO: TStringField;
    qry_bempatNUM_USER_BANCO_DESINCORPORACAO: TFloatField;
    qry_bempatCOD_ESPECIE: TFloatField;
    qry_bempatCOD_INSTITUICAO: TFloatField;
    qry_bempatNUM_DOCUMENTO_DESINCORPORACAO: TFloatField;
    qry_bempatANO_DOCUMENTO_DESINCORPORACAO: TFloatField;
    qry_bempatNUM_BEM_PAI: TFloatField;
    qry_bempatNUM_SEQ_RECEB_CEMB: TFloatField;
    qry_bempatOBS_PATRIMONIO: TStringField;
    qry_bempatNUM_DOCUMENTO_DESINC_ANTIGO: TStringField;
    qry_bempatDTA_CHAPA_PATRIMONIO: TDateTimeField;
    qry_bempatIDF_EMPRESTIMO: TFloatField;
    qry_bempatFINALIDADE: TStringField;
    qry_bempatRESTRICAO_OS: TStringField;
    ds_btgravar: TDataSource;
    qry_aux: TQuery;
    qry_tipopatCOD_TIPO_PATRIMONIO: TFloatField;
    qry_tipopatDSC_TIPO_PATRIMONIO: TStringField;
    procedure FormShow(Sender: TObject);
    procedure edtNumPatrimonioExit(Sender: TObject);
    procedure cbTipoPatrimonioChange(Sender: TObject);
    procedure cbRestOSChange(Sender: TObject);
    procedure btGravarClick(Sender: TObject);
    function validarAlteracao: Boolean;
    procedure edtNumPatrimonioKeyPress(Sender: TObject; var Key: Char);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Frm_bem_patrimonial: TFrm_bem_patrimonial;

implementation

uses U_Principal, u_Funcoes;

{$R *.dfm}


procedure TFrm_bem_patrimonial.btGravarClick(Sender: TObject);
begin
     if validarAlteracao then
         ShowMessage('Alteração feita com sucesso!');
end;

procedure TFrm_bem_patrimonial.cbRestOSChange(Sender: TObject);
begin
    if cbRestOS.Items[cbRestOS.ItemIndex] = 'S' then
    begin
        dbEdtRestOS.Text := 'SIM';
    end
    else
    begin
         dbEdtRestOS.Text := 'NÃO';
    end;
end;

procedure TFrm_bem_patrimonial.cbTipoPatrimonioChange(Sender: TObject);
var l_tipo_patrimonio: Char;
begin
    if cbTipoPatrimonio.Items[cbTipoPatrimonio.ItemIndex] = 'A' then
    begin
        dbEdtFinUso.Text := 'ASSISTÊNCIA';
    end
    else
    begin
         dbEdtFinUso.Text := 'ENS/PESQUISA';
    end;
end;

procedure TFrm_bem_patrimonial.edtNumPatrimonioExit(Sender: TObject);
begin
   if dblkpTipoPatrimonio.KeyValue = null then
   begin
     ShowMessage('Tipo de patrimonio deve ser informado.');
     Abort;
   end;
   
   with  qry_bempat do
  begin
      if Active then
      begin
        Close;
      end;

      ParamByName('NUM_PATRIMONIO').Value := edtNumPatrimonio.Text;
      ParamByName('COD_TIPO_PATRIMONIO').Value := StrToFloat(VarToStr(dblkpTipoPatrimonio.KeyValue));
      Open;
  end;

end;

procedure TFrm_bem_patrimonial.edtNumPatrimonioKeyPress(Sender: TObject; var Key: Char);
begin
    enterAsTab(Self, Key);
end;

procedure TFrm_bem_patrimonial.FormShow(Sender: TObject);
begin
 with qry_tipopat do
 begin
        if active then
        begin
            Close;
        end;

        ParamByName('NUM_USER_BANCO').Value := Frm_Principal.NumUserBanco;
        ParamByName('COD_INST_SISTEMA').Value := VS_cod_inst_sistema;
      Open;
  end;
end;

function TFrm_bem_patrimonial.validarAlteracao: Boolean;
var l_result: Integer;
begin

    l_result := 0;

    if cbTipoPatrimonio.ItemIndex  > -1 then
    begin
       with qry_aux do
       begin
           if Active then Close;
           SQL.Clear;
           SQL.Add('UPDATE BEM_PATRIMONIAL bp ');
           SQL.Add('SET  bp.idf_finalidade_uso = ' + QuotedStr(cbTipoPatrimonio.Items[cbTipoPatrimonio.ItemIndex]));
           SQL.Add('WHERE bp.num_patrimonio = ' + qry_bempatNUM_PATRIMONIO.AsString);
           SQL.Add('AND bp.cod_tipo_patrimonio = ' + qry_tipopatCOD_TIPO_PATRIMONIO.AsString);
           ExecSQL;
           Close;

           l_result := l_result + 1; //Operação passou
       end;
    end;
    if cbRestOS.ItemIndex > -1  then
    begin
        with qry_aux do
       begin
           if Active then Close;
           SQL.Clear;
           SQL.Add('UPDATE INSTITUICAO_TIPO_PATRIMONIO itp ');

           //Se tiver restrição
           if cbRestOS.Items[cbRestOS.ItemIndex] = 'S' then
           begin
              SQL.Add('SET  itp.idf_restricao_os = ' + IntToStr(cbRestOS.ItemIndex + 1));
           end
           else
           begin
              SQL.Add('SET  itp.idf_restricao_os = ' + IntToStr( cbRestOS.ItemIndex - 1));
           end;
           SQL.Add('WHERE itp.cod_tipo_patrimonio = '+ qry_tipopatCOD_TIPO_PATRIMONIO.AsString );
           ExecSQL;
           Close;

           l_result := l_result + 1; //Operação passou
       end;
    end;

    if l_result > 0 then
       result := True;
end;
 end.

