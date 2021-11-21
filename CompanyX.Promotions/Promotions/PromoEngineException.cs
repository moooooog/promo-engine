using System;

namespace CompanyX.Promotions
{
    public class PromoEngineException : Exception
    {
        public PromoEngineException()
        {
        }

        public PromoEngineException(string message)
            : base(message)
        {
        }

        public PromoEngineException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}