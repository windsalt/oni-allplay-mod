@echo off
setlocal enabledelayedexpansion

:: ========== 配置项 ==========
set "PROJECT_NAME=all-play"
set "MOD_DIR=%USERPROFILE%\Documents\Klei\OxygenNotIncluded\mods\Dev\!PROJECT_NAME!"
set "BUILD_OUTPUT=bin\Debug\netstandard2.1"
:: ===========================

if "%~1"=="" (
  echo Usage: build.bat [build^|dev]
  echo  build - 仅编译项目
  echo  dev  - 编译并复制到Mod目录
  pause
  exit /b 1
)

if /i "%~1"=="build" (
  echo Building %PROJECT_NAME% project...
  dotnet build %PROJECT_NAME%.csproj --configuration Debug --output !BUILD_OUTPUT!
  
  if %errorlevel% equ 0 (
    echo Build succeeded!
    
    echo Creating temp directory for zip...
    set "ZIP_TEMP_DIR=!TEMP!\!PROJECT_NAME!_zip_temp"
    if exist "!ZIP_TEMP_DIR!" (
      rmdir /s /q "!ZIP_TEMP_DIR!"
    )
    mkdir "!ZIP_TEMP_DIR!"
    
    echo Copying files to temp directory...
    copy "mod_info.yaml" "!ZIP_TEMP_DIR!\"
    copy "model.yaml" "!ZIP_TEMP_DIR!\"
    copy "!BUILD_OUTPUT!\%PROJECT_NAME%.dll" "!ZIP_TEMP_DIR!\"
    copy "!BUILD_OUTPUT!\%PROJECT_NAME%.pdb" "!ZIP_TEMP_DIR!\"
    
    echo Creating zip package...
    powershell -Command "Compress-Archive -Path '!ZIP_TEMP_DIR!\*' -DestinationPath '!PROJECT_NAME!.zip' -Force"
    
    echo Cleaning up temp directory...
    rmdir /s /q "!ZIP_TEMP_DIR!"
    
    echo Zip package created: !PROJECT_NAME!.zip
    ) else (
    echo Build failed with error code: %errorlevel%
    pause
  )
  
  exit /b %errorlevel%
)

if /i "%~1"=="dev" (
  :: 先停止游戏
  powershell -Command "if (Get-Process -Name 'OxygenNotIncluded' -ErrorAction SilentlyContinue) { exit 0 } else { exit 1 }"
  if !errorlevel! equ 0 (
    echo Stopping existing game...
    taskkill /IM OxygenNotIncluded.exe /F >nul 2>&1
    timeout /t 3 /nobreak >nul
    echo Game stopped.
  )
  
  echo Building %PROJECT_NAME% project...
  dotnet build %PROJECT_NAME%.csproj --configuration Debug --output !BUILD_OUTPUT!
  
  if %errorlevel% equ 0 (
    echo Build succeeded!
    
    echo Creating mod directory: !MOD_DIR!
    if not exist "!MOD_DIR!" (
      mkdir "!MOD_DIR!"
    )
    
    echo Copying yaml files to !MOD_DIR!
    copy "mod_info.yaml" "!MOD_DIR!\"
    copy "model.yaml" "!MOD_DIR!\"
    
    echo Copying dll files to !MOD_DIR!
    copy "!BUILD_OUTPUT!\%PROJECT_NAME%.dll" "!MOD_DIR!\"
    copy "!BUILD_OUTPUT!\%PROJECT_NAME%.pdb" "!MOD_DIR!\"
    
    echo All files copied successfully!
    
    echo Starting game via Steam...
    start steam://rungameid/457140
    timeout /t 5 /nobreak >nul
    echo Game launched successfully.
    ) else (
    echo Build failed with error code: %errorlevel%
    pause
  )
  
  exit /b %errorlevel%
)

echo Invalid argument: "%~1"
echo Usage: build.bat [build^|dev]
pause
exit /b 1

endlocal
