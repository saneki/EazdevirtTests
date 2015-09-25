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
eazdevirt -dj -o EazvirtMe-devirt.exe EazvirtMe-cleaned.exe
echo.

echo [4.] Cleaning
echo -------------
de4dot -f EazvirtMe-devirt.exe -o EazvirtMe-fixed.exe
echo.

echo [5.] Testing (NUnit)
echo --------------------
nunit-console EazvirtMe-fixed.exe
echo.

echo [6.] Checking (peverify)
echo ------------------------
peverify EazvirtMe-fixed.exe /IL /MD /VERBOSE
echo.
