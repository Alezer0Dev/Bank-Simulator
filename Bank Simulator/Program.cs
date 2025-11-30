using Bank_Simulator;
using System.Xml.Serialization;

Console.OutputEncoding = System.Text.Encoding.UTF8;

int mainChoice;
Random rand = new Random();

Console.WriteLine("Benvenuto in Bank Simulator");
do
{
    Console.WriteLine("\nMenu principale:\n1) Crea nuovo conto\n2) Lista conti\n3) Accedi a un conto\n4) Esci");
    while (!int.TryParse(Console.ReadLine(), out mainChoice) || mainChoice < 1 || mainChoice > 4)
    {
        Console.WriteLine("Inserisci un numero tra 1 e 4:");
    }

    switch (mainChoice)
    {
        case 1:
            Console.WriteLine("Creazione nuovo conto...");
            string name;
            do
            {
                Console.WriteLine("Inserisci il nome del titolare:");
                name = Console.ReadLine();
            }
            while (String.IsNullOrWhiteSpace(name) || BankAccount.IsNameUsed(name));

            int balance = 1000;

            string iban;
            do
            {
                iban = "IT" + rand.Next(1000000000, 1999999999);
            } while (BankAccount.IsIbanUsed(iban));

            string password;
            string confirmPassword;
            do
            {
                Console.WriteLine("Inserisci una password per il conto (minimo 4 caratteri):");
                password = Console.ReadLine();
            }
            while (String.IsNullOrWhiteSpace(password) || password.Length < 4);
            do
            {
                Console.WriteLine("Conferma la password:");
                confirmPassword = Console.ReadLine();
            }
            while (confirmPassword != password);

            var acc = new BankAccount(name, balance, iban, password);
            Console.WriteLine($"Conto creato: {acc.GetName()} - IBAN: {iban} - Saldo: {balance}");
            break;

        case 2:
            Console.WriteLine("Elenco conti esistenti:");
            try
            {
                var accounts = BankAccount.GetAccountsList();
                if (accounts == null || accounts.Count == 0)
                {
                    Console.WriteLine("Nessun account registrato.");
                    break;
                }

                for (int i = 0; i < accounts.Count; i++)
                {
                    var a = accounts[i];
                    Console.WriteLine($"Titolare: {a.GetName()} - Saldo: {a.GetBalance()} - IBAN: {a.GetIban()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            break;

        case 3:
            Console.WriteLine("Accesso a un conto. Inserisci il nome del titolare:");
            string loginName = Console.ReadLine();
            try
            {
                var currentAccount = BankAccount.FindAccountByName(loginName);
                Console.WriteLine("Inserisci la password:");
                string pw = Console.ReadLine();
                if (!currentAccount.VerifyPassword(pw))
                {
                    Console.WriteLine("Password errata.");
                    break;
                }

                int choice;
                do
                {
                    Console.WriteLine("\nCosa vuoi fare?\n1) Deposita\n2) Preleva\n3) Visualizza saldo\n4) Visualizza cronologia\n5) Modifica nome titolare\n6) Logout");
                    while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 6)
                    {
                        Console.WriteLine("Inserisci un numero tra 1 e 6:");
                    }
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Quanti soldi vuoi depositare?:");
                            int amount;
                            do { amount = Convert.ToInt32(Console.ReadLine()); } while (amount <= 0);
                            currentAccount.Deposit(amount);
                            break;
                        case 2:
                            Console.WriteLine("Quanti soldi vuoi prelevare?:");
                            do { amount = Convert.ToInt32(Console.ReadLine()); } while (amount <= 0);
                            currentAccount.Withdraw(amount);
                            break;
                        case 3:
                            Console.WriteLine($"Il tuo saldo è €{currentAccount.GetBalance()}");
                            break;
                        case 4:
                            var history = currentAccount.GetHistory();
                            Console.WriteLine("\nCronologia:");
                            for (int i = 0; i < history.Count; i++) Console.WriteLine(history[i]);
                            break;
                        case 5:
                            Console.WriteLine("Inserisci il nuovo nome del titolare:");
                            string newName;
                            do
                            {
                                newName = Console.ReadLine();
                            }
                            while (String.IsNullOrWhiteSpace(newName) || (BankAccount.IsNameUsed(newName) && newName != currentAccount.GetName()));

                            if (newName == currentAccount.GetName())
                            {
                                Console.WriteLine("Il nome inserito è uguale a quello attuale. Nessuna modifica effettuata.");
                            }
                            else
                            {
                                currentAccount.SetName(newName);
                                Console.WriteLine($"Nome titolare aggiornato in: {newName}");
                            }
                            break;
                        case 6:
                            Console.WriteLine("Logout effettuato.");
                            break;
                    }
                } while (choice != 6);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            break;

        case 4:
            Console.WriteLine("Arrivederci");
            break;
    }

} while (mainChoice != 4);
