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
        
        public void WriteMechanicFile(string filePath, MekanikerSamling ms)
        {
            try
            {
                string json = JsonSerializer.Serialize(ms);
                FileStream fs = File.OpenWrite(filePath);
                StreamWriter sw = new StreamWriter(fs);
                
                sw.WriteLine(json);
                sw.Close();
            }
            catch
            {
                FileDialog fileDialog = new OpenFileDialog();
                fileDialog.ShowDialog();
                filePath = fileDialog.FileName;

                FileStream fs = File.OpenWrite(filePath);
                StreamWriter sw = new StreamWriter(fs);
                string json = JsonSerializer.Serialize(ms);
                
                sw.Write(json);
                sw.Close();
            }
        }


        public void LoadMechanicFile(string filePath, List<Mekaniker>mekList,MekanikerSamling ms, DataGrid mekDataGrid ,System.Windows.Controls.Button load)
        {
            
            try
            {
                FileStream fs = File.OpenRead(filePath);
                string json = JsonSerializer.Serialize(ms);
                StreamReader sr = new StreamReader(fs);
                json = sr.ReadToEnd();
                MekanikerSamling uppLast = JsonSerializer.Deserialize<MekanikerSamling>(json);
                

                ms.mekaniker = uppLast.mekaniker;
                mekList = ms.mekaniker;
                sr.Close();
                foreach (var rmek in mekList)
                    mekDataGrid.Items.Remove(rmek);
                foreach (var mek in mekList)
                    mekDataGrid.Items.Add(mek);
                load.Background = Brushes.Green;
            }
            catch
            {
                load.Background = Brushes.Red;
            }
        }
    }
}
