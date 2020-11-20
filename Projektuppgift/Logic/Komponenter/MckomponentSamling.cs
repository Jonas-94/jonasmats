﻿using System;
using System.Collections.Generic;
using System.Text;
using Logic.Entities;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;
namespace Logic.DAL
{
    public class MckomponentSamling : InterfaceLoadSave
    {
        public List<McKomponenter> komp { get; set; } = new List<McKomponenter>();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[");

            for (int i = 0; i < komp.Count; i++)
            {
                sb.Append(komp[i].ToString());
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
            MckomponentSamling mckomp = JsonSerializer.Deserialize<MckomponentSamling>(json);
            sr.Close();
            return mckomp.komp as List<T>;
        }

    }
}
