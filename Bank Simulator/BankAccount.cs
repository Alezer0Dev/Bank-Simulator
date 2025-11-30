using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Simulator
{
    public class BankAccount
    {
        private string name;
        private int balance;
        private string iban;
        private List<string> history = new List<string>();
        static List<BankAccount> accounts = new List<BankAccount>();

        public BankAccount(string name, int balance, string iban)
        {
            Console.WriteLine("Creazione account..");
            this.name = name;
            this.balance = balance;
            this.iban = iban;
            accounts.Add(this);
            Console.WriteLine("Account creato");
        }

        public BankAccount LogIn(string name)
        {
            var acc = FindAccountByName(name);
            Console.WriteLine("Accesso completato con successo");
            return acc;
        }

        static BankAccount FindAccountByName(string name)
        {
            if (accounts.Count == 0) throw new Exception("Non esiste nessun account");
            for (int i = 0; i < accounts.Count; i++)
            {
                if (name == accounts[i].name)
                {
                    return accounts[i];
                }
            }
            throw new Exception("Nessun account esistente con questo nome");
        }

        public static bool IsIbanUsed(string iban)
        {
            if (accounts.Count == 0) return false;
            for (int i = 0; i < accounts.Count; i++)
            {
                if (iban == accounts[i].iban)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsNameUsed(string name)
        {
            if (accounts.Count == 0) return false;
            for (int i = 0; i < accounts.Count; i++)
            {
                if (name == accounts[i].name)
                {
                    return true;
                }
            }
            return false;
        }

        public void Deposit(int amount)
        {
            this.balance += amount;
            this.history.Add($"+{amount}€");
        }

        public void Withdraw(int amount)
        {
            if (this.balance >= amount)
            {
                this.balance -= amount;
                this.history.Add($"-{amount}€");
            }
            else { Console.WriteLine("Non hai abbastanza soldi"); }
        }

        public int GetBalance() { return this.balance; }

        public void PrintHistory()
        {
            Console.WriteLine("\nCronologia:");
            for (int i = 0; i < this.history.Count; i++)
            {
                Console.WriteLine(this.history[i]);
            }
        }
    }
}