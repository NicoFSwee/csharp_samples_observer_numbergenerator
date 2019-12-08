using System;
using System.ComponentModel;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher die Zahlen auf der Konsole ausgibt.
    /// Diese Klasse dient als Basisklasse für komplexere Beobachter.
    /// </summary>
    public class BaseObserver : IObserver
    {
        #region Fields

        private readonly IObservable _numberGenerator;

        #endregion

        #region Properties

        public int CountOfNumbersReceived { get; private set; }
        public int CountOfNumbersToWaitFor { get; private set; }

        #endregion

        #region Constructors

        public BaseObserver(IObservable numberGenerator, int countOfNumbersToWaitFor)
        {
            _numberGenerator = numberGenerator;
            _numberGenerator.Attach(this);

            if(countOfNumbersToWaitFor >= 0)
            {
                CountOfNumbersToWaitFor = countOfNumbersToWaitFor;
            }
            else
            {
                throw new ArgumentException();
            }

        }

        #endregion

        #region Methods

        #region IObserver Members

        /// <summary>
        /// Wird aufgerufen wenn der NumberGenerator eine neue Zahl generiert hat.
        /// </summary>
        /// <param name="number"></param>
        public virtual void OnNextNumber(int number)
        {
            CountOfNumbersReceived++;

            // Sobald die Anzahl der max. Beobachtungen (_countOfNumbersToWaitFor) erreicht ist -> Detach()
            if (CountOfNumbersReceived >= CountOfNumbersToWaitFor)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"   >> {this.GetType().Name}: Received '{CountOfNumbersReceived}' of '{CountOfNumbersToWaitFor}' => I am not interested in new numbers anymore => Detach().");
                Console.ResetColor();
                DetachFromNumberGenerator();
            }

        }

        #endregion

        public override string ToString()
        {
            return $"BaseObserver [CountOfNumbersReceived='{CountOfNumbersReceived}', CountOfNumbersToWaitFor='{CountOfNumbersToWaitFor}']";
        }

        protected void DetachFromNumberGenerator()
        {
            _numberGenerator.Detach(this);
        }

        #endregion
    }
}
