
using NumberGenerator.Logic;
using System;

namespace NumberGenerator.Ui
{
    class Program
    {
        static void Main()
        {
            // Zufallszahlengenerator erstelltn
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator(250);

            // Beobachter erstellen
            BaseObserver baseObserver = new BaseObserver(numberGenerator, 10);
            StatisticsObserver statisticsObserver = new StatisticsObserver(numberGenerator, 20);
            RangeObserver rangeObserver = new RangeObserver(numberGenerator, 5, 200, 300);
            QuickTippObserver quickTippObserver = new QuickTippObserver(numberGenerator);

            // Nummerngenerierung starten
            numberGenerator.StartNumberGeneration();
            // Resultat ausgeben
            Console.WriteLine("------------------------------------------------------  RESULT  --------------------------------------------------------\n");
            Console.WriteLine($"{statisticsObserver.GetType().Name}: Received {statisticsObserver.CountOfNumbersReceived} numbers => Min='{statisticsObserver.Min}', Max='{statisticsObserver.Max}', Sum='{statisticsObserver.Sum}', Avg='{statisticsObserver.Avg}'.");
            Console.WriteLine($"{rangeObserver.GetType().Name}: Received {rangeObserver.CountOfNumbersReceived} numbers => There were '{rangeObserver.NumbersInRange}' numbers between '{rangeObserver.LowerRange}' and '{rangeObserver.UpperRange}'.");
            Console.WriteLine($"{quickTippObserver.GetType().Name}: Received {quickTippObserver.CountOfNumbersReceived} numbers => Quick-Tipp is {quickTippObserver.ConvertTippToString()}.");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
