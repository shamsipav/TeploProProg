using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeploPro.Models
{
    public class ProjectDataModel
    {
        // ПАРАМЕТРЫ ДУТЬЯ
        /// <summary>
        /// Температура дутья, С
        /// </summary>
        public double BlastTemperature { get; set; }
        /// <summary>
        /// Влажность дутья, г/м3
        /// </summary>
        public double BlastHumidity { get; set; }
        /// <summary>
        /// Содержание кислорода в дутье, %
        /// </summary>
        public double OxygenContentInBlast { get; set; }

        /// <summary>
        /// Расход природного газа, м3/т чугуна
        /// </summary>
        public double NaturalGasConsumption { get; set; }

        // ПАРАМЕТРЫ КОЛОШНИКОВОГО ГАЗА
        /// <summary>
        /// Давление колошникового газа, ати
        /// </summary>
        public double ColoshGasPressure { get; set; }

        // СОСТАВ ЧУГУНА
        /// <summary>
        /// Содержание Si в чугуне, %
        /// </summary>
        public double Chugun_SI { get; set; }
        /// <summary>
        /// Содержание Mn в чугуне, %
        /// </summary>
        public double Chugun_MN { get; set; }
        /// <summary>
        /// Содержание P в чугуне, %
        /// </summary>
        public double Chugun_P { get; set; }
        /// <summary>
        /// Содержание S в чугуне, %
        /// </summary>
        public double Chugun_S { get; set; }
        /// <summary>
        /// Содержание C в чугуне, %
        /// </summary>
        //public double Chugun_C { get; set; }

        // СОСТАВ КОКСА
        /// <summary>
        /// Содержание золы в коксе, %
        /// </summary>
        public double AshContentInCoke { get; set; }
        /// <summary>
        /// Содержание серы в коксе, %
        /// </summary>
        public double SulfurContentInCoke { get; set; }

        // ПОЛУЧЕНИЕ ИСХОДНЫХ ЗНАЧЕНИЙ
        public static ProjectDataModel GetDefaultData()
        {
            return new ProjectDataModel
            {
                BlastTemperature = 1250,
                BlastHumidity = 2.3,
                OxygenContentInBlast = 25.48,
                ColoshGasPressure = 1.38,
                NaturalGasConsumption = 128.3,
                Chugun_SI = 0.62,
                Chugun_MN = 0.318,
                Chugun_P = 0.088,
                Chugun_S = 0.023,
                //Chugun_C = 4.693,
                AshContentInCoke = 12.81,
                SulfurContentInCoke = 0.56,
            };
        }
    }
}
