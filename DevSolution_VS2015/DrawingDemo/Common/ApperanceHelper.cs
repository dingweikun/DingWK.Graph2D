using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingDemo.Common
{
    public class ApperanceHelper : ObservableObject
    {
        public static readonly ApperanceHelper Singleton = new ApperanceHelper();

        private PaletteHelper Palette => new PaletteHelper();

        public Swatch CurrentPrimary
        {
            get { return Palette.QueryPalette().PrimarySwatch; }
            set
            {
                Palette.ReplacePrimaryColor(value);
                OnPropertyChanged(nameof(CurrentPrimary));
            }
        }

        public Swatch CurrentAccent
        {
            get { return Palette.QueryPalette().AccentSwatch; }
            set
            {
                Palette.ReplaceAccentColor(value);
                OnPropertyChanged(nameof(CurrentAccent));
            }

        }

        private bool isDarkTheme;
        public bool IsDarkTheme
        {
            get { return isDarkTheme; }
            set
            {
                isDarkTheme = value;
                Palette.SetLightDark(isDarkTheme);
                OnPropertyChanged(nameof(IsDarkTheme));
            }
        }

        private ApperanceHelper()
        {
        }

    }
}
