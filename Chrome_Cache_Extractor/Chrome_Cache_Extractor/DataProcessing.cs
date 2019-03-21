using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace Chrome_Cache_Extract
{
    class DataProcessing
    {
        string PATH = "C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Google\\Chrome\\User Data\\\\Cache";
        static string userPATH="";
        public struct partt
        {
            public string file;
            public string url;
            public string binary_data;
        }
        public string PATH1(string user) { return PATH.Insert(PATH.Length - 6, user); }
        public string UserPATH(string user) { return userPATH.Insert(userPATH.Length, "\\"+user); }
        public string SetUserPATH(string user) { userPATH = userPATH.Insert(userPATH.Length, user); return userPATH; }

        public string HexString2Ascii(string hexString)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= hexString.Length - 2; i += 2)
            {
                sb.Append(Convert.ToString(Convert.ToChar(int.Parse(hexString.Substring(i, 2), System.Globalization.NumberStyles.HexNumber))));
            }
            return sb.ToString().Replace("\0", String.Empty);
        }
        public string LittleEndian(string num)
        {
            int number = Convert.ToInt32(num, 16);
            byte[] bytes = BitConverter.GetBytes(number);
            string retval = "";
            foreach (byte b in bytes)
                retval += b.ToString("X2");
            return retval;
        }
        
        public List<partt> ExportURLs(List<string> addr, List<string> hexes,string user)
        {
            int[,] indexes = new int[addr.Count, 2];
            List<partt> parts = new List<partt>();
            partt part = new partt();


            //######################## Data_1 Kopyalama ve Okuma ################################
            /*||*/ Directory.CreateDirectory(UserPATH(user) + "\\Media Cache");
            /*||*/ File.Copy(PATH1(user) + "\\data_1", UserPATH(user) + "\\data_1", true);
            /*||*/ FileStream data_1 = new FileStream(UserPATH(user) + "\\data_1", FileMode.Open, FileAccess.Read);
            /*||*/ byte[] file = new byte[data_1.Length];
            /*||*/ data_1.Read(file, 0, (int)(data_1.Length));
            /*||*/ string fileString = BitConverter.ToString(file).Replace("-", String.Empty);
            //##################################################################################

            for (int index = 0; index < addr.Count; index++)
            {
                int beg = indexes[index, 0];
                int end = indexes[index, 1];
                beg = Regex.Match(fileString, addr[index]).Index + 84; //84=https baslangicina gecis
                end = Regex.Match(fileString.Remove(0, beg), "3A......00").Index;
                //################# Part nesnesine bilgileri ekleme ##########################################################
                /*||*/ part.file = hexes[index];
                /*||*/ part.url = HexString2Ascii(fileString.Substring(beg, end));
                /*||*/ string bin = fileString.Substring(beg + end + 2, 6);
                /*||*/ part.binary_data = HexString2Ascii(bin);
                //############################################################################################################

                if (!part.url.Contains("webm"))parts.Add(part);
            }

            data_1.Close();
            return parts;
        }
        public void XmlOlustur(List<partt> partList)
        {
            XmlTextWriter xml = new XmlTextWriter(userPATH + "\\parts.xml", UTF8Encoding.UTF8);
            for (int i = 0; i < partList.Count; i++)
            {
                xml.WriteStartElement("parts");
                xml.WriteStartElement("part");
                xml.WriteAttributeString("ID",(i+1).ToString());
                //xml.WriteElementString()
            }
            
        }

    }
}
