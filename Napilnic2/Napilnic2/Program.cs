using System;

namespace Napilnik1
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderForm orderForm = new OrderForm();
            PaymentHandler paymentHandler = new PaymentHandler();

            string enteredSystemId = orderForm.ShowForm();

            paymentHandler.TryToChoosePaymentSystem(enteredSystemId);
        }
    }

    public class OrderForm
    {
        public string ShowForm()
        {
            Console.WriteLine("Мы принимаем: QIWI, WebMoney, Card");

            //симуляция веб интерфейса
            Console.WriteLine("Какое системой вы хотите совершить оплату?");
            return Console.ReadLine();
        }
    }

    public class PaymentHandler
    {
        private List<IpaymentSystem> _paymentSystems;
        private QIWI _qiwi = new QIWI();
        private WebMoney _webMoney = new WebMoney();
        private Card _card = new Card();

        public PaymentHandler()
        {
            _paymentSystems = new List<IpaymentSystem>();
            _paymentSystems.Add(_qiwi);
            _paymentSystems.Add(_webMoney);
            _paymentSystems.Add(_card);
        }
        
        public void TryToChoosePaymentSystem(string enteredSystemId)
        {
            foreach (var paymentSystem in _paymentSystems)
            {
                if (paymentSystem.Id == enteredSystemId)
                {
                    paymentSystem.ConnectToSystem();
                    TryToMaketPayment(paymentSystem);
                }
            }
        }

        public void TryToMaketPayment(IpaymentSystem paymentSystem)
        {
            Console.WriteLine($"Вы оплатили с помощью {paymentSystem.Id}");

            paymentSystem.MakePayment();
        }
    }

    public interface IpaymentSystem
    {
        public string Id { get; }

        public void MakePayment();
        public void ConnectToSystem();
        public void DetermineResult();
    }

    public class QIWI : IpaymentSystem
    {
        public string Id => "QIWI";

        void IpaymentSystem.MakePayment()
        {
            Console.WriteLine("Проверка платежа через QIWI...");
            DetermineResult();
        }

        void IpaymentSystem.ConnectToSystem()
        {
            Console.WriteLine("Перевод на страницу QIWI...");
        }

        public void DetermineResult()
        {
            if (true)  //проверка
                Console.WriteLine("Оплата прошла успешно!");
            else
                Console.WriteLine("Оплата не прошла");
        }
    }

    public class WebMoney : IpaymentSystem
    {
        public string Id => "WebMoney";

        void IpaymentSystem.MakePayment()
        {
            Console.WriteLine("Проверка платежа через WebMoney...");
            DetermineResult();
        }

        void IpaymentSystem.ConnectToSystem()
        {
            Console.WriteLine("Вызов API WebMoney...");
        }

        public void DetermineResult()
        {
            if (true)  //проверка
                Console.WriteLine("Оплата прошла успешно!");
            else
                Console.WriteLine("Проверка не прошла");
        }
    }

    public class Card : IpaymentSystem
    {
        public string Id => "Card";

        void IpaymentSystem.MakePayment()
        {
            Console.WriteLine("Проверка платежа через Card...");
            DetermineResult();
        }

        void IpaymentSystem.ConnectToSystem()
        {
            Console.WriteLine("Вызов API банка эмитера карты Card...");
        }

        public void DetermineResult()
        {
            if (true)  //проверка
                Console.WriteLine("Оплата прошла успешно!");
            else
                Console.WriteLine("Оплата не прошла");
        }
    } 
}
