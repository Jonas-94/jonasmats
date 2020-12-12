using System;
using System.Collections.Generic;
using System.Text;
using Logic.Entities;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
namespace Logic.DAL
{
    public class LastbilSamling : InterfaceLoadSave
    {
        public List<Lastbil> lastbilar { get; set; } = new List<Lastbil>();
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[");

            for (int i = 0; i < lastbilar.Count; i++)
            {
                sb.Append(lastbilar[i].ToString());
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
                LastbilSamling lastbil = JsonSerializer.Deserialize<LastbilSamling>(json);
                sr.Close();
                return lastbil.lastbilar as List<T>;
            
        }
    }
}

