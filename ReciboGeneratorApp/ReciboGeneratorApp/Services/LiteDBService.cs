using LiteDB;

namespace ReciboGeneratorApp.Services;

public class LiteDBService
{
    private const string DatabaseName = "recibos.db";
    private string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseName);

    public LiteDatabase GetDatabase()
    {
        return new LiteDatabase(DatabasePath);
    }

    public ILiteCollection<Recibo> GetReciboCollection()
    {
        using var db = GetDatabase();
        return db.GetCollection<Recibo>("recibos");
    }

    // Aquí añadiremos métodos para CRUD de recibos
}

