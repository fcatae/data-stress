using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dstress
{
    public interface IDataDriver<T,TKey> 
    {
        void Init();
        TKey Create(T model);
        T Read(TKey id);
        bool Update(TKey id, T model);
        bool Delete(TKey id);
    }

    public interface IDataDriver : IDataDriver<object, string>
    {
    }

    public class InMemoryDataDriver : IDataDriver
    {
        Dictionary<string, object> _models = new Dictionary<string, object>();
        int _nextId = 0;

        public void Init()
        {
            _models.Clear();
            _nextId = 0;
        }

        public string Create(object model)
        {
            string id = GetNextId().ToString();

            _models.Add(id, model);

            return id;
        }

        public object Read(string id)
        {
            object model;

            _models.TryGetValue(id, out model);

            return model;
        }

        public bool Update(string id, object model)
        {
            if (!_models.ContainsKey(id))
                return false;

            _models[id] = model;

            return true;
        }

        public bool Delete(string id)
        {
            return _models.Remove(id);
        }

        int GetNextId()
        {
            return _nextId++;
        }
    }
}
