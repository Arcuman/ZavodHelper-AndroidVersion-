using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
namespace Info
{
    public class InstrumentAsyncRepository
    {

        SQLiteConnection database;
        public InstrumentAsyncRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
        }
        public IEnumerable<Instruments> GetItems()
        {
            return database.Table<Instruments>().ToList();
        }
        public Instruments GetItem(int id)
        {
            return database.Get<Instruments>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<Instruments>(id);
        }
        public int SaveItem(Instruments item)
        {
            if (item.IdInstrument != 0)
            {
                database.Update(item);
                return item.IdInstrument;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}
