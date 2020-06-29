using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CardView_Background
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var childCount = cardLayout.Children.Count;
            for (int i = 0; i < childCount; i++)
            {
                cardLayout.Children[i].BindingContext = viewModel.Data[i];
            }
        }
    }

    public class CustomDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BackgroundTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (Convert.ToDouble(((CardData)item).Mark) <= 40)
            {
                ((CardData)item).BackgroundColor = Color.Red;
            }
            else
            {
                ((CardData)item).BackgroundColor = Color.LightGreen;
            }

            return BackgroundTemplate;
        }
    }

    public class CardLayoutViewModel
    {
        public ObservableCollection<CardData> Data { get; set; }

        public CardLayoutViewModel()
        {
            Data = new ObservableCollection<CardData>();

            Data.Add(new CardData
            {
                Name = "Flina",
                Subject = "Chemistry",
                Mark = 74
            });

            Data.Add(new CardData
            {
                Name = "Alexander",
                Subject = "English",
                Mark = 90
            });

            Data.Add(new CardData
            {
                Name = "Jacklin",
                Subject = "Maths",
                Mark = 60
            });

            Data.Add(new CardData
            {
                Name = "Joyce Rex",
                Subject = "Science",
                Mark = 35
            });           

            Data.Add(new CardData
            {
                Name = "Alexander",
                Subject = "Maths",
                Mark = 90
            });
        }
    }

    public class CardData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

       private Color backgroundColor;

        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                backgroundColor = value;
                OnPropertyChanged();
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private double mark;

        public double Mark
        {
            get { return mark; }
            set
            {
                mark = value;
                OnPropertyChanged();
            }
        }

        private string subject;

        public string Subject
        {
            get { return subject; }
            set
            {
                subject = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ImagePathConverter : IValueConverter
    {
        public static string GetImageSource(string resourceName)
        {
            var coll = resourceName.Split('.');
            if (coll.Length == 0)
            {
                throw new ArgumentException("The provided resource name is invalid. Example, SampleBrowser.SfChart.Button.png");
            }

            var assemblyName = coll[0] + "." + coll[1] + ".UWP";
            var assetName = coll[2] + "." + coll[3];

            if (Device.RuntimePlatform != Device.UWP)
            {
                return assetName;
            }

            return assemblyName + "\\" + assetName;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                return GetImageSource(parameter.ToString());
            }

            return GetImageSource(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
