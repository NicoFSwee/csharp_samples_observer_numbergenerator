using System;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher einfache Statistiken bereit stellt (Min, Max, Sum, Avg).
    /// </summary>
    public class StatisticsObserver : BaseObserver
    {
        #region Fields
        #endregion
        int _countOfNumbers = 0;
        #region Properties

        /// <summary>
        /// Enthält das Minimum der generierten Zahlen.
        /// </summary>
        public int Min { get; private set; } 

        /// <summary>
        /// Enthält das Maximum der generierten Zahlen.
        /// </summary>
        public int Max { get; private set; }

        /// <summary>
        /// Enthält die Summe der generierten Zahlen.
        /// </summary>
        public int Sum { get; private set; }

        /// <summary>
        /// Enthält den Durchschnitt der generierten Zahlen.
        /// </summary>
        public int Avg => Sum / _countOfNumbers;

        #endregion

        #region Constructors

        public StatisticsObserver(IObservable numberGenerator, int countOfNumbersToWaitFor) : base(numberGenerator, countOfNumbersToWaitFor)
        {

        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return base.ToString() + $" => StatisticsObserver [Min='{Min}', Max='{Max}', Sum='{Sum}', Avg='{Avg}']";
        }

        public override void OnNextNumber(int number)
        {
            if(base.CountOfNumbersReceived == 0)
            {
                Min = number;
            }
            base.OnNextNumber(number);
            _countOfNumbers++;
            
            if(number < Min)
            {
                Min = number;
            }
            else if(number > Max)
            {
                Max = number;
            }

            Sum += number;
        }

        #endregion
    }
}
