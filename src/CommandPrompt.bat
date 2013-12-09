@CD /D "%~dp0"
@title WebSocketTest Command Prompt
@SET PATH=C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\;%PATH%
@doskey build=msbuild WebSocketTest.proj $*
@doskey run=msbuild WebSocketTest.proj /t:Run $*
@type ..\readme.md
@%comspec%
