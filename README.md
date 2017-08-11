
# 业务规则说明
## LNG补贴信息导入
- 重复数据检查规则
    ```
    public bool Exists(LngAllowanceInfoDto lngAllowanceInfoDto)
    {
    	return _lngAllowanceInfoDataService.GetAll()
    		.Any(x => x.TruckNo == lngAllowanceInfoDto.TruckNo ||
    				  x.CylinderDefaultId == lngAllowanceInfoDto.CylinderDefaultId);
    }
    ```

# EF集成测试
- 运行 ```Zer.Framework.Ef.IntegrationTest.Tests``` 之前，请先运行migration
- > ```update-database```
- 在运行 Update-Database 的时候指定 ```-Specify``` 标记，我们就能够使得这些更改被写入一个脚本中而不是被应用，我们同时也会为此脚本指定源迁移和目标迁移，例如我们希望产生的脚本是从一个空数据库（```$InitialDatabase```）到最新的版本（```AddPostAbstract``` 迁移）;
__（注意：如果你没有指定目标迁移，那么迁移将始终更新至最新版本；如果你没有指定源迁移，那么迁移将以数据库目前状态为初始）__
在 Package Manager Console 中运行命令
>> __```Update-Database -Script -SourceMigration: $InitialDatabase -TargetMigration: AddPostAbstract```__

### 增加SortAttribute 
> 为了在导入和导出时对数据列进行排序，增加此属性
> 使用案例:

- Export 的 Dto 定义方法如下：

```
    public class CompanyInfoDto
    {
        [ExportDisplayName("编号")]
        [Sort(1)]
        public int Id { get; set; }

        [ExportDisplayName("公司名称")]
        [Sort(3)]
        public string CompanyName { get; set; }

        [ExportDisplayName("经营范围")]
        [Sort(2)]
        public string TraderRange { get; set; }

        [ExportDisplayName("创建日期")]
        [Sort(4)]
        public DateTime CreateDate { get; set; }
    }
```

- Import 的 Dto 定义方法如下：

```
public class CompanyInfoDto
{
    [Sort(1)]
    public int Id { get; set; }

    [Sort(3)]
    public string CompanyName { get; set; }

    [Sort(2)]
    public string TraderRange { get; set; }

    [Sort(4)]
    public DateTime CreateDate { get; set; }
}
```
