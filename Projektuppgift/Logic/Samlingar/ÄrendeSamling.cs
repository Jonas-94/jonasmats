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
                System.IO.File.WriteAllText(filePath, "");
                FileStream fs = File.OpenWrite(filePath);
                StreamWriter sw = new StreamWriter(fs);
            await sw.WriteAsync(json);
            sw.Close();
            
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
