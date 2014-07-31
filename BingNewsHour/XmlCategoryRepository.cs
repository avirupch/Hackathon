using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BingNewsHour
{
    public class XmlCategoryRepository : ICategoryRepository
    {
        private static List<CategoryData> CategoryList = null;

        static XmlCategoryRepository()
        {
            XDocument loadedData = XDocument.Load("CategoryXML.xml");

            var data = from query in loadedData.Descendants("Category")
                       select new CategoryData
                       {
                           Name = (string)query.Element("Name"),
                           Image = (string)query.Element("Image"),
                           Feed = (string)query.Element("Feed"),
                           CategoryId = (int)query.Element("CategoryId"),
                       };
            CategoryList = data.ToList();
        }

        public IList<CategoryData> GetCategoryList()
        {
            return CategoryList;
        }

        public CategoryData GetCategoryById(int id)
        {
            return CategoryList.FirstOrDefault(c => c.CategoryId == id);
        }
    }
}
