@ECHO OFF
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

@ECHO ========================================
@ECHO    Compiling Practice# WiX Installation
@ECHO ========================================

ECHO+
ECHO Copying latest Release files to Deploy folder
XCOPY /Y ..\PracticeSharpApp\bin\Release\PracticeSharp.exe Deploy\
XCOPY /Y ..\PracticeSharpApp\bin\Release\PracticeSharp.exe.config Deploy\
XCOPY /Y ..\PracticeSharpApp\bin\Release\NAudio.dll Deploy\
XCOPY /Y ..\PracticeSharpApp\bin\Release\NAudio.WindowsMediaFormat.dll Deploy\
XCOPY /Y ..\PracticeSharpApp\bin\Release\NAudioOggVorbis.dll Deploy\
XCOPY /Y ..\PracticeSharpApp\bin\Release\libFlac.dll Deploy\
XCOPY /Y ..\PracticeSharpApp\bin\Release\NAudioFLAC.dll Deploy\
XCOPY /Y ..\PracticeSharpApp\bin\Release\SoundTouch.dll Deploy\
XCOPY /Y ..\PracticeSharpApp\bin\Release\LICENSES.txt Deploy\
XCOPY /Y ..\PracticeSharpApp\bin\Release\NLog.dll Deploy\
XCOPY /Y ..\PracticeSharpApp\bin\Release\NLog.config Deploy\

REM NOTE: ** Download once wix311-binaries.zip and unzip it into this folder, before running this batch file ** 
REM Download URL: http://wix.sourceforge.net/
SET PATH=%PATH%;.\wix311-binaries

ECHO+
ECHO ==== Candle ====
candle.exe PracticeSharp.wxs -out obj\PracticeSharp.wixobj

ECHO+
ECHO ==== Light ====
light.exe -ext WixUIExtension obj\PracticeSharp.wixobj -out MSI\PracticeSharp.msi

pause