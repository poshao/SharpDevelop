@rem  /p:AdditionalBuildProperties="/v:d /p:MSBuildTargetsVerbose=true"
%windir%\microsoft.net\framework\v4.0.30319\msbuild src\Automated.proj /t:buildsamples /p:ArtefactsOutputDir="%CD%\build" /p:TestReportsDir="%CD%\build"
@IF %ERRORLEVEL% NEQ 0 GOTO err
@exit /B 0
:err
@PAUSE
@exit /B 1