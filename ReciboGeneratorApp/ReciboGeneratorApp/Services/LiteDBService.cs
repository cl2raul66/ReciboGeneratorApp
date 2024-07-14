using LiteDB;
using ReceiptGeneratorApp.Models;

namespace ReciboGeneratorApp.Services;

public interface ILiteDBService
{
    bool Delete(ObjectId id);
    IEnumerable<Receipt> GetAll();
    IEnumerable<Receipt> GetAllByPages(int currentPage, int itemsByPage);
    IEnumerable<Receipt> GetByClientName(string clientName);
    IEnumerable<Receipt> GetByDateRange(DateTime startDate, DateTime endDate);
    Receipt? GetById(ObjectId id);
    void Insert(Receipt receipt);
    bool Update(Receipt receipt);
}

public class LiteDBService : ILiteDBService
{
    readonly LiteDatabase db;
    readonly ILiteCollection<Receipt> collection;

    public LiteDBService()
    {
        db = new LiteDatabase(Path.Combine(FileSystem.AppDataDirectory, "recibos.db"));
        collection = db.GetCollection<Receipt>("Receipt");
    }

    // Create: Insert a new receipt
    public void Insert(Receipt receipt) => collection.Insert(receipt);

    // Read: Get a receipt by Id
    public Receipt? GetById(ObjectId id) => collection.FindById(id);

    // Read: Get all receipts
    public IEnumerable<Receipt> GetAll() => collection.FindAll();

    // Update: Update an existing receipt
    public bool Update(Receipt receipt) => collection.Update(receipt);

    // Delete: Delete a receipt by Id
    public bool Delete(ObjectId id) => collection.Delete(id);

    // Additional: Get receipts by Client Name
    public IEnumerable<Receipt> GetByClientName(string clientName) => collection.Find(x => x.ClientDetails != null && x.ClientDetails.Name == clientName);

    // Additional: Get receipts within a date range
    public IEnumerable<Receipt> GetByDateRange(DateTime startDate, DateTime endDate) => collection.Find(
        x => x.ReceiptDetails != null
        && x.ReceiptDetails.IssueDate.Date >= startDate.Date
        && x.ReceiptDetails.IssueDate.Date <= endDate.Date
    );

    public IEnumerable<Receipt> GetAllByPages(int currentPage, int itemsByPage) => collection.Find(Query.All("ReceiptInfo.IssueDate", Query.Descending), currentPage * itemsByPage, itemsByPage);
}

