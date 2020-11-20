﻿using System;
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

        string userPath = "/User.json"; string fordonPath = "/Fordon.json"; string lastbilPath = "/Lastbilar.json";
        string bussPath = "/Buss.json"; string bilPath = "/Bil.json"; string motorcykelPath = "/Motorcyklar.json";
        string mekPath = "/Mekaniker.json"; string ärendePath = "/Ärenden.json"; string idPath = "/ID";
        string komponentPath = "/Komponenter.json";
        string bilkompPath = "/BilKomponenter.json"; string busskompPath = "/BussKomponenter.json"; string lastkompPath = "/LastbilKomponenter.json";
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
        public InterfaceLoadSave IKomponenter { get; set; } = new KomponentSamling();
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
        public KomponentSamling komponentSamling { get; set; } = new KomponentSamling();
        
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
        public void SaveBilKomp()
        {
            IBilkomp = kompBilSamling;
            IBilkomp.SaveAsync(folderPath + bilkompPath);
        }
        public void SaveBussKomp()
        {
            IBusskomp = kompBussSamling;
            IBusskomp.SaveAsync(folderPath + busskompPath);
        }
        public void SaveLastbilKomp()
        {
            ILastbkomp = kompLastBSamling;
            ILastbkomp.SaveAsync(folderPath + lastkompPath);
        }
        public void SaveMcKomp()
        {
            IMckomp = kompMcSamling;
            IMckomp.SaveAsync(folderPath + bilkompPath);
        }
        public void SaveUser() 
        {
            IUser = userSamling;
            IUser.SaveAsync(folderPath + userPath);
        }
        public void SaveMekaniker()
        {
            IMekaniker = mekSamling;
            IMekaniker.SaveAsync(folderPath + mekPath);
        }
        public void SaveLastBilar()
        {
            ILastbil = lastbilSamling;
            ILastbil.SaveAsync(folderPath + lastbilPath);
        }
        public void SaveBilar()
        {
            IBil = bilSamling;
            IBil.SaveAsync(folderPath + bilPath);
        }
        public void SaveBussar()
        {
            IBuss = bussSamling;
            IBuss.SaveAsync(folderPath + bussPath);
        }
        public void SaveMotorcyklar()
        {
            IMotorcyklar = motorcykelSamling;
            IMotorcyklar.SaveAsync(folderPath + motorcykelPath);
        }
        public async  void SaveÄrenden()
        {
            IÄrende = ärendeSamling;
            IÄrende.SaveAsync(folderPath + ärendePath);
        }
        public void SaveID()
        {
            IID = idSamling;
            IID.SaveAsync(folderPath + idPath);
        }
        public void SaveKomponenter()
        {
            IKomponenter = komponentSamling;
            IKomponenter.SaveAsync(folderPath + komponentPath);
        }
        public void SaveAllFordon()
        {
            SaveBilar();
            SaveBussar();
            SaveLastBilar();
            SaveMotorcyklar();
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
            kompMcSamling.komp = IBilkomp.Load<McKomponenter>(folderPath + mckompPath);
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
        public void LoadKomponenter()
        {
            komponentSamling.komponentlista = IKomponenter.Load<Komponenter>(folderPath + komponentPath);
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