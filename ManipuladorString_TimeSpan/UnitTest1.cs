using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ManipuladorString_TimeSpan
{
    [TestClass]
    public class UnitTest1
    {
        private string horaInicio { get; set; }
        private string horaTermino { get; set; }
        private string stringTempoReconexao { get; set; }

        private System.Globalization.NumberStyles style { get; set; }
        private System.Globalization.CultureInfo culture { get; set; }
        private string[] valor { get; set; }

        [TestInitialize]
        public void Setup()
        {
            style = System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowThousands;
            culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

            valor = new string[] {"1. 234    567,8998001",
                                "1234567.89",
                                "999.888.777.34",
                                "999,888,777,34",
                                "999,,,.888..,777.,34",
                                "10001", 
                                "1.005",
                                "1.012,89", 
                                "1,001.98",
                                "63,00",
                                "63,22", 
                                "15.00",
                                "15.33",
                                "1 234 567,89",
                                "1 234 567.89",
                                "1,234,567.89",
                                "123456789",
                                "1234567,89",
                                };
        }

        //[TestMethod]
        [Ignore]
        public void TestMethod1()
        {
            double dAchievement = 0;

            for (int i = 0; i < valor.Length; i++)
            {
                var val = FormataValorMonetario(valor[i]);

                if (!string.IsNullOrWhiteSpace(val))

                    Assert.IsTrue(double.TryParse(val, style, culture, out dAchievement), "VALOR NÃO CORRESPONDE AO PADRÃO [en-US].");

                System.Diagnostics.Debug.WriteLine("dAchievement:= " + dAchievement.ToString(culture));
            }
        }

        [Ignore]
        public void VerificarArray()
        {
            var campos = new string[] { "c.us,tom,,.Nu.m.1io", "customNum2", "customNum3" };

            for (int i = 0; i < campos.Length; i++)
            {
                var texto = campos[i].Replace(",", "");

                System.Diagnostics.Debug.WriteLine("texto formatado sem virgula " + texto);

                System.Diagnostics.Debug.WriteLine("posição casa:= " + campos[i].LastIndexOf('.'));
                System.Diagnostics.Debug.WriteLine("posição casa:= " + campos[i].LastIndexOf('.'));

                System.Diagnostics.Debug.WriteLine(campos[i].Substring(campos[i].LastIndexOf('.')));
                System.Diagnostics.Debug.WriteLine(campos[i].Substring(campos[i].LastIndexOf('.')).Replace(".", "").Length);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public string FormataValorMonetario(string valor)
        {
            if (!string.IsNullOrWhiteSpace(valor))
            {
                valor = valor.Replace(" ", "");

                var countChar = valor.Count(c => c == ',' || c == '.');

                if (countChar > 1)
                {
                    var texto_semvirgula = valor.Trim().Replace(",", ".");

                    var total_char = texto_semvirgula.Substring(texto_semvirgula.LastIndexOf('.')).Replace(".", "").Length;

                    valor = valor.Trim().Replace(",", "").Replace(".", "");

                    valor = valor.Substring(0, valor.Length - total_char) + '.' + valor.Substring(valor.Length - total_char);

                }
                else if (valor.IndexOf(',') > -1)
                {
                    valor = valor.Replace(",", ".");
                }
            }

            return valor;
        }

        [TestMethod]
        public void Validarparacampohora()
        {
            horaInicio = "00:55";
            horaTermino = "23:44";

            TimeSpan AGHoraInicio;
            TimeSpan AGHoraTermino;

            Assert.IsTrue(TimeSpan.TryParse(horaInicio, out AGHoraInicio), "Campo 'Horário de execução' inválido!!!");
            Assert.IsTrue(TimeSpan.TryParse(horaTermino, out AGHoraTermino), "Campo 'Horário linmite de processamento' inválido!!!");

            //-1 = t1 é menor que t2.
            //0 = t1 é igual a t2.
            //1 = t1 é maior que t2.
            Assert.IsTrue((TimeSpan.Compare(AGHoraInicio, AGHoraTermino) == -1));
            Assert.IsTrue((AGHoraInicio - AGHoraTermino).Duration() > AGHoraInicio);

            //System.Diagnostics.Debug.WriteLine();

        }
    }
}
