REM Practice Sharp
REM 
REM    © Copyright 2010, Yuval Naveh.
REM     All rights reserved.
REM
REM    This file is part of Practice Sharp.
REM
REM    Practice Sharp is free software: you can redistribute it and/or modify
REM    it under the terms of the GNU Lesser Public License as published by
REM    the Free Software Foundation, either version 3 of the License, or
REM    (at your option) any later version.
REM
REM    Practice Sharp is distributed in the hope that it will be useful,
REM    but WITHOUT ANY WARRANTY; without even the implied warranty of
REM    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
REM    GNU Lesser Public License for more details.
REM
REM    You should have received a copy of the GNU Lesser Public License
REM    along with Practice Sharp.  If not, see <http://www.gnu.org/licenses/>.
REM

@ECHO OFF

@ECHO ========================================
@ECHO    Compiling Practice# WiX Installation
@ECHO ========================================

ECHO Copying latest Release files
XCOPY /Y ..\PracticeSharpApp\bin\Release\PracticeSharp.exe Source\
XCOPY /Y ..\PracticeSharpApp\bin\Release\PracticeSharp.exe.config Source\
XCOPY /Y ..\PracticeSharpApp\bin\Release\NAudio.dll Source\
XCOPY /Y ..\PracticeSharpApp\bin\Release\NAudio.WindowsMediaFormat.dll Source\
XCOPY /Y ..\PracticeSharpApp\bin\Release\NAudioOggVorbis.dll Source\
XCOPY /Y ..\PracticeSharpApp\bin\Release\SoundTouch.dll Source\

SET PATH=%PATH%;C:\Program Files (x86)\Windows Installer XML v3\bin
ECHO ==== Candle ====
candle.exe PracticeSharp.wxs -out obj\PracticeSharp.wixobj
ECHO ==== Light ====
light.exe -ext WixUIExtension obj\PracticeSharp.wixobj -out MSI\PracticeSharp.msi

pause