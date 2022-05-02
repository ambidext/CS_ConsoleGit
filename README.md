@echo off&setlocal
set base=%~dp0

start %base%SP_TEST.EXE %base%Proxy-1.json
start %base%SP_TEST.EXE %base%Proxy-2.json
start %base%SP_TEST.EXE %base%Proxy-3.json
