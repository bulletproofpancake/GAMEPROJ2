namespace Money
{
    public class RoundMoneyManager : Singleton<RoundMoneyManager>
    {
        public int currentMoney, roundMoney, totalMoney;

        public void Collect(int payment)
        {
            currentMoney += payment;
        }

        public void Earn()
        {
            roundMoney += currentMoney;
            totalMoney += roundMoney;
        }

        public void EndRound()
        {
            currentMoney = 0;
            roundMoney = 0;
        }

        public void Spend(int cost)
        {
            totalMoney -= cost;
        }
        
    }
}
