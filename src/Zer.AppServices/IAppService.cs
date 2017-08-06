using System.Collections.Generic;

namespace Zer.AppServices
{
    public interface IAppService<TEntityDto,in TPrimaryKey>
    {
        TEntityDto GetById(TPrimaryKey id);
        List<TEntityDto> GetAll();

        TEntityDto Add(TEntityDto model);

        /// <summary>
        /// 进行批量新增时，请检查是否在数据库中已经存在数据
        /// </summary>
        List<TEntityDto> AddRange(List<TEntityDto> list);

    }

}
