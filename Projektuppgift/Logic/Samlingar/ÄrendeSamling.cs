using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using Logic.Entities;
using System.Threading.Tasks;
namespace Logic.DAL
{
    public class ÄrendeSamling : InterfaceLoadSave
    {
        public List<Ärende> ärenden { get; set; } = new List<Ärende>();
        string path = @"C:DAL\Ärenden.json";

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[");

            for (int i = 0; i < ärenden.Count; i++)
            {
                sb.Append(ärenden[i].ToString());
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
        
        List<T> InterfaceLoadSave.Load<T>(string filePath)
        {
            FileStream fs = File.OpenRead(filePath);
            StreamReader sr = new StreamReader(fs);
            string json = sr.ReadToEnd();
            ÄrendeSamling ärendeSamling = JsonSerializer.Deserialize<ÄrendeSamling>(json);
            sr.Close();
            return ärendeSamling.ärenden as List<T>;
        }
    }
}
