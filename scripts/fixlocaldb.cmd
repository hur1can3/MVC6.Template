net localgroup Administrators "%userdomain%\%username%" /add

net localgroup Users "%userdomain%\%username%" /add

sqllocaldb d mssqllocaldb

rd /s/q "%localappdata%\Microsoft\Microsoft SQL Server LocalDB\Instances\mssqllocaldb"

sqllocaldb c mssqllocaldb -s