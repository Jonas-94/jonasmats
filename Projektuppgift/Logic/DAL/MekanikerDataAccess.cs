using Logic.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Logic.DAL
{
    
        public class MekanikerDataAccess
        {
            private const string path = @"DAL\User.json";

            /// <summary>
            /// https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to
            /// </summary>
            /// <returns></returns>
            public List<Mekaniker> GetMekaniker()
            {

                string jsonString = File.ReadAllText(path);
                List<Mekaniker> mekaniker = JsonSerializer.Deserialize<List<Mekaniker>>(jsonString);

                return mekaniker;
            }
             public static void WriteMekaniker(List<Mekaniker>meklista)
            {
            var json = JsonSerializer.Serialize(meklista);
            
            }


    }
    
}
