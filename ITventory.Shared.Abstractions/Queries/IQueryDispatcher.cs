using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Shared.Abstractions.Queries
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);

        //Zwracamy task o typie TResult - realizowane przy pomocy metody
        //QueryAsync - która zwraca dany typ TResult. Do tego wykorzystywany jest parametr 'query', który jest
        //typu IQuery<TResult>. Musi implementować ten interfejs
    }
}
