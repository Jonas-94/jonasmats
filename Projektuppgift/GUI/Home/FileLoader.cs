using System;
using Logic.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using System.Text.Json;
using System.IO;
using System.Windows.Forms;

namespace GUI.Home
{
    public class FileLoader
    {
        //Test att spara lista till Json, ofärdig
        public void WriteFile<T>(string filePath, List<T> writeList)
        {
            
            try
            {
                System.IO.File.WriteAllText(filePath, "");
                string json = JsonSerializer.Serialize(writeList);
                FileStream fs = File.OpenWrite(filePath);
                StreamWriter sw = new StreamWriter(fs);
                
                sw.Write(json);
                sw.Close();
            }
            catch
            {
                FileDialog fileDialog = new OpenFileDialog();
                fileDialog.ShowDialog();
                filePath = fileDialog.FileName;

                FileStream fs = File.OpenWrite(filePath);
                StreamWriter sw = new StreamWriter(fs);
                string json = JsonSerializer.Serialize(writeList);
                
                sw.Write(json);
                sw.Close();
            }
        }

        //Test att ladda lista från json, ofärdig 
        public void LoadFile<T>(string filePath, List<T>list)
        {
            
            try
            {

                FileStream fs = File.OpenRead(filePath);
                StreamReader sr = new StreamReader(fs);
                string json = sr.ReadToEnd();
                List<T>upLoad = JsonSerializer.Deserialize<List<T>>(json);
                
                list = upLoad;
                sr.Close();
            }
            catch
            {
            }
        }
        public void SaveUserJson(string filePath, UserSamling us)
        {
            System.IO.File.WriteAllText(filePath, "");
            string json = JsonSerializer.Serialize(us);
            FileStream fs = File.OpenWrite(filePath);
            StreamWriter sw = new StreamWriter(fs);

            sw.Write(json);
            sw.Close();
        }
        //Sparar Mekaniker i Json
        public void SaveMechs(string filePath, MekanikerSamling meks)
        {
            System.IO.File.WriteAllText(filePath, "");
            string json = JsonSerializer.Serialize(meks);
            FileStream fs = File.OpenWrite(filePath);
            StreamWriter sw = new StreamWriter(fs);

            sw.Write(json);
            sw.Close();
        }
        //Hämtar mekaniker från Json
        public void GetMechs(string filePath, MekanikerSamling meks)
        {
            try
            {
                FileStream fs = File.OpenRead(filePath);
                StreamReader sr = new StreamReader(fs);
                string json = sr.ReadToEnd();
                MekanikerSamling ms = JsonSerializer.Deserialize<MekanikerSamling>(json);
                foreach (var mek in ms.mekaniker)
                    meks.mekaniker.Add(mek);

                sr.Close();
            }
            catch
            {
                FileDialog fileDialog = new OpenFileDialog();
                fileDialog.ShowDialog();
                filePath = fileDialog.FileName;
            }
        }
        //Experiment
        public MekanikerSamling GetMechsfromlist(string filePath, List<Mekaniker> list  )
        {
            MekanikerSamling ms = new MekanikerSamling();
            
            LoadFile(filePath, list);
            ms.mekaniker = list;
            return ms;
        }
        //Sparar fordon i Json
        public void WriteFordonFile<T>(string filePath, List<T> fordonLista)
        {
            try
            {
                string json = JsonSerializer.Serialize(fordonLista);
                FileStream fs = File.OpenWrite(filePath);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(json);
                sw.Close();
            }
            catch
            {
                FileDialog fileDialog = new OpenFileDialog();
                fileDialog.ShowDialog();
                filePath = fileDialog.FileName;

                FileStream fs = File.OpenWrite(filePath);
                StreamWriter sw = new StreamWriter(fs);
                string json = JsonSerializer.Serialize(fordonLista);

                sw.Write(json);
                sw.Close();
            }

        }
        public string AppendToString<T>(List<T> list)
        {
            StringBuilder sb = new StringBuilder("[");
            for (int i = 0; i < list.Count; i++)
            {
                sb.Append(list[i].ToString());
                sb.Append(",");
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}
