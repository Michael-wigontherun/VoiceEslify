unit _2PostEslifyVoice;

interface
  implementation
  uses xEditAPI, _wotrFunctions, Classes, SysUtils, StrUtils, Windows;

var slPostEslify : TStringList;

function Initialize: integer;
begin
  slPostEslify := TStringList.Create;
  slPostEslify.Add('EspName;FormID;compairision;');
end;

function Process(e: IInterface): integer;
var compairision :string;
begin
  if Signature(e) = 'INFO' then 
  begin
    compairision:= EditValueWithoutFormID(GetElementEditValues(e, 'Topic')) + ':' + GetElementEditValues(ElementByIndex(ElementByPath(e, 'Responses'), 0), 'NAM1 - Response Text');
    if IsMaster(e) = false then Exit;
    slPostEslify.Add(Format('%s;%s;%s;', [
      Name(GetFile(e)),
      GetStringFormID(e),
      compairision
    ]));
  end;
end;

function Finalize: integer;
var folderDirectory : TStringList;
begin
  folderDirectory := TStringList.Create;
  folderDirectory.add('VoiceEslify');
  folderDirectory.add('VoiceEslify\xEditOutput');
  createOutputFolder(folderDirectory);
  if  Assigned(slPostEslify) then 
  begin
    if  (slPostEslify.Count > 1) then begin
      slPostEslify.SaveToFile(ProgramPath+'VoiceEslify\xEditOutput\_2PostEslify.csv');
    end;
    slPostEslify.Free;
  end;
 end;

end.