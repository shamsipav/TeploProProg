using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeploPro.Models
{
    public class ProjectCalculateModel
    {
        public InputDataModel Input { get; set; }
        public ProjectDataModel Project { get; set; }
        public ReferenceModel Reference { get; set; }
        public ResultDataModel Result { get; set; }

        public ProjectCalculateModel()
        {

        }

        public ProjectCalculateModel(InputDataModel input, ProjectDataModel project, ReferenceModel reference)
        {
            Project = project;
            Reference = reference;
            Input = CalculateResult(input, project, reference);
        }

        public InputDataModel CalculateResult(InputDataModel input, ProjectDataModel projectData, ReferenceModel reference)
        {
            double cokeConsumption = input.SpecificConsumptionOfCoke;
            double furnanceCapacity = input.DailyСapacityOfFurnace;

            double changeCokeConsumption = 0;
            double changeFurnanceCapacity = 0;

            double blastTemperatureDifference = (projectData.BlastTemperature - input.BlastTemperature) / 10;
            // Сделать на форме ограничения
            if (input.BlastTemperature >= 800 && projectData.BlastTemperature <= 900)
            {
                changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionCoefficents.TemperatureIncreaseInRangeOf800to900 * blastTemperatureDifference / 100);
                changeFurnanceCapacity += (furnanceCapacity * reference.FurnanceCapacityCoefficents.TemperatureIncreaseInRangeOf800to900 * blastTemperatureDifference / 100);
            } else if (input.BlastTemperature >= 901 && projectData.BlastTemperature <= 1000)
            {
                changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionCoefficents.TemperatureIncreaseInRangeOf901to1000 * blastTemperatureDifference / 100);
                changeFurnanceCapacity += (furnanceCapacity * reference.FurnanceCapacityCoefficents.TemperatureIncreaseInRangeOf901to1000 * blastTemperatureDifference / 100);
            } else if (input.BlastTemperature >= 1001 && projectData.BlastTemperature <= 1100)
            {
                changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionCoefficents.TemperatureIncreaseInRangeOf1001to1100 * blastTemperatureDifference / 100);
                changeFurnanceCapacity += (furnanceCapacity * reference.FurnanceCapacityCoefficents.TemperatureIncreaseInRangeOf1001to1100 * blastTemperatureDifference / 100);
            } else if (input.BlastTemperature >= 1101 && projectData.BlastTemperature <= 1200)
            {
                changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionCoefficents.TemperatureIncreaseInRangeOf1101to1200 * blastTemperatureDifference / 100);
                changeFurnanceCapacity += (furnanceCapacity * reference.FurnanceCapacityCoefficents.TemperatureIncreaseInRangeOf1101to1200 * blastTemperatureDifference / 100);
            }

            double blastHumidityDifference = projectData.BlastHumidity - input.BlastHumidity;
            changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionCoefficents.IncreaseBlastHumidity * blastHumidityDifference / 100);
            changeFurnanceCapacity += (furnanceCapacity * reference.FurnanceCapacityCoefficents.IncreaseBlastHumidity * blastHumidityDifference / 100);

            double oxygenContentInBlastDifference = projectData.OxygenContentInBlast - input.OxygenContentInBlast;
            changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionCoefficents.IncreaseVolumeFractionOxygenInBlast * oxygenContentInBlastDifference / 100);
            changeFurnanceCapacity += (furnanceCapacity * reference.FurnanceCapacityCoefficents.IncreaseVolumeFractionOxygenInBlast * oxygenContentInBlastDifference / 100);

            double naturalGasConsumptionDifference = projectData.NaturalGasConsumption - input.NaturalGasConsumption;
            changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionCoefficents.IncreaseNaturalGasCunsimption * naturalGasConsumptionDifference / 100);
            changeFurnanceCapacity += (furnanceCapacity * reference.FurnanceCapacityCoefficents.IncreaseNaturalGasCunsimption * naturalGasConsumptionDifference / 100);

            double coloshGasPressureDifference = projectData.ColoshGasPressure - input.ColoshGasPressure;
            changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionCoefficents.IncreaseGasPressureUnderGrate * coloshGasPressureDifference / 100);
            changeFurnanceCapacity += (furnanceCapacity * reference.FurnanceCapacityCoefficents.IncreaseGasPressureUnderGrate * coloshGasPressureDifference / 100);

            double chugun_SIdifference = (projectData.Chugun_SI - input.Chugun_SI) / 0.1;
            changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionCoefficents.ReductionMassFractionOfSiliciumInChugun * chugun_SIdifference / 100);
            changeFurnanceCapacity += (furnanceCapacity * reference.FurnanceCapacityCoefficents.ReductionMassFractionOfSiliciumInChugun * chugun_SIdifference / 100);

            double chugun_MNdifference = (projectData.Chugun_MN - input.Chugun_MN) / 0.1;
            changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionCoefficents.IncreaseMassFractionOfManganeseInChugun * chugun_MNdifference / 100);
            changeFurnanceCapacity += (furnanceCapacity * reference.FurnanceCapacityCoefficents.IncreaseMassFractionOfManganeseInChugun * chugun_MNdifference / 100);

            double chugun_Pdifference = (projectData.Chugun_P - input.Chugun_P) / 0.1;
            changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionCoefficents.IncreaseMassFractionOfPhosphorusInChugun * chugun_Pdifference / 100);
            changeFurnanceCapacity += (furnanceCapacity * reference.FurnanceCapacityCoefficents.IncreaseMassFractionOfPhosphorusInChugun * chugun_Pdifference / 100);

            double chugun_Sdifference = (projectData.Chugun_S - input.Chugun_S) / 0.01;
            changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionCoefficents.ReductionMassFractionOfSeraInChugun * chugun_Sdifference / 100);
            changeFurnanceCapacity += (furnanceCapacity * reference.FurnanceCapacityCoefficents.ReductionMassFractionOfSeraInChugun * chugun_Sdifference / 100);

            double ashContentDifference = projectData.AshContentInCoke - input.AshContentInCoke;
            // Сделать на форме ограничения
            if (input.AshContentInCoke >= 11 && projectData.AshContentInCoke <= 12)
            {
                changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionCoefficents.ReductionMassFractionAshInCokeInRangeOf11to12Percent * ashContentDifference / 100);
                changeFurnanceCapacity += (furnanceCapacity * reference.FurnanceCapacityCoefficents.ReductionMassFractionAshInCokeInRangeOf11to12Percent * ashContentDifference / 100);
            } else if (input.AshContentInCoke >= 12 && projectData.AshContentInCoke <= 13)
            {
                changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionCoefficents.ReductionMassFractionAshInCokeInRangeOf12to13Percent * ashContentDifference / 100);
                changeFurnanceCapacity += (furnanceCapacity * reference.FurnanceCapacityCoefficents.ReductionMassFractionAshInCokeInRangeOf12to13Percent * ashContentDifference / 100);
            }

            double sulfurContentInCokeDifference = (projectData.SulfurContentInCoke - input.SulfurContentInCoke) / 0.01;
            changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionCoefficents.ReductionMassFractionOfSera * sulfurContentInCokeDifference / 100);
            changeFurnanceCapacity += (furnanceCapacity * reference.FurnanceCapacityCoefficents.ReductionMassFractionOfSera * sulfurContentInCokeDifference / 100);

            input.SpecificConsumptionOfCoke += changeCokeConsumption;
            input.DailyСapacityOfFurnace += changeFurnanceCapacity;

            return input;
        }
    }
}
