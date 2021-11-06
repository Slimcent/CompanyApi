
using System.Collections.Generic;
using System.Dynamic;

namespace Contract
{
    public interface IDataShaper<T>
    {
        // Data Shaping
        IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString);
        ExpandoObject ShapeData(T entity, string fieldsString);
    }
}
