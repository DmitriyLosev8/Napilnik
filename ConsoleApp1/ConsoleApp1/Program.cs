using System;
using System.IO;

namespace Napilnik1
{
    class Program
    {
        static void Main(string[] args)
        {
            Pathfinder variantOfLogger1 = new Pathfinder(1);
            Pathfinder variantOfLogger2 = new Pathfinder(2);
            Pathfinder variantOfLogger3 = new Pathfinder(3);
            Pathfinder variantOfLogger4 = new Pathfinder(4);
            Pathfinder variantOfLogger5 = new Pathfinder(5);
        }

        class Writer
        {
            public virtual void WriteError(string message) {}
        }

        class ConsoleLogWritter : Writer
        {
            public override void WriteError(string message)
            {
                Console.WriteLine(message);
            }
        }

        class FileLogWritter : Writer
        {
            public override void WriteError(string message)
            {
                File.WriteAllText("log.txt", message);
            }
        }

        class SecureLogWritter 
        { 
            public void SecuredWrite(Writer writer, string message)
            {
                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                {
                    writer.WriteError(message);
                }
            }
        }

        class Pathfinder : Logger
        {
            public Pathfinder(int variantOfLogger) : base(variantOfLogger) {}
           
            public void Find(string message)
            {
                ChooseLoggerToWrite(message);
            }
        }

        class Logger
        {
            private int _variantOfLogger;
            private ConsoleLogWritter _consoleLogWritter = new ConsoleLogWritter();
            private FileLogWritter _fileLogWritter = new FileLogWritter();
            private SecureLogWritter _secureLogWritter = new SecureLogWritter();

            public Logger(int variantOfLogger)
            {
                _variantOfLogger = variantOfLogger;
            }

            protected void ChooseLoggerToWrite(string message)
            {
                switch (_variantOfLogger)
                {
                    case 1:
                        _fileLogWritter.WriteError(message);
                        break;
                    case 2:
                        _consoleLogWritter.WriteError(message);
                        break;
                    case 3:
                        _secureLogWritter.SecuredWrite(_fileLogWritter,message);
                        break;
                    case 4:
                        _secureLogWritter.SecuredWrite(_consoleLogWritter, message);
                        break;
                    case 5:
                        _consoleLogWritter.WriteError(message);
                        _secureLogWritter.SecuredWrite(_fileLogWritter, message);
                        break;
                }
            }
        }
    }
}
    






