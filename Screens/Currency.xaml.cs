using Enitities.Concrete;
using System;
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
using System.Xml;

namespace Screens
{
    /// <summary>
    /// Interaction logic for Currency.xaml
    /// </summary>
    public partial class Currency : UserControl
    {
        public ObservableCollection<Currencies> currencies;
        public Currency()
        {
            InitializeComponent();
            currencies = new ObservableCollection<Currencies>();
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
                currencies.Add(new Currencies
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
