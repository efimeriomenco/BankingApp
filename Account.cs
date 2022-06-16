using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingApp.Models;

namespace BankingApp
{
    public abstract class Account
    {
        public int Id { get; }
        public double Balance { get; protected set; }
        public int numberOfcalls { get; set; } = 0;
        private List<Transaction> Transactions { get; }

        public double CalculateBalance()
        {
            double totalBalance = 0;
            foreach (var transaction in Transactions)
            {
                if (transaction.Type == TransactionType.Withdraw || 
                    transaction.Type == TransactionType.Transfer)
                {
                    totalBalance -= transaction.Amount;
                }
                else
                {
                    totalBalance += transaction.Amount;
                }
            }
            return totalBalance;
        }


        public virtual void Deposit(double amount)
        {
            Balance += amount;
            numberOfcalls += 1;
            Transactions.Add(
                    new Transaction(
                    Id,
                    amount,
                    TransactionType.Deposit));
        }

        public virtual bool Withdraw(double amount)
        {
            numberOfcalls += 1;
            if (Balance < amount)
                return false;

            Balance -= amount;

            Transactions.Add(
                new Transaction(
                    Id,
                    amount,
                    TransactionType.Withdraw));
            return true;
        }

        public virtual bool Transfer(Account accountB, int amount)
        {
            numberOfcalls += 1;
            if (Balance < amount)
                return false;

            Balance -= amount;
            
            accountB.Deposit(amount);

            Transactions.Add(
                new Transaction(
                    Id,
                    amount,
                    TransactionType.Transfer));

            return true;
        }

        protected Account(int id)
        {
            this.Id = id;
            this.Transactions = new List<Transaction>();
        }
    }

    public class Transaction
    {
        public Guid Id { get; }
        public int AccountId { get; }
        public double Amount { get; }
        public TransactionType Type { get; }
        public DateTime Date { get; }

        public Transaction(int accountId, double amount, TransactionType type)
        {
            this.Id = Guid.NewGuid();
            this.Amount = amount;
            this.AccountId = accountId;
            this.Type = type;
            this.Date = DateTime.Now;
        }
    }

    public enum TransactionType
    {
        Deposit,
        Withdraw,
        Transfer
    }
}
