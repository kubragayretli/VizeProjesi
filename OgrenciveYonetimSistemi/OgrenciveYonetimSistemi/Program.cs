using System;
using System.Collections.Generic;

// Temel sınıf
public abstract class Kisi
{
    public int Id { get; set; }
    public string Ad { get; set; }
    public string Soyad { get; set; }

    public abstract void BilgiGoster();
}

// Interface
public interface ILogin
{
    void Login();
}

// Öğrenci sınıfı
public class Ogrenci : Kisi, ILogin
{
    public List<Ders> Dersler { get; set; } = new List<Ders>();

    public void Login()
    {
        Console.WriteLine($"{Ad} {Soyad} öğrenci sistemine giriş yaptı.");
    }

    public override void BilgiGoster()
    {
        Console.WriteLine($"Öğrenci: {Ad} {Soyad}, ID: {Id}, Kayıtlı Ders Sayısı: {Dersler.Count}");
    }
}

// Öğretim Görevlisi sınıfı
public class OgretimGorevlisi : Kisi, ILogin
{
    public List<Ders> VerdigiDersler { get; set; } = new List<Ders>();

    public void Login()
    {
        Console.WriteLine($"{Ad} {Soyad} öğretim görevlisi sistemine giriş yaptı.");
    }

    public override void BilgiGoster()
    {
        Console.WriteLine($"Öğretim Görevlisi: {Ad} {Soyad}, ID: {Id}, Verdiği Ders Sayısı: {VerdigiDersler.Count}");
    }
}

// Ders sınıfı
public class Ders
{
    public string DersAdi { get; set; }
    public int Kredi { get; set; }
    public OgretimGorevlisi Ogretmen { get; set; }
    public List<Ogrenci> Ogrenciler { get; set; } = new List<Ogrenci>();

    public void BilgiGoster()
    {
        Console.WriteLine($"Ders: {DersAdi}, Kredi: {Kredi}, Öğretim Görevlisi: {Ogretmen?.Ad} {Ogretmen?.Soyad}");
        Console.WriteLine("Kayıtlı Öğrenciler:");
        foreach (var ogrenci in Ogrenciler)
        {
            Console.WriteLine($"- {ogrenci.Ad} {ogrenci.Soyad}");
        }
    }
}

// Program
class Program
{
    static List<Ogrenci> ogrenciler = new List<Ogrenci>();
    static List<OgretimGorevlisi> ogretimGorevlileri = new List<OgretimGorevlisi>();
    static List<Ders> dersler = new List<Ders>();

    static void Main(string[] args)
    {
        int secim;
        do
        {
            Console.WriteLine("\n--- Sistem Menüsü ---");
            Console.WriteLine("1. Yeni Öğrenci Ekle");
            Console.WriteLine("2. Yeni Öğretim Görevlisi Ekle");
            Console.WriteLine("3. Yeni Ders Ekle");
            Console.WriteLine("4. Öğrenciyi Derse Kayıt Et");
            Console.WriteLine("5. Bilgileri Göster");
            Console.WriteLine("6. Çıkış");
            Console.Write("Seçiminizi yapın: ");
            secim = int.Parse(Console.ReadLine());

            switch (secim)
            {
                case 1:
                    YeniOgrenciEkle();
                    break;
                case 2:
                    YeniOgretimGorevlisiEkle();
                    break;
                case 3:
                    YeniDersEkle();
                    break;
                case 4:
                    OgrenciyiDerseKayitEt();
                    break;
                case 5:
                    BilgileriGoster();
                    break;
                case 6:
                    Console.WriteLine("Çıkış yapılıyor...");
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim! Tekrar deneyin.");
                    break;
            }
        } while (secim != 6);
    }

    static void YeniOgrenciEkle()
    {
        Console.Write("Öğrenci Adı: ");
        string ad = Console.ReadLine();
        Console.Write("Öğrenci Soyadı: ");
        string soyad = Console.ReadLine();
        int id = ogrenciler.Count + 1;

        var ogrenci = new Ogrenci { Id = id, Ad = ad, Soyad = soyad };
        ogrenciler.Add(ogrenci);
        Console.WriteLine($"Yeni öğrenci eklendi: {ad} {soyad}, ID: {id}");
    }

    static void YeniOgretimGorevlisiEkle()
    {
        Console.Write("Öğretim Görevlisi Adı: ");
        string ad = Console.ReadLine();
        Console.Write("Öğretim Görevlisi Soyadı: ");
        string soyad = Console.ReadLine();
        int id = ogretimGorevlileri.Count + 1;

        var ogretimGorevlisi = new OgretimGorevlisi { Id = id, Ad = ad, Soyad = soyad };
        ogretimGorevlileri.Add(ogretimGorevlisi);
        Console.WriteLine($"Yeni öğretim görevlisi eklendi: {ad} {soyad}, ID: {id}");
    }

    static void YeniDersEkle()
    {
        Console.Write("Ders Adı: ");
        string dersAdi = Console.ReadLine();
        Console.Write("Kredi: ");
        int kredi = int.Parse(Console.ReadLine());

        Console.WriteLine("Öğretim Görevlisi ID'si: ");
        foreach (var ogretimGorevlisi in ogretimGorevlileri)
        {
            Console.WriteLine($"ID: {ogretimGorevlisi.Id}, Ad: {ogretimGorevlisi.Ad} {ogretimGorevlisi.Soyad}");
        }
        Console.Write("Seçim: ");
        int ogretmenId = int.Parse(Console.ReadLine());

        var ogretmen = ogretimGorevlileri.Find(o => o.Id == ogretmenId);
        if (ogretmen == null)
        {
            Console.WriteLine("Geçersiz öğretim görevlisi seçimi!");
            return;
        }

        var ders = new Ders { DersAdi = dersAdi, Kredi = kredi, Ogretmen = ogretmen };
        dersler.Add(ders);
        ogretmen.VerdigiDersler.Add(ders);
        Console.WriteLine($"Yeni ders eklendi: {dersAdi}, Öğretim Görevlisi: {ogretmen.Ad} {ogretmen.Soyad}");
    }

    static void OgrenciyiDerseKayitEt()
    {
        Console.WriteLine("Öğrenci ID'si: ");
        foreach (var ogrenci in ogrenciler)
        {
            Console.WriteLine($"ID: {ogrenci.Id}, Ad: {ogrenci.Ad} {ogrenci.Soyad}");
        }
        Console.Write("Seçim: ");
        int ogrenciId = int.Parse(Console.ReadLine());

        var ogrenciSecim = ogrenciler.Find(o => o.Id == ogrenciId);
        if (ogrenciSecim == null)
        {
            Console.WriteLine("Geçersiz öğrenci seçimi!");
            return;
        }

        Console.WriteLine("Ders ID'si: ");
        for (int i = 0; i < dersler.Count; i++)
        {
            Console.WriteLine($"ID: {i + 1}, Ders: {dersler[i].DersAdi}");
        }
        Console.Write("Seçim: ");
        int dersId = int.Parse(Console.ReadLine());

        if (dersId < 1 || dersId > dersler.Count)
        {
            Console.WriteLine("Geçersiz ders seçimi!");
            return;
        }

        var secilenDers = dersler[dersId - 1];
        secilenDers.Ogrenciler.Add(ogrenciSecim);
        ogrenciSecim.Dersler.Add(secilenDers);
        Console.WriteLine($"{ogrenciSecim.Ad} {ogrenciSecim.Soyad}, {secilenDers.DersAdi} dersine kaydedildi.");
    }

    static void BilgileriGoster()
    {
        Console.WriteLine("\n--- Öğrenciler ---");
        foreach (var ogrenci in ogrenciler)
        {
            ogrenci.BilgiGoster();
        }

        Console.WriteLine("\n--- Öğretim Görevlileri ---");
        foreach (var ogretimGorevlisi in ogretimGorevlileri)
        {
            ogretimGorevlisi.BilgiGoster();
        }

        Console.WriteLine("\n--- Dersler ---");
        foreach (var ders in dersler)
        {
            ders.BilgiGoster();
        }
    }
}
