using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TeploPro.Models;
using TeploPro.Models.HomeViewModels;

// Версия для оформления свидетельства государственной регистрации
namespace TeploPro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext _context;
        public HomeController(ApplicationContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Index(InputDataModel input, bool isSaveInputs, int? inputId)
        {
            input.DateOfResult = DateTime.Now;

            if (isSaveInputs)
            {
                _context.InputData.Add(input);
                await _context.SaveChangesAsync();

            }
            if (inputId != null)
            {
                var existInputData = _context.InputData.FirstOrDefault(x => x.Id == inputId);

                if (existInputData != null)
                {
                    existInputData.NumberOfFurnace = input.NumberOfFurnace;
                    existInputData.UsefulVolumeOfFurnace = input.UsefulVolumeOfFurnace;
                    existInputData.UsefulHeightOfFurnace = input.UsefulHeightOfFurnace;
                    existInputData.DiameterOfColoshnik = input.DiameterOfColoshnik;
                    existInputData.DiameterOfRaspar = input.DiameterOfRaspar;
                    existInputData.DiameterOfHorn = input.DiameterOfHorn;
                    existInputData.HeightOfHorn = input.HeightOfHorn;
                    existInputData.HeightOfTuyeres = input.HeightOfTuyeres;
                    existInputData.HeightOfZaplechiks = input.HeightOfZaplechiks;
                    existInputData.HeightOfRaspar = input.HeightOfRaspar;
                    existInputData.HeightOfShaft = input.HeightOfShaft;
                    existInputData.HeightOfColoshnik = input.HeightOfColoshnik;
                    existInputData.EstablishedLevelOfEmbankment = input.EstablishedLevelOfEmbankment;
                    existInputData.NumberOfTuyeres = input.NumberOfTuyeres;
                    existInputData.DailyСapacityOfFurnace = input.DailyСapacityOfFurnace;
                    existInputData.SpecificConsumptionOfCoke = input.SpecificConsumptionOfCoke;
                    existInputData.SpecificConsumptionOfZRM = input.SpecificConsumptionOfZRM;
                    existInputData.ShareOfPelletsInCharge = input.ShareOfPelletsInCharge;
                    existInputData.BlastConsumption = input.BlastConsumption;
                    existInputData.BlastTemperature = input.BlastTemperature;
                    existInputData.BlastPressure = input.BlastPressure;
                    existInputData.BlastHumidity = input.BlastHumidity;
                    existInputData.OxygenContentInBlast = input.OxygenContentInBlast;
                    existInputData.NaturalGasConsumption = input.NaturalGasConsumption;
                    existInputData.ColoshGasTemperature = input.ColoshGasTemperature;
                    existInputData.ColoshGasPressure = input.ColoshGasPressure;
                    existInputData.ColoshGas_CO = input.ColoshGas_CO;
                    existInputData.ColoshGas_CO2 = input.ColoshGas_CO2;
                    existInputData.ColoshGas_H2 = input.ColoshGas_H2;
                    existInputData.Chugun_SI = input.Chugun_SI;
                    existInputData.Chugun_MN = input.Chugun_MN;
                    existInputData.Chugun_P = input.Chugun_P;
                    existInputData.Chugun_S = input.Chugun_S;
                    existInputData.Chugun_C = input.Chugun_C;
                    existInputData.AshContentInCoke = input.AshContentInCoke;
                    existInputData.VolatileContentInCoke = input.VolatileContentInCoke;
                    existInputData.SulfurContentInCoke = input.SulfurContentInCoke;
                    existInputData.SpecificSlagYield = input.SpecificSlagYield;
                    existInputData.HeatCapacityOfAgglomerate = input.HeatCapacityOfAgglomerate;
                    existInputData.HeatCapacityOfPellets = input.HeatCapacityOfPellets;
                    existInputData.HeatCapacityOfCoke = input.HeatCapacityOfCoke;
                    existInputData.AcceptedTemperatureOfBackupZone = input.AcceptedTemperatureOfBackupZone;
                    existInputData.ProportionOfHeatLossesOfLowerPart = input.ProportionOfHeatLossesOfLowerPart;
                    existInputData.AverageSizeOfPieceCharge = input.AverageSizeOfPieceCharge;

                    existInputData.HeatOfBurningOfNaturalGasOnFarms = input.HeatOfBurningOfNaturalGasOnFarms;
                    existInputData.HeatOfIncompleteBurningCarbonOfCoke = input.HeatOfIncompleteBurningCarbonOfCoke;
                    existInputData.TemperatureOfCokeThatCameToTuyeres = input.TemperatureOfCokeThatCameToTuyeres;
                    //existInputData.DateOfResult = DateTime.Now;

                    _context.InputData.Update(existInputData);
                    await _context.SaveChangesAsync();
                }

            }

            return RedirectToAction("Result", input);
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                InputDataModel input = await _context.InputData.FirstOrDefaultAsync(p => p.Id == id);

                ViewData["Variant"] = input.Id;
                ViewData["VariantDateTime"] = input.DateOfResult;

                ViewBag.IsSavedInputData = true;

                if (input != null)
                    return View(input);
            }
            else
            {
                InputDataModel inputData = InputDataModel.GetDefaultData();

                ViewBag.IsSavedInputData = false;

                return View(inputData);
            }
            return NotFound();
        }

        public IActionResult Result(InputDataModel input)
        {
            ResultViewModel viewModel = new ResultViewModel(input);

            return View(viewModel);
        }

        public async Task<IActionResult> Inputs()
        {
            var inputData = await _context.InputData.ToListAsync();

            return View(inputData);
        }

        [HttpPost]
        public string DeleteInputVariant(int? inputId)
        {
            if (inputId != null)
            {
                InputDataModel deleteInput = _context.InputData.FirstOrDefault(d => d.Id == inputId);
                _context.InputData.Remove(deleteInput);
                _context.SaveChanges();

                return JsonConvert.SerializeObject(deleteInput);
            }

            return null;
        }

        [HttpPost]
        public JsonResult GetInputData(int? inputId)
        {
            InputDataModel inputData = _context.InputData.FirstOrDefault(d => d.Id == inputId);
            return Json(inputData);
        }

        [HttpPost]
        public async Task<IActionResult> Comparison(int? baseInputDataId, int? comparativeInputDataId)
        {
            var basePeriodInput = await _context.InputData.FirstOrDefaultAsync(u => u.Id == baseInputDataId);
            ResultViewModel viewModelBase = new ResultViewModel(basePeriodInput);

            var comparativePeriodInput = await _context.InputData.FirstOrDefaultAsync(u => u.Id == comparativeInputDataId);
            ResultViewModel viewModelComparative = new ResultViewModel(comparativePeriodInput);

            ViewBag.Inputs = await _context.InputData.ToListAsync();

            var baseData_1 = viewModelBase.Result.IndexOfTheBottomOfTheFurnace;
            var baseData_2 = viewModelBase.Result.IndexOfTheFurnaceTop;
            var baseData_3 = viewModelBase.Result.TheoreticalBurningTemperatureOfCarbonCoke;

            var compData_1 = viewModelComparative.Result.IndexOfTheBottomOfTheFurnace;
            var compData_2 = viewModelComparative.Result.IndexOfTheFurnaceTop;
            var compData_3 = viewModelComparative.Result.TheoreticalBurningTemperatureOfCarbonCoke;


            string baseDataJson_1 = JsonConvert.SerializeObject(baseData_1);
            string baseDataJson_2 = JsonConvert.SerializeObject(baseData_2);
            string baseDataJson_3 = JsonConvert.SerializeObject(baseData_3);

            string compDataJson_1 = JsonConvert.SerializeObject(compData_1);
            string compDataJson_2 = JsonConvert.SerializeObject(compData_2);
            string compDataJson_3 = JsonConvert.SerializeObject(compData_3);

            ViewBag.baseData_1 = baseDataJson_1;
            ViewBag.baseData_2 = baseDataJson_2;
            ViewBag.baseData_3 = baseDataJson_3;

            ViewBag.compData_1 = compDataJson_1;
            ViewBag.compData_2 = compDataJson_2;
            ViewBag.compData_3 = compDataJson_3;

            return View(new ComparisonViewModel { BasePeriodData = viewModelBase, СomparativePeriodData = viewModelComparative });
        }

        public async Task<IActionResult> Comparison()
        {
            ViewBag.Inputs = await _context.InputData.ToListAsync();

            return View();
        }

        public IActionResult CoefficientsReference()
        {
            СoefficientsReference cokeCoefficients = _context.Сoefficients.FirstOrDefault(i => i.Id == 1);
            СoefficientsReference furnanceCapacityCoefficients = _context.Сoefficients.FirstOrDefault(i => i.Id == 2);

            ReferenceModel referenceModel = new ReferenceModel { CokeCunsumptionCoefficents = cokeCoefficients, FurnanceCapacityCoefficents = furnanceCapacityCoefficients };

            return View(referenceModel);
        }

        [HttpPost]
        public IActionResult CoefficientsReference(ReferenceModel referenceModel)
        {
            if (referenceModel != null)
            {
                СoefficientsReference cokeCofficients = _context.Сoefficients.FirstOrDefault(i => i.Id == 1);
                СoefficientsReference furnanceCapacityCoefficients = _context.Сoefficients.FirstOrDefault(i => i.Id == 2);

                cokeCofficients.IronMassFractionIncreaseInOreRash = referenceModel.CokeCunsumptionCoefficents.IronMassFractionIncreaseInOreRash;
                cokeCofficients.ShareCrudeOreReductionCharge = referenceModel.CokeCunsumptionCoefficents.ShareCrudeOreReductionCharge;
                cokeCofficients.TemperatureIncreaseInRangeOf800to900 = referenceModel.CokeCunsumptionCoefficents.TemperatureIncreaseInRangeOf800to900;
                cokeCofficients.TemperatureIncreaseInRangeOf901to1000 = referenceModel.CokeCunsumptionCoefficents.TemperatureIncreaseInRangeOf901to1000;
                cokeCofficients.TemperatureIncreaseInRangeOf1001to1100 = referenceModel.CokeCunsumptionCoefficents.TemperatureIncreaseInRangeOf1001to1100;
                cokeCofficients.TemperatureIncreaseInRangeOf1101to1200 = referenceModel.CokeCunsumptionCoefficents.TemperatureIncreaseInRangeOf1101to1200;
                cokeCofficients.IncreaseGasPressureUnderGrate = referenceModel.CokeCunsumptionCoefficents.IncreaseGasPressureUnderGrate;
                cokeCofficients.ReductionMassFractionOfSiliciumInChugun = referenceModel.CokeCunsumptionCoefficents.ReductionMassFractionOfSiliciumInChugun;
                cokeCofficients.ReductionMassFractionOfSeraInChugun = referenceModel.CokeCunsumptionCoefficents.ReductionMassFractionOfSeraInChugun;
                cokeCofficients.IncreaseMassFractionOfPhosphorusInChugun = referenceModel.CokeCunsumptionCoefficents.IncreaseMassFractionOfPhosphorusInChugun;
                cokeCofficients.IncreaseMassFractionOfManganeseInChugun = referenceModel.CokeCunsumptionCoefficents.IncreaseMassFractionOfManganeseInChugun;
                cokeCofficients.IncreaseMassFractionOfTitanInChugun = referenceModel.CokeCunsumptionCoefficents.IncreaseMassFractionOfTitanInChugun;
                cokeCofficients.IncreaseBlastHumidity = referenceModel.CokeCunsumptionCoefficents.IncreaseBlastHumidity;
                cokeCofficients.IncreaseNaturalGasCunsimption = referenceModel.CokeCunsumptionCoefficents.IncreaseNaturalGasCunsimption;
                cokeCofficients.OutputFromLimestoneCharge = referenceModel.CokeCunsumptionCoefficents.OutputFromLimestoneCharge;
                cokeCofficients.IncreaseVolumeFractionOxygenInBlast = referenceModel.CokeCunsumptionCoefficents.IncreaseVolumeFractionOxygenInBlast;
                cokeCofficients.ReductionMassFractionTrifles = referenceModel.CokeCunsumptionCoefficents.ReductionMassFractionTrifles;
                cokeCofficients.ReductionMassFractionAshInCokeInRangeOf11to12Percent = referenceModel.CokeCunsumptionCoefficents.ReductionMassFractionAshInCokeInRangeOf11to12Percent;
                cokeCofficients.ReductionMassFractionAshInCokeInRangeOf12to13Percent = referenceModel.CokeCunsumptionCoefficents.ReductionMassFractionAshInCokeInRangeOf12to13Percent;
                cokeCofficients.ReductionMassFractionOfSera = referenceModel.CokeCunsumptionCoefficents.ReductionMassFractionOfSera;

                furnanceCapacityCoefficients.IronMassFractionIncreaseInOreRash = referenceModel.FurnanceCapacityCoefficents.IronMassFractionIncreaseInOreRash;
                furnanceCapacityCoefficients.ShareCrudeOreReductionCharge = referenceModel.FurnanceCapacityCoefficents.ShareCrudeOreReductionCharge;
                furnanceCapacityCoefficients.TemperatureIncreaseInRangeOf800to900 = referenceModel.FurnanceCapacityCoefficents.TemperatureIncreaseInRangeOf800to900;
                furnanceCapacityCoefficients.TemperatureIncreaseInRangeOf901to1000 = referenceModel.FurnanceCapacityCoefficents.TemperatureIncreaseInRangeOf901to1000;
                furnanceCapacityCoefficients.TemperatureIncreaseInRangeOf1001to1100 = referenceModel.FurnanceCapacityCoefficents.TemperatureIncreaseInRangeOf1001to1100;
                furnanceCapacityCoefficients.TemperatureIncreaseInRangeOf1101to1200 = referenceModel.FurnanceCapacityCoefficents.TemperatureIncreaseInRangeOf1101to1200;
                furnanceCapacityCoefficients.IncreaseGasPressureUnderGrate = referenceModel.FurnanceCapacityCoefficents.IncreaseGasPressureUnderGrate;
                furnanceCapacityCoefficients.ReductionMassFractionOfSiliciumInChugun = referenceModel.FurnanceCapacityCoefficents.ReductionMassFractionOfSiliciumInChugun;
                furnanceCapacityCoefficients.ReductionMassFractionOfSeraInChugun = referenceModel.FurnanceCapacityCoefficents.ReductionMassFractionOfSeraInChugun;
                furnanceCapacityCoefficients.IncreaseMassFractionOfPhosphorusInChugun = referenceModel.FurnanceCapacityCoefficents.IncreaseMassFractionOfPhosphorusInChugun;
                furnanceCapacityCoefficients.IncreaseMassFractionOfManganeseInChugun = referenceModel.FurnanceCapacityCoefficents.IncreaseMassFractionOfManganeseInChugun;
                furnanceCapacityCoefficients.IncreaseMassFractionOfTitanInChugun = referenceModel.FurnanceCapacityCoefficents.IncreaseMassFractionOfTitanInChugun;
                furnanceCapacityCoefficients.IncreaseBlastHumidity = referenceModel.FurnanceCapacityCoefficents.IncreaseBlastHumidity;
                furnanceCapacityCoefficients.IncreaseNaturalGasCunsimption = referenceModel.FurnanceCapacityCoefficents.IncreaseNaturalGasCunsimption;
                furnanceCapacityCoefficients.OutputFromLimestoneCharge = referenceModel.FurnanceCapacityCoefficents.OutputFromLimestoneCharge;
                furnanceCapacityCoefficients.IncreaseVolumeFractionOxygenInBlast = referenceModel.FurnanceCapacityCoefficents.IncreaseVolumeFractionOxygenInBlast;
                furnanceCapacityCoefficients.ReductionMassFractionTrifles = referenceModel.FurnanceCapacityCoefficents.ReductionMassFractionTrifles;
                furnanceCapacityCoefficients.ReductionMassFractionAshInCokeInRangeOf11to12Percent = referenceModel.FurnanceCapacityCoefficents.ReductionMassFractionAshInCokeInRangeOf11to12Percent;
                furnanceCapacityCoefficients.ReductionMassFractionAshInCokeInRangeOf12to13Percent = referenceModel.FurnanceCapacityCoefficents.ReductionMassFractionAshInCokeInRangeOf12to13Percent;
                furnanceCapacityCoefficients.ReductionMassFractionOfSera = referenceModel.FurnanceCapacityCoefficents.ReductionMassFractionOfSera;


                _context.Сoefficients.Update(cokeCofficients);
                _context.Сoefficients.Update(furnanceCapacityCoefficients);
                _context.SaveChanges();
            }

            return View(referenceModel);
        }

        public IActionResult ProjectMode()
        {
            ProjectDataModel projectData = ProjectDataModel.GetDefaultData();

            ViewBag.Inputs = _context.InputData.ToList();

            // fixed (?) to simple class
            ProjectViewModel projectView = new ProjectViewModel { Project = projectData };

            return View(projectView);
        }

        [HttpPost]
        public IActionResult ProjectMode(ProjectViewModel projectView, int? inputDataId)
        {
            InputDataModel inputData = _context.InputData.FirstOrDefault(d => d.Id == inputDataId);

            // Из базы
            СoefficientsReference cokeCoefficients = _context.Сoefficients.FirstOrDefault(i => i.Id == 1);
            СoefficientsReference furnanceCapacityCoefficients = _context.Сoefficients.FirstOrDefault(i => i.Id == 2);

            ReferenceModel reference = new ReferenceModel { CokeCunsumptionCoefficents = cokeCoefficients, FurnanceCapacityCoefficents = furnanceCapacityCoefficients };

            ProjectCalculateModel projectResult = new ProjectCalculateModel(inputData, projectView.Project, reference);

            inputData.BlastTemperature = projectResult.Project.BlastTemperature;
            inputData.BlastHumidity = projectResult.Project.BlastHumidity;
            inputData.OxygenContentInBlast = projectResult.Project.OxygenContentInBlast;
            inputData.ColoshGasPressure = projectResult.Project.ColoshGasPressure;
            inputData.NaturalGasConsumption = projectResult.Project.NaturalGasConsumption;
            inputData.Chugun_SI = projectResult.Project.Chugun_SI;
            inputData.Chugun_MN = projectResult.Project.Chugun_MN;
            inputData.Chugun_P = projectResult.Project.Chugun_P;
            inputData.Chugun_S = projectResult.Project.Chugun_S;
            inputData.AshContentInCoke = projectResult.Project.AshContentInCoke;
            inputData.SulfurContentInCoke = projectResult.Project.SulfurContentInCoke;

            inputData.DateOfResult = DateTime.Now;

            return RedirectToAction("ProjectResult", projectResult.Input);
        }

        public IActionResult ProjectResult(InputDataModel inputProject) 
        {
            InputDataModel inputBase = _context.InputData.FirstOrDefault(d => d.Id == inputProject.Id);

            ResultViewModel baseResult = new ResultViewModel(inputBase);

            ResultViewModel projectResult = new ResultViewModel(inputProject);

            var baseData_1 = baseResult.Result.IndexOfTheBottomOfTheFurnace;
            var baseData_2 = baseResult.Result.IndexOfTheFurnaceTop;
            var baseData_3 = baseResult.Result.TheoreticalBurningTemperatureOfCarbonCoke;

            var progData_1 = projectResult.Result.IndexOfTheBottomOfTheFurnace;
            var progData_2 = projectResult.Result.IndexOfTheFurnaceTop;
            var progData_3 = projectResult.Result.TheoreticalBurningTemperatureOfCarbonCoke;


            string baseDataJson_1 = JsonConvert.SerializeObject(baseData_1);
            string baseDataJson_2 = JsonConvert.SerializeObject(baseData_2);
            string baseDataJson_3 = JsonConvert.SerializeObject(baseData_3);

            string progDataJson_1 = JsonConvert.SerializeObject(progData_1);
            string progDataJson_2 = JsonConvert.SerializeObject(progData_2);
            string progDataJson_3 = JsonConvert.SerializeObject(progData_3);

            ViewBag.baseData_1 = baseDataJson_1;
            ViewBag.baseData_2 = baseDataJson_2;
            ViewBag.baseData_3 = baseDataJson_3;

            ViewBag.progData_1 = progDataJson_1;
            ViewBag.progData_2 = progDataJson_2;
            ViewBag.progData_3 = progDataJson_3;

            return View(new ProjectResultViewModel { BaseResult = baseResult, ProjectResult = projectResult });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
