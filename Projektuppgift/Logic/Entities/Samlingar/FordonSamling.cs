using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;

namespace Logic.Entities
{
    public class FordonSamling : InterfaceLoadSave
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
        public void AddFordon<T>(List<T>list)
        {
            foreach (var ford in list)
                fordon.Add(ford as Fordon);
        }
    }
}
