using System;
using Logic.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Linq;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;

namespace Logic.DAL
{
    public class FileLoader
    {
        public static string folderPath { get; set; } //= "C:/Users/pc/Documents/GitHub/jonasmats/Projektuppgift/Logic/DAL/";
        public static int Id { get; set; }

        string userPath = "/User.json"; string lastbilPath = "/Lastbilar.json";
        string bussPath = "/Buss.json"; string bilPath = "/Bil.json"; string motorcykelPath = "/Motorcyklar.json";
        string mekPath = "/Mekaniker.json"; string ärendePath = "/Ärenden.json"; string idPath = "/ID";
        string bilkompPath = "/BilKomponenter.json"; string busskompPath = "/BussKomponenter.json";
        string lastkompPath = "/LastbilKomponenter.json";
        string mckompPath = "/McKomponenter.json";
        public string missingFile { get; set; } = "";
        public InterfaceLoadSave IUser { get; set; } = new UserSamling();
        public InterfaceLoadSave IMekaniker { get; set; } = new MekanikerSamling();
        public InterfaceLoadSave IBil { get; set; } = new BilSamling();
        public InterfaceLoadSave IFordon { get; set; } = new FordonSamling();
        public InterfaceLoadSave IBuss { get; set; } = new BussSamling();
        public InterfaceLoadSave ILastbil { get; set; } = new LastbilSamling();
        public InterfaceLoadSave IMotorcyklar { get; set; } = new MotorcykelSamling();
        public InterfaceLoadSave IÄrende { get; set; } = new ÄrendeSamling();
        public InterfaceLoadSave IID { get; set; } = new IDSamling();
        public InterfaceLoadSave IBilkomp { get; set; } = new BilkomponentSamling();
        public InterfaceLoadSave IBusskomp { get; set; } = new BusskomponentSamling();
        public InterfaceLoadSave ILastbkomp { get; set; } = new LastbilkomponentSamling();
        public InterfaceLoadSave IMckomp { get; set; } = new MckomponentSamling();
        public BilkomponentSamling kompBilSamling { get; set; } = new BilkomponentSamling();
        public BusskomponentSamling kompBussSamling { get; set; } = new BusskomponentSamling();
        public LastbilkomponentSamling kompLastBSamling { get; set; } = new LastbilkomponentSamling();
        public MckomponentSamling kompMcSamling { get; set; } = new MckomponentSamling();
        public MekanikerSamling mekSamling { get; set; } = new MekanikerSamling();
        public FordonSamling fordonSamling { get; set; } = new FordonSamling();
        public ÄrendeSamling ärendeSamling { get; set; } = new ÄrendeSamling();
        public MotorcykelSamling motorcykelSamling { get; set; } = new MotorcykelSamling();
        public BilSamling bilSamling { get; set; } = new BilSamling();
        public BussSamling bussSamling { get; set; } = new BussSamling();
        public UserSamling userSamling { get; set; } = new UserSamling();
        public LastbilSamling lastbilSamling { get; set; } = new LastbilSamling();
        public IDSamling idSamling { get; set; } = new IDSamling();
        
        public int SendID()
        {
            return Id;
        }
        public void FindFolderPath(string foundfolder)
        {
            folderPath = foundfolder;
        }
        public void SetID(int id)
        {
            Id = id;
        }
        public async Task SaveBilKomp()
        {
            IBilkomp = kompBilSamling;
            await IBilkomp.SaveAsync(folderPath + bilkompPath);
        }
        public async Task SaveBussKomp()
        {
            IBusskomp = kompBussSamling;
            await IBusskomp.SaveAsync(folderPath + busskompPath);
        }
        public async Task  SaveLastbilKomp()
        {
            ILastbkomp = kompLastBSamling;
            await ILastbkomp.SaveAsync(folderPath + lastkompPath);
        }
        public async Task SaveMcKomp()
        {
            IMckomp = kompMcSamling;
            await IMckomp.SaveAsync(folderPath + mckompPath);
        }
        public async Task SaveUser() 
        {
            IUser = userSamling;
            await IUser.SaveAsync(folderPath + userPath);
        }
        public async Task SaveMekaniker()
        {
            IMekaniker = mekSamling;
            await IMekaniker.SaveAsync(folderPath + mekPath);
        }
        public async Task SaveLastBilar()
        {
            ILastbil = lastbilSamling;
            await ILastbil.SaveAsync(folderPath + lastbilPath);
        }
        public async Task SaveBilar()
        {
            IBil = bilSamling;
            await IBil.SaveAsync(folderPath + bilPath);
        }
        public async Task SaveBussar()
        {
            IBuss = bussSamling;
            await IBuss.SaveAsync(folderPath + bussPath);
        }
        public async Task SaveMotorcyklar()
        {
            IMotorcyklar = motorcykelSamling;
            await IMotorcyklar.SaveAsync(folderPath + motorcykelPath);
        }
        public async Task SaveÄrenden()
        {
            IÄrende = ärendeSamling;
            await IÄrende.SaveAsync(folderPath + ärendePath);
        }
        public async Task SaveID()
        {
            IID = idSamling;
            await IID.SaveAsync(folderPath + idPath);
        }
        public async Task SaveAllFordon()
        {
            await SaveBilar();
            await SaveBussar();
            await SaveLastBilar();
            await SaveMotorcyklar();
        }
        public async Task SaveAll()
        {
            await SaveBilar();
            await SaveBussar();
            await SaveLastBilar();
            await SaveMotorcyklar();
            await SaveMekaniker();
            await SaveID();
            await SaveÄrenden();
            await SaveMcKomp();
            await SaveLastbilKomp();
            await SaveBussKomp();
            await SaveBilKomp();
        }
        public void FordonReload()
        {
            fordonSamling.fordon.Clear();
            fordonSamling.AddFordon<Bil>(bilSamling.Bilar);
            fordonSamling.AddFordon<Buss>(bussSamling.Bussar);
            fordonSamling.AddFordon<Lastbil>(lastbilSamling.lastbilar);
            fordonSamling.AddFordon<Motorcykel>(motorcykelSamling.motorcyklar);
        }
        public void LoadBilKomp()
        {
            kompBilSamling.komp = IBilkomp.Load<BilKomponenter>(folderPath + bilkompPath);
        }
        public void LoadLastbilKomp()
        {
            kompLastBSamling.komp = ILastbkomp.Load<LastbilKomponenter>(folderPath + lastkompPath);
        }
        public void LoadBussKomp()
        {
           kompBussSamling.komp = IBusskomp.Load<BussKomponenter>(folderPath + busskompPath);
        }
        public void LoadMcKomp()
        {
            kompMcSamling.komp = IMckomp.Load<McKomponenter>(folderPath + mckompPath);
        }
        public void LoadBilar()
        {
                bilSamling.Bilar = IBil.Load<Bil>(folderPath + bilPath);
        }
        public void LoadBussar()
        {
                bussSamling.Bussar = IBuss.Load<Buss>(folderPath + bussPath);
        }
        public void LoadLastbilar()
        {
                lastbilSamling.lastbilar = ILastbil.Load<Lastbil>(folderPath + lastbilPath);
        }
        public void LoadMotorcyklar()
        {
                motorcykelSamling.motorcyklar = IMotorcyklar.Load<Motorcykel>(folderPath + motorcykelPath);
        }
        public void LoadMekaniker()
        {
                mekSamling.mekaniker = IMekaniker.Load<Mekaniker>(folderPath + mekPath);
        }
        public void LoadMekanikerTvå()
        {
                mekSamling.mekaniker = IMekaniker.Load<Mekaniker>(folderPath + mekPath);
        }
        public void LoadUsers()
        {
                userSamling.users = IUser.Load<User>(folderPath + userPath);
        }
        public void LoadÄrenden()
        {
                ärendeSamling.ärenden = IÄrende.Load<Ärende>(folderPath + ärendePath);
        }
        public void LoadID()
        {
            idSamling.idlista = IID.Load<ID>(folderPath + idPath);
        }

        public void LoadFiles()
        {
            LoadBilar();
            LoadBussar();
            LoadLastbilar();
            LoadMotorcyklar();
            LoadÄrenden();
            LoadUsers();
            LoadMekaniker();
        }
        public void LoadFordon()
        {
            try { LoadBilar(); }
            catch { }
            try { LoadLastbilar(); }
            catch { }
            try { LoadBussar(); }
            catch { }
            try { LoadMotorcyklar(); }
            catch { }

        }
        
    }
}