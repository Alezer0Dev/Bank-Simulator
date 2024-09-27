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

Console.WriteLine("Inserisci il tuo nome: ");

do {
    name = Console.ReadLine();
}
while (String.IsNullOrWhiteSpace(name) || BankAccount.IsNameUsed(name));

balance += bonus;

do {
    iban = "IT";
    iban = iban += rand.Next(1000000000, 1999999999);
} while (BankAccount.IsIbanUsed(iban));

BankAccount.NewAccount(name, balance, iban);
Console.WriteLine($"Conto creato con i seguenti dati:\nNome titolare: {name}\nSaldo: {balance}\nIBAN: {iban}");
do
{
    Console.WriteLine("Cosa vuoi fare?\n1) Deposita\n2) Preleva\n3) Visualizza saldo\n4) Visualizza cronologia delle operazioni\n5) Logout");

    do {
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
            BankAccount.Deposit(amount);
            amount = 0;
            break;

        case 2:
            Console.WriteLine("Quanti soldi vuoi prelevare?:");
            do
            {
                amount = Convert.ToInt32(Console.ReadLine());
            }
            while (amount <= 0);
            BankAccount.Withdraw(amount);
            amount = 0;
            break;

        case 3:
            Console.WriteLine($"Il tuo saldo è €{BankAccount.GetBalance()}");
            break;

        case 4:
            BankAccount.PrintHistory();
            break;

        case 5:
            BankAccount.LogOut();
            break;

        default:
            Console.WriteLine("Scelta non valida");
            break;
    }
}
while (choice != 5);
