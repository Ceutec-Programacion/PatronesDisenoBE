namespace PatronesDisenoEstructurales
{
    public class AdapterPattern
    {

        public interface IPaymentGateway
        {
            Task<string> PayAsync(int amountCents, string currency);
        }

        public sealed class LegacyPayments
        {
            public async Task<(string Id, string Status)> MakePaymentAsync(decimal total, string currency)
                => await Task.FromResult((Guid.NewGuid().ToString(), "OK"));
        }

        public sealed class LegacyPaymentsAdapter : IPaymentGateway
        {
            private readonly LegacyPayments _payments;
            public async Task<string> PayAsync(int amountCents, string currency)
            {
                var total = amountCents / 100m;
                var (id, status) = await _payments.MakePaymentAsync(total, currency);
                if (status != "OK") throw new InvalidOperationException("payment failed");
                return id;
            }
        }
    }
}
