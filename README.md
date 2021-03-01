# RiseTech

# ASP.NET Core İçin Adımlar

İlk olarak proje git üzerinden clone alınır
--
Sonrasında proje üzerinde NuGet Packages, Restore Packages ile kontrolü sağlanıp eksik olanlar varsa projeye include edilir
--
Sonrasında Startup projesi olarak entity projemiz Data->Sağ tık->Set as Startup Project(Başlangıç Projesi Olarak Ayarla) ile seçilir
--
Sonrasında ise local SQL Express kurulum aşamasında bir Instance ismi tanımlanmadı veya SQLExpress haricinde bir Instance adı kullanıdı ise;
Data->Models->RiseTechDBContext.cs içerisinde 26. satır connection string belirtilen alandaki Server=.\\SQLEXPRESS olan kısım Server=. olarak düzeltilmeli
--
Elbette bu işlemi WebAPI ismindeki api projemizdeki appsettings.json içerisindeki ConnectionString jobject nesnesi içerisindeki DefaultConnection property'sine karşılık gelen
valuedeki connection string de değiştirilmeli
--
Diğer bir adımımız ise context ve modellerimizden Database'imizi oluşturmak bunun için
Tools(Araçlar)->NuGet Package Manager(NuGet Paket Yöneticisi)->Package Manager Console(Paket Yöneticisi Konsolu)'u başlatmak
--
Sonrasında ise açılan paket yöneticisi konsolundan add-migration "migration ismi" verip komutu çalıştırmak
İşlem tamamlanıp Build success olduğunda update-database ile son database yapısını SQL Server'a aktarmak olucak
--
Başlangıç projemiz şuan Entity yapısı olan Data projemizde Web API'mizi ayağa kaldırmak için ise WebAPI projemizi startup'a çekelim
--
Flutter projemizde WebAPI'imizi localhost url'i ile isteklerimize yanıt alamayacağımız için
Uygulamamızın url ve start ayarlarını düzeltmemiz gerekiyor (local SSL'de kapalı olmalı)

Öncelikle Windows konsoldan ipconfig /all ile internal ip adresimizi alalım
![alt text](https://raw.githubusercontent.com/metintaslik/RiseTech/master/ipconfig.png)

Daha sonrasında WebAPI projemizdeki properties kısmına giderek düzenleme işlemini gerçekleştirelim (iki görseldeki gibi de düzenleyebiliriz)
![alt text](https://raw.githubusercontent.com/metintaslik/RiseTech/master/launchSettings.png)
![alt text](https://raw.githubusercontent.com/metintaslik/RiseTech/master/properties.png)
--
Sonrasında ise WebAPI projemizi ayağa kaldıralım
--

