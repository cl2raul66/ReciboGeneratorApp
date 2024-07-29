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
    bool Insert(Receipt receipt);
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

    public bool Insert(Receipt receipt)
    {
        receipt.Id ??= ObjectId.NewObjectId();
        var result = collection.Insert(receipt);
        return result is not null;
    }

    public Receipt? GetById(ObjectId id) => collection.FindById(id);

    public IEnumerable<Receipt> GetAll() => collection.FindAll();

    public bool Update(Receipt receipt) => collection.Update(receipt);

    public bool Delete(ObjectId id) => collection.Delete(id);

    public IEnumerable<Receipt> GetByClientName(string clientName) => collection.Find(x => x.ClientDetails != null && x.ClientDetails.Name == clientName);

    public IEnumerable<Receipt> GetByDateRange(DateTime startDate, DateTime endDate) => collection.Find(
        x => x.ReceiptDetails != null
        && x.ReceiptDetails.IssueDate.Date >= startDate.Date
        && x.ReceiptDetails.IssueDate.Date <= endDate.Date
    );

    public IEnumerable<Receipt> GetAllByPages(int currentPage, int itemsByPage) => collection.Find(Query.All("ReceiptInfo.IssueDate", Query.Descending), currentPage * itemsByPage, itemsByPage);
}

