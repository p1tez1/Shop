using Restoran.Entity;

namespace Restoran.Service.Interface
{
    public interface IReceiptService
    {
        public Receipt GetReceiptById(Guid receiptid);
        public IEnumerable<Receipt> GettAllReceipt();
        public Receipt PaidForOrder(Guid orderId);
        public Receipt PaidForOrderInExistingReceipt(Guid orderId, Guid receiptId);
        public bool DeleteReceipt(Guid receiptid);
    }
}
