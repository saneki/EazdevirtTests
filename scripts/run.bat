:: Obfuscates, deobfuscates, devirtualizes and tests EazvirtMe.exe
:: This script expects certain programs to be available on the PATH

@echo off
cls

echo [1.] Obfuscating + Virtualizing
echo -------------------------------
Eazfuscator.NET.exe EazvirtMe.exe
echo.

echo [2.] Deobfuscating
echo ------------------
de4dot --dont-rename --keep-types --preserve-tokens EazvirtMe.exe --strtyp none
echo.

echo [3.] Devirtualizing
echo -------------------
eazdevirt -d -o EazvirtMe-devirt.exe EazvirtMe-cleaned.exe
echo.

echo [4.] Testing (NUnit)
echo --------------------
nunit-console EazvirtMe-devirt.exe
echo.

echo [5.] Checking (peverify)
echo ------------------------
peverify EazvirtMe-devirt.exe /IL /MD /VERBOSE
echo.
