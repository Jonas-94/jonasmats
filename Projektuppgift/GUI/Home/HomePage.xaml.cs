﻿using System;
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

namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();


            
        }

        Mekaniker mekaniker;

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            mekaniker = new Mekaniker();
            mekaniker.Namn = txtBoxName.Text;
            mekaniker.Födelsedatum = txtBoxBirthday.Text;
            mekaniker.Anställninsdatum = txtBoxEmploymentDate.Text;
            mekaniker.Slutdatum = txtBoxUnEmploymentDate.Text;
            
            

        }
    }
}
