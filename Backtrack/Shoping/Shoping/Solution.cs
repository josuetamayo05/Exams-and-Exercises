using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

public interface IProduct
{
    int Price { get; }
    string Name { get; }
}

public interface IProductQuantity
{
    IProduct Product { get; }
    int Quantity { get; }
}

public interface ICombo
{
    int Price { get; }
    IProductQuantity[] Products { get; }
}
public static class Exam
{
    public static int Solve(IProduct[] products, ICombo[] combos, IProductQuantity[] desired)
    {
        int cantidad = 0;
        for (int i = 0; i < desired.Length; i++)
        {
            cantidad += desired[i].Quantity;
        }
        (IProduct, int)[] product = PrepararComida(desired);
        int min_actual = Buy_Individual(product, desired);
        int actual = min_actual;

        return Solve(product, combos, desired, min_actual, actual, 0);
    }

    public static (IProduct, int)[] PrepararComida(IProductQuantity[] desired)
    {
        (IProduct, int)[] ans = new (IProduct, int)[desired.Length];
        for (int i = 0; i < desired.Length; i++)
        {
            ans[i] = (desired[i].Product, 0);
        }
        return ans;
    }
    public static int Solve((IProduct, int)[] product, ICombo[] combos, IProductQuantity[] desired, int min, int gastoCompraDirecta, int count)
    {
        if (count >= gastoCompraDirecta) return int.MaxValue;
        if (AllBulled(product, desired)) return 0;

        for (int i = 0; i < combos.Length; i++)
        {
            if (ContainsCombo(combos[i], desired))
            {
                (IProduct, int)[] aux = ((IProduct, int)[])product.Clone();
                int gastoAux = min;
                ComprarCombo(combos[i], aux);
                int nextBuy = Solve(aux, combos, desired, Buy_Individual(aux, desired), gastoCompraDirecta, count + combos[i].Price);
                int compareBuy = nextBuy == int.MaxValue ? int.MaxValue : nextBuy + combos[i].Price;
                min = Math.Min(compareBuy, gastoAux);
            }
        }
        return min;
    }
    static bool ContainsCombo(ICombo combo, IProductQuantity[] desired)
    {
        for (int i = 0; i < desired.Length; i++)
        {
            for (int j = 0; j < combo.Products.Length; j++)
            {
                if (combo.Products[j].Product.Name == desired[i].Product.Name) return true;
            }
        }
        return false;
    }
    static void ComprarCombo(ICombo combo, (IProduct, int)[] products)
    {
        for (int i = 0; i < combo.Products.Length; i++)
        {
            for (int j = 0; j < products.Length; j++)
            {
                if (combo.Products[i].Product.Name == products[j].Item1.Name)
                {
                    products[j].Item2 += combo.Products[i].Quantity;
                }
            }
        }
    }
    static bool AllBulled((IProduct, int)[] product, IProductQuantity[] desired)
    {
        for (int i = 0; i < desired.Length; i++)
        {
            if (product[i].Item2 < desired[i].Quantity) return false;
        }
        return true;
    }
    public static int Buy_Individual((IProduct, int)[] products, IProductQuantity[] desired)
    {
        int result = 0;
        int necesario = 0;
        for (int index = 0; index < desired.Length; index++)
        {
            necesario = Math.Max(0, desired[index].Quantity - products[index].Item2);
            result += desired[index].Product.Price * necesario;
        }
        return result;
    }
}