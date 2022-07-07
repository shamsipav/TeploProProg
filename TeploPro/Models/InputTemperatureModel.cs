using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeploPro.Models
{
    public class InputTemperatureModel
    {
        /// <summary>
        /// Первичный ключ для сохранения вариантов исходных данных
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Содержание кислорода в дутье, %
        /// </summary>
        public double OxygenContentInTheBlast { get; set; }

        /// <summary>
        /// Температура горячего дутья, °C
        /// </summary>
        public double HotBlastTemperature { get; set; }
        
        /// <summary>
        /// Содержание влаги в дутье, г/м3
        /// </summary>
        public double MoistureContentInTheBlast { get; set; }
        
        /// <summary>
        /// Количество углерода, сгорающего у фурм, кг/т чугуна
        /// </summary>
        public double AmountOfCarbonBurnedAtTheTuyeres { get; set; }
        
        /// <summary>
        /// Расход природного газа, м3/т чугуна
        /// </summary>
        public double NaturalGasConsumption { get; set; }
        
        /// <summary>
        /// Теплота горения природного газа на фурмах, кДж/м3
        /// </summary>
        public double HeatOfBurningOfNaturalGasOnFarms { get; set; }
        
        /// <summary>
        /// Теплота неполного горения углерода кокса, кДж/кг
        /// </summary>
        public double HeatOfIncompleteBurningCarbonOfCoke { get; set; }
        
        /// <summary>
        /// Теплоёмкость кокса, кДж/(кг * К)
        /// </summary>
        public double HeatCapacityOfCoke { get; set; }

        /// <summary>
        /// Температура кокса, пришедшего к фурмам, °C
        /// </summary>
        public double TemperatureOfCokeThatCameToTuyeres { get; set; }

        /// <summary>
        /// Дата проведения расчёта
        /// </summary>
        public DateTime DateOfResult { get; set; }

        // ПОЛУЧЕНИЕ ИСХОДНЫХ ЗНАЧЕНИЙ
        public static InputTemperatureModel GetDefaultData()
        {
            return new InputTemperatureModel
            {
                OxygenContentInTheBlast = 23.3,
                HotBlastTemperature = 974,
                MoistureContentInTheBlast = 10.7,
                AmountOfCarbonBurnedAtTheTuyeres = 288.4,
                NaturalGasConsumption = 68.2,
                HeatOfBurningOfNaturalGasOnFarms = 1590,
                HeatOfIncompleteBurningCarbonOfCoke = 9800,
                HeatCapacityOfCoke = 1.65,
                TemperatureOfCokeThatCameToTuyeres = 1500
            };
        }
    }
}
