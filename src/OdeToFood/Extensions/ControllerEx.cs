using Microsoft.AspNet.Mvc;
using System;

namespace OdeToFood.Extensions
{
    public static class ControllerEx
    {
        public static string NameOf(this Type controller)
        {
            return controller.Name.Replace(nameof(Controller), string.Empty);
        }
    }
}
