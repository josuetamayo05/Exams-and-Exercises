namespace JuanSalado
{
    public class Solution
    {
        public static long Solve(long capacity, long[] buy_prices, long[] buy_capacities, long[] sell_prices, long[] sell_capacities)
        {
            long sum = sell_capacities.Where(x => x <= capacity).Sum();
            long maxCapacity = Math.Min(sum, capacity);
            (long, long)[] buy = new (long, long)[buy_prices.Length];
            (long, long)[] sell = new (long, long)[sell_capacities.Length];
            for (int i = 0; i < buy_prices.Length; i++)
            {
                buy[i] = (buy_prices[i], buy_capacities[i]);
            }
            for (int j = 0; j < sell_capacities.Length; j++)
            {
                sell[j] = (sell_prices[j], sell_capacities[j]);
            }
            buy = buy.OrderBy(x => x.Item1).ToArray();
            (long,long) buy_current = Buy(maxCapacity, buy,0);        

            sell = sell.Where(x => x.Item2 <= capacity).ToArray();
            sell = sell.OrderByDescending(x => x.Item1).ToArray();

            return Sell(buy_current.Item2, sell, buy, 0, 0, buy_current.Item1);
        }
        private static (long, long) Buy(long capacity, (long, long)[] buy, long actualCapacity)
        {
            if (actualCapacity == capacity) return (0, actualCapacity);
            long perdida = 0;
            int index = 0;
            while (actualCapacity != capacity)
            {
                if (buy[index].Item2 == 0)
                {
                    index++;
                }
                else
                {
                    perdida += buy[index].Item1;
                    buy[index].Item2 -= 1;
                    actualCapacity += 1;
                }
            }
            return (perdida, actualCapacity);
        }
        private static long Sell(long capacity, (long, long)[] sell, (long, long)[] buy, long index, long money, long loss)
        {
            if (index == sell.Length || capacity == 0)
            {
                return money - loss; 
            }
            if (sell[index].Item2 > capacity)
            {
                return Sell(capacity, sell, buy, index+1, money,loss);
            }
            long bestProfit = Math.Max(
                Sell(capacity-sell[index].Item2, sell, buy, index+1, money + (sell[index].Item1 * sell[index].Item2), loss),
                Sell(capacity, sell, buy, index+1, money,loss)
            );
            return bestProfit;
        }
    }
}