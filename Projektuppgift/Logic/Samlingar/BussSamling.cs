using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using Logic.Entities;
using System.Threading.Tasks;
namespace Logic.DAL
{
    public class BussSamling : InterfaceLoadSave
    {
        public List<Buss> Bussar { get; set; } = new List<Buss>();
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[");

            for (int i = 0; i < Bussar.Count; i++)
            {
                sb.Append(Bussar[i].ToString());
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
                await sw.WriteAsync(json);
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
            BussSamling buss = JsonSerializer.Deserialize<BussSamling>(json);
            sr.Close();
            return buss.Bussar as List<T>;
        }
    }
}
