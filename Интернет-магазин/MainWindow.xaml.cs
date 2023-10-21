using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using Интернет_магазин.DataSet2TableAdapters;
using System.Text.RegularExpressions;

namespace Интернет_магазин
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataSet2 dataset;
        DoljnostTableAdapter doljnostiTA;
        SotrudnikiTableAdapter sotrudnikiTA;
        Buh_OtchTableAdapter buhotchTA;
        SkladTableAdapter skladTA;
        TovarTableAdapter tovarTA;
        Tovar_MarkTableAdapter markTA;
        Type_TovarTableAdapter typeTA;
        View_SotrudnikTableAdapter view_SotrudnikTA;
        View_Buh_OtchTableAdapter view_Buh_OtchTA;
        View_SkladTableAdapter view_SkladTA;
        View_TovarsTableAdapter view_TovarsTA;

        int temp1;

        public MainWindow()
        {
            InitializeComponent();
            dataset = new DataSet2();

            doljnostiTA = new DoljnostTableAdapter();
            sotrudnikiTA = new SotrudnikiTableAdapter();
            buhotchTA = new Buh_OtchTableAdapter();
            skladTA = new SkladTableAdapter();
            tovarTA = new TovarTableAdapter();
            markTA = new Tovar_MarkTableAdapter();
            typeTA = new Type_TovarTableAdapter();

            view_SotrudnikTA = new View_SotrudnikTableAdapter();
            view_Buh_OtchTA = new View_Buh_OtchTableAdapter();
            view_SkladTA = new View_SkladTableAdapter();
            view_TovarsTA = new View_TovarsTableAdapter();

            sotrudnikiTA.Fill(dataset.Sotrudniki);
            doljnostiTA.Fill(dataset.Doljnost);
            buhotchTA.Fill(dataset.Buh_Otch);
            skladTA.Fill(dataset.Sklad);
            tovarTA.Fill(dataset.Tovar);
            typeTA.Fill(dataset.Type_Tovar);
            markTA.Fill(dataset.Tovar_Mark);

            view_SotrudnikTA.Fill(dataset.View_Sotrudnik);
            view_Buh_OtchTA.Fill(dataset.View_Buh_Otch);
            view_SkladTA.Fill(dataset.View_Sklad);
            view_TovarsTA.Fill(dataset.View_Tovars);

            sotrCB.ItemsSource = dataset.Sotrudniki.DefaultView;
            sotrCB.DisplayMemberPath = "Surname_Sotrudniki";
            sotrCB.SelectedValuePath = "ID_Sotrudniki";
            sotrCB.SelectedIndex = 0;

            sotrCB1.ItemsSource = dataset.Sotrudniki.DefaultView;
            sotrCB1.DisplayMemberPath = "Surname_Sotrudniki";
            sotrCB1.SelectedValuePath = "ID_Sotrudniki";
            sotrCB1.SelectedIndex = 0;

            doljCB.ItemsSource = dataset.Doljnost.DefaultView;
            doljCB.DisplayMemberPath = "Name_Doljnost";
            doljCB.SelectedValuePath = "ID_Doljnost";
            doljCB.SelectedIndex = 0;

            Marka_CB.ItemsSource = dataset.Tovar_Mark.DefaultView;
            Marka_CB.DisplayMemberPath = "Name_Tovar_Mark";
            Marka_CB.SelectedValuePath = "ID_Tovar_mark";
            Marka_CB.SelectedIndex = 0;

            Type_CB.ItemsSource = dataset.Type_Tovar.DefaultView;
            Type_CB.DisplayMemberPath = "Type_name_Tovar";
            Type_CB.SelectedValuePath = "ID_Type_Tovar";
            Type_CB.SelectedIndex = 0;

            Sklad_CB.ItemsSource = dataset.Sklad.DefaultView;
            Sklad_CB.DisplayMemberPath = "Adress_Sklad";
            Sklad_CB.SelectedValuePath = "ID_Sklad";
            Sklad_CB.SelectedIndex = 0;

            data.CanUserAddRows = false;
            data.CanUserDeleteRows = false;
            data.IsReadOnly = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            data.Columns[0].Visibility = Visibility.Hidden;
            data.Columns[1].Visibility = Visibility.Hidden;
            data.Columns[2].Visibility = Visibility.Hidden;
        }
        public void UpdateData()
        {
            sotrudnikiTA.Fill(dataset.Sotrudniki);
            view_SotrudnikTA.Fill(dataset.View_Sotrudnik);
            doljnostiTA.Fill(dataset.Doljnost);
            view_Buh_OtchTA.Fill(dataset.View_Buh_Otch);
            view_SkladTA.Fill(dataset.View_Sklad);
            view_TovarsTA.Fill(dataset.View_Tovars);
            markTA.Fill(dataset.Tovar_Mark);
            typeTA.Fill(dataset.Type_Tovar);
            sotrCB.SelectedIndex = 0;
            sotrCB1.SelectedIndex = 0;
            doljCB.SelectedIndex = 0;
            Sklad_CB.SelectedIndex = 0;
            Marka_CB.SelectedIndex = 0;
            Type_CB.SelectedIndex = 0;
        }
        private void changeBTN_Click(object sender, RoutedEventArgs e)
        {
            switch (tabcontr.SelectedIndex)
            {
                case 0:
                    if (!Regex.IsMatch(famTB.Text, @"^[!-/:-~]") && !Regex.IsMatch(imTB.Text, @"^[!-/:-~]") && !Regex.IsMatch(stavkaTB.Text, @"^[[!-/:-~]") && data.SelectedItem != null)
                    {
                        try
                        {
                            sotrudnikiTA.UpdateSotrudniki(famTB.Text, imTB.Text, otchTB.Text, (int)data.SelectedValue, Convert.ToInt32(stavkaTB.Text), (int)data.SelectedValue, (int)doljCB.SelectedValue, temp1);
                        }
                        catch { }
                    }
                    break;
                case 1:
                    if (!Regex.IsMatch(doljTB.Text, @"^[!-/:-~]") && !Regex.IsMatch(DataVyplatyTB.Text, @"^[!-/:-~]") && DataVyplatyTB.Text.Contains(" ") && !Regex.IsMatch(NalogTB.Text, @"^[!-$&-/:-~а-яА-Я]") && NalogTB.Text.Contains("%") && !Regex.IsMatch(OkladTB.Text, @"^[!-/:-~а-яА-Я]") && data.SelectedItem != null)
                    {
                        try
                        {
                            doljnostiTA.UpdateDoljn(doljTB.Text, Convert.ToInt32(OkladTB.Text), DataVyplatyTB.Text, NalogTB.Text, (int)data.SelectedValue);
                        }
                        catch { }
                    }
                    break;
                case 2:
                    if (!Regex.IsMatch(naimenovanieTB.Text, @"^[!-/:-~]") && !Regex.IsMatch(dataTB.Text, @"^[!--:-~а-яА-Я]") && dataTB.Text.Contains(".") && !Regex.IsMatch(pribylTB.Text, @"^[!-/:-~а-яА-Я]") && data.SelectedItem != null)
                    {
                        try
                        {
                            buhotchTA.UpdateBuhOtch(naimenovanieTB.Text, dataTB.Text, Convert.ToInt32(pribylTB.Text), (int)data.SelectedValue, (int)data.SelectedValue, (int)sotrCB.SelectedValue, temp1);
                        }
                        catch { }
                    }
                    break;
                case 3:
                    if (!Regex.IsMatch(AddressTB.Text, @"^[!-/:-~]") && !Regex.IsMatch(NyacheekTB.Text, @"^[!-/:-~а-яА-Я]") && data.SelectedItem != null)
                    {
                        try
                        {
                            skladTA.UpdateSklad(AddressTB.Text, Convert.ToInt32(NyacheekTB.Text), (int)data.SelectedValue, (int)sotrCB1.SelectedValue, (int)data.SelectedValue, temp1);
                        }
                        catch { }
                    }
                        break;
                case 4:
                    if (!Regex.IsMatch(Name_TovarTB.Text, @"^[!-/:-~]") && !Regex.IsMatch(KolTB.Text, @"^[!-/:-~а-яА-Я]") && !Regex.IsMatch(NYachTB.Text, @"^[!-/:-~а-яА-Я]") && data.SelectedItem != null)
                    {
                        try
                        {
                            tovarTA.UpdateTovar(Name_TovarTB.Text, (int)Type_CB.SelectedValue, (int)Marka_CB.SelectedValue, (int)data.SelectedValue, Convert.ToInt32(KolTB.Text), Convert.ToInt32(NYachTB.Text), (int)data.SelectedValue, (int)Sklad_CB.SelectedValue, temp1);
                        }
                        catch { }
                    }
                    break;
                case 5:
                    if (!Regex.IsMatch(MarkaTB.Text, @"^[!-/:-~]") && data.SelectedItem != null)
                    {
                        markTA.UpdateMark(MarkaTB.Text, (int)data.SelectedValue);
                    }
                    break;
                case 6:
                    if (!Regex.IsMatch(KatTB.Text, @"^[!-/:-~]") && data.SelectedItem != null)
                    {
                        typeTA.UpdateType(KatTB.Text, (int)data.SelectedValue);
                    }
                    break;
            }
            UpdateData();
        }

        private void deleteBTN_Click(object sender, RoutedEventArgs e)
        {

            switch (tabcontr.SelectedIndex)
            {
                case 0:
                    if (data.SelectedItem != null)
                    {
                        sotrudnikiTA.DeleteSotrudniki((int)data.SelectedValue);
                    }
                        break;
                case 1:
                    if (data.SelectedItem != null)
                    {
                        doljnostiTA.DeleteDoljn((int)data.SelectedValue);
                    }
                    break;
                case 2:
                    if (data.SelectedItem != null)
                    {
                        buhotchTA.DeleteBuhOtch((int)data.SelectedValue);
                    }
                    break;
                case 3:
                    if (data.SelectedItem != null)
                    {
                        skladTA.DeleteSklad((int)data.SelectedValue);
                    }
                    break;
                case 4:
                    if (data.SelectedItem != null)
                    {
                        tovarTA.DeleteTovar((int)data.SelectedValue);
                    }
                    break;
                case 5:
                    if (data.SelectedItem != null)
                    {
                        markTA.DeleteMark((int)data.SelectedValue);
                    }
                    break;
                case 6:
                    if (data.SelectedItem != null)
                    {
                        typeTA.DeleteType((int)data.SelectedValue);
                    }
                    break;
            }
            UpdateData();
        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
        switch (tabcontr.SelectedIndex)
        {
            case 0:
                    if (!Regex.IsMatch(famTB.Text, @"^[!-/:-~]") && !Regex.IsMatch(imTB.Text, @"^[!-/:-~]"))
                    {
                        try
                        {
                            sotrudnikiTA.InsertSotrudniki(famTB.Text, imTB.Text, otchTB.Text, Convert.ToInt32(stavkaTB.Text), (int)doljCB.SelectedValue);
                        }
                        catch { }
                    }
                break;
            case 1:
                    if (!Regex.IsMatch(doljTB.Text, @"^[!-/:-~]") && !Regex.IsMatch(DataVyplatyTB.Text, @"^[!-/:-~]") && DataVyplatyTB.Text.Contains(" ") && !Regex.IsMatch(NalogTB.Text, @"^[!-$&-/:-~а-яА-Я]") && NalogTB.Text.Contains("%") && !Regex.IsMatch(OkladTB.Text, @"^[!-/:-~а-яА-Я]"))
                    {
                        try
                        {
                            doljnostiTA.Insert(doljTB.Text, Convert.ToInt32(OkladTB.Text), DataVyplatyTB.Text, NalogTB.Text);
                        }
                        catch { }
                    }
                    break;
            case 2:
                    if (!Regex.IsMatch(naimenovanieTB.Text, @"^[!-/:-~]") && !Regex.IsMatch(dataTB.Text, @"^[!--:-~а-яА-Я]") && dataTB.Text.Contains(".") && !Regex.IsMatch(pribylTB.Text, @"^[!-/:-~а-яА-Я]"))
                    {
                        try
                        {
                            buhotchTA.InsertBuhOtch(naimenovanieTB.Text, dataTB.Text, Convert.ToInt32(pribylTB.Text), (int)sotrCB.SelectedValue);
                        }
                        catch { }
                    }
                break;
                case 3:
                    if (!Regex.IsMatch(AddressTB.Text, @"^[!-/:-~]") && !Regex.IsMatch(NyacheekTB.Text, @"^[!-/:-~а-яА-Я]") )
                    {
                        try
                        {
                            skladTA.InsertSklad(AddressTB.Text, Convert.ToInt32(NyacheekTB.Text), (int)sotrCB1.SelectedValue);
                        }
                        catch { }
                    }
                    break;
                case 4:
                    if (!Regex.IsMatch(Name_TovarTB.Text, @"^[!-/:-~]") && !Regex.IsMatch(KolTB.Text, @"^[!-/:-~а-яА-Я]") && !Regex.IsMatch(NYachTB.Text, @"^[!-/:-~а-яА-Я]"))
                    {
                        try
                        {
                            tovarTA.InsertTovar(Name_TovarTB.Text, (int)Type_CB.SelectedValue, (int)Marka_CB.SelectedValue, Convert.ToInt32(KolTB.Text), Convert.ToInt32(NYachTB.Text), (int)Sklad_CB.SelectedValue);
                        }
                        catch { }
                    }
                    break;
                case 5:
                    if (!Regex.IsMatch(MarkaTB.Text, @"^[!-/:-~]"))
                    {
                        markTA.Insert(MarkaTB.Text);
                    }
                    break;
                case 6:
                    if (!Regex.IsMatch(KatTB.Text, @"^[!-/:-~]"))
                    {
                        typeTA.Insert(KatTB.Text);
                    }
                    break;
            }
            UpdateData();
    }

        private void data_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e
        {
            DataRowView datarowview = (DataRowView)data.SelectedItem;
            int a;
            if (datarowview != null)
            {
                switch (tabcontr.SelectedIndex)
                {
                    case 0:
                        famTB.Text = datarowview.Row.Field<String>("Фамилия");
                        imTB.Text = datarowview.Row.Field<String>("Имя");
                        otchTB.Text = datarowview.Row.Field<String>("Отчество");
                        doljCB.SelectedValue = datarowview.Row.Field<int>("ID_Doljnost");
                        temp1 = datarowview.Row.Field<int>("ID_Sotrudniki_Doljnost");
                        break;
                    case 1:
                        doljTB.Text = datarowview.Row.Field<String>("Name_Doljnost");
                        a = datarowview.Row.Field<int>("Oklad_Doljnost");
                        OkladTB.Text = a.ToString();
                        DataVyplatyTB.Text = datarowview.Row.Field<String>("Data_vyplaty_Doljnost");
                        NalogTB.Text = datarowview.Row.Field<String>("Nalog_Doljnost");
                        break;
                    case 2:
                        naimenovanieTB.Text = datarowview.Row.Field<String>("Наименование отчета");
                        dataTB.Text = datarowview.Row.Field<String>("Дата отчета");
                        a = datarowview.Row.Field<int>("Прибыль");
                        pribylTB.Text = a.ToString();
                        sotrCB.SelectedValue = datarowview.Row.Field<int>("ID_Sotrudniki");
                        temp1 = datarowview.Row.Field<int>("ID_Buh_Otch_Sotrudniki");
                        break;
                    case 3:
                        AddressTB.Text = datarowview.Row.Field<String>("Адрес склада");
                        a = datarowview.Row.Field<int>("Количество ячеек");
                        NyacheekTB.Text = a.ToString();
                        sotrCB1.SelectedValue = datarowview.Row.Field<int>("ID_Sotrudniki");
                        temp1 = datarowview.Row.Field<int>("ID_Sotrudniki_Sklad");
                        break;
                    case 4:
                        Name_TovarTB.Text = datarowview.Row.Field<String>("Полное название товара");
                        a = datarowview.Row.Field<int>("Количество товара");
                        KolTB.Text = a.ToString();
                        a = datarowview.Row.Field<int>("Номер ячейки на складе");
                        NYachTB.Text = a.ToString();
                        Marka_CB.SelectedValue = datarowview.Row.Field<int>("ID_Tovar_mark");
                        Type_CB.SelectedValue = datarowview.Row.Field<int>("ID_Type_Tovar");
                        Sklad_CB.SelectedValue = datarowview.Row.Field<int>("ID_Sklad");
                        temp1 = datarowview.Row.Field<int>("ID_Tovar_Sklad");
                        break;
                    case 5:
                        MarkaTB.Text = datarowview.Row.Field<String>("Name_Tovar_Mark");
                        break;
                    case 6:
                        KatTB.Text = datarowview.Row.Field<String>("Type_name_Tovar");
                        break;
                }
            }
        }

        private void tabcontr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            data.FontSize = 12;
            switch (tabcontr.SelectedIndex)
            {
                case 0:
                    try
                    {
                        data.ItemsSource = dataset.View_Sotrudnik.DefaultView;
                        data.SelectionMode = DataGridSelectionMode.Single;
                        data.SelectedValuePath = "ID_Sotrudniki";
                        data.Columns[0].Visibility = Visibility.Hidden;
                        data.Columns[1].Visibility = Visibility.Hidden;
                        data.Columns[2].Visibility = Visibility.Hidden;
                        imTB.Text = "Имя";
                        famTB.Text = "Фамилия";
                        stavkaTB.Text = "Ставка";
                        otchTB.Text = "Отчество";
                    }
                    catch { }
                    break;
                case 1:
                    data.ItemsSource = dataset.Doljnost.DefaultView;
                    data.SelectionMode = DataGridSelectionMode.Single;
                    data.SelectedValuePath = "ID_Doljnost";
                    data.Columns[0].Visibility = Visibility.Hidden;
                    data.Columns[1].Header = "Наименование должности";
                    data.Columns[2].Header = "Оклад";
                    data.Columns[3].Header = "Дата выплат";
                    data.Columns[4].Header = "Налог";
                    data.Columns[5].Visibility = Visibility.Hidden;
                    doljTB.Text = "Должность";
                    OkladTB.Text = "Оклад";
                    DataVyplatyTB.Text = "Дата выплат";
                    NalogTB.Text = "Налог";
                    break;
                case 2:
                    data.ItemsSource = dataset.View_Buh_Otch.DefaultView;
                    data.SelectionMode = DataGridSelectionMode.Single;
                    data.SelectedValuePath = "ID_Buh_Otch";
                    data.Columns[0].Visibility = Visibility.Hidden;
                    data.Columns[1].Visibility = Visibility.Hidden;
                    data.Columns[2].Visibility = Visibility.Hidden;
                    naimenovanieTB.Text = "Название отчета";
                    dataTB.Text = "Дата отчета";
                    pribylTB.Text = "Прибыль";
                    break;
                case 3:
                    data.ItemsSource = dataset.View_Sklad.DefaultView;
                    data.SelectionMode = DataGridSelectionMode.Single;
                    data.SelectedValuePath = "ID_Sklad";
                    data.Columns[0].Visibility = Visibility.Hidden;
                    data.Columns[1].Visibility = Visibility.Hidden;
                    data.Columns[2].Visibility = Visibility.Hidden;
                    AddressTB.Text = "Адрес";
                    NyacheekTB.Text = "Количество ячеек";
                    break;
                case 4:
                    data.ItemsSource = dataset.View_Tovars.DefaultView;
                    data.SelectionMode = DataGridSelectionMode.Single;
                    data.SelectedValuePath = "ID_Tovar";
                    data.Columns[0].Visibility = Visibility.Hidden;
                    data.Columns[1].Visibility = Visibility.Hidden;
                    data.Columns[2].Visibility = Visibility.Hidden;
                    data.Columns[3].Visibility = Visibility.Hidden;
                    data.Columns[4].Visibility = Visibility.Hidden;
                    Name_TovarTB.Text = "Название товара";
                    KolTB.Text = "Количество";
                    NYachTB.Text = "Номер ячейки";
                    break;
                case 5:
                    data.ItemsSource = dataset.Tovar_Mark.DefaultView;
                    data.SelectionMode = DataGridSelectionMode.Single;
                    data.SelectedValuePath = "ID_Tovar_mark";
                    data.Columns[0].Visibility = Visibility.Hidden;
                    data.Columns[1].Header = "Марки товаров";
                    data.Columns[2].Visibility = Visibility.Hidden;
                    data.FontSize = 30;
                    MarkaTB.Text = "Марка товара";
                    break;
                case 6:
                    data.ItemsSource = dataset.Type_Tovar.DefaultView;
                    data.SelectionMode = DataGridSelectionMode.Single;
                    data.SelectedValuePath = "ID_Type_Tovar";
                    data.Columns[0].Visibility = Visibility.Hidden;
                    data.Columns[1].Header = "Типы товаров";
                    data.Columns[2].Visibility = Visibility.Hidden;
                    data.FontSize = 30;
                    KatTB.Text = "Тип товара";
                    break;
            }
        }
    }
}