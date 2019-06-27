using System;
using UmrechnungTaschenrechner.Calculators;
using UmrechnungTaschenrechner.Converters;

namespace UmrechnungTaschenrechner
{
    class ConsoleView
    {
        static string[] history = new string[50];
        static int amountHistory = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Alle Zahlen brauchen einen Präfix.\nd = Dezimal\no = Octal\nb = Binär\nh = Hexadezimal");
            bool repeat = true;
            while (repeat)
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("1 - Berechnen");
                Console.WriteLine("2 - Historie ansehen");
                Console.WriteLine("3 - Beenden");
                Console.WriteLine("----------------------------");
                switch (Console.ReadLine())
                {
                    case "1":
                        Calculate();
                        break;
                    case "2":
                        History();
                        break;
                    case "3":
                        repeat = false;
                        break;
                    default:
                        Console.WriteLine("Bitte eine der Nummern aus dem Menü eingeben!");
                        break;
                }
            }
        }

        static void Calculate()
        {
            Console.WriteLine("Bitte einen Term eingeben und dabei Zahlen, Operanden und Klammern mit ' '(Leerzeichen) trennen:");
            var term = Console.ReadLine();
            term = term.Replace('.', ',');
            Console.WriteLine("Das Ergebnis lautet:");
            var res = Term.DeriveTerm(term).Substring(1);
            Console.WriteLine("Dezimal: " + Converter.DecimalToString(res, 10));
            Console.WriteLine("Binär: " + Converter.DecimalToString(res, 2));
            Console.WriteLine("Octal: " + Converter.DecimalToString(res, 8));
            Console.WriteLine("Hexadezimal: " + Converter.DecimalToString(res, 16));
            AddHistory(term + " = d" + res);
        }

        static void History()
        {
            if(amountHistory == 0)
            {
                Console.WriteLine("Noch gibt es hier nichts zu sehen.");
                return;
            }
            Console.WriteLine("Historie: ");
            for(int i = 0; i < amountHistory; i++)
            {
                Console.WriteLine(history[i]);
            }
        }

        static void AddHistory(string s)
        {
            if(amountHistory == 50)
            {
                for(int i = 0; i <amountHistory-1; i++)
                {
                    string tmp = history[i];
                    history[i] = history[i + 1];
                    history[i + 1] = tmp;
                }
                amountHistory--;
            }
            history[amountHistory++] = s;
        }
    }
}
