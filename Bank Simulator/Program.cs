using Bank_Simulator;
using System.Xml.Serialization;

Console.OutputEncoding = System.Text.Encoding.UTF8;
string name;
int bonus = 1000;
int balance = 0;
string iban;
int choice;
int amount;
Random rand = new Random();
Bank_Simulator.BankAccount currentAccount = null;

Console.WriteLine("Inserisci il tuo nome: ");

do
{
    name = Console.ReadLine();
}
while (String.IsNullOrWhiteSpace(name) || BankAccount.IsNameUsed(name));

balance += bonus;

do
{
    iban = "IT";
    iban = iban += rand.Next(1000000000, 1999999999);
} while (BankAccount.IsIbanUsed(iban));

currentAccount = new BankAccount(name, balance, iban);
Console.WriteLine($"Conto creato con i seguenti dati:\nNome titolare: {name}\nSaldo: {balance}\nIBAN: {iban}");
do
{
    Console.WriteLine("Cosa vuoi fare?\n1) Deposita\n2) Preleva\n3) Visualizza saldo\n4) Visualizza cronologia delle operazioni\n5) Logout");

    do
    {
        choice = Convert.ToInt32(Console.ReadLine());
    }
    while (choice < 1 || choice > 5);

    switch (choice)
    {
            case 1:
            Console.WriteLine("Quanti soldi vuoi depositare?:");
            do
            {
                amount = Convert.ToInt32(Console.ReadLine());
            }
            while (amount <= 0);
            currentAccount.Deposit(amount);
            amount = 0;
            break;

            case 2:
            Console.WriteLine("Quanti soldi vuoi prelevare?:");
            do
            {
                amount = Convert.ToInt32(Console.ReadLine());
            }
            while (amount <= 0);
            currentAccount.Withdraw(amount);
            amount = 0;
            break;

        case 3:
            Console.WriteLine($"Il tuo saldo è €{currentAccount.GetBalance()}");
            break;

        case 4:
            var history = currentAccount.GetHistory();
            Console.WriteLine("\nCronologia:");
            for (int i = 0; i < history.Count; i++)
            {
                Console.WriteLine(history[i]);
            }
            break;

        case 5:
            currentAccount = null;
            break;

        default:
            Console.WriteLine("Scelta non valida");
            break;
    }
}
while (choice != 5);