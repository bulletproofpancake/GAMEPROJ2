public class RoundStatManager : Singleton<RoundStatManager>
{
    public int moneyEarned, obstaclesHit, totalPassengers, totalMoney;

    public int net;
    // public void Collect(int payment)
    // {
    //     currentMoney += payment;
    // }
    //
    // public void Earn()
    // {
    //     roundMoney += currentMoney;
    //     totalMoney += roundMoney;
    // }
    //
    // public void EndRound()
    // {
    //     currentMoney = 0;
    //     roundMoney = 0;
    // }
    //
    // public void Spend(int cost)
    // {
    //     totalMoney -= cost;
    // }

    public void CollectPayment(int payment)
    {
        moneyEarned += payment;
    }

    public void HitObstacle()
    {
        obstaclesHit++;
    }

    public void CompleteCustomer()
    {
        totalPassengers++;
    }
    
    private void CalculateNet()
    {
        net = moneyEarned - obstaclesHit * 10;
        if (net <= 0)
        {
            net = 0;
        }
    }
    
    public void Earn()
    {
        CalculateNet();
        totalMoney += net;
    }

    public void EndRound()
    {
        moneyEarned = 0;
        obstaclesHit = 0;
        totalPassengers = 0;
    }
    
}