using System;
using System.IO;

namespace Napilnik1
{
    class Program
    {
        static void Main(string[] args)
        {
            FileLogWritter fileLogWritter = new FileLogWritter();
            ConsoleLogWritter consoleLogWritter = new ConsoleLogWritter();

            Pathfinder pathfinder1 = new Pathfinder(fileLogWritter);
            Pathfinder pathfinder2 = new Pathfinder(consoleLogWritter);
            Pathfinder pathfinder3 = new Pathfinder(new SecureLogWritter(fileLogWritter));
            Pathfinder pathfinder4 = new Pathfinder(new SecureLogWritter(consoleLogWritter));
            Pathfinder pathfinder5 = new Pathfinder(new DifferentWaysLogWritter(consoleLogWritter, fileLogWritter));
        }

        class FileLogWritter : Ilogger
        {
            void Ilogger.WriteError(string message)
            {
                File.WriteAllText("log.txt", message);
            }
        }

        class ConsoleLogWritter : Ilogger
        {
            void Ilogger.WriteError(string message)
            {
                Console.WriteLine(message);
            }
        }

        class SecureLogWritter : Ilogger
        {
            private Ilogger _logger;

            public SecureLogWritter(Ilogger logger)
            {
                _logger = logger;
            }

            public void SecuredWrite(string message)
            {
                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                    _logger.WriteError(message);
            }
        }

        class DifferentWaysLogWritter : Ilogger
        {
            private Ilogger _securedLogger;
            private Ilogger _unSecuredLogger;

          public DifferentWaysLogWritter(Ilogger unSecuredLogger, Ilogger securedLogger)
            {
                _securedLogger = securedLogger;
                _unSecuredLogger = unSecuredLogger;
            }

            public void DifferentWaysWrite( string message)
            {
                _unSecuredLogger.WriteError(message);

                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                    _securedLogger.WriteError(message);
            }
        }

        class Pathfinder 
        {
            private Ilogger _logger;

            public Pathfinder(Ilogger logger)
            {
                _logger = logger;
            }

            public void Find(string message)
            {
                _logger.WriteError(message);
            }
        }

        public interface Ilogger
        {
            public void WriteError(string message) { }
        }
    }
}
    






