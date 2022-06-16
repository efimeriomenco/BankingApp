namespace BankingApp.Models
{
    public class VipAccount : Account
    {
     
        public VipAccount(int id): base(id)
        {
        }

        public override void Deposit(double amount)
        {
           // Balance += amount * 1.001;
           
            var percentageToEarn = 0.1;
            var earnedInterest = amount * percentageToEarn / (double)100;

            Balance += amount + earnedInterest;
        }

    }
}
