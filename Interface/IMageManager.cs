using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;

namespace RabbiBot_Backend.Interface
{
    interface IImageManager
    {
        Task<int> Count(string search);

        Task<int> Update(Images images);

        Task<Images> GetById(int Id);

        Task<List<Images>> ListAll(int skip, int take, string orderBy, string direction, string search);
    }
}
