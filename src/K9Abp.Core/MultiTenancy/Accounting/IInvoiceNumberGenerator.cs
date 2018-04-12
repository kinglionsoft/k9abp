﻿using System.Threading.Tasks;
using Abp.Dependency;

namespace K9Abp.Core.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}
