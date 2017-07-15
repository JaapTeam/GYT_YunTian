

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
