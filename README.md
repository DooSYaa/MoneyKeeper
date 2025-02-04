# MoneyKeeper
 
1. Install projecj

Obowiązkowa obecność .NET SDK (projekt został napisany w wersji 8).

Aby zainstalować frameworki, użyj: dotnet restore

Przed utworzeniem bazy danych zmień dane w pliku appsettings.json, dodając tam swoją bazę.

Następnie utwórz bazę danych, a do przeniesienia tabel użyj migracji:

dotnet ef migrations add migration_name
dotnet ef database update
