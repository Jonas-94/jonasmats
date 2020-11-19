using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using Logic.Entities;
using System.Threading.Tasks;
namespace Logic.DAL
{
    public interface InterfaceLoadSave
    {
        
        public void Save(string filePath) { }

        public async Task SaveAsync(string filePath){ }
        public List<T> Load<T>(string filePath) { List<T> list = new List<T>(); return list; }
    }
}
