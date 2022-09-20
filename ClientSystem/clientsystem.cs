using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace dotframe
{
    internal class clientsystem
    {
        [System.Serializable]
        struct Client
        {
            public string Name;
            public string email;
            public string ssn;

        }

        static List<Client> clients = new List<Client>();


        enum Menu { List = 1, Add, Remove, Exit}
        static void Main(string[] args)
            {
            Load();
            bool ChExit = false;

            while (!ChExit)
            {
                Console.Clear();


                Console.WriteLine("Clients System - Welcome!");
                Console.WriteLine("1- Listing\n2-Add\n3-Remove\n4-Exit");
                int ChMenu = int.Parse(Console.ReadLine());

                Menu option = (Menu)ChMenu;


                switch (option)
                {
                    case Menu.List:
                        List();
                        break;
                    case Menu.Add:
                        Add();
                        break;
                    case Menu.Remove:
                        Remove();
                        break;
                    case Menu.Exit:
                        ChExit = true;
                        break;
                }

            }

        }
                 static void List()
                 {
            Console.Clear();
            Console.WriteLine("Clients list:");
            Console.WriteLine($"-----------------------------------");
            int i = 0;
            foreach (Client client in clients)
            {
                Console.WriteLine($"Index ID:{i}");
                Console.WriteLine($"Name: {client.Name}");
                Console.WriteLine($"Email: {client.email}");
                Console.WriteLine($"SSN: {client.ssn}");
                i++;
                Console.WriteLine($"-----------------------------------");

            }
            Console.WriteLine("Press ENTER to return to main menu.");
            Console.ReadLine();

                 }
                 static void Add()
                 {
            Console.Clear();
            Client client = new Client();
            Console.WriteLine("Client register");
            Console.WriteLine("Client name:");
            client.Name = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Client email:");
            client.email = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Client SSN:");
            client.ssn = Console.ReadLine();
            Console.Clear();


            clients.Add(client);
            Save(); 

            Console.WriteLine("Registration completed!\nPress ENTER to return to main menu.");
            Console.ReadLine();
        }
        static void Remove()
                 {
            Console.Clear();
            
            try
            {
                Console.WriteLine($"Enter client ID to remove it:\n0 to {clients.Count -1}");
                int id = int.Parse(Console.ReadLine());
                if (id >= 0 && id < clients.Count)
                {
                    clients.RemoveAt(id);
                    Save();

                }
                else
                {
                    Console.WriteLine("Invalid ID, try again.");
                    Console.WriteLine("Press ENTER to return to main menu");
                    Console.ReadLine();
                }
            }catch
            {
                Console.WriteLine("Invalid ID, try again.");
                Console.WriteLine("Press ENTER to return to main menu");
                Console.ReadLine();

            }

                }
        static void Save()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, clients);

            stream.Close();


        }
        static void Load()
            {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            try {
                BinaryFormatter encoder = new BinaryFormatter();

                clients = (List<Client>)encoder.Deserialize(stream);

              if (clients == null)
                {
                    clients = new List<Client>();
                    
                }
                

            }
            catch (Exception ex)
            {
                clients = new List<Client>();
                
            }

            stream.Close();
        }

        

    }
}
