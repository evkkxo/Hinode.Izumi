﻿using System;

namespace Hinode.Izumi.Services.WebServices.CommandWebService.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandUsageAttribute : Attribute
    {
        public string[] Usages { get; }

        public CommandUsageAttribute(params string[] usages) => Usages = usages;
    }
}
