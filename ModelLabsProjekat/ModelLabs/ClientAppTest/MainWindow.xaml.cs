using FTN.Common;
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

namespace ClientAppTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TestGdaApp testGdaApp = new TestGdaApp();
        public MainWindow()
        {
            InitializeComponent();

            ComboBoxMethod.ItemsSource = new List<string> { "GetValues", "GetExtentValues", "GetRelatedVlaues" };
        }

        private void ComboBoxMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string value = (string)ComboBoxMethod.SelectedItem;
            MethodEnum.Methods method = MethodEnum.GetMethodEnum(value);

            switch (method)
            {
                case MethodEnum.Methods.GetValues:
                    LabelNum2.Content = "Choose GID:";
                    LabelNum3.Content = "Choose propertis:";

                    Dictionary<string, string> gidName = testGdaApp.GetGIDValues();

                    List<string> gidStrings = new List<string>();

                    foreach (var item in gidName)
                    {
                        gidStrings.Add(item.Key + "-" + item.Value);
                    }

                    ComboBox2.ItemsSource = gidStrings;
                    ComboBox3.ItemsSource = "";
                    LabelNum4.Content = "Selected propertis";

                    TextBoxProps.Text = "";
                    break;

                case MethodEnum.Methods.GetExtentValues:
                    LabelNum2.Content = "Choose entity type:";
                    LabelNum3.Content = "Choose propertis:";

                    List<string> concreteClasses = new List<string>
                    {
                        "RTP",
                        "DAYTYPE",
                        "SEASON",
                        "BREAKER",
                        "SWITCHSCHEDULE",
                        "REGCONTROL",
                        "REGSCHEDULE"
                    };

                    List<string> modelCodeStrings = concreteClasses;

                    ComboBox2.ItemsSource = modelCodeStrings;
                    ComboBox3.ItemsSource = "";

                    TextBoxProps.Text = "";
                    break;
                case MethodEnum.Methods.GetRelatedVlaues:
                    LabelNum2.Content = "";
                    LabelNum3.Content = "";

                    TextBoxProps.Text = "";
                    break;
                case MethodEnum.Methods.Unknown:
                    break;
            }
        }

        private void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> listProp;

            string valueMethod = (string)ComboBoxMethod.SelectedItem;
            MethodEnum.Methods method = MethodEnum.GetMethodEnum(valueMethod);

            switch (method)
            {
                case MethodEnum.Methods.GetValues:
                    string gidValue = (string)ComboBox2.SelectedItem;

                    if (gidValue == null)
                        return;

                    string gid = gidValue.Split('-')[0];
                    string name = gidValue.Split('-')[1];

                    listProp = new List<string>();

                    ResourceDescription rd = testGdaApp.GetValues(long.Parse(gid));

                    foreach (var item in rd.Properties)
                    {
                        listProp.Add(item.Id.ToString());
                    }

                    ComboBox3.ItemsSource = listProp;

                    TextBoxProps.Text = "";

                    break;

                case MethodEnum.Methods.GetExtentValues:
                    string modelCodeType = (string)ComboBox2.SelectedItem;
                    ModelCode model;
                    ModelCode.TryParse(modelCodeType, out model);

                    listProp = testGdaApp.GetModelCodesForEntity(model);

                    ComboBox3.ItemsSource = listProp;
                    TextBoxProps.Text = "";

                    break;
                case MethodEnum.Methods.GetRelatedVlaues:
                    break;
                case MethodEnum.Methods.Unknown:
                    break;
            }
        }

        private void ComboBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string propValue = "";

            string valueMethod = (string)ComboBoxMethod.SelectedItem;
            MethodEnum.Methods method = MethodEnum.GetMethodEnum(valueMethod);

            bool exist = false;

            switch (method)
            {
                case MethodEnum.Methods.GetValues:
                    propValue = (string)ComboBox3.SelectedItem;

                    if(TextBoxProps.Text == "")
                    {
                        TextBoxProps.Text = propValue;
                    }
                    else
                    {
                        foreach (var item in TextBoxProps.Text.Split('\n'))
                        {
                            if (item == propValue)
                            {
                                exist = true;
                                break;
                            }
                        }
                        if (!exist)
                        {
                            string str = TextBoxProps.Text;
                            str += "\n" + propValue;
                            TextBoxProps.Text = str;
                        }
                    }

                    break;

                case MethodEnum.Methods.GetExtentValues:
                    propValue = (string)ComboBox3.SelectedItem;

                    if (TextBoxProps.Text == "")
                    {
                        TextBoxProps.Text = propValue;
                    }
                    else
                    {
                        foreach (var item in TextBoxProps.Text.Split('\n'))
                        {
                            if (item == propValue)
                            {
                                exist = true;
                                break;
                            }
                        }
                        if (!exist)
                        {
                            string str = TextBoxProps.Text;
                            str += "\n" + propValue;
                            TextBoxProps.Text = str;
                        }
                    }
                    break;

                case MethodEnum.Methods.GetRelatedVlaues:
                    break;
                case MethodEnum.Methods.Unknown:
                    break;
            }
        }

        private void Button_Click_Execute(object sender, RoutedEventArgs e)
        {
            string valueMethod = (string)ComboBoxMethod.SelectedItem;
            MethodEnum.Methods method = MethodEnum.GetMethodEnum(valueMethod);

            switch (method)
            {
                case MethodEnum.Methods.GetValues:
                    {
                        string gidValue = (string)ComboBox2.SelectedItem;

                        if (gidValue == null)
                            return;

                        string gid = gidValue.Split('-')[0];
                        string name = gidValue.Split('-')[1];

                        string richText = "-------------------------------------------------------------" + DateTime.Now + "-------------------------------------------------------------\n";
                        richText += "\tMethod: GetValues\n";
                        richText += "\tClass: " + name.Split('_')[0] + " " + name.Split('_')[1] + "\n";
                        richText += "\tGID: " + gid + "\n";

                        List<string> listProp = TextBoxProps.Text.Split('\n').ToList();

                        if (listProp.Count <= 1 && listProp[0] == "")
                            return;

                        ResourceDescription rd = testGdaApp.GetValues(long.Parse(gid));

                        foreach (var itemProp in listProp)
                        {
                            foreach (var itemRd in rd.Properties)
                            {
                                if (itemProp == itemRd.Id.ToString())
                                {
                                    richText += "\t\t" + itemProp + " : " + itemRd + "\n";
                                }
                            }
                        }

                        TextRange textRange = new TextRange(RichTextBoxValues.Document.ContentStart, RichTextBoxValues.Document.ContentEnd);
                        string text = textRange.Text;

                        richText += "--------------------------------------------------------------------------------------------------------------------------------------------------\n\n";

                        richText += text;

                        RichTextBoxValues.Document.Blocks.Clear();

                        RichTextBoxValues.Document.Blocks.Add(new Paragraph(new Run(richText)));

                        break;
                    }
                case MethodEnum.Methods.GetExtentValues:
                    {
                        List<ModelCode> models = new List<ModelCode>();
                        string modelCodeType = (string)ComboBox2.SelectedItem;

                        ModelCode model;
                        Enum.TryParse(modelCodeType, out model);

                        if (modelCodeType == null)
                            return;

                        string richText = "-------------------------------------------------------------" + DateTime.Now + "-------------------------------------------------------------\n";
                        richText += "\tMethod: GetExtentValues\n";
                        richText += "\tModelCode: " + modelCodeType + "\n";

                        List<string> listProp = TextBoxProps.Text.Split('\n').ToList();

                        if (listProp.Count <= 1 && listProp[0] == "")
                            return;

                        foreach (var item in listProp)
                        {
                            ModelCode modelProp;
                            Enum.TryParse(item, out modelProp);
                            models.Add(modelProp);
                        }

                        List<long> ids = testGdaApp.GetExtentValues(model, models);

                        foreach (var id in ids)
                        {
                            ResourceDescription rd = testGdaApp.GetValues(id, models);
                            richText += "\n";

                            foreach (var itemProp in listProp)
                            {
                                foreach (var prop in rd.Properties)
                                {
                                    if(itemProp == prop.Id.ToString())
                                    {
                                        richText += "\t\t" + itemProp + " : " + prop + "\n";
                                        break;
                                    }
                                }
                            }
                            richText += "\n";
                        }

                        TextRange textRange = new TextRange(RichTextBoxValues.Document.ContentStart, RichTextBoxValues.Document.ContentEnd);
                        string text = textRange.Text;

                        richText += "--------------------------------------------------------------------------------------------------------------------------------------------------\n\n";

                        richText += text;

                        RichTextBoxValues.Document.Blocks.Clear();

                        RichTextBoxValues.Document.Blocks.Add(new Paragraph(new Run(richText)));

                        break;
                    }
                case MethodEnum.Methods.GetRelatedVlaues:
                    break;
                case MethodEnum.Methods.Unknown:
                    break;
            }
        }

        private void Button_Click_Restart(object sender, RoutedEventArgs e)
        {
            TextBoxProps.Text = string.Empty;
        }
    }
}
