using DrawingDemo.Common;
using MaterialDesignColors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingDemo.Settings
{
    public class ApperanceSettingViewModel
    {
        public ApperanceHelper Apperance => ApperanceHelper.Singleton;

        public IEnumerable<Swatch> PrimarySwatches { get; }
        public IEnumerable<Swatch> AccentSwatches { get; }

        public ApperanceSettingViewModel()
        {
            PrimarySwatches = new SwatchesProvider().Swatches;
            AccentSwatches = new List<Swatch>();
            foreach (Swatch s in PrimarySwatches)
            {
                if (s.IsAccented)
                {
                    ((List<Swatch>)AccentSwatches).Add(s);
                }
            }
        }

    }
}
