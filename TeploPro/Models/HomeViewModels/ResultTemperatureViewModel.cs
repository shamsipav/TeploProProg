using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeploPro.Models.HomeViewModels
{
    public class ResultTemperatureViewModel
    {
        public InputTemperatureModel Input { get; set; }
        public ResultTemperatureModel Result { get; set; }

        public ResultTemperatureViewModel(InputTemperatureModel input)
        {
            Input = input;
            Result = CalculateResult(input);
        }

        public ResultTemperatureModel CalculateResult(InputTemperatureModel input)
        {
            var result = new ResultTemperatureModel();

            result.BlastConsumptionRequiredForBurningOneKgOfCarbonCoke = (0.9333)/(0.01 * input.OxygenContentInTheBlast + 0.00124 * input.MoistureContentInTheBlast);
            result.BlastConsumptionForConversionOfOneMeterCoubOfNaturalGas = (0.5)/(0.01 * input.OxygenContentInTheBlast + 0.00124 * input.MoistureContentInTheBlast);
            result.OutputOfTheTuyereGasBurningAtTheTuyeres = 1.8667 + result.BlastConsumptionRequiredForBurningOneKgOfCarbonCoke * (1 - 0.01 * input.OxygenContentInTheBlast + 0.00124 * input.MoistureContentInTheBlast);
            result.OutputOfTuyereGasOfNaturalGasDuringСonversion = 3 + result.BlastConsumptionForConversionOfOneMeterCoubOfNaturalGas * (1 - 0.01 * input.OxygenContentInTheBlast + 0.00124 * input.MoistureContentInTheBlast);
            result.HeatCapacityOfDiatomicGasesAtHotBlastTemperature = 1.2897 + 0.000121 * input.HotBlastTemperature;
            result.HeatCapacityOfWaterVaporAtHotBlastTemperature = 1.4560 + 0.000282 * input.HotBlastTemperature;
            result.HeatContentOfHotBlast = result.HeatCapacityOfDiatomicGasesAtHotBlastTemperature * input.HotBlastTemperature - 0.00124 * input.MoistureContentInTheBlast * (10800 - result.HeatCapacityOfWaterVaporAtHotBlastTemperature * input.HotBlastTemperature);
            result.HeatContentOfCarbonOfCokeToTuyeres = input.HeatCapacityOfCoke * input.TemperatureOfCokeThatCameToTuyeres;
            result.NaturalGasConsumptionPerOneKgOfCoke = input.NaturalGasConsumption / input.AmountOfCarbonBurnedAtTheTuyeres;
            //result.HeatContentOfFurnaceGases = (input.HeatOfIncompleteBurningCarbonOfCoke + result.BlastConsumptionForConversionOfOneMeterCoubOfNaturalGas * result.HeatContentOfHotBlast + result.HeatContentOfCarbonOfCokeToTuyeres + result.NaturalGasConsumptionPerOneKgOfCoke * (input.HeatOfBurningOfNaturalGasOnFarms + result.BlastConsumptionForConversionOfOneMeterCoubOfNaturalGas))/(result.OutputOfTheTuyereGasBurningAtTheTuyeres + result.NaturalGasConsumptionPerOneKgOfCoke * result.OutputOfTuyereGasOfNaturalGasDuringСonversion);
            result.HeatContentOfFurnaceGases = (input.HeatOfIncompleteBurningCarbonOfCoke + result.BlastConsumptionRequiredForBurningOneKgOfCarbonCoke * result.HeatContentOfHotBlast + result.HeatContentOfCarbonOfCokeToTuyeres + result.NaturalGasConsumptionPerOneKgOfCoke * (input.HeatOfBurningOfNaturalGasOnFarms + result.BlastConsumptionForConversionOfOneMeterCoubOfNaturalGas * result.HeatContentOfHotBlast))/(result.OutputOfTheTuyereGasBurningAtTheTuyeres + result.NaturalGasConsumptionPerOneKgOfCoke * result.OutputOfTuyereGasOfNaturalGasDuringСonversion);
            result.TheoreticalBurningTemperatureOfCarbonCoke = 165 + 0.6113 * result.HeatContentOfFurnaceGases;

            return result;
        }
    }
}
