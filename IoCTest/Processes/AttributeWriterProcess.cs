namespace IoCTest.Processes
{
    //TODO: AttributeInfo writer generic implementation (Uses TD old helpers...)
    public class AttributeWriterProcess
    {

    }
    
    /// <summary>
    /// Given an excel file, attributes, 
    /// </summary>
    /// <param name="m">Stupid blob of unknown garbage...</param>
    /// <returns></returns>
    //public virtual bool WriteToFile(object m)
    //{
    //    try
    //    {
    //        object[] array = m as object[];

    //        Database Db = array[0] as Database;
    //        string path = array[1] as string;
    //        ExcelInfo excel = array[2] as ExcelInfo;
    //        string rowKey = array[3] as string;

    //        Dictionary<string, List<AttributeInfo>> attributes;

    //        attributes = JSONStack.DeserializeFromString<Dictionary<string, List<AttributeInfo>>>(EntityHelper.GetAttributeConfig(excel.ProjectNumber));

    //        foreach (var attributeList in attributes)
    //        {
    //            Dictionary<string, string> dictionary = new Dictionary<string, string>();
    //            Dictionary<string, Dictionary<string, string>> finalDictionary = new Dictionary<string, Dictionary<string, string>>();

    //            foreach (var att in attributeList.Value)
    //            {
    //                var value = excel.ExcelRows[rowKey].Where(p => p.Name == att.Name).FirstOrDefault();

    //                if (value != null)
    //                {
    //                    if (!String.IsNullOrEmpty(value.Value))  //<-- Is it okay to skip if empty string?
    //                    {
    //                        if (!dictionary.Keys.Contains(att.BlockName.ToLower()))
    //                        {
    //TODO: Magical area to obliterate for more easily configurable writes.
    //                            dictionary.Add(att.BlockName.ToLower(), value.Value);
    //                        }
    //                    }
    //                }
    //            }

    //            finalDictionary.Add(attributeList.Key, dictionary);

    //            Db.ChangeSingle(finalDictionary);
    //        }

    //        return true;
    //    }
    //    catch (System.Exception e)
    //    {
    //        return false;
    //    }
    //}
}