using System;
using System.Collections.Generic;
using System.Text;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher die Anzahl der generierten Zahlen in einem bestimmten Bereich zählt. 
    /// </summary>
    public class RangeObserver : BaseObserver
    {
        #region Properties

        /// <summary>
        /// Enthält die untere Schranke (inkl.)
        /// </summary>
        public int LowerRange { get; private set; }
        
        /// <summary>
        /// Enthält die obere Schranke (inkl.)
        /// </summary>
        public int UpperRange { get; private set; }

        /// <summary>
        /// Enthält die Anzahl der Zahlen, welche sich im Bereich befinden.
        /// </summary>
        public int NumbersInRange { get; private set; }

        /// <summary>
        /// Enthält die Anzahl der gesuchten Zahlen im Bereich.
        /// </summary>
        public int NumbersOfHitsToWaitFor { get; private set; }

        #endregion

        #region Constructors

        public RangeObserver(RandomNumberGenerator numberGenerator, int numberOfHitsToWaitFor, int lowerRange, int upperRange) : base(numberGenerator, int.MaxValue)
        {
            if(lowerRange > upperRange)
            {
                throw new ArgumentException("Lower bound is bigger than upper bound.");
            }
            if(numberOfHitsToWaitFor <= 0)
            {
                throw new ArgumentException("Negative numbers not allowed");
            }

            UpperRange = upperRange;
            LowerRange = lowerRange;
            NumbersOfHitsToWaitFor = numberOfHitsToWaitFor;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return base.ToString() + $" RangeObserver[LowerRange = '{LowerRange}', UpperRange = '{UpperRange}', NumbersInRange = '{NumbersInRange}', NumberHitsToWaitFor = '{NumbersOfHitsToWaitFor}']";
        }

        public override void OnNextNumber(object sender, int number)
        {
            base.OnNextNumber(sender, number);

            if (number >= LowerRange && number <= UpperRange)
            {            
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"   >> {this.GetType().Name}: Number is in range ('{LowerRange}' - '{UpperRange}')!");
                Console.ResetColor();

                NumbersInRange++;
            }

            if(NumbersInRange >= NumbersOfHitsToWaitFor)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"   >> {this.GetType().Name}: Got '{NumbersInRange}' numbers in the configured range => I am not interested in new numbers anymore!");
                Console.ResetColor();
                base.DetachFromNumberGenerator();
            }
        }

        #endregion
    }
}
