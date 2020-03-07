using Domain;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IBookLendsRepository
    {
        List<BookLend> LoadLends(BookGuid id);
        Task<BookLend> LoadLend(Guid id);
        Task Delete(BookLend obj);
        Task Add(BookLend newLend);
    }
}
