using System;

namespace Napilnik1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Выведите платёжные ссылки для трёх разных систем платежа: 
            //pay.system1.ru/order?amount=12000RUB&hash={MD5 хеш ID заказа}
            //order.system2.ru/pay?hash={MD5 хеш ID заказа + сумма заказа}
            //system3.com/pay?amount=12000&curency=RUB&hash={SHA-1 хеш сумма заказа + ID заказа + секретный ключ от системы}

            Order order = new Order(333, 10);
            Mir mir = new Mir();
            Visa visa = new Visa();
            MasterCard mastercard = new MasterCard();

            Console.WriteLine(mir.GetPayingLink(order));
            Console.WriteLine(visa.GetPayingLink(order));
            Console.WriteLine(mastercard.GetPayingLink(order));
        }
    }

    public class Order
    {
        public readonly int Id;
        public readonly int Amount;

        public Order(int id, int amount) => (Id, Amount) = (id, amount);
    }

    public interface IPaymentSystem
    {
        public string Link { get; }
        public string GetPayingLink(Order order);
        
    }

    public class Mir : IPaymentSystem
    {
        public string Link => "pay.system1.ru/order?amount=12000RUB&hash={MD5 хеш ID заказа}";

        public string GetPayingLink(Order order) => $"ID заказа - {order.Id}, колличество - {order.Amount}, сслыка - {Link}";
    }

    public class Visa : IPaymentSystem
    {
        public string Link => "order.system2.ru/pay?hash={MD5 хеш ID заказа + сумма заказа}";

        public string GetPayingLink(Order order) => $"ID заказа - {order.Id}, колличество - {order.Amount}, сслыка - {Link}";
    }

    public class MasterCard : IPaymentSystem
    {
        public string Link => "system3.com/pay?amount=12000&curency=RUB&hash={SHA-1 хеш сумма заказа + ID заказа + секретный ключ от системы}";

        public string GetPayingLink(Order order) => $"ID заказа - {order.Id}, колличество - {order.Amount}, сслыка - {Link}";   
    }
}
