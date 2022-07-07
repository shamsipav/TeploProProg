using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeploPro.Models
{
    public class ResultTemperatureModel
    {
        /// <summary>
        /// Расход дутья, необходимый для сжигания 1 кг углерода кокса, м3/кг
        /// </summary>
        public double BlastConsumptionRequiredForBurningOneKgOfCarbonCoke { get; set; }

        /// <summary>
        /// Расход дутья, необходимый для конверсии 1 м3 природного газа, м3/м3
        /// </summary>
        public double BlastConsumptionForConversionOfOneMeterCoubOfNaturalGas{ get; set; }

        /// <summary>
        /// Выход фурменного газа в расчёте на 1 кг углерода кокса, сгорающего у фурм, м3/кг
        /// </summary>
        public double OutputOfTheTuyereGasBurningAtTheTuyeres { get; set; }

        /// <summary>
        /// Выход фурменного газа из 1 м3 природного газа при его конверсии в фурменном очаге, м3/м3
        /// </summary>
        public double OutputOfTuyereGasOfNaturalGasDuringСonversion { get; set; }

        /// <summary>
        /// Теплоёмкость двухатомных газов при температуре горячего дутья, кДж/(м3 * К)
        /// </summary>
        public double HeatCapacityOfDiatomicGasesAtHotBlastTemperature { get; set; }

        /// <summary>
        /// Теплоёмкость паров воды при температуре горячего дутья, кДж/(м3 * К)
        /// </summary>
        public double HeatCapacityOfWaterVaporAtHotBlastTemperature { get; set; }

        /// <summary>
        /// Теплосодержание горячего дутья за вычетом теплоты разложения влаги дутья, кДж/м3
        /// </summary>
        public double HeatContentOfHotBlast { get; set; }

        /// <summary>
        /// Теплосодержание углерода кокса пришедшего к фурмам, кДж/кг
        /// </summary>
        public double HeatContentOfCarbonOfCokeToTuyeres { get; set; }

        /// <summary>
        /// Расход природного газа в расчете на 1 кг кокса, приходящего к фурмам, м3/кг
        /// </summary>
        public double NaturalGasConsumptionPerOneKgOfCoke { get; set; }

        /// <summary>
        /// Теплосодержание горновых газов, кДж/м3
        /// </summary>
        public double HeatContentOfFurnaceGases { get; set; }

        /// <summary>
        /// Теоретическая температура горения углерода кокса, °С
        /// </summary>
        public double TheoreticalBurningTemperatureOfCarbonCoke { get; set; }
    }
}
