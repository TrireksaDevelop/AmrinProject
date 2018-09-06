using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MobileApp
{


    public delegate void delOnClick(int btn);

    public class StepProgressBarControl : StackLayout
    {
        public event delOnClick OnClick;
        Button _lastStepSelected;
        public static readonly BindableProperty StepsProperty = BindableProperty.Create(nameof(Steps), typeof(int), typeof(StepProgressBarControl), 0);
        public static readonly BindableProperty StepSelectedProperty = BindableProperty.Create(nameof(StepSelected), typeof(int), typeof(StepProgressBarControl), 0, defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty StepColorProperty = BindableProperty.Create(nameof(StepColor), typeof(Xamarin.Forms.Color), typeof(StepProgressBarControl), Color.FromHex("#AA8F66"), defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty PassColorProperty = BindableProperty.Create(nameof(PassColor), 
            typeof(Xamarin.Forms.Color), typeof(StepProgressBarControl), Color.FromHex("#BBDB9B"), defaultBindingMode: BindingMode.TwoWay);


        public Color PassColor
        {
            get { return (Color)GetValue(PassColorProperty); }
            set { SetValue(PassColorProperty, value); }
        }

        public Color StepColor
        {
            get { return (Color)GetValue(StepColorProperty); }
            set { SetValue(StepColorProperty, value); }
        }

        public int Steps
        {
            get { return (int)GetValue(StepsProperty); }
            set { SetValue(StepsProperty, value); }
        }

        public int StepSelected
        {
            get { return (int)GetValue(StepSelectedProperty); }
            set { SetValue(StepSelectedProperty, value); }
        }


        public StepProgressBarControl()
        {
            Orientation = StackOrientation.Horizontal;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            Padding = new Thickness(10, 0);
            Spacing = 0;
            
            AddStyles();

        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == StepsProperty.PropertyName)
            {
                for (int i = 0; i < Steps; i++)
                {
                    var button = new Button();
                    button.Text = $"{i + 1}";
                    button.ClassId = $"{i + 1}";

                    if (i < StepSelected)
                    {
                       button.Style = Resources["passStyle"] as Style;
                    }else
                    {
                        button.Style = Resources["unSelectedStyle"] as Style;
                    }

                    button.Clicked += Handle_Clicked;

                    this.Children.Add(button);

                    if (i < Steps - 1)
                    {
                        var separatorLine = new BoxView()
                        {
                            BackgroundColor = Color.Silver,
                            HeightRequest = 1,
                            WidthRequest = 5,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.FillAndExpand
                        };
                        this.Children.Add(separatorLine);
                    }
                }
            }
            else if (propertyName == StepSelectedProperty.PropertyName)
            {
                if(StepSelected>0)
                {
                    var children = this.Children.First(p => (!string.IsNullOrEmpty(p.ClassId) && Convert.ToInt32(p.ClassId) == StepSelected));
                    if (children != null) SelectElement(children as Button);
                }
                var index = 0;
                foreach (Button b in this.Children.OfType<Button>())
                {
                    index++;
                    if(index<StepSelected)
                        b.Style = Resources["passStyle"] as Style;
                }

            }
            else if (propertyName == StepColorProperty.PropertyName)
            {
                AddStyles();
            }
        }

        internal void Complete()
        {
            foreach (Button b in this.Children.OfType<Button>())
            {
                b.Style = Resources["passStyle"] as Style;
            }
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            var elementSelected = sender as Button;
          //  SelectElement(elementSelected);
            
       var data = Convert.ToInt32(elementSelected.Text);
            OnClick?.Invoke(data);
        }

        void SelectElement(Button elementSelected)
        {

            if (_lastStepSelected != null) _lastStepSelected.Style = Resources["unSelectedStyle"] as Style;

            elementSelected.Style = Resources["selectedStyle"] as Style;

            StepSelected = Convert.ToInt32(elementSelected.Text);
            _lastStepSelected = elementSelected;
           

        }

        void AddStyles()
        {
            var unselectedStyle = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = BackgroundColorProperty,   Value = Color.White},
                    new Setter { Property = Button.BorderColorProperty,   Value = StepColor },
                    new Setter { Property = Button.TextColorProperty,   Value = StepColor },
                    new Setter { Property = Button.BorderWidthProperty,   Value = 0.5 },
                    new Setter { Property = Button.BorderRadiusProperty,   Value = 20 },
                    new Setter { Property = HeightRequestProperty,   Value = 40 },
                    new Setter { Property = WidthRequestProperty,   Value = 40 }
            }
            };

            var selectedStyle = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = BackgroundColorProperty, Value = StepColor },
                    new Setter { Property = Button.TextColorProperty, Value = Color.White },
                    new Setter { Property = Button.BorderColorProperty, Value = StepColor },
                    new Setter { Property = Button.BorderWidthProperty,   Value = 0.5 },
                    new Setter { Property = Button.BorderRadiusProperty,   Value = 20 },
                    new Setter { Property = HeightRequestProperty,   Value = 40 },
                    new Setter { Property = WidthRequestProperty,   Value = 40 },
                    new Setter { Property = Button.FontAttributesProperty,   Value = FontAttributes.Bold }
            }
            };

            var passStyle = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = BackgroundColorProperty,   Value = Color.FromHex("#BBDB9B")},
                    new Setter { Property = Button.BorderColorProperty,   Value = StepColor },
                    new Setter { Property = Button.TextColorProperty,   Value = StepColor },
                    new Setter { Property = Button.BorderWidthProperty,   Value = 0.5 },
                    new Setter { Property = Button.BorderRadiusProperty,   Value = 20 },
                    new Setter { Property = HeightRequestProperty,   Value = 40 },
                    new Setter { Property = WidthRequestProperty,   Value = 40 }
            }
            };

            Resources = new ResourceDictionary();
            Resources.Add("unSelectedStyle", unselectedStyle);
            Resources.Add("selectedStyle", selectedStyle);
            Resources.Add("passStyle", passStyle);
        }
    }
}
