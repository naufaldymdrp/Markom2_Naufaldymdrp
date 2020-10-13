# Markom2_Naufaldymdrp
Pengerjaan proyek Markom 2 di Xsis

## Keterangan 
Tidak perlu Visual Studio 2019 untuk menjalankan program ini.

### Syarat runtime
> minimal .NET Core SDK 3.1.401

## Berikut adalah 2 cara restore packages
### Melalui .NET Core CLI
1. Lakukan cd (pindah folder) hingga ke tingkat sln
2. Kemudian ketik dan eksekusi 'dotnet tool restore' untuk melakukan restore dotnet tool
3. Kemudian ketik dan eksekusi 'dotnet restore' untuk melakukan restore package seluruh .csproj
4. Kemudian ketik dan eksekusi 'dotnet ef database update -p Markom2.Repository/Markom2.Repository.csproj -s Markom2.Web/Markom2.Web.csproj' untuk melakukan migrasi skema ke database, \n
   jangan lupa untuk ubah 'connection string' di file appsettings.json
5. lakukan 'dotnet run' untuk menjalankan program
6. Lakukan pendaftaran terlebih dahulu
7. Apabila melakukan git pull, maka lakukan langkah no. 4 karena mungkin ada perubahan skema database
