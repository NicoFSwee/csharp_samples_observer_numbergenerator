using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher auf einen vollständigen Quick-Tipp wartet: 6 unterschiedliche Zahlen zw. 1 und 45.
    /// </summary>
    public class QuickTippObserver
    {
        #region Fields

        private RandomNumberGenerator _numberGenerator;
        private const int _FULLTIPP = 6;

        #endregion

        #region Properties

        public List<int> QuickTippNumbers { get; private set; }
        public int CountOfNumbersReceived { get; private set; }

        #endregion

        #region Constructor

        public QuickTippObserver(RandomNumberGenerator numberGenerator)
        {
            QuickTippNumbers = new List<int>();
            _numberGenerator = numberGenerator;
            numberGenerator.NumberChanged += OnNextNumber;
        }

        #endregion

        #region Methods

        public void OnNextNumber(object sender, int number)
        {
            CountOfNumbersReceived++;

            if (!QuickTippNumbers.Contains(number) && QuickTippNumbers.Count < _FULLTIPP && number < 46)
            {
                QuickTippNumbers.Add(number);
            }

            if(QuickTippNumbers.Count == _FULLTIPP)
            {
                DetachFromNumberGenerator();
            }
        }

        public override string ToString()
        {
            return base.ToString() + $" QuickTippObserver => CountOfNumbersRecieve: {CountOfNumbersReceived}";
        }

        private void DetachFromNumberGenerator()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"   >> {this.GetType().Name}: Got a full Qick-Tipp => I am not interested in numbers anymore!");
            Console.ResetColor();
            _numberGenerator.NumberChanged -= OnNextNumber;
        }

        public string ConvertTippToString()
        {
            QuickTippNumbers.Sort();

            return $"{QuickTippNumbers[0]}, {QuickTippNumbers[1]}, {QuickTippNumbers[2]}, {QuickTippNumbers[3]}, {QuickTippNumbers[4]}, {QuickTippNumbers[5]}";
        }

        #endregion
    }
}
