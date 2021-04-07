using AutoMapper;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
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
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System.Xml;
using System.Net;
using System.Collections.ObjectModel;
using Enitities.Concrete;
using System.ComponentModel;
using System.Linq;

namespace Screens
{
    /// <summary>
    /// Interaction logic for Borsa.xaml
    /// </summary>
    public partial class Borsa : UserControl
    {
        public ObservableCollection<Currency> currencies;

        public Borsa()
        {
            InitializeComponent();

            currencies = new ObservableCollection<Currency>();
            KurBilgileri();
            CurrencyList.ItemsSource = currencies;

        }

        public void KurBilgileri()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load("https://www.tcmb.gov.tr/kurlar/today.xml");
            var Tarih_Date_Nodes = xml.SelectSingleNode("//Tarih_Date");
            var CurrencyNodes = Tarih_Date_Nodes.SelectNodes("//Currency");
            int CurrencyLength = CurrencyNodes.Count;

            for (int i = 0; i < CurrencyLength; i++)
            {
                var cn = CurrencyNodes[i];
                currencies.Add(new Currency
                {
                    Unit = cn.ChildNodes[0].InnerXml,
                    CurrencyName = cn.ChildNodes[2].InnerXml,
                    ForexBuying = cn.ChildNodes[3].InnerXml,
                    ForexSelling = cn.ChildNodes[4].InnerXml,
                });
            }
        }
    }
}

