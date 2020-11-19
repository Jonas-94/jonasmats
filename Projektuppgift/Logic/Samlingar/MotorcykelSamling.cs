using System;
using System.Collections.Generic;
using System.Text;
using Logic.Entities;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
namespace Logic.DAL
{
    public class MotorcykelSamling : InterfaceLoadSave
    {
        public List<Motorcykel> motorcyklar { get; set; } = new List<Motorcykel>();
        string filePath = @"DAL\Motorcyklar.json";
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[");

            for (int i = 0; i < motorcyklar.Count; i++)
            {
                sb.Append(motorcyklar[i].ToString());
                sb.Append(",");
            }
            sb.Append("]");

            return sb.ToString();

        }
        async Task InterfaceLoadSave.SaveAsync(string filePath)
        {
            string json = JsonSerializer.Serialize(this);
            if (File.Exists(filePath))
            {
                System.IO.File.WriteAllText(filePath, "");
                FileStream fs = File.OpenWrite(filePath);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(json);
                sw.Close();
            }
            else if (!File.Exists(filePath))
            {
                FileStream ffs = File.OpenWrite(filePath);
                StreamWriter tw = new StreamWriter(ffs);
                await tw.WriteAsync(json);
                tw.Write(json);
                tw.Close();
            }
        }
        void InterfaceLoadSave.Save(string filePath)
        {
            System.IO.File.WriteAllText(filePath, "");
            string json = JsonSerializer.Serialize(this);
            FileStream fs = File.OpenWrite(filePath);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(json);
            sw.Close();
        }
        List<T> InterfaceLoadSave.Load<T>(string filePath)
        {
            FileStream fs = File.OpenRead(filePath);
            StreamReader sr = new StreamReader(fs);
            string json = sr.ReadToEnd();
            MotorcykelSamling mcSamling = JsonSerializer.Deserialize<MotorcykelSamling>(json);
            sr.Close();
            return mcSamling.motorcyklar as List<T>;
        }
    }
}

