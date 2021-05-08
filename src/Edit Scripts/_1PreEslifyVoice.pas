unit _1PreEslifyVoice;

interface
  implementation
  uses xEditAPI, _wotrFunctions, Classes, SysUtils, StrUtils, Windows;

var slPreEslify : TStringList;

function Initialize: integer;
begin
  slPreEslify := TStringList.Create;
  slPreEslify.Add('EspName;FormID;compairision;');
end;

function Process(e: IInterface): integer;
var compairision :string;
begin
  if Signature(e) = 'INFO' then 
  begin
    compairision:= EditValueWithoutFormID(GetElementEditValues(e, 'Topic')) + ':' + GetElementEditValues(ElementByIndex(ElementByPath(e, 'Responses'), 0), 'NAM1 - Response Text');
    if IsMaster(e) = false then Exit;
    slPreEslify.Add(Format('%s;%s;%s;', [
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
  if  Assigned(slPreEslify) then 
  begin
    if  (slPreEslify.Count > 1) then begin
      slPreEslify.SaveToFile(ProgramPath+'VoiceEslify\xEditOutput\_1PreEslify.csv');
    end;
    slPreEslify.Free;
  end;
 end;

end.