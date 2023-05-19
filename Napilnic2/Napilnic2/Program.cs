using System;

namespace Napilnik1
{
    class Program
    {
        static void Main(string[] args)
        {
            Good iPhone12 = new Good("IPhone 12");
            Good iPhone11 = new Good("IPhone 11");

            Warehouse warehouse = new Warehouse();

            Shop shop = new Shop(warehouse);

            warehouse.Delive(iPhone12, 10);
            warehouse.Delive(iPhone11, 1);

            //Вывод всех товаров на складе с их остатком
            warehouse.ShowGoods();

            Cart cart = shop.Cart();
            cart.Add(iPhone12, 4);
            cart.Add(iPhone11, 3); //при такой ситуации возникает ошибка так, как нет нужного количества товара на складе

            //Вывод всех товаров в корзине
            cart.ShowGoods();

            Console.WriteLine(cart.Order().Paylink);

            cart.Add(iPhone12, 9); //Ошибка, после заказа со склада убираются заказанные товары
        }

        class Good
        {
            public string Title { get; private set; }

            public Good(string title)
            {
                Title = title;
            }
        }

        class Warehouse
        {
            public Dictionary<Good, int> Goods { get; private set; }

            public Warehouse()
            {
                Goods = new Dictionary<Good, int>();
            }

            public void Delive(Good good, int count)
            {
                if (count > 0 || good != null)
                {
                    if (Goods.ContainsKey(good))
                        Goods[good] += count;
                    else
                        Goods.Add(good, count);
                }
            }

            public void RemoveGoods(Good good, int count)
            {
                if (Goods.ContainsKey(good))
                {
                    if (Goods[good] - count == 0)
                        Goods.Remove(good);
                    else
                        Goods[good] -= count;
                }
                else
                    throw new Exception();
            }

            public void ShowGoods()
            {
                foreach (var good in Goods)
                {
                    Console.WriteLine($"{good.Key.Title} колличество - {good.Value}");
                }
            }
        }

        class Shop
        { 
            public string Paylink = "Оплата";

            public Warehouse Warehouse { get; private set; }

            public Shop(Warehouse warehouse)
            {
                Warehouse = warehouse;
            }

            public Cart Cart() => new Cart(this);
        }

        class Cart
        {     
            private Dictionary<Good, int> _goods;

            public Shop Shop { get; private set; }

            public Cart(Shop shop)
            {
                Shop = shop;
                _goods = new Dictionary<Good, int>();
            }

            public Shop Order() => Shop;

            public void ShowGoods()
            {
                foreach (var good in _goods)
                    Console.WriteLine($"Товары в корзине: {good.Key.Title} колличество -  {good.Value}");
            }

            public void Add(Good good, int count)
            {
                if (Shop.Warehouse.Goods.ContainsKey(good))
                {
                    if (Shop.Warehouse.Goods[good] >= count)
                    {
                        Shop.Warehouse.RemoveGoods(good, count);
                        _goods.Add(good, count);
                    }
                    else
                        throw new ArgumentOutOfRangeException();
                }
                else
                    throw new ArgumentOutOfRangeException();
            }
        }
    }  
}
