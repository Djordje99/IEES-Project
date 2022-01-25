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
            LabelNum6.Content = "Selected propertis";
        }

        private void ComboBoxMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string value = (string)ComboBoxMethod.SelectedItem;
            MethodEnum.Methods method = MethodEnum.GetMethodEnum(value);

            switch (method)
            {
                case MethodEnum.Methods.GetValues:
                    {
                        LabelNum2.Content = "Choose GID:";
                        LabelNum3.Content = "";
                        LabelNum4.Content = "";
                        LabelNum5.Content = "Choose propertis:";

                        ComboBox3.ItemsSource = "";
                        ComboBox4.ItemsSource = "";
                        ListBoxProp.ItemsSource = "";
                        TextBoxProps.Text = "";

                        Dictionary<string, string> gidName = testGdaApp.GetGIDValues();

                        List<string> gidStrings = new List<string>();

                        foreach (var item in gidName)
                        {
                            gidStrings.Add(item.Key + "-" + item.Value);
                        }

                        ComboBox2.ItemsSource = gidStrings;

                        break;
                    }
                case MethodEnum.Methods.GetExtentValues:
                    {
                        LabelNum2.Content = "Choose entity type:";
                        LabelNum3.Content = "";
                        LabelNum4.Content = "";
                        LabelNum5.Content = "Choose propertis:";

                        ComboBox3.ItemsSource = "";
                        ComboBox4.ItemsSource = "";
                        ListBoxProp.ItemsSource = "";
                        TextBoxProps.Text = "";

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

                        break;
                    }
                case MethodEnum.Methods.GetRelatedVlaues:
                    {
                        LabelNum2.Content = "Choose GID:";
                        LabelNum3.Content = "Choose PrpopertyId:";
                        LabelNum4.Content = "Choose Type:";
                        LabelNum5.Content = "Choose propertis:";

                        ComboBox3.ItemsSource = "";
                        ComboBox4.ItemsSource = "";
                        ListBoxProp.ItemsSource = "";
                        TextBoxProps.Text = "";

                        Dictionary<string, string> gidName = testGdaApp.GetGIDValues();

                        List<string> gidStrings = new List<string>();

                        foreach (var item in gidName)
                        {
                            gidStrings.Add(item.Key + "-" + item.Value);
                        }

                        ComboBox2.ItemsSource = gidStrings;

                        break;
                    }
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
                    {
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

                        ListBoxProp.ItemsSource = listProp;

                        TextBoxProps.Text = "";

                        break;
                    }
                case MethodEnum.Methods.GetExtentValues:
                    {
                        string modelCodeType = (string)ComboBox2.SelectedItem;
                        ModelCode model;
                        ModelCode.TryParse(modelCodeType, out model);

                        listProp = testGdaApp.GetModelCodesForEntity(model);

                        ListBoxProp.ItemsSource = listProp;
                        TextBoxProps.Text = "";

                        break;
                    }
                case MethodEnum.Methods.GetRelatedVlaues:
                    {
                        string gidValue = (string)ComboBox2.SelectedItem;

                        if (gidValue == null)
                            return;

                        string gid = gidValue.Split('-')[0];
                        string name = gidValue.Split('-')[1];

                        listProp = new List<string>();

                        List<string> refList = testGdaApp.GetReferencesForGID(long.Parse(gid));

                        foreach (var item in refList)
                        {
                            listProp.Add(item);
                        }

                        ComboBox3.ItemsSource = listProp;

                        TextBoxProps.Text = "";

                        break;
                    }
                case MethodEnum.Methods.Unknown:
                    break;
            }
        }

        private void ListBoxProp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string propValue = "";

            string valueMethod = (string)ComboBoxMethod.SelectedItem;
            MethodEnum.Methods method = MethodEnum.GetMethodEnum(valueMethod);

            bool exist = false;

            switch (method)
            {
                case MethodEnum.Methods.GetValues:
                    propValue = (string)ListBoxProp.SelectedItem;

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

                case MethodEnum.Methods.GetExtentValues:
                    propValue = (string)ListBoxProp.SelectedItem;

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
                    {
                        propValue = (string)ListBoxProp.SelectedItem;

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
                    }
                case MethodEnum.Methods.Unknown:
                    break;
            }
        }

        private void ComboBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string valueMethod = (string)ComboBoxMethod.SelectedItem;
            MethodEnum.Methods method = MethodEnum.GetMethodEnum(valueMethod);

            if (MethodEnum.Methods.GetRelatedVlaues.Equals(method))
            {
                List<string> concreteClasses = new List<string>
                        {
                        "NONE",
                        "RTP",
                        "DAYTYPE",
                        "SEASON",
                        "BREAKER",
                        "SWITCHSCHEDULE",
                        "REGCONTROL",
                        "REGSCHEDULE"
                        };

                List<string> modelCodeStrings = concreteClasses;

                ComboBox4.ItemsSource = modelCodeStrings;
            }
        }

        private void ComboBox4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string valueMethod = (string)ComboBoxMethod.SelectedItem;
            MethodEnum.Methods method = MethodEnum.GetMethodEnum(valueMethod);

            if (MethodEnum.Methods.GetRelatedVlaues.Equals(method))
            {
                List<string> modelCodes = new List<string>();

                string type = (string)ComboBox4.SelectedItem;

                if(type == "NONE")
                {
                    //modelCodes = testGdaApp.GetModelCodes();
                    
                    modelCodes.Add("IDOBJ_GID");
                    modelCodes.Add("IDOBJ_ALIASNAME");
                    modelCodes.Add("IDOBJ_MRID");
                    modelCodes.Add("IDOBJ_NAME");

                }
                else
                {
                    ModelCode model;
                    ModelCode.TryParse(type, out model);

                    modelCodes = testGdaApp.GetModelCodesForEntity(model);
                }

                ListBoxProp.ItemsSource = modelCodes;
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

                        string richText = "---------------------------------------------------------" + DateTime.Now + "-------------------------------------------------------\n";
                        richText += "\tMethod: GetValues\n";
                        richText += "\tClass: " + name.Split('_')[0] + " " + name.Split('_')[1] + "\n";
                        richText += "\tGID: " + gid + "\n";

                        List<string> listProp = TextBoxProps.Text.Split('\n').ToList();

                        if (listProp.Count <= 1 && listProp[0] == "")
                            return;

                        ResourceDescription rd = testGdaApp.GetValues(long.Parse(gid));

                        foreach (var itemProp in listProp)
                        {
                            foreach (var prop in rd.Properties)
                            {
                                if (itemProp == prop.Id.ToString())
                                {
                                    if (prop.Type == PropertyType.Reference)
                                    {
                                        richText += "\t\t" + itemProp + " : " + prop.AsReference() + "\n";
                                    }
                                    else if (prop.Type == PropertyType.ReferenceVector)
                                    {
                                        string str = "";

                                        foreach (var item in prop.AsReferences())
                                        {
                                            str += item + ", ";
                                        }

                                        richText += "\t\t" + itemProp + " : " + str + "\n";
                                    }
                                    else if (prop.Type == PropertyType.Enum)
                                    {
                                        if (prop.Id.ToString() == "REGCONTROL_MODE")
                                        {

                                            richText += "\t\t" + itemProp + " : " + (RegulatingControlModelKind)prop.AsEnum() + "\n";
                                        }
                                        else if (prop.Id.ToString() == "REGCONTROL_MONITOREDPHASE")
                                        {
                                            richText += "\t\t" + itemProp + " : " + (PhaseCode)prop.AsEnum() + "\n";
                                        }
                                        else if (prop.Id.ToString() == "BIS_VALUE1UNIT")
                                        {
                                            richText += "\t\t" + itemProp + " : " + (UnitSymbol)prop.AsEnum() + "\n";
                                        }
                                    }
                                    else
                                    {
                                        richText += "\t\t" + itemProp + " : " + prop + "\n";
                                    }
                                    break;
                                }
                            }
                        }

                        TextRange textRange = new TextRange(RichTextBoxValues.Document.ContentStart, RichTextBoxValues.Document.ContentEnd);
                        string text = textRange.Text;

                        richText += "------------------------------------------------------------------------------------------------------------------------------------------\n\n";

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

                        string richText = "---------------------------------------------------------" + DateTime.Now + "-------------------------------------------------------\n";
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
                                        if (prop.Type == PropertyType.Reference)
                                        {
                                            richText += "\t\t" + itemProp + " : " + prop.AsReference() + "\n";
                                        }
                                        else if (prop.Type == PropertyType.ReferenceVector)
                                        {
                                            string str = "";

                                            foreach (var item in prop.AsReferences())
                                            {
                                                str += item + ", ";
                                            }

                                            richText += "\t\t" + itemProp + " : " + str + "\n";
                                        }
                                        else if (prop.Type == PropertyType.Enum)
                                        {
                                            if (prop.Id.ToString() == "REGCONTROL_MODE")
                                            {

                                                richText += "\t\t" + itemProp + " : " + (RegulatingControlModelKind)prop.AsEnum() + "\n";
                                            }
                                            else if (prop.Id.ToString() == "REGCONTROL_MONITOREDPHASE")
                                            {
                                                richText += "\t\t" + itemProp + " : " + (PhaseCode)prop.AsEnum() + "\n";
                                            }
                                            else if (prop.Id.ToString() == "BIS_VALUE1UNIT")
                                            {
                                                richText += "\t\t" + itemProp + " : " + (UnitSymbol)prop.AsEnum() + "\n";
                                            }
                                        }
                                        else
                                        {
                                            richText += "\t\t" + itemProp + " : " + prop + "\n";
                                        }
                                        break;
                                    }
                                }
                            }
                            richText += "\n";
                        }

                        TextRange textRange = new TextRange(RichTextBoxValues.Document.ContentStart, RichTextBoxValues.Document.ContentEnd);
                        string text = textRange.Text;

                        richText += "------------------------------------------------------------------------------------------------------------------------------------------\n\n";

                        richText += text;

                        RichTextBoxValues.Document.Blocks.Clear();

                        RichTextBoxValues.Document.Blocks.Add(new Paragraph(new Run(richText)));

                        break;
                    }
                case MethodEnum.Methods.GetRelatedVlaues:
                    {
                        string gidValue = ((string)ComboBox2.SelectedItem).Split('-')[0];
                        long gid = long.Parse(gidValue);

                        string propertyID = (string)ComboBox3.SelectedItem;
                        ModelCode modelPropID;
                        Enum.TryParse(propertyID, out modelPropID);

                        string type = (string)ComboBox4.SelectedItem;
                        ModelCode modelType;
                        Enum.TryParse(type, out modelType);

                        List<ModelCode> models = new List<ModelCode>();
                        List<string> listProp = TextBoxProps.Text.Split('\n').ToList();
                        List<long> ids = new List<long>();

                        if (listProp.Count <= 1 && listProp[0] == "")
                            return;

                        foreach (var item in listProp)
                        {
                            ModelCode modelProp;
                            Enum.TryParse(item, out modelProp);
                            models.Add(modelProp);
                        }

                        Association association = new Association();
                        association.PropertyId = modelPropID;
                        association.Type = modelType;

                        ids = testGdaApp.GetRelatedValues(gid, association, models);


                        string richText = "-------------------------------------------------------" + DateTime.Now + "-------------------------------------------------------\n";
                        richText += "\tMethod: GetRelatedVlaues\n";
                        richText += "\tProperyID: " + propertyID + "\n";
                        richText += "\tType: " + type + "\n";

                        foreach (var item in listProp)
                        {
                            ModelCode modelProp;
                            Enum.TryParse(item, out modelProp);
                            models.Add(modelProp);
                        }

                        foreach (var id in ids)
                        {
                            ResourceDescription rd = testGdaApp.GetValues(id, models);
                            richText += "\n";

                            foreach (var itemProp in listProp)
                            {
                                foreach (var prop in rd.Properties)
                                {
                                    if (itemProp == prop.Id.ToString())
                                    {
                                        if (prop.Type == PropertyType.Reference)
                                        {
                                            richText += "\t\t" + itemProp + " : " + prop.AsReference() + "\n";
                                        }
                                        else if (prop.Type == PropertyType.ReferenceVector)
                                        {
                                            string str = "";

                                            foreach (var item in prop.AsReferences())
                                            {
                                                str += item + ", ";
                                            }

                                            richText += "\t\t" + itemProp + " : " + str + "\n";
                                        }
                                        else if (prop.Type == PropertyType.Enum)
                                        {
                                            if (prop.Id.ToString() == "REGCONTROL_MODE")
                                            {

                                                richText += "\t\t" + itemProp + " : " + (RegulatingControlModelKind)prop.AsEnum() + "\n";
                                            }
                                            else if (prop.Id.ToString() == "REGCONTROL_MONITOREDPHASE")
                                            {
                                                richText += "\t\t" + itemProp + " : " + (PhaseCode)prop.AsEnum() + "\n";
                                            }
                                            else if (prop.Id.ToString() == "BIS_VALUE1UNIT")
                                            {
                                                richText += "\t\t" + itemProp + " : " + (UnitSymbol)prop.AsEnum() + "\n";
                                            }
                                        }
                                        else
                                        {
                                            richText += "\t\t" + itemProp + " : " + prop + "\n";
                                        }
                                        break;
                                    }
                                }
                            }
                            richText += "\n";
                        }

                        TextRange textRange = new TextRange(RichTextBoxValues.Document.ContentStart, RichTextBoxValues.Document.ContentEnd);
                        string text = textRange.Text;

                        richText += "------------------------------------------------------------------------------------------------------------------------------------------\n\n";

                        richText += text;

                        RichTextBoxValues.Document.Blocks.Clear();

                        RichTextBoxValues.Document.Blocks.Add(new Paragraph(new Run(richText)));

                        break;
                    }

                case MethodEnum.Methods.Unknown:
                    break;
            }
        }

        private void Button_Click_Restart(object sender, RoutedEventArgs e)
        {
            TextBoxProps.Text = string.Empty;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> propList = (List<string>)ListBoxProp.ItemsSource;
            TextBoxProps.Text = "";

            foreach (var item in propList)
            {
                if(TextBoxProps.Text == "")
                {
                    TextBoxProps.Text = item;
                }
                else
                {
                    TextBoxProps.Text += "\n" + item;
                }
                
            }
        }
    }
}
