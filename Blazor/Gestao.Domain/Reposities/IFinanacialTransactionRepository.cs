﻿using Gestao.Client.Libraries.Utilities;
using Gestao.Domain;
using Gestao.Domain.Enums;

namespace Gestao.Domain.Repositories
{
    public interface IFinanacialTransactionRepository
    {
        Task Add(FinancialTransaction entity);
        Task Delete(int id);
        Task<FinancialTransaction?> Get(int id);
        Task<PaginatedList<FinancialTransaction>> GetAll(int companyId, TypeFinancialTransaction type, int pageIndex, int pageSize, string searchWord);
        Task Update(FinancialTransaction entity);
    }
}