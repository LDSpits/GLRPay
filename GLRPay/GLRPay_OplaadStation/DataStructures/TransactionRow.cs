using System;

namespace GLRPay_OplaadStation
{
    public class TransactionRow
    {
        public DateTime TransactionExecuted { get; private set; }
        public double AmountTransferred { get; private set; }

        public TransactionRow(DateTime TransactionDate, double Amount)
        {
            AmountTransferred = Amount;
            TransactionExecuted = TransactionDate;
        }
    }
}
