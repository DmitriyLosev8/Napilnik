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
            private Dictionary<Good, int> _goods; 

            public Warehouse()
            {
                _goods = new Dictionary<Good, int>();
            }

            public void Delive(Good good, int count)
            {
                if (count > 0 || good != null)
                {
                    if (_goods.ContainsKey(good))
                        _goods[good] += count;
                    else
                        _goods.Add(good, count);
                }
            }

            public void RemoveGoods(Good good, int count)
            {
                if (_goods.ContainsKey(good))
                {
                    if (_goods[good] - count == 0)
                        _goods.Remove(good);
                    else
                        _goods[good] -= count;
                }
                else
                    throw new Exception();
            }

            public void ShowGoods()
            {
                foreach (var good in _goods)
                {
                    Console.WriteLine($"{good.Key.Title} колличество - {good.Value}");
                }
            }

            public bool TryToSendGood(Good goodToSend)
            {
                if (_goods.ContainsKey(goodToSend))
                {
                    return true;
                }
                return false;
            }

            public bool VerifyСount(Good good, int count)
            {
                if (_goods[good] >= count)
                {
                    return true;
                }
                return false;
            }
        }

        class Shop
        {
            private Warehouse _warehouse;

            public string Paylink = "Оплата";

            public Shop(Warehouse warehouse)
            {
                _warehouse = warehouse;
            }

            public Cart Cart() => new Cart(this);

            public bool TryToTakeGood(Good goodToSend) => _warehouse.TryToSendGood(goodToSend);

            public bool VerifyСountInWarehouse(Good good, int count) => _warehouse.VerifyСount(good, count);

            public void RemoveGoodFromWarehouse(Good good, int count)
            {
                _warehouse.RemoveGoods(good, count);
            }
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

            public void Add(Good goodToAdd, int count)
            {
                if (Shop.TryToTakeGood(goodToAdd))
                {
                    if (Shop.VerifyСountInWarehouse(goodToAdd, count))
                    {
                        Shop.RemoveGoodFromWarehouse(goodToAdd, count);
                        _goods.Add(goodToAdd, count);
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
