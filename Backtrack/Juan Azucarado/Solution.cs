namespace JuanAzucarado
{
    public class Solution
    {
        public static long Solve(long capacity, long[] buy_prices, long[] buy_capacities, long[] sell_prices, long[] sell_capacities)
        {
            (long, long)[] buy = new (long, long)[buy_prices.Length];
            (long, long)[] sell = new (long, long)[sell_capacities.Length];
            for (int i = 0; i < buy.Length; i++)
            {
                buy[i] = (buy_prices[i], buy_capacities[i]);
            }
            for (int j = 0; j < sell.Length; j++)
            {
                sell[j] = (sell_prices[j], sell_capacities[j]);
            }
            buy = buy.OrderBy(x => x.Item1).ToArray();

            return Sell(capacity, buy, sell, capacity, 0, 0);
        }

        private static long Buy(long capacity, (long, long)[] buy, long capacity_actual)
        {
            long result = 0;
            int i = 0;
            long j = buy[i].Item2;
            if (capacity_actual == capacity) return 0;
            while (capacity_actual != capacity && i < buy.Length)
            {
                capacity_actual += 1;
                j -= 1;
                result += buy[i].Item1;
                if (j == 0)
                {
                    if (i + 1 < buy.Length)
                    {
                        i++;
                        j = buy[i].Item2;
                    }
                }
            }
            return result;
        }
        static long Sell(long maxCapacity, (long, long)[] buy, (long, long)[] sell, long actual_capacity, long money, long index)
        {
            if (index == sell.Length || actual_capacity == 0)
            {
                long pross = Buy(maxCapacity, buy, actual_capacity);
                long profit = money - pross;
                return profit;
            }
            if (actual_capacity < sell[index].Item2)
            {
                return Sell(maxCapacity, buy, sell, actual_capacity, money, ++index);  
            }
            long bestProfit = Math.Max(
                //sell
                Sell(maxCapacity, buy, sell, actual_capacity - sell[index].Item2, money + (sell[index].Item1 * sell[index].Item2), ++index),
                //ignore
                Sell(maxCapacity, buy, sell, actual_capacity, money, index)
            );
            return bestProfit;
        }
    }
}