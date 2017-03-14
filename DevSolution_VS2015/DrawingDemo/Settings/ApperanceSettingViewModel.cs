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

        public IEnumerable<Swatch> PrimarySwatches => Apperance.PrimarySwatches;
        public IEnumerable<Swatch> AccentSwatches => Apperance.AccentSwatches;

        public ApperanceSettingViewModel()
        {
        }

    }
}
