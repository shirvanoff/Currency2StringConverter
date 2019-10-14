using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;
using Converter;

namespace Currency2StringConverterServer
{
    // NOTE: In order to launch WCF Test Client for testing this service, please select ConverterService.svc or ConverterService.svc.cs at the Solution Explorer and start debugging.
    public class ConverterService : IConverterService
    {
        private static string pattern = @"^(\d{0,9}(" + Regex.Escape(Converter.Properties.Resources.DecimalSeperator) + @"\d{1,2}){0,1}){1}$";
        private static readonly Regex rgx = new Regex(pattern);

        public string ConvertString(string value)
        {
            Logger.Log.Debug($"Entered into ConvertString({value})");
            try
            {
                var temp = value.Replace(" ", "");
                if (!CheckText(temp))
                    return string.Format(Converter.Properties.Resources.ErrorTextTemplate, value, Converter.Properties.Resources.DecimalSeperator);
                var converter = new NumericConverter { AddCurrency = true };
                var result = converter.Convert(temp);
                switch (result)
                {
                    case Success success:
                        return Convert.ToString(success.Value);
                    case Error error:
                        return error.ErrorMessage;
                    default:
                        return $"Something wrong with {value}";
                }
            }
            catch (Exception ex)
            {
                var msg = $"Error with ConvertString({value})";
                Logger.Log.Error(ex, msg);
                return msg;
            }
            finally
            {
                Logger.Log.Debug($"Finished ConvertString({value})");
            }
        }

        public string TestConnetion() => "OK";

        private bool CheckText(string value) => !string.IsNullOrWhiteSpace(value) && rgx.IsMatch(value);
    }
}