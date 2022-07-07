using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeploPro.Models.HomeViewModels
{
    public class ComparisonTemperatureViewModel
    {
        public ResultTemperatureViewModel BaseTemperaturePeriodData { get; set; }
        public ResultTemperatureViewModel СomparativeTemperaturePeriodData { get; set; }
    }
}
