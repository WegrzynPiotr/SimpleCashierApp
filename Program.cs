using System.Xml.Linq;

namespace Cashier
{
   struct Articles
    {
        public string name;
        public double productPrice;
        public double weight;
        public double capacity;
    }
    internal class Program
    {
        static List<string> articlesList = new List<string>();
        static List<double> articlesListPrice = new List<double>();
        static List<double> articlesListWeight = new List<double>() {0,0,0,0,0 };
        static List<int> listOfOrders = new List<int>();
        static List<double> TotalOrderPrice = new List<double>();
        static List<dynamic> listOfStock = new List<dynamic>() { 0, 0, 0, 0, 0 };
        static string customerName = "";
        static void Main(string[] args)
        {
            setArticles();
            start();
            Console.ReadLine();
        }
        static void start() {
            greetings();
            userChoice();
        }
        static string cashierNameRandomizer()
        {
            string name = "";
            Random random = new System.Random();
            List<string> cashierNames = new List<string>() { "Ardrian", "Pacper", "Kunegunda", "Brzegoż Grzenczyszczykiewicz" };
            int value = random.Next(cashierNames.Count);
            name = cashierNames[value];
            return name;
        }
        static void setArticles()
        {
            Articles banan;
            banan.name = "banan";
            banan.weight = 0;
            banan.productPrice = 3.5;
            Articles frytki;
            frytki.name = "frytki";
            frytki.productPrice = 12;
            frytki.weight = 0.9;
            Articles cola;
            cola.name = "coca cola";
            cola.productPrice = 6;
            cola.capacity = 2;
            Articles marmolada;
            marmolada.name = "marmolada";
            marmolada.productPrice = 3.99;
            marmolada.weight = 0.6;
            Articles piwo;
            piwo.name = "Tyskie";
            piwo.productPrice = 3;
            piwo.capacity = 0.3;
            /*Wiem, że można dać Articles banan, Articles frytki itd. do zakresu globalnego i używać struktury, 
              ale po co skoro można sobie utrudnić życie? :)*/

            //Lista Artykułów [Banan ID 0, Frytki ID 1, Cola ID 2, Marmolada ID 3, Piwo ID 4]
            articlesList.Add(banan.name);
            articlesList.Add(frytki.name);
            articlesList.Add(cola.name);
            articlesList.Add(marmolada.name);
            articlesList.Add(piwo.name);

            //Lista Cen dla artykułów, posiadają to samo ID co artykuły
            articlesListPrice.Add(banan.productPrice);
            articlesListPrice.Add(frytki.productPrice);
            articlesListPrice.Add(cola.productPrice);
            articlesListPrice.Add(marmolada.productPrice);
            articlesListPrice.Add(piwo.productPrice);

            //Waga lub pojemność dla standardowych artykułów, posiadają to samo ID co artykuły
            articlesListWeight[1] = frytki.weight;
            articlesListWeight[2] = cola.capacity;
            articlesListWeight[3] = marmolada.weight;
            articlesListWeight[4] = piwo.capacity;
        }
        static void greetings()
        {
            Console.WriteLine("Jestem " + cashierNameRandomizer() + ", witam w moim sklepie, jak masz na imię?");
            customerName = Console.ReadLine();
            customerName = customerName.Length > 0? customerName = customerName[0].ToString().ToUpper() + customerName.Substring(1): customerName = "Andrzej";
     
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Cześć " + customerName + "...");
            System.Threading.Thread.Sleep(1500);
            Console.WriteLine("W naszym sklepie mamy do dyspozycji: (Wybieraj nr 0 - 4)");
            displayMenu();
        }
        static void displayMenu() {
            string weightOrQuantity = "";
            string menu = "";
            int itemLength = 0;
            for(int i=0;i<articlesList.Count;i++)
            {
                if (articlesList[i] == "banan")
                {
                    weightOrQuantity = "za kg";
                }
                else
                {
                    weightOrQuantity = "za szt";
                }

                menu+= i + ". " + articlesList[i] + " za " + articlesListPrice[i] +"zł " + weightOrQuantity +"\n";
                if (i == 0)
                {
                    itemLength = menu.Length-1;
                }
            }
            string menuTitle = "";
            for(int i = 0; i < itemLength/2; i++)
            {
                menuTitle+="-";
            }
            menuTitle += "Artykuły";
            for (int i = 0; i < (itemLength/2)-1; i++)
            {
                menuTitle += "-";
            }
            Console.WriteLine();
            Console.WriteLine(menuTitle);
            Console.Write(menu);
            for (int i = 0; i < menuTitle.Length; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine() ;
        }
        static string[] formatQuestions(int choiceId)
        {
            string package = "", product = "";
            product = articlesList[choiceId];

            if (articlesList[choiceId] == "coca cola" || articlesList[choiceId] == "marmolada")
            {
                product = product.Substring(0, product.Length - 1) + "e";
            }
            if (articlesList[choiceId] == "banan")
            {
                product += "y";
            }

            if (articlesList[choiceId] == "Tyskie" || articlesList[choiceId] == "coca cola")
            {
                package = "butelek";
            }
            if (articlesList[choiceId] == "marmolada" || articlesList[choiceId] == "frytki")
            {
                package = "opakowań";
            }
            string[] values = new string[] { product, package };

            Console.Clear();
            return values;

        }
        static double weightOrQuantity(int choiceId)
        {
            double articlePrice = 0;
            int quantity = 0;
            

            if (articlesListWeight[choiceId] > 0)
            {
                Console.WriteLine("Wybrałeś " + formatQuestions(choiceId)[0] + ", ile " + formatQuestions(choiceId)[1] + " potrzebujesz?");
               int.TryParse(Console.ReadLine(),out quantity);
                if(quantity > 0)
                {
                    listOfStock[choiceId] = quantity;
                    articlePrice = listOfStock[choiceId] * articlesListPrice[choiceId];
                }
                else
                {
                    setDefaultQuantity();
                }   
            }
            else
            {
                Console.WriteLine("Wybrałeś " + formatQuestions(choiceId)[0] + " które są produktem na wagę, ile kg" + formatQuestions(choiceId)[1] + " potrzebujesz?");
                double.TryParse(Console.ReadLine(),out double weight);
                if(weight>0) {
                    if (listOfStock[choiceId] == 0)
                {
                    listOfStock[choiceId] = "--";
                }
                articlesListWeight[choiceId] = weight;
                articlePrice = weight * articlesListPrice[choiceId];
                }
                else
                {
                    setDefaultQuantity();
                }
            }
            return articlePrice;
        }
        static void setDefaultQuantity()
        {
            Console.WriteLine("Nie wybrałeś ilości, a niczego do paragonu nie dobiję! :)");
            System.Threading.Thread.Sleep(3000);
            Console.Clear();
            displayMenu();
            userChoice();
        }
        static void userChoice()
        {
            Console.WriteLine("Wybierz artykuł:");
            int.TryParse(Console.ReadLine(), out int choiceId);
            if (choiceId < articlesList.Count)
            {
                //Sprawdza czy produkt jest do zakupu w ilości sztuk czy jest na wagę
                double total = weightOrQuantity(choiceId);

                listOfOrders.Add(choiceId);
                TotalOrderPrice.Add(total);
                afterOrder();
            }
            else
            {
                Console.WriteLine("Nieprawidłowy wybór");
                System.Threading.Thread.Sleep(1500);
                userChoice();
            }
        } 
        static string questionIsAll()
        {
            Random random = new System.Random();
            List<string> messages = new List<string>() { "Podać coś jeszcze? (tak/nie)", "Czy coś jeszcze potrzeba? (tak/nie)", "Chcesz jeszcze coś dodać? (tak/nie)" };
            int value = random.Next(messages.Count);
            string message = messages[value];
            return message;
        }
        static void afterOrder() {
            Console.WriteLine(questionIsAll());
            System.Threading.Thread.Sleep(1500);
            string userChoose = Console.ReadLine();

            switch (userChoose)
            {
                case "nie":
                    Console.Clear();
                    receipt();
                    break;
                case "tak":
                    Console.Clear();
                    displayMenu();
                    userChoice();
                    break;
                default:
                    Console.Clear();
                    receipt();
                    break;
            }

        }
        static void receipt()
        {
            DateTime aDate = DateTime.Now;
            string currentDate = aDate.ToString("dd/MM/yyyy");
            string weightUnity = "";
            string printRecipe = "----Paragon " + customerName + " wystawiony w dniu " + currentDate +"----";
            Console.WriteLine(printRecipe);
            Console.WriteLine();
            double totalPrice = 0;
            listOfOrders.ForEach((id) =>
            {
                if (id == 2 || id == 4)
                {
                    weightUnity = "l";
                }
                else
                {
                    weightUnity = "kg";
                }

                Console.WriteLine(articlesList[id] + " " + articlesListWeight[id] + weightUnity + " " + listOfStock[id] + "szt " + articlesListPrice[id] + "zł");
            }
            );
            Console.WriteLine();

            for (int i = 0; i < printRecipe.Length; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();

            string toPay = "";
            TotalOrderPrice.ForEach(price =>
            {
                totalPrice += price;
            });
            totalPrice = Math.Round(totalPrice,2);
            toPay = "\n|Kwota do zapłaty to: " + totalPrice + "zł|";


            for (int i = 0; i < toPay.Length - 1; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine(toPay);
            for (int i = 0; i < toPay.Length - 1; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();

            restart();
        }
        static void restart()
        {
            System.Threading.Thread.Sleep(1500);
            Console.WriteLine("Jeżeli chcesz uruchomić program ponownie wpisz: start");
            Console.WriteLine("Aby zakończyć program wpisz: narazie");
            string userChoose = Console.ReadLine();
            switch (userChoose)
            {
                case "start":
                    Console.Clear();
                    listOfOrders.Clear();
                    start();
                    break;
                case "narazie":
                    Console.WriteLine("\nDzięki za zakupy, do widzenia!");
                    System.Threading.Thread.Sleep(1000);
                    Environment.Exit(0);
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }
        } 
    }
}