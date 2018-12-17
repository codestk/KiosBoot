using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace KiosBoot.Model
{
    public class Contact
    {
        private static Random random = new Random();

        #region Properties

        private string initials;

        public string Initials
        {
            get
            {
                if (initials == string.Empty && FirstName != string.Empty && LastName != string.Empty)
                {
                    initials = FirstName[0].ToString() + LastName[0].ToString();
                }
                return initials;
            }
        }

        private string name;

        public string Name
        {
            get
            {
                if (name == string.Empty && FirstName != string.Empty && LastName != string.Empty)
                {
                    name = FirstName + " " + LastName;
                }
                return name;
            }
        }

        private string lastName;

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                initials = string.Empty; // force to recalculate the value
                name = string.Empty; // force to recalculate the value
            }
        }

        private string firstName;

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                initials = string.Empty; // force to recalculate the value
                name = string.Empty; // force to recalculate the value
            }
        }

        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string Biography { get; set; }

        public string image { get; set; }

        public string video { get; set; }

        #endregion Properties

        public Contact()
        {
            // default values for each property.
            initials = string.Empty;
            name = string.Empty;
            LastName = string.Empty;
            FirstName = string.Empty;
            Position = string.Empty;
            PhoneNumber = string.Empty;
            Biography = string.Empty;
        }

        #region Public Methods

        public static Contact GetNewContact()
        {
            return new Contact()
            {
                FirstName = GenerateFirstName(),
                LastName = GenerateLastName(),
                Biography = GetBiography(),
                PhoneNumber = GeneratePhoneNumber(),
                Position = GeneratePosition(),
            };
        }

        public static ObservableCollection<Contact> GetContacts(int numberOfContacts)
        {
            ObservableCollection<Contact> contacts = new ObservableCollection<Contact>();

            for (int i = 0; i < numberOfContacts; i++)
            {
                contacts.Add(GetNewContact());
            }

            //Contact c1 = new Contact();
            //c1.firstName = "Product 1";
            //c1.FirstName = "Product 1";
            //c1.Biography = "Text Animation เป็นเครื่องมือสำหรับใส่เทคนิคพิเศษให้กับข้อความ ตัวอย่างเช่น หัวข้อเรื่องต่างๆ หรือสไลค์หน้าแรกที่เราต้องการจะเน้นจุดสนใจให้ดูสะดุดตา โดย Captivate จะมีรูปแบบของการเคลื่อนไหวให้เลือกอยู่มากมาย";
            //c1.name = "Product 1";
            //c1.Position = "2143045311828";
            //c1.image = "ms-appx:///Assets/Product1/p1.jpg";
            //contacts.Add(c1);

            //Contact c2 = new Contact();
            //c2.firstName = "Product 1";
            //c2.FirstName = "Product 1";
            //c2.Biography = "เปิดตัวแรงส่งท้ายปีกับ ASUS TUF Gaming FX505 ที่เป็นการต่อยอดมาจาก ASUS TUF Gaming FX504 รุ่นก่อนหน้าที่ประสบความสำเร็จเป็นอย่างดี จากความคุ้มค่าที่ตัวเครื่องมอบให้ รวมถึงการระบายความร้อนที่ยอดเยี่ยม ไม่แปลกที่หลายร้านของหมดอยากจะได้ทีต้องสั่งจองล่วงหน้ากันทีเดียว และแน่นอนว่า FX505 ก็ถือเป็นรุ่นที่พัฒนาอัปเกรดจาก FX504 ขึ้นมาอีกขั้น โดยดีไซน์ยังคงเอกลักษณ์แบบเดิม เพิ่มเติมคือขอบจอบางตามสมัยนิยม และวัสดุแข็งแรงขึ้น";
            //c2.name = "Product 2";
            //c2.Position = "7273662773690";
            
            
            //c2.video = "ms-appx:///Assets/Mp4/demo.mp4";//"ms-appx:///Assets/Mp4/Demo.mkv";
            //c2.image = " ";
            //contacts.Add(c2);


            //Contact c3 = new Contact();
            //c3.firstName = "Product 1";
            //c3.FirstName = "Product 1";
            //c3.Biography = "ทางด้านราคาของ ASUS TUF Gaming FX505 รุ่นที่ทีมงานรีวิวในบทความนี้รหัส FX505GM-BQ163T สนนราคาอยู่ที่ 35,990 บาท การรับประกัน 2 ปี และที่สำคัญเมื่อเอาซีเรียลไปลงทะเบียนในเว็บไซต์ ASUS จะได้รับประกันอุบัติเหตุฟรี 1 ปีแรกจากทาง ASUS อีกด้วยอุ่นใจจัดเต็ม";
            //c3.name = "Product 3";
            //c3.Position = "4400352543379";
            //c3.image = "ms-appx:///Assets/Product1/p3.jpg";
            //c3.video = "";
            //contacts.Add(c3);

            //Contact c4 = new Contact();
            //c4.firstName = "Product 1";
            //c4.FirstName = "Product 1";
            //c4.Biography = "เด็กรุ่นใหม่หลายคนอาจจะสงสัยกันว่าแบรนด์ ATEN นี่คือแบรนด์อะไร ไม่เคยได้ยินมาก่อน ซึ่งจริงๆ แล้วแบรนด์ ATEN นี่อายุเกือบจะ 40 ปีล่ะ ก่อตั้งขึ้นในปี 1979 มี โดยเป็นแบรนด์ที่มีความเชี่ยวชาญเกี่ยวกับสายเชื่อมต่อ สายแปลงต่างๆ ที่มีมาตรฐานไต้หวันระดับมืออาชีพ มีการประกัน ซื้อไปแล้วใช้ได้ชัวร์ไม่ต้องลุ้น เป็นอีกหนึ่งแบรนด์ที่การันตีได้เลยว่าสินค้าเขามีคุณภาพแน่นอน";
            //c4.name = "Product 4";
            //c4.Position = "9862955023171";
            //c4.image = "ms-appx:///Assets/Product1/p4.jpg";
            //contacts.Add(c4);


            //Contact c5 = new Contact();
            //c5.firstName = "Product 1";
            //c5.FirstName = "Product 1";
            //c5.Biography = "พอร์ตเทพที่สามารถทำได้ทุกอย่างจากที่กล่าวมาบนก่อนหน้านี้ ที่เทพขนาดนี้ก็เพราะตัวเครื่องจะมีคอนโทรลเลอร์พิเศษไว้ควบคุมเฉพาะตัว ซึ่งสามารถจ่ายไฟได้สูงสุดถึง 100 w รับส่งข้อมูลด้วยความเร็วสูงสุดถึงระดับ 40 GB/s และยังสามารถต่อ eGPU หรือกล่องการ์ดจอแยกได้อีกด้วย ปกติแล้วพอร์ตนี้จะอยู่ในเฉพาะอัลตร้าบุ๊คระดับสูงเท่านั้น สัญลักษณ์จะใช้เป็นรูปสายฟ้าเฉยๆ";
            //c5.name = "Product 5";
            //c5.Position = "6477567444173";
            //c5.image = "ms-appx:///Assets/Product1/p5.jpg";
            //c5.video = "";
            //contacts.Add(c5);


            return contacts;
        }

        public static ObservableCollection<GroupInfoList> GetContactsGrouped(int numberOfContacts)
        {
            ObservableCollection<GroupInfoList> groups = new ObservableCollection<GroupInfoList>();

            var query = from item in GetContacts(numberOfContacts)
                        group item by item.LastName.Substring(0, 1).ToUpper() into g
                        orderby g.Key
                        select new { GroupName = g.Key, Items = g };

            foreach (var g in query)
            {
                GroupInfoList info = new GroupInfoList();
                info.Key = g.GroupName;
                foreach (var item in g.Items)
                {
                    info.Add(item);
                }
                groups.Add(info);
            }

            return groups;
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion Public Methods

        #region Helpers

        private static string GeneratePosition()
        {
            List<string> positions = new List<string>() { "Program Manager", "Developer", "Product Manager", "Evangelist" };
            return positions[random.Next(0, positions.Count)];
        }

        private static string GetBiography()
        {
            List<string> biographies = new List<string>()
            {
                @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer id facilisis lectus. Cras nec convallis ante, quis pulvinar tellus. Integer dictum accumsan pulvinar. Pellentesque eget enim sodales sapien vestibulum consequat.",
                @"Maecenas eu sapien ac urna aliquam dictum.",
                @"Nullam eget mattis metus. Donec pharetra, tellus in mattis tincidunt, magna ipsum gravida nibh, vitae lobortis ante odio vel quam.",
                @"Quisque accumsan pretium ligula in faucibus. Mauris sollicitudin augue vitae lorem cursus condimentum quis ac mauris. Pellentesque quis turpis non nunc pretium sagittis. Nulla facilisi. Maecenas eu lectus ante. Proin eleifend vel lectus non tincidunt. Fusce condimentum luctus nisi, in elementum ante tincidunt nec.",
                @"Aenean in nisl at elit venenatis blandit ut vitae lectus. Praesent in sollicitudin nunc. Pellentesque justo augue, pretium at sem lacinia, scelerisque semper erat. Ut cursus tortor at metus lacinia dapibus.",
                @"Ut consequat magna luctus justo egestas vehicula. Integer pharetra risus libero, et posuere justo mattis et.",
                @"Proin malesuada, libero vitae aliquam venenatis, diam est faucibus felis, vitae efficitur erat nunc non mauris. Suspendisse at sodales erat.",
                @"Aenean vulputate, turpis non tincidunt ornare, metus est sagittis erat, id lobortis orci odio eget quam. Suspendisse ex purus, lobortis quis suscipit a, volutpat vitae turpis.",
                @"Duis facilisis, quam ut laoreet commodo, elit ex aliquet massa, non varius tellus lectus et nunc. Donec vitae risus ut ante pretium semper. Phasellus consectetur volutpat orci, eu dapibus turpis. Fusce varius sapien eu mattis pharetra.",
                @"Nam vulputate eu erat ornare blandit. Proin eget lacinia erat. Praesent nisl lectus, pretium eget leo et, dapibus dapibus velit. Integer at bibendum mi, et fringilla sem."
            };
            return biographies[random.Next(0, biographies.Count)];
        }

        private static string GeneratePhoneNumber()
        {
            return string.Format("{0:(###)} {1:###}-{2:####}", random.Next(100, 999), random.Next(100, 999), random.Next(1000, 9999));
        }

        private static string GenerateFirstName()
        {
            List<string> names = new List<string>() { "Lilly", "Mukhtar", "Sophie", "Femke", "Abdul-Rafi'", "Chirag-ud-D...", "Mariana", "Aarif", "Sara", "Ibadah", "Fakhr", "Ilene", "Sardar", "Hanna", "Julie", "Iain", "Natalia", "Henrik", "Rasa", "Quentin", "Gadi", "Pernille", "Ishtar", "Jimme", "Justina", "Lale", "Elize", "Rand", "Roshanara", "Rajab", "Bijou", "Marcus", "Marcus", "Alima", "Francisco", "Thaqib", "Andreas", "Mariana", "Amalie", "Rodney", "Dena", "Fadl", "Ammar", "Anna", "Nasreen", "Reem", "Tomas", "Filipa", "Frank", "Bari'ah", "Parvaiz", "Jibran", "Tomas", "Elli", "Carlos", "Diego", "Henrik", "Aruna", "Vahid", "Eliana", "Roxane", "Amanda", "Ingrid", "Wander", "Malika", "Basim", "Eisa", "Alina", "Andreas", "Deeba", "Diya", "Parveen", "Bakr", "Celine", "Bakr", "Marcus", "Daniel", "Mathea", "Edmee", "Hedda", "Maria", "Maja", "Alhasan", "Alina", "Hedda", "Victor", "Aaftab", "Guilherme", "Maria", "Kai", "Sabien", "Abdel", "Fadl", "Bahaar", "Vasco", "Jibran", "Parsa", "Catalina", "Fouad", "Colette" };
            return names[random.Next(0, names.Count)];
        }

        private static string GenerateLastName()
        {
            List<string> lastnames = new List<string>() { "Carlson", "Attia", "Quint", "Hollenberg", "Khoury", "Araujo", "Hakimi", "Seegers", "Abadi", "al", "Krommenhoek", "Siavashi", "Kvistad", "Sjo", "Vanderslik", "Fernandes", "Dehli", "Sheibani", "Laamers", "Batlouni", "Lyngvær", "Oveisi", "Veenhuizen", "Gardenier", "Siavashi", "Mutlu", "Karzai", "Mousavi", "Natsheh", "Seegers", "Nevland", "Lægreid", "Bishara", "Cunha", "Hotaki", "Kyvik", "Cardoso", "Pilskog", "Pennekamp", "Nuijten", "Bettar", "Borsboom", "Skistad", "Asef", "Sayegh", "Sousa", "Medeiros", "Kregel", "Shamoun", "Behzadi", "Kuzbari", "Ferreira", "Van", "Barros", "Fernandes", "Formo", "Nolet", "Shahrestaani", "Correla", "Amiri", "Sousa", "Fretheim", "Van", "Hamade", "Baba", "Mustafa", "Bishara", "Formo", "Hemmati", "Nader", "Hatami", "Natsheh", "Langen", "Maloof", "Berger", "Ostrem", "Bardsen", "Kramer", "Bekken", "Salcedo", "Holter", "Nader", "Bettar", "Georgsen", "Cunha", "Zardooz", "Araujo", "Batalha", "Antunes", "Vanderhoorn", "Nader", "Abadi", "Siavashi", "Montes", "Sherzai", "Vanderschans", "Neves", "Sarraf", "Kuiters" };
            return lastnames[random.Next(0, lastnames.Count)];
        }

        #endregion Helpers
    }
}
