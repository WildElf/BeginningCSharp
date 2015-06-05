using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DieClassExample
{
    /// <summary>
    /// A die.
    /// </summary>
    class Die
    {
        #region Fields

        // need the number of sides and what is on top
        int numSides;
        int topSide;
        Random rand = new Random();

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for a 6-sided die.
        /// </summary>
        public Die() : this(6)
        {
        }

        /// <summary>
        /// Constructor with specified number of sides.
        /// </summary>
        /// <param name="sides"></param>
        public Die(int numSides)
        {
            this.numSides = numSides;
            topSide = 1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of sides.
        /// </summary>
        public int NumSides
        {
            get {return numSides; }
        }

        /// <summary>
        /// Reads the result of the die roll.
        /// </summary>
        public int TopSide
        {
            get { return topSide; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Generate a new die result.
        /// </summary>
        public void Roll()
        {
            topSide = rand.Next(1, numSides + 1);
        }

        #endregion

    }
}
