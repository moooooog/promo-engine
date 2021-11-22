using System;

namespace CompanyX.Promotions
{
    /// <summary>
    /// An exception thrown by the promotion engine as part of processing an order.
    /// </summary>
    public class PromoEngineException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the PromoEngineException class.
        /// </summary>
        public PromoEngineException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the PromoEngineException class with a specified error message.
        /// </summary>
        /// <param name="message">The localized error message string.</param>
        public PromoEngineException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PromoEngineException class with a specified error message and inner exception.
        /// </summary>
        /// <param name="message">The localized error message string.</param>
        /// <param name="inner">The inner exception.</param>
        public PromoEngineException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}