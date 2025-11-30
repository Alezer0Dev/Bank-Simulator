using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Simulator
{
    public class BankAccount
    {
        private string _name;
        private int _balance;
        private string _password;
        private string _iban;
        private List<string> _history = new List<string>();
        static List<BankAccount> _accounts = new List<BankAccount>();

        public BankAccount(string name, int balance, string iban, string password)
        {
            Console.WriteLine("Creazione account..");
            this._name = name;
            this._balance = balance;
            this._iban = iban;
            this._password = password;
            _accounts.Add(this);
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
            if (_accounts.Count == 0) throw new Exception("Non esiste nessun account");
            for (int i = 0; i < _accounts.Count; i++)
            {
                if (name == _accounts[i]._name)
                {
                    return _accounts[i];
                }
            }
            throw new Exception("Nessun account esistente con questo nome");
        }

        public static bool IsIbanUsed(string iban)
        {
            if (_accounts.Count == 0) return false;
            for (int i = 0; i < _accounts.Count; i++)
            {
                if (iban == _accounts[i]._iban)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsNameUsed(string name)
        {
            if (_accounts.Count == 0) return false;
            for (int i = 0; i < _accounts.Count; i++)
            {
                if (name == _accounts[i]._name)
                {
                    return true;
                }
            }
            return false;
        }

        public void Deposit(int amount)
        {
            this._balance += amount;
            this._history.Add($"+{amount}€");
        }

        // Verifica la password fornita (non espone la password reale)
        public bool VerifyPassword(string password)
        {
            return this._password == password;
        }

        // Permette di cambiare la password fornendo la vecchia password
        public bool ChangePassword(string oldPassword, string newPassword)
        {
            if (!VerifyPassword(oldPassword)) return false;
            this._password = newPassword;
            return true;
        }

        public void Withdraw(int amount)
        {
            if (this._balance >= amount)
            {
                this._balance -= amount;
                this._history.Add($"-{amount}€");
            }
            else { Console.WriteLine("Non hai abbastanza soldi"); }
        }

        public int GetBalance() { return this._balance; }

        public string GetName() => this._name;

        public void SetName(string name) => this._name = name;

        public List<string> GetHistory() => this._history;
    }
}