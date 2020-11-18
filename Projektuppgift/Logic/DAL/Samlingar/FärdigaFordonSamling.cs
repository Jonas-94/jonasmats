using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using Logic.Entities;
namespace Logic.DAL
{
    public class FärdigaFordonSamling : InterfaceLoadSave
    {
        public List<Fordon> fordon { get; set; } = new List<Fordon>();
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[");

            for (int i = 0; i < fordon.Count; i++)
            {
                sb.Append(fordon[i].ToString());
                sb.Append(",");
            }
            sb.Append("]");

            return sb.ToString();

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
            FärdigaFordonSamling färdigafordon = JsonSerializer.Deserialize<FärdigaFordonSamling>(json);
            sr.Close();
            return färdigafordon.fordon as List<T>;
        }
    }
    
}
