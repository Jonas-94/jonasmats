using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
namespace Logic.Entities
{
    public interface InterfaceLoadSave
    {
        
        public void Save(string filePath) { }
        public List<T> Load<T>(string filePath) { List<T> list=new List<T>();  return list; }
    }
}
