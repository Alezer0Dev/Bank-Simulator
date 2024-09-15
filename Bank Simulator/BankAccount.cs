using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Simulator
{
    public class BankAccount {
        private string name;
        private int balance;
        private string iban;
        private List<string> history = new List<string>();
        static List<BankAccount> accounts = new List<BankAccount>();
        static BankAccount currentAccount;

        BankAccount(string name, int balance, string iban) {
            this.name = name;
            this.balance = balance;
            this.iban = iban;
        }

        public static void NewAccount(string name, int balance, string iban) {
            Console.WriteLine("Creazione account..");
            accounts.Add(new BankAccount(name, balance, iban));
            Console.WriteLine("Account creato");
            Console.WriteLine("Accesso in corso..");
            LogIn(name);
        }

        public static void LogIn(string name) {
            currentAccount = FindAccountByName(name);
            Console.WriteLine("Accesso completato con successo");
        }

        static BankAccount FindAccountByName (string name) {
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

        public static bool IsIbanUsed(string iban) {
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

        public static void Deposit(int amount) {
            currentAccount.balance += amount;
            currentAccount.history.Add($"+{amount}€");
        }

        public static void Withdraw(int amount) {
            if (currentAccount.balance >= amount)
            {
                currentAccount.balance -= amount;
                currentAccount.history.Add($"-{amount}€");
            }
            else { Console.WriteLine("Non hai abbastanza soldi"); }
        }

        public static int GetBalance() { return currentAccount.balance; }

        public static void PrintHistory() {
            Console.WriteLine("\nCronologia:");
            for (int i = 0; i < currentAccount.history.Count; i++) {
                Console.WriteLine(currentAccount.history[i]);
            }
        }

        public static void LogOut() {
            Console.WriteLine("Sto uscendo dall'account..");
            currentAccount = null;
            Console.WriteLine("Logout effettuato con successo");
        }
    }
}
