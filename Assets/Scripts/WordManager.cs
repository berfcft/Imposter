using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Kelime yönetimi için statik sınıf.
/// Oyuncu sayısına göre kelime listesi üretir ve karıştırır.
/// </summary>
public static class WordManager
{
    // Konu kategorileri → birden fazla (main, impostor) çifti
    private static readonly Dictionary<string, List<(string mainWord, string impostorWord)>> topicToWordPairs =
        new Dictionary<string, List<(string, string)>>()
        {
            {
                "Meyveler",
                new List<(string, string)>
                {
                    ("Limon", "Sarı"),
                    ("Portakal", "Turuncu"),
                    ("Mandalina", "Kabuk"),
                    ("Greyfurt", "Ekşi"),
                    ("Elma", "Tatlı"),
                    ("Armut", "Sulu"),
                    ("Muz", "Eğri"),
                    ("Kivi", "Yeşil"),
                    ("Çilek", "Küçük"),
                    ("Kiraz", "Kırmızı"),
                    ("Vişne", "Ekşi"),
                    ("Şeftali", "Tüylü"),
                    ("Kayısı", "Turuncu"),
                    ("Erik", "Mor"),
                    ("Üzüm", "Salkım"),
                    ("Kavun", "Tatlı"),
                    ("Karpuz", "Çekirdek"),
                    ("Nar", "Tane"),
                    ("Dut", "Beyaz"),
                    ("Böğürtlen", "Siyah"),
                    ("Ahududu", "Pembe"),
                    ("Yaban mersini", "Mavi"),
                    ("Hurma", "Kahverengi"),
                    ("İncir", "Mor"),
                    ("Avokado", "Çekirdek"),
                    ("Mango", "Tropik"),
                    ("Ananas", "Sert"),
                    ("Hindistan cevizi", "Sert"),
                    ("Trabzon hurması", "Turuncu"),
                    ("Kızılcık", "Ekşi"),
                    ("Karadut", "Siyah"),
                    ("Beyaz dut", "Tatlı"),
                    ("Kan portakalı", "Kırmızı"),
                    ("Nektarin", "Çekirdeksiz"),
                    ("Zerdali", "Kayısı")
                }
            },
            {
                "Araçlar",
                new List<(string, string)>
                {
                    ("Araba", "Tekerlek"),
                    ("Otobüs", "Yolcu"),
                    ("Kamyon", "Yük"),
                    ("Tır", "Uzun"),
                    ("Minibüs", "Küçük"),
                    ("Taksi", "Sarı"),
                    ("Bisiklet", "Pedal"),
                    ("Motosiklet", "Kask"),
                    ("Scooter", "Elektrikli"),
                    ("Traktör", "Tarla"),
                    ("Tren", "Ray"),
                    ("Metro", "Yeraltı"),
                    ("Tramvay", "Şehir"),
                    ("Hızlı tren", "Hız"),
                    ("Uçak", "Kanat"),
                    ("Helikopter", "Pervane"),
                    ("Jet", "Sürat"),
                    ("Gemi", "Deniz"),
                    ("Yat", "Lüks"),
                    ("Vapur", "Duman"),
                    ("Sandal", "Kürek"),
                    ("Kayık", "Su"),
                    ("Feribot", "Araç"),
                    ("Denizaltı", "Derin"),
                    ("Tank", "Zırh"),
                    ("Askeri jip", "Yeşil"),
                    ("Polis arabası", "Siren"),
                    ("Ambulans", "Acil"),
                    ("İtfaiye aracı", "Merdiven"),
                    ("Karavan", "Tatil"),
                    ("Pikap", "Bagaj"),
                    ("SUV", "Büyük"),
                    ("Spor araba", "Hızlı"),
                    ("Cabrio", "Açık"),
                    ("Limuzin", "Uzun"),
                    ("At arabası", "At"),
                    ("Fayton", "Çan"),
                    ("Patpat", "Köy"),
                    ("Kepçe", "İnşaat"),
                    ("Dozer", "Toprak"),
                    ("Ekskavatör", "Kol"),
                    ("Kamyonet", "Küçük"),
                    ("Çekici", "Kurtarma"),
                    ("Tekerlekli sandalye", "Engel"),
                    ("Kaykay", "Tahta"),
                    ("Hoverboard", "Elektrik"),
                    ("Teleferik", "Halat"),
                    ("Vinç", "Yük"),
                    ("Balon", "Hava"),
                    ("Roket", "Uzay")
                }
            },
            {
                "Hayvanlar",
                new List<(string, string)>
                {
                    ("Kedi", "Tüy"),
                    ("Köpek", "Havlama"),
                    ("Aslan", "Yele"),
                    ("Kaplan", "Çizgi"),
                    ("Fil", "Hortum"),
                    ("Zürafa", "Uzun"),
                    ("Ayı", "Güçlü"),
                    ("Tilki", "Kurnaz"),
                    ("Kurt", "Uluma"),
                    ("Tavşan", "Havuç"),
                    ("At", "Nalı"),
                    ("Eşek", "Anırma"),
                    ("İnek", "Süt"),
                    ("Koyun", "Yün"),
                    ("Keçi", "Boynuz"),
                    ("Tavuk", "Yumurta"),
                    ("Horoz", "Ötme"),
                    ("Ördek", "Vak"),
                    ("Kaz", "Boyun"),
                    ("Hindi", "Gaga"),
                    ("Maymun", "Muz"),
                    ("Panda", "Bambu"),
                    ("Kanguru", "Keseli"),
                    ("Koala", "Ağaç"),
                    ("Balık", "Yüzmek"),
                    ("Yunus", "Zeki"),
                    ("Köpek balığı", "Diş"),
                    ("Balina", "Dev"),
                    ("Penguen", "Buz"),
                    ("Kartal", "Kanat"),
                    ("Baykuş", "Gece"),
                    ("Serçe", "Küçük"),
                    ("Papağan", "Konuşma"),
                    ("Güvercin", "Barış"),
                    ("Kanarya", "Sarı")
                }
            },
            {
                "Doğa",
                new List<(string, string)>
                {
                    ("Güneş", "Sıcak"),
                    ("Ay", "Gece"),
                    ("Yıldız", "Parlak"),
                    ("Bulut", "Beyaz"),
                    ("Yağmur", "Islak"),
                    ("Kar", "Soğuk"),
                    ("Rüzgar", "Esinti"),
                    ("Fırtına", "Gürültü"),
                    ("Gökkuşağı", "Renk"),
                    ("Şimşek", "Işık"),
                    ("Gök gürültüsü", "Ses"),
                    ("Deniz", "Mavi"),
                    ("Nehir", "Akmak"),
                    ("Göl", "Sakin"),
                    ("Şelale", "Düşmek"),
                    ("Okyanus", "Dalga"),
                    ("Toprak", "Kahverengi"),
                    ("Dağ", "Zirve"),
                    ("Tepe", "Küçük"),
                    ("Vadi", "Derin"),
                    ("Orman", "Ağaç"),
                    ("Çöl", "Sıcak"),
                    ("Kum", "Sarı"),
                    ("Taş", "Sert"),
                    ("Kaya", "Büyük"),
                    ("Çimen", "Yeşil"),
                    ("Çiçek", "Koku"),
                    ("Gül", "Kırmızı"),
                    ("Lale", "Bahar"),
                    ("Ayçiçeği", "Sarı"),
                    ("Zambak", "Beyaz"),
                    ("Orkide", "Güzel"),
                    ("Lavanta", "Mor"),
                    ("Ağaç", "Gövde"),
                    ("Yaprak", "Yeşil")
                }
            },
            {
                "Yiyecek & İçecekler",
                new List<(string, string)>
                {
                    ("Ekmek", "Fırın"),
                    ("Simit", "Susam"),
                    ("Börek", "Hamur"),
                    ("Pide", "Taş fırın"),
                    ("Lahmacun", "Acılı"),
                    ("Pizza", "Peynir"),
                    ("Hamburger", "Köfte"),
                    ("Patates kızartması", "Kızgın"),
                    ("Makarna", "Sos"),
                    ("Pilav", "Pirinç"),
                    ("Çorba", "Sıcak"),
                    ("Kebab", "Et"),
                    ("Döner", "Lavaş"),
                    ("Köfte", "Izgara"),
                    ("Balık", "Izgara"),
                    ("Tavuk", "Kanat"),
                    ("Yumurta", "Sarı"),
                    ("Peynir", "Beyaz"),
                    ("Yoğurt", "Ekşi"),
                    ("Ayran", "Köpük"),
                    ("Çay", "Demlik"),
                    ("Kahve", "Kahverengi"),
                    ("Şalgam", "Acı"),
                    ("Gazoz", "Köpük"),
                    ("Kola", "Siyah"),
                    ("Su", "Şeffaf"),
                    ("Şarap", "Kırmızı"),
                    ("Bira", "Köpük"),
                    ("Süt", "Beyaz"),
                    ("Dondurma", "Soğuk"),
                    ("Çikolata", "Tatlı"),
                    ("Tatlı", "Şeker"),
                    ("Baklava", "Şerbet"),
                    ("Künefe", "Peynir"),
                    ("Lokum", "Pudra")
                }
            },
            {
                "Meslekler",
                new List<(string, string)>
                {
                    ("Doktor", "Stetoskop"),
                    ("Hemşire", "Serum"),
                    ("Öğretmen", "Tahta"),
                    ("Mühendis", "Proje"),
                    ("Avukat", "Mahkeme"),
                    ("Hakim", "Gavel"),
                    ("Polis", "Siren"),
                    ("İtfaiyeci", "Merdiven"),
                    ("Aşçı", "Tencere"),
                    ("Garson", "Tepsi"),
                    ("Pilot", "Uçak"),
                    ("Hostes", "Kabin"),
                    ("Şoför", "Direksiyon"),
                    ("Taksi şoförü", "Sarı"),
                    ("Postacı", "Mektup"),
                    ("Çiftçi", "Tarla"),
                    ("Balıkçı", "Ağ"),
                    ("Kuyumcu", "Altın"),
                    ("Berber", "Makas"),
                    ("Kuaför", "Saç"),
                    ("Ressam", "Fırça"),
                    ("Müzisyen", "Nota"),
                    ("Şarkıcı", "Mikrofon"),
                    ("Oyuncu", "Sahne"),
                    ("Yönetmen", "Kamera"),
                    ("Yazar", "Kalem"),
                    ("Gazeteci", "Haber"),
                    ("Fotoğrafçı", "Kamera"),
                    ("Bilgisayar mühendisi", "Kod"),
                    ("Yazılım geliştirici", "Klavye"),
                    ("Pilot astronot", "Uzay"),
                    ("Tesisatçı", "Boru"),
                    ("Elektrikçi", "Kablo"),
                    ("Marangoz", "Tahta"),
                    ("Sporcu", "Forma")
                }
            },
            {
                "Şehirler",
                new List<(string, string)>
                {
                    ("İstanbul", "Boğaziçi"),
                    ("Ankara", "Meclis"),
                    ("İzmir", "Kordon"),
                    ("Antalya", "Tatil"),
                    ("Bursa", "Uludağ"),
                    ("Konya", "Mevlana"),
                    ("Adana", "Kebap"),
                    ("Gaziantep", "Baklava"),
                    ("Kayseri", "Mantı"),
                    ("Mersin", "Liman"),
                    ("Trabzon", "Çay"),
                    ("Samsun", "Sahil"),
                    ("Malatya", "Kayısı"),
                    ("Erzurum", "Palandöken"),
                    ("Van", "Göl"),
                    ("Diyarbakır", "Sur"),
                    ("Şanlıurfa", "Balıklı Göl"),
                    ("Amasya", "Elma"),
                    ("Rize", "Çay"),
                    ("Edirne", "Selimiye"),
                    ("Kars", "Kaşar"),
                    ("Balıkesir", "Zeytin"),
                    ("Çanakkale", "Boğaz"),
                    ("Hatay", "Antakya"),
                    ("Isparta", "Gül"),
                    ("Ordu", "Fındık"),
                    ("Sivas", "Divriği"),
                    ("Erzincan", "Tulum"),
                    ("Niğde", "Elma"),
                    ("Uşak", "Halı"),
                    ("Manisa", "Mesir"),
                    ("Aydın", "İncir"),
                    ("Denizli", "Pamukkale"),
                    ("Kütahya", "Seramik"),
                    ("Tekirdağ", "Rakı")
                }
            },
            {
                "Sporlar",
                new List<(string, string)>
                {
                    ("Futbol", "Top"),
                    ("Basketbol", "Potaya"),
                    ("Voleybol", "Ağ"),
                    ("Tenis", "Raket"),
                    ("Masa tenisi", "Küçük top"),
                    ("Hentbol", "El topu"),
                    ("Yüzme", "Havuz"),
                    ("Atletizm", "Koşu"),
                    ("Maraton", "Uzun"),
                    ("Bisiklet", "Pedal"),
                    ("Dağcılık", "Tırmanış"),
                    ("Kayak", "Kar"),
                    ("Snowboard", "Tahta"),
                    ("Boks", "Eldiven"),
                    ("Güreş", "Minder"),
                    ("Judo", "Gi"),
                    ("Karate", "Kuşak"),
                    ("Taekwondo", "Tekme"),
                    ("Jimnastik", "Alet"),
                    ("Yoga", "Mat"),
                    ("Pilates", "Esneklik"),
                    ("Golf", "Sopalar"),
                    ("Bowling", "Top"),
                    ("Bilardo", "Masa"),
                    ("Dart", "Oku"),
                    ("Surf", "Dalga"),
                    ("Sörf", "Tahta"),
                    ("Kayak Atlama", "Tepelik"),
                    ("Formula 1", "Hız"),
                    ("Motor Yarışı", "Piston"),
                    ("Atlet", "Koşu"),
                    ("Futbol Amer.", "Kale"),
                    ("Rugby", "Oval top"),
                    ("Hokey", "Buz"),
                    ("Buz Pateni", "Bıçak")
                }
            },
            {
                "Renkler",
                new List<(string, string)>
                {
                    ("Kırmızı", "Elma"),
                    ("Mavi", "Deniz"),
                    ("Yeşil", "Çimen"),
                    ("Sarı", "Güneş"),
                    ("Turuncu", "Portakal"),
                    ("Mor", "Lavanta"),
                    ("Pembe", "Gül"),
                    ("Beyaz", "Kar"),
                    ("Siyah", "Gece"),
                    ("Gri", "Taş"),
                    ("Kahverengi", "Toprak"),
                    ("Lacivert", "Gece"),
                    ("Turkuaz", "Deniz"),
                    ("Açık mavi", "Gökyüzü"),
                    ("Koyu yeşil", "Orman"),
                    ("Bej", "Kum"),
                    ("Krem", "Süt"),
                    ("Altın", "Parlak"),
                    ("Gümüş", "Metal"),
                    ("Bakır", "Tel"),
                    ("Bordo", "Şarap"),
                    ("Fuşya", "Canlı"),
                    ("Mercan", "Deniz"),
                    ("Zümrüt", "Taş"),
                    ("Turuncu-kırmızı", "Gün batımı"),
                    ("Lavanta", "Çiçek"),
                    ("Açık pembe", "Şeker"),
                    ("Koyu mavi", "Deniz"),
                    ("Çivit", "Kumaş"),
                    ("Gül kurusu", "Ton"),
                    ("Turuncu-sarı", "Sonbahar"),
                    ("Fıstık yeşili", "Meyve"),
                    ("Açık gri", "Bulut"),
                    ("Koyu kahverengi", "Ahşap"),
                    ("Lila", "Çiçek")
                }
            },
            {
                "Müzik / Enstrümanlar",
                new List<(string, string)>
                {
                    ("Piyano", "Tuş"),
                    ("Gitar", "Tel"),
                    ("Keman", "Yay"),
                    ("Çello", "Yay"),
                    ("Kontrbas", "Büyük"),
                    ("Flüt", "Nefes"),
                    ("Klarnet", "Siyah"),
                    ("Saksafon", "Metal"),
                    ("Trompet", "Bakır"),
                    ("Trombon", "Kaydırak"),
                    ("Davul", "Vuruş"),
                    ("Bateri", "Ritim"),
                    ("Marakas", "Sallama"),
                    ("Ksilofon", "Ahşap"),
                    ("Organ", "Tuş"),
                    ("Akordeon", "Körük"),
                    ("Mandolin", "Tel"),
                    ("Banjo", "Tel"),
                    ("Ukulele", "Küçük"),
                    ("Harp", "Teller"),
                    ("Trompet", "Nefes"),
                    ("Tuba", "Büyük"),
                    ("Oboe", "Nefes"),
                    ("Fagot", "Uzun"),
                    ("Cornet", "Bakır"),
                    ("Bongo", "El"),
                    ("Conga", "Vuruş"),
                    ("Cajon", "Kutulu"),
                    ("Elektrikli gitar", "Amfi"),
                    ("Synthesizer", "Tuş"),
                    ("Marimba", "Ahşap"),
                    ("Zurna", "Geleneksel"),
                    ("Ney", "Türk"),
                    ("Davul seti", "Ritm"),
                    ("Timpani", "Büyük")
                }
            },
            {
                "Film / Dizi",
                new List<(string, string)>
                {
                    ("Titanic", "Gemi"),
                    ("Harry Potter", "Asa"),
                    ("Yüzüklerin Efendisi", "Halka"),
                    ("Avengers", "Süper kahraman"),
                    ("Star Wars", "Uzay"),
                    ("Game of Thrones", "Taht"),
                    ("Breaking Bad", "Kimya"),
                    ("Stranger Things", "Bilim kurgu"),
                    ("Friends", "Kahve"),
                    ("The Office", "Ofis"),
                    ("Joker", "Palyaço"),
                    ("Inception", "Rüya"),
                    ("Matrix", "Yeşil"),
                    ("Gladiator", "Kılıç"),
                    ("The Lion King", "Aslan"),
                    ("Frozen", "Buz"),
                    ("Moana", "Deniz"),
                    ("Toy Story", "Oyuncak"),
                    ("Finding Nemo", "Balık"),
                    ("The Godfather", "Mafya"),
                    ("Sherlock Holmes", "Dedektif"),
                    ("House", "Doktor"),
                    ("Black Mirror", "Teknoloji"),
                    ("The Witcher", "Kılıç"),
                    ("Dark", "Zaman"),
                    ("La Casa de Papel", "Maske"),
                    ("Breaking Bad", "Kimya"),
                    ("Stranger Things", "Işık"),
                    ("Friends", "Kahkaha"),
                    ("How I Met Your Mother", "Bar"),
                    ("Westworld", "Robot"),
                    ("The Mandalorian", "Bounty hunter"),
                    ("Peaky Blinders", "Şapka"),
                    ("Vikings", "Longship"),
                    ("The Crown", "Taç")
                }
            },
            {
                "Hobiler",
                new List<(string, string)>
                {
                    ("Resim yapmak", "Fırça"),
                    ("Müzik dinlemek", "Kulaklık"),
                    ("Kitap okumak", "Sayfa"),
                    ("Yazı yazmak", "Kalem"),
                    ("Fotoğraf çekmek", "Kamera"),
                    ("Film izlemek", "Televizyon"),
                    ("Dans etmek", "Ayakkabı"),
                    ("Yüzmek", "Havuz"),
                    ("Koşmak", "Spor ayakkabı"),
                    ("Bisiklete binmek", "Pedal"),
                    ("Bahçe işleri", "Toprak"),
                    ("Balık tutmak", "Olta"),
                    ("Kamp yapmak", "Çadır"),
                    ("Dağcılık", "Tırmanış"),
                    ("Yoga yapmak", "Mat"),
                    ("Puzzle yapmak", "Parça"),
                    ("Origami yapmak", "Kağıt"),
                    ("Dikiş dikmek", "İplik"),
                    ("Örgü örmek", "Yün"),
                    ("Koleksiyon yapmak", "Figür"),
                    ("Oyun oynamak", "Konsol"),
                    ("Araba sürmek", "Direksiyon"),
                    ("Seyahat etmek", "Bavul"),
                    ("Kuş gözlemlemek", "Dürbün"),
                    ("Astronomi", "Teleskop"),
                    ("Kampüs etkinlikleri", "Arkadaş"),
                    ("Fotoğraf düzenlemek", "Bilgisayar"),
                    ("Video çekmek", "Tripod"),
                    ("Blog yazmak", "Klavye"),
                    ("Müzik aleti çalmak", "Gitar"),
                    ("Pişirme / Yemek yapmak", "Fırın"),
                    ("Tatil planlamak", "Harita"),
                    ("Meditasyon", "Sessizlik"),
                    ("Kart oyunu oynamak", "Masa"),
                    ("Satranç oynamak", "Taşlar")
                }
            },
            {
                "Bilim / Uzay",
                new List<(string, string)>
                {
                    ("Güneş", "Sıcak"),
                    ("Ay", "Gece"),
                    ("Yıldız", "Parlak"),
                    ("Gezegen", "Yuvarlak"),
                    ("Dünya", "Mavi"),
                    ("Mars", "Kırmızı"),
                    ("Jüpiter", "Büyük"),
                    ("Satürn", "Halka"),
                    ("Merkür", "Küçük"),
                    ("Venüs", "Sıcak"),
                    ("Meteor", "Hızlı"),
                    ("Kuyruklu yıldız", "Kuyruk"),
                    ("Asteroit", "Taş"),
                    ("Uzay gemisi", "Metal"),
                    ("Roket", "Yükselmek"),
                    ("Teleskop", "Cam"),
                    ("Astronot", "Kask"),
                    ("Uzay istasyonu", "Yörünge"),
                    ("Kara delik", "Çekim"),
                    ("Galaksi", "Spiral"),
                    ("Nebula", "Bulut"),
                    ("Evren", "Sonsuz"),
                    ("Işık yılı", "Mesafe"),
                    ("Kainat", "Geniş"),
                    ("Yerçekimi", "Çekim"),
                    ("Enerji", "Güç"),
                    ("Atom", "Parçacık"),
                    ("Molekül", "Bağ"),
                    ("Kimya", "Reaksiyon"),
                    ("Fizik", "Kurallar"),
                    ("Biyoloji", "Canlı"),
                    ("Genetik", "DNA"),
                    ("Nöroloji", "Beyin"),
                    ("Botanik", "Bitki"),
                    ("Zooloji", "Hayvan")
                }
            },
            {
                "Teknoloji / Cihazlar",
                new List<(string, string)>
                {
                    ("Telefon", "Ekran"),
                    ("Bilgisayar", "Klavye"),
                    ("Laptop", "Pil"),
                    ("Tablet", "Dokunmatik"),
                    ("Akıllı saat", "Bileklik"),
                    ("Kulaklık", "Ses"),
                    ("Hoparlör", "Gürültü"),
                    ("Kamera", "Lens"),
                    ("Drone", "Uçmak"),
                    ("Televizyon", "Uzaktan"),
                    ("Monitör", "Görüntü"),
                    ("Yazıcı", "Kağıt"),
                    ("Tarayıcı", "Doküman"),
                    ("Router", "İnternet"),
                    ("Modem", "Bağlantı"),
                    ("Projeksiyon", "Film"),
                    ("Konsol", "Oyun"),
                    ("Mouse", "Tıklama"),
                    ("Klavye", "Tuş"),
                    ("USB bellek", "Taşınabilir"),
                    ("Hard disk", "Depolama"),
                    ("SSD", "Hızlı"),
                    ("Mikrofon", "Ses"),
                    ("Web kamerası", "Görüntü"),
                    ("Akıllı televizyon", "İnternet"),
                    ("Akıllı ev cihazı", "Otomasyon"),
                    ("Robot süpürge", "Temizlik"),
                    ("Elektrikli scooter", "Taşıma"),
                    ("Elektrikli araba", "Şarj"),
                    ("Kamera dronu", "Hava"),
                    ("3D yazıcı", "Plastik"),
                    ("VR gözlük", "Sanal"),
                    ("Hologram", "Işık"),
                    ("Akıllı ışık", "Renk"),
                    ("Termostat", "Sıcaklık")
                }
            },
            {
                "Tatil / Festival",
                new List<(string, string)>
                {
                    ("Yılbaşı", "Havai fişek"),
                    ("Noel", "Çam ağacı"),
                    ("Sevgililer Günü", "Kalp"),
                    ("Cadılar Bayramı", "Kostüm"),
                    ("Şükran Günü", "Hindi"),
                    ("Paskalya", "Yumurta"),
                    ("Ramazan Bayramı", "Şeker"),
                    ("Kurban Bayramı", "Et"),
                    ("23 Nisan", "Çocuk"),
                    ("19 Mayıs", "Spor"),
                    ("Cumhuriyet Bayramı", "Bayrak"),
                    ("Halloween", "Kabak"),
                    ("Mardi Gras", "Maske"),
                    ("Karnaval", "Renk"),
                    ("Oktoberfest", "Bira"),
                    ("Holi Festivali", "Renk"),
                    ("Diwali", "Işık"),
                    ("Bastille Günü", "Fırlatma"),
                    ("Çin Yeni Yılı", "Ejderha"),
                    ("Songkran", "Su"),
                    ("Carnival", "Dans"),
                    ("Thanksgiving", "Aile"),
                    ("Easter", "Tavşan"),
                    ("Ramadan", "Oruç"),
                    ("Hanukkah", "Mum"),
                    ("Festivali müzik", "Konser"),
                    ("Film festivali", "Ekran"),
                    ("Jazz festivali", "Saxofon"),
                    ("Spor festivali", "Madalya"),
                    ("Yaz tatili", "Plaj"),
                    ("Kış tatili", "Kar"),
                    ("Bahar festivali", "Çiçek"),
                    ("Sonbahar festivali", "Yaprak"),
                    ("Müzik festivali", "Ritim"),
                    ("Sokak festivali", "Kalabalık")
                }
            }
        };

    /// <summary>
    /// Tüm kategorilerden rastgele kelime çifti alır
    /// </summary>
    /// <returns>Rastgele seçilen (ana kelime, impostor kelimesi) çifti</returns>
    public static (string mainWord, string impostorWord) GetRandomWords()
    {
        // Tüm kategorilerden rastgele bir kategori seç
        var topics = topicToWordPairs.Keys.ToList();
        if (topics.Count > 0)
        {
            string randomTopic = topics[Random.Range(0, topics.Count)];
            if (topicToWordPairs.TryGetValue(randomTopic, out var list) && list.Count > 0)
            {
                int idx = Random.Range(0, list.Count);
                return list[idx];
            }
        }

        return ("Elma", "Armut");
    }

    /// <summary>
    /// Oyuncu ve impostor sayısına göre kelime listesi üretir
    /// </summary>
    /// <param name="playerCount">Toplam oyuncu sayısı</param>
    /// <param name="impostorCount">Impostor sayısı</param>
    /// <param name="mainWord">Ana kelime (normal oyuncular için)</param>
    /// <param name="impostorWord">Impostor kelimesi</param>
    /// <returns>Karıştırılmış kelime listesi</returns>
    public static List<string> GenerateWords(int playerCount, int impostorCount, string mainWord, string impostorWord)
    {
        List<string> words = new List<string>();
        
        // Normal oyuncular için ana kelimeyi ekle
        for (int i = 0; i < playerCount - impostorCount; i++)
        {
            words.Add(mainWord);
        }
        
        // Impostorlar için impostor kelimesini ekle
        for (int i = 0; i < impostorCount; i++)
        {
            words.Add(impostorWord);
        }

        // Fisher–Yates Shuffle algoritması ile kelimeleri karıştır
        for (int i = words.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = words[i];
            words[i] = words[j];
            words[j] = temp;
        }
        
        return words;
    }
}
