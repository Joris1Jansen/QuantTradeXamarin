using System;
using Newtonsoft.Json;
using Xamarin.UITest;
using Xamarin.UITest.Android;

namespace QuantTrade.UITest
{
    public static class BackdoorService
    {
        public static object InvokeBackdoorMethod(this IApp app, string backdoorMethodName, string parameter = "") => app switch
        {
            AndroidApp androidApp when string.IsNullOrWhiteSpace(parameter) => androidApp.Invoke(backdoorMethodName),
            AndroidApp androidApp => androidApp.Invoke(backdoorMethodName, parameter),
            _ => throw new NotSupportedException("Platform Not Supported"),
        };

        public static T InvokeBackdoorMethod<T>(this IApp app, string backdoorMethodName, string parameter = "")
        {
            var result = app.InvokeBackdoorMethod(backdoorMethodName, parameter).ToString();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(result) ?? throw new JsonException();
        }
    }
}