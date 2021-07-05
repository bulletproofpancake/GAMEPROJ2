public class RoundStatManager : Singleton<RoundStatManager>
{
    public int moneyEarned, obstaclesHit, totalPassengers, totalMoney;

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

    public void Earn()
    {
        totalMoney += moneyEarned;
    }

    public void EndRound()
    {
        moneyEarned = 0;
        obstaclesHit = 0;
        totalPassengers = 0;
    }
    
}