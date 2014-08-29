; Inno Setup Script created by Attila Abrudan.
; WorldWideAstronomy - WWA

[Setup]
OutputBaseFilename=WWA_Setup
WizardStyle=modern
Compression=lzma2/ultra
DisableStartupPrompt=1
;AlwaysRestart=yes
;;Minimum Windows XP
MinVersion=5.1
AlwaysShowDirOnReadyPage=yes
AppMutex=WWAMutex
CreateAppDir=yes

AppName=WWA
AppVerName=WWA v1.0
;AppPublisher=
AppPublisherURL=https://wwa.codeplex.com
AppSupportURL=https://wwa.codeplex.com
AppUpdatesURL=https://wwa.codeplex.com
DefaultDirName={pf}\WWA
DefaultGroupName=WWA
AllowNoIcons=yes
;AlwaysCreateUninstallIcon=yes

;LicenseFile=W:\!...\Setup\License.txt
;InfoBeforeFile=W:\!...\Setup\info before install.txt
;InfoAfterFile=W:\!...\Setup\info after install.txt
; uncomment the following line if you want your installation to run on NT 3.51 too.
; MinVersion=4,3.51

;COSMETIC
;Ha WindowVisible=Yes, akkor nagy kék hátterû az install!
WindowVisible=no
AppCopyright=Copyright (c) 2014 WWA
;BackColor=$E85A6B
;BackColor2=$931B29
WindowResizable=no
WindowShowCaption=no
;WizardSmallImageFile=
WizardImageFile=WizImg15.bmp

;UNINSTALL
Uninstallable=yes
CreateUninstallRegKey=yes

[Tasks]
Name: "desktopicon"; Description: "Create a &desktop icon"; GroupDescription: "Additional icons:"; MinVersion: 4,4
;Name: "quicklaunchicon"; Description: "Create a &Quick Launch icon"; GroupDescription: "Additional icons:"; MinVersion: 4,4; Flags: unchecked

[Files]
Source: "W:\!AA_Works\CLOUD\CodePlex\wwa\WorldWideAstronomy\WWATest\bin\Release\Copyright.txt"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "W:\!AA_Works\CLOUD\CodePlex\wwa\WorldWideAstronomy\WWATest\bin\Release\WWA.dll"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "W:\!AA_Works\CLOUD\CodePlex\wwa\WorldWideAstronomy\WWATest\bin\Release\WWATest.exe"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "W:\!AA_Works\CLOUD\CodePlex\wwa\WorldWideAstronomy\WWATest\bin\Release\WWATest.exe.config"; DestDir: "{app}"; CopyMode: alwaysoverwrite

[Run]

[Registry]
Root: HKLM; Subkey:"Software\WWA\"; Flags: uninsdeletekeyifempty
Root: HKLM; Subkey:"Software\WWA\KebPro"; flags: uninsdeletekey
Root: HKLM; Subkey:"Software\WWA\WWA"; Valuetype: string; ValueName: "ApplicationPath"; ValueData:"{app}\"
Root: HKLM; Subkey:"Software\WWA\WWA"; Valuetype: string; ValueName: "AppVersionNumber"; ValueData:"1.0"


[INI]
Filename: "{app}\WWA.url"; Section: "InternetShortcut"; Key: "URL"; String: "https://wwa.codeplex.com"; flags: uninsdeleteentry;

[Icons]
Name: "{group}\WWA"; Filename: "{app}\WWATest.exe"; WorkingDir: "{app}"; MinVersion:4,4
Name: "{group}\WWA on the Web"; Filename: "{app}\WWA.url"
Name: "{group}\Uninstall WWA"; Filename: "{uninstallexe}"
Name: "{userdesktop}\WWA"; Filename: "{app}\WWATest.exe"; WorkingDir: "{app}"; MinVersion:4,4; Tasks: desktopicon

[Run]
Filename: "{app}\WWATest.exe"; Description: "Launch WWA Test"; Flags: shellexec postinstall skipifsilent

[UninstallDelete]
Type: files; Name: "{app}\WWA.url"

