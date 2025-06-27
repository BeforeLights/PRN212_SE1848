using System.Collections.Generic;

namespace MaiQuocAnhWPF.Data
{
    public interface IDataPersistence<T> where T : class
    {
        void SaveData(IEnumerable<T> data, string fileName);
        IEnumerable<T> LoadData(string fileName);
    }
}