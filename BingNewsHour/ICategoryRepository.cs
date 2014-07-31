using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingNewsHour
{
    public interface ICategoryRepository
    {
        IList<CategoryData> GetCategoryList();
        CategoryData GetCategoryById(int id);
    }
}
