using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DeepShiShared.Helpers
{
    public static class UtilityHelper
    {
        private static Random random = new Random();
        private static string alphaCaps = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string alphaLow = "abcdefghijklmnopqrstuvwxyz";
        private static string numerics = "1234567890";
        private static string special = "@#$-_=";
        private static string allChars = alphaCaps + alphaLow + numerics + special;


        public static string RandomString(int length)
        {
            String generatedPassword = "";

            int lowerpass, upperpass, numpass, specialchar;
            string posarray = "0123456789";
            if (length < posarray.Length)
                posarray = posarray.Substring(0, length);
            lowerpass = getRandomPosition(ref posarray);
            upperpass = getRandomPosition(ref posarray);
            numpass = getRandomPosition(ref posarray);
            specialchar = getRandomPosition(ref posarray);


            for (int i = 0; i < length; i++)
            {
                if (i == lowerpass)
                    generatedPassword += getRandomChar(alphaCaps);
                else if (i == upperpass)
                    generatedPassword += getRandomChar(alphaLow);
                else if (i == numpass)
                    generatedPassword += getRandomChar(numerics);
                else if (i == specialchar)
                    generatedPassword += getRandomChar(special);
                else
                    generatedPassword += getRandomChar(allChars);
            }
            return generatedPassword;
        }

        private static string getRandomChar(string fullString)
        {
            return fullString.ToCharArray()[(int)Math.Floor(random.NextDouble() * fullString.Length)].ToString();
        }

        private static int getRandomPosition(ref string posArray)
        {
            int pos;
            string randomChar = posArray.ToCharArray()[(int)Math.Floor(random.NextDouble() * posArray.Length)].ToString();
            pos = int.Parse(randomChar);
            posArray = posArray.Replace(randomChar, "");
            return pos;
        }

        public static T GetAttributeFrom<T>(this object instance, string propertyName) where T : Attribute
        {
            var attrType = typeof(T);
            var property = instance.GetType().GetProperty(propertyName);
            return (T)property.GetCustomAttributes(attrType, false).FirstOrDefault();
        }

        public static DataTable PropertyValueIntoDataTable<T>(T items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            dataTable.Columns.Add("PropName");
            dataTable.Columns.Add("Value");
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Props = Props.Where(x => !x.PropertyType.Name.ToLower().Contains("list") && !x.PropertyType.Name.ToLower().Contains("collection")).ToArray();
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                //dataTable.Columns.Add(prop.Name);
                if (!prop.PropertyType.Name.ToLower().Contains("list") && !prop.PropertyType.Name.ToLower().Contains("collection"))
                {
                    DataRow dr = dataTable.NewRow();
                    //Setting column names as Property names
                    dr["PropName"] = prop.Name;
                    dr["Value"] = items.GetType().GetProperty(prop.Name).GetValue(items, null);

                    dataTable.Rows.Add(dr);
                }
            }

            return dataTable;
        }



    }
}
