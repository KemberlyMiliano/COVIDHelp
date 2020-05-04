using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Xamarin.Forms;

namespace COVIDHelp.Views.DailogsViews
{
        public class RatingSlider : Grid
        {
            public static readonly BindableProperty SelectedPositionProperty = BindableProperty.Create(nameof(SelectedPosition), typeof(int), typeof(RatingSlider), 0);

            public int SelectedPosition
            {
                get { return (int)GetValue(SelectedPositionProperty); }
                set { SetValue(SelectedPositionProperty, value); }
            }

            public static readonly BindableProperty ItemSpacingProperty = BindableProperty.Create(nameof(ItemSpacing), typeof(double), typeof(RatingSlider), (double)5.0f);

            public double ItemSpacing
            {
                get { return (double)GetValue(ItemSpacingProperty); }
                set { SetValue(ItemSpacingProperty, value); }
            }


            public static readonly BindableProperty NumberOfItemsProperty = BindableProperty.Create(nameof(NumberOfItems), typeof(int), typeof(RatingSlider), 5);

            public int NumberOfItems
            {
                get { return (int)GetValue(NumberOfItemsProperty); }
                set { SetValue(NumberOfItemsProperty, value); }
            }

            public static readonly BindableProperty ItemCornerRadiusProperty = BindableProperty.Create(nameof(ItemCornerRadius), typeof(double), typeof(RatingSlider), (double)0.0f);

            public double ItemCornerRadius
            {
                get { return (double)GetValue(ItemCornerRadiusProperty); }
                set { SetValue(ItemCornerRadiusProperty, value); }
            }

            public static readonly BindableProperty ItemHeightProperty = BindableProperty.Create(nameof(ItemHeight), typeof(double), typeof(RatingSlider), (double)50.0f);

            public double ItemHeight
            {
                get { return (double)GetValue(ItemHeightProperty); }
                set { SetValue(ItemHeightProperty, value); }
            }


            public static readonly BindableProperty ItemSelectedColorProperty = BindableProperty.Create(nameof(ItemSelectedColor), typeof(Color), typeof(RatingSlider), Color.Yellow);

            public Color ItemSelectedColor
            {
                get { return (Color)GetValue(ItemSelectedColorProperty); }
                set { SetValue(ItemSelectedColorProperty, value); }
            }


            public static readonly BindableProperty ItemUnselectedColorProperty = BindableProperty.Create(nameof(ItemUnselectedColor), typeof(Color), typeof(RatingSlider), Color.Transparent);

            public Color ItemUnselectedColor
            {
                get { return (Color)GetValue(ItemUnselectedColorProperty); }
                set { SetValue(ItemUnselectedColorProperty, value); }
            }

            public event EventHandler<SelectedPositionChangedEventArgs> OnSelectedPositionChanged = delegate { };

            IList<Image> image;
            public RatingSlider()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand;
                image = new List<Image>();

                RowDefinitions.Add(new RowDefinition() { Height = new GridLength(ItemHeight, GridUnitType.Absolute) });

                SetupItems();
                ColumnSpacing = ItemSpacing;

            }

            void SetupItems()
            {
                image.Clear();
                Children.Clear();
                ColumnDefinitions.Clear();

                for (int i = 0; i < NumberOfItems; i++)
                {
                    ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                    var box = new Image() { Source = "ic_star_border.png", Aspect = Aspect.AspectFill, BackgroundColor = ItemUnselectedColor };
                    image.Add(box);
                    Children.Add(box, i, 0);
                }

            }


            protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                base.OnPropertyChanged(propertyName);

                if (propertyName == ItemSpacingProperty.PropertyName)
                {
                    ColumnSpacing = ItemSpacing;
                }
                else if (propertyName == SelectedPositionProperty.PropertyName || propertyName == ItemSelectedColorProperty.PropertyName || propertyName == ItemUnselectedColorProperty.PropertyName)
                {
                    UpdatePositionColor();
                }
                else if (propertyName == NumberOfItemsProperty.PropertyName)
                {
                    SetupItems();
                }

            }

            public void UpdatePositionColor()
            {
                if (SelectedPosition >= 1 && image.Count >= SelectedPosition)
                {
                    for (int p = 0; p < image.Count; p++)
                    {
                        if ((p + 1) <= SelectedPosition)
                        {
                            image[p].Source = "ic_star_Yellow.png";
                        }
                        else
                        {
                            image[p].Source = "ic_star_border.png";
                        }
                    }

                }
                else
                {
                    for (int p = 0; p < image.Count; p++)
                    {
                        image[p].Source = "ic_star_border.png";
                    }
                }
            }


            public void SetSelectedPosition(int position)
            {
                OnSelectedPositionChanged(this, new SelectedPositionChangedEventArgs(position));

                SelectedPosition = position;


            }
        }
    }