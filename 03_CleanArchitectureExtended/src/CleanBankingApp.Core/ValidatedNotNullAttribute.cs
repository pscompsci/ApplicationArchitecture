using System;
using System.Collections.Generic;
using System.Text;

namespace CleanBankingApp.Core
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public sealed class ValidatedNotNullAttribute : Attribute { }
}
