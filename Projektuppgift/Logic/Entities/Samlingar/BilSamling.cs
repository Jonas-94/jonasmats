﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;

namespace Logic.Entities
{
    public class BilSamling : InterfaceLoadSave
    {
        public List<Bil> Bilar { get; set; } = new List<Bil>();
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[");

            for (int i = 0; i < Bilar.Count; i++)
            {
                sb.Append(Bilar[i].ToString());
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
            BilSamling bil = JsonSerializer.Deserialize<BilSamling>(json);
            sr.Close();
            return bil.Bilar as List<T>;
        }
    }
}
